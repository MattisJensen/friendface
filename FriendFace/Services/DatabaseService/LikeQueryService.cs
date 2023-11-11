using FriendFace.Data;

namespace FriendFace.Services.DatabaseService;

public class LikeQueryService
{
    private readonly ApplicationDbContext _context;
    
    public LikeQueryService(ApplicationDbContext context)
    {
        _context = context;
    }
}