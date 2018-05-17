using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTester.Win.Framework
{
    public class StringHelper : IStringHelper
    {
        public string ToBase64(string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }
    }
}
