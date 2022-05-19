var hours = 1;
var minutes = 59;
var seconds = 55;
function clockTimer(sub) {

    if (sub == "Физика" || sub == "Математика") {

        if (hours == 3 && minutes == 20 && seconds == 0) {
            alert("Осталось 10 минут до завершения теста");
        }

        if (hours == 3 && minutes == 30 && seconds == 0) {
            var stop = document.getElementById('save');
            stop.click();
        }

    }

    if (sub == "Химия") {

        if (hours == 2 && minutes == 20 && seconds == 0) {
                             
            alert("Осталось 10 минут до завершения теста");
        }

        if (hours == 2 && minutes == 30 && seconds == 0) {
            var stop = document.getElementById('save');
            stop.click();
        }

    }

    if (sub == "Биология" || sub == "Английский язык" || sub == "Русский язык" || sub == "Беларуская мова") {

        if (hours == 1 && minutes == 50 && seconds == 0) {
            
            //alert("Осталось 10 минут до завершения теста");
        }

        if (hours == 2 && minutes == 0 && seconds == 0) {
            var stop = document.getElementById('save');
            stop.click();
        }

    }

    if (sub == "История Беларуси" || sub == "Обществоведение") {

        if (hours == 1 && minutes == 20 && seconds == 0) {
            alert("Осталось 10 минут до завершения теста");
        }

        if (hours == 1 && minutes == 30 && seconds == 0) {
            var stop = document.getElementById('save');
            stop.click();
        }

    }


    seconds++;
    if (seconds == 60) {
        minutes++;
        seconds = 0;
    }
    if (minutes == 60) {
        hours++;
        minutes = 0;
    }

    var time = [hours, minutes, seconds];

    if (time[0] < 10) { time[0] = "0" + time[0]; }
    if (time[1] < 10) { time[1] = "0" + time[1]; }
    if (time[2] < 10) { time[2] = "0" + time[2]; }

    var current_time = [time[0], time[1], time[2]].join(':');
    var clock = document.getElementById("clock");

    clock.innerHTML = current_time;
    var fun = "clockTimer('" + sub + "')";
    setTimeout(fun, 1000);
}