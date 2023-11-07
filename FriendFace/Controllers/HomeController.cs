using FriendFace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FriendFace.Data;
using FriendFace.ViewModels;
using Microsoft.Extensions.Logging;
using System.Linq;
using FriendFace.Services.DatabaseService;
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
            User loggedInUser = UserQueryService.getUser(_context, 1);
            
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel()
            {
                User = loggedInUser,
                PostsInFeed = PostQueryService.getLatestPostsFromFollowingUserIDs(_context, UserQueryService.getFollowingUserIds(loggedInUser))
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
        
        public void likePost(int userId)
        {
            
        }
    }
}