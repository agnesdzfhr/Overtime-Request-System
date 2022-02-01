// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*================================Show Data Table================================*/
//var table = "";
//$(document).ready(function () {
//    table = $('#tableOvertimeRequest').DataTable({
//        'ajax': {
//            'url': "https://localhost:44388/API/OvertimeSchedules/",
//            'dataType': 'json',
//            'dataSrc': ''
//        },
//        'columns': [
//            {
//                'data': null,
//                'render': function (data, type, row, meta) {
//                    return meta.row + meta.settings._iDisplayStart + 1 + ".";
//                }
//            },
//            {
//                'data': 'nik',
//            },
//            {
//                'data': 'date',
//                'render': (data, type, row) => {
//                    var dataGet = new Date(row['date']);
//                    return dataGet.toLocaleDateString();
//                }
//            },
//            {
//                'data': 'startTime'
//            },
//            {
//                'data': 'endTime'
//            },
//            {
//                'data': 'action',
//                'bSortable': false,
//                'render': function (data, type, row, meta) {
//                    return '<button class="btn btn-primary" data-toggle="modal" data-target="#detailModalRequest">Detail</button>';
//                }
//            }
//        ]
//    });
//});



$.ajax({
    url: "https://localhost:44376/employees/getdata",
}).done((result) => {
    console.log(result);
    $.ajax({
        url: "https://localhost:44388/API/OvertimeRequests/GetForManager/" + result.nik
    }).done((results) => {
        console.log(results);
        var text = "";
        $.each(results, function (key, val) {
            console.log(val.fullName)
            text += `<div class="card shadow" onclick="getDetail(${val.overtimeSchedule_ID})" data-toggle="modal" data-target="#detailModalRequest" style="cursor: pointer;width: auto; height: auto;">
                <div class="card-body">
                    <li class="media">
                        <div class="media-body">
                            <div id="requesterName" class="mt-0 mb-1 font-weight-bold">${val.fullName} <span id="needApproval" class="badge ${val.approvalStatus}">${val.approvalStatus}</span></div>
                            <div class="text-small font-600-bold">has been request for Overtime Schedule. Click this for more information.</div>
                        </div>
                    </li>
                </div>
            </div>`;
        });
        console.log(text);
        $("#cardNotification").html(text);


    }).fail((error) => {
        console.log(error);
    })

}).fail((error) => {
    console.log(error)
})

/*function getData() {
    $.ajax({
        url: "https://localhost:44376/employees/getdata",
    }).done((result) => {
        console.log(result);

        return result.nik

    }).fail((error) => {
        console.log(error)
    })
}

function getForManager() {
    nik = getData();
    $.ajax({
        url: "https://localhost:44388/API/OvertimeRequests/GetForManager/" + nik,
    }).done((result) => {
        console.log(result);

        return result.nik

    }).fail((error) => {
        console.log(error)
    })
}*/



function getDetail(id) {
    $.ajax({
        url: "https://localhost:44388/API/OvertimeRequests/GetOvertimeRequestByID/" + id
    }).done((result) => {
        console.log(result);

        $("#nikModal").val(result.nik);
        $("#requestID").val(result.overtimeSchedule_ID);
        $("#fullNameModal").val(result.fullName);
        $("#dateModal").val(result.dateStr);
        $("#startTimeModal").val(result.startTime);
        $("#endTimeModal").val(result.endTime);
        $("#noteModal").html(result.jobNote);
    }).fail((error) => {
        console.log(error);
    });
}


function acceptRequest() {
    console.log("Berhasil");
    Swal.fire({
        title:'Are you sure to accept this request?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '##ff0000',
        cancelButtonColor: '#d33',
        confirmButtonText: "Yes, accept this request!"
    }).then((result) => {
        if (result.isConfirmed) {
            id = parseInt($("#requestID").val());

            obj = new Object();
            obj.managerApprovalStatus = 2;
            obj.overtimeRequestID = id;
            var objJson = JSON.stringify(obj);
            console.log(objJson);

            $.ajax({
                url: "https://localhost:44388/api/ManagerApprovals/Approval",
                type: "POST",
                contentType: "application/json;charset=utf-8",
                data: objJson
            }).done((result) => {
                console.log(result);
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: "This request is Accepted",
                    type: 'success'
                });
                location.reload(true);

            }).fail((error) => {
                console.log(error);
                Swal.fire({
                    icon: 'error',
                    title: 'Failed',
                    text: error.responseText,
                    type: 'failed'
                });
            })
        }
    })
}

function rejectRequest() {
    console.log("Berhasil");
    Swal.fire({
        title: 'Are you sure to reject this request?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '##ff0000',
        cancelButtonColor: '#d33',
        confirmButtonText: "Yes, reject this request!"
    }).then((result) => {
        if (result.isConfirmed) {
            id = parseInt($("#requestID").val());

            obj = new Object();
            obj.managerApprovalStatus = 1;
            obj.overtimeRequestID = id;
            var objJson = JSON.stringify(obj);
            console.log(objJson);

            $.ajax({
                url: "https://localhost:44388/api/ManagerApprovals/Approval",
                type: "POST",
                contentType: "application/json;charset=utf-8",
                data: objJson
            }).done((result) => {
                console.log(result);
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: "This request is Rejected",
                    type: 'success'
                });
                location.reload(true);

            }).fail((error) => {
                console.log(error);
                Swal.fire({
                    icon: 'error',
                    title: 'Failed',
                    text: error.responseText,
                    type: 'failed'
                });
            })
        }
    })
}