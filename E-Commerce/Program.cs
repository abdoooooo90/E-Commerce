
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;

namespace E_Commerce
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			#region Services
			builder.Services.AddScoped<IDbInitializer, DbInitializer>();
			builder.Services.AddDbContext<StoreContext>((options)=>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			#endregion
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();
			await InitializeDbAsync(app);

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

			async Task InitializeDbAsync(WebApplication app)
			{
				//Create Object From Type That Implements IDbInitializer
				using var scope = app.Services.CreateScope();
				var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
				await dbInitializer.InitializerAsync();
			}
		}
	}
}
