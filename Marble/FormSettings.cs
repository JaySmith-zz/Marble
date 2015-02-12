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
            comboBoxCalendars.SelectedIndex = comboBoxCalendars.FindStringExact(Settings.CalendarId);

            checkBoxSyncEveryHour.Checked = Settings.SyncEveryHour;
            textBoxMinuteOffset.Text = Settings.SyncMinutesOffset.ToString();
            checkBoxStartWithWindows.Checked = Settings.StartWithWindows;
            textBoxSyncDaysInPast.Text = Settings.CalendarDaysInThePast.ToString();
            textBoxSyncDaysInFuture.Text = Settings.CalendarDaysInTheFuture.ToString();

        }

        void ButtonGetCalendarsClick(object sender, EventArgs e)
        {
            GetCalendars();
        }

        void ButtonOkClick(object sender, EventArgs e)
        {
            var selectedItem = (GoogleCalendarInfo)comboBoxCalendars.SelectedItem;

            Settings.CalendarAccount = selectedItem.Id;
            Settings.CalendarId = selectedItem.Name;
            Settings.SyncEveryHour = checkBoxSyncEveryHour.Checked;
            Settings.SyncMinutesOffset = int.Parse(textBoxMinuteOffset.Text);
            Settings.StartWithWindows = checkBoxStartWithWindows.Checked;
            Settings.CalendarDaysInThePast = int.Parse(textBoxSyncDaysInPast.Text);
            Settings.CalendarDaysInTheFuture = int.Parse(textBoxSyncDaysInFuture.Text);

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
            googleClient = new GoogleClient(Settings.DataStoreFolderNameCalendar);
            calendarService = new GoogleCalendarService(googleClient);
            
            buttonGetCalendars.Enabled = false;
            comboBoxCalendars.Enabled = false;

            var calendars = calendarService.GetCalendars();

            comboBoxCalendars.Items.Clear();
            foreach (var item in calendars)
            {
                comboBoxCalendars.Items.Add(item);
            }

            comboBoxCalendars.SelectedIndex = -1;
            buttonGetCalendars.Enabled = true;
            comboBoxCalendars.Enabled = true;
        }

        void ButtonClearDataStoreClick(object sender, EventArgs e)
        {
            googleClient.ClearDataStore();
            comboBoxCalendars.Items.Clear();
            comboBoxCalendars.Text = string.Empty;
            labelSelectedAccountName.Text = string.Empty;
            Settings.CalendarId = string.Empty;
            Settings.CalendarAccount = string.Empty;
            Settings.Save();

            googleClient.GetAuthorization();

            InitializeForm();
        }

        void ComboBoxCalendarSelectedIndexChange(object sender, EventArgs e)
        {
            var selectedItem = ((GoogleCalendarInfo)comboBoxCalendars.SelectedItem);
            Settings.CalendarAccount = selectedItem.Id;
            Settings.CalendarId = selectedItem.Name;
            labelSelectedAccountName.Text = Settings.CalendarAccount;
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