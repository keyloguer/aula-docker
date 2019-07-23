var countDownDate = new Date("Apr 06, 2019 09:00:00").getTime();
var x = setInterval(function() {
  var now = new Date().getTime();
  var distance = countDownDate - now;
  var days = Math.floor(distance / (1000 * 60 * 60 * 24));
  var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
  var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
  var seconds = Math.floor((distance % (1000 * 60)) / 1000);
  document.getElementById("countdown").innerHTML = "<strong class='data'>" + days + "</strong> dias <strong class='data'>" + hours + "</strong> horas <strong class='data'>"
  + minutes + "</strong> minutos";
  if (distance < 0) {
    clearInterval(x);
    document.getElementById("countdown").innerHTML = "<strong class='data'>Começou!</strong>";
  }
}, 1000);