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
});


$(document).on('click', 'ul .nav-item', function () {
    $(this).addClass('active').siblings().removeClass('active')
});

$('#password, #confirm_password').on('keyup', function () {
    if ($('#password').val() == $('#confirm_password').val()) {
        $('#message').html('Matching').css('color', 'green');
        $('#but').prop('disabled', false);
    } else {
        $('#message').html('Not Matching').css('color', 'red');
        $('#but').prop('disabled', true);
    }
});
$(document).ready(function () {
    $("#isCheck").change(function () {
        if (this.checked) {
            $('#message2').css('display', 'none');
        } else {
            $('#message2').css('display', 'block');
        }
    });
});
$('#but').click(function () {

    var status = $('#isCheck').is(':checked');
    if (status) {
        $('#message2').css('display', 'none');
    } else {
        $('#message2').css('display', 'block');
    }

});


