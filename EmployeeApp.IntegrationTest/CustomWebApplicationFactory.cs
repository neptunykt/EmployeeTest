using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace EmployeeApp.IntegrationTest
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder) =>
            builder.ConfigureAppConfiguration(
                configBuilder =>
                {
                    var defaultConfig = new Dictionary<string, string>
                    {
                        {"DatabaseEngine:Option", "InMemory"}
                    };
                    configBuilder.AddInMemoryCollection(defaultConfig);
                });
        
    }
}