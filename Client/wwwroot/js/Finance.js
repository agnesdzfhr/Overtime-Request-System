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
                'data': 'action',
                'bSortable': false,
                'render': function (data, type, row, meta) {
                    return '<div id="addFee"><button class="btn btn-success onclick="submitFeeToDatabaes()"><i class="fa fa-check"></i></button></div>';
                }
            }
        ]
    });
    console.log(table);
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

function submitFeeToDatabaes() {
    
}