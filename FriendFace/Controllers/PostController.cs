using FriendFace.Data;
using FriendFace.Models;
using FriendFace.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;

namespace FriendFace.Controllers;

/*
 NB: All of the following CRUD methods, are quite possibly in the wrong class. Each method should possibly
 be sorted into separate service classes
 */
public class PostController : Controller
{
    private readonly ApplicationDbContext _context;

    public PostController(ApplicationDbContext context)
    {
        this._context = context;
    }

    // Queries
    public Post GetPostFromId(ApplicationDbContext context, int postId)
    {
        return PostQueryService.getPostFromId(context, postId);
    }

    public int GetNumberOfLikes(ApplicationDbContext context, int postId)
    {
        return PostQueryService.getNumberOfLikes(context, postId);
    }

    public bool UserHasLikedPost(ApplicationDbContext context, int userId, int postId)
    {
        return PostQueryService.userHasLikedPost(context, userId, postId);
    }

    public List<Post> GetLatestPostsFromFollowingUserIDs(ApplicationDbContext context,
        List<int> followingUserIds)
    {
        return PostQueryService.getLatestPostsFromFollowingUserIDs(context, followingUserIds);
    }
    
    // Creation
    public bool CreatePost(ApplicationDbContext context, string content, User sourceUser)
    {
        return PostCreateService.CreatePost(context, content, sourceUser);
    }
    
    
    // Updates
    public void AddLikeToPost(ApplicationDbContext context, int postId, int userId)
    {
        PostUpdateService.addLikeToPost(context, postId, userId);
    }

    public bool UpdatePost(ApplicationDbContext context, int postId, string updatedContent)
    {
        return PostUpdateService.UpdatePost(context, postId, updatedContent);
    }

    public bool DeletePost(ApplicationDbContext context, int postId)
    {
        return PostUpdateService.DeletePost(context, postId);
    }
}