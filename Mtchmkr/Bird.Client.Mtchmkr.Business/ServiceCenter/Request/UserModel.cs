using System;
namespace Bird.Client.Mtchmkr.Business.ServiceCenter.Request
{
    public class UserModel
    {
        public Guid userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string telephone { get; set; }
        public DateTime createdDate { get; set; }
    }

    public class ImageDTO
    {
        public Guid userId { get; set; }
        public string imageTitle { get; set; }
        public string imageData { get; set; }
        public string imageExtension { get; set; }
        public DateTime createdDate { get; set; }
    }


    public class NetworkAuthData
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public string Picture { get; set; }

        public string Background { get; set; }

        public string Foreground { get; set; }
    }
}
