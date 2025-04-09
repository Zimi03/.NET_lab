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
        public async Task GetData(string Date)
        {
            var app_id = "1a8807692b4544a6af47c891eaa77045";
            var date = Date;
            string call = "https://openexchangerates.org/api/historical/"+date+".json?app_id=" + app_id;
            string response = await client.GetStringAsync(call);
            Currencies currencyData = JsonSerializer.Deserialize<Currencies>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

            Console.WriteLine(currencyData.ToString());
            foreach (var rate in currencyData.rates)
            {
                Console.WriteLine($"{rate.Key}: {rate.Value}");
            }
            using var context = new CurrencyExchangeOffice();

            // Sprawdzenie czy BaseCurrency z takim timestamp i baseCurrency już istnieje
            var existing = context.BaseCurrencies
                .FirstOrDefault(x => x.timestamp == currencyData.timestamp && x.Currency == currencyData.Base);

            if (existing != null)
            {
                Console.WriteLine("Dane już istnieją w bazie.");
                return;
            }

            var baseCurrency = new BaseCurrency
            {
                timestamp = currencyData.timestamp,
                Currency = currencyData.Base
            };

            foreach (var rate in currencyData.rates)
            {
                baseCurrency.ExchangeCurrencies.Add(new CurrencyRate
                {
                    ExchangeCurrency = rate.Key,
                    ExchangeRate = rate.Value,
                    BaseCurrency = baseCurrency
                });
            }

            context.BaseCurrencies.Add(baseCurrency);
            await context.SaveChangesAsync();

            Console.WriteLine("Dane zostały zapisane do bazy.");
        }    
        
    }
}
 