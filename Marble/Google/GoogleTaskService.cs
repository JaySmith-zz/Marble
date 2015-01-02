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
using Google.Apis.Tasks.v1.Data;

namespace Marble.Google
{
	/// <summary>
	/// Description of GoogleTaskService.
	/// </summary>
	public class GoogleTaskService
	{
		readonly GoogleClient client;
		readonly TasksService service;
		
		public GoogleTaskService(GoogleClient googleClient)
		{
			client = googleClient;
			service = new TasksService(googleClient.Initializer);
		}
		
		public List<TaskList> GetTaskLists()
		{
			return service.Tasklists.List().Execute().Items as List<TaskList>;
		}
	}
}
