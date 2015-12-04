using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.WebServices.Data;

using Marble.Properties;

namespace Marble.Exchange
{
    public class ExchangeService : IOutlookCalendarService
    {
        public List<Data.Appointment> GetAppointmentsInRange()
        {
            var startDate = Settings.CalendarRangeMinDate.AddMinutes(1);
            var endDate = Settings.CalendarRangeMaxDate;

            var service = new Microsoft.Exchange.WebServices.Data.ExchangeService();

            service.UseDefaultCredentials = true;
            service.AutodiscoverUrl(Settings.ExchangeEmailAddress, RedirectionCallback);

            var calendar = CalendarFolder.Bind(service, WellKnownFolderName.Calendar);

            var view = new CalendarView(startDate, endDate);
            view.PropertySet = new PropertySet(
                AppointmentSchema.Subject, 
                AppointmentSchema.Start, 
                AppointmentSchema.End,
                AppointmentSchema.IsAllDayEvent,
                AppointmentSchema.Location,
                //AppointmentSchema.OptionalAttendees,
                //AppointmentSchema.RequiredAttendees,
                AppointmentSchema.Organizer,
                AppointmentSchema.IsReminderSet,
                AppointmentSchema.ReminderMinutesBeforeStart
                );

            FindItemsResults<Appointment> appointments = calendar.FindAppointments(view);

            var result = new List<Data.Appointment>();
            foreach (Appointment item in appointments)
            {
                item.Load();
                result.Add(GetOutlookAppointment(item));
            }

            return result;
        }

        static Data.Appointment GetOutlookAppointment(Appointment appointment)
        {
            var newAppointment = new Data.Appointment
            {
                Description = appointment.Body,
                Summary = appointment.Subject,
                Start = appointment.Start,
                End = appointment.End,
                IsAllDayEvent = appointment.IsAllDayEvent,
                Location = appointment.Location,
                OptionalAttendees = GetAttendees(appointment.OptionalAttendees),
                RequiredAttendees = GetAttendees(appointment.RequiredAttendees),
                Organizer = appointment.Organizer.Name,
                ReminderMinutesBeforeStart = appointment.ReminderMinutesBeforeStart,
                IsReminderSet = appointment.IsReminderSet
            };
            return newAppointment;
        }

        static List<string> GetAttendees(AttendeeCollection attendees)
        {
            var attendeeList = new List<string>();

            foreach (var item in attendees)
            {
                attendeeList.Add(item.Name);
            }

            return attendeeList;
        }

        private bool RedirectionCallback(string url)
        {
            return url.ToLower().StartsWith("https://");
        }
    }
}
