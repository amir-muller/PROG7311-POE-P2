using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PROG7311_POE_P2.Services;
using Moq;
using PROG7311_POE_P2.Controllers;
using PROG7311_POE_P2.Data;
using PROG7311_POE_P2.Models.DashboardViewModel;
using Xunit;

namespace PROG7311_P2;

public class HomeControllerTests
{
    private ApplicationDBContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDBContext(options);
    }

    [Fact]
    public void Index_ReturnsViewWithDashboardViewModel()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var mockLogger = new Mock<ILogger<HomeController>>();
        var mockService = new Mock<CurrencyService>(new HttpClient());
        var controller = new HomeController(mockLogger.Object, context, mockService.Object);

        // Act
        var result = controller.Index() as ViewResult;

        // Assert
        Assert.IsType<DashboardViewModel>(result.Model);
    }
}