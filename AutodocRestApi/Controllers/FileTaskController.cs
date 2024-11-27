using Microsoft.AspNetCore.Mvc;

using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileTaskController : ControllerBase
    {
        private readonly IDBRepository<FileTask> _repository;

        public FileTaskController(IDBRepository<FileTask> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetFileTasks()
        {
            var fileTasks = await _repository.GetAllAsync();
            return Ok(fileTasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFileTask([FromBody] FileTask fileTask)
        {
            await _repository.AddAsync(fileTask);
            await _repository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFileTaskById), new { id = fileTask.Id }, fileTask);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFileTaskById(int id)
        {
            var fileTask = await _repository.GetByIdAsync(id);
            if (fileTask == null) return NotFound();
            return Ok(fileTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFileTask(int id, [FromBody] FileTask fileTask)
        {
            if (id != fileTask.Id) return BadRequest();

            _repository.Update(fileTask);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFileTask(int id)
        {
            var fileTask = await _repository.GetByIdAsync(id);
            if (fileTask == null) return NotFound();

            _repository.Remove(fileTask);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

    }
}
