using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CombatManagerApi
{
 
    public static class NetworkingExtensions
    {
        public static async Task<T> PostAsync<T>(string address, object parameters)
        {
            var json = JsonConvert.SerializeObject(parameters);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(address, data);

                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        public static async Task<T> GetAsync<T>(string address,string passcode)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("passcode",passcode);
                client.BaseAddress = new Uri(address);
                var result = await client.GetAsync(address);
                var content = await result.Content.ReadAsStringAsync();
                if (result.StatusCode != HttpStatusCode.OK)
                    throw new Exception("Error: " + result.StatusCode.ToString() + " Address: " + address);
                Console.WriteLine(content);
                Console.WriteLine(result.StatusCode);
                return JsonConvert.DeserializeObject<T>(content);
            }
             
        }
        public static async Task<string> GetAsync(string address, string passcode)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("passcode", passcode);
                client.BaseAddress = new Uri(address);
                var result = await client.GetAsync(address);
                var content = await result.Content.ReadAsStringAsync();
                //var obj = JObject.FromObject( result.Content.ReadAsStreamAsync());
                Console.WriteLine(content);
                Console.WriteLine(result.StatusCode);
                return content;
            }
            
        }
        ///combat/rollinit
    }
}