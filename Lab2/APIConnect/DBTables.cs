using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIConnect
{
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
    internal class CurrencyExchangeOffice : DbContext
    {
        public DbSet<BaseCurrency> BaseCurrencies { get; set; }
        public DbSet<CurrencyRate> CurrenciesRates { get; set; }
        public CurrencyExchangeOffice()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@"Data source = CurrExOff.db");
        }
    }
}
