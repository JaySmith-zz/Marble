using System;
using System.Collections.Generic;
using System.Linq;

using System.Windows.Forms.VisualStyles;
using System.Xml;
using Google.Apis.Calendar.v3.Data;
using Microsoft.SqlServer.Server;
using Marble.Data;
using Marble.Google;
using Marble.Outlook;

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

        public void Sync()
        {
        	if (!ValidateSettings()) return;

            List<Appointment> outlookAppoinments = outlookCalendarService.GetAppointmentsInRange();
            List<Appointment> googleAppoinments = googleCalendarService.GetAppointmentsInRange();

            var comparer = new AppointmentComparer();
            var googleItemsToDelete = googleAppoinments.Except(outlookAppoinments, comparer).ToList();
            var googleItemsToAdd = outlookAppoinments.Except(googleAppoinments, comparer).ToList();

            // Items in google that are not in outlook should be deleted
            RemoveOldCalendarEventsFromGoogleCalendar(googleItemsToDelete);

            // items in outlook that are not in google should be created
            AddOutLookEventsToGoogleCalendar(googleItemsToAdd);

            if (Globals.HasError)
            {
                System.Windows.Forms.MessageBox.Show(Globals.ErrorMessage);
            }
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
            //return String.Join(Environment.NewLine, tmp1);
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
                        //Start = new EventDateTime(),
                        //End = new EventDateTime(),
                        Summary = item.Summary,
                        Location = item.Location,
                        Description = item.Description,
                        Attendees = new List<EventAttendee>()
                    };

                    var currentTimeZone = TimeZone.CurrentTimeZone;

                    var startDateTime = new DateTimeOffset(item.Start, currentTimeZone.GetUtcOffset(item.Start));
                    var startDate = new EventDateTime();
                    //startDate.DateTime = startDateTime.ToString("o");
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
                    //endDate.Date = item.IsAllDayEvent ? item.End.ToString("yyyy-MM-dd") : item.End.ToString();
                    if (item.IsAllDayEvent)
                    {
                        endDate.Date = endDateTime.ToString("yyy-MM-dd");
                    }
                    else
                    {
                        endDate.DateTime = endDateTime.DateTime;
                    }
                    //endDate.Date = endDateTime.ToString("o");
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

        public void ClearAllRemoteItems()
        {
            googleCalendarService.RemoveAllItemsInRange();

        }
    }
}
