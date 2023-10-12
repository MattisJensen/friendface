using FriendFace.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendFace.Models
{
    public class Post
    {
        public int Id { get; set; }
        // ... other properties ...

        public string Content { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual FriendFaceUser User { get; set; }  // custom user class that extends IdentityUser
    }
}
