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
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public LoginController(ApplicationDbContext context, SignInManager<User> signInManager, 
                                UserManager<User> userManager)
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
            Console.WriteLine(result);

            if (result.Succeeded)
            {   
                Console.Write("Login successful!");
                // Redirect the user to the return URL or a default page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Authentication has failed. Show login form with error message.
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return RedirectToAction("Index", "Home");
            }
        }

        
        // Registration-page
        public async Task<IActionResult> DoRegister(string uname, string psw, string email)
        {
            var user = new User { UserName = uname, FirstName = "Test", LastName = "Test", Email = email };
            var result = await _userManager.CreateAsync(user, psw);

            if (result.Succeeded)
            {
                await _signInManager.PasswordSignInAsync(user.UserName, psw, isPersistent: true, lockoutOnFailure: false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View("Register"); // Adjust as necessary
            }
        }

    }
}
