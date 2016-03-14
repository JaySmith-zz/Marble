/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 3/7/2016
 * Time: 11:25 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.Reflection;

namespace Marble.Data
{
	/// <summary>
	/// Description of AppointmentSerialization.
	/// </summary>
	public static class AppointmentSerialization
	{
		static IsolatedStorageFile isoStoreFile;
		
		static AppointmentSerialization()
		{
			isoStoreFile = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
		}	
		
		public static void Save(IEnumerable<Appointment> appoinments)
		{
			var mySerializer = new XmlSerializer(typeof(List<Appointment>));
       
            var stream = new IsolatedStorageFileStream(Settings.AppointmentCacheFileName, FileMode.Create, FileAccess.ReadWrite, isoStoreFile);
                    
            mySerializer.Serialize(stream, appoinments);
            stream.Close();
		}
		
		public static List<Appointment> Read()
		{
			if (!isoStoreFile.FileExists(Settings.AppointmentCacheFileName)) return new List<Appointment>();

            var stream = new IsolatedStorageFileStream(Settings.AppointmentCacheFileName, FileMode.Open, isoStoreFile);
            string path = stream.GetType().GetField("m_FullPath", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(stream).ToString();
                    
            // Call the Deserialize method and cast to the object type.
            var mySerializer = new XmlSerializer(typeof(List<Appointment>));
            var items = mySerializer.Deserialize(stream) as List<Appointment>;
            
            stream.Close();

            return items;
		}
		
		public static void Clear()
		{
			if (isoStoreFile.FileExists(Settings.AppointmentCacheFileName))
			    isoStoreFile.DeleteFile(Settings.AppointmentCacheFileName);
		}
		
		public static string AppointmentDataStorePath()
		{
			var stream = new IsolatedStorageFileStream(Settings.AppointmentCacheFileName, FileMode.Open, isoStoreFile);
            string path = stream.GetType().GetField("m_FullPath", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(stream).ToString();
            stream.Close();
            
            return Path.GetDirectoryName(path);
		}
	}
}
