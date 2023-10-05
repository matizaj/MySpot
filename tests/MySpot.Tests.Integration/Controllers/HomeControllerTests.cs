using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MySpot.Tests.Integration.Controllers
{
    public class HomeControllerTests : ControllerTestsBase
    {
        public HomeControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
        {
        }

        [Fact]
        public async Task get_200_ok_when_hit_base_endpoint()
        {
           
            var response = await Client.GetAsync("/api");
            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            content.ShouldBe("MySpot API");

        }
    }
}
