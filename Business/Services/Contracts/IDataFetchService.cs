using UltraPlayTask.Data.Models;

namespace UltraPlayTask.Business.Services.Contracts
{
    public interface IDataFetchService
    {
        Task<T> FetchData<T>(string url) where T : BaseDataModel;
    }
}