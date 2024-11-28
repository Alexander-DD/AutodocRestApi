
using Microsoft.EntityFrameworkCore;

using TaskManagementAPI.Data;
using TaskManagementAPI.Repositories;

namespace AutodocRestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Настраиваем EF
            var mssqlConnection = builder.Configuration.GetConnectionString("MSSqlConnection");
            builder.Services.AddDbContext<MSSqlDbContext>(options =>
                options.UseSqlServer(mssqlConnection));
            //var postgresConnection = builder.Configuration.GetConnectionString("PostgresConnection");
            //builder.Services.AddDbContext<PostgresDbContext>(options =>
            //    options.UseNpgsql(postgresConnection));

            // Настройка репозитория бд
            builder.Services.AddScoped(typeof(IDBRepository<>), typeof(MSSqlRepository<>));
            //builder.Services.AddScoped(typeof(IDBRepository<>), typeof(PostgresRepository<>));

            // Добавляем хранилище файлов
            builder.Services.AddScoped<IFileStorageRepository, DiskFileStorageRepository>();
            //builder.Services.AddScoped<IFileStorageRepository, MongoFileStorageRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
