using FinalProjectAPBD.Models;
using Newtonsoft.Json;

namespace FinalProjectAPBD.Helpers;

public class CurrencyHelper
{
    public static async Task<decimal> ConvertCurrency(decimal amount, string fromCurrency, string toCurrency, IConfiguration configuration)
    {
        var apiKey = configuration["CurrencyApiKey"];
        var url = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/{fromCurrency}";

        using (var client = new HttpClient())
        {
            var response = await client.GetStringAsync(url);
            ExchangeRatesModel exchangeRates = JsonConvert.DeserializeObject<ExchangeRatesModel>(response);
            exchangeRates.conversion_rates.TryGetValue(toCurrency, out decimal rate);
            return amount * rate;
        }
    }
}