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
using System.IO;
using System.Threading;

using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;

namespace Marble
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
	}
}
