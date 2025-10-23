using System;
using System.Diagnostics;
using JetBrains.Annotations;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace Crux.Core.Runtime.Diagnostics
{
    /// <summary>
    /// Provides some logging methods that only execute when in "development mode"
    /// -- that is, when CRUX_DEV is defined.
    /// </summary>
    [PublicAPI]
    public static class CoreLog
    {
        private static readonly Action<object, Object> InfoLogger = Debug.Log;
        private static readonly Action<object, Object> WarningLogger = Debug.LogWarning;
        private static readonly Action<object, Object> ErrorLogger = Debug.LogError;

        /// <summary>
        /// Writes an info message to the console if CRUX_DEV is defined.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        [HideInCallstack]
        [Conditional("CRUX_DEV")]
        public static void Log(object message, Object context = null) => DoLog(InfoLogger, message, context);
        
        /// <summary>
        /// Writes a warning message to the console if CRUX_DEV is defined.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        [HideInCallstack]
        [Conditional("CRUX_DEV")]
        public static void LogWarning(object message, Object context = null) => DoLog(WarningLogger, message, context);
        
        /// <summary>
        /// Writes an error message to the console if CRUX_DEV is defined.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        [HideInCallstack]
        [Conditional("CRUX_DEV")]
        public static void LogError(object message, Object context = null) => DoLog(ErrorLogger, message, context);

        [HideInCallstack]
        private static void DoLog(Action<object, Object> logger, object message, Object context)
        {
            if (!context && message is Object newContext)
                logger(message, newContext);
            else
                logger(message, context);
        }
    }
}