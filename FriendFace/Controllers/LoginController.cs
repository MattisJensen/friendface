using System.Threading.Tasks;
using FriendFace.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Microsoft.Extensions.Configuration;
using NpgsqlTypes;
using SignInResult = Microsoft.AspNetCore.Mvc.SignInResult;

namespace FriendFace.Controllers
{
    public class LoginController : Controller
    {   
        private static IConfiguration _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager, 
                                UserManager<IdentityUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        // Main login-page
        public IActionResult Index()
        {
            return View();
        }
        
        // Register new user page
        public IActionResult Register()
        {
            return View();
        }
        
        // Handling the log-in info
        public async Task<IActionResult> DoLogin(string uname, string psw)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(uname, psw, isPersistent: true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Redirect the user to the return URL or a default page
                return RedirectToAction("Index", "Home");
            }
            else if (result.IsLockedOut)
            {
                // Handle user lockout scenario
            }
            else
            {
                // Authentication has failed. Show login form with error message.
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        
        // Registration-page
        public async Task<IActionResult> DoRegister(string uname, string psw, string email)
        {
            
            
            // Create a new User object
            var user = new User { Username = uname, FirstName = "Test", LastName = "Test", Password = psw, Email = email };
        
            // Add the new user to the DbContext
            _context.Users.Add(user);
        
            // Save changes to the database
            var affectedRows = _context.SaveChanges();

            if (affectedRows == 1)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Username, user.Password, 
                                                            isPersistent: true, lockoutOnFailure: false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                throw new Exception("User was not created.");
            }
        }
    }
}
