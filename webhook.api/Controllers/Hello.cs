using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webhook.api.Controllers
{
  public class Hello : ResponseHandler<HelloRequest, HelloResponse>
  {
    public async override Task<HelloResponse> Handle([FromBody] HelloRequest request)
    {
      return new HelloResponse
      {
        Greeting = $"Hello, {request.Name}"
      };
    }
  }

  public class HelloRequest
  {
    public string Name { get; set; }
  }

  public class HelloResponse
  {
    public string Greeting { get; set; }
  }
}
