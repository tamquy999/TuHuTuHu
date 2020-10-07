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