/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 3/7/2016
 * Time: 11:43 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Marble.Data;
using Marble.Google;

using NLog;
using System.Diagnostics;

namespace Marble
{
    /// <summary>
    /// Description of CalendarSyncCached.
    /// </summary>
    public class CalendarSyncCached
    {
        private readonly IOutlookCalendarService _outlookCalendarService;

        private readonly GoogleCalendarService _googleCalendarService;
        private readonly AppointmentCache _cache;

        private static Logger _logger;

        public CalendarSyncCached()
        {
            _logger = LogManager.GetCurrentClassLogger();

            _cache = new AppointmentCache();

            var googleClient = new GoogleClient(Settings.DataStoreFolderNameCalendar);
            _googleCalendarService = new GoogleCalendarService(googleClient);
            _outlookCalendarService = OutlookCalendarServiceFactory.Instance();
        }

        public CalendarSyncInfo Sync()
        {

            var isOutlookRunning = IsOutlookClientRunning();

            var syncInfo = new CalendarSyncInfo();

            if (!Settings.IsValid)
            {
                syncInfo.Status = CalendarSyncStatus.Skipped;
                return syncInfo;
            }

            // Get Current appointments from outlook
            var appointments = _outlookCalendarService.GetAppointmentsInRange();

            var comparer = new AppointmentComparer();

            // Find all appointments in cache not in outlook, need to be removed
            var toBeRemoved = _cache.Items.Except(appointments, comparer).ToList();
            syncInfo.ItemsRemovedCount = toBeRemoved.Count;
            RemoveEvents(toBeRemoved);

            // Find all appointments in outlook not in cache, need to be added
            var toBeAdded = appointments.Except(_cache.Items, comparer).ToList();
            syncInfo.ItemsAddCount = toBeAdded.Count;
            AddEvents(toBeAdded);

            _cache.Save();

            return syncInfo;
        }

        private void AddEvents(IEnumerable<Appointment> appointments)
        {
            foreach (var appointment in appointments)
            {
                var googleEvent = AppointmentMapper.MapToGoogleEvent(appointment);
                var newEvent = _googleCalendarService.AddEvent(googleEvent);

                appointment.GoogleId = newEvent.Id;
                _cache.Add(appointment);
            }
        }

        private void RemoveEvents(IEnumerable<Appointment> appointments)
        {
            var toBeRemoved = new List<Appointment>();
            foreach (var appointment in appointments)
            {
                try
                {
                    _googleCalendarService.RemoveEvent(Settings.CalendarAccount, appointment.GoogleId);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
                finally
                {
                    toBeRemoved.Add(appointment);
                }
            }

            foreach (var appointment in toBeRemoved)
            {
                _cache.Remove(appointment);
            }
        }

        public void RemoveEventsAndClearCache()
        {
            RemoveEvents(_cache.Items);
            ClearCache();
        }

        public void ClearCache()
        {
            _cache.Clear();
        }

        private bool IsOutlookClientRunning()
        {
            int procCount = 0;
            Process[] processlist = Process.GetProcessesByName("OUTLOOK");
            foreach (Process theprocess in processlist)
            {
                procCount++;
            }

            return procCount > 0;
        }

    }
}







