using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTester.StartupTester.Middlewares
{
    public class MiddlewareConfiguration
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public MiddlewareConfiguration(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, IOptions<AppSettings> settings)
        {
            var config = _configuration.Get<AppSettings>();
            await context.Response.WriteAsync($"Version:{config.Version},Instance:{config.InstanceCount}。");
            // await context.Response.WriteAsync(settings.Value.Version);           
        }
    }
}
