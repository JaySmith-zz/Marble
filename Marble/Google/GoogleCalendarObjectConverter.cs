/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 2/26/2016
 * Time: 2:22 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Marble;

using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

namespace Marble.Google
{
	/// <summary>
	/// Description of GoogleCalendarObjectConverter.
	/// </summary>
	public class GoogleCalendarObjectConverter
	{
		public GoogleCalendarObjectConverter()
		{
		}
		
		public Appointment MapGoogleEventtoAppointment(Event googleEvent)
		{
			var appointment = new Appointment();
			
			try
			{
				appointment.Id = googleEvent.Id;
				
				if (googleEvent.Start.DateTime == null)
				{
					appointment.IsAllDayEvent = true;
					appointment.Start = DateTime.Parse(googleEvent.Start.Date);
					appointment.End = DateTime.Parse(googleEvent.End.Date);
				}
				else 
				{
					appointment.IsAllDayEvent = false;
					appointment.Start = (DateTime) googleEvent.Start.DateTime;
					appointment.End = (DateTime) googleEvent.End.DateTime;
				}
				

				appointment.Summary = googleEvent.Summary;
				appointment.Location = string.IsNullOrEmpty(googleEvent.Location) ? "none" : googleEvent.Location;
			}
			catch(Exception ex)
			{
				Globals.HasError = true;
				Globals.ErrorMessage = "An error occured while updated Google Calendar.  Please see the log file for more details.";
				Logger.Fatal(ex.Message, ex);
			}
			
			return appointment;
		}
	}
}
