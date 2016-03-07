using System;
using System.Collections.Generic;
using System.Linq;

using Google.Apis.Calendar.v3.Data;
using Marble.Data;
using Marble.Google;

namespace Marble
{
    /// <summary>
    /// Description of CalendarSync.
    /// </summary>
    public class CalendarSync
    {
        readonly IOutlookCalendarService outlookCalendarService;

        readonly GoogleClient googleClient;
        readonly GoogleCalendarService googleCalendarService;
        readonly OutlookServiceProvider sourceCalendarProvider;

        public CalendarSync()
        {
            googleClient = new GoogleClient(Settings.DataStoreFolderNameCalendar);
            googleCalendarService = new GoogleCalendarService(googleClient);

            sourceCalendarProvider = (OutlookServiceProvider)Enum.Parse(typeof(OutlookServiceProvider), Settings.OutlookCalendarServiceProvider);
            if (sourceCalendarProvider == OutlookServiceProvider.Interop)
            {
                outlookCalendarService = new OulookCalendarService_Introp();
            }
            else if (sourceCalendarProvider == OutlookServiceProvider.Exchange)
            {
                outlookCalendarService = new Exchange.ExchangeService();
            }
            else
            {
                outlookCalendarService = new OutlookCalendarService();
            }
        }

        public CalendarSyncInfo Sync()
        {
        	//ClearAllRemoteItems();
        	
        	var syncInfo = new CalendarSyncInfo();
        	
        	if (!ValidateSettings()) 
        	{
        		syncInfo.Status = CalendarSyncStatus.Skipped;
        		return syncInfo;
        	}

            List<Appointment> outlookAppoinments = outlookCalendarService.GetAppointmentsInRange();
            List<Appointment> googleAppoinments = googleCalendarService.GetAppointmentsInRange();

            var comparer = new AppointmentComparer();

            var googleItemsToDelete = googleAppoinments.Except(outlookAppoinments, comparer).ToList();
            //syncInfo.ItemsRemovedCount = googleItemsToDelete.Count();
            syncInfo.ItemsRemovedCount = googleAppoinments.Count();
            RemoveOldCalendarEventsFromGoogleCalendar(googleAppoinments);
            //RemoveOldCalendarEventsFromGoogleCalendar(googleItemsToDelete);

            //var googleItemsToAdd = outlookAppoinments.Except(googleAppoinments, comparer).ToList();
            //syncInfo.ItemsAddCount = googleItemsToAdd.Count();
            syncInfo.ItemsAddCount = googleAppoinments.Count();
            //AddOutLookEventsToGoogleCalendar(googleItemsToAdd);
            AddOutLookEventsToGoogleCalendar(outlookAppoinments);

            syncInfo.Status = CalendarSyncStatus.Success;
            syncInfo.Text = "Synchronization complete.";
            if (Globals.HasError)
            {
            	syncInfo.Status = CalendarSyncStatus.Failed;
            	syncInfo.Text = Globals.ErrorMessage;
            }

            return syncInfo;
        }

        bool ValidateSettings()
        {
        	var valid = true;
        	
        	var alertMessages = new List<string>();
            if (Settings.CalendarId == "")
            {
            	alertMessages.Add("Google Calendar not selected.");
            }
            
            if ((sourceCalendarProvider == OutlookServiceProvider.Exchange) && 
                (string.IsNullOrEmpty(Settings.ExchangeEmailAddress)))
            {
            	
            	alertMessages.Add("You must provide your Exchange email address if selecting to connect directly to Exchange server."); 	
            }
            	
            if (alertMessages.Count > 0)
            {
            	valid = false;
            	var message = string.Empty;
            	foreach (var alertMessage in alertMessages) {
            		message += alertMessage + "\n";
            	}
            	System.Windows.Forms.MessageBox.Show(message, "Configuration Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Stop);
            }
            
            return valid;
        }
        
        string[] splitAttendees(string attendees)
        {
            if (attendees == null) return new string[0];
            string[] tmp1 = attendees.Split(';');
            for (int i = 0; i < tmp1.Length; i++) tmp1[i] = tmp1[i].Trim();
            
            return tmp1;
        }

        void RemoveOldCalendarEventsFromGoogleCalendar(List<Appointment> items)
        {
            if (items.Count > 0)
            {
                foreach (var item in items) googleCalendarService.DeleteCalendarEntry(Settings.CalendarAccount, item.Id);
            }
        }

        void AddOutLookEventsToGoogleCalendar(List<Appointment> items)
        {
            if (items.Count > 0)
            {
                foreach (Appointment item in items)
                {
                    var googleEvent = new Event
                    {
                        Summary = item.Summary,
                        Location = item.Location,
                        Description = item.Description,
                        Attendees = new List<EventAttendee>()
                    };

                    var currentTimeZone = TimeZone.CurrentTimeZone;

                    var startDateTime = new DateTimeOffset(item.Start, currentTimeZone.GetUtcOffset(item.Start));
                    var startDate = new EventDateTime();
                    
                    if (item.IsAllDayEvent)
                    {
                        startDate.Date = startDateTime.ToString("yyy-MM-dd");
                    }
                    else
                    {
                        startDate.DateTime = startDateTime.DateTime;
                    }

                    //startDate.Date = item.IsAllDayEvent ? item.Start.ToString("yyyy-MM-dd") : item.Start.ToString();
                    googleEvent.Start = startDate;

                    var endDateTime = new DateTimeOffset(item.End, currentTimeZone.GetUtcOffset(item.End));
                    var endDate = new EventDateTime();
                    
                    if (item.IsAllDayEvent)
                    {
                        endDate.Date = endDateTime.ToString("yyy-MM-dd");
                    }
                    else
                    {
                        endDate.DateTime = endDateTime.DateTime;
                    }
                    
                    googleEvent.End = endDate;

                    //consider the reminder set in Outlook
                    if (item.IsReminderSet)
                    {
                        googleEvent.Reminders = new Event.RemindersData { UseDefault = false };
                        var reminder = new EventReminder { Method = "popup", Minutes = item.ReminderMinutesBeforeStart };
                        googleEvent.Reminders.Overrides = new List<EventReminder> { reminder };
                    }

                    googleCalendarService.AddEntry(googleEvent);

                }
            }
        }

        public CalendarSyncInfo ClearAllRemoteItems()
        {
        	var syncInfo = new CalendarSyncInfo();
        	syncInfo.Text = "Deleted remote items in range.";
        	syncInfo.Status = CalendarSyncStatus.Success;
        	
        	try {
        		syncInfo.ItemsRemovedCount = googleCalendarService.RemoveAllItemsInRange();	
        	} catch (Exception) {
        		syncInfo.Status = CalendarSyncStatus.Failed;
        		syncInfo.Text = "Error removing remote items";
        	}
        	
        	return syncInfo;

        }
    }
}
