using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FriendFace.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [MaxLength(100)]
    public string Password { get; set; }

    // Navigation properties
    public virtual ICollection<UserFollowsUser> Following { get; set; }
    public virtual ICollection<UserFollowsUser> Followers { get; set; }
    
    public virtual ICollection<Post> Posts { get; set; }

    public virtual ICollection<UserLikesPost> Likes { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }
}