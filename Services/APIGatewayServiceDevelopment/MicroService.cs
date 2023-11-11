using System.Text;

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
			string requestContent;
			using (Stream receiveStream = httpRequest.Body)
			{
				using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
				{
					requestContent = readStream.ReadToEnd();
				}
			}

			using (HttpRequestMessage newRequest = new HttpRequestMessage(new HttpMethod(httpRequest.Method), CreateDestinationUri(httpRequest)))
			{
				newRequest.Content = new StringContent(requestContent, Encoding.UTF8, httpRequest.ContentType);

				foreach (var header in httpRequest.Headers)
				{
					newRequest.Headers.TryAddWithoutValidation(header.Key, header.Value.ToString());
				}

				return await client.SendAsync(newRequest);
			}
		}

		private string CreateDestinationUri(HttpRequest request)
		{
			return $"{this.serviceUri.Scheme}://{this.serviceUri.Authority}{request.Path}";
		}
	}
}
