// Css loader on page load
$(function () {
    $("#preloader").fadeOut("slow");
});

// Css loader on form submit
// No mechanism to remove the loader after submit, so commented out
//window.onload = () => {
//    document.body.addEventListener('submit', function (e) {
//        $("#preloader").fadeIn("slow");
//    });
//}