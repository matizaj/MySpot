using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace MySpot.Tests.Integration
{
    internal class MySpotTestApp : WebApplicationFactory<Program>
    {
        public HttpClient Client { get; }
        public MySpotTestApp(Action<IServiceCollection> services)
        {
            Client = base.WithWebHostBuilder(builder =>
            {
                if(services != null)
                {
                    builder.ConfigureServices(services);
                }
                builder.UseEnvironment("test");
            }).CreateClient();
        }
    }
}
