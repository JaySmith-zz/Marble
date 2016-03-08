using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using NLog;

namespace Marble
{
	public sealed class NotificationIcon
	{
		NotifyIcon notifyIcon;
		ContextMenu notificationMenu;
		
		DateTime lastSyncTime;
		System.Windows.Forms.Timer syncTimer;
		
		static Logger Logger;
		
		public NotificationIcon()
		{
			Logger = LogManager.GetCurrentClassLogger();
			notifyIcon = new NotifyIcon();
			notificationMenu = new ContextMenu(InitializeMenu());
			
			var resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationIcon));
			notifyIcon.Icon = (Icon)resources.GetObject("$this.Icon");
			notifyIcon.ContextMenu = notificationMenu;
			
			syncTimer = new System.Windows.Forms.Timer { Interval = 60000 };
			SetupTimer();
		}
		
		void SetupTimer()
		{
			if (syncTimer.Enabled) syncTimer.Stop();
	
			syncTimer.Tick += syncTimerTick;
					
			lastSyncTime = DateTime.Now;
			syncTimer.Start();
		}

		void syncTimerTick(object sender, EventArgs e)
		{	
			DateTime newtime = DateTime.Now;
			
			if (Settings.SyncFrequencyType == SyncFrequencyType.EveryHour)
			{
	            if (newtime.Minute != lastSyncTime.Minute)
	            {
	                lastSyncTime = newtime;
	                if (newtime.Minute == Settings.SyncHourlyMinutesOffset)
	                {
	                	Sync();
	                }
	            }
			} else {
				var minutesSinceLastSync = (newtime - lastSyncTime).Minutes;
				if (minutesSinceLastSync == Settings.SyncEveryNMinutes)
				{
					lastSyncTime = newtime;
					Sync();
				}
			}
		}
		
		MenuItem[] InitializeMenu()
		{
			var menu = new MenuItem[] {
				new MenuItem("Sync Now", menuSyncCalendarClick),
				new MenuItem("Delete Remote Items In Range", menuClearRemoteCalendarClick),
				new MenuItem("Clear Cache", menuClearCache),
				new MenuItem("-"),
				new MenuItem("Settings...", menuSettingsClick),
				new MenuItem("About", menuAboutClick),
				new MenuItem("-"),
				new MenuItem("Exit", menuExitClick)
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
			using (Mutex mtx = new Mutex(true, "5CD5D07B-6285-48BF-BE5D-027C4EC8C8E3", out isFirstInstance)) {
				if (isFirstInstance) {
					try
					{
					NotificationIcon notificationIcon = new NotificationIcon();
					notificationIcon.notifyIcon.Visible = true;
					Application.Run();
					notificationIcon.notifyIcon.Dispose();
					}
					catch (Exception ex)
					{
						MessageBox.Show("Unhandled Exception");
					}
				} else {
					// The application is already running
					// TODO: change focus to existing application instance
					Logger.Information("Marble arleady running shutting down");
					
					
				}
			} // releases the Mutex
			//logger.Information("Marble shutting down");
		}

		private void menuAboutClick(object sender, EventArgs e)
		{
			new FormAbout().ShowDialog();
		}
		
		private void menuExitClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		private void menuSettingsClick(object sender, EventArgs e)
		{
			new FormSettings().ShowDialog();
		}
	
		private void menuSyncCalendarClick(object sender, EventArgs e)
		{
			Sync();
		}
		
		private void menuClearCache(object sender, EventArgs e)
		{
			new CalendarSyncCached().ClearCache();
		}
		
		private void menuClearRemoteCalendarClick(object sender, EventArgs e)
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
			
			if (syncInfo.Status == CalendarSyncStatus.Success) notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
			if (syncInfo.Status == CalendarSyncStatus.Failed) notifyIcon.BalloonTipIcon = ToolTipIcon.Error;
			notifyIcon.BalloonTipTitle = "Marble Status";
			notifyIcon.BalloonTipText = string.Format("{0}\nCalendar Sync Complete!\n {1} added, {2} removed", 
			                                          syncInfo.Text, syncInfo.ItemsAddCount, syncInfo.ItemsRemovedCount);
			notifyIcon.ShowBalloonTip(5000);
		}
	}
}
