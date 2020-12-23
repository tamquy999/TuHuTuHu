function Init() {
    $('.signup-container').hide();
}

// Sign up ask click
function SignupClick() {
    $('.login-container').hide();
    $('.signup-container').show();
    $('.image').css('left', '400px');
    // $('.login-container').css('left', '0px');
    // $('.signup-container').css('left', '0px');
    // $('.login-container').css('opacity', '0');
    // $('.signup-container').css('opacity', '1');
    // $('.signup-container').css('z-index', '0');
    $('.logo').css('margin-top', '30px');
    $('.logo').css('margin-bottom', '10px');
    $('.does-have-acc').css('margin-top', '20px');
    $('#realname, #username, #password').css('margin-top', '2px');
}

// login ask click
function LoginClick() {
    $('.signup-container').hide();
    $('.login-container').show();
    $('.image').css('left', '0px');
    // $('.login-container').css('left', '400px');
    // $('.signup-container').css('left', '400px');
    // $('.login-container').css('opacity', '1');
    // $('.signup-container').css('opacity', '0');
    // $('.signup-container').css('z-index', '-1');
    $('.logo').css('margin-top', '50px');
    $('.does-have-acc').css('margin-top', '30px');
    $('#realname, #username, #password').css('margin-top', '10px');
}