using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            var items = await _context.Items
                .OrderBy(x => x.CreatedDate)
                .ToListAsync();
            return Ok(items); // 返回 200 狀態碼和資料
        }

        [HttpPost("CreateItem")]
        public async Task<IActionResult> CreateItem ([FromBody] Item item)
        {
            if(ModelState.IsValid)
            {
                // 確保 id 為 0，這樣 EF 會將它視為新的項目並讓資料庫自動生成 id
                item.Id =  Guid.NewGuid();
                item.CreatedDate = DateTime.Now;

                _context.Items.Add(item);
                await _context.SaveChangesAsync();

                return Ok(item);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteItem/{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            if(item == null)
            {
                return NotFound($"Item with ID {id} not found");
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return Ok($"Item with ID {id} deleted");
        }

        [HttpPut("UpdateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] Item item)
        {
            if (ModelState.IsValid)
            {
                var existingItem = await _context.Items.FindAsync(item.Id);

                if (existingItem == null)
                {
                    return NotFound();
                }

                // 更新資料
                existingItem.Name = item.Name;
                existingItem.Phone = item.Phone;

                await _context.SaveChangesAsync();
                return Ok(existingItem);
            }
            return BadRequest(ModelState);
          
        }
    }
}
