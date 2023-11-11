using FriendFace.Data;

namespace FriendFace.Services.DatabaseService;

public class LikeCreateService
{
    private readonly ApplicationDbContext _context;
    
    public LikeCreateService(ApplicationDbContext context)
    {
        _context = context;
    }
}