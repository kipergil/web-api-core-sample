using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using RunPath.WebApi.Services.Interfaces;
using RunPath.WebApi.Domain;

namespace RunPath.WebApi.Services
{
    public class HttpDataReaderService<T> : IHttpDataReaderService<T>
    {
        public HttpDataReaderService()
        {

        }

        public T ReadAsItem(string url)
        {
            T result = default(T);

            var response = ReadFromUrl(url);

            var content = response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = content.Result;
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonResult);
            }
            else
            {
                //throw new ApiException
                //{
                //    StatusCode = (int)response.StatusCode,
                //    Content = content.Result
                //};
            }

            return result;
        }

        public List<T> ReadAsList(string url)
        {
            List<T> result = null;
            var response = ReadFromUrl(url);
            var content = response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = content.Result;
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(jsonResult);
            }
            else
            {
                //throw new ApiException
                //{
                //    StatusCode = (int)response.StatusCode,
                //    Content = content.Result
                //};
            }
            return result;
        }

        private HttpResponseMessage ReadFromUrl(string url)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("")
            };

            using (HttpClient client = new HttpClient { BaseAddress = new Uri(url) })
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync(url).Result;
            }

            return response;
        }
    }

}
