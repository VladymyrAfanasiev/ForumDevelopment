using System.Net;
using APIGatewayServiceDevelopment.Modules.Configurations;

namespace APIGatewayServiceDevelopment
{
	public class Router
	{
		private MicroService authorizationMicroService;
		private List<MicroService> microServices = new List<MicroService>();

		public Router(MicroservicesConfigurations configurations)
		{
			ArgumentNullException.ThrowIfNull(nameof(configurations));

			this.authorizationMicroService = new MicroService(configurations.AuthorizationServiceUrl);

			foreach (string microServiceUrl in configurations.Routes)
			{
				this.microServices.Add(new MicroService(microServiceUrl));
			}
		}

		public async Task<HttpResponseMessage> RouteRequest(HttpRequest httpRequest)
		{
			if (authorizationMicroService.IsRoute(httpRequest.Path))
			{
				return await authorizationMicroService.SendRequest(httpRequest);
			}

			MicroService microService = microServices.FirstOrDefault(ms => ms.IsRoute(httpRequest.Path));
			if (microService == null)
			{
				return new HttpResponseMessage
				{
					StatusCode = HttpStatusCode.NotFound
				};
			}

			return await microService.SendRequest(httpRequest);
		}
	}
}
