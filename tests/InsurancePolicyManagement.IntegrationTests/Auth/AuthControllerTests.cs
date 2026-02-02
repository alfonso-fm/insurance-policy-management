using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace InsurancePolicyManagement.IntegrationTests;

public class AuthControllerTests : IClassFixture<ApiFactory>
{
  private readonly HttpClient _client;

  public AuthControllerTests(ApiFactory factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task Login_WithValidAdmin_ReturnsToken()
  {
    var response = await _client.PostAsJsonAsync("/api/auth/login", new
    {
      Email = "admin@insurance.com",
      Password = "Admin123!"
    });

    response.StatusCode.Should().Be(HttpStatusCode.OK);
  }
}