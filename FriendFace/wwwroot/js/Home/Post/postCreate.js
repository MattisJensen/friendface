function postCreate() {
    $('#postCreateContainer').show();
    $('#post-btn').attr('disabled', true);

    let postContentPublishField = $('#postContent-publishField');
    postContentPublishField.focus();
    
    let charsInPublishFieldPlaceholder = $('#postContent-chars');
    let charLimit = Number($('#postContent-chars-limit').text());
    let publishButton = $('#postContent-button-publish');

    postContentPublishField.on('input', function () {
        // Update character count text
        charsInPublishFieldPlaceholder.text(postContentPublishField.val().length);

        // Disable publish button if content length exceeds char limit
        publishButton.attr('disabled', postContentPublishField.val().length > charLimit);
    });
}

