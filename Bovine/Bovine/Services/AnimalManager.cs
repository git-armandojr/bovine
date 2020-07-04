using Bovine.Models.Animal;
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
    public class AnimalManager
    {
        public async Task<IEnumerable<Animal>> GetAll()
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl, "?type=Animal"));

            string result = string.Empty;

            try
            {
                HttpClient client = await GetClient();
                result = await client.GetStringAsync(uri);
            }
            catch (HttpRequestException e)
            {
                Log.Warning("HttpRequest", e.Message);
            }

            return JsonConvert.DeserializeObject<IEnumerable<Animal>>(result);
        }

        public async Task Update(Animal animal)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl, "/" + animal.id + "/attrs?"));

            string json = JsonConvert.SerializeObject(animal,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

            var raw = json;
            var o = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(json);
            o.Property("id").Remove();

            HttpClient client = await GetClient();
            await client.PostAsync(uri,
                new StringContent(
                    o.ToString(),
                    Encoding.UTF8, "application/json"));
        }

        public async Task<HttpResponseMessage> Add(Animal animal)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

            string json = JsonConvert.SerializeObject(animal,
                             Newtonsoft.Json.Formatting.None,
                             new JsonSerializerSettings
                             {
                                 NullValueHandling = NullValueHandling.Ignore
                             });

            HttpClient client = await GetClient();
            HttpResponseMessage response = await client.PostAsync(uri,
                new StringContent(
                    json,
                    Encoding.UTF8, "application/json"));

            //return JsonConvert.DeserializeObject<AgriFarm>(await response.Content.ReadAsStringAsync());
            return response;
        }

        public async Task<IRestResponse> Delete(string id)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl, "/" + id));
            /*
            NÃO SEI PQ ESSA BOSTA NÃO FUNCIONA!!!!
            
            HttpClient client = await GetClient();
            await client.DeleteAsync(url + agriFarm.id);
            */

            var client = new RestClient(uri);
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
