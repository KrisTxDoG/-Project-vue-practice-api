using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace vue_practice_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // GET: api/home
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/home/{id}
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value 測試資料喔";
        }

        // POST: api/home
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/home/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/home/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
