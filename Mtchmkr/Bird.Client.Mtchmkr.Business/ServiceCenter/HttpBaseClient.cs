using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Bird.Client.Mtchmkr.Business.Common;
using Newtonsoft.Json;

namespace Bird.Client.Mtchmkr.Business.ServiceCenter
{
    public class HttpBaseClient
    {
        protected readonly string Endpoint;
        protected readonly string AccessToken;
        protected readonly string DeviceId;
        HttpClient httpClient;
        public HttpBaseClient(string endpoint, string accessToken = null, string deviceId = null)
        {
            Endpoint = endpoint;
            httpClient = new HttpClient();
            AccessToken = accessToken;
            DeviceId = deviceId;
        }

        private void SetHeader(bool isFileUpload = false)
        {
            if (isFileUpload)
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            }
            else
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            if (!string.IsNullOrWhiteSpace(AccessToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                httpClient.DefaultRequestHeaders.Add("DeviceId", DeviceId);
            }
        }

        public async Task<string> Get()
        {
            string response;
            try
            {
                this.SetHeader();
                response = await this.httpClient.GetStringAsync(Endpoint);
            }
            catch (UnauthorizedAccessException ex)
            {
                response = BuildErrorMessage(ex);
            }
            catch (TaskCanceledException exception)
            {
                response = BuildErrorMessage(exception);
            }
            catch (HttpRequestException exception)
            {
                response = BuildErrorMessage(exception);
            }
            catch (Exception exception)
            {
                response = BuildErrorMessage(exception);
            }
            finally
            {
                httpClient.Dispose();
            }
            response = response
                        .Trim('"')
                        .Replace(@"\", "");

            return response;
        }

        public async Task<string> Put(string payload)
        {
            HttpResponseMessage responseMessage;
            string result = null;

            try
            {
                this.SetHeader();

                responseMessage = await this.httpClient.PutAsync(Endpoint, new StringContent(payload, System.Text.Encoding.UTF8, "application/json"));

                result = await responseMessage.Content.ReadAsStringAsync();

            }
            catch (UnauthorizedAccessException ex)
            {
                result = BuildErrorMessage(ex);
            }
            catch (TaskCanceledException exception)
            {
                result = BuildErrorMessage(exception);
            }
            catch (HttpRequestException exception)
            {
                result = BuildErrorMessage(exception);
            }
            catch (Exception exception)
            {
                result = BuildErrorMessage(exception);
            }
            finally
            {
                httpClient.Dispose();
            }

            return result;
        }

        public async Task<string> Post(string payload, string mediaType = "application/json")
        {
            HttpResponseMessage responseMessage;
            string result = null;
            try
            {
                this.SetHeader();
                // Content-type cannot be set on DefaultRequestHeaders but must be set on HttpContent
                // else a 419 will be thrown
                HttpContent content = new StringContent(payload, System.Text.Encoding.UTF8);
                content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                responseMessage = await this.httpClient.PostAsync(Endpoint, content);
                result = await responseMessage.Content.ReadAsStringAsync();
            }
            catch (UnauthorizedAccessException ex)
            {
                // immediately remove token by clearing it
                result = BuildErrorMessage(ex);
            }
            catch (TaskCanceledException ex)
            {
                result = BuildErrorMessage(ex);
            }
            catch (HttpRequestException ex)
            {
                result = BuildErrorMessage(ex);
            }
            catch (Exception ex)
            {
                result = BuildErrorMessage(ex);
            }
            finally
            {
                httpClient.Dispose();
            }
            if (result == "0")
                result = true.ToString();
            return result;
        }

        public async Task<string> Delete()
        {
            HttpResponseMessage responseMessage;
            string result = null;

            try
            {
                this.SetHeader();

                responseMessage = await this.httpClient.DeleteAsync(Endpoint);

                result = await responseMessage.Content.ReadAsStringAsync();

            }
            catch (TaskCanceledException exception)
            {
                result = BuildErrorMessage(exception);
            }
            catch (HttpRequestException exception)
            {
                result = BuildErrorMessage(exception);
            }
            catch (Exception exception)
            {
                result = BuildErrorMessage(exception);
            }
            finally
            {
                httpClient.Dispose();
            }

            return result;
        }

        public string BuildErrorMessage(Exception ex)
        {
            ErrorMessage msg;
            if (ex == null)
            {
                msg = new ErrorMessage()
                {
                    Message = "Constants.ErrorInServiceMessage",
                    StackTrace = ex.StackTrace
                };
            }
            else
            {
                if (ex.Equals(ex as OperationCanceledException))
                {
                    msg = new ErrorMessage()
                    {
                        Message = ex.Message.Equals("A task was canceled.") ? Constants.ServerNotResponding : string.Empty,
                        StackTrace = ex.StackTrace
                    };
                }
                else if (ex.Equals(ex as UnauthorizedAccessException))
                {
                    msg = new ErrorMessage()
                    {
                        Message = "Licence invalid",
                        StackTrace = ex.StackTrace
                    };
                }
                else
                    msg = new ErrorMessage()
                    {
                        Message = ex.Message,
                        StackTrace = ex.StackTrace
                    };
            }
            return JsonConvert.SerializeObject(msg);
        }
    }
}