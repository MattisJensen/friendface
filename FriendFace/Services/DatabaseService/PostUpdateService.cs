using FriendFace.Data;
using FriendFace.Models;

namespace FriendFace.Services.DatabaseService;

public class PostUpdateService
{
    private readonly ApplicationDbContext _context;
    
    public PostUpdateService(ApplicationDbContext context)
    {
        _context = context;
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
    
    public bool UpdatePost(int postId, string updatedContent)
    {
        if (_context.Posts.Find(postId) == null)
        {
            throw new KeyNotFoundException();
        }

        Post orgPost = _context.Posts.Find(postId);

        Post post = orgPost;
        post.Content = updatedContent;

        _context.Posts.Add(post);
        _context.SaveChanges();

        return true;
    }
    
    public bool DeletePost(int postId)
    {
        if (_context.Posts.Find(postId) == null)
        {
            throw new KeyNotFoundException();
        }

        _context.Posts.Remove(_context.Posts.Find(postId));
        _context.SaveChanges();
        
        return true;
    }
}