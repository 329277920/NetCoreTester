using NetCoreTester.Model;
using System;

namespace NetCoreTester.Dal.Interfaces
{
    public interface IUser
    {
        UserInfo GetUser(int userId);
    }
}
