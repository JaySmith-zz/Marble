/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 1/2/2015
 * Time: 9:02 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

using Google.Apis.Tasks.v1;

namespace Marble.Google
{
	/// <summary>
	/// Description of GoogleTaskService.
	/// </summary>
	public class GoogleTaskService
	{
		readonly GoogleClient client;
		readonly TasksService Service;
		
		public GoogleTaskService(GoogleClient googleClient)
		{
			client = googleClient;
			Service = new TasksService(googleClient.Initializer);
		}
		
		public void GetTasks()
		{
			var taskList = Service.Tasklists.List().Execute().Items;
		//	var task = Service.Tasks.List(
		}
	}
}
