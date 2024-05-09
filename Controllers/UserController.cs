using Microsoft.AspNetCore.Mvc;

namespace Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    [HttpGet]
    public ActionResult<string> Get()
    {
        return "Hello";
    }

    [HttpPost]
    public ActionResult<string> Post([FromBody] string value)
    {
        Console.WriteLine(value);
        return "Hello";
    }

}

