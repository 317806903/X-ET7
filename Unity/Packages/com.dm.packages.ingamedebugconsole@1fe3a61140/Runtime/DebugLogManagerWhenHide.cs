using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace IngameDebugConsole
{
	public class DebugLogManagerWhenHide : MonoBehaviour
	{
		public static DebugLogManagerWhenHide Instance { get; private set; }
		private int maxLogLength = 10000;
		private DynamicCircularBuffer<QueuedDebugLogEntry> queuedLogEntries;
		private object logEntriesLock;
		private void Awake()
		{
			// Only one instance of debug console is allowed
			if( !Instance )
			{
				Instance = this;
			}
			else if( Instance != this )
			{
				Destroy( gameObject );
				return;
			}
			queuedLogEntries = new DynamicCircularBuffer<QueuedDebugLogEntry>( 16 );
			logEntriesLock = new object();
		}

		private void OnEnable()
		{
			// Intercept debug entries
			Application.logMessageReceivedThreaded -= ReceivedLog;
			Application.logMessageReceivedThreaded += ReceivedLog;
		}

		private void OnDisable()
		{
			// Stop receiving debug entries
			Application.logMessageReceivedThreaded -= ReceivedLog;
			if (DebugLogManager.Instance != null)
			{
				SendQueuedLog2DebugLog();
			}
		}

		public void SendQueuedLog2DebugLog()
		{
			lock( logEntriesLock )
			{
				if (this.queuedLogEntries.Count > 0)
				{
					if (IngameDebugConsole.DebugLogManager.Instance != null)
					{
						IngameDebugConsole.DebugLogManager.Instance.ReceivedLog(this.queuedLogEntries);
					}
				}
			}
		}

		// A debug entry is received
		public void ReceivedLog( string logString, string stackTrace, LogType logType )
		{
			if (logType == LogType.Log)
			{
				return;
			}
			else if (logType == LogType.Warning)
			{
				return;
			}
			else if (logType == LogType.Error)
			{

			}
			else if (logType == LogType.Exception)
			{

			}
			else if (logType == LogType.Assert)
			{

			}

			// Truncate the log if it is longer than maxLogLength
			int logLength = logString.Length;
			if( stackTrace == null )
			{
				if( logLength > maxLogLength )
					logString = logString.Substring( 0, maxLogLength - 11 ) + "<truncated>";
			}
			else
			{
				logLength += stackTrace.Length;
				if( logLength > maxLogLength )
				{
					// Decide which log component(s) to truncate
					int halfMaxLogLength = maxLogLength / 2;
					if( logString.Length >= halfMaxLogLength )
					{
						if( stackTrace.Length >= halfMaxLogLength )
						{
							// Truncate both logString and stackTrace
							logString = logString.Substring( 0, halfMaxLogLength - 11 ) + "<truncated>";

							// If stackTrace doesn't end with a blank line, its last line won't be visible in the console for some reason
							stackTrace = stackTrace.Substring( 0, halfMaxLogLength - 12 ) + "<truncated>\n";
						}
						else
						{
							// Truncate logString
							logString = logString.Substring( 0, maxLogLength - stackTrace.Length - 11 ) + "<truncated>";
						}
					}
					else
					{
						// Truncate stackTrace
						stackTrace = stackTrace.Substring( 0, maxLogLength - logString.Length - 12 ) + "<truncated>\n";
					}
				}
			}

			QueuedDebugLogEntry queuedLogEntry = new QueuedDebugLogEntry( logString, stackTrace, logType );

			lock( logEntriesLock )
			{
				queuedLogEntries.Add( queuedLogEntry );
			}
		}
	}
}