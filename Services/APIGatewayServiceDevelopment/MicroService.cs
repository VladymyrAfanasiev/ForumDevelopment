using Microsoft.AspNetCore.Mvc.WebApiCompatShim;

namespace APIGatewayServiceDevelopment
{
	public class MicroService
	{
		private Uri serviceUri;
		private HttpClient client = new HttpClient();

		public MicroService(string authorizationServiceUrl)
		{
			this.serviceUri = new Uri(authorizationServiceUrl);
		}

		public bool IsRoute(string path)
		{
			return path.TrimEnd('/').StartsWith(this.serviceUri.AbsolutePath.TrimEnd('/'));
		}

		public async Task<HttpResponseMessage> SendRequest(HttpRequest httpRequest)
		{
			HttpRequestMessageFeature hreqmf = new HttpRequestMessageFeature(httpRequest.HttpContext);
			HttpRequestMessage httpRequestMessage = hreqmf.HttpRequestMessage;

			// redirect
			httpRequestMessage.RequestUri = CreateDestinationUri(httpRequest);

			return await client.SendAsync(httpRequestMessage);
		}

		private Uri CreateDestinationUri(HttpRequest request)
		{
			return new Uri($"{this.serviceUri.Scheme}://{this.serviceUri.Authority}{request.Path}");
		}
	}
}
