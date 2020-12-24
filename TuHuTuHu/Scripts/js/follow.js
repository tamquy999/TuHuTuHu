var main_fg_color = "#00BDD7";
var main_bg_color = "#FAFAFB";


// Old Method for Tabs follow
// function changeColorBottomBorder(btnIn) {
//     var btnFL = document.querySelectorAll("[id='btnFL']");

//     for (var i = 0; i < btnFL.length; i++) {
//         btnFL[i].style.borderBottomColor = "";
//         btnFL[i].style.color = '';
//         btnFL[i].style.backgroundColor = "";
//     }

//     //Có bug màu trên lúc set màu này ? Nó bị đen đi.
//     btnIn.style.borderBottomColor = "cyan";
//     // btnIn.style.backgroundColor = "#eaf8ffFF";
//     btnIn.style.color = main_fg_color;
//     // btnIn.style.backgroundColor = "#eaf8ff";

// }

function openFollowContent(event, nameOfContent) {
    var tabcontent = document.getElementsByClassName("followTabContent");
    for (var i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    var tabLinks = document.getElementsByClassName("followTabLink");
    for (var i = 0; i < tabLinks.length; i++) {
        tabLinks[i].className = tabLinks[i].className.replace(" active", "");
    }

    document.getElementById(nameOfContent).style.display = "block";
    event.currentTarget.className += " active";

}


function ReverseFollowingState(myUserID, theirID, followID) {
    $.ajax({
        url: '/Follow/ReverseFollowingState',
        type: 'GET',
        dataType: 'json',
        data: { myUserID: Number(myUserID), theirID: Number(theirID), followID: Number(followID) },
        success: function (data) {
            if (data == true) {
                //Neu la o tab Dang Theo doi
                    if ($('#followingBtn_' + followID).attr('class') == 'btnFollowing') {
                        $('#followingBtn_' + followID).attr('class', 'btnFollow');
                        $('#followingBtn_' + followID).html('<span>Theo dõi</span>');
                    }
                    else if ($('#followingBtn_' + followID).attr('class') == 'btnFollow') {
                        $('#followingBtn_' + followID).attr('class', 'btnFollowing');
                        $('#followingBtn_' + followID).html('<span>Đang theo dõi</span>');
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



function ReverseFollowedState(myUserID, theirID, followID) {
    $.ajax({
        url: '/Follow/ReverseFollowedState',
        type: 'GET',
        dataType: 'json',
        data: { myUserID: Number(myUserID), theirID: Number(theirID), followID: Number(followID) },
        success: function (data) {
            if (data == true) {
                    if ($('#followedBtn_' + followID).attr('class') == 'btnFollow') {
                        $('#followedBtn_' + followID).attr('class', 'btnFollowed');
                        $('#followedBtn_' + followID).html('<span>Đang theo dõi</span>');
                    }
                    else if ($('#followedBtn_' + followID).attr('class') == 'btnFollowed') {
                        $('#followedBtn_' + followID).attr('class', 'btnFollow');
                        $('#followedBtn_' + followID).html('<span>Theo dõi</span>');
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