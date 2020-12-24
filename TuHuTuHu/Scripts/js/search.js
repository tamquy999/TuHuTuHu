$(document).ready(function () {
    $("#searchText").keypress(function (event) {
        if (event.which == '13') {
            return false;
        }
    });
})


function SearchText() {
    var searchInput = document.getElementById('searchText').value;
    searchInput = '/Search?searchString=' + searchInput;
    window.location.href = searchInput;
}