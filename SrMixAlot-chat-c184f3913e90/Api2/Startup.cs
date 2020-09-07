using System.Linq;
using System.Text;
using Api.Services.Connection;
using Api.Services.Connection.Interfaces;
using AutoMapper;
using Data;
using Data.Configuration;
using Data.Entities;
using Data.Services;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Models.Dto;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
		{
			services.AddIdentity<User, IdentityRole>(options =>
			{
                options.User.RequireUniqueEmail = true;

				// set password requirements
                options.Password.RequireDigit = false;
				options.Password.RequiredLength = 6;
				options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
				
			}).AddEntityFrameworkStores<ChatContext>();

			// todo: set this from configuration
			var tokenConfiguration = new TokenConfiguration();
			services.AddSingleton(tokenConfiguration);

			// add authentication
			services.AddAuthentication()
				.AddCookie()
				.AddJwtBearer(cfg =>
				{
					cfg.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidIssuer = tokenConfiguration.Issuer,
						ValidAudience = tokenConfiguration.Audience,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.Key))
					};
				});

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			// add transient services
			services.AddTransient<ChatSeeder>();
			services.AddTransient<ConnectionInitializer>();

            var connection = @"Server=(localdb)\mssqllocaldb;Database=ChatDb4;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<ChatContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("Api")));

			// add scoped services
			services.AddScoped<IChatRoomDataService, ChatRoomDataService>();
			services.AddScoped<IChatMessageDataService, ChatMessageDataService>();
			services.AddScoped<IUserService, UserService>();
			
			// add singleton services
			services.AddSingleton<IExchangeService, ExchangeService>();
			services.AddSingleton<IMessageService, MessageService>();

            Mapper.Initialize(config =>
			{
				config.CreateMap<User, UserDto>()
					.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));
				config.CreateMap<ChatMessage, ChatMessageDto>();
				config.CreateMap<ChatRoom, ChatRoomDto>()
					.ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.UserRooms.Select(userRoom => userRoom.User)))
					.ForMember(dest => dest.ChatMessages, opt => opt.MapFrom(src => src.ChatMessages));
			});

			services.AddScoped(_ => Mapper.Configuration.CreateMapper());
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
			app.UseAuthentication();
            app.UseMvc();
		}
    }
}
