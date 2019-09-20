using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Utilities
{
    public class VendorCall
    {
        public static async Task<string> CallDream11API(string json)
        {
            string jsonResponse = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6740");
                var req = json;
                var buffer = System.Text.Encoding.UTF8.GetBytes(req);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await client.PostAsync("https://www.dream11.com/graphql", byteContent);
                if (result.IsSuccessStatusCode)
                {
                    jsonResponse = await result.Content.ReadAsStringAsync();
                }

            }
            return jsonResponse;
        }

        public static async Task<string> CallDream11NewAPI(string json)
        {
            string jsonResponse = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6740");
                var req = json;
                var buffer = System.Text.Encoding.UTF8.GetBytes(req);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await client.PostAsync("https://www.dream11.com/graphql/query/pwa/fantasy-score-card", byteContent);
                if (result.IsSuccessStatusCode)
                {
                    jsonResponse = await result.Content.ReadAsStringAsync();
                }

            }
            return jsonResponse;
        }

    }
}
