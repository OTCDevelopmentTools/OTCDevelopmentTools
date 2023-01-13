using System;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Request;

namespace Bird.Client.Mtchmkr.Business.ServiceCenter
{
    public class ServiceManager
    {
        private readonly HttpRequestHelper httpRequestHelper;

        public ServiceManager()
        {
            httpRequestHelper = new HttpRequestHelper();
        }
    }
}