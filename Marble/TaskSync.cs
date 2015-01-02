/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 1/2/2015
 * Time: 3:54 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using Marble.Google;
using Marble.Outlook;

namespace Marble
{
	/// <summary>
	/// Description of TaskSync.
	/// </summary>
	public class TaskSync
	{
		readonly GoogleClient googleClient;
		readonly GoogleTaskService service;
		
		readonly OutlookClient outlookClient;
		
		public TaskSync()
		{
			googleClient = new GoogleClient(Settings.DataStoreFolderNameCalendar, Settings.ScopeTasks);
			service = new GoogleTaskService(googleClient);
			
			outlookClient = new OutlookClient();
		}
		
		public void Sync()
		{
			var items = service.GetTaskLists();
		}
	}
}
