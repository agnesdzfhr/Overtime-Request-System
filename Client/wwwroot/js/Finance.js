/*================================Show Data Table================================*/
var table = "";
$(document).ready(function () {
    table = $('#tableValidateBonus').DataTable({
        'ajax': {
            'url': "https://localhost:44388/api/OvertimeRequests/GetRegisteredRequest",
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
                'data': 'salary',
                'render': function (data, type, row, meta) {
                    return formatRupiah('' + row['salary'], '');
                }
            },
            {
                'data': 'totalFee',
                'render': function (data, type, row, meta) {
                    return formatRupiah('' + row['totalFee'], '');
                }
            },
            {
                'data': null,
                'bSortable': false,
                'render': function (data, type, row, meta) {
                    if (data.financeApproval == "Need Approval") {
                        return '<div id="addFee"><div id="f1"><button class="btn btn-primary"><i class="fa fa-plus"></i></button></div></div>';
                    } else {
                        return `<span id="" class="badge Completed">Completed</span>`
                    }
                }
            }
        ]
    });
    console.log(table);
});

$('#tableValidateBonus').on('click', '#addFee', function () {
    let rowData = $('#tableValidateBonus').DataTable().row($(this).closest('tr')).data();
    var id = `<div id="f${rowData.overtimeRequestID}"><button class="btn btn-success"><i class="fa fa-plus"></i></button></div>`;
    $('#addFee').html(id);
    console.log($('#addFee').html());
    console.log(rowData);
    submitFeeToDatabase(rowData.overtimeRequestID, rowData.totalFee);
});

function formatRupiah(angka, prefix) {
    var number_string = angka.replace(/[^,\d]/g, '').toString(),
        split = number_string.split(','),
        sisa = split[0].length % 3,
        rupiah = split[0].substr(0, sisa),
        ribuan = split[0].substr(sisa).match(/\d{3}/gi);
    if (ribuan) {
        separator = sisa ? '.' : '';
        rupiah += separator + ribuan.join('.');
    }
    rupiah = split[1] != undefined ? rupiah + ',' + split[1] : rupiah;
    return prefix == undefined ? rupiah : (rupiah ? 'Rp. ' + rupiah : '');
}

function submitFeeToDatabase(id, totalFee) {
    console.log("error1");
    Swal.fire({
        title: 'Yakin ingin menginput data ini?',
        text: "Data akan diinput ke dalam database",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yakin',
        cancelButtonText: 'Batal',
    }).then((result) => {
        if (result.isConfirmed) {
            console.log("error2");
            obj = new Object();
            obj.overtimeRequestID = id;
            obj.totalFee = totalFee;
            var objJson = JSON.stringify(obj);
            console.log(objJson);
            $.ajax({
                url: "https://localhost:44388/API/OvertimeRequests/InsertCountBonus",
                data: objJson,
                contentType: "application/json;charset=utf-8",
                type: "POST",
                crossDomain: true,
            }).done((result) => {
                Swal.fire(
                    'Berhasil',
                    result.messageResult,
                    'success'
                )
                table.ajax.reload();
            }).fail((error) => {
                Swal.fire(
                    'Gagal',
                    'error'
                )
            })
        }
    })
}