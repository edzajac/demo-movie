using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Demo.Movie.Core.AppSetup;
using Newtonsoft.Json;

namespace Demo.Movie.Core.Services
{
    public class BaseHttpClient
    {
        private JsonSerializer _serializer = new JsonSerializer();

        private readonly Uri _baseAddress;

        protected readonly string ApiKey;

        public BaseHttpClient()
        {
            string address = AppSettingsManager.Settings["BaseAddress"];
            string version = AppSettingsManager.Settings["Version"];
            string apiKey = AppSettingsManager.Settings["ApiKey"];

            _baseAddress = new Uri($"{address}{version}");

            ApiKey = apiKey;
        }

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
                //TODO
                var tst = ex;
            }

            return default(T);
        }
    }
}
