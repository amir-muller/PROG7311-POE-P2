using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using PROG7311_POE_P2.Controllers; 
using Web_API.Models.DashboardViewModel;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace PROG7311_P2;

public class HomeControllerTests
{
    [Fact]
    public async Task Index_ReturnsViewWithDashboardViewModel()
    {
        //arrange
        var mockLogger = new Mock<ILogger<HomeController>>();
        var mockHttpClientFactory = new Mock<IHttpClientFactory>();

        //sim api response
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(new List<object>()) 
            });

        var client = new HttpClient(mockHttpMessageHandler.Object)
        {
            BaseAddress = new Uri("http://localhost/")
        };

        mockHttpClientFactory.Setup(_ => _.CreateClient("MyWebAPI")).Returns(client);

        var controller = new HomeController(mockLogger.Object, mockHttpClientFactory.Object);

        // Act
        var result = await controller.Index() as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DashboardViewModel>(result.Model);
    }
}