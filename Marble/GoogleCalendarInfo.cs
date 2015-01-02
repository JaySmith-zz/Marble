/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 12/31/2014
 * Time: 9:18 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Google.Apis.Calendar.v3.Data;


namespace Marble
{
	/// <summary>
	/// Description of GoogleCalendarInfo.
	/// </summary>
	public class GoogleCalendarInfo
	{
		public string Id;
		public string Name;
		
		public GoogleCalendarInfo(CalendarListEntry item)
		{
			Id = item.Id;
			Name = item.Summary;
		}
		
		public override string ToString()
		{
			return Name;
		}

	}
}
