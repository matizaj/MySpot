using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MySpot.Tests.Integration
{
    internal class MySpotTestApp : WebApplicationFactory<Program>
    {
        public HttpClient Client { get; }
        public MySpotTestApp()
        {
            Client = base.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("test");
            }).CreateClient();
        }
    }
}
