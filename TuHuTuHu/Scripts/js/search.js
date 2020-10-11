$(document).ready(function () {
    $("#searchText").keypress(function (event) {
        if (event.which == '13') {
            return false;
        }
    });
})