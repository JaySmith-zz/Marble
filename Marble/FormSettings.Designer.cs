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
		private System.Windows.Forms.CheckBox checkBoxSyncEveryHour;
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
			this.textBoxSyncDaysInFuture = new System.Windows.Forms.TextBox();
			this.buttonClearDataStore = new System.Windows.Forms.Button();
			this.textBoxSyncDaysInPast = new System.Windows.Forms.TextBox();
			this.labelSelectedAccountName = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelSelectedAccount = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBoxSyncEveryHour = new System.Windows.Forms.CheckBox();
			this.textBoxMinuteOffset = new System.Windows.Forms.TextBox();
			this.buttonGetCalendars = new System.Windows.Forms.Button();
			this.dropdownListCalendars = new System.Windows.Forms.ComboBox();
			this.labelCalendar = new System.Windows.Forms.Label();
			this.buttonOk = new System.Windows.Forms.Button();
			this.groupBoxOptions = new System.Windows.Forms.GroupBox();
			this.checkBoxStartWithWindows = new System.Windows.Forms.CheckBox();
			this.groupBoxGoogleCalendar.SuspendLayout();
			this.groupBoxOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(419, 242);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 0;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
			// 
			// groupBoxGoogleCalendar
			// 
			this.groupBoxGoogleCalendar.Controls.Add(this.textBoxSyncDaysInFuture);
			this.groupBoxGoogleCalendar.Controls.Add(this.buttonClearDataStore);
			this.groupBoxGoogleCalendar.Controls.Add(this.textBoxSyncDaysInPast);
			this.groupBoxGoogleCalendar.Controls.Add(this.labelSelectedAccountName);
			this.groupBoxGoogleCalendar.Controls.Add(this.label2);
			this.groupBoxGoogleCalendar.Controls.Add(this.labelSelectedAccount);
			this.groupBoxGoogleCalendar.Controls.Add(this.label1);
			this.groupBoxGoogleCalendar.Controls.Add(this.checkBoxSyncEveryHour);
			this.groupBoxGoogleCalendar.Controls.Add(this.textBoxMinuteOffset);
			this.groupBoxGoogleCalendar.Controls.Add(this.buttonGetCalendars);
			this.groupBoxGoogleCalendar.Controls.Add(this.dropdownListCalendars);
			this.groupBoxGoogleCalendar.Controls.Add(this.labelCalendar);
			this.groupBoxGoogleCalendar.Location = new System.Drawing.Point(13, 13);
			this.groupBoxGoogleCalendar.Name = "groupBoxGoogleCalendar";
			this.groupBoxGoogleCalendar.Size = new System.Drawing.Size(481, 164);
			this.groupBoxGoogleCalendar.TabIndex = 1;
			this.groupBoxGoogleCalendar.TabStop = false;
			this.groupBoxGoogleCalendar.Text = "Google Calendar";
			// 
			// textBoxSyncDaysInFuture
			// 
			this.textBoxSyncDaysInFuture.Location = new System.Drawing.Point(112, 134);
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
			this.textBoxSyncDaysInPast.Location = new System.Drawing.Point(112, 108);
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
			this.label2.Location = new System.Drawing.Point(8, 137);
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
			this.label1.Location = new System.Drawing.Point(6, 111);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Days in the past";
			// 
			// checkBoxSyncEveryHour
			// 
			this.checkBoxSyncEveryHour.Location = new System.Drawing.Point(8, 84);
			this.checkBoxSyncEveryHour.Name = "checkBoxSyncEveryHour";
			this.checkBoxSyncEveryHour.Size = new System.Drawing.Size(229, 24);
			this.checkBoxSyncEveryHour.TabIndex = 3;
			this.checkBoxSyncEveryHour.Text = "Sync every hour at these Minutes Offset(s)";
			this.checkBoxSyncEveryHour.UseVisualStyleBackColor = true;
			// 
			// textBoxMinuteOffset
			// 
			this.textBoxMinuteOffset.Location = new System.Drawing.Point(243, 86);
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
			this.buttonOk.Location = new System.Drawing.Point(338, 242);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 3;
			this.buttonOk.Text = "OK";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.ButtonOkClick);
			// 
			// groupBoxOptions
			// 
			this.groupBoxOptions.Controls.Add(this.checkBoxStartWithWindows);
			this.groupBoxOptions.Location = new System.Drawing.Point(12, 183);
			this.groupBoxOptions.Name = "groupBoxOptions";
			this.groupBoxOptions.Size = new System.Drawing.Size(482, 53);
			this.groupBoxOptions.TabIndex = 3;
			this.groupBoxOptions.TabStop = false;
			this.groupBoxOptions.Text = "Options";
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
			this.ClientSize = new System.Drawing.Size(501, 274);
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
			this.ResumeLayout(false);

		}

        private System.Windows.Forms.ComboBox dropdownListCalendars;
	}
}
