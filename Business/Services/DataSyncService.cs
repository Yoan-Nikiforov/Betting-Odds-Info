using Microsoft.EntityFrameworkCore;
using UltraPlayTask.Business.Constants;
using UltraPlayTask.Business.Helpers;
using UltraPlayTask.Business.Services.Contracts;
using UltraPlayTask.Data.DbHandlers;
using UltraPlayTask.Data.Models;

namespace UltraPlayTask.Business.Services
{
    public class DataSyncService : IHostedService
    {
        private readonly IDataFetchService _dataFetchService;
        private readonly ILogger<DataSyncService> _logger;
        private readonly UrlHelper _urlConstructor;
        private readonly IConfiguration _configuration;
        private readonly DbHandlerFactory dbHandlerFactory;

        private readonly string _sportsXmlEndpoint = string.Empty;
        private readonly string _clientKey = string.Empty;

        public event Action<string> EntityUpdated;

        public DataSyncService(DataFetchService dataFetchService,
            ILogger<DataSyncService> logger,
            IConfiguration configuration,
            DbHandlerFactory dbHandlerFactory)
        {
            _dataFetchService = dataFetchService;
            _logger = logger;
            _configuration = configuration;

            _urlConstructor = new UrlHelper(configuration[ApiConstants.BaseUrlConfigSection]);
            _sportsXmlEndpoint = configuration[ApiConstants.SportsXmlEndpointConfigSection];
            _clientKey = configuration[ApiConstants.ClientKeyConfigSection];
            this.dbHandlerFactory = dbHandlerFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _ = Task.Run(async () =>
             {

                 while (!cancellationToken.IsCancellationRequested)
                 {
                     var requestUrl = _urlConstructor.ConstructUrl(_sportsXmlEndpoint, new Dictionary<string, string>
                     {
                        {ApiConstants.ClientKeyQueryParam, _clientKey },
                        { ApiConstants.SportIdQueryParam, ApiConstants.EsportsSportId },
                        { ApiConstants.DaysQueryParam, ApiConstants.DaysValue }
                     });

                     var sportsContainer = await _dataFetchService.FetchData<XmlSports>(requestUrl);

                     await SyncData<Sport>(sportsContainer?.Sports.FirstOrDefault());

                     await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
                 }
             }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private async Task SyncData<T>(T entityToUpdate) where T : BaseDataModel
        {
            try
            {
                if (entityToUpdate == null)
                {
                    _logger.LogWarning(string.Format(LoggingConstants.UnsuccsessfullDataFetchMsg, nameof(SyncData)));
                    return;
                }

                var dbHandler = dbHandlerFactory.CreateDbHandler();

                //Removing outright matches due to requirements.
                if (entityToUpdate is Sport sportData)
                {
                    sportData = RemoveOutrightMatches(sportData);
                    dbHandler.SyncSport(sportData);
                }
                else
                {
                    if (dbHandler.GetEntities<T>().Any(x => x.ID == entityToUpdate.ID))
                    {
                        dbHandler.UpdateEntity<T>(entityToUpdate);
                        EntityUpdated?.Invoke(string.Format(LoggingConstants.EntityUpdatedMessage, entityToUpdate.Name, entityToUpdate.ID));

                        _logger.LogDebug(string.Format(LoggingConstants.EntityUpdatedMessage, entityToUpdate.Name, entityToUpdate.ID));
                    }
                    else
                    {
                        dbHandler.AddEntity<T>(entityToUpdate);
                        EntityUpdated?.Invoke(string.Format(LoggingConstants.EntityCreatedMessage, entityToUpdate.Name, entityToUpdate.ID));

                        _logger.LogDebug(LoggingConstants.EntityCreatedMessage, entityToUpdate.Name, entityToUpdate.ID);

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(LoggingConstants.UnsuccsessfullDataUpdateMsg, nameof(SyncData), entityToUpdate.ID));
            }

        }

        private static Sport? RemoveOutrightMatches(Sport data)
        {
            data.Events = data.Events.Select(e => new Event
            {
                ID = e.ID,
                Name = e.Name,
                Matches = e.Matches.Where(x => x.MatchType != Enums.SportsMatchType.OutRight).ToList(),
            }).Where(e => e.Matches.Count != 0).ToList();

            return data;
        }
    }
}
