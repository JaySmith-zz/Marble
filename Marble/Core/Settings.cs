using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Marble
{
	/// <summary>
	/// Description of Settings.
	/// </summary>
	public static class Settings
	{
		static readonly AssemblyInfo assemblyInfo;
		
		static Settings()
		{
			assemblyInfo = new AssemblyInfo();
			
			CalendarAccount = Properties.Settings.Default.CalendarAccount;
			CalendarId = Properties.Settings.Default.CalendarId;
		}
		
		public static string ApplicationName
		{
			get
			{
				return Properties.Settings.Default.ApplicationName;
			}
		}
		
		public static string DataStoreFolderNameCalendar
		{
			get
			{
				return Properties.Settings.Default.DataStoreFolderNameCalendar;
			}
		}
		
		public static string DataStoreFolderNameTasks
		{
			get
			{
				return Properties.Settings.Default.DataStoreFolderNameTasks;
			}
		}
		
		public static string ScopeCalendar
		{
			get
			{
				return Properties.Settings.Default.ScopeCalendar;
			}
		}
		
		public static string ClientSecret
		{
			get
			{
				return Properties.Settings.Default.ClientSecret;
			}
		}
		
		public static string ClientId
		{
			get
			{
				return Properties.Settings.Default.ClientId;
			}
		}

		public static string CalendarId 
		{ 
			get
			{
				return Properties.Settings.Default.CalendarId;
			}
			set
			{
				Properties.Settings.Default.CalendarId = value;
			}
		}
		
		public static string CalendarAccount
		{ 
			get
			{
				return Properties.Settings.Default.CalendarAccount;
			}
			set
			{
				Properties.Settings.Default.CalendarAccount = value;
			}
		}
		
		public static int CalendarDaysInThePast
		{
			get
			{
				return Properties.Settings.Default.CalendarDaysInPast;
			}
			set
			{
				Properties.Settings.Default.CalendarDaysInPast = value;
			}
		}
		
		public static int CalendarDaysInTheFuture
		{
			get { return Properties.Settings.Default.CalendarDaysInTheFuture;  }
			set { Properties.Settings.Default.CalendarDaysInTheFuture = value; }
		}

//		public static bool SyncEveryHour		
//		{
//			get { return Properties.Settings.Default.SyncEveryHour;  }
//			set { Properties.Settings.Default.SyncEveryHour = value; }
//		}
		
		public static SyncFrequencyType SyncFrequencyType	
		{
			get 
			{
				var enumReturn = (SyncFrequencyType)Enum.Parse(typeof(SyncFrequencyType), Properties.Settings.Default.SyncFrequencyType);
				return (SyncFrequencyType)Enum.Parse(typeof(SyncFrequencyType), Properties.Settings.Default.SyncFrequencyType);;  
			}
			set 
			{ 
				var newValue = value.ToString();
				Properties.Settings.Default.SyncFrequencyType = value.ToString();
			}
		}
		
		public static int SyncEveryNMinutes
		{
			get { return Properties.Settings.Default.SyncEveryNMinutes;  }
			set { Properties.Settings.Default.SyncEveryNMinutes = value; }
		}
		
		public static int SyncHourlyMinutesOffset		
		{
			get { return Properties.Settings.Default.SyncMinutesOffset;  }
			set { Properties.Settings.Default.SyncMinutesOffset = value; }
		}
		
		public static bool StartWithWindows		
		{
			get { return Properties.Settings.Default.StartWithWindows;  }
			set { Properties.Settings.Default.StartWithWindows = value; }
		}
		
		public static void Save()
		{
			Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
		}
		
		public static string Description
		{
			get
			{
				return assemblyInfo.Description;
			}
		}
		
		public static string ProductTitle
		{
			get
			{
				return assemblyInfo.ProductTitle;
			}
		}
		
		public static string Version
		{
			get
			{
				return assemblyInfo.Version;
			}
		}
		
		public static string Product
		{
			get
			{
				return assemblyInfo.Product;
			}
		}
		
		public static string Copyright
		{
			get
			{
				return assemblyInfo.Copyright;
			}
		}
		
		public static string Company
		{
			get
			{
				return assemblyInfo.Company;
			}
		}

        public static DateTime CalendarRangeMinDate
        {
            get
            {
               return Convert.ToDateTime(DateTime.Now.AddDays(-Settings.CalendarDaysInThePast).ToShortDateString());
            }
        }
        public static DateTime CalendarRangeMaxDate
        {
            get
            {
            	return Convert.ToDateTime(DateTime.Now.AddDays(+Settings.CalendarDaysInTheFuture + 1).ToShortDateString());
            }
        }
		
		public static string OutlookCalendarServiceProvider
		{
			get
			{
				return Properties.Settings.Default.OutlookCalendarServiceProvider;
			}
			set
			{
				Properties.Settings.Default.OutlookCalendarServiceProvider = value;
			}
		}
		
		public static List<string> OutlookServiceProviders
		{
			get 
			{
				return Enum.GetNames(typeof(OutlookServiceProvider)).ToList();
			}
		}

        public static string ExchangeEmailAddress
        {
            get
            {
                return Properties.Settings.Default.ExchangeEmail;
            }
            set
            {
                Properties.Settings.Default.ExchangeEmail = value;
            }
        }
        
        public static bool ShowNotifications
        {
            get
            {
                return Properties.Settings.Default.ShowNotifications;
            }
            set
            {
                Properties.Settings.Default.ShowNotifications = value;
            }
        }
		
	}
}
