namespace BetterAddressablesGroup.Editor
{
    using System;
    using UnityEngine;

    internal static class LogUtils
    {
        private const string prefix = @"<BetterAddressablesGroup> : ";

        public static void Log(object msg)
            => Debug.Log($"{prefix}{msg}");

        public static void LogWarning(object msg)
            => Debug.LogWarning($"{prefix}{msg}");

        public static void LogError(object msg)
            => Debug.LogError($"{prefix}{msg}");

        public static void LogError(Exception e)
            => LogError(e.Message);
    }
}
