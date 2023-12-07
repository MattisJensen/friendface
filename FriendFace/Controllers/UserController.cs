using FriendFace.Services.DatabaseService;
using FriendFace.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FriendFace.Controllers
{
    public class UserController : Controller
    {
        private readonly UserQueryService _userQueryService;
        private readonly ILogger<UserController> _logger;


        public UserController(ILogger<UserController> logger, UserQueryService userQueryService)
        {
            _userQueryService = userQueryService;
            _logger = logger;

        }

        public IActionResult Profile(int userId)
        {
            // Retrieve the profile user based on the userId.
            _logger.LogInformation("Profile action called with userId: {UserId}", userId);

            var profileUser = _userQueryService.GetSimpleUserById(10); // WHEN I HARDCODE 10 INSTEAD OF userId from method parameter IT WORKS
            if (profileUser == null)
            {
                _logger.LogWarning("No user found with userId: {UserId}", userId);
                return NotFound();
            }

            var loggedInUser = _userQueryService.GetLoggedInUser();
            bool isCurrentUser = loggedInUser != null && loggedInUser.Id == profileUser.Id;

            _logger.LogInformation("Profile found. Current user: {IsCurrentUser}", isCurrentUser);

            // Create the ViewModel for the view.
            var viewModel = new UserProfileViewModel
            {
                user = profileUser,
                isCurrentUser = isCurrentUser
                // Populate other properties of the ViewModel as needed.
            };

            // Pass the ViewModel to the view.
            return View("ViewProfile", viewModel);
        }


    }

}
