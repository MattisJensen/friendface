document.addEventListener('DOMContentLoaded', function () {
    attachEditFieldListeners();
});

function attachEditFieldListeners() {
    var followButtons = $('[id^="followButton-"]');
    var unfollowButtons = $('[id^="unfollowButton-"]');
    var userSearchContainers = $('[id^="userSearchContainer-"]');
    // Remove existing listeners to prevent multiple listeners being attached to one field
    followButtons.off('click');
    unfollowButtons.off('click');
    userSearchContainers.off('click');
    
    // Attach listener to each follow button
    followButtons.each(function () {
        $(this).on('click', function (event) {
            var userId = this.id.split('-')[1];
            var action = "follow";
            toggleFollow(action, userId);
        });
    });

    // Attach listener to each unfollow button
    unfollowButtons.each(function () {
        $(this).on('click', function (event) {
            var userId = this.id.split('-')[1];
            var action = "unfollow";
            toggleFollow(action, userId);
        });
    });

    // Attach listener to each result
    userSearchContainers.each(function () {
        $(this).on('click', function (event) {
            var userId = this.id.split('-')[1];
            openProfile(userId);
        });
    });
}

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



