namespace FriendFace.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime DateCommented { get; set; }
        public int UserId { get; set; } // Foreign Key
        public int PostId { get; set; } // Foreign Key

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }

}
