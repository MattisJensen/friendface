using FriendFace.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FriendFace.Models;

namespace FriendFace.Data;

public class FriendFaceContext : IdentityDbContext<FriendFaceUser>
{
    public FriendFaceContext(DbContextOptions<FriendFaceContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<FriendFace.Models.Post> Post { get; set; } = default!;
}
