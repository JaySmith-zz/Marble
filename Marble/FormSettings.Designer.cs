/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 12/16/2014
 * Time: 8:41 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Marble
{
	partial class FormSettings
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.GroupBox groupBoxGoogleCalendar;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.Button buttonGetCalendars;
		private System.Windows.Forms.Label labelCalendar;
		private System.Windows.Forms.TextBox textBoxMinuteOffset;
		private System.Windows.Forms.TextBox textBoxSyncDaysInFuture;
		private System.Windows.Forms.TextBox textBoxSyncDaysInPast;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelSelectedAccountName;
		private System.Windows.Forms.Label labelSelectedAccount;
		private System.Windows.Forms.Button buttonClearDataStore;
		private System.Windows.Forms.CheckBox checkBoxStartWithWindows;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonCancel = new System.Windows.Forms.Button();
			this.groupBoxGoogleCalendar = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxHourlyOffset = new System.Windows.Forms.TextBox();
			this.radioButtonSyncEveryHour = new System.Windows.Forms.RadioButton();
			this.radioButtonSyncEveryNMinutes = new System.Windows.Forms.RadioButton();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxSyncDaysInFuture = new System.Windows.Forms.TextBox();
			this.buttonClearDataStore = new System.Windows.Forms.Button();
			this.textBoxSyncDaysInPast = new System.Windows.Forms.TextBox();
			this.labelSelectedAccountName = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelSelectedAccount = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxMinuteOffset = new System.Windows.Forms.TextBox();
			this.buttonGetCalendars = new System.Windows.Forms.Button();
			this.dropdownListCalendars = new System.Windows.Forms.ComboBox();
			this.labelCalendar = new System.Windows.Forms.Label();
			this.buttonOk = new System.Windows.Forms.Button();
			this.groupBoxOptions = new System.Windows.Forms.GroupBox();
			this.checkBoxShowNotifications = new System.Windows.Forms.CheckBox();
			this.comboBoxOutlookServiceProvider = new System.Windows.Forms.ComboBox();
			this.textBoxExchangeEmail = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.checkBoxStartWithWindows = new System.Windows.Forms.CheckBox();
			this.groupBoxGoogleCalendar.SuspendLayout();
			this.groupBoxOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(414, 343);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 0;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
			// 
			// groupBoxGoogleCalendar
			// 
			this.groupBoxGoogleCalendar.Controls.Add(this.label6);
			this.groupBoxGoogleCalendar.Controls.Add(this.label5);
			this.groupBoxGoogleCalendar.Controls.Add(this.textBoxHourlyOffset);
			this.groupBoxGoogleCalendar.Controls.Add(this.radioButtonSyncEveryHour);
			this.groupBoxGoogleCalendar.Controls.Add(this.radioButtonSyncEveryNMinutes);
			this.groupBoxGoogleCalendar.Controls.Add(this.label4);
			this.groupBoxGoogleCalendar.Controls.Add(this.textBoxSyncDaysInFuture);
			this.groupBoxGoogleCalendar.Controls.Add(this.buttonClearDataStore);
			this.groupBoxGoogleCalendar.Controls.Add(this.textBoxSyncDaysInPast);
			this.groupBoxGoogleCalendar.Controls.Add(this.labelSelectedAccountName);
			this.groupBoxGoogleCalendar.Controls.Add(this.label2);
			this.groupBoxGoogleCalendar.Controls.Add(this.labelSelectedAccount);
			this.groupBoxGoogleCalendar.Controls.Add(this.label1);
			this.groupBoxGoogleCalendar.Controls.Add(this.textBoxMinuteOffset);
			this.groupBoxGoogleCalendar.Controls.Add(this.buttonGetCalendars);
			this.groupBoxGoogleCalendar.Controls.Add(this.dropdownListCalendars);
			this.groupBoxGoogleCalendar.Controls.Add(this.labelCalendar);
			this.groupBoxGoogleCalendar.Location = new System.Drawing.Point(13, 13);
			this.groupBoxGoogleCalendar.Name = "groupBoxGoogleCalendar";
			this.groupBoxGoogleCalendar.Size = new System.Drawing.Size(481, 213);
			this.groupBoxGoogleCalendar.TabIndex = 1;
			this.groupBoxGoogleCalendar.TabStop = false;
			this.groupBoxGoogleCalendar.Text = "Google Calendar";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(175, 116);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(72, 23);
			this.label6.TabIndex = 16;
			this.label6.Text = "Minutes Offset";
			this.label6.Visible = false;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(175, 93);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 23);
			this.label5.TabIndex = 14;
			this.label5.Text = "Minutes";
			this.label5.Visible = false;
			// 
			// textBoxHourlyOffset
			// 
			this.textBoxHourlyOffset.Location = new System.Drawing.Point(253, 113);
			this.textBoxHourlyOffset.Name = "textBoxHourlyOffset";
			this.textBoxHourlyOffset.Size = new System.Drawing.Size(35, 20);
			this.textBoxHourlyOffset.TabIndex = 15;
			// 
			// radioButtonSyncEveryHour
			// 
			this.radioButtonSyncEveryHour.Location = new System.Drawing.Point(9, 110);
			this.radioButtonSyncEveryHour.Name = "radioButtonSyncEveryHour";
			this.radioButtonSyncEveryHour.Size = new System.Drawing.Size(142, 24);
			this.radioButtonSyncEveryHour.TabIndex = 13;
			this.radioButtonSyncEveryHour.TabStop = true;
			this.radioButtonSyncEveryHour.Text = "Sync every hour";
			this.radioButtonSyncEveryHour.UseVisualStyleBackColor = true;
			// 
			// radioButtonSyncEveryNMinutes
			// 
			this.radioButtonSyncEveryNMinutes.Location = new System.Drawing.Point(9, 87);
			this.radioButtonSyncEveryNMinutes.Name = "radioButtonSyncEveryNMinutes";
			this.radioButtonSyncEveryNMinutes.Size = new System.Drawing.Size(142, 24);
			this.radioButtonSyncEveryNMinutes.TabIndex = 12;
			this.radioButtonSyncEveryNMinutes.TabStop = true;
			this.radioButtonSyncEveryNMinutes.Text = "Sync every minutes";
			this.radioButtonSyncEveryNMinutes.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(3, 152);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(103, 23);
			this.label4.TabIndex = 11;
			this.label4.Text = "Date Range";
			this.label4.Visible = false;
			// 
			// textBoxSyncDaysInFuture
			// 
			this.textBoxSyncDaysInFuture.Location = new System.Drawing.Point(279, 172);
			this.textBoxSyncDaysInFuture.Name = "textBoxSyncDaysInFuture";
			this.textBoxSyncDaysInFuture.Size = new System.Drawing.Size(39, 20);
			this.textBoxSyncDaysInFuture.TabIndex = 3;
			// 
			// buttonClearDataStore
			// 
			this.buttonClearDataStore.Location = new System.Drawing.Point(374, 29);
			this.buttonClearDataStore.Name = "buttonClearDataStore";
			this.buttonClearDataStore.Size = new System.Drawing.Size(101, 23);
			this.buttonClearDataStore.TabIndex = 10;
			this.buttonClearDataStore.Text = "Switch Account";
			this.buttonClearDataStore.UseVisualStyleBackColor = true;
			this.buttonClearDataStore.Click += new System.EventHandler(this.ButtonClearDataStoreClick);
			// 
			// textBoxSyncDaysInPast
			// 
			this.textBoxSyncDaysInPast.Location = new System.Drawing.Point(109, 172);
			this.textBoxSyncDaysInPast.Name = "textBoxSyncDaysInPast";
			this.textBoxSyncDaysInPast.Size = new System.Drawing.Size(39, 20);
			this.textBoxSyncDaysInPast.TabIndex = 2;
			// 
			// labelSelectedAccountName
			// 
			this.labelSelectedAccountName.Location = new System.Drawing.Point(101, 34);
			this.labelSelectedAccountName.Name = "labelSelectedAccountName";
			this.labelSelectedAccountName.Size = new System.Drawing.Size(267, 23);
			this.labelSelectedAccountName.TabIndex = 9;
			this.labelSelectedAccountName.Visible = false;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(175, 175);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Days in the future";
			// 
			// labelSelectedAccount
			// 
			this.labelSelectedAccount.Location = new System.Drawing.Point(6, 34);
			this.labelSelectedAccount.Name = "labelSelectedAccount";
			this.labelSelectedAccount.Size = new System.Drawing.Size(103, 23);
			this.labelSelectedAccount.TabIndex = 8;
			this.labelSelectedAccount.Text = "Selected Account";
			this.labelSelectedAccount.Visible = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3, 175);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Days in the past";
			// 
			// textBoxMinuteOffset
			// 
			this.textBoxMinuteOffset.Location = new System.Drawing.Point(253, 91);
			this.textBoxMinuteOffset.Name = "textBoxMinuteOffset";
			this.textBoxMinuteOffset.Size = new System.Drawing.Size(35, 20);
			this.textBoxMinuteOffset.TabIndex = 4;
			// 
			// buttonGetCalendars
			// 
			this.buttonGetCalendars.Location = new System.Drawing.Point(374, 58);
			this.buttonGetCalendars.Name = "buttonGetCalendars";
			this.buttonGetCalendars.Size = new System.Drawing.Size(101, 23);
			this.buttonGetCalendars.TabIndex = 2;
			this.buttonGetCalendars.Text = "Get Calendars";
			this.buttonGetCalendars.UseVisualStyleBackColor = true;
			this.buttonGetCalendars.Click += new System.EventHandler(this.ButtonGetCalendarsClick);
			// 
			// dropdownListCalendars
			// 
			this.dropdownListCalendars.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.dropdownListCalendars.FormattingEnabled = true;
			this.dropdownListCalendars.Location = new System.Drawing.Point(71, 60);
			this.dropdownListCalendars.Name = "dropdownListCalendars";
			this.dropdownListCalendars.Size = new System.Drawing.Size(297, 21);
			this.dropdownListCalendars.TabIndex = 1;
			// 
			// labelCalendar
			// 
			this.labelCalendar.Location = new System.Drawing.Point(8, 63);
			this.labelCalendar.Name = "labelCalendar";
			this.labelCalendar.Size = new System.Drawing.Size(57, 18);
			this.labelCalendar.TabIndex = 0;
			this.labelCalendar.Text = "Calendar";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(333, 343);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 3;
			this.buttonOk.Text = "OK";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.ButtonOkClick);
			// 
			// groupBoxOptions
			// 
			this.groupBoxOptions.Controls.Add(this.checkBoxShowNotifications);
			this.groupBoxOptions.Controls.Add(this.comboBoxOutlookServiceProvider);
			this.groupBoxOptions.Controls.Add(this.textBoxExchangeEmail);
			this.groupBoxOptions.Controls.Add(this.label3);
			this.groupBoxOptions.Controls.Add(this.checkBoxStartWithWindows);
			this.groupBoxOptions.Location = new System.Drawing.Point(12, 237);
			this.groupBoxOptions.Name = "groupBoxOptions";
			this.groupBoxOptions.Size = new System.Drawing.Size(482, 83);
			this.groupBoxOptions.TabIndex = 3;
			this.groupBoxOptions.TabStop = false;
			this.groupBoxOptions.Text = "Options";
			// 
			// checkBoxShowNotifications
			// 
			this.checkBoxShowNotifications.Location = new System.Drawing.Point(140, 19);
			this.checkBoxShowNotifications.Name = "checkBoxShowNotifications";
			this.checkBoxShowNotifications.Size = new System.Drawing.Size(229, 24);
			this.checkBoxShowNotifications.TabIndex = 7;
			this.checkBoxShowNotifications.Text = "Show Notifications";
			this.checkBoxShowNotifications.UseVisualStyleBackColor = true;
			// 
			// comboBoxOutlookServiceProvider
			// 
			this.comboBoxOutlookServiceProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxOutlookServiceProvider.FormattingEnabled = true;
			this.comboBoxOutlookServiceProvider.Location = new System.Drawing.Point(10, 49);
			this.comboBoxOutlookServiceProvider.Name = "comboBoxOutlookServiceProvider";
			this.comboBoxOutlookServiceProvider.Size = new System.Drawing.Size(121, 21);
			this.comboBoxOutlookServiceProvider.TabIndex = 4;
			// 
			// textBoxExchangeEmail
			// 
			this.textBoxExchangeEmail.Location = new System.Drawing.Point(244, 49);
			this.textBoxExchangeEmail.Name = "textBoxExchangeEmail";
			this.textBoxExchangeEmail.Size = new System.Drawing.Size(224, 20);
			this.textBoxExchangeEmail.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(137, 52);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "Exchange Email ";
			// 
			// checkBoxStartWithWindows
			// 
			this.checkBoxStartWithWindows.Location = new System.Drawing.Point(7, 19);
			this.checkBoxStartWithWindows.Name = "checkBoxStartWithWindows";
			this.checkBoxStartWithWindows.Size = new System.Drawing.Size(229, 24);
			this.checkBoxStartWithWindows.TabIndex = 4;
			this.checkBoxStartWithWindows.Text = "Start with Windows";
			this.checkBoxStartWithWindows.UseVisualStyleBackColor = true;
			// 
			// FormSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(501, 378);
			this.ControlBox = false;
			this.Controls.Add(this.groupBoxOptions);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.groupBoxGoogleCalendar);
			this.Controls.Add(this.buttonCancel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSettings";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.FormSettingsLoad);
			this.groupBoxGoogleCalendar.ResumeLayout(false);
			this.groupBoxGoogleCalendar.PerformLayout();
			this.groupBoxOptions.ResumeLayout(false);
			this.groupBoxOptions.PerformLayout();
			this.ResumeLayout(false);

		}

        private System.Windows.Forms.ComboBox dropdownListCalendars;
        private System.Windows.Forms.ComboBox comboBoxOutlookServiceProvider;
        private System.Windows.Forms.TextBox textBoxExchangeEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxShowNotifications;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxHourlyOffset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioButtonSyncEveryHour;
        private System.Windows.Forms.RadioButton radioButtonSyncEveryNMinutes;
        private System.Windows.Forms.Label label4;
	}
}
