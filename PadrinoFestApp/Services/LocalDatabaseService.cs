using SQLite;

using PadrinoFestApp.Models;
using PadrinoFestApp.Helpers;
using System.Diagnostics;

namespace PadrinoFestApp.Services
{
    public class LocalDatabaseService : ILocalDatabaseService
    {
        private string dbPath;
        private SQLiteAsyncConnection connection;

        public LocalDatabaseService(string dbPath)
        {
            this.dbPath = dbPath;
        }

        private async Task Init()
        {
            if (connection != null)
                return;

            try
            {
                connection = new SQLiteAsyncConnection(dbPath);

                connection.Tracer = new Action<string>(q => Debug.WriteLine(q));
                connection.Trace = true;

                var commandsT = await FileAccessHelper.ReadFile("padrinofest_tables.txt");

                foreach (var command in commandsT)
                {
                    var op = await ExecuteQuery(command);
                    Debug.WriteLine(op);
                }

                await connection.CreateTableAsync<Evento>();
                await connection.CreateTableAsync<Speaker>();

                var eventsCount = await CountItemsWithQuery("SELECT COUNT(*) FROM eventos");
                Debug.WriteLine($"Eventos: {eventsCount}");

                var speakersCount = await CountItemsWithQuery("SELECT COUNT(*) FROM speakers");
                Debug.WriteLine($"Speakers: {speakersCount}");

                if (eventsCount == 0 && speakersCount == 0)
                {
                    var commandsD = await FileAccessHelper.ReadFile("padrinofest_data.txt");
                    foreach (var command in commandsD)
                    {
                        var op = await ExecuteQuery(command);
                        Debug.WriteLine(op);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task<List<T>> GetItemsWithQuery<T>(string query) where T : new()
        {
            await Init();

            return await connection.QueryAsync<T>(query);
        }

        public async Task<int> CountItemsWithQuery(string query) 
        {
            await Init();

            return await connection.ExecuteScalarAsync<int>(query);
        }

        public async Task<bool> ExecuteQuery(string query)
        {
            await Init();

            var op = await connection.ExecuteAsync(query);
            return op > 0;
        }
    }
}
