using MySpot.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MySpot.Tests.Integration.Controllers
{
    [Collection("api")]
    public abstract class ControllerTestsBase : IClassFixture<OptionsProvider>
    {
        protected HttpClient Client { get; }
        public ControllerTestsBase(OptionsProvider optionsProvider)
        {
            var app =  new MySpotTestApp();
            Client = app.Client;
        }
    }
}
