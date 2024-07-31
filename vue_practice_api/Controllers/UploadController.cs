using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // 確保引入這個命名空間來使用 IFormFile
using System.IO;
using System.Threading.Tasks;

namespace vue_practice_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        public UploadController()
        {
            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var filePath = Path.Combine(_uploadFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { FilePath = filePath });
        }
    }
}
