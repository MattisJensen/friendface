using FriendFace.Data;
using FriendFace.Models;

namespace FriendFace.Services.DatabaseService;

public class CommentCreateService
{
    private readonly ApplicationDbContext _context;
    private readonly PostQueryService _postQueryService;

    public CommentCreateService(ApplicationDbContext context)
    {
        _context = context;
        _postQueryService = new PostQueryService(context);
    }

    public bool CreateCommment(string content, int postId, User sourceUser)
    {
        const int maxSize = 280;
        if (content.Length > maxSize) throw new Exception("Comment content too long.");

        try
        {
            try
            {
                Post targetPost = _postQueryService.GetPostFromId(postId);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
                throw;
            }

            var comment = new Comment
            {
                Content = content,
                User = sourceUser,
                PostId = postId,
                Time = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}