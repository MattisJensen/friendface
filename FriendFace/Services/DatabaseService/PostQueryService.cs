using FriendFace.Data;
using FriendFace.Models;
using Microsoft.EntityFrameworkCore;

namespace FriendFace.Services.DatabaseService;

public class PostQueryService
{
    public static Post getPostFromId(ApplicationDbContext context, int postId)
    {
        return context.Posts
            .Include(p => p.User)
            .Include(p => p.Likes)
            .Include(p => p.Comments)
            .FirstOrDefault(p => p.Id == postId);
    }

    public static int getNumberOfLikes(ApplicationDbContext context, int postId)
    {
        return context.UserLikesPosts
            .Count(ul => ul.PostId == postId);
    }

    public static bool userHasLikedPost(ApplicationDbContext context, int userId, int postId)
    {
        // Checks UserLikesPost for the given user and post IDs
        return context.UserLikesPosts
            .Any(ulp => ulp.UserId == userId && ulp.PostId == postId);
    }

    public static List<Post> getLatestPostsFromFollowingUserIDs(ApplicationDbContext context,
        List<int> followingUserIds)
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