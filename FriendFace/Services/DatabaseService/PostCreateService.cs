using FriendFace.Data;
using FriendFace.Models;

namespace FriendFace.Services.DatabaseService;

public class PostCreateService
{
    private readonly ApplicationDbContext _context;
    
    public PostCreateService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public bool CreatePost(string content, User sourceUser)
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

        _context.Posts.Add(post);
        _context.SaveChanges();

        return true;
    }
    
    public void addLikeToPost(int postId, int userId)
    {
        var like = new UserLikesPost
        {
            PostId = postId,
            UserId = userId
        };
        
        _context.UserLikesPosts.Add(like);
        _context.SaveChanges();
    }
}