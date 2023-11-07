using FriendFace.Data;
using FriendFace.Models;

namespace FriendFace.Services.DatabaseService;

public class PostUpdateService
{
    public static void addLikeToPost(ApplicationDbContext context, int postId, int userId)
    {
        var like = new UserLikesPost
        {
            PostId = postId,
            UserId = userId
        };
        
        context.UserLikesPosts.Add(like);
        context.SaveChanges();
    }
}