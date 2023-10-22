using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace FriendFace.Controllers
{
    public class LoginController : Controller
    {
        private static IConfiguration _config = new ConfigurationBuilder() // Uses secrets. One needs to have a secrets.json file setup: https://blog.jetbrains.com/dotnet/2023/01/17/securing-sensitive-information-with-net-user-secrets/
            .AddUserSecrets<Program>()
            .Build();
        
        private static string _connString = _config["connString"]; // Retrieves the connection string as a stored secret, on the format: "jdbc:postgresql://cornelius.db.elephantsql.com:5432/<database>"
        

        
        // Main login-page
        public IActionResult Index()
        {
            return View();
        }
        
        
        // Registration-page
        public async Task<bool> Register()
        {
            // Establish DB connection
            await using var dataSource = NpgsqlDataSource.Create(_connString); // Possibly have to call a different method, for login credentials.
            
            // Sanitize input
            // ...
            
            // Insert input data
            await using (var cmd = dataSource.CreateCommand("INSERT INTO "))
            {
                
            }
        }
    }
}
