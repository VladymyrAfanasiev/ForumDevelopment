using System.Text;
using ForumServiceDevelopment.Data;
using ForumServiceDevelopment.Models.Configurations;
using ForumServiceDevelopment.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ForumServiceDevelopment
{
	public class Startup
	{
		private IConfiguration configuration { get; }

		public Startup(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			AuthenticationConfiguration authenticationConfiguration = new AuthenticationConfiguration();
			configuration.Bind("Authentication", authenticationConfiguration);
			services.AddSingleton(authenticationConfiguration);

			string connection = configuration.GetConnectionString("ForumDatabaseConnection");
			services.AddDbContext<ForumDatabaseContext>(options =>
				options.UseSqlServer(connection));
			services.AddDatabaseDeveloperPageExceptionFilter();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.AccessTokenSecret)),
						ValidateIssuerSigningKey = true,
					};
				});

			services.AddControllers();

			services.AddTransient<IGroupService, GroupService>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}
