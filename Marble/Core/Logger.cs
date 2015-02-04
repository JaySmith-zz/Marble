/*
 * Created by SharpDevelop.
 * User: SMITHJAY
 * Date: 1/28/2015
 * Time: 11:39 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using log4net;

namespace Marble
{
	/// <summary>
	/// Description of Logger.
	/// </summary>
	public class Logger
	{
        public Logger(ILog log)
        {
            _log = log;
        }

        readonly ILog _log;

        public void Debug(string message)
        {
            _log.Debug(message);
        }

        public void Information(string message)
        {
            _log.Info(message);
        }

        public void Information(string message, Exception exception)
        {
            _log.Info(message, exception);
        }
        
        public void Warn(string message)
        {
            _log.Warn(message);
        }

        public void Warn(string message, Exception exception)
        {
            _log.Warn(message, exception);
        }

        public void Error(string message)
        {
            _log.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            _log.Error(message, exception);
        }

        public void Fatal(string message)
        {
            _log.Fatal(message);
        }

        public void Fatal(string message, Exception exception)
        {
            _log.Fatal(message, exception);
        }
    }
}
