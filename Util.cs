using UnityEngine;
using ColossalFramework.Plugins;
using System;
using System.Threading;

namespace WaterPipeUpgraderv2
{
    public class Util
    {
        public const string modPrefix = "[Water Pipe Upgrader " + Mod._version + "] ";

        public static void DebugPrint(params string[] args)
        {
            DateTime now = System.DateTime.Now;
            long millis = now.Ticks / 10000;
            string s = String.Format("[WPU v2] {0, -42} {1, 22} {2, 2}  {3}.{4}", String.Join(" ", args), now, Thread.CurrentThread.ManagedThreadId, millis / 1000, millis % 1000);
            Debug.Log(s);
        }

        public static void Message(string message)
        {
            Log(message);
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, modPrefix + message);
        }

        public static void Warning(string message)
        {
            Debug.LogWarning(modPrefix + message);
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Warning, modPrefix + message);
        }

        public static void Log(string message)
        {
            if (message == m_lastLog)
            {
                m_duplicates++;
            }
            else if (m_duplicates > 0)
            {
                Debug.Log(modPrefix + "(x" + (m_duplicates + 1) + ")");
                Debug.Log(modPrefix + message);
                m_duplicates = 0;
            }
            else
            {
                Debug.Log(modPrefix + message);
            }
            m_lastLog = message;
        }

        public static void LogException(Exception e)
        {
            Log("Intercepted exception (not game breaking):");
            Debug.LogException(e);
        }

        private static string m_lastLog;
        private static int m_duplicates = 0;
    }
}
