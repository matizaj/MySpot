using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MySpot.Tests.Integration.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public async Task get_200_ok_when_hit_base_endpoint()
        {
            var app = new MySpotTestApp();
            var response = await app.Client.GetAsync("/api");
            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            content.ShouldBe("MySpot API");

        }
    }
}
