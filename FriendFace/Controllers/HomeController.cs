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
        private readonly UserQueryService _userQueryService;
        private readonly UserCreateService _userCreateService;
        private readonly PostQueryService _postQueryService;
        private readonly PostDeleteService _postDeleteService;
        private readonly PostCreateService _postCreateService;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,
            UserQueryService userQueryService, PostQueryService postQueryService,
            PostDeleteService postDeleteService, PostCreateService postCreateService,
            UserCreateService userCreateService)
        {
            _logger = logger;
            _context = context;
            _userQueryService = userQueryService;
            _userCreateService = userCreateService;
            _postQueryService = postQueryService;
            _postDeleteService = postDeleteService;
            _postCreateService = postCreateService;
        }

        public IActionResult Index()
        {
            // !! here we still need to find the user that is logged in, and handle if no user is logged in !!
            User loggedInUser = _userQueryService.getUser(1);
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel()
            {
                User = loggedInUser,
                PostsInFeed =
                    _postQueryService.getLatestPostsFromFollowingUserIDs(
                        _userQueryService.getFollowingUserIds(loggedInUser))
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
        public async Task<IActionResult> ToggleLikePost([FromBody] int postId)
        {
            var post = _postQueryService.getPostFromId(postId);

            if (post == null) return NotFound();

            var user = _userQueryService.getUser(1);
            var existingLike = _postQueryService.hasUserLikedPost(user.Id, postId);

            if (existingLike)
            {
                // If a like by the user already exists, remove it
                _postDeleteService.removeLikeFromPost(postId, user.Id);
            }
            else
            {
                _postCreateService.addLikeToPost(postId, user.Id);
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPostLikes([FromQuery] int postId)
        {
            var post = _postQueryService.getPostFromId(postId);

            if (post == null) return NotFound();

            var user = _userQueryService.getUser(1);

            var isLiked = _postQueryService.hasUserLikedPost(user.Id, postId);

            var likeCount = post.Likes.Count;

            return Json(new { likeCount = likeCount, isLiked = isLiked });
        }
    }
}