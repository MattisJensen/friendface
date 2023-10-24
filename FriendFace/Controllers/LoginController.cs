using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Microsoft.Extensions.Configuration;
using NpgsqlTypes;

namespace FriendFace.Controllers
{
    public class LoginController : Controller
    {
        private static IConfiguration _config = new ConfigurationBuilder() // Uses secrets. One needs to have a secrets.json file setup: https://blog.jetbrains.com/dotnet/2023/01/17/securing-sensitive-information-with-net-user-secrets/
            .AddUserSecrets<Program>()
            .Build();
        
        private static string _connString = _config["connString"]; // Retrieves the connection string as a stored secret, on the format: "Host=cornelius.db.elephantsql.com:5432;Username=<username>;Password=<password;Database=<database>"
        
        
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
        public async Task<bool> DoLogin()
        {
            // Establish DB connection
            await using var dataSource = NpgsqlDataSource.Create(_connString);
            
            // Sanitize input
            // ...
            
            string username = Request.Form["uname"];
            string password = Request.Form["psw"]; // This far, the password is probably plain-text.
            
            bool hasReturn;
            
            // Insert input data
            await using (var cmd = dataSource.CreateCommand("SELECT id FROM users WHERE username = $1 " +
                                                            "AND password = crypt($2, password)"))
            {
                cmd.Parameters.AddWithValue(NpgsqlDbType.Varchar, username);
                cmd.Parameters.AddWithValue(NpgsqlDbType.Varchar, password);

                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    hasReturn = reader.HasRows;
                }
            }

            return hasReturn;
        }
        
        // Registration-page
        public async Task<bool> DoRegister()
        {
            // Establish DB connection
            await using var dataSource = NpgsqlDataSource.Create(_connString);
            
            string username = Request.Form["uname"];
            string password = Request.Form["psw"]; // This far, the password is probably plain-text.
            string email = Request.Form["email"];

            bool affectedOneRow;
            
            // Sanitize input
            // ...
            
            
            // Insert input data
            await using (var cmd = dataSource.CreateCommand("INSERT INTO users (username, email, password) " +
                                                            "VALUES ($1, $2, crypt($3, gen_salt('bf')))")) // Do some database "sanitation" here. Think of what will happen if user already exists, for instance. 
            {
                cmd.Parameters.AddWithValue(NpgsqlDbType.Varchar, username);
                cmd.Parameters.AddWithValue(NpgsqlDbType.Varchar, email);
                cmd.Parameters.AddWithValue(NpgsqlDbType.Varchar, password);
                
                await cmd.ExecuteNonQueryAsync();
            }
            
            // Verify insertion
            await using (var cmd = dataSource.CreateCommand("SELECT id FROM users WHERE username = $1 " +
                                                            "AND password = crypt($2, password)"))
            {
                cmd.Parameters.AddWithValue(NpgsqlDbType.Varchar, username);
                cmd.Parameters.AddWithValue(NpgsqlDbType.Varchar, password);
            
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    affectedOneRow = reader.HasRows;;
                }
            }

            return affectedOneRow;
        }
    }
}
