using FriendFace.Data;

namespace FriendFace.Services.DatabaseService;

public class LikeDeleteService
{
    private readonly ApplicationDbContext _context;
    
    public LikeDeleteService(ApplicationDbContext context)
    {
        _context = context;
    }
}