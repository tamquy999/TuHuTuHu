function convertNumOfFollow(Following, Followed) {
    // var test = 1234556;
    var numFollowing = document.getElementById("numFollowing");
    numFollowing.innerHTML = ("<b>" + kFormatter(Following) + "</b>");

    var numFollowed = document.getElementById("numFollowed");
    numFollowed.innerHTML = "<b>" + kFormatter(Followed) + "</b>";
}

function kFormatter(num) {
    return Math.abs(num) > 999 ? Math.sign(num) * ((Math.abs(num) / 1000).toFixed(1)) + 'k' : Math.sign(num) * Math.abs(num)
}