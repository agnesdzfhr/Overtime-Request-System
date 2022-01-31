// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*================================Show Data Table================================*/
var table = "";
$(document).ready(function () {
    table = $('#tableOvertimeRequest').DataTable({
        'ajax': {
            'url': "https://localhost:44388/API/OvertimeSchedules/",
            'dataType': 'json',
            'dataSrc': ''
        },
        'columns': [
            {
                'data': null,
                'render': function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1 + ".";
                }
            },
            {
                'data': 'nik',
            },
            {
                'data': 'date',
                'render': (data, type, row) => {
                    var dataGet = new Date(row['date']);
                    return dataGet.toLocaleDateString();
                }
            },
            {
                'data': 'startTime'
            },
            {
                'data': 'endTime'
            },
            {
                'data': 'action',
                'bSortable': false,
                'render': function (data, type, row, meta) {
                    return '<button class="btn btn-primary" data-toggle="modal" data-target="#detailModalRequest">Detail</button>';
                }
            }
        ]
    });
});



function getDetail(link) {
    $.ajax({
        url: link
    }).done((result) => {
        console.log(result);
        $(".nik").html(result.nik);
        $(".fullname").html(result.firstName + result.lastName);
        $(".date").html(result.date);
        $(".startTime").html(result.startTime);
        $(".endTime").html(result.endtime);
        $(".note").html(result.endtime);
    }).fail((error) => {
        console.log(error);
    });
}