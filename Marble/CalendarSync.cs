using System;
using System.Collections.Generic;
using System.Linq;

using Google.Apis.Calendar.v3.Data;
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
		readonly OutlookCalendarService outlookCalendarService;
		
		readonly GoogleClient googleClient;
		readonly GoogleCalendarService googleCalendarService;
		
		public CalendarSync()
		{
			googleClient = new GoogleClient(Settings.DataStoreFolderNameCalendar, Settings.ScopeCalendar);
			googleCalendarService = new GoogleCalendarService(googleClient);
			
			outlookCalendarService = new OutlookCalendarService();
		}
		
		public void Sync()
		{
			if (Settings.CalendarId == "")
			{
                System.Windows.Forms.MessageBox.Show("You need to select a Google Calendar first on the 'Settings' tab.");
                return;
            }
			
			List<Appointment> googleAppoinments = googleCalendarService.GetAppointmentsInRange();
			List<Appointment> outlookAppoinments = outlookCalendarService.GetAppointmentsInRange();
			
			
			var comparer = new AppointmentComparer();
			// Items in google that are not in outlook should be deleted
			var googleItemsToDelete = googleAppoinments.Except(outlookAppoinments, comparer).ToList();
			RemoveOldCalendarEventsFromGoogleCalendar(googleItemsToDelete);
			
			// items in outlook that are not in google should be created
			var googleItemsToAdd = outlookAppoinments.Except(googleAppoinments, comparer).ToList();
			AddOutLookEventsToGoogleCalendar(googleItemsToAdd);
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
                foreach (var item in items) googleCalendarService.DeleteCalendarEntry(Settings.CalendarId, item.Id);
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
                        Start = new EventDateTime(),
                        End = new EventDateTime(),
                        Summary = item.Summary,
                        Location = item.Location,
                        Description = item.Description
                    };

                    var timezone = TimeZone.CurrentTimeZone;
                   	
                    var startDateTime = new DateTimeOffset(item.Start, timezone.GetUtcOffset(item.Start));
                    var startDate = new EventDateTime();
                    startDate.Date = startDateTime.ToString("o");
					//startDate.Date = item.IsAllDayEvent ? item.Start.ToString("yyyy-MM-dd") : item.Start.ToString();
                	googleEvent.Start = startDate;
                	                    	
                	var endDateTime = new DateTimeOffset(item.End, timezone.GetUtcOffset(item.End));
                	var endDate = new EventDateTime();
					//endDate.Date = item.IsAllDayEvent ? item.End.ToString("yyyy-MM-dd") : item.End.ToString();
                	endDate.Date = endDateTime.ToString("o");
                	googleEvent.End = endDate;
               
                    //consider the reminder set in Outlook
                    if (item.IsReminderSet)
                    {
                        googleEvent.Reminders = new Event.RemindersData { UseDefault = false };
                        var reminder = new EventReminder { Method = "popup", Minutes = item.ReminderMinutesBeforeStart };
                        googleEvent.Reminders.Overrides = new List<EventReminder> { reminder };
                    }

                    var organizer = new Event.OrganizerData();
                    organizer.DisplayName = item.Organizer;
                    googleEvent.Organizer = organizer;
                    
                    if (googleEvent.Attendees == null)
                    {
                    	googleEvent.Attendees = new List<EventAttendee>();
                    }
                    
                    foreach (var attendee in item.RequiredAttendees) {
                    	
                    	var eventAttendee = new EventAttendee();
                    	
                    	eventAttendee.DisplayName = attendee;
                    
                    	googleEvent.Attendees.Add(eventAttendee);
                    }
                    
                    foreach (var attendee in item.OptionalAttendees) {
                    	
                    	var eventAttendee = new EventAttendee();
                    	
                    	eventAttendee.DisplayName = attendee;

                    	googleEvent.Attendees.Add(eventAttendee);
                    }
                    
                    googleEvent.Attendees.Add(new EventAttendee { Email = Settings.CalendarAccount });
                    googleCalendarService.AddEntry(googleEvent);
                    
                }
            }
		}
	}
}
