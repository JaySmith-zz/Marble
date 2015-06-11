/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 6/10/2015
 * Time: 11:11 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Marble
{
	/// <summary>
	/// Description of Globals.
	/// </summary>
	public static class Globals
	{
		public static string CalendarAccount { 
			get { return Properties.Settings.Default.CalendarAccount; }
		}
		
		public static bool HasError { get; set; }
		
		public static string ErrorMessage { get; set; }
	}
}
