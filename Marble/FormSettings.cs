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

using Marble.Google;

namespace Marble
{
	/// <summary>
	/// Description of FormSettings.
	/// </summary>
	public partial class FormSettings : Form
	{
		GoogleClient googleClient;
		readonly GoogleCalendarService calendarService;
		
		public FormSettings()
		{
			InitializeComponent();
			googleClient = new GoogleClient();
			calendarService = new GoogleCalendarService(googleClient);
		}
		
		void FormSettingsLoad(object sender, EventArgs e)
		{
			InitializeForm();
		}
		
		void InitializeForm()
		{
			GetCalendars();
			labelSelectedAccountName.Text = Settings.UserName;
		}
		
		void ButtonGetCalendarsClick(object sender, EventArgs e)
		{
			GetCalendars();
		}
		
		void ButtonOkClick(object sender, EventArgs e)
		{
			var selectedItem =  (GoogleCalendarInfo) comboBoxCalendars.SelectedItem;
			
			Settings.UserCalendar = selectedItem.Name;
			Settings.UserName = selectedItem.Id;
			
			Settings.Save();
			
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
			Settings.UserName = selectedItem.Id;
			Settings.Save();
			
		}
		
		void ButtonClearDataStoreClick(object sender, EventArgs e)
		{
			googleClient.ClearDataStore();
			Settings.UserCalendar = string.Empty;
			Settings.UserName = string.Empty;
			Settings.Save();
			
			googleClient.GetAuthorization();
			
			InitializeForm();
		}
	}
}
