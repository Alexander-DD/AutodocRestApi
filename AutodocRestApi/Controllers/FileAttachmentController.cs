using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileAttachmentsController : ControllerBase
    {
        private readonly IDBRepository<FileAttachment> _fileRepository;
        private readonly IDBRepository<FileTask> _taskRepository;
        private readonly IFileStorageRepository _fileStorageRepository;

        public FileAttachmentsController(
            IDBRepository<FileAttachment> fileRepository,
            IDBRepository<FileTask> taskRepository,
            IFileStorageRepository fileStorageRepository)
        {
            _fileRepository = fileRepository;
            _taskRepository = taskRepository;
            _fileStorageRepository = fileStorageRepository;
        }

        /// <summary>
        /// Загрузка файла для задачи.
        /// </summary>
        [HttpPost("upload/{fileTaskId}")]
        public async Task<IActionResult> UploadFile(int fileTaskId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Не загружен файл");

            var task = await _taskRepository.GetByIdAsync(fileTaskId);
            if (task == null)
                return NotFound("Task не найдена");

            var fileId = await _fileStorageRepository.UploadFileAsync(file);

            var fileAttachment = new FileAttachment
            {
                FileName = file.FileName,
                FilePath = fileId, // Сохраняем fileId, связанный с NoSQL-хранилищем
                FileSize = file.Length,
                FileTaskId = fileTaskId
            };

            await _fileRepository.AddAsync(fileAttachment);
            await _fileRepository.SaveChangesAsync();

            return Ok(new { Message = "Файл успешно загружен", fileAttachment.Id, fileAttachment.FileName });
        }

        [HttpGet("files/{fileId}")]
        public async Task<IActionResult> DownloadFile(string fileId) // fileId — строка
        {
            var stream = await _fileStorageRepository.DownloadFileAsync(fileId);
            if (stream == null)
                return NotFound("Файл не найден");

            return File(stream, "application/octet-stream");
        }

        [HttpDelete("files/{fileId}")]
        public async Task<IActionResult> DeleteFile(string fileId)
        {
            var file = await _fileRepository.FindAsync(f => f.FilePath == fileId);
            if (!file.Any())
                return NotFound("Файл не найден");

            await _fileStorageRepository.DeleteFileAsync(fileId);

            _fileRepository.Remove(file.First());
            await _fileRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
