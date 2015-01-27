/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 12/16/2014
 * Time: 8:44 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace Marble
{
	/// <summary>
	/// Description of FormAbout.
	/// </summary>
	public partial class FormAbout : Form
	{
		public FormAbout()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
				
		void FormAboutLoad(object sender, EventArgs e)
		{
			labelAbout.Text = labelAbout.Text.Replace("{version}", Application.ProductVersion);
		}
	
		void LinkLabelMarbleProjectLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://github.com/JaySmith/Marble");
		}

		void ButtonCloseClick(object sender, EventArgs e)
		{
			this.Close();
		}
		
	}
}
