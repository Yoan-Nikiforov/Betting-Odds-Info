namespace UltraPlayTask.Data.DbHandlers
{
    //Only created for DataSyncService due to service lifetimes, but separated for better readability
    public class DbHandlerFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DbHandlerFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public DbHandler CreateDbHandler()
        {
            var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<DbHandler>();
        }
    }
}