using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadrinoFestApp.Services
{
    public interface IRemoteDatabaseApiService
    {
        Task<List<T>> GetItems<T>(string endpoint) where T : new();
        Task<T> GetItem<T>(string endpoint, int id) where T : new();
        Task<bool> AddItem<T>(string endpoint, T item) where T : new();
        Task<bool> AddItems<T>(string endpoint, List<T> items) where T : new();
        Task<bool> UpdateItem<T>(string endpoint, int id, T item) where T : new();
        Task<bool> DeleteItem<T>(string endpoint, int id) where T : new();
    }
}
