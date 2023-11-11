async function toggleLikeOfPost(heartIcon) {
    var postId = heartIcon.getAttribute('data-post-id');
    console.log('postId:', postId);

    try {
        const response = await fetch('/Home/ToggleLikePost', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(postId)
        });

        if (!response.ok) {
            throw new Error('Failed to like/unlike post');
        }

        const dataResponse = await fetch(`/Home/GetPostLikes?postId=${postId}`);
        const data = await dataResponse.json();

        document.getElementById(`likeCount-${postId}`).textContent = data.likeCount;
        heartIcon.className = data.isLiked ? "fas fa-heart" : "far fa-heart";
        heartIcon.style.color = data.isLiked ? "red" : "inherit";
    } catch (error) {
        console.error('There was an error:', error);
    }
}
