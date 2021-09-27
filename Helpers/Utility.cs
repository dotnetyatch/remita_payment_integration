using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RemitaMiddleWare
{
    public class Utility
    {
        public async Task<HttpResponseMessage> MakeHttpRequest(object request, string baseAddress, string requestUri, HttpMethod method, Dictionary<string, string> headers = null)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    if (headers != null)
                    {
                        foreach (KeyValuePair<string, string> header in headers)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }
                    if (method == HttpMethod.Post)
                    {
                        string data = JsonConvert.SerializeObject(request);
                        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
                        return await client.PostAsync(requestUri, content);
                    }
                    else if (method == HttpMethod.Get)
                    {
                        return await client.GetAsync(requestUri);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                // add logging
                return null;
            }
        }

        private static Random random = new Random();
        public static string Generate(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string getTimeStamp()
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var day = DateTime.Now.Day;
            var hour = DateTime.Now.Hour;
            var minute = DateTime.Now.Minute;
            var second = DateTime.Now.Second;
            var timeStamp = year + "-" + month + "-" + day + "T" + hour + ":" + minute + ":" + second + "+000000";

            return timeStamp;
        }
    }


}
