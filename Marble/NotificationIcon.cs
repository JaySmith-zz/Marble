using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using NLog;
using Marble.Data;

namespace Marble
{
	public sealed class NotificationIcon
	{
	    readonly NotifyIcon _notifyIcon;

	    DateTime _lastSyncTime;
	    public readonly System.Windows.Forms.Timer SyncTimer;

	    public static Logger Logger;
		
		public NotificationIcon()
		{
		    Logger = LogManager.GetCurrentClassLogger();
			_notifyIcon = new NotifyIcon();
			var notificationMenu = new ContextMenu(InitializeMenu());
			
			var resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationIcon));
			_notifyIcon.Icon = (Icon)resources.GetObject("$this.Icon");
			_notifyIcon.ContextMenu = notificationMenu;
			
			SyncTimer = new System.Windows.Forms.Timer { Interval = 60000 };
			SetupTimer();
		}

	    private void SetupTimer()
		{
			if (SyncTimer.Enabled) SyncTimer.Stop();
	
			SyncTimer.Tick += SyncTimerTick;
					
			_lastSyncTime = DateTime.Now;
			SyncTimer.Start();
		}

	    public void SyncTimerTick(object sender, EventArgs e)
		{	
			var newtime = DateTime.Now;
			
			if (Settings.SyncFrequencyType == SyncFrequencyType.EveryHour)
			{
			    if (newtime.Minute == _lastSyncTime.Minute) return;
			    _lastSyncTime = newtime;
			    if (newtime.Minute == Settings.SyncHourlyMinutesOffset)
			    {
			        Sync();
			    }
			} else {
				var minutesSinceLastSync = (newtime - _lastSyncTime).Minutes;
			    if (minutesSinceLastSync != Settings.SyncEveryNMinutes) return;
			    _lastSyncTime = newtime;
			    Sync();
			}
		}

	    private MenuItem[] InitializeMenu()
		{
			var menu = new[] {
				new MenuItem("Sync Now", MenuSyncCalendarClick),
				new MenuItem("Clear Items and Cache", MenuClearRemoteCalendarClick),
				new MenuItem("Clear Cache", MenuClearCache),
				new MenuItem("Open Cache Location", MenuOpenCacheLocation),
				new MenuItem("-"),
				new MenuItem("Settings...", MenuSettingsClick),
				new MenuItem("About", MenuAboutClick),
				new MenuItem("-"),
				new MenuItem("Exit", MenuExitClick)
			};
			
			return menu;
		}
		
		/// <summary>Program entry point.</summary>
		/// <param name="args">Command Line Arguments</param>
		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			bool isFirstInstance;
			// Please use a unique name for the mutex to prevent conflicts with other programs
			using (new Mutex(true, "5CD5D07B-6285-48BF-BE5D-027C4EC8C8E3", out isFirstInstance))
			{
			    if (isFirstInstance) {
			        try
			        {
			            var notificationIcon = new NotificationIcon();
			            notificationIcon._notifyIcon.Visible = true;
			            Application.Run();
			            notificationIcon._notifyIcon.Dispose();
			        }
			        catch (Exception ex)
			        {
			            Logger.Error(ex);
			        }
			    } else {
			        // The application is already running
			        Logger.Info("Marble arleady running shutting down");
			    }
			} // releases the Mutex
			//logger.Information("Marble shutting down");
		}

		private static void MenuAboutClick(object sender, EventArgs e)
		{
			new FormAbout().ShowDialog();
		}
		
		private static void MenuExitClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		private static void MenuSettingsClick(object sender, EventArgs e)
		{
			new FormSettings().ShowDialog();
		}
	
		private void MenuSyncCalendarClick(object sender, EventArgs e)
		{
			Sync();
		}

	    public void MenuOpenCacheLocation(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(AppointmentSerialization.AppointmentDataStorePath());
		}
		
		private static void MenuClearCache(object sender, EventArgs e)
		{
			//var calendarSync = new CalendarSyncCached();
			//calendarSync.ClearCache();
			AppointmentSerialization.Clear();
		}

	    public void MenuClearRemoteCalendarClick(object sender, EventArgs e)
		{
//			var syncInfo = calenderSync.ClearAllRemoteItems();
			new CalendarSyncCached().RemoveEventsAndClearCache();
			//DisplayBalloonTip(syncInfo);
		}
				
		private void Sync()
		{
			var calendarSync = new CalendarSyncCached();
			var syncInfo = calendarSync.Sync();
			DisplayBalloonTip(syncInfo);
		}
		
		private void DisplayBalloonTip(CalendarSyncInfo syncInfo)
		{
			if (!Settings.ShowNotifications || syncInfo.Status == CalendarSyncStatus.Skipped) return;
			
			if (syncInfo.Status == CalendarSyncStatus.Success) _notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
			if (syncInfo.Status == CalendarSyncStatus.Failed) _notifyIcon.BalloonTipIcon = ToolTipIcon.Error;
			_notifyIcon.BalloonTipTitle = "Marble Status";
			_notifyIcon.BalloonTipText = string.Format("{0}\nCalendar Sync Complete!\n {1} added, {2} removed", 
			                                          syncInfo.Text, syncInfo.ItemsAddCount, syncInfo.ItemsRemovedCount);
			_notifyIcon.ShowBalloonTip(5000);
		}
	}
}
