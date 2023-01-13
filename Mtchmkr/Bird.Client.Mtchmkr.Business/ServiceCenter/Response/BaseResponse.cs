using System;
namespace Bird.Client.Mtchmkr.Business.ServiceCenter.Response
{
    public class BaseResponse
    {
        public object Message { get; set; }
        public bool Success { get; set; }
        public int MessageType { get; set; }
        public object ApiToken { get; set; }
    }
}
