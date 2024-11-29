using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using GameBookASP.Data;
using GameBookASP.Models.InputModels;
using GameBookASP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace GameBookASP.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly AppDbContext _context;

        public UploadFileController(IWebHostEnvironment environment, AppDbContext context)
        {
            _environment = environment;
            _context = context;
        }

        // GET ONE FILE
        [HttpGet("download/{fileId}")]
        public async Task<IActionResult> GetFile(Guid fileId)
        {
            var file = await _context.Files.FindAsync(fileId);
            if (file == null)
            {
                return NotFound();
            }
            if (!System.IO.File.Exists(file.FilePath))
            {
                return NotFound("File not found.");
            }

            return PhysicalFile(file.FilePath, file.FileType, file.FileName);
        }

        // get list of all files
        [HttpGet("allfiles")]
        public async Task<IActionResult> GetFiles()
        {
            var files = await _context.Files.ToListAsync();
            return Ok(files);
        }

        // POST
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFiles([FromForm] InputFilesUpload model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return Unauthorized("User not found"); // neni prihlasenej
            }

            var userRoles = await _context.UserRoles.Where(ur => ur.UserId == userId).ToListAsync();
            var userRoleIds = userRoles.Select(ur => ur.RoleId).ToList();
            var adminRoleId = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
            
            if (!userRoleIds.Contains(adminRoleId!.Id))
            {
                return Unauthorized("User is not an admin");
            }

            if (model.Files.Count == 0)
            {
                return NotFound("No file uploaded.");
            }

            IList<dynamic> newFiles = new List<dynamic>();

            foreach (var file in model.Files)
            {
                var fileName = Path.GetFileName(file.FileName);
                var imageContentTypes = new List<string> {
                    "image/jpeg",
                    "image/png",
                    "image/gif",
                    "image/webp",
                    "image/avif"
                };

                if (!imageContentTypes.Contains(file.ContentType))
                {
                    return NotFound("Too large");
                }
                if (file.Length > 12 * 1024 * 1024)
                {
                    return NotFound("Too large");
                    continue;
                }

                var imgId = Guid.NewGuid();
                var filePath = Path.Combine(_environment.WebRootPath, "Uploads", imgId.ToString() + Path.GetExtension(fileName));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                        await file.CopyToAsync(stream);
                    }
                    catch (Exception e)
                    {
                        newFiles.Add(new
                        {
                            FileName = fileName,
                            FileType = file.ContentType,
                            FileSize = file.Length,
                            Status = e.Message
                        });
                        continue;
                    }
                }
                newFiles.Add(new
                {
                    FileName = fileName,
                    FileType = file.ContentType,
                    FileSize = file.Length,
                    Status = "Success",
                });

                var newFile = new Models.File
                {
                    Id = imgId,
                    FileName = fileName,
                    FilePath = filePath,
                    FileType = file.ContentType,
                    UploadedAt = DateTime.Now
                };
                _context.Files.Add(newFile);
            }

            await _context.SaveChangesAsync();
            return Ok(newFiles);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Policy = "Author")]
        public async Task<ActionResult> DeleteFile(Guid id)
        {
            var file = await _context.Files.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
            return Ok(new { id = file.Id });
        }
    }
}