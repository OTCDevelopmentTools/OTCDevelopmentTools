using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Bird.Client.Mtchmkr.Business.Common;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Response;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class ScoreCardViewModel : BaseViewModel
    {
        private readonly IProgressDialog _progDialog;

        private ObservableCollection<PlayerDTO> playersItemsSource;
        public ObservableCollection<PlayerDTO> PlayersItemsSource
        {
            get
            {
                return playersItemsSource;
            }
            set
            {
                playersItemsSource = value;
                OnPropertyChanged();
            }
        }

        private PlayerDTO playerOne;
        public PlayerDTO PlayerOne
        {
            get
            {
                return playerOne;
            }
            set
            {
                playerOne = value;
                OnPropertyChanged();
            }
        }

        private PlayerDTO playerTwo;
        public PlayerDTO PlayerTwo
        {
            get
            {
                return playerTwo;
            }
            set
            {
                playerTwo = value;
                OnPropertyChanged();
            }
        }

        private bool isNoDataView = false;
        public bool IsNoDataView
        {
            get
            {
                return isNoDataView;
            }
            set
            {
                isNoDataView = value;
                OnPropertyChanged();
            }
        }

        private bool isScoreCardView = false;
        public bool IsScoreCardView
        {
            get
            {
                return isScoreCardView;
            }
            set
            {
                isScoreCardView = value;
                OnPropertyChanged();
            }
        }

        private int maxFrame = 20;
        public int MaxFrame
        {
            get
            {
                return maxFrame;
            }
            set
            {
                maxFrame = value;
                OnPropertyChanged();
            }
        }

        private int completedFrame;
        public int CompletedFrame
        {
            get
            {
                return completedFrame;
            }
            set
            {
                completedFrame = value;
                if (CompletedFrame == MaxFrame)
                {
                    PlayersFrameButtonEnable = false;
                }
                OnPropertyChanged();
            }
        }

        private int playerOneFrame;
        public int PlayerOneFrame
        {
            get
            {
                return playerOneFrame;
            }
            set
            {
                playerOneFrame = value;
                OnPropertyChanged();
            }
        }

        private int playerTwoFrame;
        public int PlayerTwoFrame
        {
            get
            {
                return playerTwoFrame;
            }
            set
            {
                playerTwoFrame = value;
                OnPropertyChanged();
            }
        }

        private bool playersFrameButtonEnable = true;
        public bool PlayersFrameButtonEnable
        {
            get
            {
                return playersFrameButtonEnable;
            }
            set
            {
                playersFrameButtonEnable = value;
                OnPropertyChanged();
            }
        }

        public ICommand PlayerOneFrameCommand { get => new Command(() => PlayerOneFrameCommandMethod()); }

        public ICommand PlayerTwoFrameCommand { get => new Command(() => PlayerTwoFrameCommandMethod()); }

        public ICommand UndoCommand { get => new Command(() => UndoCommandMethod()); }

        public ScoreCardViewModel()
        {
            _progDialog = DependencyService.Get<IProgressDialog>();
        }

        async void PlayerOneFrameCommandMethod()
        {
            if ((PlayerOneFrame + PlayerTwoFrame) < MaxFrame)
            {
                PlayerOneFrame++;
                CompletedFrame = PlayerOneFrame + PlayerTwoFrame;
            }
        }

        async void PlayerTwoFrameCommandMethod()
        {
            if ((PlayerOneFrame + PlayerTwoFrame) < MaxFrame)
            {
                PlayerTwoFrame++;
                CompletedFrame = PlayerOneFrame + PlayerTwoFrame;
            }
        }

        async void UndoCommandMethod()
        {
            PlayerOneFrame = 0;
            PlayerTwoFrame = 0;
            CompletedFrame = 0;
        }

        public async void SearchPlayersMethod()
        {
            var matchIdString = Preferences.Get("MatchId", string.Empty);
            
            if (string.IsNullOrEmpty(matchIdString))
            {
                IsNoDataView = true;
                IsScoreCardView = false;
                return;
            }

            IsNoDataView = false;
            IsScoreCardView = true;
            var matchId = Guid.Parse(matchIdString);
            _progDialog.ShowProgress("Loading...");
            
            var result = await App.ServiceManager.ScoreCardGetUserDetailsByMatchId(matchId);
            if (result != null && result.Count > 0)
            {
                PlayersItemsSource = new ObservableCollection<PlayerDTO>(result);
                PlayerOne = result.FirstOrDefault();
                PlayerTwo = result.LastOrDefault();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "No Data Found", "Ok");
            }

            _progDialog.HideProgress();
        }
    }
}