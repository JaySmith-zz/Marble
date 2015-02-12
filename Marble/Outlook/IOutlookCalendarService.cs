/*
 * Created by SharpDevelop.
 * User: SMITHJAY
 * Date: 2/10/2015
 * Time: 9:50 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Marble.Data;

namespace Marble
{
	public interface IOutlookCalendarService
	{
		List<Appointment> GetAppointmentsInRange();
	}
}


