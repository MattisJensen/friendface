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
    
    public static bool UpdatePost(ApplicationDbContext context, int postId, string updatedContent)
    {
        if (context.Posts.Find(postId) == null)
        {
            throw new KeyNotFoundException();
        }

        Post orgPost = context.Posts.Find(postId);

        Post post = orgPost;
        post.Content = updatedContent;

        context.Posts.Add(post);
        context.SaveChanges();

        return true;
    }
    
    public static bool DeletePost(ApplicationDbContext context, int postId)
    {
        if (context.Posts.Find(postId) == null)
        {
            throw new KeyNotFoundException();
        }

        context.Posts.Remove(context.Posts.Find(postId));
        context.SaveChanges();
        
        return true;
    }
}