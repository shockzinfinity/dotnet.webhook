using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace webhook.api
{
  public class WebhookOptions
  {
    public string RoutePrefix { get; set; } = "webhooks";
  }

  public static class WebhookExtensions
  {
    public static IServiceCollection AddWebhooks(this IServiceCollection services, Action<WebhookOptions> spaceAction = null)
    {
      var options = new WebhookOptions();
      services.Configure<RouteOptions>(opt =>
      {
        opt.ConstraintMap.Add("webhookRoutePrefix", typeof(WebhookRoutePrefixConstraint));
      });
      spaceAction?.Invoke(options);
      services.AddSingleton(options);

      return services;
    }
  }

  public class WebhookRoutePrefixConstraint : IRouteConstraint
  {
    // TODO: webhook secret key 는?
    // routeKey 검증 필요
    public bool Match(HttpContext? httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
      if (values.TryGetValue("prefix", out var value) && value is string actual)
      {
        var options = (WebhookOptions)httpContext?.RequestServices.GetService(typeof(WebhookOptions));
        // urls are case sensitive
        var expected = options?.RoutePrefix;
        return expected == actual;
      }
      return false;
    }
  }
}
