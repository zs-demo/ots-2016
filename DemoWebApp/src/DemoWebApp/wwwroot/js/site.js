// Write your Javascript code.

$('.nav-main .nav-submenu').on('click', function () {
    var c = $(this);
    var d = c.parent("li");

    if (d.hasClass("open")) {
        d.removeClass("open");
    } else {
        c.closest("ul").find("> li").removeClass("open");
        d.addClass("open");
    }
});

$('button[data-action=sidebar_mini_toggle]').on('click', function () {
    var b = $('#page-container');
    b.toggleClass('sidebar-mini');
});

function updateContainer() {
    var m = $(window).height();
    var x = (m - $('.side-header').outerHeight());
    $('#sidebar').height(x);
}

$(document).ready(function () {
    $('.nav-main a').removeClass('active');
    $('.nav-main a[data-tmp=' + $('.page-heading').attr('data-tmp') + ']').addClass('active');

});
