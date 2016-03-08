/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 3/7/2016
 * Time: 11:43 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Marble.Data;
using Marble.Google;

using Google.Apis.Calendar.v3.Data;

namespace Marble
{
	/// <summary>
	/// Description of CalendarSyncCached.
	/// </summary>
	public class CalendarSyncCached
	{
		
		readonly OutlookServiceProvider sourceCalendarProvider;
		readonly IOutlookCalendarService outlookCalendarService;
		
		readonly GoogleClient googleClient;
        readonly GoogleCalendarService googleCalendarService;
        readonly AppointmentCache cache;
		        
		public CalendarSyncCached()
		{
			cache = new AppointmentCache();
			
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
			var syncInfo = new CalendarSyncInfo();
        	
        	if (!Settings.IsValid) 
        	{
        		syncInfo.Status = CalendarSyncStatus.Skipped;
        		return syncInfo;
        	}
        	
			// Get Current appointments from outlook
			List<Appointment> appointments = outlookCalendarService.GetAppointmentsInRange();
			
			// Load Items from Cache
			//var cachedItems = AppointmentSerialization.Read();
			
			var comparer = new AppointmentComparer();
			
			// Find all appointments in cache not in outlook, need to be removed
			var toBeRemoved = cache.Items.Except(appointments, comparer).ToList();
			syncInfo.ItemsRemovedCount = toBeRemoved.Count;
			RemoveEvents(toBeRemoved);
			
			// Find all appointments in outlook not in cache, need to be added
			var toBeAdded = appointments.Except(cache.Items, comparer).ToList();
			syncInfo.ItemsAddCount = toBeAdded.Count;
			AddEvents(toBeAdded);
			
			cache.Save();
			
			return syncInfo;
		}
		
		private void AddEvents(List<Appointment> appointments)
        {
            if (appointments.Count > 0)
            {
                foreach (Appointment appointment in appointments)
                {
                	var googleEvent = MapToGoogleEvent(appointment);
					var newEvent = googleCalendarService.AddEvent(googleEvent);
					
					appointment.GoogleId = newEvent.Id;
					cache.Add(appointment);
                }
                //AppointmentSerialization.Save(appointments);
            }
        }
		
		private void RemoveEvents(List<Appointment> appointments)
		{
			if (appointments.Count > 0)
            {
                foreach (Appointment appointment in appointments)
                {
                	googleCalendarService.RemoveEvent(Settings.CalendarAccount, appointment.GoogleId);
                	cache.Remove(appointment);
                }
            }
		}
	
		private static Event MapToGoogleEvent(Appointment appointment)
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
	
		public void RemoveEventsAndClearCache()
		{
			var cachedItems = AppointmentSerialization.Read();
			RemoveEvents(cachedItems);
			cache.Clear();
		}
		
		public void ClearCache()
		{
			cache.Clear();
		}
	}
}
		

		 

		 
		

