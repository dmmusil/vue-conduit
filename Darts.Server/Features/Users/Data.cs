using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Darts.Server.Features.Users;
public class User : IdentityUser
{
        public UserProfile Profile { get; set; }
}

[PrimaryKey(nameof(UserId))]
public class UserProfile
{
    public string UserId { get; set; }
    public string? Bio { get; set; }
    public string? Image { get; set; }
    
    public User User { get; set; }
}
    
public class UsersDbContext : DbContext
{
    protected UsersDbContext()
    {
    }

    public UsersDbContext(DbContextOptions options) : base(options)
    {
    }
        
    public DbSet<User> Users { get; set; }
}