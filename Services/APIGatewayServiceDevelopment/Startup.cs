using APIGatewayServiceDevelopment.Middleware;
using APIGatewayServiceDevelopment.Modules.Configurations;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace APIGatewayServiceDevelopment
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
			services.Configure<KestrelServerOptions>(options =>
			{
				options.AllowSynchronousIO = true;
			});

			services.Configure<IISServerOptions>(options =>
			{
				options.AllowSynchronousIO = true;
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseMiddleware<ErrorHandlerMiddleware>();

			MicroservicesConfigurations microservicesConfigurations = new MicroservicesConfigurations();
			configuration.Bind("Microservices", microservicesConfigurations);
			Router router = new Router(microservicesConfigurations);

			app.Run(async (context) =>
			{
				using (HttpResponseMessage httpResponseMessage = await router.RouteRequest(context.Request))
				{
					context.Response.StatusCode = (int)httpResponseMessage.StatusCode;

					context.Response.Headers["Content-Type"] = "application/json";
					foreach (var header in httpResponseMessage.Headers)
					{
						if (header.Key == "Date" ||
							header.Key == "Server" ||
							header.Key == "Transfer-Encoding")
						{
							continue;
						}

						context.Response.Headers[header.Key] = header.Value.ToArray();
					}

					using (var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync())
					{
						await contentStream.CopyToAsync(context.Response.Body);
					}
				}
			});
		}
	}
}
