using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using ODataServerClient;
using Xunit;

namespace Server.Tests
{
  public class CompanyTests : TestFixtureBase
  {
    public CompanyTests(WebApplicationFactory<Startup> factory)
    : base(factory)
    {
      //TODO doesn't work
      //Factory.ClientOptions.BaseAddress = new Uri(Factory.ClientOptions.BaseAddress, "odata");
    }

    [Fact]
    public async Task ShouldCreateCompany()
    {
      // Arrange 
      var client = Factory.CreateClient();

      // Act 
      var response = await client.GetAsync("odata/companies");

      // Assert
      response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
    }


  }
}
