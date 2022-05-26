// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');

        }
    });
}
showInPopup2 = (id) => {
    console.log(id);
    $.ajax({
        type: 'GET',
        url: "AddEditDepartment/"+id,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            //$("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');

        }
    });
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    location.reload();
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

//For Designation 

showInPopup1 = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $("#form-modal1 .modal-body").html(res);
            $("#form-modal1 .modal-title").html(title);
            $("#form-modal1").modal('show');

        }
    });
}
showInPopup3 = (id) => {
    console.log(id);
    $.ajax({
        type: 'GET',
        url: "AddEditDesignation/" + id,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            //$("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');

        }
    });
}

jQueryAjaxPost1 = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal1 .modal-body').html('');
                    $('#form-modal1 .modal-title').html('');
                    $('#form-modal1').modal('hide');
                    location.reload();
                }
                else
                    $('#form-modal1 .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}


$(function () {
    $("body").on('click keypress', function () {
        ResetThisSession();
    });
});

var timeInSecondsAfterSessionOut = 30; // (in seconds).
var secondTick = 0;

function ResetThisSession() {
    secondTick = 0;
}

function StartThisSessionTimer() {
    secondTick++;
    var timeLeft = ((timeInSecondsAfterSessionOut - secondTick) / 60).toFixed(0); // in minutes
    timeLeft = timeInSecondsAfterSessionOut - secondTick;  

    $("#spanTimeLeft").html(timeLeft);

    if (secondTick > timeInSecondsAfterSessionOut) {
        clearTimeout(tick);
        window.location = "/Account/Logout";
        return;
    }
    tick = setTimeout("StartThisSessionTimer()", 1000);
}

StartThisSessionTimer();