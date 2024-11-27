using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    /// <summary>
    /// Представляет файл, связанный с задачей.
    /// </summary>
    public class FileAttachment
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        public long FileSize { get; set; }
        public string FilePath { get; set; }

        /// <summary>
        /// ID связанной задачи.
        /// </summary>
        public int FileTaskId { get; set; }

        /// <summary>
        /// Связанная задача.
        /// </summary>
        public FileTask FileTask { get; set; }
    }
}
