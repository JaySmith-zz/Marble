﻿using System;
using System.Collections.Generic;
using NetOffice.OutlookApi;
using NetOffice.OutlookApi.Enums;
using Marble.Data;
using System.Globalization;

namespace Marble.Outlook
{
    /// <summary>
    /// Description of OutlookClient.
    /// </summary>
    public class OutlookCalendarService
    {
        public MAPIFolder outlookCalendar;

        public OutlookCalendarService()
        {
            // Create the Outlook application.
            var oApp = new Application();

            // Get the NameSpace and Logon information.
            var oNS = oApp.GetNamespace("MAPI");

            //Log on by using a dialog box to choose the profile.
            oNS.Logon("", "", true, true);

            // Get the Calendar folder.
            outlookCalendar = oNS.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);

            // Done. Log off.
            oNS.Logoff();
        }

        public List<Appointment> GetAppointmentsInRange()
        {
            var result = new List<Appointment>();

            Items OutlookItems = outlookCalendar.Items as Items;
            OutlookItems.IncludeRecurrences = true;
            OutlookItems.Sort("[Start]", OlSortOrder.olAscending);

            if (OutlookItems != null)
            {
                string filter = "[Start] >= '" + Settings.CalendarRangeMinDate.ToString("g") + "' AND [End] <= '" + Settings.CalendarRangeMaxDate.ToString("g") + "'";
                var filteredAppoinments = OutlookItems.Restrict(filter);

                foreach (_AppointmentItem ai in filteredAppoinments)
                {
                    if (ai.MeetingStatus != OlMeetingStatus.olMeetingCanceled && ai.MeetingStatus != OlMeetingStatus.olMeetingReceivedAndCanceled)
                    {
                        if (ai.IsRecurring == true)
                        {
                            RecurrencePattern pattern = ai.GetRecurrencePattern();
                            List<DayOfWeek> daysOfWeekList = new List<DayOfWeek>();

                            switch (pattern.RecurrenceType)
                            {
                                case OlRecurrenceType.olRecursDaily:
                                    CreateDailyOccurences(result, ai, pattern);
                                    break;

                                case OlRecurrenceType.olRecursWeekly:
                                    if (pattern.DayOfWeekMask.ToString().Length < 4)
                                        daysOfWeekList = FindDaysOfWeekFromMask(pattern, daysOfWeekList);

                                    else daysOfWeekList.Add(GetDayOfWeekFromOlDaysOfWeek(pattern.DayOfWeekMask));

                                    CreateWeeklyOccurences(result, ai, pattern, daysOfWeekList);
                                    break;

                                case OlRecurrenceType.olRecursMonthly:
                                    case OlRecurrenceType.olRecursMonthNth:
                                        case OlRecurrenceType.olRecursYearly:
                                            case OlRecurrenceType.olRecursYearNth:
                                    CreateMonthlyOccurences(result, ai, pattern, daysOfWeekList);
                                    break;
                            }
                        }
                        else
                        {
                            result.Add(GetOutlookAppointment(ai));
                        }
                    }
                }
            }
            return result;
        }

        private List<DayOfWeek> FindDaysOfWeekFromMask(RecurrencePattern pattern, List<DayOfWeek> daysOfWeekList)
        {
            var binaryCountdown = 64;
            int dayOfWeekMask = Convert.ToInt16(pattern.DayOfWeekMask);
            for (int i = 6; i >= 0; i--)
            {
                if (binaryCountdown <= dayOfWeekMask)
                {
                    string dayOfWeek = Enum.GetName(typeof(DayOfWeek), i);
                    daysOfWeekList.Add((DayOfWeek)Enum.Parse(typeof(DayOfWeek), dayOfWeek));
                    dayOfWeekMask = (dayOfWeekMask - binaryCountdown);
                }
                binaryCountdown = (binaryCountdown / 2);
            }
            return daysOfWeekList;
        }

        static void CreateDailyOccurences(List<Appointment> result, _AppointmentItem ai, RecurrencePattern pattern)
        {
            DateTime incrementDate = ai.Start;
            while (incrementDate <= Settings.CalendarRangeMaxDate && incrementDate <= pattern.PatternEndDate)
            {
                if (incrementDate >= Settings.CalendarRangeMinDate)
                {
                    result.Add(GetOutlookAppointment(ai, incrementDate));
                }
                incrementDate = incrementDate.AddDays(pattern.Interval);
            }
        }

