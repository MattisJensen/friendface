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
    
    public Post getPostFromId(int postId)
    {
        return _context.Posts
            .Include(p => p.User)
            .Include(p => p.Likes)
            .Include(p => p.Comments)
            .FirstOrDefault(p => p.Id == postId);
    }

    public int getNumberOfLikes(int postId)
    {
        return _context.UserLikesPosts
            .Count(ul => ul.PostId == postId);
    }

    public bool hasUserLikedPost(int userId, int postId)
    {
        var existingLike = _context.UserLikesPosts.SingleOrDefault(like =>
            like.UserId == userId && like.PostId == postId);
        
        return existingLike != null;
    }
    
    public List<Post> getLatestPostsFromFollowingUserIDs(List<int> followingUserIds)
    {
        return _context.Posts
            .Include(p => p.User)
            .Include(p => p.Likes)
            .Include(p => p.Comments)
            .ThenInclude(c => c.User)
            .Where(p => followingUserIds.Contains(p.UserId))
            .OrderByDescending(p => p.Time)
            .ToList();
    }
}