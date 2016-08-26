using System.Threading.Tasks;
using Common.Logging;
using Devpro.EmbyData.Dto;

namespace Devpro.EmbyData.ClientApiLib
{
    public class EmbyApiClient : RestApiClientBase
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(EmbyApiClient));

        public string Hostname { get; set; }
        public string Complement { get; set; }
        public string ApiKey { get; set; }

        public EmbyApiClient()
        {
            Hostname   = "http://localhost:8096";
            Complement = "mediabrowser";
        }

        public async Task<ItemsCountDto> GetItemsCount()
        {
            var url = string.Format(
                "{0}/{1}/Items/Counts?api_key={2}&format=json",
                Hostname,
                Complement,
                ApiKey
            );
            return await CallAsync<ItemsCountDto>(url);
        }
    }
}
