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


function postDelete(deleteField, postId) {
    event.stopPropagation(); // Prevents dropdown from closing

    // Changes text and turn it red
    deleteField.innerText = 'Confirm';
    deleteField.style.color = 'rgb(255,255,255)';
    deleteField.style.backgroundColor = 'rgb(220,53,69)';


    // Add listener to execute delete request
    deleteField.addEventListener('click', deleteRequest);

    // Add listener to reset the delete text when the dropdown is closed
    document.addEventListener('click', function (e) {
        if (!e.target.closest('.dropdown-menu')) resetDeleteText(postId); // Checks if click is outside of dropdown menu
    });

    function deleteRequest() {
        window.location.href = '/Home/DeletePost?postId=' + postId;
    }

    function resetDeleteText(postId) {
        deleteField.innerText = 'Delete';
        // reset color to default
        deleteField.style.color = '';
        deleteField.style.backgroundColor = '';

        deleteField.removeEventListener('click', deleteRequest);
    }
}
