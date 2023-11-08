using FriendFace.Data;
using FriendFace.Models;

namespace FriendFace.Services.DatabaseService;

public class PostCreateService
{
    public bool CreatePost(ApplicationDbContext context, string content, User sourceUser)
    {
        if (content.Length > 280)
        {
            throw new Exception("Post content string too long.");
        }

        var post = new Post
        {
            Content = content,
            User = sourceUser,
            Time = DateTime
                .UtcNow, // Uses UtcNow, such that the view can calculate the posts createTime in localtime, by comparing local timezone to UTC.
        };

        context.Posts.Add(post);
        context.SaveChanges();

        return true;
    }
}