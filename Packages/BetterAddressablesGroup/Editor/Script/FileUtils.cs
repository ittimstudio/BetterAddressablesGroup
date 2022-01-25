namespace BetterAddressablesGroup.Editor
{
    using System;
    using System.IO;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Threading.Tasks;
    using static LogUtils;

    internal static class FileUtils
    {
        private const string prefix = @"ProjectSettings/";

        public static void WriteAllText(string filePath, string content)
        {
            var path = $"{prefix}{filePath}";
            File.WriteAllText(path, content);
        }

        public static bool GetIsFileExist(string filePath)
        {
            var path = $"{prefix}{filePath}";

            return File.Exists(path);
        }

        public static string ReadAllText(string filePath)
        {
            var path = $"{prefix}{filePath}";

            if (!File.Exists(path))
            {
                LogError($"File not found >> {path}");
                return string.Empty;
            }

            return File.ReadAllText(path);
        }
    }
}