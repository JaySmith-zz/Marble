using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Calendar.v3;

using Google.Apis.Calendar.v3.Data;

namespace Marble.Google
{
	public class CalendarServiceGoogle
	{	
		readonly GoogleClient client;
		readonly CalendarService service;
			
		public CalendarServiceGoogle(GoogleClient googleClient)
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

		public Event AddEvent(Event googleEvent)
		{
			var myEvent = service.Events.Insert(googleEvent, Settings.CalendarAccount).Execute();
			
			return myEvent;
		}
		
		public void RemoveEvent(string calenderId, string eventId)
		{
			string result = service.Events.Delete(calenderId, eventId).Execute();
		}
		
	}
}
