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
    private readonly PostDeleteService _postDeleteService;

    public PostController(ApplicationDbContext context, PostQueryService postQueryService,
        PostCreateService postCreateService, PostUpdateService postUpdateService, PostDeleteService postDeleteService)
    {
        _context = context;
        _postQueryService = postQueryService;
        _postCreateService = postCreateService;
        _postUpdateService = postUpdateService;
        _postDeleteService = postDeleteService;
    }

    // Queries
    public Post GetPostFromId(int postId)
    {
        return _postQueryService.GetPostFromId(postId);
    }

    public int GetNumberOfLikes(int postId)
    {
        return _postQueryService.GetNumberOfLikes(postId);
    }

    public bool UserHasLikedPost(int userId, int postId)
    {
        return _postQueryService.HasUserLikedPost(userId, postId);
    }

    public List<Post> GetLatestPostsFromFollowingUserIDs(List<int> followingUserIds)
    {
        return _postQueryService.GetLatestPostsFromFollowingUserIDs(followingUserIds);
    }

    // Creation
    public bool CreatePost(string content, User sourceUser)
    {
        return _postCreateService.CreatePost(content, sourceUser);
    }


    // Updates
    public void AddLikeToPost(int postId, int userId)
    {
        _postUpdateService.AddLikeToPost(postId, userId);
    }

    public bool UpdatePost(int postId, string updatedContent)
    {
        return _postUpdateService.UpdatePost(postId, updatedContent);
    }

    public bool DeletePost(int postId)
    {
        return _postDeleteService.SoftDeletePost(postId);
    }
}