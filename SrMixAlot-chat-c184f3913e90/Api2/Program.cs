using Api.Models.Entities;
using Api.Services.Connection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
		{
			var host = CreateWebHostBuilder(args).Build();

			// seed the database
			RunSeeding(host);

			// initialize connection queue's and bindings to exchange
			InitializeConnectionBindings(host);

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

		private static void InitializeConnectionBindings(IWebHost host)
		{
			// get scope factory from web host
			var scopeFactory = host.Services.GetService<IServiceScopeFactory>();

			using (var scope = scopeFactory.CreateScope())
			{
				// get connection initializer from web host scope factory so that you can access scoped services within transient ones
				var connectionInitializer = scope.ServiceProvider.GetService<ConnectionInitializer>();
				// initalize
				connectionInitializer.Initialize();
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
