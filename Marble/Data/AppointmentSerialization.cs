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
using System.Xml.Serialization;

namespace Marble.Data
{
	/// <summary>
	/// Description of AppointmentSerialization.
	/// </summary>
	public static class AppointmentSerialization
	{
	
		public static void Save(IEnumerable<Appointment> appoinments)
		{
			var mySerializer = new XmlSerializer(typeof(List<Appointment>));
			
            // To write to a file, create a StreamWriter object.
            var myWriter = new StreamWriter(Settings.AppointmentCacheFileName);
            
            mySerializer.Serialize(myWriter, appoinments);
            myWriter.Close();
		}
		
		public static List<Appointment> Read()
		{
			if (!File.Exists(Settings.AppointmentCacheFileName)) return new List<Appointment>();
			
			var mySerializer = new XmlSerializer(typeof(List<Appointment>));
            
            // To read the file, create a FileStream.
            var stream = new FileStream(Settings.AppointmentCacheFileName, FileMode.Open);
            
            // Call the Deserialize method and cast to the object type.
            var items = (List<Appointment>)mySerializer.Deserialize(stream);
            
            stream.Close();

            return items;
		}
	}
}
