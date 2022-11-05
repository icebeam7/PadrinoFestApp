namespace PadrinoFestApp.Services
{
    public interface ILocalDatabaseService
    {
        Task<List<T>> GetItemsWithQuery<T>(string query) where T : new();
        Task<bool> ExecuteQuery(string query);
        Task<int> CountItemsWithQuery(string query);
    }
}