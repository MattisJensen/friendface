using FriendFace.Data;
using FriendFace.Models;
using Microsoft.EntityFrameworkCore;

namespace FriendFace.Services.DatabaseService;

public class PostQueryService
{
    public static List<Post> getLatestPostsFromFollowingUserIDs(ApplicationDbContext context, List<int> followingUserIds)
    {
        return context.Posts
            .Include(p => p.User)
            .Include(p => p.Likes)
            .Include(p => p.Comments)
            .Where(p => followingUserIds.Contains(p.UserId))
            .OrderByDescending(p => p.Time)
            .ToList();
    }
}