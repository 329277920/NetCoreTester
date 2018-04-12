using NetCoreTester.Dal.Interfaces;
using System;
using NetCoreTester.Model;

namespace NetCoreTester.Dal
{
    public class Users : IUser
    {
        private NetCoreTester.Framework.IHelper _helper;

        public Users(NetCoreTester.Framework.IHelper helper)
        {
            _helper = helper;
        }

        public UserInfo GetUser(int userId)
        {
            return new UserInfo() { UserId = userId, UserName = _helper.ToBase64(userId.ToString()) };
        }        
    }
}
