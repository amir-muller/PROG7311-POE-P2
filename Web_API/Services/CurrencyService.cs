using System.Net.Http;
using System.Text.Json;

namespace Web_API.Services;

public class CurrencyService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string ApiKey = "f9ddef59cd5776e9128ab203";

    public CurrencyService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<decimal> ConvertUsdToZar(decimal usdAmount)
    {
        string url = $"https://v6.exchangerate-api.com/v6/{ApiKey.Trim()}/pair/USD/ZAR";

        try
        {
            using var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"API Payload Received: {json}");

                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (root.TryGetProperty("result", out var resultProp) && resultProp.GetString() == "success")
                {
                    if (root.TryGetProperty("conversion_rate", out var rateProp))
                    {
                        decimal rate = rateProp.GetDecimal();
                        return usdAmount * rate;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching exchange rate: {ex.Message}");
        }

        return 0;
    }
}
