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
        public async Task<IActionResult> GetItems(int page = 1, int pageSize = 10, string search= "")
        {
            // 確保頁碼和頁面大小為有效值
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 1 ? 10 : pageSize;

            // 準備查詢
            var query = _context.Items.AsQueryable();

            // 如果提供了搜尋字串, 則進行篩選
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(item => item.Name.Contains(search));
            }

            // 獲取總的項目數量
            var totalItems = await query.CountAsync();

            // 根據頁碼和頁面大小獲取數據
            var items = await query
                .OrderBy(x => x.CreatedDate)
                .Skip((page -1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return Ok(new
            {
                Items = items,
                TotalItems = totalItems
            }); 
        }

        [HttpPost("CreateItem")]
        public async Task<IActionResult> CreateItem ([FromBody] Item item)
        {
            if(ModelState.IsValid)
            {
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
