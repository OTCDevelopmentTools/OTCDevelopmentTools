using System.Collections.Generic;

namespace Bird.Client.Mtchmkr.Portable.Models
{

    public class PlayerModel: BasePlayerModel
    {
      

        public bool Selected { get; set; }

        public System.Drawing.Color Colour { get; set; }



    }
    public class PlayersModel : List<PlayerModel>
    {

    }
}