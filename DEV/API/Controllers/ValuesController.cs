using Business;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Produces("application/json")]
    [EnableCors("any")] //设置跨域处理的代理
    [Route("api/[controller]/[action]")]
    public class ValuesController : Controller
    {
        private readonly ITestServices _iTestServices;
        //private readonly ITimeJobService _timeJobService;

        public ValuesController(ITestServices iTestServices
            )
        {
            _iTestServices = iTestServices;
            //_timeJobService = timeJobService;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            if (_iTestServices.Test() == null)
            {
                _iTestServices.TestAdd();
            }
            return new string[] { "value1", "TestServices:" + _iTestServices.Test().Name};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
