﻿using Microsoft.Extensions.Primitives;

namespace APIGatewayServiceDevelopment.Middleware
{
	public class OptionsMiddleware
	{
		private readonly RequestDelegate _next;

		public OptionsMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public Task Invoke(HttpContext context)
		{
			return BeginInvoke(context);
		}

		private Task BeginInvoke(HttpContext context)
		{
			if (context.Request.Headers.TryGetValue("Origin", out StringValues values))
			{
				context.Response.Headers.Add("Access-Control-Allow-Origin", values.FirstOrDefault());
			}

			if (context.Request.Method.Equals("OPTIONS", StringComparison.CurrentCultureIgnoreCase))
			{
				context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin, X-Requested-With, Content-Type, Accept", "Authorization" });
				context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, DELETE, OPTIONS" });
				context.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
				context.Response.StatusCode = 200;
				return context.Response.WriteAsync("OK");
			}

			return _next.Invoke(context);
		}
	}
}
