using UltraPlayTask.Business.Constants;
using UltraPlayTask.Business.Helpers;
using UltraPlayTask.Business.Services.Contracts;
using UltraPlayTask.Data.Models;

namespace UltraPlayTask.Business.Services
{
    public class DataFetchService : IDataFetchService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<DataFetchService> _logger;

        public DataFetchService(ILogger<DataFetchService> logger,
            IHttpClientFactory httpClientFactory)
        {
            _clientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<T> FetchData<T>(string url) where T : BaseDataModel
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(string.Format(LoggingConstants.UnsuccsessfullDataFetchMsg, nameof(FetchData)));
            }

            var content = await response.Content.ReadAsStringAsync();
            return XmlHelper.ParseData<T>(content);
        }
    }
}