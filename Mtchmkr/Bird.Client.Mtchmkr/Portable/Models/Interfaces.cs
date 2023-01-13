using System;
using System.Collections.ObjectModel;

namespace Bird.Client.Mtchmkr.Portable.Models
{
    public interface IMatch
    {
        Guid MatchKey { get; set; }
        string GameName { get; set; }
        string Location { get; set; }
        string Duration { get; set; }
        string OccuranceSuffix { get; set; }
        DateTime Date { get; set; }
        string DayName { get; set; }
        int Day { get; set; }
        int MinimumSkills { get; set; }
        int MaximumSkills { get; set; }
    }
    public interface IGame
    {
        Guid GameKey { get; set; }
        string GameName { get; set; }
        string GameImage { get; set; }

    }
    public interface IPlayer
    {
        Guid PlayerKey { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string PlayerImage { get; set; }

        int Skill { get; set; }

        ObservableCollection<GameModel> Games { get; set; }
    }


}