using FriendFace.Data;
using FriendFace.Models;
using Microsoft.AspNetCore.Mvc;

namespace FriendFace.Controllers;

/*
 NB: All of the following CRUD methods, are quite possibly in the wrong class. Each method should possibly
 be sorted into seperate service classes
 */
public class PostController : Controller
{
    private readonly ApplicationDbContext _context;

    public PostController(ApplicationDbContext context)
    {
        this._context = context;
    }

    public bool CreatePost(string content, User sourceUser)
    {
        if (content.Length > 280)
        {
            throw new Exception("Post content string too long.");
        }

        var post = new Post
        {
            Content = content,
            User = sourceUser,
            Time = DateTime
                .UtcNow, // Uses UtcNow, such that the view can calculate the posts createTime in localtime, by comparing local timezone to UTC.
        };

        _context.Posts.Add(post);
        _context.SaveChanges();

        return true;
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