using Microsoft.AspNetCore.Http;
using NetCoreTester.StartupTester.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTester.StartupTester.Middlewares
{
    public class Middleware2
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _users;      

        public Middleware2(RequestDelegate next, IUserService users)
        {
            _next = next;
            _users = users;            
        }

        public async Task Invoke(HttpContext context, ITraceService trace)
        {
            trace.Debug($"检测用户登录:{_users.CheckLogin("cnf")}");

            context.Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            await context.Response.WriteAsync("请求成功。");

            // await _next.Invoke(context);
        }
    }
}
