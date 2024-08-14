using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vue_practice_api.DataBase;

namespace vue_practice_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackstageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BackstageController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await _context.Items.ToListAsync();
            return Ok(items); // 返回 200 狀態碼和資料
        }
    }
}
