using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;
using log4net.Config;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Marble")]
[assembly: AssemblyDescription("Google Calendar Sync for Outlook")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Jay Smith")]
[assembly: AssemblyProduct("Marble")]
[assembly: AssemblyCopyright("Copyright 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Configure log4net using the .config file
//(ConfigFile = "Log.config", Watch = true)
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

// This sets the default COM visibility of types in the assembly to invisible.
// If you need to expose a type to COM, use [ComVisible(true)] on that type.
[assembly: ComVisible(false)]

// Following Symantic Version http://semver.org/
[assembly: AssemblyVersion("0.0.1.13")]

public class AssemblyInfo
{
	readonly Assembly assembly;
	
	public AssemblyInfo()
	{
		assembly = Assembly.GetEntryAssembly();
	}
	
	/// <summary>
	/// Gets the title property
	/// </summary>
	public string ProductTitle 
	{
		get {
			return GetAttributeValue<AssemblyTitleAttribute>(a => a.Title, 
				Path.GetFileNameWithoutExtension(assembly.CodeBase));
		}
	}

	/// <summary>
	/// Gets the application's version
	/// </summary>
	public string Version 
	{
		get {
			string result = string.Empty;
			Version version = assembly.GetName().Version;
			
			return version.ToString();
		}
	}

	/// <summary>
	/// Gets the description about the application.
	/// </summary>
	public string Description 
	{
		get { return GetAttributeValue<AssemblyDescriptionAttribute>(a => a.Description); }
	}


	/// <summary>
	///  Gets the product's full name.
	/// </summary>
	public string Product 
	{
		get { return GetAttributeValue<AssemblyProductAttribute>(a => a.Product); }
	}

	/// <summary>
	/// Gets the copyright information for the product.
	/// </summary>
	public string Copyright 
	{
		get { return GetAttributeValue<AssemblyCopyrightAttribute>(a => a.Copyright); }
	}

	/// <summary>
	/// Gets the company information for the product.
	/// </summary>
	public string Company 
	{
		get { return GetAttributeValue<AssemblyCompanyAttribute>(a => a.Company); }
	}

	protected string GetAttributeValue<TAttr>(Func<TAttr, string> resolveFunc, string defaultResult = null) where TAttr : Attribute
	{
		object[] attributes = assembly.GetCustomAttributes(typeof(TAttr), false);
		
		if (attributes.Length > 0)
			return resolveFunc((TAttr)attributes[0]);
		
		return defaultResult;
	}
}
