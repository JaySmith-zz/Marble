/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 1/2/2015
 * Time: 3:54 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;

using Google.Apis.Calendar.v3.Data;
using Marble.Google;
using Marble.Outlook;

namespace Marble
{
	/// <summary>
	/// Description of CalendarSync.
	/// </summary>
	public class CalendarSync
	{
		readonly OutlookCalendarService outlookClient;
		readonly GoogleClient googleClient;
		
		readonly GoogleCalendarService googleCalendarService;
		
		public CalendarSync()
		{
			googleClient = new GoogleClient(Settings.DataStoreFolderNameCalendar, Settings.ScopeCalendar);
			googleCalendarService = new GoogleCalendarService(googleClient);
			
			outlookClient = new OutlookCalendarService();
		}
		
		public void Sync()
		{
			if (Settings.CalendarId == "")
			{
                System.Windows.Forms.MessageBox.Show("You need to select a Google Calendar first on the 'Settings' tab.");
                return;
            }
			
			List<Event> googleItems = googleCalendarService.GetCalendarEntriesInRange();
			List<OutlookAppointment> outlookItems = outlookClient.GetCalendarEntriesInRange();
			
			IdentifyGoogleAddDeletes(outlookItems, googleItems);
			
			if (googleItems.Count > 0)
            {
                foreach (var googleEvent in googleItems) googleCalendarService.DeleteCalendarEntry(googleEvent);
            }

            if (outlookItems.Count > 0)
            {
                foreach (OutlookAppointment outlookAppointment in outlookItems)
                {
                    var googleEvent = new Event
                    {
                        Start = new EventDateTime(),
                        End = new EventDateTime()
                    };

                    if (outlookAppointment.AllDayEvent)
                    {
                        googleEvent.Start.Date = outlookAppointment.Start.ToString("yyyy-MM-dd");
                        googleEvent.End.Date = outlookAppointment.End.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        //ev.Start.DateTime = ai.Start;
                        //ev.End.DateTime = ai.End;
                        googleEvent.Start.DateTime = outlookAppointment.Start;
                        googleEvent.End.DateTime = outlookAppointment.End;
                    }
                    googleEvent.Summary = outlookAppointment.Subject;
                    if ( Settings.AddDescription)
                    {
                        googleEvent.Description = outlookAppointment.Body;
                    }
                    googleEvent.Location = outlookAppointment.Location;


                    //consider the reminder set in Outlook
                    if (Settings.AddReminders && outlookAppointment.ReminderSet)
                    {
                        googleEvent.Reminders = new Event.RemindersData { UseDefault = false };
                        var reminder = new EventReminder { Method = "popup", Minutes = outlookAppointment.ReminderMinutesBeforeStart };
                        googleEvent.Reminders.Overrides = new List<EventReminder> { reminder };
                    }

                    googleEvent.Description = outlookAppointment.Body;

                    var organizer = new Event.OrganizerData();
                    organizer.DisplayName = outlookAppointment.Organizer;
                    googleEvent.Organizer = organizer;
                    
                    var requiredAttendees = splitAttendees(outlookAppointment.RequiredAttendees).ToList();
                    foreach (var attendee in requiredAttendees) {
                    	
                    	var eventAttendee = new EventAttendee();
                    	
                    	eventAttendee.DisplayName = attendee;
                    	
                    	if (googleEvent.Attendees == null)
                    	{
                    		googleEvent.Attendees = new List<EventAttendee>();
                    	}
                    	googleEvent.Attendees.Add(eventAttendee);
                    }
//                    
//                    foreach (var attendee in outlookAppointment.OptionalAttendees.Split(';').ToArray())
//                    {
//                    	
//                    }
                    //googleEvent.Attendees = GetAttendeeList(outlookAppointment);
                    
                    
                    // Set Attendees
//                    if (Settings.Instance.AddAttendeesToDescription)
//                    {
//                        var footer = new StringBuilder();
//                        footer.Append(Environment.NewLine);
//                        footer.Append(Environment.NewLine + "==============================================");
//                        footer.Append(Environment.NewLine + "Added by OutlookGoogleSync:" + Environment.NewLine);
//                        footer.Append(Environment.NewLine + "ORGANIZER: " + Environment.NewLine + outlookAppointment.Organizer + Environment.NewLine);
//                        footer.Append(Environment.NewLine + "REQUIRED: " + Environment.NewLine + splitAttendees(outlookAppointment.RequiredAttendees) + Environment.NewLine);
//                        footer.Append(Environment.NewLine + "OPTIONAL: " + Environment.NewLine + splitAttendees(outlookAppointment.OptionalAttendees));
//                        footer.Append(Environment.NewLine + "==============================================");
//
//                        googleEvent.Description = googleEvent.Description + footer;
//                    }

                    //GoogleCalendar.Instance.AddEntry(ev);
                }
            }
			
		}
		
		private string[] splitAttendees(string attendees)
        {
			if (attendees == null) return new string[0];
            string[] tmp1 = attendees.Split(';');
            for (int i = 0; i < tmp1.Length; i++) tmp1[i] = tmp1[i].Trim();
            //return String.Join(Environment.NewLine, tmp1);
            return tmp1;
        }
		
		public void IdentifyGoogleAddDeletes(List<OutlookAppointment> outlookItems, List<Event> googleItems)
		{
			// Count backwards so that we can remove found items without affecting the order of remaining items
            for (int i = outlookItems.Count - 1; i >= 0; i--)
            {
                for (int j = googleItems.Count - 1; j >= 0; j--)
                {
                    if (String.CompareOrdinal(Signature(outlookItems[i]), Signature(googleItems[j])) == 0)
                    {
                        outlookItems.Remove(outlookItems[i]);
                        googleItems.Remove(googleItems[j]);
                        break;
                    }
                }
            }
		}
		
		
		string Signature(Event ev)
        {
			return (ev.Start + ";" + ev.End + ";" + ev.Summary + ";" + ev.Location).Trim();
        }
		
		string Signature(OutlookAppointment appointment)
		{
			return (appointment.Start + ";" + appointment.End + ";" + appointment.Subject + ";" + appointment.Location).Trim();
		}
	}
}
