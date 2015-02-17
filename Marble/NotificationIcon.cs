using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Marble
{
	public sealed class NotificationIcon
	{
		private NotifyIcon notifyIcon;
		private ContextMenu notificationMenu;
		
		private DateTime lastSyncTime;
		private System.Windows.Forms.Timer syncTimer;
		
		public NotificationIcon()
		{
			syncTimer = new System.Windows.Forms.Timer { Interval = 30000 };
			syncTimer.Tick += syncTimerTick;
			lastSyncTime = DateTime.Now;
			syncTimer.Start();
			
			notifyIcon = new NotifyIcon();
			notificationMenu = new ContextMenu(InitializeMenu());
			
			//notifyIcon.DoubleClick += IconDoubleClick;
			var resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationIcon));
			notifyIcon.Icon = (Icon)resources.GetObject("$this.Icon");
			notifyIcon.ContextMenu = notificationMenu;
		}

		void syncTimerTick(object sender, EventArgs e)
		{
			if (!Settings.SyncEveryHour) return;
            DateTime newtime = DateTime.Now;
            if (newtime.Minute != lastSyncTime.Minute)
            {
                lastSyncTime = newtime;
                if (newtime.Minute == Settings.SyncMinutesOffset)
                {
                	Sync();
                }
            }
		}
		
		MenuItem[] InitializeMenu()
		{
			var menu = new MenuItem[] {
				//new MenuItem("Sync All", menuSyncAllClick),
				new MenuItem("Sync Calendar", menuSyncCalendarClick),
				//new MenuItem("Sync Tasks", menuTasksClick),
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
			var logger = LogFactory.GetLoggerFor(typeof(NotificationIcon));
			logger.Information("Marble Started");
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			bool isFirstInstance;
			// Please use a unique name for the mutex to prevent conflicts with other programs
			using (Mutex mtx = new Mutex(true, "Marble", out isFirstInstance)) {
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
						logger.Fatal("Unhandled Exception", ex);
					}
				} else {
					// The application is already running
					// TODO: change focus to existing application instance
					logger.Information("Marble arleady running shutting down");
					
				}
			} // releases the Mutex
			logger.Information("Marble shutting down");
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
		
		private void menuSyncAllClick(object sender, EventArgs e)
		{
			var sync = new MarbleSync();
			sync.SyncAll();
		}
		
		private void menuSyncCalendarClick(object sender, EventArgs e)
		{
			var sync = new MarbleSync();
			sync.SyncCalendar();
		}
		
//		private void IconDoubleClick(object sender, EventArgs e)
//		{
//			MessageBox.Show("The icon was double clicked");
//		}

		private void Sync()
		{
			var sync = new MarbleSync();
			sync.SyncCalendar();
		}
	}
}
