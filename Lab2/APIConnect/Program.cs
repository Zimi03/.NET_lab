using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace APIConnect
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CurrencyExchangeOffice ceo = new CurrencyExchangeOffice();
            TestAPI t = new TestAPI();

            Console.WriteLine("Wybierz funkcję");
            Console.WriteLine("1. Wyswietl dane");
            Console.WriteLine("2. Dodaj rekord");
            Console.WriteLine("3. Pobierz dane z API");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.WriteLine("Podaj tabele");
                    Console.WriteLine("1. BaseCurrency");
                    Console.WriteLine("2. CurrencyRate");
                    int option_2 = Convert.ToInt32(Console.ReadLine());
                    switch (option_2) 
                    {
                        case 1:
                            var baseCurrencies = ceo.BaseCurrencies.ToList();

                            foreach (var item in baseCurrencies)
                            {
                                Console.WriteLine(item);
                            }
                            break;
                        case 2:
                            var Exchange = ceo.CurrenciesRates.ToList();
                            foreach (var item in Exchange)
                            {
                                Console.WriteLine(item);
                            }
                            break;
                    }

                    break;
                case 2:
                    Console.WriteLine("Podaj tabele");
                    Console.WriteLine("1. BaseCurrency");
                    Console.WriteLine("2. urrencyRate");
                    int option_3 = Convert.ToInt32(Console.ReadLine());
                    switch (option_3)
                    {
                        case 1:
                            Console.WriteLine("Podaj podstawowa walute");
                            string baseCurr = Console.ReadLine();
                            Console.WriteLine("Podaj timestamp (opcjonalne)");
                            string Timestamp = Console.ReadLine();
                            long TimestampLong = long.Parse(Timestamp);
                            ceo.BaseCurrencies.Add(new BaseCurrency() { Currency = baseCurr, timestamp = TimestampLong});
                            break;
                        case 2:
                            Console.WriteLine("Podaj walute docelowa");
                            string curr = Console.ReadLine();
                            Console.WriteLine("Podaj wartosc");
                            float rate = float.Parse(Console.ReadLine());
                            Console.WriteLine("Id podstawowej tabeli");
                            int foreignKey = int.Parse(Console.ReadLine());
                            ceo.CurrenciesRates.Add(new CurrencyRate() { ExchangeCurrency = curr, ExchangeRate = rate, BaseCurrencyId = foreignKey });

                            break;
                    }
                    break;
                case 3:
                    Console.WriteLine("Podaj date w formacie (YYYY-MM-DD)");
                    string date = Console.ReadLine();
                    t.GetData(date).Wait();
                    break;
            }

            
            //TestAPI t = new TestAPI();
            //t.GetData().Wait();
            //CurrencyExchangeOffice ceo = new CurrencyExchangeOffice();
            string currency = Console.ReadLine();
            
            ceo.SaveChanges();
            
            
        }
    }
}
