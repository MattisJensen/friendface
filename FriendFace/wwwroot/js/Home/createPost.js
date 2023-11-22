function createPost() {
    $.ajax({
        type: "GET",
        url: "/Home/GetPostCharLimit",
        success: function (data) {
            var postCharacterLimit = data;
            var postCreateContainer = document.getElementById('postCreateContainer');
            var postContentEditField = document.getElementById('postContent-create');
            var postButton = document.getElementById('post-btn');

            // Publish button settings
            postButton.disabled = true;

            // Post field settings
            postCreateContainer.style.display = 'block';
            postContentEditField.textContent = '';
            postContentEditField.setAttribute('contenteditable', true);
            postContentEditField.focus();

            // New elements
            var publishButton = document.createElement('button');
            publishButton.id = 'publishButton-create';
            publishButton.innerHTML = '<i class="fas fa-arrow-up"></i> Publish Post';
            publishButton.className = 'btn btn-success btn-sm mt-2 mb-4 me-2';
            publishButton.addEventListener('click', function () {
                publishPost(postCreateContainer, postContentEditField, postButton, publishButton, cancelButton, charCountText);
            });

            var cancelButton = document.createElement('button');
            cancelButton.id = 'cancelButton-create';
            cancelButton.textContent = 'Cancel';
            cancelButton.className = 'btn btn-secondary btn-sm mt-2 mb-4';
            cancelButton.addEventListener('click', function () {
                cancel(postCreateContainer, postContentEditField, postButton, publishButton, cancelButton, charCountText);
            });

            var charCountText = document.createElement('span');
            charCountText.id = 'charCount-create';
            charCountText.textContent = postContentEditField.textContent.length + '/' + postCharacterLimit + ' chars';
            charCountText.className = 'text-muted small d-flex';

            postContentEditField.addEventListener('input', function () {
                // Update character count text
                charCountText.textContent = postContentEditField.textContent.length + '/' + postCharacterLimit + ' chars ';

                // Disable publish button if content length exceeds char limit
                publishButton.disabled = postContentEditField.textContent.length > postCharacterLimit;
            });

            // Add objects to post (adding order matters)
            postContentEditField.insertAdjacentElement('afterend', cancelButton);
            postContentEditField.insertAdjacentElement('afterend', publishButton);
            postContentEditField.insertAdjacentElement('afterend', charCountText);
        },
        error: function (error) {
            console.error("Error fetching post character limit: ", error);
        }
    });
}

function publishPost(postCreateContainer, postContentEditField, postButton, publishButton, cancelButton, charCountText) {
    $.ajax({
        url: '/Home/CreatePost',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(postContentEditField.textContent),
        dataType: 'json',
        success: function (data) {
            $("#profile").prepend(data);
            
            cancel(postCreateContainer, postContentEditField, postButton, publishButton, cancelButton, charCountText);
        },
        error: function (error) {
            console.error("Error creating post: ", error);
        }
    });
}

function cancel(postCreateContainer, postContentEditField, postButton, publishButton, cancelButton, charCountText) {
    postButton.disabled = false;

    publishButton.remove();
    cancelButton.remove();
    charCountText.remove();

    postCreateContainer.style.display = 'none';
    postContentEditField.setAttribute('contenteditable', 'false');
    postContentEditField.blur();
}

