using FriendFace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FriendFace.Data;
using FriendFace.ViewModels;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FriendFace.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // !! here we still need to find the user that is logged in, and handle if no user is logged in !!
            User userLoggedIn = _context.Users
                .Include(u => u.Following)  // Load the users UserA follows
                .Include(u => u.Followers)   // Load the users following UserA
                .FirstOrDefault(u => u.Id == 1);
            
            // Fetch the IDs of the users UserA is following
            List<int> followingUserIds = userLoggedIn.Following.Select(f => f.FollowingId).ToList();


            // Then, query for the 10 newest posts from those users
            List<Post> latestPostsFromFollowing = _context.Posts
                .Include(p => p.User)
                .Where(post => followingUserIds.Contains(post.UserId))
                .OrderByDescending(post => post.Time)
                .Take(20)
                .ToList();
            
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel()
            {
                User = userLoggedIn,
                PostsInFeed = latestPostsFromFollowing
            };

            return View(homeIndexViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}