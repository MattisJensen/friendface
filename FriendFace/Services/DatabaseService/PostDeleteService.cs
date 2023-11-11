using FriendFace.Data;

namespace FriendFace.Services.DatabaseService;

public class PostDeleteService
{
    private readonly ApplicationDbContext _context;
    
    public PostDeleteService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // removes like from post
    public void removeLikeFromPost(int postId, int userId)
    {
        var existingLike = _context.UserLikesPosts.SingleOrDefault(like =>
            like.UserId == userId && like.PostId == postId);

        if (existingLike != null)
        {
            _context.UserLikesPosts.Remove(existingLike);
            _context.SaveChanges();
        }
    }
}