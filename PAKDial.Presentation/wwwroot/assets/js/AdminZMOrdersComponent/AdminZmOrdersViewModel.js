$(function () {
    $('#AdminZmOrder').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminZMOrderAssigning/LoadZMOrders",
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
            { "data": "id", "name": "id", "orderable": false },
            { "data": "companyName", "name": "companyName", "orderable": false },
            { "data": "salePersonEmail", "name": "salePersonEmail", "orderable": false },
            { "data": "assignedToEmail", "name": "assignedToEmail", "orderable": false },
            { "data": "statusName", "name": "statusName", "orderable": false },
            {
                "data": "deposited", "name": "deposited", "orderable": false,
                "render": function (data) {
                    if (data == true) {
                        return '<div class="badge badge-primary">Yes</div>';
                    }
                    else {
                        return '<div class="badge badge-danger">No</div>';
                    }
                }
            },

        ],
        columnDefs: [
            { "width": "10%", "targets": 0 },
            { "width": "10%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "13%", "targets": 3 },
            { "width": "12%", "targets": 4 },
            { "width": "5%", "targets": 5 },
            { "width": "5%", "targets": 6 },
            {
                "width": "30%",
                targets: 7,
                render: function (data, type, full, meta) {
                    var Id = full.id;
                    var Transfering = "";
                    if (full.assignedToEmail == null) {
                        Transfering = '<button id="ZmOrderTransfering" value=' + Id + ' type="button" class="btn mr-1 mb-1 btn-danger btn-sm">&nbsp;Transfer&nbsp;</button>'
                    }
                    else if (full.assignedToEmail != null && (full.statusName == 'Process' || full.statusName == 'Approved'))
                    {
                        Transfering = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm">&nbsp;Cleared&nbsp;</button>'
                    }
                    else {
                        Transfering = '<button type="button" class="btn mr-1 mb-1 btn-info btn-sm">&nbsp;Transfered&nbsp;</button>'
                    }
                    return '<td> ' + Transfering + ' </td>'
                }
            }
        ],
        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            if (aData.assignedToEmail == null) {
                $('td', nRow).css('background-color', '#FFA599');
            }
        }

    });

    $("#AdminZmOrder").on('click', '#ZmOrderTransfering', function () {
        var AssignedId = $(this).val();
        window.location.href = "/AdminZMOrderAssigning/GetUnAssignedOrders?Id=" + AssignedId
    });
});