document.addEventListener('DOMContentLoaded', function () {
    var editFields = document.querySelectorAll('[id^="editField-"]');

    // Attach listener to each delete field
    editFields.forEach(function (editField) {
        editField.addEventListener('click', function (event) {
            event.preventDefault(); // Prevent page from scrolling to top because of href="#"
            var postId = editField.id.split('-')[1];
            postEdit(postId, );
        });
    });
});

function postEdit(postId) {
    var saveButton = document.getElementById('editContent-button-save-' + postId);
    var cancelButton = document.getElementById('editContent-button-cancel-' + postId);

    var charContainer = document.getElementById('editContent-charcounter-container-' + postId);
    var charCountText = document.getElementById('editContent-chars-' + postId);
    var charLimit = postCharLimit; // Defined in Index.cshtml

    var menuButton = document.getElementById('postMenuButton-' + postId);
    var editField = document.getElementById('editContent-editField-' + postId);
    
    saveButton.removeAttribute('disabled');
    cancelButton.removeAttribute('disabled');
    charCountText.textContent = editField.value.length;
    
    editField.readOnly = false;
    editField.classList.add('input-field');
    editField.focus();
    
    saveButton.classList.remove('d-none');
    cancelButton.classList.remove('d-none');
    charContainer.classList.remove('d-none');
    
    menuButton.classList.add('d-none');

    // Save the original post content in case user cancels the edit
    let originalPostContent = editField.value;
    
    cancelButton.addEventListener('click', function (event) {
        leaveEditMode(postId, originalPostContent);
    });
    

    editField.addEventListener('input', function () {
        // Update character count text
        charCountText.textContent = editField.value.length;

        // Disable save button if content length exceeds char limit
        saveButton.disabled = editField.value.length > charLimit;
    });
}

function leaveEditMode(postId, originalPostContent) {
    var saveButton = document.getElementById('editContent-button-save-' + postId);
    var cancelButton = document.getElementById('editContent-button-cancel-' + postId);

    var charContainer = document.getElementById('editContent-charcounter-container-' + postId);
    var charCountText = document.getElementById('editContent-chars-' + postId);

    var menuButton = document.getElementById('postMenuButton-' + postId);
    var editField = document.getElementById('editContent-editField-' + postId);
    
    saveButton.classList.add('d-none');
    saveButton.disabled = true;
    cancelButton.classList.add('d-none');
    cancelButton.disabled = true;
    charContainer.classList.add('d-none');

    menuButton.classList.remove('d-none');

    charCountText.textContent = 0;
    editField.readOnly = true;
    editField.classList.remove('input-field');
    editField.value = originalPostContent;
}

