using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendFace.Models
{
    public class Post
    {
        [Key] public int Id { get; set; }

        [Required] public string Content { get; set; }

        [Required] public DateTime Time { get; set; }

        [ForeignKey("UserId")] public virtual User User { get; set; }

        [ForeignKey("PostId")] public virtual ICollection<UserLikesPost> LikedByUsers { get; set; }

        [ForeignKey("PostId")] public virtual ICollection<Comment> Comments { get; set; }

    }
}