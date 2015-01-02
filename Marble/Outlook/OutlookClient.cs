/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 1/2/2015
 * Time: 3:58 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Outlook;

namespace Marble.Outlook
{
	/// <summary>
	/// Description of OutlookClient.
	/// </summary>
	public class OutlookClient
	{
		public MAPIFolder UseOutlookCalendar;
		
		public OutlookClient()
		{
			// Create the Outlook application.
            var oApp = new Application();

            // Get the NameSpace and Logon information.
            // Outlook.NameSpace oNS = (Outlook.NameSpace)oApp.GetNamespace("mapi");
            var oNS = oApp.GetNamespace("mapi");

            //Log on by using a dialog box to choose the profile.
            oNS.Logon("", "", true, true);

            // Get the Calendar folder.
            UseOutlookCalendar = oNS.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);

            // Done. Log off.
            oNS.Logoff();
		}
		
		public List<OutlookAppointment> GetCalendarEntries()
        {
            Items OutlookItems = UseOutlookCalendar.Items;

            var result = new List<OutlookAppointment>();

            if (OutlookItems != null)
            {
                foreach (AppointmentItem ai in OutlookItems)
                {
                    result.Add(GetOutlookAppointment(ai));
                }
            }
            return result;
        }

        public List<OutlookAppointment> GetCalendarEntriesInRange()
        {
            var result = new List<OutlookAppointment>();

            Items OutlookItems = UseOutlookCalendar.Items;
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
        
        private static OutlookAppointment GetOutlookAppointment(AppointmentItem appointment)
        {
            var newAppointment = new OutlookAppointment
                {
                    Body = appointment.Body,
                    Subject = appointment.Subject,
                    Start = appointment.Start,
                    End = appointment.End,
                    AllDayEvent = appointment.AllDayEvent,
                    Location = appointment.Location,
                    OptionalAttendees = appointment.OptionalAttendees,
                    RequiredAttendees = appointment.RequiredAttendees,
                    Organizer = appointment.Organizer,
                    ReminderMinutesBeforeStart = appointment.ReminderMinutesBeforeStart,
                    ReminderSet = appointment.ReminderSet
                };
            return newAppointment;
        }
	}
}
