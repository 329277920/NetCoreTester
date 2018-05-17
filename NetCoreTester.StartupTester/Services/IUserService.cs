using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTester.StartupTester.Services
{
    /// <summary>
    /// 定义 UserService 服务接口
    /// </summary>
    public interface IUserService
    {
        bool CheckLogin(string token);
    }
}
