using NLog;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Marble
{
	public class Program
	{
		private static Logger Logger; 

		/// <summary>Program entry point.</summary>
		/// <param name="args">Command Line Arguments</param>
		[STAThread]
		public static void Main(string[] args)
		{
			Logger = LogManager.GetCurrentClassLogger();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// Please use a unique name for the mutex to prevent conflicts with other programs
			using (new Mutex(true, "5CD5D07B-6285-48BF-BE5D-027C4EC8C8E3", out bool isFirstInstance))
			{
				if (isFirstInstance)
				{
					try
					{
						var notificationIcon = new NotificationIcon();
						notificationIcon.NotifyIcon.Visible = true;
						Application.Run();

						notificationIcon.NotifyIcon.Dispose();
					}
					catch (Exception ex)
					{
						Logger.Error(ex);
					}
				}
				else
				{
					Logger.Info("Marble arleady running shutting down");
				}
			} // releases the Mutex
		}
	}
}
