using Bird.Client.Mtchmkr.Portable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bird.Client.Mtchmkr.Portable.Services
{
    public class RequestDataStore : IDataStore<RequestModel>
    {
        readonly List<RequestModel> items;

        public RequestDataStore()
        {
            items = new List<RequestModel>()
            {
                //new RequestModel { Key = Guid.NewGuid(), Text = "First item", Description="This is an item description." },
                //new RequestModel { Key = Guid.NewGuid(), Text = "Second item", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(RequestModel item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(RequestModel item)
        {
            var oldItem = items.Where((RequestModel arg) => arg.Key == item.Key).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Guid key)
        {
            var oldItem = items.Where((RequestModel arg) => arg.Key == key).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<RequestModel> GetItemAsync(Guid key)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Key == key));
        }

        public async Task<IEnumerable<RequestModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}