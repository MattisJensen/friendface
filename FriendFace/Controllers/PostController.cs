using FriendFace.Data;
using FriendFace.Models;
using Microsoft.AspNetCore.Mvc;

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
}