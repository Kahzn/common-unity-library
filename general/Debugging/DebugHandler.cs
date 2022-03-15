using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CUL
{
    /// <summary>
    /// Handles Debugging centrally for all CUL classes.
    /// </summary>
    public static class DebugHandler
    {
        public static bool showLogs = true;
        public static bool showWarnings = false;
        public static bool showErrors = true;

        public static void Log(string message, object sender)
        {
            if (showLogs)
            {
                Debugging.Log(message, sender);
            }
        }

        public static void Warning(string message, object sender)
        {
            if (showWarnings)
            {
                Debugging.LogWarning(message, sender);
            }
        }

        public static void Error(string message, object sender)
        {
            if (showErrors)
            {
                Debugging.LogError(message, sender);
            }
        }


    }
}

