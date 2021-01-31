using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace webhook.api.Controllers
{
  public class Yep : AcceptedHandler<HelloRequest>
  {
    private readonly ILogger<Yep> _logger;
    public Yep(ILogger<Yep> logger)
    {
      this._logger = logger;
    }

    public override Task Handle([FromBody] HelloRequest request)
    {
      this._logger.LogInformation($"Hello, {request.Name}");
      return Task.CompletedTask;
    }
  }
}
