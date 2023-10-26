using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FriendFace.Data;

namespace FriendFace.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        [Key] public int Id { get; set; }

        [Required] public string Username { get; set; }

        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [NotMapped] public int Followers { get; set; }

        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }

        [ForeignKey("FollowerId")] public virtual ICollection<UserFollowsUser> FollowedUsers { get; set; }

        [ForeignKey("UserId")] public virtual ICollection<UserLikesPost> LikedPosts { get; set; }

        [ForeignKey("UserId")] public virtual ICollection<Post> Posts { get; set; }

        [ForeignKey("UserId")] public virtual ICollection<Comment> Comments { get; set; }

        public void CalculateFollowersCount(ApplicationDbContext context)
        {
            this.Followers = context.UserFollowsUsers.Count(follow => follow.FollowedUserId == Id);
        }
    }
}