using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;

namespace Marble.Exchange
{
    public class ExchangeService : IOutlookCalendarService
    {
        public List<Data.Appointment> GetAppointmentsInRange()
        {
            var startDate = Settings.CalendarRangeMinDate.AddMinutes(1);
            var endDate = Settings.CalendarRangeMaxDate;

            var service = new Microsoft.Exchange.WebServices.Data.ExchangeService {UseDefaultCredentials = true};

            service.AutodiscoverUrl(Settings.ExchangeEmailAddress, RedirectionCallback);

            var calendar = CalendarFolder.Bind(service, WellKnownFolderName.Calendar);

            var view = new CalendarView(startDate, endDate);
            var propertySet = new PropertySet(
                //AppointmentSchema.TextBody,
                //AppointmentSchema.NormalizedBody,
                ItemSchema.Subject,
                AppointmentSchema.Start,
                AppointmentSchema.End,
                AppointmentSchema.IsAllDayEvent,
                AppointmentSchema.Location,
                //AppointmentSchema.OptionalAttendees,
                //AppointmentSchema.RequiredAttendees,
                AppointmentSchema.Organizer,
                ItemSchema.IsReminderSet,
                ItemSchema.ReminderMinutesBeforeStart
                );

            view.PropertySet = propertySet;

            FindItemsResults<Appointment> appointments = calendar.FindAppointments(view);

            var itemPropertySet = new PropertySet(
                ItemSchema.TextBody,
                ItemSchema.NormalizedBody,
                ItemSchema.Subject,
                AppointmentSchema.Start,
                AppointmentSchema.End,
                AppointmentSchema.IsAllDayEvent,
                AppointmentSchema.Location,
                AppointmentSchema.OptionalAttendees,
                AppointmentSchema.RequiredAttendees,
                AppointmentSchema.Organizer,
                ItemSchema.IsReminderSet,
                ItemSchema.ReminderMinutesBeforeStart
                );

            var result = new List<Data.Appointment>();
            foreach (var item in appointments)
            {
                item.Load(itemPropertySet);
                result.Add(GetOutlookAppointment(item));
            }

            return result;
        }

        private static Data.Appointment GetOutlookAppointment(Appointment appointment)
        {
            var newAppointment = new Data.Appointment
            {
                Description = appointment.TextBody,
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

        private static List<string> GetAttendees(AttendeeCollection attendees)
        {
            var attendeeList = new List<string>();

            foreach (var item in attendees)
            {
                attendeeList.Add(item.Name);
            }

            return attendeeList;
        }

        private static bool RedirectionCallback(string url)
        {
            return url.ToLower().StartsWith("https://", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
