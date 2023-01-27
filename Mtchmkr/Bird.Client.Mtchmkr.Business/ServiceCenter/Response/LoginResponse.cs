using System;
namespace Bird.Client.Mtchmkr.Business.ServiceCenter.Response
{
    public class LoginResponse
    {
        public string userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string telephone { get; set; }
        public DateTime createdDate { get; set; }
    }

    public class FcmDeviceInfo
    {
        public string deviceId { get; set; }
        public Guid userId { get; set; }
        public string deviceToken { get; set; }
        public string deviceType { get; set; }
        public DateTime createdDate { get; set; }
    }

}
