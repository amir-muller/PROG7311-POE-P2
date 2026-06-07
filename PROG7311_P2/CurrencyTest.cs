using Xunit;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Json;
using Web_API.Services;

namespace PROG7311_P2;

public class CurrencyTest
{
    [Fact]
    public async Task ConvertUsdToZar_ReturnsCorrectCalculatedProduct()
    {
        //arrange 
        decimal usdAmount = 50;

        var mockResponsePayload = new
        {
            result = "success",
            conversion_rate = 16.00m 
        };

        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(mockResponsePayload)
            });

        var mockHttpClient = new HttpClient(mockMessageHandler.Object);
        var currencyService = new CurrencyService(mockHttpClient);

        //act
        decimal zarAmount = await currencyService.ConvertUsdToZar(usdAmount);

        // assert
        Assert.Equal(800, zarAmount);
    }
}