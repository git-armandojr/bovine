using Bovine.Models;
using Bovine.Models.AgriFarm;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Bovine.Services
{
    public class AgriFarmManager
    {
        const string URL = "http://ec10-ftt.ddns.net:1026/v2/entities";

        public async Task<IEnumerable<AgriFarm>> GetAll()
        {
            string result = string.Empty;

            try
            {
                HttpClient client = await GetClient();
                result = await client.GetStringAsync(URL + "?type=AgriFarm");
            }
            catch (HttpRequestException e)
            {
                Log.Warning("HttpRequest", e.Message);
            }

            return JsonConvert.DeserializeObject<IEnumerable<AgriFarm>>(result);
        }

        public async Task Update(AgriFarm agriFarm)
        {
            string url = URL + "/" + agriFarm.id + "/attrs?";

            string json = JsonConvert.SerializeObject(agriFarm,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

            var raw = json;
            var o = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(json);
            o.Property("id").Remove();

            HttpClient client = await GetClient();
            await client.PostAsync(url,
                new StringContent(
                    o.ToString(),
                    Encoding.UTF8, "application/json"));
        }

        public async Task<HttpResponseMessage> Add(AgriFarm agriFarm)
        {

            string json = JsonConvert.SerializeObject(agriFarm,
                             Newtonsoft.Json.Formatting.None,
                             new JsonSerializerSettings
                             {
                                 NullValueHandling = NullValueHandling.Ignore
                             });

            HttpClient client = await GetClient();
            HttpResponseMessage response = await client.PostAsync(URL,
                new StringContent(
                    json,
                    Encoding.UTF8, "application/json"));

            //return JsonConvert.DeserializeObject<AgriFarm>(await response.Content.ReadAsStringAsync());
            return response;
        }


        public async Task<IRestResponse> Delete(string id)
        {
            string url = URL + "/" + id;
            /*
            NÃO SEI PQ ESSA BOSTA NÃO FUNCIONA!!!!
            
            HttpClient client = await GetClient();
            await client.DeleteAsync(url + agriFarm.id);
            */

            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return response;
        }

        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();            
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }
    }
}
