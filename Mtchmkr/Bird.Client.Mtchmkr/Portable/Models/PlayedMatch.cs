using System;
using System.Text;
using System.Collections.Generic;
using Bird.Client.Mtchmkr.Helpers;

namespace Bird.Client.Mtchmkr.Portable.Models
{

    public class PlayedMatchModel
    {
        public Guid Key { get; set; }
        public int Matches { get; set; }
        public int Player1Wins { get; set; }
        public int Player2Wins { get; set; }
        public string Game { get; set; }
        public string GameImage { get; set; }
        public string Player1 { get; set; }
        public string Player1Initials { get => Naming.CreateInitials(Player1); }

        public string Player1Image { get; set; }
        public string Player2Image { get; set; }
        public string Player2 { get; set; }
        public string Player2Initials { get => Naming.CreateInitials(Player2); }

        public int Games { get => (Player1Wins + Player2Wins); }
        public DateTime Date { get; set; }

        public System.Drawing.Color Colour { get; set; }
        public bool Confirmed { get; set; }
        public bool NotConfirmed { get => !Confirmed; }

 
    }
}