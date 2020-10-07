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
    chatbody.scrollTop = chatbody.scrollHeight;
}


// Open chat bubble
function openChat() {
    var chatbox = document.getElementById('chatbox');
    chatbox.style.height = '450px';
    chatbox.style.width = '330px';
    chatbox.style.visibility = 'visible';

    var contacts = document.getElementById('contacts');
    contacts.style.height = 'calc(100% - 530px)';

    var chatbody = document.getElementById("chatbody");
    chatbody.scrollTop = chatbody.scrollHeight;

    $('#btn-bubble').hide();

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

$(window).resize(function() {
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

var imageList = [];
var index = 0;

var loadImage = function(event) {
    imageList.push(event.target.files[0]);

    // document.getElementById('addimg-btn').insertBefore = '<img class="output">';
    $('.listimg').append('<div class="output" id="output' + index + '"></div>');
    // $('.output').attr('src', URL.createObjectURL(imageList[0]));

    $('#output' + index).css('background-image', 'url(' + URL.createObjectURL(imageList[index]) + ')');
    $('#output' + index).css('display', 'block');
    index++;
    // var addimg = document.getElementById('addimg');
    // addimg.style.display = "none";
    // addimg.style.display = "block";
};

// Click love button
function LoveClick() {
    $('.love-icon').attr('src', '/Content/images/heart.svg');
}