using FriendFace.Data;

namespace FriendFace.Services.DatabaseService;

public class CommentCreateService
{
    private readonly ApplicationDbContext _context;
    
    public CommentCreateService(ApplicationDbContext context)
    {
        _context = context;
    }
}