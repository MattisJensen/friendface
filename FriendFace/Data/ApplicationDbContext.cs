using FriendFace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FriendFace.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        // DbSet for each entity/table we want to interact with
        public DbSet<User> Users { get; set; }
        public DbSet<UserFollowsUser> UserFollowsUsers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserLikesPost> UserLikesPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to be called before the custom model configurations
            
            modelBuilder.Entity<User>()
                .Ignore(u => u.UserName)
                .Ignore(u => u.NormalizedUserName)
                .Ignore(u => u.NormalizedEmail)
                .Ignore(u => u.EmailConfirmed)
                .Ignore(u => u.PasswordHash)
                .Ignore(u => u.SecurityStamp)
                .Ignore(u => u.ConcurrencyStamp)
                .Ignore(u => u.PhoneNumber)
                .Ignore(u => u.PhoneNumberConfirmed)
                .Ignore(u => u.TwoFactorEnabled)
                .Ignore(u => u.LockoutEnd)
                .Ignore(u => u.LockoutEnabled)
                .Ignore(u => u.AccessFailedCount);
            
            modelBuilder.Entity<UserFollowsUser>()
                .HasOne(ufu => ufu.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(ufu => ufu.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFollowsUser>()
                .HasOne(ufu => ufu.Following)
                .WithMany(u => u.Followers)
                .HasForeignKey(ufu => ufu.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}