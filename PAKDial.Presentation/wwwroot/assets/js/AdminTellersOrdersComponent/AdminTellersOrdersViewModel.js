$(function () {

    $('#AdminTellersOrder').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminTellerOrders/LoadTellerOrders",
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
            { "data": "salePersonEmail", "name": "salePersonEmail", "orderable": false },
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
            { "width": "15%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "15%", "targets": 3 },
            { "width": "15%", "targets": 4 },
            { "width": "10%", "targets": 5 },
            { "width": "5%", "targets": 6 },
            {
                "width": "15%",
                targets: 7,
                render: function (data, type, full, meta) {
                    var Id = full.id;
                    var Deposit = "";
                    var Invoice = '<button id="TellerOrderGenrateInv" value=' + Id + ' type="button" class="btn mr-1 mb-1 btn-primary btn-sm">&nbsp;Invoice&nbsp;</button>'

                    if (full.deposited == false) {
                        Deposit = '<button id="EditTellerDep" value=' + Id + ' type="button" class="btn mr-1 mb-1 btn-primary btn-sm">&nbsp;Deposit&nbsp;</button>'
                    }
                    else {
                        Deposit = '<button type="button" class="btn mr-1 mb-1 btn-info btn-sm">&nbsp;Deposited&nbsp;</button>'
                    }
                    return '<td> ' + Deposit +" "+ Invoice + ' </td>'
                }
            }
        ],
        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            if (aData.deposited == false) {
                $('td', nRow).css('background-color', '#FFA599');
            }
        }
    });

    $("#AdminTellersOrder").on('click', '#TellerOrderGenrateInv', function () {
        var invoiceNo = $(this).val();
        window.location.href = "/AdminTellerOrders/SaleTellerReport?InvoiceId=" + invoiceNo
    });

    // On Click Update Modal Opened
    $("#AdminTellersOrder").on('click', '#EditTellerDep', function () {
        var Id = $(this).val();
        var Updatevalidator = $("#UpdateUnDepToDep_Submit_Form").validate();
        Updatevalidator.resetForm();
        ResetTellerIds();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminTellerOrders/GetUndepositedOrder",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    $("#UtellerBanksSystem").hide();
                    $("#UtellerBanksCheq").hide();
                    $("#UtellerBanksAcc").hide();
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                     else if (response != null) {
                        $("#UtellerInvoiceId").val(response.id);
                        $("#UtellerCombineArea").val(response.combineAreas);
                        $("#UtellerCompanyName").val(response.companyName);
                        $("#UtellerPackageName").val(response.packageName);
                        $("#UtellerModeName").val(response.modeName);
                        var paymentmode = response.modeName.split(" ").join("");
                        var trimpaymentmode = $.trim(paymentmode);
                        if (trimpaymentmode.toLowerCase() == PaymentsMode.PayOrder.toLowerCase() || trimpaymentmode.toLowerCase() == PaymentsMode.Cheque.toLowerCase()) {
                            $("#UtellerBanksSystem").show();
                            $("#UtellerBanksCheq").show();
                            $("#UtellerBanksAcc").hide();
                            $("#UtellerBankName").val(response.bankName);
                            $("#UtellerChqNo").val(response.chequeNo);
                        }
                        else if (trimpaymentmode.toLowerCase() == PaymentsMode.OnlinePayment.toLowerCase()) {
                            $("#UtellerBanksSystem").show();
                            $("#UtellerBanksAcc").show();
                            $("#UtellerBanksCheq").hide();
                            $("#UtellerBankName").val(response.bankName);
                            $("#UtellerAccNo").val(response.account_No);
                        }
                        $("#UpdateUnDepToDep").modal('show');
                    }
                    else {
                        toastr.error("Order Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }

    });

    //Deposited From UnDep Form Submit
    $("#UpdateUnDepToDep_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#UpdateUTellerShowLoader").show();
            $("#UpdateUTellerShowButtons").hide();
            $.ajax({
                url: "/AdminTellerOrders/UpdateUnDepositedOrders",
                type: "post",
                datatype: "json",
                data: { Id: $("#UtellerInvoiceId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response.invoiceNo > 0) {
                        $("#UpdateUnDepToDep").modal("hide");
                        $("#UpdateUTellerShowLoader").hide();
                        $("#UpdateUTellerShowButtons").show();
                        toastr.success(response.processMessage, "Success");
                        window.location.href = "/AdminTellerOrders/SaleTellerReport?InvoiceId=" + response.invoiceNo
                    }
                    else {
                        $("#UpdateUTellerShowLoader").hide();
                        $("#UpdateUTellerShowButtons").show();
                        toastr.error(response.processMessage, "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateUTellerShowLoader").hide();
                    $("#UpdateUTellerShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
});

function ResetTellerIds() {
    $("#UtellerInvoiceId").val('');
    $("#UtellerCombineArea").val('');
    $("#UtellerCompanyName").val('');
    $("#UtellerPackageName").val('');
    $("#UtellerModeName").val('');
    $("#UtellerBankName").val('');
    $("#UtellerChqNo").val('');
    $("#UtellerAccNo").val('');
}