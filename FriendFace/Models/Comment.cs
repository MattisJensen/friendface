using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendFace.Models
{
    public class Comment
    {
        [Key] public int Id { get; set; }

        [Required] public string Content { get; set; }

        [Required] public DateTime CommentTime { get; set; }

        [ForeignKey("UserId")] public int UserId { get; set; }

        [ForeignKey("PostId")] public int PostId { get; set; }

        [ForeignKey("UserId")] public virtual User User { get; set; }

        [ForeignKey("PostId")] public virtual Post Post { get; set; }
    }
}