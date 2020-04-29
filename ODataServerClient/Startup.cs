using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Server.Data;

namespace ODataServerClient
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services
        .AddEntityFrameworkNpgsql()
        .AddDbContext<AppDbContext>();

      services.AddControllers(o => o.EnableEndpointRouting = false);

      services.AddOData();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthorization();

      app.UseMvc(routeBuilder =>
      {
        routeBuilder
        .Select()
        .Filter()
        .Expand()
        .Count()
        .OrderBy();

        routeBuilder
        .MapODataServiceRoute("odata", "odata", createModel());

        static IEdmModel createModel()
        {
          var odataBuilder = new ODataConventionModelBuilder();

          var company = odataBuilder
          .EntitySet<Company>("Companies")
          .EntityType;

          //odataBuilder.ComplexType<Contact>();
          //odataBuilder.ComplexType<Phone>();

          //company
          //.CollectionProperty(c => c.Contacts);

          return odataBuilder.GetEdmModel();
        }
      });
    }
  }
}
