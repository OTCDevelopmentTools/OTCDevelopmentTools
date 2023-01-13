using Bird.Client.Mtchmkr.Portable.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private Guid itemId;
        private string text;
        private string description;
        public Guid Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public Guid ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(Guid itemId)
        {
            //try
            //{
            //    var item = await DataStore.GetItemAsync(itemId);
            //    Id = item.Key;
            //    Text = item.Text;
            //    Description = item.Description;
            //}
            //catch (Exception)
            //{
            //    Debug.WriteLine("Failed to Load Item");
            //}
        }
    }
}
