using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // 確保引入這個命名空間來使用 IFormFile
using System.IO;
using System.Threading.Tasks;
using vue_practice_api.DataBase;
using vue_practice_api.DataBase.Dto;

namespace vue_practice_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        private readonly AppDbContext _context;

        public UploadController(AppDbContext context)
        {
            _context = context;
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

                // 存入資料庫
                var fileUpload = new FileUploads
                {
                    Name = file.FileName,
                    Description = "some description",
                };

                _context.FileUploads.Add(fileUpload);
                await _context.SaveChangesAsync();

                return Ok(new { FilePath = filePath });
            }
        }

        [HttpGet("download/{fileName}")]
        public IActionResult Download(string fileName)
        {
            var filePath = Path.Combine(_uploadFolder, fileName);
            if(!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found");
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;

            return File(memory, "application/octet-stream", fileName);
        }

        [HttpPost("UploadFileBase64")]
        public async Task<IActionResult> UploadFileBase64([FromBody] FileUploadDto fileUploadDto)
        {
            var base64String = fileUploadDto.FileDate;

            // 將 Base64 字串轉換回 byte[]
            var fileBytes = Convert.FromBase64String(base64String);

            // 將這些 bytes 儲存到資料庫中
            var DbFile = new FileUploadBase64
            {
                Name = "uploadedFile",     // 儲存的名稱
                Data = fileBytes                 // 儲存到資料庫
            };

            _context.FileUploadBase64.Add(DbFile);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
