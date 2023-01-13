using Bird.Client.Mtchmkr.Portable.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    [QueryProperty(nameof(Key), nameof(Key))]
    public class RequestDetailViewModel : BaseViewModel
    {
        private Guid m_Key;
        private string text;
        private string description;
        public string Id { get; set; }

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

        public Guid Key
        {
            get
            {
                return m_Key;
            }
            set
            {
                m_Key = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(Guid key)
        {
            //try
            //{
            //    var item = await DataStore.GetItemAsync(key);
            //    Key = item.Key;
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
