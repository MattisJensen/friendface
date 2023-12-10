document.addEventListener('DOMContentLoaded', function () {
    // Attach listener to each follow button
    var followButtons = $('[id^="followButton-"]');

    followButtons.each(function () {
        $(this).on('click', function (event) {
            var userId = this.id.split('-')[1];
            var action = "follow";
            toggleFollow(action, userId);
        });
    });
    
    // Attach listener to each unfollow button
    var unfollowButtons = $('[id^="unfollowButton-"]');

    unfollowButtons.each(function () {
        $(this).on('click', function (event) {
            var userId = this.id.split('-')[1];
            var action = "unfollow";
            toggleFollow(action, userId);
        });
    });
    
    // Attach listener to each result
    var userSearchContainers = $('[id^="userSearchContainer-"]');

    userSearchContainers.each(function () {
        $(this).on('click', function (event) {
            var userId = this.id.split('-')[1];
            openProfile(userId);
        });
    });
});

function toggleFollow(action, userId) {
    if (action === "follow") {
        window.location.href = '/Controller/MethodName?methodParameter=' + userId;
    } else if (action === "unfollow") {
        window.location.href = '/Controller/MethodName?methodParameter=' + userId;
    }
}

function openProfile(userId) {
    window.location.href = "/Home/Index/";
}



