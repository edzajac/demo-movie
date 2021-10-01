using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Demo.Movie.Core.AppSetup;
using Demo.Movie.Core.Helpers;
using Newtonsoft.Json;

namespace Demo.Movie.Core.Services
{
    public abstract class BaseHttpClient
    {
        private JsonSerializer _serializer = new JsonSerializer();

        private readonly Uri _baseAddress;

        protected readonly string ApiKey;

        protected abstract string ServiceName { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseHttpClient()
        {
            string address = AppSettingsManager.Settings["BaseAddress"];
            string version = AppSettingsManager.Settings["Version"];
            string apiKey = AppSettingsManager.Settings["ApiKey"];

            _baseAddress = new Uri($"{address}{version}");

            ApiKey = apiKey;
        }

        /// <summary>
        /// GetAsync method 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        protected async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                var client = new HttpClient();

                client.BaseAddress = _baseAddress;

                client.Timeout = TimeSpan.FromMilliseconds(350000);

                HttpResponseMessage response = await client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    return _serializer.Deserialize<T>(json);
                }
            }
            catch(Exception ex)
            {
                AppCenterLogger.TrackError(obj: this.ServiceName,
                                           action: $"GetAsync<{typeof(T)}>({uri})",
                                           exception: ex);
            }

            return default(T);
        }
    }
}
