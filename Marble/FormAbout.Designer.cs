/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 12/16/2014
 * Time: 8:44 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Marble
{
	partial class FormAbout
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Label labelAbout;
		private System.Windows.Forms.LinkLabel linkLabelMarbleProject;
		
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
			this.buttonClose = new System.Windows.Forms.Button();
			this.labelAbout = new System.Windows.Forms.Label();
			this.linkLabelMarbleProject = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(202, 113);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 0;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
			// 
			// labelAbout
			// 
			this.labelAbout.Location = new System.Drawing.Point(12, 9);
			this.labelAbout.Name = "labelAbout";
			this.labelAbout.Size = new System.Drawing.Size(265, 66);
			this.labelAbout.TabIndex = 5;
			this.labelAbout.Text = "Marble - Google Calendar Sync for Outlook {version}\r\n\r\nBased on Outlook Google Sy" +
	"nc by Rantsi\r\nFork by JaySmith 2014\r\n\r\n";
			this.labelAbout.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// linkLabelMarbleProject
			// 
			this.linkLabelMarbleProject.Location = new System.Drawing.Point(12, 87);
			this.linkLabelMarbleProject.Name = "linkLabelMarbleProject";
			this.linkLabelMarbleProject.Size = new System.Drawing.Size(265, 23);
			this.linkLabelMarbleProject.TabIndex = 6;
			this.linkLabelMarbleProject.TabStop = true;
			this.linkLabelMarbleProject.Text = "Marble Project on GitHub";
			this.linkLabelMarbleProject.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.linkLabelMarbleProject.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelMarbleProjectLinkClicked);
			// 
			// FormAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 145);
			this.ControlBox = false;
			this.Controls.Add(this.linkLabelMarbleProject);
			this.Controls.Add(this.labelAbout);
			this.Controls.Add(this.buttonClose);
			this.Name = "FormAbout";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About Marble";
			this.Load += new System.EventHandler(this.FormAboutLoad);
			this.ResumeLayout(false);

		}
	}
}
