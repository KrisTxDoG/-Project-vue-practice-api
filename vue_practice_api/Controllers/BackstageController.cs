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

        [HttpPost("CreateItem")]
        public async Task<IActionResult> CreateItem ([FromBody] Item item)
        {
            if(ModelState.IsValid)
            {
                // 確保 id 為 0，這樣 EF 會將它視為新的項目並讓資料庫自動生成 id
                item.Id = 0;

                _context.Items.Add(item);
                await _context.SaveChangesAsync();

                return Ok(item);
            }

            return BadRequest(ModelState);
        }
    }
}
