using System;
using Microsoft.AspNetCore.Mvc.Testing;
using ODataServerClient;
using Xunit;

namespace Server.Tests
{
  public abstract class TestFixtureBase : IClassFixture<WebApplicationFactory<Startup>>
  {
    protected WebApplicationFactory<Startup> Factory { get; }
    protected TestFixtureBase(WebApplicationFactory<Startup> factory)
    {
      Factory = factory;
      //Factory.ClientOptions.BaseAddress = new Uri(Factory.ClientOptions.BaseAddress, "odata");
    }
  }
}
