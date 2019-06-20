using Api.Models.Dto;
using Api.Models.Entities;
using Api.Services.Connection;
using Api.Services.Connection.Interfaces;
using Api.Services.Data;
using Api.Services.Data.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            var connection = @"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.AspNetCore.NewDb;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<ChatContext>
                (options => options.UseSqlServer(connection));

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
					.ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
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
