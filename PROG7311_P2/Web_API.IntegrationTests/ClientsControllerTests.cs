using System.Net;
using System.Net.Http.Json;
using Web_API.IntegrationTests;
using Web_API.Models.Client;
using Xunit;

namespace Web_API.IntegrationTests;

public class ClientsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public ClientsControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateClient_And_GetClients_ReturnsSuccessAndCorrectData()
    {
        // Arrange
        var newClient = new Client
        {
            Name = "Amir Tech Solutions",
            contactDetails = "011-555-0123",
            Region = "Gauteng"
        };

        // Act
        var postResponse = await _client.PostAsJsonAsync("api/clients", newClient);

        // Assert
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
        var createdClient = await postResponse.Content.ReadFromJsonAsync<Client>();
        Assert.NotNull(createdClient);
        Assert.True(createdClient.ClientId > 0);

        // Act 
        var getResponse = await _client.GetAsync("api/clients");

        // Assert
        getResponse.EnsureSuccessStatusCode();
        var clientsList = await getResponse.Content.ReadFromJsonAsync<List<Client>>();

        Assert.NotNull(clientsList);
        Assert.Contains(clientsList, c => c.Name == "Amir Tech Solutions");
    }

    [Fact]
    public async Task GetClient_ReturnsNotFound_ForInvalidId()
    {
        // Act
        var response = await _client.GetAsync("api/clients/9999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}