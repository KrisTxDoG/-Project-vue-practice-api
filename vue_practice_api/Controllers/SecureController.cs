using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace vue_practice_api.Controllers
{
    //[Authorize(Roles = "Admin")]
    //[Route("secure")]
    public class SecureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetSecureData()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(new { Message = "This is a secure data", UserId = userId });
        }
    }
}
