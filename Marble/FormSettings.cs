/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 12/16/2014
 * Time: 8:41 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

using Microsoft.Win32;

using Marble.Google;

namespace Marble
{
	/// <summary>
	/// Description of FormSettings.
	/// </summary>
	public partial class FormSettings : Form
	{
		readonly GoogleClient googleClient;
		readonly GoogleCalendarService calendarService;
		
		public FormSettings()
		{
			InitializeComponent();
			googleClient = new GoogleClient(Settings.DataStoreFolderNameCalendar, "Calendar");
			calendarService = new GoogleCalendarService(googleClient);
		}
		
		void FormSettingsLoad(object sender, EventArgs e)
		{
			InitializeForm();
		}
		
		void InitializeForm()
		{
			GetCalendars();
			labelSelectedAccountName.Text = Settings.CalendarAccount;
			checkBoxSyncEveryHour.Checked = Settings.SyncEveryHour;
			textBoxMinuteOffset.Text = Settings.SyncMinutesOffset.ToString();
			checkBoxStartWithWindows.Checked = Settings.StartWithWindows;
		}
		
		void ButtonGetCalendarsClick(object sender, EventArgs e)
		{
			GetCalendars();
		}
		
		void ButtonOkClick(object sender, EventArgs e)
		{
			var selectedItem =  (GoogleCalendarInfo) comboBoxCalendars.SelectedItem;
			
			Settings.CalendarAccount = selectedItem.Id;
			Settings.CalendarId = selectedItem.Name;
			Settings.SyncEveryHour = checkBoxSyncEveryHour.Checked;
			Settings.SyncMinutesOffset = int.Parse(textBoxMinuteOffset.Text);
			Settings.StartWithWindows = checkBoxStartWithWindows.Checked;
			
			Settings.Save();
			
			ConfigureWindowsStartUp();
			
			DialogResult = DialogResult.OK;
			Close();
		}
		
		void ButtonCancelClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
		
		void GetCalendars()
		{
			buttonGetCalendars.Enabled = false;
			comboBoxCalendars.Enabled = false;
			
			var calendars = calendarService.GetCalendars();
			
			comboBoxCalendars.Items.Clear();
			foreach (var item in calendars) {
				comboBoxCalendars.Items.Add(item);
			}
			
			comboBoxCalendars.SelectedIndex = 0;
			buttonGetCalendars.Enabled = true;
			comboBoxCalendars.Enabled = true;
			
			GoogleCalendarInfo selectedItem = comboBoxCalendars.SelectedItem as GoogleCalendarInfo;
			Settings.CalendarId = selectedItem.Id;
			Settings.Save();
			
		}
		
		void ButtonClearDataStoreClick(object sender, EventArgs e)
		{
			googleClient.ClearDataStore();
			Settings.CalendarId = string.Empty;
			Settings.CalendarAccount = string.Empty;
			Settings.Save();
			
			googleClient.GetAuthorization();
			
			InitializeForm();
		}
		
		void ConfigureWindowsStartUp()
		{
            const string path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            var key = Registry.CurrentUser.OpenSubKey(path, true);

            if (Settings.StartWithWindows)
            {
                // set value in registry
                if (key != null) key.SetValue(Application.ProductName, Application.ExecutablePath);
            }
            else
            {
                // remove value from registry
                if (key != null) key.DeleteValue(Application.ProductName, false);
            }
		}
	}
}
