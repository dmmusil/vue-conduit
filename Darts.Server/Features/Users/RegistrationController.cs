using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Darts.Server.Features.Users;

[ApiController]
[Route("api/users")]
public class RegistrationController(UserManager<User> userManager)
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UserResponse>> Register(
        [FromBody] UserRequest request)
    {
        var result = await userManager.CreateAsync(
            new User
            {
                UserName = request.user.username,
                Email = request.user.email
            }, request.user.password);

        if (result.Succeeded)
        {
            return new UserResponse(new UserResponseBody(request.user.email,
                request.user.username,
                null, null, null));
        }

        return BadRequest(result.Errors.Select(e => e.Description));
    }
}

// ReSharper disable InconsistentNaming
public record UserRequest(UserRequestBody user);

public record UserRequestBody(string username, string email, string password);

public record UserResponse(UserResponseBody user);

public record UserResponseBody(
    string email,
    string username,
    string? token,
    string? bio,
    string? image);

// ReSharper restore InconsistentNaming