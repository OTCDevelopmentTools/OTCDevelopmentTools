using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Bird.Client.Mtchmkr.Business.ServiceCenter;
using Newtonsoft.Json;

namespace Bird.Client.Mtchmkr.Business.ServiceCenter.Request
{
    public class HttpRequestHelper
    {
        protected readonly string ServiceEndpoint;
        public bool Authenticated = false;

        public HttpRequestHelper()
        {
            ServiceEndpoint = ServiceHelper.ServiceUrl;
        }


        public async Task<T> CreateGetResponse<T>(string ServiceAPIResourcePath, string accessToken = null, string deviceId = null)
        {
            try
            {

                var httpBaseClient = new HttpBaseClient(System.IO.Path.Combine(new string[] {
                    ServiceEndpoint,
                    ServiceAPIResourcePath
                }), accessToken, deviceId);

                var responseCRServiceContent = await httpBaseClient.Get();
                return ServiceHelper.GetResponse<T>(responseCRServiceContent, ref this.Authenticated);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task<string> CreateImagePostRequest<T>(string ServiceAPIResourcePath, object payload)
        {
            var httpBaseClient = new HttpBaseClient(System.IO.Path.Combine(new string[] {
                ServiceEndpoint,
                ServiceAPIResourcePath
            }));
            var responseServiceContent = await httpBaseClient.Post(JsonConvert.SerializeObject(payload));
            return JsonConvert.DeserializeObject<string>(responseServiceContent);
        }

        public async Task<T> CreatePostRequest<T>(string ServiceAPIResourcePath, object payload, string accessToken = null)
        {
            var httpBaseClient = new HttpBaseClient(System.IO.Path.Combine(new string[] {
                ServiceEndpoint,
                ServiceAPIResourcePath
            }), accessToken);
            if (payload.GetType().ToString() != "System.String")
            {
                payload = JsonConvert.SerializeObject(payload);
            }
            var responseServiceContent = await httpBaseClient.Post(payload.ToString());
            return ServiceHelper.GetResponse<T>(responseServiceContent, ref this.Authenticated);
        }


        public async Task<T> CreatePutRequest<T>(string ServiceAPIResourcePath, object payload, string accessToken = null)
        {
            var httpBaseClient = new HttpBaseClient(System.IO.Path.Combine(new string[] {
                ServiceEndpoint,
                ServiceAPIResourcePath
            }), accessToken);
            if (payload.GetType().ToString() != "System.String")
            {
                payload = JsonConvert.SerializeObject(payload);
            }
            var responseServiceContent = await httpBaseClient.Put(payload.ToString());
            return ServiceHelper.GetResponse<T>(responseServiceContent, ref this.Authenticated);
        }

        public async Task<T> CreateDeleteRequest<T>(string ServiceAPIResourcePath, string accessToken = null)
        {
            try
            {
                var httpBaseClient = new HttpBaseClient(System.IO.Path.Combine(new string[] {
                    ServiceEndpoint,
                    ServiceAPIResourcePath
                }), accessToken);

                var responseCRServiceContent = await httpBaseClient.Delete();
                return ServiceHelper.GetResponse<T>(responseCRServiceContent, ref this.Authenticated);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

    }
}