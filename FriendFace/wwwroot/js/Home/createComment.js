// Create event listener for all comment buttons
document.addEventListener('DOMContentLoaded', function () {
    document.addEventListener('click', function (event) {
        // Check if the clicked element has a class or data attribute that identifies it as a comment button
        if (event.target && (event.target.classList.contains('comment-btn') || event.target.dataset.postId)) {
            createComment(event.target);
        }
    });
});

var commentBtnsClicked = [];

function createComment(comment) {
// Create a comment, reminiscent of the PostCreatePartial.
    const _postId = comment.getAttribute('data-post-id');
    $.ajax({
        type: "GET",
        url: "/Home/GetPostCharLimit",
        success: function (data) {
            var commentCharacterLimit = data;
            var commentCreateContainer = document.getElementById('commentCreateContainer-' + _postId);
            var commentContentEditField = document.getElementById('commentContent-create-' + _postId);
            var commentButton = document.getElementById('comment-btn-' + _postId);
            if(commentBtnsClicked.includes(_postId)) { // Prevents multiple comment boxes from being created for same post
                return;
            }
            commentBtnsClicked.push(_postId);

            // Publish button settings
            // commentButton.disabled = true;  // TODO: Replace this with func appropriate for comment button

            // comment field settings
            commentCreateContainer.style.display = 'block';
            commentContentEditField.textContent = '';
            commentContentEditField.setAttribute('contenteditable', true);
            commentContentEditField.focus();

            // New elements
            var publishButton = document.createElement('button');
            publishButton.id = 'publishButton-create';
            publishButton.innerHTML = '<i class="fas fa-arrow-up"></i> Publish Comment';
            publishButton.className = 'btn btn-success btn-sm mt-2 mb-4 me-2';
            publishButton.addEventListener('click', function () {
                publishcomment(commentCreateContainer, commentContentEditField, commentButton, publishButton, cancelButton, charCountText, _postId);
            });

            var cancelButton = document.createElement('button');
            cancelButton.id = 'cancelButton-create';
            cancelButton.textContent = 'Cancel';
            cancelButton.className = 'btn btn-secondary btn-sm mt-2 mb-4';
            cancelButton.addEventListener('click', function () {
                cancelComment(commentCreateContainer, commentContentEditField, commentButton, publishButton, cancelButton, charCountText, _postId);
            });

            var charCountText = document.createElement('span');
            charCountText.id = 'charCount-create';
            charCountText.textContent = commentContentEditField.textContent.length + '/' + commentCharacterLimit + ' chars';
            charCountText.className = 'text-muted small d-flex';

            commentContentEditField.addEventListener('input', function () {
                // Update character count text
                charCountText.textContent = commentContentEditField.textContent.length + '/' + commentCharacterLimit + ' chars ';

                // Disable publish button if content length exceeds char limit
                publishButton.disabled = commentContentEditField.textContent.length > commentCharacterLimit;
            });

            // Add objects to comment (adding order matters)
            commentContentEditField.insertAdjacentElement('afterend', cancelButton);
            commentContentEditField.insertAdjacentElement('afterend', publishButton);
            commentContentEditField.insertAdjacentElement('afterend', charCountText);
        },
        error: function (error) {
            console.error("Error fetching comment character limit: ", error);
        }
    });
}

function publishcomment(commentCreateContainer, commentContentEditField, commentButton, publishButton, cancelButton, charCountText, postId) {
    $.ajax({
        url: `/Home/CreateComment/?postId=${postId}`,
        type: 'comment',
        contentType: 'application/json',
        data: JSON.stringify(commentContentEditField.textContent),
        dataType: 'json',
        success: function (data) {
            // Add new comment to top of feed
            cancelComment(commentCreateContainer, commentContentEditField, commentButton, publishButton, cancelButton, charCountText, postId);
        },
        error: function (error) {
            console.error("Error creating comment: ", error);
        }
    });
}

function cancelComment(commentCreateContainer, commentContentEditField, commentButton, publishButton, cancelButton, charCountText, postId) {
    publishButton.remove();
    cancelButton.remove();
    charCountText.remove();

    commentCreateContainer.style.display = 'none';
    commentContentEditField.setAttribute('contenteditable', 'false');
    commentContentEditField.blur();
    commentBtnsClicked = commentBtnsClicked.filter(function(value){ // Remove this postId from array
        return value !== postId;
    })
}
