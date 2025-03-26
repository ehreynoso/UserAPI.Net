using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        _logger.LogInformation("Getting all the users");
        return Ok(UserStore._users);
    }

    [HttpGet("id:int", Name = "GetUser")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<User> GetUser(int id)
    {
        var user = UserStore._users.FirstOrDefault(u => u.Id == id);

        if(user == null)
        {
            _logger.LogError("User not found");
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<User> CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (UserStore._users.FirstOrDefault(u=>u.Name.ToLower() == user.Name.ToLower()) != null)
        {
            ModelState.AddModelError("Duplicated Name", "The user name already exist");
            return BadRequest(ModelState);
        }
        user.Id = UserStore._users.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
        UserStore._users.Add(user);

        return CreatedAtRoute("GetUser", new { id = user.Id }, user);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult UpdateUser(int id, [FromBody] User user)
    {
        if (user == null || id != user.Id)
        {
            return BadRequest();
        }

        var user2 = UserStore._users.FirstOrDefault( u => u.Id == id);
        if (user2 == null)
        {
            return NotFound();
        }

        user2.Id = user.Id;
        user2.Name = user.Name;

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteUser(int id)
    {
        var user = UserStore._users.FirstOrDefault(user=>user.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        UserStore._users.Remove(user);

        return NoContent();
    }
}