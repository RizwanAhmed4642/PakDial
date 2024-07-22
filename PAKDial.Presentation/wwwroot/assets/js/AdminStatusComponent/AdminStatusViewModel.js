$(function () {
    $('#AdminWorkStatus').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminStatus/LoadEmployees",
            type: "POST",
            datatype: "json",
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        },
        "columns": [
            {
                "data": "id",
                "name": "id",
                "orderable": true,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "fullName", "name": "fullName", "orderable": false },
            { "data": "email", "name": "email", "orderable": false },
            { "data": "designationName", "name": "designationName", "orderable": false },

        ],
        columnDefs: [
            { "width": "15%", "targets": 0 },
            { "width": "20%", "targets": 1 },
            { "width": "25%", "targets": 2 },
            { "width": "20%", "targets": 3 },
            {
                "width": "20%",
                targets: 4,
                render: function (data, type, full, meta) {
                    var Id = full.userId;
                    var Redirect = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="RedirectStatuses">&nbsp;Stats&nbsp;</button>'
                    return '<td> ' + Redirect + ' </td>'
                }
            }
        ],

    });
});

$("#AdminWorkStatus").on('click','#RedirectStatuses', function () {
    var localId = $(this).val();
    window.location.href = "/AdminStatus/EmpReport?LocalId="+localId;
});