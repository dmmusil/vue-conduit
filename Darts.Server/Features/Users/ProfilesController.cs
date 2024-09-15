using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Darts.Server.Features.Users;

[ApiController]
[Route("api/profiles")]
public class ProfilesController(UserManager<User> userManager) : ControllerBase
{
    [HttpGet("{username}")]
    public async Task<ActionResult<ProfileResponse>> Get(string username)
    {
        var user =
            await userManager.Users.FirstOrDefaultAsync(u =>
                u.UserName == username);
        if (user == null)
        {
            return NotFound();
        }

        return new ProfileResponse(
            new ProfileResponseBody(user.UserName!, null, null, false));
    }
}

// ReSharper disable InconsistentNaming
public record ProfileResponse(ProfileResponseBody profile);

public record ProfileResponseBody(
    string username,
    string? bio,
    string? image,
    bool following);

// ReSharper restore InconsistentNaming
