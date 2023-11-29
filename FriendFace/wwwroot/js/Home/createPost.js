function createPost() {
    $('#postCreateContainer').show();
    $('#post-btn').attr('disabled', true);

    const postContentPublishField = $('#postContent-publishField');
    postContentPublishField.focus();
    
    const charsInPublishFieldPlaceholder = $('#postContent-chars');
    const charLimit = Number($('#postContent-chars-limit').text());
    const publishButton = $('#postContent-button-publish');

    postContentPublishField.on('input', function () {
        // Update character count text
        charsInPublishFieldPlaceholder.text(postContentPublishField.val().length);

        // Disable publish button if content length exceeds char limit
        publishButton.attr('disabled', postContentPublishField.val().length > charLimit);
    });
}

