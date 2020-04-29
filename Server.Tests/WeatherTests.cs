using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using ODataServerClient;
using Xunit;

namespace Server.Tests
{
  public class WeatherTests : TestFixtureBase
  {
    public WeatherTests(WebApplicationFactory<Startup> factory) : base(factory)
    {
    }

    [Fact]
    public async Task ShouldGetForecast()
    {
      // Arrange 
      var client = Factory.CreateClient();

      // Act
      var result = await client.GetAsync("weatherforecast");

      // Assert
      result.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
    }

  }
}
