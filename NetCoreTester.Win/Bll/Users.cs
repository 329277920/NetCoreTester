using NetCoreTester.Win.Framework;
using System;
using System.Collections.Generic;
 
using System.Text;
using Unity.Attributes;

namespace NetCoreTester.Win.Bll
{
    public class Users 
    {
        private IStringHelper _stringHelper;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="stringHelper"></param>
        public Users(IStringHelper stringHelper)
        {
            _stringHelper = stringHelper;
        }

        /// <summary>
        /// 属性注入
        /// </summary>
        [Dependency]
        public IStringHelper StringHelper { get; set; }

        public string FormatUserName(string userName)
        {
            return _stringHelper.ToBase64(userName) + ":" + StringHelper.ToBase64(userName);
        }

        [InjectionMethod]
        public string NewFormatUserName(IStringHelper stringHelper, string userName)
        {
            return stringHelper.ToBase64(userName);
        }
    }
}
