/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 1/2/2015
 * Time: 9:03 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using Google.Apis.Calendar.v3;
using Google.Apis.Tasks.v1;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;

namespace Marble.Google
{
	/// <summary>
	/// Description of GoogleClient.
	/// </summary>
	public class GoogleClient
	{
		FileDataStore FileDataStore { get; set; }
		string ClientScope { get; set; }
		
		public BaseClientService.Initializer Initializer { get; set; }
		
		public GoogleClient(string dataStoreFolderName, string clientScope)
		{
			FileDataStore = new FileDataStore(dataStoreFolderName);
			ClientScope = clientScope;
			GetAuthorization();
		}
		
		public void GetAuthorization()
		{
			UserCredential credential;
			
			var scopes = new List<string>();
			var user = String.IsNullOrEmpty(Settings.UserName) ? "@gmail.com" : Settings.UserName;

			if (ClientScope.Equals("Tasks"))
		    {
				scopes.Add(TasksService.Scope.Tasks);
			}
			
			if (ClientScope.Equals("Calendar"))
			{
				scopes.Add(CalendarService.Scope.Calendar);
			}
			
			using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker
                	.AuthorizeAsync(
                    	GoogleClientSecrets.Load(stream).Secrets,
                    	scopes, 
                    	"@gmail.com", 
                    	CancellationToken.None,
                    	FileDataStore
                   ).Result;
            }
			
			Initializer = new BaseClientService.Initializer{
				HttpClientInitializer = credential,
				ApplicationName = Settings.ApplicationName
			};
			
		}
		
		public void ClearDataStore()
		{
			FileDataStore.ClearAsync();
			Settings.UserCalendar = string.Empty;
			Settings.UserName = string.Empty;
		}
	}
}
