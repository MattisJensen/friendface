function postPublish() {
    const postContentPublishField = $('#postContent-publishField');

    $.ajax({
        url: '/Home/CreatePost',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(postContentPublishField.textContent),
        dataType: 'json',
        success: function (data) {
            $("#profile").prepend(data);
        },
        error: function (error) {
            console.error("Error creating post: ", error);
        }
    });
}