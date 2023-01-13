using Bird.Client.Mtchmkr.Portable.Models;
using Bird.Client.Mtchmkr.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class PlayersViewModel : BaseViewModel
    {
        private PlayerModel _selectedItem;

        private List<PlayerModel> m_Players;
        public ObservableCollection<PlayerModel> Items { get; set; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<PlayerModel> ItemTapped { get; }
        public PlayersViewModel()
        {
            Title = "Players";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            Load();
        }
        private async void Load()
        {
            PlayerDataStore store = new PlayerDataStore();
            var response = await store.GetItemsAsync();
            Items = new ObservableCollection<PlayerModel>();
            foreach(var item in response)
            {
                Items.Add(item);
            }
        }
        public List<PlayerModel> Players
        {
            get { return m_Players; }
            set {  m_Players = value; }
        }
        public ICommand OpenWebCommand { get; }
    }
}