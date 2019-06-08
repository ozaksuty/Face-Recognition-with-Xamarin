using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FaceSample.Provider
{
    public class ServiceManager : IServiceManager
    {
        private static HttpClient _client;
        private static HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient
                    {
                        BaseAddress = new Uri("https://westeurope.api.cognitive.microsoft.com/face/v1.0/")
                    };
                    _client.DefaultRequestHeaders.Add("Accept", "application/json");
                    _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "your key");
                }
                return _client;
            }
        }

        public async Task<T> Get<T>(string url)
        {
            try
            {
                var result = await Client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception ex)
            {
                //Handle exception
                return default(T);
            }
        }

        public async Task<T> Post<T, K>(K requestModel, string url)
        {
            try
            {
                var model = JsonConvert.SerializeObject(requestModel);
                var result = await Client.PostAsync(url,
                    new StringContent(model, Encoding.UTF8, "application/json"));
                var response = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(response);
            }
            catch (Exception ex)
            {
                //Handle exception
                return default(T);
            }
        }

        public async Task<T> PostStream<T>(Stream stream, string url)
        {
            try
            {
                byte[] byteData = ReadFully(stream);
                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    var result = await Client.PostAsync(url, content);

                    if (result.IsSuccessStatusCode && result.StatusCode == HttpStatusCode.OK)
                    {
                        var response = await result.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(response);
                    }
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                //Handle Exception
                return default(T);
            }
        }

        byte[] ReadFully(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}