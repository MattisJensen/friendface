using FriendFace.Data;
using Microsoft.EntityFrameworkCore;

namespace FriendFace.Services.DatabaseService;

public class UserQueryService
{
    public static User getUser(ApplicationDbContext context, int userId)
    {
        return context.Users
            .Include(u => u.Following) // Load the users UserA follows
            .Include(u => u.Followers) // Load the users following UserA
            .FirstOrDefault(u => u.Id == userId);
    }
    
    public static List<int> getFollowingUserIds(User user)
    {
        return user.Following.Select(f => f.FollowingId).ToList();
    }
    
    
}