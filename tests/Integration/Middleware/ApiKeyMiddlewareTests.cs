namespace CardActionServiceTests.Integration.Middleware;

public class ApiKeyMiddlewareTests(CustomWebAppFactory factory) : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Request_WithValidApiKey_ReturnsSuccess()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Card?UserId=ignored&CardNumber=4111111111111111");
        request.Headers.Add("X-API-KEY", "test-key");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    [Fact]
    public async Task Request_WithoutApiKey_ReturnsUnauthorized()
    {
        var response = await _client.GetAsync("/api/v1/Card?UserId=ignored&CardNumber=Card11");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Request_WithInvalidApiKey_ReturnsUnauthorized()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Card?UserId=ignored&CardNumber=Card11");
        request.Headers.Add("X-API-KEY", "wrong-key");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}