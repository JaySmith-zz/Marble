/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 1/2/2015
 * Time: 3:52 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Marble.Outlook;

namespace Marble
{
	/// <summary>
	/// Description of MarbleSyncTask.
	/// </summary>
	public class MarbleSync
	{
		public void SyncAll()
		{
            //This syncs calendar
			SyncCalendar();
		}
		
		public void SyncCalendar()
		{
			var calendarSync = new CalendarSync();
			calendarSync.Sync();
		}
	}
}
