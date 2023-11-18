function toggleFeed(feed) {
    if (feed === 'Feed') {
        $('#feed').show();
        $('#profile').hide();
    } else if (feed === 'Profile') {
        $('#feed').hide();
        $('#profile').show();
    }
}