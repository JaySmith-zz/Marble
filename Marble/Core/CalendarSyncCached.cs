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

using Google.Apis.Calendar.v3.Data;

namespace Marble
{
	/// <summary>
	/// Description of CalendarSyncCached.
	/// </summary>
	public class CalendarSyncCached
	{
		readonly IOutlookCalendarService outlookCalendarService;
		
		readonly GoogleClient googleClient;
        readonly GoogleCalendarService googleCalendarService;
        readonly AppointmentCache cache;
        
        static Logger Logger;
		        
		public CalendarSyncCached()
		{
			Logger = LogManager.GetCurrentClassLogger();
			
			cache = new AppointmentCache();
			
			googleClient = new GoogleClient(Settings.DataStoreFolderNameCalendar);
            googleCalendarService = new GoogleCalendarService(googleClient);
            outlookCalendarService = OutlookCalendarServiceFactory.Instance();
		}
		
		public CalendarSyncInfo Sync()
        {
			var syncInfo = new CalendarSyncInfo();
        	
        	if (!Settings.IsValid) 
        	{
        		syncInfo.Status = CalendarSyncStatus.Skipped;
        		return syncInfo;
        	}
        	
			// Get Current appointments from outlook
			List<Appointment> appointments = outlookCalendarService.GetAppointmentsInRange();
				
			var comparer = new AppointmentComparer();
			
			// Find all appointments in cache not in outlook, need to be removed
			var toBeRemoved = cache.Items.Except(appointments, comparer).ToList();
			syncInfo.ItemsRemovedCount = toBeRemoved.Count;
			RemoveEvents(toBeRemoved);
			
			// Find all appointments in outlook not in cache, need to be added
			var toBeAdded = appointments.Except(cache.Items, comparer).ToList();
			syncInfo.ItemsAddCount = toBeAdded.Count;
			AddEvents(toBeAdded);
			
			cache.Save();
			
			return syncInfo;
		}
		
		void AddEvents(List<Appointment> appointments)
        {
            foreach (Appointment appointment in appointments)
            {
            	var googleEvent = AppointmentMapper.MapToGoogleEvent(appointment);
				var newEvent = googleCalendarService.AddEvent(googleEvent);
				
				appointment.GoogleId = newEvent.Id;
				cache.Add(appointment);
            }
        }

		void RemoveEvents(List<Appointment> appointments)
		{
			var toBeRemoved = new List<Appointment>();
		    foreach (Appointment appointment in appointments)
            {
		    	try
		    	{
		    		googleCalendarService.RemoveEvent(Settings.CalendarAccount, appointment.GoogleId);
            			
		    	} 
		    	catch (Exception ex)
		    	{
		    		Logger.Error(ex);
		    	}
		    	finally
		    	{
		    		toBeRemoved.Add(appointment);
		    	}
            }
		    
		    foreach (var appointment in toBeRemoved) 
		    {
		    	cache.Remove(appointment);
		    }
		}
		
		public void RemoveEventsAndClearCache()
		{
			RemoveEvents(cache.Items);
			ClearCache();
		}	
		
		public void ClearCache()
		{
			cache.Clear();
		}
	
	}
}
		

		 

		 
		

