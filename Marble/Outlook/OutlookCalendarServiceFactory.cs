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
	/// Description of OutlookCalendarServiceFactory.
	/// </summary>
	public static class OutlookCalendarServiceFactory
	{

		public static IOutlookCalendarService Instance()
		{
			IOutlookCalendarService service = null;
			var sourceCalendarProvider = (OutlookServiceProvider)Enum.Parse(typeof(OutlookServiceProvider), Settings.OutlookCalendarServiceProvider);
			
			if (sourceCalendarProvider == OutlookServiceProvider.Interop)
            {
                service = new OulookCalendarService_Introp();
            }
            else if (sourceCalendarProvider == OutlookServiceProvider.Exchange)
            {
                service = new Exchange.ExchangeService();
            }
            else
            {
                service = new OutlookCalendarService();
            }
            
            return service;
		}
	}
}
