

public class EditProfileViewModel

{

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Bio { get; set; }

    // This will be used just to display the existing profile picture
    public string ProfilePicturePath { get; set; }

    // This will be used to upload a new profile picture
    // public HttpPostedFileBase ProfilePicture { get; set; }
}
