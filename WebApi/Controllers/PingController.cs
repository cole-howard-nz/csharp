using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PingController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Ping()
    {
        return Ok(value: "pong");
    }

    [HttpGet("{name}")]
    public ActionResult<string> NamedPing(string name)
    {
        return Ok(value: $"pong, {name}");
    }
}
