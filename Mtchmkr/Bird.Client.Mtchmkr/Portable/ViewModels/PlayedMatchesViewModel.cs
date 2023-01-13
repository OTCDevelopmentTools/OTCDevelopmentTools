using Bird.Client.Mtchmkr.Portable.Models;
using System.Collections.ObjectModel;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class PlayedMatchesViewModel : BaseViewModel
    {
        public ObservableCollection<PlayedMatchModel> Matches { get => CreateMatches(); }

        private ObservableCollection<PlayedMatchModel> CreateMatches()
        {
            return new ObservableCollection<PlayedMatchModel>
            {
                new PlayedMatchModel { Key = System.Guid.NewGuid(), Player1 = "Todd Carty", Player2="Tucker Jenkins", Player1Wins=30, Player2Wins=20,
                    Player1Image="AB.jpg" , Player2Image="JB.jpg", Confirmed=true,
                    Colour = System.Drawing.Color.Red ,Game="Pool", GameImage="Pool.jpg"},
                new PlayedMatchModel { Key = System.Guid.NewGuid(), Player1 = "Todd Carty", Player2="Tucker Jenkins", Player1Wins=30, Player2Wins=20,
                    Player1Image="AB.jpg" , Player2Image="JB.jpg", Confirmed=false,
                    Colour = System.Drawing.Color.Red ,Game="Snooker", GameImage="Snooker.jpg"},


            };
        }
    }
}