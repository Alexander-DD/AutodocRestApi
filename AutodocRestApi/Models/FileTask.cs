using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    /// <summary>
    /// Представляет задачу в системе.
    /// </summary>
    public class FileTask
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = String.Empty;

        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Связанные файлы.
        /// </summary>
        public List<FileAttachment> FileAttachments { get; set; } = new();
    }
}
