using Microsoft.EntityFrameworkCore;

namespace Darts.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration["DATABASE_URL"] ??
                                   throw new ArgumentException(
                                       "DATABASE_URL not defined in configuration");
            builder.Services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseNpgsql(UrlToConnectionString.Convert(connectionString));
            });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            using (var scope = app.Services.CreateAsyncScope())
            {
                var usersDbContext =
                    scope.ServiceProvider.GetService<UsersDbContext>() ??
                    throw new NullReferenceException(
                        "Unable to get instance of UsersDbContext");
                await usersDbContext.Database.MigrateAsync();
            }

            await app.RunAsync();
        }
    }

    public class UsersDbContext : DbContext
    {
        protected UsersDbContext()
        {
        }

        public UsersDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}