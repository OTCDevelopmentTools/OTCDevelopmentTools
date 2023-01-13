//using System.Drawing;

using Bird.Client.Mtchmkr.Portable.Models;

namespace Bird.Client.Mtchmkr.Helpers
{
    public class ProfileHelper
    {
        public static ProfileModel Create()
        {
            ProfileModel model = new ProfileModel();
            PlayerHelper.Build(model);
            model.IsAdmin = true;
            
            //            GameHelper.Build(model);
            return model;
        }
    }
}
