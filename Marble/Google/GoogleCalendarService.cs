using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Google.Apis.Calendar.v3;

using Google.Apis.Calendar.v3.Data;
using Marble.Data;

namespace Marble.Google
{
	public class GoogleCalendarService
	{	
		readonly GoogleClient client;
		readonly CalendarService service;
		readonly Logger Logger = LogFactory.GetLoggerFor(typeof(GoogleCalendarService));
			
		
		public GoogleCalendarService(GoogleClient googleClient)
		{
			client = googleClient;
			service = new CalendarService(googleClient.Initializer);
		}
		
		public List<GoogleCalendarInfo> GetCalendars()
		{
			var calendars = service.CalendarList.List().Execute().Items.Where(x => x.AccessRole == "owner");

            var items = new List<GoogleCalendarInfo>();
			foreach (var calendar in calendars) {
				items.Add( new GoogleCalendarInfo(calendar));
			}
			
			return items;
		}
		
		List<Event> GetGoogleEventsInRange(DateTime startDate, DateTime endDate)
		{
			var results = new List<Event>();
			if (string.IsNullOrEmpty(Settings.CalendarId)) 
			{
				throw new ApplicationException("User Calendar Not Specified");
			}
			
			var eventRequest = service.Events.List(Settings.CalendarAccount);
			
			eventRequest.TimeMax = startDate;
            eventRequest.TimeMin = endDate;
            
            //eventRequest.ShowDeleted = true;
            //eventRequest.MaxResults = 2500;

			results.AddRange(eventRequest.Execute().Items);
				
			return results;
		}
		
		public List<Appointment> GetAppointmentsInRange()
		{
			var googleEvents = GetGoogleEventsInRange(Settings.CalendarRangeMinDate, Settings.CalendarRangeMaxDate);
			
			var appointments = new List<Appointment>();
			foreach (var googleEvent in googleEvents) 
			{
				var appointment = MapGoogleEventtoAppointment(googleEvent);
				appointments.Add(appointment);
			}
			
			return appointments;
		}
		
		Appointment MapGoogleEventtoAppointment(Event googleEvent)
		{
			var appointment = new Appointment();
			
			try
			{
				appointment.Id = googleEvent.Id;
				
				if (googleEvent.Start.DateTime == null)
				{
					appointment.IsAllDayEvent = true;
					appointment.Start = DateTime.Parse(googleEvent.Start.Date);
					appointment.End = DateTime.Parse(googleEvent.End.Date);
				}
				else 
				{
					appointment.IsAllDayEvent = false;
					appointment.Start = (DateTime) googleEvent.Start.DateTime;
					appointment.End = (DateTime) googleEvent.End.DateTime;
				}
				

				appointment.Summary = googleEvent.Summary;
				appointment.Location = string.IsNullOrEmpty(googleEvent.Location) ? "none" : googleEvent.Location;
			}
			catch(Exception ex)
			{
				Globals.HasError = true;
				Globals.ErrorMessage = "An error occured while updated Google Calendar.  Please see the log file for more details.";
				Logger.Fatal(ex.Message, ex);
			}
			
			return appointment;
		}
		
		public void DeleteCalendarEntry(string calenderId, string eventId)
		{
			string result = service.Events.Delete(calenderId, eventId).Execute();
		}		
		
		public void AddEntry(Event googleEvent)
		{
			var myEvent = service.Events.Insert(googleEvent, Settings.CalendarAccount).Execute();
		}
		
		public Event AddEvent(Event googleEvent)
		{
			var myEvent = service.Events.Insert(googleEvent, Settings.CalendarAccount).Execute();
			
			return myEvent;
		}
		
		public void RemoveEvent(string calenderId, string eventId)
		{
			string result = service.Events.Delete(calenderId, eventId).Execute();
		}
		
		public int RemoveAllItemsInRange()
		{
			//TODO: Need to batch these in groups to avoid request limit
			const int batchSize = 100;
			const int batchWaitTime = 3000; //wait time between batches in miliseconds
			var batchItems = new List<Event>();
			
			var minDate = Settings.CalendarRangeMinDate;
			var maxDate = Settings.CalendarRangeMaxDate;

			List<Event> items = GetGoogleEventsInRange(minDate, maxDate);
					
			if (items.Count > 0)
        	{
				DeleteCalendarEntries(items);
//				if (items.Count > batchSize)
//				{
//					foreach (var item in items)
//					{
//						// Build up batch of items
//						if (batchItems.Count <= batchSize) batchItems.Add(item);
//						if (batchItems.Count == batchSize)
//						{
//							DeleteCalendarEntries(batchItems);
//							batchItems.Clear();
//							Thread.Sleep(batchWaitTime);
//						}
//					}
//					if (batchItems.Count > 0) DeleteCalendarEntries(batchItems);
//				} 
//				else
//				{
//					DeleteCalendarEntries(items);
//				}
        	}
			
			return items.Count;
		}
	
		public void DeleteCalendarEntries(List<Event> items)
		{
			foreach (var item in items) {
				DeleteCalendarEntry(Settings.CalendarAccount, item.Id);
			}
		}
	}
}
