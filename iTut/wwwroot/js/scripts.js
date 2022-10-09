$(document).ready(function () {
    $('.offcanvas-button').click(function () {
        $('.offcanvas-collapse').toggleClass('open');
    });

    $('.logout-button').click(function () {
        localStorage.clear();
        window.location.href = "/Identity/Account/Logout";
    });

    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    });
});