using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using vue_practice_api.DataBase;

namespace vue_practice_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public ItemsController(AppDbContext context)
        {
             _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }
    }
}
