/*
 * Created by SharpDevelop.
 * User: SMITHJAY
 * Date: 2/10/2015
 * Time: 9:50 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Outlook;

using Marble.Data;

namespace Marble
{
	/// <summary>
	/// Description of CalendarServiceOutlook.
	/// </summary>
	public class CalendarServiceOutlook : ICalendarService
	{
		public MAPIFolder outlookCalendar;
		
		public CalendarServiceOutlook()
		{
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
			OutlookItems.IncludeRecurrences = true;
			OutlookItems.Sort("[Start]", Type.Missing);

			if (OutlookItems != null)
			{
				var min = Settings.CalendarRangeMinDate.AddMinutes(1);
				var max = Settings.CalendarRangeMaxDate;

				string filter = "[End] >= '" + min.ToString("g") + "' AND [Start] < '" + max.ToString("g") + "'";

				var filteredAppoinments = OutlookItems.Restrict(filter);
				foreach (AppointmentItem ai in filteredAppoinments)
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
