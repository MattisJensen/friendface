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
    private readonly PostQueryService _postQueryService;
    private readonly PostCreateService _postCreateService;
    private readonly PostUpdateService _postUpdateService;

    public PostController(ApplicationDbContext context, PostQueryService postQueryService,
        PostCreateService postCreateService, PostUpdateService postUpdateService)
    {
        _context = context;
        _postQueryService = postQueryService;
        _postCreateService = postCreateService;
        _postUpdateService = postUpdateService;
    }

    // Queries
    public Post GetPostFromId(int postId)
    {
        return _postQueryService.getPostFromId(postId);
    }

    public int GetNumberOfLikes(int postId)
    {
        return _postQueryService.getNumberOfLikes(postId);
    }

    public bool UserHasLikedPost(int userId, int postId)
    {
        return _postQueryService.hasUserLikedPost(userId, postId);
    }

    public List<Post> GetLatestPostsFromFollowingUserIDs(List<int> followingUserIds)
    {
        return _postQueryService.getLatestPostsFromFollowingUserIDs(followingUserIds);
    }

    // Creation
    public bool CreatePost(string content, User sourceUser)
    {
        return _postCreateService.CreatePost(content, sourceUser);
    }


    // Updates
    public void AddLikeToPost(int postId, int userId)
    {
        _postUpdateService.addLikeToPost(postId, userId);
    }

    public bool UpdatePost(int postId, string updatedContent)
    {
        return _postUpdateService.UpdatePost(postId, updatedContent);
    }

    public bool DeletePost(int postId)
    {
        return _postUpdateService.DeletePost(postId);
    }
}