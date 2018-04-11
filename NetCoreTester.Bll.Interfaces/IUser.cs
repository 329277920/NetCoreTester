using NetCoreTester.Model;
using System;

namespace NetCoreTester.Bll.Interfaces
{
    public interface IUser
    {
        UserInfo GetUser(int userId);
    }
}
