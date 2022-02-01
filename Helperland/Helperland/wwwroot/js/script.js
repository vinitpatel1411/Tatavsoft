window.addEventListener("scroll", () => {
    let nav = document.querySelector("nav");
    let winPos = window.scrollY > 0;
    var logo_img = document.getElementById("logo-img");
    nav.classList.toggle("scroll-active", winPos);
    if (window.scrollY > 0) {
        logo_img.src = "/asset/logo-small.png";
    }
    else {
        logo_img.src = "/asset/logo-large.png";
    }
});



jQuery('.ok-btn').click(function (e) {
    e.preventDefault();
    var _this = jQuery(this);
    _this.closest('.footer-bottom').slideUp();

})

$(document).on('click', 'ul .nav-item', function () {
    $(this).addClass('active').siblings().removeClass('active')
})
/*
$(document).ready(function () {
    $(window).scroll(function () {
        $('.nav').toggleClass("scroll-active", ($(window).scrollTop() > 0));
    });
});
*/
