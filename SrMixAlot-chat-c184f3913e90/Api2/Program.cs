using System.IO;
using Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
		{
			var host = CreateWebHostBuilder(args).Build();

			// seed the database
			RunSeeding(host);

			// run web server
            host.Run();
        }

		private static void RunSeeding(IWebHost host)
		{
			// get scope factory from web host
			var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
			
			using (var scope = scopeFactory.CreateScope())
			{
				// get seeder from web host scope factory so that you can access scoped services within transient ones
				var seeder = scope.ServiceProvider.GetService<ChatSeeder>();
				// run the seeder
				seeder.SeedAsync().Wait();
            }
		}

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseIISIntegration()
                .UseStartup<Startup>();

            return host;
        }
    }
}
