using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Demo.Movie.Core.AppSetup
{
    public class AppSettingsManager
    {
        private const string _NAMESPACE = "Demo.Movie.Core";
        private const string _SETTINGS_FILE = "appsettings.json";

        private static AppSettingsManager _appSettingsInstance;
        
        private JObject _secrets;

        /// <summary>
        /// Implements the singleton instance of the AppSettingsManager
        /// and pulls the json assembly of the appsettings.json file
        /// </summary>
        private AppSettingsManager()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AppSettingsManager)).Assembly;

            var stream = assembly.GetManifestResourceStream($"{_NAMESPACE}.{_SETTINGS_FILE}");

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                _secrets = JObject.Parse(json);
            }
        }

        /// <summary>
        /// Accesses the singletoin instance of the AppSettingsManager
        /// </summary>
        public static AppSettingsManager Settings
        {
            get
            {
                if (_appSettingsInstance == null)
                {
                    _appSettingsInstance = new AppSettingsManager();
                }

                return _appSettingsInstance;
            }
        }

        /// <summary>
        /// Returns string value of name value from appsettings.json file
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string name]
        {
            get
            {
                try
                {
                    var path = name.Split(':');

                    JToken node = _secrets[path[0]];

                    for (int index = 1; index < path.Length; index++)
                    {
                        node = node[path[index]];
                    }

                    return node.ToString();
                }
                catch (Exception)
                {
                    Debug.WriteLine($"Unable to retrieve secret '{name}'");

                    return string.Empty;
                }
            }
        }
    }
}

