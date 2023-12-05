function postCancel() {
    $('#postCreateContainer').hide();
    $('#postContent-publishField').val('');
    $('#postContent-chars').text('0');
    $('#post-btn').removeAttr('disabled');
}