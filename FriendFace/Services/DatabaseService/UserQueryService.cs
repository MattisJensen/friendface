using FriendFace.Data;
using Microsoft.EntityFrameworkCore;

namespace FriendFace.Services.DatabaseService;

public class UserQueryService
{
    private readonly ApplicationDbContext _context;
    
    public UserQueryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public User GetLoggedInUser()
    {
        return GetUserById(1);
    }
    
    public User GetUserById(int userId)
    {
        return _context.Users
            .Include(u => u.Following) // Load the users UserA follows
            .Include(u => u.Followers) // Load the users following UserA
            .FirstOrDefault(u => u.Id == userId);
    }
    
    public List<int> GetFollowingUserIds(User user)
    {
        return user.Following.Select(f => f.FollowingId).ToList();
    }
    
    
}