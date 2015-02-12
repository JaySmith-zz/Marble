using System;
using Google.Apis.Calendar.v3.Data;


namespace Marble.Google
{
	/// <summary>
	/// Description of GoogleCalendarInfo.
	/// </summary>
	public class GoogleCalendarInfo
	{
		public string Id;
		public string Name;

        public GoogleCalendarInfo(string id, string name)
        {
            Id = id;
            Name = name;
        }
		
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
