using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTester.StartupTester.Services
{
    /// <summary>
    /// IUserService 默认实现
    /// </summary>
    public class UserService : IUserService
    {
        public bool CheckLogin(string token)
        {
            return !string.IsNullOrEmpty(token);
        }
    }
}
