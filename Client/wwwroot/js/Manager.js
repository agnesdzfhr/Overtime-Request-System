﻿





$.ajax({
    url: "https://localhost:44376/employees/getdata",
}).done((result) => {

    /*================================Show Data Table================================*/
    var table = "";
    $(document).ready(function () {
        table = $('#tableRequest').DataTable({
            'ajax': {
                'url': "https://localhost:44388/API/OvertimeRequests/GetForManager/" + result.nik,
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
                    'data': 'fullName',
                },
                {
                    'data': 'dateStr',
                },
                {
                    'data': 'startTime'
                },
                {
                    'data': 'endTime'
                },
                {
                    'data': null,
                    'render': function (data, type, row, meta) {
                        if (data.approvalStatus == "Need Approval") {
                            return `<div id=""><div id=""><button class="btn btn-primary" onclick="getDetail(${data.overtimeSchedule_ID})" data-toggle="modal" data-target="#detailModalRequest"><i class="fas fa-info"></i></button></div></div>`;
                        } else {
                            return `<span id="" class="badge ${data.approvalStatus}">${data.approvalStatus}</span>`
                        }
                    }
                }
            ]
        });
        console.log(table);
    });


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

let overtimeID = null;

function getDetail(id) {
    $.ajax({
        url: "https://localhost:44388/API/OvertimeRequests/GetOvertimeRequestByID/" + id
    }).done((result) => {
        console.log(result);

        overtimeID = result.overtimeSchedule_ID;

        $("#nikModal").val(result.nik);;
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
            id = overtimeID;
            managerNote = $("#managerNoteModal").val();

            obj = new Object();
            obj.managerApprovalStatus = 2;
            obj.overtimeRequestID = id;
            obj.managerNote = managerNote;
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
            id = overtimeID;
            managerNote = $("#managerNoteModal").val();

            obj = new Object();
            obj.managerApprovalStatus = 1;
            obj.overtimeRequestID = id;
            obj.managerNote = managerNote;
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