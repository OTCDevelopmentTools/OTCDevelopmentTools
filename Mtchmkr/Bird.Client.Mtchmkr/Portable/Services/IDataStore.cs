using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bird.Client.Mtchmkr.Portable.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(Guid key);
        Task<T> GetItemAsync(Guid key);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
