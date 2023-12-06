document.addEventListener('DOMContentLoaded', function () {
    var form = $('#editContent-form');
    form.action = "/Home/EditPost";
    form.method = "post";

    var editFields = $('[id^="editField-"]');

    // Attach listener to each delete field
    editFields.each(function () {
        $(this).on('click', function (event) {
            event.preventDefault();
            var postId = this.id.split('-')[1];
            postEdit(postId);
        });
    });
});

function postEdit(postId) {
    var saveButton = $('#editContent-button-save-' + postId);
    var cancelButton = $('#editContent-button-cancel-' + postId);

    var charContainer = $('#editContent-charcounter-container-' + postId);
    var charCountText = $('#editContent-chars-' + postId);
    var charLimit = postCharLimit;

    var menuButton = $('#postMenuButton-' + postId);
    var editField = $('#editContent-editField-' + postId);

    saveButton.removeAttr('disabled');
    cancelButton.removeAttr('disabled');
    charCountText.text(editField.val().length);

    editField.prop('readOnly', false);
    editField.addClass('input-field');
    editField.focus();

    saveButton.removeClass('d-none');
    cancelButton.removeClass('d-none');
    charContainer.removeClass('d-none');

    menuButton.addClass('d-none');

    let originalPostContent = editField.val();

    cancelButton.on('click', function (event) {
        leaveEditMode(postId, originalPostContent);
    });
    
    editField.on('input', function () {
        charCountText.text(editField.val().length); // Update character count text
        saveButton.prop('disabled', editField.val().length > charLimit); // Disable save button if content length exceeds char limit
    });
}

function leaveEditMode(postId, originalPostContent) {
    var saveButton = $('#editContent-button-save-' + postId);
    var cancelButton = $('#editContent-button-cancel-' + postId);

    var charContainer = $('#editContent-charcounter-container-' + postId);
    var charCountText = $('#editContent-chars-' + postId);

    var menuButton = $('#postMenuButton-' + postId);
    var editField = $('#editContent-editField-' + postId);

    saveButton.addClass('d-none');
    saveButton.prop('disabled', true);
    cancelButton.addClass('d-none');
    cancelButton.prop('disabled', true);
    charContainer.addClass('d-none');

    menuButton.removeClass('d-none');

    charCountText.text(0);
    editField.prop('readOnly', true);
    editField.removeClass('input-field');
    editField.val(originalPostContent);
}
