using FriendFace.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FriendFace.Services.DatabaseService;

public class UserQueryService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserQueryService(ApplicationDbContext context, UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public User GetLoggedInUser()
    {
        // Check if user is logged in
        if (_signInManager.IsSignedIn(_signInManager.Context.User))
        {
            // Get the user from the database
            var user = _userManager.GetUserAsync(_signInManager.Context.User).Result;
            return user;
        }
        else
        {
            // User is not logged in
            return null;
        }
    }

    public User GetUserById(int userId)
    {
        return _context.Users
            .Include(u => u.Following) // Load the users UserA follows
            .Include(u => u.Followers) // Load the users following UserA
            .FirstOrDefault(u => u.Id == userId) ?? throw new InvalidOperationException();
    }

    public List<int> GetFollowingUserIds(int userId)
    {
        var user = GetUserById(userId);
        return user.Following.Select(f => f.FollowingId).ToList();
    }
    
    public List<User> GetUsersBySearch(string search)
    {
        // Split the search string into terms
        var searchTerms = search.ToLower().Split(" ").ToList();
        var searchResult = new List<User>();
        
        foreach (var term in searchTerms)
        {
            var resultsForTerm = _context.Users
                .Where(user =>
                    user.UserName.ToLower().Contains(term) ||
                    user.FirstName.ToLower().Contains(term) ||
                    user.LastName.ToLower().Contains(term) ||
                    user.Email.ToLower().Contains(term)
                )
                .ToList();

            searchResult.AddRange(resultsForTerm);
        }
    
        return searchResult.Distinct().ToList();
    }
}