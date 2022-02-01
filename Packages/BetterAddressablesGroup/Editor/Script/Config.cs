namespace BetterAddressablesGroup.Editor
{
    using System.IO;
    using System.Text;
    using Newtonsoft.Json;
    using static LogUtils;

    public sealed class Config
    {
        #region fields
        public string Version = @"1.0.0";
        public string GroupSeprarator = @"_";
        #endregion

        #region variable
        private const string ConfigVersion = @"1.0.0";
        private const string ConfigFilePath = @"ProjectSettings/BetterAddressablesGroup.json";
        private static Encoding encoding = Encoding.GetEncoding(@"ISO-8859-1");

        private static Config instance;
        public static Config Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }

                if (!File.Exists(ConfigFilePath))
                {
                    instance = new Config();
                    Save();
                }
                else
                {
                    var json = File.ReadAllText(ConfigFilePath, encoding);
                    instance = JsonConvert.DeserializeObject<Config>(json);
                }
                return instance;
            }
        }
        #endregion

        #region API
        public static void Save()
        {
            if (instance == null) { return; }
            File.WriteAllText(ConfigFilePath, JsonConvert.SerializeObject(instance, Formatting.Indented), encoding);
            Log(@"Saved");
        }

        public static void Reload()
        {
            instance = null;
            Log($"Reload result = {Instance != null}");
        }
        #endregion
    }
}