        static void CreateWeeklyOccurences(List<Appointment> result, _AppointmentItem ai, RecurrencePattern pattern, List<DayOfWeek> daysOfWeekList)
        {
            int interval = pattern.Interval == 0 ? 1 : pattern.Interval;
            int weekCount = interval;
            DateTime incrementDate = Settings.CalendarRangeMinDate;
            while (incrementDate <= Settings.CalendarRangeMaxDate && incrementDate <= pattern.PatternEndDate)
            {
                if (daysOfWeekList.Contains(incrementDate.DayOfWeek) && (weekCount % interval) == 0)
                {
                    result.Add(GetOutlookAppointment(ai, incrementDate));
                }
                incrementDate = incrementDate.AddDays(1);
                if (incrementDate.DayOfWeek == DayOfWeek.Sunday) weekCount++; 
            }
        }

        static void CreateMonthlyOccurences(List<Appointment> result, _AppointmentItem ai, RecurrencePattern pattern, List<DayOfWeek> daysOfWeekList)
        {
            DateTime incrementDate = GetIncrementDate(pattern, pattern.PatternStartDate);
            while (incrementDate <= Settings.CalendarRangeMaxDate)
            {
                if (incrementDate >= Settings.CalendarRangeMinDate && incrementDate <= pattern.PatternEndDate)
                {
                    result.Add(GetOutlookAppointment(ai, incrementDate));
                }
                incrementDate = GetIncrementDate(pattern, incrementDate.AddMonths(pattern.Interval));
            }
        }

        static DateTime GetIncrementDate(RecurrencePattern pattern, DateTime apptBeginDate)
        {
            DateTime incrementDate;
            if (pattern.DayOfMonth == 0)
            {
                DayOfWeek dayOfWeek = GetDayOfWeekFromOlDaysOfWeek(pattern.DayOfWeekMask);
                incrementDate = GetDateFromWeekNumber(dayOfWeek, pattern.Instance, new DateTime(apptBeginDate.Year, apptBeginDate.Month, 1));
            }

            else
            {
                incrementDate = new DateTime(apptBeginDate.Year, apptBeginDate.Month, pattern.DayOfMonth);
            }
            return incrementDate;
        }

        static DateTime GetDateFromWeekNumber(DayOfWeek dayOfWeekMask, int instance, DateTime dtFirstOfMonth)
        {
            DateTime dt = dtFirstOfMonth.DayOfWeek == dayOfWeekMask ? dtFirstOfMonth : dtFirstOfMonth.AddDays(dayOfWeekMask - dtFirstOfMonth.DayOfWeek);

            if (dt.Month == dtFirstOfMonth.Month) instance--;

            dt = dt.AddDays(7 * instance);
            return dt;
        }

        static DayOfWeek GetDayOfWeekFromOlDaysOfWeek(OlDaysOfWeek dayOfWeek)
        {
            return (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dayOfWeek.ToString().Replace("ol", ""));
        }

        static Appointment GetOutlookAppointment(_AppointmentItem appointment)
        {
            var newAppointment = new Appointment
            {
                Description = appointment.Body,
                Summary = appointment.Subject,
                Start = appointment.Start,
                End = appointment.End,
                IsAllDayEvent = appointment.AllDayEvent,
                Location = appointment.Location,
                OptionalAttendees = GetAttendees(appointment.OptionalAttendees),
                RequiredAttendees = GetAttendees(appointment.RequiredAttendees),
                Organizer = appointment.Organizer,
                ReminderMinutesBeforeStart = appointment.ReminderMinutesBeforeStart,
                IsReminderSet = appointment.ReminderSet
            };
            return newAppointment;
        }

        static Appointment GetOutlookAppointment(_AppointmentItem appointment, DateTime startDate)
        {
            DateTime newStartDate = DateTime.Parse(startDate.ToShortDateString() + " " + appointment.Start.ToShortTimeString());
            DateTime newEndDate = newStartDate.AddMinutes(appointment.Duration);

            var newAppointment = new Appointment
            {
                Description = appointment.Body,
                Summary = appointment.Subject,
                Start = newStartDate,
                End = newEndDate,
                IsAllDayEvent = appointment.AllDayEvent,
                Location = appointment.Location,
                OptionalAttendees = GetAttendees(appointment.OptionalAttendees),
                RequiredAttendees = GetAttendees(appointment.RequiredAttendees),
                Organizer = appointment.Organizer,
                ReminderMinutesBeforeStart = appointment.ReminderMinutesBeforeStart,
                IsReminderSet = appointment.ReminderSet
            };
            return newAppointment;
        }

        static List<string> GetAttendees(string attendees)
        {
            var attendeeList = new List<string>();

            if (attendees == null) return attendeeList;

            string[] tmp1 = attendees.Split(';');

            for (int i = 0; i < tmp1.Length; i++)
            {
                attendeeList.Add(tmp1[i].Trim());
            }

            return attendeeList;
        }
    }
}
