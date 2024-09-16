using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Darts.Server.Features.Users;

[ApiController]
[Route("api/profiles")]
public class ProfilesController(UsersDbContext dbContext) : ControllerBase
{
    [HttpGet("{username}")]
    public async Task<ActionResult<ProfileResponse>> Get(string username)
    {
        var user = await dbContext.Users
            .Where(u => u.UserName == username)
            .Select(u => new { u.UserName, u.Profile.Bio, u.Profile.Image })
            .FirstOrDefaultAsync();
        
        if (user == null)
        {
            return NotFound();
        }

        return new ProfileResponse(
            new ProfileResponseBody(user.UserName!, user.Bio,
                user.Image, false));
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