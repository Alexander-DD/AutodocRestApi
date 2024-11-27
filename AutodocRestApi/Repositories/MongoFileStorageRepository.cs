using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace TaskManagementAPI.Repositories
{
    public class MongoFileStorageRepository : IFileStorageRepository
    {
        private readonly GridFSBucket _gridFS;

        public MongoFileStorageRepository(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoSettings:ConnectionString"));
            var database = client.GetDatabase(configuration["MongoSettings:DatabaseName"]);
            _gridFS = new GridFSBucket(database, new GridFSBucketOptions
            {
                BucketName = configuration["MongoSettings:GridFSBucketName"]
            });
        }

        /// <inheritdoc />
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var fileId = await _gridFS.UploadFromStreamAsync(file.FileName, stream);
                return fileId.ToString(); // Возвращаем ID файла в формате строки
            }
        }

        /// <inheritdoc />
        public async Task<Stream?> DownloadFileAsync(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return null;

            try
            {
                return await _gridFS.OpenDownloadStreamAsync(objectId);
            }
            catch (GridFSFileNotFoundException)
            {
                return null; // Файл не найден
            }
        }

        /// <inheritdoc />
        public async Task DeleteFileAsync(string id)
        {
            if (ObjectId.TryParse(id, out var objectId))
            {
                await _gridFS.DeleteAsync(objectId);
            }
        }
    }
}
