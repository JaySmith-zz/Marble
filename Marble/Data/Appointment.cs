/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 1/16/2015
 * Time: 3:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Marble.Data
{
	/// <summary>
	/// Description of Appointment.
	/// </summary>
	public class Appointment
	{
		public string Id { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public string Description { get; set; }
		public string Summary { get; set; }
		public string Location { get; set; }
		public bool IsAllDayEvent { get; set; }
		public bool IsReminderSet { get; set; }
		public int ReminderMinutesBeforeStart { get; set; }
		public string Organizer { get; set; }	
		public List<string> RequiredAttendees { get; set; }
		public List<string> OptionalAttendees { get; set; }
		
		
		public string GoogleId { get; set; }
				
	}
	
	public class AppointmentComparer : IEqualityComparer<Appointment> 
    { 
       
		public bool Equals(Appointment x, Appointment y)
		{
       	
			//Check whether the compared objects reference the same data.
			if (Object.ReferenceEquals(x, y)) return true;
			
			//Check whether any of the compared objects is null.
			if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
			    return false;
			
			var isMatch = x.Start == y.Start && x.End == y.End && x.Summary == y.Summary;// && x.Location == y.Location;
			
			return isMatch;
		
		} 
 
		public int GetHashCode(Appointment appointment)
		{
			//Check whether the object is null
			if (Object.ReferenceEquals(appointment, null)) return 0;
			
			int hashStart = appointment.Start == DateTime.MinValue ? 0 : appointment.Start.GetHashCode();
			int hashEnd = appointment.End == DateTime.MinValue ? 0 : appointment.End.GetHashCode();
			int hashSummary = appointment.Summary == null ? 0 : appointment.Summary.GetHashCode();
			//int hashLocation = appointment.Location == null ? 0 : appointment.Location.GetHashCode();
			
			//Calculate the hash code for the product.
			var returnValue = hashStart ^ hashEnd ^ hashSummary;// ^ hashLocation;
			
			return returnValue;

		}
   } 
}
