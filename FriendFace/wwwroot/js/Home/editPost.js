function editPost(postId) {
    
    // Make the post content editable
    let postContentItem = $('#postContent-' + postId);
    postContentItem.attr('contenteditable', true);
    postContentItem.css({
        'background-color': 'rgb(231,231,231)',
        'border-radius': '5px',
        'padding': '10px',
        'outline': '0'
    });

    $('#postMenuButton-' + postId).hide();

    // Save the original post content in case the user cancels the edit
    let originalPostContent = postContentItem.text();

    // Create save & cancel button
    var saveButton = $('<button>', {
        id: 'saveButton-' + postId,
        text: 'Save',
        class: 'btn btn-success btn-sm mt-2 me-2',
        click: function () {
            savePost(postId);
        }
    });

    var cancelButton = $('<button>', {
        id: 'cancelButton-' + postId,
        text: 'Cancel',
        class: 'btn btn-secondary btn-sm mt-2 me-2',
        click: function () {
            leaveEditMode(postId, originalPostContent);
        }
    });

    var charCountText = $('<span>', {
        id: 'charCount-' + postId,
        text: postContentItem.text().length + '/280 chars',
        class: 'text-muted small d-flex align-items-left'
    });

    postContentItem.on('input', function () {
        // Update character count text
        charCountText.text(postContentItem.text().length + '/280 chars');

        // Disable save button if content length exceeds 280 characters
        saveButton.prop('disabled', postContentItem.text().length > 280);
    });

    // Append save & cancel button
    $('#postContainer-' + postId).append(saveButton);
    $('#postContainer-' + postId).append(cancelButton);
    $('#postContainer-' + postId).append(charCountText);
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
    $('#postMenuButton-' + postId).show();
    $('#saveButton-' + postId).remove();
    $('#cancelButton-' + postId).remove();
    $('#charCount-' + postId).remove();

    let postContentItem = $('#postContent-' + postId);
    postContentItem.attr('contenteditable', false);
    postContentItem.css({
        'background-color': '',
        'border-radius': '',
        'padding': '',
        'outline': ''
    });
    postContentItem.text(originalPostContent);

}

