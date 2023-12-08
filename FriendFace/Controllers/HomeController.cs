using FriendFace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FriendFace.Data;
using FriendFace.ViewModels;
using Microsoft.Extensions.Logging;
using System.Linq;
using FriendFace.Services;
using FriendFace.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
        
        private readonly CommentService _commentService;
        private readonly PostService _postService;

        private readonly RazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;

      

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,
            UserQueryService userQueryService, PostQueryService postQueryService,
            PostDeleteService postDeleteService, PostCreateService postCreateService,
            UserCreateService userCreateService, PostService postService, CommentService commentService)
        {
            _logger = logger;
            _context = context;

            _userQueryService = userQueryService;
            _userCreateService = userCreateService;
            _postQueryService = postQueryService;
            _postDeleteService = postDeleteService;
            _postCreateService = postCreateService;

            _postService = postService;

            _commentService = commentService;
        }

        public IActionResult Index()
        {

            
            var loggedInUser = _userQueryService.GetLoggedInUser();
            if (loggedInUser == null)
            {
                var topLikedPosts = _postQueryService.GetPostsByLikes(5);

                var guestViewModel = new GuestIndexViewModel
                {
                    TopLikedPosts = topLikedPosts
                };
                return View("GuestIndex", guestViewModel);
            }
            var postsInFeed = _postQueryService.GetLatestPostsFromFeed(loggedInUser.Id);
            var postsByLoggedInUser = _postQueryService.GetPostsFromUserId(loggedInUser.Id);
            var postCharLimit = _postQueryService.GetPostCharacterLimit();

            var homeIndexViewModel = new HomeIndexViewModel
            {
                User = loggedInUser,
                PostsInFeed = postsInFeed,
                PostsByLoggedInUser = postsByLoggedInUser,
                PostCharLimit = postCharLimit
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
        
        [HttpGet]
        public IActionResult ToggleLikePost(int postId)
        {
            _postService.ToggleLikePost(postId);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult GetPostLikes([FromQuery] int postId)
        {
            var result = _postService.GetPostLikes(postId);
            return Json(result);
        }

        [HttpGet]
        public IActionResult DeletePost(int postId)
        {
            _postService.DeletePost(postId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditPost(PostIdContentModel model)
        {
            _postService.EditPost(model);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult CreatePost(string content)
        {
            _postService.CreatePost(content, ControllerContext);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult CreateComment([FromBody] PostIdContentModel request)
        {
            var result = _commentService.CreateComment(request.Content, request.PostId);
            return Json(result);
        }
    }
}