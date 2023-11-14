using FriendFace.Data;
using FriendFace.Models;
using Microsoft.EntityFrameworkCore;

namespace FriendFace.Services.DatabaseService;

public class PostQueryService
{
    private readonly ApplicationDbContext _context;

    public PostQueryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Post GetPostFromId(int postId)
    {
        return _context.Posts
            .Include(p => p.User)
            .Include(p => p.Likes)
            .Include(p => p.Comments)
            .FirstOrDefault(p => p.Id == postId) ?? throw new NullReferenceException();
    }
    
    public List<Post> GetPostsFromUserId(int userId)
    {
        return _context.Posts
            .Include(p => p.User)
            .Include(p => p.Likes)
            .Include(p => p.Comments)
            .ThenInclude(c => c.User)
            .Where(p => p.UserId == userId)
            .Where(p => !p.IsDeleted)
            .OrderByDescending(p => p.Time)
            .ToList();
    }

    public int GetNumberOfLikes(int postId)
    {
        return _context.UserLikesPosts.Count(ul => ul.PostId == postId);
    }

    public bool HasUserLikedPost(int userId, int postId)
    {
        var existingLike = _context.UserLikesPosts.SingleOrDefault(like => like.UserId == userId && like.PostId == postId);

        return existingLike != null;
    }

    public List<Post> GetLatestPostsFromFeed(int userId)
    {
        return GetPostsFromUserId(userId);
        var usq = new UserQueryService(_context);
        var followingUserIds = usq.GetFollowingUserIds(userId);
            
        return _context.Posts
            .Include(p => p.User)
            .Include(p => p.Likes)
            .Include(p => p.Comments)
            .ThenInclude(c => c.User)
            .Where(p => followingUserIds.Contains(p.UserId))
            .Where(p => !p.IsDeleted)
            .OrderByDescending(p => p.Time)
            .ToList();
    }
    
}