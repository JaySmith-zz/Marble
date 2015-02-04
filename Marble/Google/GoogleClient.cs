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
		//string ClientScope { get; set; }
		
		public BaseClientService.Initializer Initializer { get; set; }
		
		public GoogleClient(string dataStoreFolderName)
		{
			FileDataStore = new FileDataStore(dataStoreFolderName);
			//ClientScope = clientScope;
			GetAuthorization();
		}		
		public void GetAuthorization()
		{
			UserCredential credential;
			
			var scopes = new List<string>();
			scopes.Add(Settings.ScopeCalendar);
			
			var clientSecrets = new ClientSecrets() { ClientId = Settings.ClientId, ClientSecret = Settings.ClientSecret };
			
			credential = GoogleWebAuthorizationBroker
	        	.AuthorizeAsync(
	            	clientSecrets,
	            	scopes, 
	            	"user", 
	            	CancellationToken.None,
	            	FileDataStore
	           ).Result;

			Initializer = new BaseClientService.Initializer{
				HttpClientInitializer = credential,
				ApplicationName = Settings.ApplicationName
			};
		}
		
		public void ClearDataStore()
		{
			FileDataStore.ClearAsync();
			Settings.CalendarId = string.Empty;
			Settings.CalendarAccount = string.Empty;
		}
		
	}
}
