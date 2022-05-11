$(document).ready(function () {
    $('.offcanvas-button').click(function () {
        $('.offcanvas-collapse').toggleClass('open');
    });

    $('.logout-button').click(function () {
        localStorage.clear();
        window.location.href = "/Account/Logout";
    });
});