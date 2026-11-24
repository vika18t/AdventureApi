
namespace AdventureApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.WebHost.UseUrls("http://0.0.0.0:5050");
			builder.Services.AddControllers();

			var app = builder.Build();

			// Configure the HTTP request pipeline.

			app.MapControllers();

			app.Run();
		}
	}
}
