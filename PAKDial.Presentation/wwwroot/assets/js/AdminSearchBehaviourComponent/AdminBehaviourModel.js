$(function () {

    $('#AdminSearches').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminSearchBehaviour/LoadSearchBehaviour",
            type: "POST",
            datatype: "json",
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        },
        "columns": [
            {
                "data": "totalSearches",
                "name": "totalSearches",
                "orderable": true,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "searchResults", "name": "searchResults", "orderable": false },
            { "data": "locationSearch", "name": "locationSearch", "orderable": false },
            { "data": "areaSearch", "name": "areaSearch", "orderable": false },
            { "data": "totalSearches", "name": "totalSearches", "orderable": false },

        ],
        columnDefs: [
            { "width": "10%", "targets": 0 },
            { "width": "30%", "targets": 1 },
            { "width": "20%", "targets": 2 },
            { "width": "30%", "targets": 3 },
            { "width": "10%", "targets": 4 },
        ],

    });

});