// Example controller

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class GreetingsController : ControllerBase
{
    private readonly List<string> greetings = ["Kia ora", "Hello", "Chur"];

    [HttpGet]
    public IEnumerable<string> GetAll()
    {
        return greetings;
    }

    [HttpGet("{index:int}")]
    public ActionResult<string> GetSingle(int index)
    {
        if (index < 0 || index >= greetings.Count())
        {
            return NotFound();
        }
        return Ok(greetings[index]);
    }
}
