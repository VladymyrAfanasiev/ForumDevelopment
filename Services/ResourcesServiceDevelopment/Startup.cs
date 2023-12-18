using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using ResourcesServiceDevelopment.Accessors;
using ResourcesServiceDevelopment.Models.Configurations;
using ResourcesServiceDevelopment.Transactions;

namespace ResourcesServiceDevelopment
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

			BaseUrlAccessor baseUrlAccessor = new BaseUrlAccessor()
			{
				BaseUrl = configuration[WebHostDefaults.ServerUrlsKey]
			};

			services.AddControllers();
			services.AddSingleton<IBaseUrlAccessor>(baseUrlAccessor);
			services.AddSingleton<IVaultTransactionProvider, VaultTransactionProvider>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Vault", "Public")),
				RequestPath = "/Public"
			});

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}
