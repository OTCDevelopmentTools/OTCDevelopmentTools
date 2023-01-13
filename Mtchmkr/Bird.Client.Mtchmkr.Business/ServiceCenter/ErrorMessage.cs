using System;
namespace Bird.Client.Mtchmkr.Business.ServiceCenter
{
    public class ErrorMessage
    {
        public ErrorMessage()
        {
        }

        public string Message { get; set; }

        public string StackTrace { get; set; }

    }
}