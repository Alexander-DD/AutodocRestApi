using Microsoft.EntityFrameworkCore;

using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data
{
    public class MSSqlDbContext : DbContext
    {
        public MSSqlDbContext(DbContextOptions<MSSqlDbContext> options) : base(options)
        {
        }

        public DbSet<FileTask> FileTasks { get; set; }
        public DbSet<FileAttachment> FileAttachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FileTask>()
                .Property(f => f.Id)
                .HasColumnName("id")
                .IsRequired();

            modelBuilder.Entity<FileAttachment>()
                .Property(f => f.Id)
                .HasColumnName("id")
                .IsRequired();

            // Добавляем начальные данные для FileTask
            modelBuilder.Entity<FileTask>().HasData(
                new FileTask
                {
                    Id = 1,
                    Name = "Initial Task",
                    Date = DateTime.UtcNow,
                    IsCompleted = false
                },
                new FileTask
                {
                    Id = 2,
                    Name = "Another Task",
                    Date = DateTime.UtcNow.AddDays(-1),
                    IsCompleted = true
                }
            );

            // Добавляем начальные данные для FileAttachment
            modelBuilder.Entity<FileAttachment>().HasData(
                new FileAttachment
                {
                    Id = 1,
                    FileName = "example1.txt",
                    FilePath = "file1",
                    FileSize = 1024,
                    FileTaskId = 1
                },
                new FileAttachment
                {
                    Id = 2,
                    FileName = "example2.pdf",
                    FilePath = "file2",
                    FileSize = 2048,
                    FileTaskId = 1
                },
                new FileAttachment
                {
                    Id = 3,
                    FileName = "example3.docx",
                    FilePath = "file3",
                    FileSize = 512,
                    FileTaskId = 2
                }
            );
        }
    }
}
