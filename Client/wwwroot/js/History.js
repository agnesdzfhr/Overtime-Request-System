/*================================Show Data Table================================*/
var table = "";
$(document).ready(function () {
    console.log("Berhasil");
    table = $('#tableHistory').DataTable({
        'ajax': {
            'url': "https://localhost:44376/Employees/GetOvertimeHistory",
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
                'data': 'dateStr',
            },
            {
                'data': 'startTime',
            },
            {
                'data': 'endTime',
            },
            {
                'data': 'totalHour',
            },
            {
                'data': 'jobNote',
            },
            {
                'data': null,
                'render': function (data) {
                    return `<span id="needApproval" class="badge ${data.approvalStatus}">${data.approvalStatus}</span>`;
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