# Lab2 - Odebranie danych z API i zapisanie ich do bazy danych
Celem Laboratorium było podłączyć się do wybranego API, odebrać z niego dane, zdeserializować i wyświetlić na ekran.
## 1. Podłączenie do API
Do projektu zostało wybrane API openexchangerates.org - endpoint historical - link do dokumentacji: https://docs.openexchangerates.org/reference/historical-json.
Endpoint zwracał poniższy JSON:
``` JSON
{
"disclaimer": "Usage subject to terms: https://openexchangerates.org/terms",
"license": "https://openexchangerates.org/license",
"timestamp": 1341936000,
"base": "USD",
"rates": {
    "AED": 3.672914,
    "AFN": 48.337601,
    "ALL": 111.863334,
    "AMD": 404.739995,
    .
    .
    .
    "ZMK": 4715.31642,
    "ZWD": 361.898139,
    "ZWL": 322.355011
  }
}
```
JSON został zdeserializowany do poniższej klasy:
``` C#
public class Currencies
    {
        public long timestamp { get; set; }
        public string? Base { get; set; }
        public Dictionary<string, float>? rates { get; set; }  

        public override string ToString()
        {
            return $"Timestamp: {timestamp}, Base: {Base}, First Rate: {{rates?.FirstOrDefault().Key}} - {{rates?.FirstOrDefault().Value";
        }
    }
```
Podjęta została próba zdeserializowania pola rates do listy, która przyjmowałaby klasę Rates, która to miała mieć pola string - do skrótu waluty oraz float do wartości przeliczenia. API jednak jest zaprojektowane typowo pod słownik, przez co nie udało się dokonać tego w taki sposób. Próba z listą była wykonana w celu prostszego zapisu danych do bazy. 
W klasie przeciążono metodę ToString(), tak aby móc swobodnie wypisać wyniki. 
*Pobranie wyników* odbywało się z użyciem poniższej metody: 
``` C#
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
        }
}
```
Metoda GetData jest typu async Task, tak aby odbieranie danych nie blokowało dalszego działania programu.
## 2. Baza Danych
Następnie stworzona została baza danych, do której zapisywane były pobrane dane.
Baza składa się z dwóch tabel: 
``` C#
internal class BaseCurrency
    {
        public int Id { get; set; }
        public long? timestamp { get; set; }
        public required string Currency { get; set; }
        public ICollection<CurrencyRate> ExchangeCurrencies { get; set; } = new List<CurrencyRate>();
        public override string ToString()
        {
            return $"Id: {Id}, Timestamp: {timestamp?.ToString() ?? "N/A"}, Currency: {Currency}";
        }
    }
    internal class CurrencyRate
    {
        public int Id { get; set; }
        public required string ExchangeCurrency { get; set; }
        public required float ExchangeRate { get; set; }
        public int BaseCurrencyId { get; set; }
        public BaseCurrency BaseCurrency { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Currency: {ExchangeCurrency}, Rate: {ExchangeRate}, BaseCurrencyId: {BaseCurrencyId}";
        }

    }
```
Tabela BaseCurrency przechowuje informacje o podstawowej walucie (w przypadku naszego API zawsze jest to USD) oraz o dacie (w postaci timestampu) z której brane były kursy poszczególnych walut.
Tabela CurrencyRate przechowywała skrót docelowej waluty oraz jej kurs. Tabele były połączone za pomocą relacji, tak aby dane dało się wykorzystać np. do przeliczenia walut w danym dniu.
Zapisywanie do bazy zostało zaimplementowane w funkcji GetData(string Date), która jako parametr przyjmowała datę, z której miałby być brane kursy walut. Poniżej pełna metoda GetData:
``` C#
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
```
Aby zapobiec przechowywaniu tych samych kilka razy, została dodana walidacja, która sprawdza czy pobrane dane nie są już zapisane w bazie. 
Na koniec została napisana prosta aplikacja konsolowa, która pozwalała użtkownikowi na: 
1. Wyświetlenie wskazanej tabeli z bazy
2. Dodanie rekordu do wskazanej tabeli
3. Pobranie danych z API i zapisanie ich do bazy - wymaga wskazania daty, z której mają zostać wzięte kursy walut.
































