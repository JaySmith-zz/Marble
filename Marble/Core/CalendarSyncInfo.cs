/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 1/28/2016
 * Time: 9:17 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Marble
{
	/// <summary>
	/// Description of CalendarSyncInfo.
	/// </summary>
	public class CalendarSyncInfo
	{
		public CalendarSyncStatus Status { get; set; }
		public string Text { get; set; }
		public int ItemsAddCount { get; set; }
		public int ItemsRemovedCount { get; set; }
	}
}
