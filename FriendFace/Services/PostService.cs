using FriendFace.Data;
using FriendFace.Models;
using FriendFace.Services.DatabaseService;
using FriendFace.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

public class PostService
{
    private readonly PostCreateService _postCreateService;
    private readonly PostQueryService _postQueryService;
    private readonly PostUpdateService _postUpdateService;
    private readonly PostDeleteService _postDeleteService;
    private readonly UserQueryService _userQueryService;

    public PostService(PostCreateService postCreateService, PostQueryService postQueryService,
        PostUpdateService postUpdateService, PostDeleteService postDeleteService, UserQueryService userQueryService)
    {
        _postCreateService = postCreateService;
        _postQueryService = postQueryService;
        _postUpdateService = postUpdateService;
        _postDeleteService = postDeleteService;
        _userQueryService = userQueryService;
    }

    public void ToggleLikePost(int postId)
    {
        var post = _postQueryService.GetPostFromId(postId);

        if (post == null) throw new InvalidOperationException("Post not found");

        var user = _userQueryService.GetLoggedInUser();

        try
        {
            var existingLike = _postQueryService.HasUserLikedPost(user.Id, postId);

            if (existingLike)
            {
                // If a like by the user already exists, remove it
                _postDeleteService.RemoveLikeFromPost(postId, user.Id);
            }
            else
            {
                _postCreateService.AddLikeToPost(postId, user.Id);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while toggling the like on the post. ", ex);
        }
    }

    public object GetPostLikes(int postId)
    {
        var post = _postQueryService.GetPostFromId(postId);

        if (post == null) throw new InvalidOperationException("Post not found");

        var user = _userQueryService.GetLoggedInUser();

        try
        {
            var isLiked = _postQueryService.HasUserLikedPost(user.Id, postId);
            var likeCount = _postQueryService.GetNumberOfLikes(postId);

            return new { likeCount = likeCount, isLiked = isLiked };
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving post likes", ex);
        }
    }

    public object DeletePost(int postId)
    {
        try
        {
            // Check if the logged-in user is the owner of the post
            var loggedInUser = _userQueryService.GetLoggedInUser();
            var post = _postQueryService.GetPostFromId(postId);

            if (post.UserId == loggedInUser.Id)
            {
                if (_postDeleteService.SoftDeletePost(postId))
                {
                    return new { success = true };
                }
                else
                {
                    return new { success = false, message = "An error occurred while deleting the post." };
                }
            }
            else
            {
                return new { success = false, message = "You do not have permission to delete this post." };
            }
        }
        catch (Exception ex)
        {
            return new { success = false, message = "An error occurred while deleting the post." };
        }
    }

    public object EditPost(PostIdContentModel model)
    {
        try
        {
            int postId = model.PostId;
            string editedContent = model.Content;

            // Check if the logged-in user is the owner of the post
            var loggedInUser = _userQueryService.GetLoggedInUser();
            var post = _postQueryService.GetPostFromId(postId);

            if (post.UserId == loggedInUser.Id)
            {
                // Check if the edited content is within the character limit
                if (editedContent.Length <= _postQueryService.GetPostCharacterLimit())
                {
                    return new { success = _postUpdateService.UpdatePost(postId, editedContent) };
                }
                else
                {
                    return new
                    {
                        success = false,
                        message = "Edited content exceeds " + _postQueryService.GetPostCharacterLimit() + " characters."
                    };
                }
            }
            else
            {
                return new { success = false, message = "You do not have permission to edit this post." };
            }
        }
        catch (Exception ex)
        {
            return new { success = false, message = "An error occurred while editing the post." };
        }
    }

    public string CreatePost(string content, ControllerContext controllerContext)
    {
        try
        {
            // Check if the logged-in user is the owner of the post
            var loggedInUser = _userQueryService.GetLoggedInUser();

            if (loggedInUser != null && loggedInUser.Id > 0)
            {
                // Check if the edited content is within the character limit
                if (content.Length <= _postQueryService.GetPostCharacterLimit() &&
                    _postCreateService.CreatePost(content, loggedInUser))
                {
                    var latestPost = _postQueryService.GetLatestPostFromUserId(loggedInUser.Id);

                    var postPartialHtml = RenderViewToString(controllerContext, "_PostProfilePartial",
                        new HomeIndexViewModel
                        {
                            PostsByLoggedInUser = new List<Post> { latestPost },
                            User = loggedInUser
                        });

                    return postPartialHtml;
                }
                else
                {
                    throw new Exception("Content exceeds " + _postQueryService.GetPostCharacterLimit() +
                                        " characters.");
                }
            }
            else
            {
                throw new Exception("You do not have permission to create this post.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating the post.", ex);
        }
    }

    private string RenderViewToString(ControllerContext controllerContext, string viewName, object model)
    {
        using (var sw = new StringWriter())
        {
            var engine = controllerContext.HttpContext.RequestServices.GetRequiredService<IRazorViewEngine>();
            var viewResult = engine.FindView(controllerContext, viewName, false);
        
            if (viewResult.View == null)
            {
                throw new ArgumentNullException($"{viewName} does not match any available view");
            }

            var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };

            var viewContext = new ViewContext(controllerContext, viewResult.View,
                viewDictionary,
                new TempDataDictionary(controllerContext.HttpContext, controllerContext.HttpContext.RequestServices.GetRequiredService<ITempDataProvider>()),
                sw,
                new HtmlHelperOptions());

            var t = viewResult.View.RenderAsync(viewContext);

            t.Wait();

            return sw.GetStringBuilder().ToString();
        }
    }
}