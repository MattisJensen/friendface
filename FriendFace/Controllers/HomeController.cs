using FriendFace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FriendFace.Data;
using FriendFace.ViewModels;
using Microsoft.Extensions.Logging;
using System.Linq;
using FriendFace.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace FriendFace.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserQueryService _userQueryService;
        private readonly UserCreateService _userCreateService;
        private readonly PostQueryService _postQueryService;
        private readonly PostDeleteService _postDeleteService;
        private readonly PostCreateService _postCreateService;
        
        private readonly PostService _postService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,
            UserQueryService userQueryService, PostQueryService postQueryService,
            PostDeleteService postDeleteService, PostCreateService postCreateService,
            UserCreateService userCreateService, PostService postService)
        {
            _logger = logger;
            _context = context;
            _userQueryService = userQueryService;
            _userCreateService = userCreateService;
            _postQueryService = postQueryService;
            _postDeleteService = postDeleteService;
            _postCreateService = postCreateService;
            
            _postService = postService;
        }

        public IActionResult Index()
        {
            // !! here we still need to find the user that is logged in, and handle if no user is logged in !!
            User loggedInUser = _userQueryService.GetLoggedInUser();
            var homeIndexViewModel = new HomeIndexViewModel()
            {
                User = loggedInUser,
                PostsInFeed = _postQueryService.GetLatestPostsFromFeed(loggedInUser.Id)
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

        [HttpPost]
        public IActionResult ToggleLikePost([FromBody] int postId)
        {
            _postService.ToggleLikePost(postId);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetPostLikes([FromQuery] int postId)
        {
            var result = _postService.GetPostLikes(postId);
            return Json(result);
        }
    }
}