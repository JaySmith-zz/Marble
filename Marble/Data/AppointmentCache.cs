/*
 * Created by SharpDevelop.
 * User: smithjay
 * Date: 3/7/2016
 * Time: 10:29 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Marble.Data
{
	/// <summary>
	/// Description of AppointmentCache.
	/// </summary>
	public class AppointmentCache
	{
		public List<Appointment> Items { get; private set;} 
				
		public AppointmentCache()
		{
			Items = AppointmentSerialization.Read();
		}
		
		/// <summary>
		/// Add item to cache
		/// </summary>
		/// <param name="item"></param>
		public void Add(Appointment item)
		{
			if (!Items.Contains(item))
			{
				Items.Add(item);
			}
		}
		
		
		/// <summary>
		/// Remove item from cache
		/// </summary>
		/// <param name="item"></param>
		public void Remove(Appointment item)
		{
			if (Items.Contains(item))
		    {
				Items.Remove(item);
		    }
		}
		
		public void Save()
		{
			AppointmentSerialization.Save(Items);
		}
		
		public void Clear()
		{
			AppointmentSerialization.Clear();
		}
	}
}
