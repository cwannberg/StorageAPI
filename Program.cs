
using StorageAPI.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace StorageAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<StorageAPIContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("StorageAPIContext") ?? throw new InvalidOperationException("Connection string 'StorageAPIContext' not found.")));

            builder.Services.AddDbContext<StorageAPIContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("StorageAPIContext") 
               ?? throw new InvalidOperationException("Connection string 'StorageAPIContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
