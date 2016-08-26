using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Logging;

namespace Devpro.EmbyData.ClientApiLib
{
    public class RestApiClientBase
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RestApiClientBase));

        public async Task<T> CallAsync<T>(string url)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(url) })
            {
                var result = await SendGetRequest(client, url);
                var output = await result.Content.ReadAsAsync<T>();
                return output;
            }
        }

        public async Task<string> CallAsyncString<T>(string url)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(url) })
            {
                var result = await SendGetRequest(client, url);
                var output = await result.Content.ReadAsStringAsync();
                return output;
            }
        }

        /// <summary>
        /// Send a REST Get request.
        /// </summary>
        /// <param name="client">Http client</param>
        /// <param name="url">string url</param>
        /// <returns>Http response message, throws an exception if the result is not successful</returns>
        private async Task<HttpResponseMessage> SendGetRequest(HttpClient client, string url)
        {
            Logger.Debug(string.Format("Calling url \"{0}\"", url));
            var result = await client.GetAsync(url);
            if (!result.IsSuccessStatusCode)
                throw new Exception(string.Format("Not OK response received on calling \"{1}\". StatusCode=\"{0}\"", result.StatusCode, url));
            return result;
        }
    }
}
