$(function () {

    $('#AdminSubOrders').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminSubOrders/LoadAllOrders",
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
            { "data": "fullName", "name": "fullName", "orderable": false },
            {
                "data": "listingFrom", "name": "listingFrom", "orderable": false,
                "render": function (data) {
                    if (data != null) {
                        var date = new Date(data);
                        var month = date.getMonth() + 1;
                        return date.getDate() + "/" + (month.toString().length > 1 ? month : "0" + month) + "/" + date.getFullYear();

                    }
                    else {
                        return "";
                    }
                }
            },
            {
                "data": "listingTo", "name": "listingTo", "orderable": false,
                "render": function (data) {
                    if (data != null) {
                        var date = new Date(data);
                        var month = date.getMonth() + 1;
                        return date.getDate() + "/" + (month.toString().length > 1 ? month : "0" + month) + "/" + date.getFullYear();

                    }
                    else {
                        return "";
                    }
                }
            },
            { "data": "statusName", "name": "statusName", "orderable": false },
            {
                "data": "isActive", "name": "isActive", "orderable": false,
                "render": function (data) {
                    if (data == true) {
                        return '<div class="badge badge-primary">Active</div>';
                    }
                    else {
                        return '<div class="badge badge-danger">InActive</div>';
                    }
                }
            },
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
            { "width": "15%", "targets": 3 },
            { "width": "10%", "targets": 4 },
            { "width": "10%", "targets": 5 },
            { "width": "5%", "targets": 6 },
            { "width": "5%", "targets": 7 },
            { "width": "5%", "targets": 8 },
            {
                "width": "15%",
                targets: 9,
                render: function (data, type, full, meta) {
                    var Id = full.id;
                    var Invoice = '<button id="SubOrderGenrate" value=' + Id + ' type="button" class="btn mr-1 mb-1 btn-primary btn-sm">&nbsp;Invoice&nbsp;</button>'
                    var Update = "";
                    if (full.statusName == "Process" || full.statusName == "Pending") {
                        Update = '<button id="SubOrderUpdate" value=' + Id + ' type="button" class="btn mr-1 mb-1 btn-danger btn-sm">&nbsp;Update&nbsp;</button>'
                    }
                    return '<td> ' + Invoice + Update + ' </td>'
                }
            }
        ],
        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            if (aData.statusName == "Pending") {
                $('td', nRow).css('background-color', '#A9DFBF');
            }
            if (aData.statusName == "Rejected") {
                $('td', nRow).css('background-color', '#FFA599');
            }
            if (aData.statusName == "Approved") {
                $('td', nRow).css('background-color', '#99ffa5');
            }
        }

    });

    $("#AdminSubOrders").on('click', '#SubOrderGenrate', function () {
        var invoiceNo = $(this).val();
        window.location.href = "/AdminSubOrders/SalesReport?InvoiceId=" + invoiceNo
    });
    $("#AdminSubOrders").on('click', '#SubOrderUpdate', function () {
        var invoiceNo = $(this).val();
        window.location.href = "/AdminSubOrders/GetOrdersById?Id=" + invoiceNo
    });

});