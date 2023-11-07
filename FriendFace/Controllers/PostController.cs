using FriendFace.Data;
using FriendFace.Models;
using Microsoft.AspNetCore.Mvc;

namespace FriendFace.Controllers;

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
}