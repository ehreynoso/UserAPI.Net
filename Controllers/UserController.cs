using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private static readonly List<User> _users = new()
    {
        new User { Id = 1, Name = "Jhon Doe" },
        new User { Id = 2, Name = "Jane Doe" }
    };

    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        return Ok();
    }
}