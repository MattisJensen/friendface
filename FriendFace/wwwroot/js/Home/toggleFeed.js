document.addEventListener('DOMContentLoaded', function () {
    attachEditFieldListeners();
});

function attachEditFieldListeners() {
    var followingFeedButton = $('#following-feed');
    followingFeedButton.on('click', function (event) {
        followingFeed();
    });

    var profileFeedButton = $('#profile-feed');
    profileFeedButton.on('click', function (event) {
        profileFeed();
    });
}

function followingFeed() {
    $('#feed').show();
    $('#profile').hide();
}

function profileFeed() {
    $('#feed').hide();
    $('#profile').show();
}