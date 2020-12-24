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

function ReverseFollowingStateUserPage(myUserID, theirID, followID) {
    $.ajax({
        url: '/Userpage/ReverseFollowingStateUserPage',
        type: 'GET',
        dataType: 'json',
        data: { myUserID: Number(myUserID), theirID: Number(theirID), followID: Number(followID) },
        success: function (data) {
            if (data == true) {
                if (followID != -1) {

                    if ($('#followingBtn_' + followID).attr('class') == 'btnFollowing2') {
                        $('#followingBtn_' + followID).attr('class', 'btnFollow2');
                        $('#followingBtn_' + followID).html('<span>Theo dõi</span>');
                    }
                    else if ($('#followingBtn_' + followID).attr('class') == 'btnFollow2') {
                        $('#followingBtn_' + followID).attr('class', 'btnFollowing2');
                        $('#followingBtn_' + followID).html('<span>Đang theo dõi</span>');
                    }
                }
                //None nghĩa là đang có FollowID = -1
                else {
                    if ($('#followingBtn_NONE').attr('class') == 'btnFollowing2') {
                        $('#followingBtn_NONE').attr('class', 'btnFollow2');
                        $('#followingBtn_NONE').html('<span>Theo dõi</span>');
                    }
                    else if ($('#followingBtn_NONE').attr('class') == 'btnFollow2') {
                        $('#followingBtn_NONE').attr('class', 'btnFollowing2');
                        $('#followingBtn_NONE').html('<span>Đang theo dõi</span>');
                    }
                }
            }
            else {

            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //some errror, some show err msg to user and log the error
            alert(xhr.responseText);
        }
    });
}