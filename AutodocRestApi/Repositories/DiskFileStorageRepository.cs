namespace TaskManagementAPI.Repositories
{
    public class DiskFileStorageRepository : IFileStorageRepository
    {
        private readonly string _storagePath;

        public DiskFileStorageRepository(IConfiguration configuration)
        {
            _storagePath = configuration["DiskStorage:BasePath"] ?? "Uploads";

            // Убедимся, что директория существует
            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        /// <inheritdoc />
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var fileId = Guid.NewGuid().ToString(); // Генерируем уникальный ID файла
            var filePath = Path.Combine(_storagePath, fileId + Path.GetExtension(file.FileName));

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileId; // Возвращаем уникальный ID файла
        }

        /// <inheritdoc />
        public async Task<Stream?> DownloadFileAsync(string id)
        {
            var files = Directory.GetFiles(_storagePath, id + ".*");

            if (files.Length == 0)
                return null; // Файл не найден

            var filePath = files[0];
            var memoryStream = new MemoryStream();

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                await stream.CopyToAsync(memoryStream);
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        /// <inheritdoc />
        public async Task DeleteFileAsync(string id)
        {
            var files = Directory.GetFiles(_storagePath, id + ".*");

            foreach (var filePath in files)
            {
                File.Delete(filePath);
            }

            await Task.CompletedTask;
        }
    }
}
