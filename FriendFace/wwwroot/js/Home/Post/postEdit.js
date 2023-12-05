function postEdit(postId) {
    $.ajax({
        type: "GET",
        url: "/Home/GetPostCharLimit", 
        success: function (data) {
            var postCharacterLimit = data;

            // Make the post content editable
            let postContentItem = $('#postContent-' + postId);
            postContentItem.attr('contenteditable', true);
            postContentItem.addClass('input-field');

            $('#postMenuButton-' + postId).hide();

            // Save the original post content in case the user cancels the edit
            let originalPostContent = postContentItem.text();

            // Create save & cancel button
            var saveButton = $('<button>', {
                id: 'saveButton-' + postId,
                text: 'Save',
                class: 'btn btn-success btn-sm mt-2 me-2 mb-4',
                type: 'submit',
                click: function () {
                    savePost(postId);
                }
            });

            var cancelButton = $('<button>', {
                id: 'cancelButton-' + postId,
                text: 'Cancel',
                class: 'btn btn-secondary btn-sm mt-2 mb-4',
                click: function () {
                    leaveEditMode(postId, originalPostContent);
                }
            });

            var charCountText = $('<span>', {
                id: 'charCount-' + postId,
                text: postContentItem.text().length + '/' + postCharacterLimit + ' chars',
                class: 'text-muted small d-flex'
            });
            
            postContentItem.on('input', function () {
                // Update character count text
                charCountText.text(postContentItem.text().length + '/' + postCharacterLimit + ' chars ');

                // Disable save button if content length exceeds char limit
                saveButton.prop('disabled', postContentItem.text().length > postCharacterLimit);
            });

            // Add objects to post (adding order matters)
            postContentItem.after(cancelButton);
            postContentItem.after(saveButton);
            postContentItem.after(charCountText);
        },
        error: function (error) {
            console.error("Error fetching post character limit: ", error);
        }
    });
}

function savePost(postId, originalPostContent) {
    var editedContent = $('#postContent-' + postId).text();

    var postData = {
        PostId: postId,
        EditedContent: editedContent
    };

    // AJAX request to save the edited content
    $.ajax({
        url: '/Home/EditPost',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(postData),
        dataType: 'json',
        success: function (data) {
            if (data.success) {
                $('#postContent-' + postId).text(editedContent);
                console.error('3');
            }
            leaveEditMode(postId)
        },
        error: function (xhr, status, error) {
            console.error('Error editing post:', error);
            leaveEditMode(postId, originalPostContent)
        }
    });
}

function leaveEditMode(postId, originalPostContent) {
    let postContentItem = $('#postContent-' + postId);
    $('#postMenuButton-' + postId).show();
    $('#saveButton-' + postId).remove();
    $('#cancelButton-' + postId).remove();
    $('#charCount-' + postId).remove();

    postContentItem.attr('contenteditable', false);
    postContentItem.css({
        'background-color': '',
        'border-radius': '',
        'padding': '',
        'outline': ''
    });
    postContentItem.text(originalPostContent);

}

