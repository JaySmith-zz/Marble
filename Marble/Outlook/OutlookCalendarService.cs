using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Outlook;

using Marble.Data;

namespace Marble.Outlook
{
	/// <summary>
	/// Description of OutlookClient.
	/// </summary>
	public class OutlookCalendarService
	{
		public MAPIFolder outlookCalendar;
		
		public OutlookCalendarService()
		{
			// Create the Outlook application.
            var oApp = new Application();

            // Get the NameSpace and Logon information.
            // Outlook.NameSpace oNS = (Outlook.NameSpace)oApp.GetNamespace("mapi");
            var oNS = oApp.GetNamespace("mapi");

            //Log on by using a dialog box to choose the profile.
            oNS.Logon("", "", true, true);

            // Get the Calendar folder.
            outlookCalendar = oNS.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);

            // Done. Log off.
            oNS.Logoff();
		}

        public List<Appointment> GetAppointmentsInRange()
        {
            var result = new List<Appointment>();

            Items OutlookItems = outlookCalendar.Items;
            OutlookItems.Sort("[Start]", Type.Missing);
            OutlookItems.IncludeRecurrences = true;

            if (OutlookItems != null)
            {
                DateTime min = DateTime.Now.AddDays(-Settings.CalendarDaysInThePast);
                DateTime max = DateTime.Now.AddDays(+Settings.CalendarDaysInTheFuture + 1);

                string filter = "[End] >= '" + min.ToString("g") + "' AND [Start] < '" + max.ToString("g") + "'";

                foreach (AppointmentItem ai in OutlookItems.Restrict(filter))
                {
                    result.Add(GetOutlookAppointment(ai));
                }
            }

            return result;
        }
 
        static Appointment GetOutlookAppointment(AppointmentItem appointment)
        {
            var newAppointment = new Appointment
                {
                    Description = appointment.Body,
                    Summary = appointment.Subject,
                    Start = appointment.Start,
                    End = appointment.End,
                    IsAllDayEvent = appointment.AllDayEvent,
                    Location = appointment.Location,
                    OptionalAttendees = GetAttendees(appointment.OptionalAttendees),
                    RequiredAttendees = GetAttendees(appointment.RequiredAttendees),
                    Organizer = appointment.Organizer,
                    ReminderMinutesBeforeStart = appointment.ReminderMinutesBeforeStart,
                    IsReminderSet = appointment.ReminderSet
                };
            return newAppointment;
        }

		static List<string> GetAttendees(string attendees)
		{
			var attendeeList = new List<string>();
			
			if (attendees == null) return attendeeList;
			
            string[] tmp1 = attendees.Split(';');
            
            for (int i = 0; i < tmp1.Length; i++)
            {
            	attendeeList.Add(tmp1[i].Trim());
            }
            
            return attendeeList;
		}
	}
}
