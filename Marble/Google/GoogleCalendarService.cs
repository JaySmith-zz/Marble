/*
 * Created by SharpDevelop.
 * User: SMITHJAY
 * Date: 12/23/2014
 * Time: 11:14 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

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
		
		public List<Event> GetCalendarEntriesInRange()
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
		
		public void DeleteCalendarEntry(Event e)
        {
//            try
//            {
//                if (service != null) service.Events.Delete(Settings.UserCalendar, e.Id).Fetch();
//            }
//            catch (Exception ex)
//            {
//                FormMain.Instance.HandleException(ex);
//            }       
        }		
		
		public void AddEntry(Event e)
		{
//            try
//            {
//                if (service != null) service.Events.Insert(e, Settings.UserCalendar).Fetch();
//            }
//            catch (Exception ex)
//            {
//                //FormMain.Instance.HandleException(ex);
//            }   
		}
	}
}
