function postDelete(postId) {

    event.stopPropagation();

    // Find the delete text field
    var deleteField = document.getElementById('deleteField-' + postId);

    // Changes text and turn it red
    deleteField.innerText = 'Confirm';
    deleteField.style.color = 'rgb(255,255,255)';
    deleteField.style.backgroundColor = 'rgb(220,53,69)';


    // Adds click event listener to execute delete request
    deleteField.addEventListener('click', deleteRequest);

    // Adds listener to reset the delete text when the dropdown is closed
    document.addEventListener('click', function (e) {
        if (!e.target.closest('.dropdown-menu')) resetDeleteText(postId); // Checks if click is outside of dropdown menu
    });

    function deleteRequest() {
        $.ajax({
            url: '/Home/DeletePost',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(postId),
            dataType: 'json',
            success: function (data) {
                $('#postContainer-' + postId).remove();
            },
            error: function () {
                console.error('Error deleting post.');
            }
        });
    }

    function resetDeleteText(postId) {
        deleteField.innerText = 'Delete';
        // reset color to default
        deleteField.style.color = ''; 
        deleteField.style.backgroundColor = '';

        deleteField.removeEventListener('click', deleteRequest);
    }
}
