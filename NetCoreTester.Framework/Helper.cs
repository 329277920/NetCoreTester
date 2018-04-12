using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTester.Framework
{
    public class Helper : IHelper
    {        
        public int Idx = 0;

        public string ToBase64(string value)
        {
            return $"{++Idx} : {Convert.ToBase64String(Encoding.UTF8.GetBytes(value))}";
        }
    }
}
