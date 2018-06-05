using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTester.LogTest.Loggers.ExceptionLess
{
    /// <summary>
    /// ExceptionLess 日志实体接口，定义了 ExceptionLess 一些日志特性
    /// </summary>
    public interface IExceptionLessLogEntity
    {        
        string[] Tags { get; set; }

        Exceptionless.Models.Data.UserInfo User { get; set; }

        object Data { get; set; }
    }
}
