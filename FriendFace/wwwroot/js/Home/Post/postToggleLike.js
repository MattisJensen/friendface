document.addEventListener('DOMContentLoaded', function () {
    var likeFields = $('[id^="likeField-"]');

    // listener for each like field
    likeFields.each(function () {
        $(this).on('click', function (event) {
            event.preventDefault(); // Prevent page from scrolling to top because of href="#"
            var postId = this.id.split('-')[1];
            toggleLike($(this), postId);
        });
    });
});

function toggleLike(deleteField, postId) {
    window.location.href = '/Home/DeletePost?postId=' + postId;
}