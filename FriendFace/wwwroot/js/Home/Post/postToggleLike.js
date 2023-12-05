document.addEventListener('DOMContentLoaded', function () {
    var deleteFields = document.querySelectorAll('[id^="deleteField-"]');

    // Attach listener to each delete field
    deleteFields.forEach(function (deleteField) {
        deleteField.addEventListener('click', function (event) {
            event.preventDefault(); // Prevent page from scrolling to top because of href="#"
            var postId = deleteField.id.split('-')[1];
            postDelete(deleteField, postId);
        });
    });
});


function toggleLike(deleteField, postId) {
    window.location.href = '/Home/DeletePost?postId=' + postId;