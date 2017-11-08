/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 3/11/2016
 * Time: 12:59 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Marble
{
	/// <summary>
	/// Description of CalendarServiceFactory.
	/// </summary>
	public static class CalendarServiceFactory
	{

		public static ICalendarService Instance()
		{
			ICalendarService service = null;
			var sourceCalendarProvider = (OutlookServiceProvider)Enum.Parse(typeof(OutlookServiceProvider), Settings.OutlookCalendarServiceProvider);
			
			if (sourceCalendarProvider == OutlookServiceProvider.Outlook)
			{
				service = new CalendarServiceOutlook();
			}
			else if (sourceCalendarProvider == OutlookServiceProvider.Exchange)
			{
				service = new Exchange.ExchangeService();
			}
			
			return service;
		}
	}
}
