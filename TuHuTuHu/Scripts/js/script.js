var customScroll;

// function Init() {
//     customScroll = new slimScroll(document.getElementById('contacts'));
//     window.onresize = customScroll.resetValues;
// }

// Auto grow post textarea
function auto_grow(element) {
    element.style.height = "0px";
    element.style.height = (element.scrollHeight) + "px";
}

function auto_grow_chat(element) {
    element.style.height = "0px";
    element.style.height = (element.scrollHeight) + "px";

    var chatbody = document.getElementById("chatbody");
    var styleVal = getComputedStyle(chatbody);
    // console.log(parseInt(element.scrollHeight));
    chatbody.style.height = (368 - parseInt(element.scrollHeight)).toString() + "px";
    //chatbody.scrollTop = chatbody.scrollHeight;
}


// CHat head click
function chatheadClick() {
    window.location.href = "/Userpage/Index/" + $('#chatheadID').html();
}

// Open chat bubble
function anotherOpenChat() {
    var chatbox = document.getElementById('chatbox');
    chatbox.style.height = '450px';
    chatbox.style.width = '330px';
    chatbox.style.visibility = 'visible';

    var contacts = document.getElementById('contacts');
    contacts.style.height = 'calc(100% - 530px)';

    var chatbody = document.getElementById("chatbody");
    chatbody.scrollTop = chatbody.scrollHeight;

    $('#btn-bubble').hide();
}
function openChat(fullName, accID) {

    $.ajax({
        url: '/Base/LoadConversation',
        type: "POST",
        //contentType: "application/json",
        //dataType: "text",
        data: { accID },
        success: function (data) {
            $('#chatbox').html(data);
            var chatbox = document.getElementById('chatbox');
            chatbox.style.height = '450px';
            chatbox.style.width = '330px';
            chatbox.style.visibility = 'visible';

            var contacts = document.getElementById('contacts');
            contacts.style.height = 'calc(100% - 530px)';

            var chatbody = document.getElementById("chatbody");
            chatbody.scrollTop = chatbody.scrollHeight;

            $('#chatname').html(fullName);
            $('#chatheadID').html(accID);

            $('#btn-bubble').hide();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //some errror, some show err msg to user and log the error  
            $('#chatbox').html(xhr.responseText);
            //alert(xhr.responseText);

        }
    });

    

    // customScroll.resetValues;
}

// Close chat bubble
function closeChat() {
    var chatbox = document.getElementById('chatbox');
    chatbox.style.height = '60px';
    chatbox.style.width = '60px';
    chatbox.style.visibility = 'hidden';

    var contacts = document.getElementById('contacts');
    contacts.style.height = 'calc(100% - 140px)';

    $('#btn-bubble').show();
}

// toggle Contacts bubble
function toggleContacts() {
    if ($('#contacts').css('opacity') == '1') {
        $('#contacts').css('width', '0px');
        $('#contacts').css('opacity', '0');
    } else {
        $('#contacts').css('width', '290px');
        $('#contacts').css('opacity', '1');
    }
}

$(window).resize(function () {
    // if ($(window).width() > 900) {
    if (window.matchMedia('(max-width: 900px)').matches) {
        $('#contacts').css('width', '0px');
        $('#contacts').css('opacity', '0');
    } else {
        $('#contacts').css('width', '290px');
        $('#contacts').css('opacity', '1');
    }
});


// Click add img button to trigger file input button
function triggerFileButton() {
    $('#file-input').trigger('click');
}

// Click logout div button to trigger logout input button
function triggerLogoutButton() {
    $('#logout-btn').trigger('click');
}

// Click Post div button to trigger Post input button
function triggerPostButton() {
    $('#post-btn').trigger('click');
}


// Load image
var loadImage = function (event) {
    $('#output').css('background-image', 'url(' + URL.createObjectURL(event.target.files[0]) + ')');
    $('#output').css('display', 'block');
    index++;
};

// CHeck love status
function LoveCheck(postID) {
    $.ajax({
        url: '/Base/LoveCheck',
        type: 'GET',
        dataType: 'json',
        data: { postID: postID },
        success: function (data) {
            if (data == true) {
                $('#love_' + postID).attr('src', '/Content/images/heart.svg');
                //$('#love_' + postID).load(location.href + ' #love_' + postID, '');
            }
            else {
                $('#love_' + postID).attr('src', '/Content/images/heart_line.svg');
                //$('#love_' + postID).load(location.href + ' #love_' + postID, '');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //some errror, some show err msg to user and log the error
            alert(xhr.responseText);

        }
    });
}

// Click love button
function LoveClick(postID) {
    //var temp = '#love_' + postID;
    if ($('#love_' + postID).attr('src') == '/Content/images/heart_line.svg') {
        $.ajax({
            url: '/Base/LoveClick',
            type: 'GET',
            dataType: 'json',
            data: { postID: postID },
            success: function (data) {
                $('#love_' + postID).attr('src', '/Content/images/heart.svg');
                $('#lovetext_' + postID).load(location.href + ' #lovetext_' + postID, '');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //some errror, some show err msg to user and log the error
                alert(xhr.responseText);

            }
        });
    }
    else {
        $.ajax({
            url: '/Base/LoveUnClick',
            type: 'GET',
            dataType: 'json',
            data: { postID: postID },
            success: function (data) {
                $('#love_' + postID).attr('src', '/Content/images/heart_line.svg');
                $('#lovetext_' + postID).load(location.href + ' #lovetext_' + postID, '');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //some errror, some show err msg to user and log the error
                alert(xhr.responseText);

            }
        });
    }
}

// Click comment button
function CommentIcon_Click(Comment_PostID) {
    $('#' + Comment_PostID).focus();
}

$('#deleteBtn').on('show.bs.modal', function (e) {
    var postID = $('#deleteBtn').data('post-id');
    $('#selectedPost').html(postID);
});


// Confirm dialogs

$(document).on("click", ".open-DeletePostDialog", function (e) {

	e.preventDefault();

	var _self = $(this);

	var postId = _self.data('post-id');
    $("#selectedPost").val(postId);

	$(_self.attr('href')).modal('show');
});

$(document).on("click", ".open-EditPostDialog", function (e) {

    e.preventDefault();

    var _self = $(this);

    var postId = _self.data('post-id');
    $("#selectedPost1").val(postId);

    var content = $('.post-text[data-post-id="' + postId + '"]').text().trim();
    $("textarea#editInput").html(content);

    $(_self.attr('href')).modal('show');
});