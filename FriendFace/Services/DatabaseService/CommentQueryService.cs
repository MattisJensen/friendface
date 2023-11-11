using FriendFace.Data;

namespace FriendFace.Services.DatabaseService;

public class CommentQueryService
{
    private readonly ApplicationDbContext _context;
    
    public CommentQueryService(ApplicationDbContext context)
    {
        _context = context;
    }
}