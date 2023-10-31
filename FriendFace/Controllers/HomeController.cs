using FriendFace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FriendFace.Data;
using FriendFace.ViewModels;
using Microsoft.Extensions.Logging;
using System.Linq;

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
            /*User userLoggedIn = _context.Users.Find(1); // !! here we still need to find the user that is logged in, and handle if no user is logged in !!

            // Fetches the latest posts from the users that the logged in user follows
            var latestPostsFromFollowing = (from post in _context.Posts
                where userLoggedIn.FollowedUsers.Any(follow => follow.FollowedUserId == post.User.Id)
                orderby post.Time descending
                select post).ToList();


            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel()
            {
                User = userLoggedIn,
                PostsInFeed = latestPostsFromFollowing
            };

            return View(homeIndexViewModel);*/

            return View();
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