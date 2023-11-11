using System.Text;
using AuthorizationServiceDevelopment.Data;
using AuthorizationServiceDevelopment.Models.Configurations;
using AuthorizationServiceDevelopment.Services;
using AuthorizationServiceDevelopment.Services.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationServiceDevelopment
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

			string connection = configuration.GetConnectionString("AuthorizationDatabaseConnection");
			services.AddDbContext<AuthorizationDbContext>(options =>
				options.UseSqlServer(connection));
			services.AddDatabaseDeveloperPageExceptionFilter();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidIssuer = authenticationConfiguration.Issuer,
						ValidateAudience = true,
						ValidAudience = authenticationConfiguration.Audience,
						ValidateLifetime = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.AccessTokenSecret)),
						ValidateIssuerSigningKey = true,
					};
				});

			services.AddControllers();

			services.AddTransient<IUserService, UserService>();
			services.AddTransient<AccessTokenGenerator>();
			services.AddTransient<Authenticator>();
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
