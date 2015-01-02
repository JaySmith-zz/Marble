/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 1/2/2015
 * Time: 4:41 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using Microsoft.Exchange.WebServices.Data;

namespace Marble.Outlook
{
	/// <summary>
	/// Description of OutlookAppointment.
	/// </summary>
	public class OutlookAppointment
	{

		public ExchangeService Service { get; set; }
        public bool IsEWS { get; set; }
        public bool AllDayEvent { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Location { get; set; }
        public bool ReminderSet { get; set; }
        public int ReminderMinutesBeforeStart { get; set; }
        public string Organizer { get; set; }
        string _requiredAttendees;

        public string RequiredAttendees
        {
            get
            {
                LoadItemDataFromEWS();
                return _requiredAttendees;
            }
            set { _requiredAttendees = value; }
        }
        string _optionalAttendees;

        public string OptionalAttendees
        {
            get
            {
                LoadItemDataFromEWS();
                return _optionalAttendees;
            }
            set { _optionalAttendees = value; }
        }

        public string Subject { get; set; }

        string _body;
        public string Body
        {
            get
            {
                LoadItemDataFromEWS();
                return _body;
            }
            set
            {
                _body = value;
            }
        }

        private void LoadItemDataFromEWS()
        {
            if (_body == null && IsEWS)
            {
                var appointment = Appointment.Bind(Service, new ItemId(Id));
                appointment.Load();
                _body = appointment.Body.Text;
                _optionalAttendees = GetAttendeeString(appointment.OptionalAttendees);
                _requiredAttendees = GetAttendeeString(appointment.RequiredAttendees);
            }
        }

        public string Id { get; set; }

        string GetAttendeeString(Microsoft.Exchange.WebServices.Data.AttendeeCollection collection)
        {
            var sb = new StringBuilder();
            foreach (var attendee in collection)
            {
                sb.Append(attendee.ToString() + ";");
            }
            return sb.ToString();
        }
    }
}
