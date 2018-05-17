using NetCoreTester.Win.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Attributes;

namespace NetCoreTester.Win.Bll
{
    public class Products
    {
        /// <summary>
        /// 属性注入
        /// </summary>
        [Dependency]
        public IStringHelper StringHelper { get; set; }

        /// <summary>
        /// 方法注入
        /// </summary>    
        /// <param name="productName"></param>
        /// <returns></returns>
        public string FormatProductName(string productName)
        {
            return StringHelper.ToBase64(productName);
        }
    }
}
