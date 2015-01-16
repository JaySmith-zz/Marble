using System;
using System.Collections.Generic;

using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

using Marble.Data;

namespace Marble.Google
{
	public class GoogleCalendarService
	{	
		readonly GoogleClient client;
		readonly CalendarService service;
		
		public GoogleCalendarService(GoogleClient googleClient)
		{
			client = googleClient;
			service = new CalendarService(googleClient.Initializer);
		}
		
		public List<GoogleCalendarInfo> GetCalendars()
		{
			var calendars = service.CalendarList.List().Execute().Items;		
			
			var items = new List<GoogleCalendarInfo>();
			foreach (var calendar in calendars) {
				items.Add( new GoogleCalendarInfo(calendar));
			}
			
			return items;
		}
		
		List<Event> GetCalendarEntriesInRange()
        {
            var results = new List<Event>();
            if (string.IsNullOrEmpty(Settings.CalendarId)) 
            {
            	throw new ApplicationException("User Calendar Not Specified");
            }
            
            var eventRequest = service.Events.List(Settings.CalendarAccount);
            
            eventRequest.TimeMin = DateTime.Now.AddDays(-Settings.CalendarDaysInThePast);
            eventRequest.TimeMax = DateTime.Now.AddDays(+Settings.CalendarDaysInTheFuture + 1);

            results.AddRange(eventRequest.Execute().Items);
            	
            return results;
        }
		
		public List<Appointment> GetAppointmentsInRange()
		{
			var googleEvents = GetCalendarEntriesInRange();
			
			var appointments = new List<Appointment>();
			foreach (var googleEvent in googleEvents) 
			{
				var appointment = new Appointment() {
					Id = googleEvent.Id,
					Start = googleEvent.Start.DateTime,
					End = googleEvent.End.DateTime,
					Summary = googleEvent.Summary,
					Location = googleEvent.Location
				};
				
				appointments.Add(appointment);
			}
			
			return appointments;
		}
		
		public void DeleteCalendarEntry(string calenderId, string eventId)
        {
			service.Events.Delete(calenderId, eventId);
        }		
		
		public void AddEntry(Event googleEvent)
		{
			service.Events.Insert(googleEvent, Settings.CalendarId).Execute();
		}
	}
}
