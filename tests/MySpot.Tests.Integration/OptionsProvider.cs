using Microsoft.Extensions.Configuration;
using MySpot.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Tests.Integration
{
    internal class OptionsProvider
    {
        private readonly IConfiguration _configuration;

        public OptionsProvider(IConfiguration configuration)
        {
            _configuration = GetConfiguration();
        }

        public T Get<T>(string sectionName) where T:class, new()
        {
            return _configuration.GetOptions<T>(sectionName);
        }

        private static IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.test.json", true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
