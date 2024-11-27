namespace TaskManagementAPI.Repositories
{
    public interface IFileStorageRepository
    {
        /// <summary>
        /// Загрузить файл в хранилище.
        /// </summary>
        /// <param name="file">Файл для загрузки.</param>
        /// <returns>ID файла в хранилище.</returns>
        Task<string> UploadFileAsync(IFormFile file);

        /// <summary>
        /// Загрузить файл из хранилища.
        /// </summary>
        /// <param name="id">ID файла в хранилище.</param>
        /// <returns>Поток с содержимым файла.</returns>
        Task<Stream?> DownloadFileAsync(string id);

        /// <summary>
        /// Удалить файл из хранилища.
        /// </summary>
        /// <param name="id">ID файла в хранилище.</param>
        /// <returns>Задача удаления.</returns>
        Task DeleteFileAsync(string id);
    }
}
