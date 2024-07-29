using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace vue_practice_api.Controllers
{
    [Authorize(Roles = "User")]
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

        // 受保護的端點
        [Authorize]
        [HttpGet("protected")]
        public ActionResult<string> GetProtected()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return $"This is protected data for user {userId}";
        }
    }
}
