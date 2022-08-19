// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#forgotPassword').submit(function (e) {
    e.preventDefault();
    ForgotPassword()
    $('#forgotPassword').trigger("reset");
});

function ForgotPassword() {
    var email = $('#inputEmail').val();
    var forgotPassword = Object();
    forgotPassword.email = email;
    console.log(forgotPassword);
    $.ajax({
        url: "/Login/ForgotPassword",
        type: "POST",
        data: forgotPassword
    }).done((result) => {
        console.log(result.forgotPassword);
    }).fail((error) => {
        onsole.log(error);
    });
}