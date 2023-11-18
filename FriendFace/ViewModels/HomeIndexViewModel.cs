using FriendFace.Models;

namespace FriendFace.ViewModels;

public class HomeIndexViewModel
{
    // contains all the information needed for the home page
    public User User { get; set; }
    public List<Post> PostsInFeed { get; set; }
    public List<Post> PostsByLoggedInUser { get; set; }
}