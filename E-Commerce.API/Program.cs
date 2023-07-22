using E_Commerce.API.Middleware;
using Ecommerce.Core.Interfaces;
using ECommerce.Infrastructure.Classes;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API
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


            // Add DbContext 
            builder.Services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped (typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddCors(options=>
            options.AddPolicy("CorsPolicy", policy=> policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();


            app.MapControllers();

            using var scope = app.Services.CreateScope();
            var Services = scope.ServiceProvider;
            var context = Services.GetRequiredService<ApplicationDbContext>();

            var Logger = Services.GetRequiredService<ILogger<Program>>();

            try
            {
                await context.Database.MigrateAsync();
                await StoreContextSeeding.SeedAsync(context);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occured while migrating process");
            }

            app.Run();
        }
    }
}