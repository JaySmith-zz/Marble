/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 1/2/2015
 * Time: 3:54 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using Marble.Google;
using Marble.Outlook;

namespace Marble
{
	/// <summary>
	/// Description of CalendarSync.
	/// </summary>
	public class CalendarSync
	{
		readonly OutlookClient outlookClient;
		readonly GoogleClient googleClient;
		
		readonly GoogleCalendarService googleCalendarService;
		
		public CalendarSync()
		{
			googleClient = new GoogleClient(Settings.DataStoreFolderNameCalendar, Settings.ScopeCalendar);
			googleCalendarService = new GoogleCalendarService(googleClient);
			
			outlookClient = new OutlookClient();
		}
		
		public void Sync()
		{
			var items = googleCalendarService.GetCalendars();
		}
	}
}
