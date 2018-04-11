using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreTester.Model;

namespace NetCoreTester.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {       
        /// <summary>
        /// 注入的业务层的类
        /// </summary>
        private NetCoreTester.Bll.Interfaces.IUser _bll;

        public ValuesController(NetCoreTester.Bll.Interfaces.IUser bll)
        {
            _bll = bll;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public UserInfo Get(int id)
        {
            var user = _bll.GetUser(id);
          
            return user;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
