using NetCoreTester.Bll.Interfaces;
using System;
using NetCoreTester.Model;

namespace NetCoreTester.Bll
{
    public class Users : IUser
    {
        NetCoreTester.Dal.Interfaces.IUser _dal;

        public Users(NetCoreTester.Dal.Interfaces.IUser dal)
        {
            _dal = dal;
        }

        public UserInfo GetUser(int userId)
        {
            return _dal.GetUser(userId);
        }
    }
}
