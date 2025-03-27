using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json;

namespace APIConnect
{
    class TestAPI
    {
        private HttpClient client; 
        public TestAPI()
        {
            client = new HttpClient();
        }
        public async Task GetData()
        {
            var app_id = "1a8807692b4544a6af47c891eaa77045";
            var date = "2012-12-12";
            string call = "https://openexchangerates.org/api/historical/"+date+".json?app_id=" + app_id;
            string response = await client.GetStringAsync(call);
            Currencies currencyData = JsonSerializer.Deserialize<Currencies>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

            Console.WriteLine(currencyData.ToString());
            foreach (var rate in currencyData.rates)
            {
                Console.WriteLine($"{rate.Key}: {rate.Value}");
            }

        }    
        
    }
}
 