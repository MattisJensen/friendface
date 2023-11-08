using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Npgsql;
using Microsoft.Extensions.Configuration;
using NpgsqlTypes;

namespace FriendFace.Controllers
{
    public class LoginController : Controller
    {
        /*private static IConfiguration _config = new ConfigurationBuilder() // Uses secrets. One needs to have a secrets.json file setup: https://blog.jetbrains.com/dotnet/2023/01/17/securing-sensitive-information-with-net-user-secrets/
            .AddUserSecrets<Program>()
            .Build();
        
        private static string _connString = _config["connectionString"]; // Retrieves the connection string as a stored secret, on the format: "Host=cornelius.db.elephantsql.com:5432;Username=<username>;Password=<password;Database=<database>"
        */
        
        // Retrieve connection string from appsettings.json
        private static IConfiguration _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        private static string _connString = _config.GetConnectionString("FriendFaceIdentityDbContextConnection");

        
        
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
            await using var conn = new SqlConnection(_connString);
            await conn.OpenAsync();


            string username = Request.Form["uname"];
            string password = Request.Form["psw"]; // This far, the password is probably plain-text.
            string email = Request.Form["email"];
            
            Console.WriteLine(email);

            bool affectedOneRow;
            
            // Sanitize input
            // ...
            
            
            // Insert User to MySQL DB
            await using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO users (username, password, email) VALUES (@username, crypt(@password, gen_salt('bf')), @email)";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);

                affectedOneRow = await cmd.ExecuteNonQueryAsync() == 1;
            }
            
            
            // Verify insertion
            await using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT id FROM users WHERE username = @username";
                cmd.Parameters.AddWithValue("@username", username);

                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var id = reader.GetInt32(0);
                        // ...
                    }
                }
            }
            

            return affectedOneRow;
        }
    }
}
