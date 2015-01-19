using System;
using System.Text;

namespace Marble.Outlook
{
	/// <summary>
	/// Description of OutlookAppointment.
	/// </summary>
	public class OutlookAppointment
	{
        public bool AllDayEvent { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Location { get; set; }
        public bool ReminderSet { get; set; }
        public int ReminderMinutesBeforeStart { get; set; }
        public string Organizer { get; set; }
		public string RequiredAttendees { get; set; }
		public string OptionalAttendees { get; set; }
        public string Subject { get; set; }
		public string Body { get; set; }
        public string Id { get; set; }
    }
}
