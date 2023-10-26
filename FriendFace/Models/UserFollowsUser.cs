using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendFace.Models;

public class UserFollowsUser
{
    [Key] public int Id { get; set; }

    [ForeignKey("FollowerId")] public int FollowerId { get; set; }

    [ForeignKey("FollowedUserId")] public int FollowedUserId { get; set; }

    [ForeignKey("FollowerId")] public virtual User Follower { get; set; }

    [ForeignKey("FollowedUserId")] public virtual User FollowedUser { get; set; }
}