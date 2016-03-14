/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 3/11/2016
 * Time: 12:57 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

using Google.Apis.Calendar.v3.Data;

namespace Marble.Data
{
	/// <summary>
	/// Description of AppointmentMapper.
	/// </summary>
	public static class AppointmentMapper
	{
		
		public static Event MapToGoogleEvent(Appointment appointment)
		{
			var googleEvent = new Event
            {
                Summary = appointment.Summary,
                Location = appointment.Location,
                Description = appointment.Description,
                Attendees = new List<EventAttendee>()
            };

            var currentTimeZone = TimeZone.CurrentTimeZone;

            var startDateTime = new DateTimeOffset(appointment.Start, currentTimeZone.GetUtcOffset(appointment.Start));
            var startDate = new EventDateTime();
            
            if (appointment.IsAllDayEvent)
            {
                startDate.Date = startDateTime.ToString("yyy-MM-dd");
            }
            else
            {
                startDate.DateTime = startDateTime.DateTime;
            }

            //startDate.Date = item.IsAllDayEvent ? item.Start.ToString("yyyy-MM-dd") : item.Start.ToString();
            googleEvent.Start = startDate;

            var endDateTime = new DateTimeOffset(appointment.End, currentTimeZone.GetUtcOffset(appointment.End));
            var endDate = new EventDateTime();
            
            if (appointment.IsAllDayEvent)
            {
                endDate.Date = endDateTime.ToString("yyy-MM-dd");
            }
            else
            {
                endDate.DateTime = endDateTime.DateTime;
            }
            
            googleEvent.End = endDate;

            //consider the reminder set in Outlook
            if (appointment.IsReminderSet)
            {
                googleEvent.Reminders = new Event.RemindersData { UseDefault = false };
                var reminder = new EventReminder { Method = "popup", Minutes = appointment.ReminderMinutesBeforeStart };
                googleEvent.Reminders.Overrides = new List<EventReminder> { reminder };
            }
            
            return googleEvent;
		}
	}
}
