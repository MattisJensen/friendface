document.addEventListener('DOMContentLoaded', function() {
    var cancelButton = document.getElementById('postContent-button-cancel');

    cancelButton.addEventListener('click', function(event) {
        postCancel();
    });
});

function postCancel() {
    $('#postCreateContainer').hide();
    $('#postContent-publishField').val('');
    $('#postContent-publishField').get(0).readOnly = true;
    $('#postContent-chars').text('0');
    $('#postContent-button-publish').attr('disabled', true);
    $('#post-btn').removeAttr('disabled');
}