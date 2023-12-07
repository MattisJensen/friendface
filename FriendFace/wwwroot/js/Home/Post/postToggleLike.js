document.addEventListener('DOMContentLoaded', function () {
    var likeButtons = $('[id^="likeButton-"]');

    // listener for each like field
    likeButtons.each(function () {
        $(this).on('click', function (event) {
            var postId = this.id.split('-')[1];
            toggleLike(postId);
        });
    });
});

function toggleLike(postId) {
    window.location.href = '/Home/ToggleLikePost?postId=' + postId;
}