// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



$.ajax({
    url: "https://localhost:44376/employees/getdata",
}).done((result) => {
    console.log(result);
    var userName = `${result.firstName} ${result.lastName}`;
    $("#userName").html(userName);

    $("input#nikUser").val(`${result.nik}`);
    $("input#firstName").val(`${result.firstName}`);
    $("input#lastName").val(`${result.lastName}`);
    $("input#department").val(`${result.department}`);
}).fail((error) => {
    console.log(error)
})
