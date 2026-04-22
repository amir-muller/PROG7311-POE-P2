using System.Net.Http;
using System.Text.Json;

namespace PROG7311_POE_P2.Services;

public class CurrencyService
{

    private readonly HttpClient _httpClient;
    private const string ApiKey = "f9ddef59cd5776e9128ab203";

    public CurrencyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> ConvertUsdToZar(decimal usdAmount)
    {
        // my API key
        string url = $"https://v6.exchangerate-api.com/v6/{ApiKey}/pair/USD/ZAR";

        try
        {
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (root.GetProperty("result").GetString() == "success")
                {
                    decimal rate = root.GetProperty("conversion_rate").GetDecimal();
                    return usdAmount * rate;
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"Error fetching exchange rate: {ex.Message}");
        }

        return 0;

        //var response = await _httpClient.GetAsync(url);
        //if (response.IsSuccessStatusCode)
        //{
        //    var json = await response.Content.ReadAsStringAsync();
        //    using var doc = JsonDocument.Parse(json);

        //    decimal rate = doc.RootElement.GetProperty("conversion_rate").GetDecimal();

        //    return usdAmount * rate;
    }



}
