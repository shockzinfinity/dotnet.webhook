using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace webhook.api
{
  /// <summary>
  /// Base class for all handlers
  /// route pattern : webhooks/{handler name}
  /// </summary>
  /// <typeparam name="TRequest"></typeparam>
  /// <typeparam name="TResponse"></typeparam>
  [ApiController, Route("{prefix:webhookRoutePrefix}/[controller]")]
  public abstract class ResponseHandler<TRequest, TResponse>
  {
    [HttpPost, Route("")]
    public abstract Task<TResponse> Handle([FromBody] TRequest request);
  }

  /// <summary>
  /// 
  /// route pattern : webhooks/{handler name}
  /// </summary>
  /// <typeparam name="TRequest"></typeparam>
  [ApiController, Route("{prefix:webhookRoutePrefix}/[controller]")]
  public abstract class AcceptedHandler<TRequest>
  {
    [HttpPost, Route(""), Status(HttpStatusCode.Accepted)]
    public abstract Task Handle([FromBody] TRequest request);
  }

  public class Status : ActionFilterAttribute
  {
    private readonly HttpStatusCode _statusCode;

    public Status(HttpStatusCode statusCode)
    {
      this._statusCode = statusCode;
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
      base.OnActionExecuted(context);
      context.Result = new StatusCodeResult((int)_statusCode);
    }
  }
}
