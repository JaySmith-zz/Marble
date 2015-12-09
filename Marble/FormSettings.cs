using System;
using System.Linq;
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
        private GoogleClient googleClient;
        private GoogleCalendarService calendarService;

        public FormSettings()
        {
            InitializeComponent();
        }

        void FormSettingsLoad(object sender, EventArgs e)
        {
            InitializeForm();
        }

        void InitializeForm()
        {
            GetCalendars();
            labelSelectedAccountName.Text = Settings.CalendarAccount;
            dropdownListCalendars.SelectedIndex = dropdownListCalendars.FindStringExact(Settings.CalendarId);

            checkBoxSyncEveryHour.Checked = Settings.SyncEveryHour;
            textBoxMinuteOffset.Text = Settings.SyncMinutesOffset.ToString();
            checkBoxStartWithWindows.Checked = Settings.StartWithWindows;
            textBoxSyncDaysInPast.Text = Settings.CalendarDaysInThePast.ToString();
            textBoxSyncDaysInFuture.Text = Settings.CalendarDaysInTheFuture.ToString();
            
            comboBoxOutlookServiceProvider.DataSource = Settings.OutlookServiceProviders;
            comboBoxOutlookServiceProvider.SelectedIndex = comboBoxOutlookServiceProvider.FindStringExact(Settings.OutlookCalendarServiceProvider);
            textBoxExchangeEmail.Text = Settings.ExchangeEmailAddress;
            checkBoxShowNotifications.Checked = Settings.ShowNotifications;
        }

        void ButtonGetCalendarsClick(object sender, EventArgs e)
        {
            GetCalendars();
        }

        void ButtonOkClick(object sender, EventArgs e)
        {
            var selectedItem = (GoogleCalendarInfo)dropdownListCalendars.SelectedItem;
            
            var googleCalendarValid = selectedItem != null;
            var exchangeDataValid = (comboBoxOutlookServiceProvider.SelectedItem.ToString() == "Exchange") == !string.IsNullOrEmpty(textBoxExchangeEmail.Text);

            if (googleCalendarValid && exchangeDataValid)
            {
            Settings.CalendarAccount = selectedItem.Id;
            Settings.CalendarId = selectedItem.Name;
            Settings.SyncEveryHour = checkBoxSyncEveryHour.Checked;
            Settings.SyncMinutesOffset = int.Parse(textBoxMinuteOffset.Text);
            Settings.StartWithWindows = checkBoxStartWithWindows.Checked;
            Settings.CalendarDaysInThePast = int.Parse(textBoxSyncDaysInPast.Text);
            Settings.CalendarDaysInTheFuture = int.Parse(textBoxSyncDaysInFuture.Text);
            Settings.OutlookCalendarServiceProvider = comboBoxOutlookServiceProvider.SelectedItem.ToString();
            Settings.ExchangeEmailAddress = textBoxExchangeEmail.Text;
            Settings.ShowNotifications = checkBoxShowNotifications.Checked;
            
            Settings.Save();
            
            ConfigureWindowsStartUp();

            DialogResult = DialogResult.OK;
            Close();
            }
            else 
            {
            	DisplayErrorMessage(googleCalendarValid, exchangeDataValid);	
            }
        }
        
        void DisplayErrorMessage(bool googleValid, bool exchangeValid)
        {
        	var message = "Please address the following:";
        	
        	if (!googleValid) message += "\n\nGoogle Calendar must be seleced.";
        	if (!exchangeValid) message += "\n\nExchange email required when using Exchange provider.";
        	
        	MessageBox.Show(message, "Invalid settings", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        
        void ButtonCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        void GetCalendars()
        {
            googleClient = new GoogleClient(Settings.DataStoreFolderNameCalendar);
            calendarService = new GoogleCalendarService(googleClient);
            
            buttonGetCalendars.Enabled = false;
            dropdownListCalendars.Enabled = false;

            var calendars = calendarService.GetCalendars();

            dropdownListCalendars.Items.Clear();
            foreach (var item in calendars)
            {
                dropdownListCalendars.Items.Add(item);
            }

            dropdownListCalendars.SelectedIndex = -1;
            buttonGetCalendars.Enabled = true;
            dropdownListCalendars.Enabled = true;
        }

        void ButtonClearDataStoreClick(object sender, EventArgs e)
        {
            googleClient.ClearDataStore();
            dropdownListCalendars.Items.Clear();
            dropdownListCalendars.Text = string.Empty;
            labelSelectedAccountName.Text = string.Empty;
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