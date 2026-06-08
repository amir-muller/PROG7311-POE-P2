using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Web_API.Data;
using Web_API.Models.ServiceRequest;
using Xunit;

namespace Web_API.IntegrationTests;

public class ServiceRequestsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public ServiceRequestsControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetServiceRequests_ConvertsCostFromUsdToZarOnTheFly()
    {
        // Arrange: Seed data into the test database directly
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

            // Clear existing items
            db.ServiceRequests.RemoveRange(db.ServiceRequests);

            db.ServiceRequests.Add(new ServiceRequest
            {
                ContractId = 101,
                Description = "Database Migration & Setup",
                Cost = 100.00m, 
                Status = "Pending"
            });
            await db.SaveChangesAsync();
        }

        // Act
        var response = await _client.GetAsync("api/servicerequests");

        // Assert
        response.EnsureSuccessStatusCode();
        var requests = await response.Content.ReadFromJsonAsync<List<ServiceRequest>>();

        Assert.NotNull(requests);
        Assert.Single(requests);

        var targetRequest = requests.First();

        Assert.True(targetRequest.Cost > 100.00m, $"Expected cost conversion processing, found: {targetRequest.Cost}");
    }
}