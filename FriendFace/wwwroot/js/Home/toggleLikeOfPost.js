function toggleLikeOfPost(heartIcon) {
    var postId = heartIcon.getAttribute('data-post-id');

    $.ajax({
        type: 'POST',
        url: '/Home/ToggleLikePost',
        contentType: 'application/json',
        data: JSON.stringify(postId),
        success: function () {
            // If like/unlike successful, fetch and update the likes
            $.ajax({
                type: 'GET',
                url: `/Home/GetPostLikes?postId=${postId}`,
                success: function (data) {
                    document.getElementById(`likeCount-${postId}`).textContent = data.likeCount;
                    heartIcon.className = data.isLiked ? "fas fa-heart" : "far fa-heart";
                    heartIcon.style.color = data.isLiked ? "red" : "inherit";
                },
                error: function (error) {
                    console.error('Failed to fetch like information:', error);
                }
            });
        },
        error: function (error) {
            console.error('Failed to like/unlike post:', error);
        }
    });
}
