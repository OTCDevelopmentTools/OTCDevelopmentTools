using Bird.Client.Mtchmkr.Portable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bird.Client.Mtchmkr.Portable.Services
{
    public class PlayerDataStore : IDataStore<PlayerModel>
    {
        readonly List<PlayerModel> items;

        public PlayerDataStore()
        {
            items = new List<PlayerModel>()
            {
                //new PlayerModel { Key = Guid.NewGuid(),Image="Jb.jpg", FirstName = "James",LastName="Broughton",Colour=System.Drawing.Color.Red, Description="This is an item description." },
                //new PlayerModel { Key = Guid.NewGuid(), FirstName = "Todd",LastName="Carty",Colour=System.Drawing.Color.Green, Description="This is an item description." },
                //new PlayerModel { Key = Guid.NewGuid(), FirstName = "Tucker",LastName="Jenkins",Colour=System.Drawing.Color.Orange, Description="This is an item description." },
                //new PlayerModel { Key = Guid.NewGuid(), FirstName = "Simon",LastName="Pieman",Colour=System.Drawing.Color.PaleVioletRed, Description="This is an item description." },
                //new PlayerModel { Key = Guid.NewGuid(),Image="AB.jpg", FirstName = "Andrew",LastName="Bird",Colour=System.Drawing.Color.Blue, Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(PlayerModel item)
        {
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(PlayerModel item)
        {
            var oldItem = items.Where((PlayerModel arg) => arg.PlayerKey == item.PlayerKey).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Guid key)
        {
            var oldItem = items.Where((PlayerModel arg) => arg.PlayerKey == key).FirstOrDefault();
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<PlayerModel> GetItemAsync(Guid key)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.PlayerKey == key));
        }

        public async Task<IEnumerable<PlayerModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}