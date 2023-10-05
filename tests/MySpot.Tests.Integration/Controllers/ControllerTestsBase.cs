using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Tests.Integration.Controllers
{
    public abstract class ControllerTestsBase
    {
        protected HttpClient Client { get; }
        public ControllerTestsBase()
        {
            var app =  new MySpotTestApp();
            Client = app.Client;
        }
    }
}
