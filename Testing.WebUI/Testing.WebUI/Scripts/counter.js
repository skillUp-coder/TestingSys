

var min = document.getElementById('minRemaining');

var second = 59, minute = , hour = 0;
var interval = null;
function show() {
    if (second != 0) second = second - 1;
    else {
        if (minute != 0) {
            minute = minute - 1;
            second = 59;
        }
        else {
            alert("Süreniz doldu!");
            var btn = $("#btnFinishExam");
            btn[0].click();
            clearInterval(interval);
        }

    }
    $("#counter").html(hour + " : " + minute + " : " + second);
}

$(document).ready(function () {
    
    var datatime = $("#counter");
    $("#counter").html("");
    interval = setInterval(show, 1000);
});
