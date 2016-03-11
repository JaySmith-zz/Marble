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
			
            // To write to a file, create a StreamWriter object.
            //var myWriter = new StreamWriter(Settings.AppointmentCacheFileName);
            //var myWriter = new IsolatedStorageFileStream(Settings.AppointmentCacheFileName, FileMode.OpenOrCreate);
            Stream myWriter = new IsolatedStorageFileStream(Settings.AppointmentCacheFileName, FileMode.OpenOrCreate, isoStoreFile);
            
            mySerializer.Serialize(myWriter, appoinments);
            myWriter.Close();
		}
		
		public static List<Appointment> Read()
		{
			if (!isoStoreFile.FileExists(Settings.AppointmentCacheFileName)) return new List<Appointment>();
			
			var mySerializer = new XmlSerializer(typeof(List<Appointment>));
            
            // To read the file, create a FileStream.
            //var stream = new FileStream(Settings.AppointmentCacheFileName, FileMode.Open);
            Stream stream = new IsolatedStorageFileStream(Settings.AppointmentCacheFileName, FileMode.Open, isoStoreFile);
            
            // Call the Deserialize method and cast to the object type.
            var items = (List<Appointment>)mySerializer.Deserialize(stream);
            
            stream.Close();

            return items;
		}
		
		public static void Clear()
		{
			if (isoStoreFile.FileExists(Settings.AppointmentCacheFileName))
			    isoStoreFile.DeleteFile(Settings.AppointmentCacheFileName);
			
			//if (File.Exists(Settings.AppointmentCacheFileName))
			//	File.Delete(Settings.AppointmentCacheFileName);
		}
	}
}
