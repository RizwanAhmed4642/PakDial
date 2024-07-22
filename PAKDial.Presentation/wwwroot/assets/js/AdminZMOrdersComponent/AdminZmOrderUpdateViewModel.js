$(function () {
    $.ajax({
        url: "/AdminZMOrderAssigning/GetUnAssignedOrder",
        type: "get",
        datatype: "json",
        data: { Id: $("#AUZmOrderInvoiceId").val() },
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            AddZmOrdersReset();
            if (response.error == "403") {
                location.href = "/Account/AccessDenied";
            }
            else if (response != null) {
                $("#UZmOrderInvoiceId").val(response.id);
                $("#UZmOrderCombineAreas").val(response.combineAreas);
                $("#UZmOrderCompanyName").val(response.companyName);
                $("#UZmOrderPackage").val(response.packageName);
                $("#UZmOrderPayment").val(response.modeName);
                var paymentmode = response.modeName.split(" ").join("");
                var trimpaymentmode = $.trim(paymentmode);
                if (trimpaymentmode.toLowerCase() == PaymentsMode.PayOrder.toLowerCase() || trimpaymentmode.toLowerCase() == PaymentsMode.Cheque.toLowerCase()) {
                    $("#UZmOrderBankChequeOnlineId").show();
                    $("#UZmOrderBankName").val(response.bankName);
                    $("#UZmOrderChqueNo").val(response.chequeNo);
                }
                UpdateUsersFromZone();
            }
            else {
                toastr.error("Order Not Availible", "Error");
            }
        },
        error: function (response) {
            toastr.error(response, "Error");
        }

    });
});

//Get Assigning Users List
function UpdateUsersFromZone() {
    var Bind = $("#UZmOrderAssignedTo");
    Bind.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
    $.ajax({
        url: "/AdminZMOrderAssigning/GetUserByZM",
        type: "get",
        datatype: "json",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            Bind.empty().append('<option selected="selected" value="">Please select</option>');
            $.each(response, function (i) {
                Bind.append($("<option></option>").val(this['id']).html(this['email']));
            });
        },
        error: function (response) {
            toastr.error(response, "Error");
        }

    });
}


$("#UpdateZmOrder_Submit_Form").validate({
    rules: {
        UZmOrderInvoiceId: {
            required: true,
            numberonly: true,
        },
        UZmOrderAssignedTo: {
            required: true,
        },
        UZmOrderNotes: {
            required: true,
        },
    },
    messages: {
        UZmOrderInvoiceId: {
            required: "Please Provide Passcode"
        },
        UZmOrderAssignedTo: {
            required: "Please Assigned User."
        },
        UZmOrderNotes: {
            required: "Please Provide Notes."
        },
    },

    highlight: function (element) {
        $(element).closest('.form-group').addClass('text-danger');
    },
    unhighlight: function (element) {
        $(element).closest('.form-group').removeClass('text-danger');
    },
    wrapper: 'div',
    errorClass: 'text-danger',
    errorPlacement: function (error, element) {
        error.insertAfter(element);
    },

    submitHandler: function (form) {
        $("#AddZmOrderTransferShowLoader").show();
        $("#AddZmOrderTransferShowButtons").hide();
        var instance = new VMZoneManagerTransfer();
        instance.Id = $("#UZmOrderInvoiceId").val();
        instance.AssignedTo = $("#UZmOrderAssignedTo").val();
        instance.Notes = $("#UZmOrderNotes").val();
        $.ajax({
            url: "/AdminZMOrderAssigning/UpdateAssigningOrders",
            type: "post",
            datatype: "json",
            data: { instance: instance },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else if (response.invoiceNo > 0) {
                    $("#AddZmOrderTransferShowLoader").hide();
                    $("#AddZmOrderTransferShowButtons").show();
                    toastr.success(response.processMessage, "Success");
                    window.location.href = "/AdminZMOrderAssigning/Index"
                }
                else {
                    $("#AddZmOrderTransferShowLoader").hide();
                    $("#AddZmOrderTransferShowButtons").show();
                    toastr.error(response.processMessage, "Error");
                }
            },
            error: function (response) {
                $("#AddZmOrderTransferShowLoader").hide();
                $("#AddZmOrderTransferShowButtons").show();
                toastr.error(response.processMessage, "Error");
            }

        });
    }
});

function AddZmOrdersReset() {
    $("#UZmOrderInvoiceId").val("");
    $("#UZmOrderCompanyName").val("");
    $("#UZmOrderCombineAreas").val("");
    $("#UZmOrderPackage").val("");
    $("#UZmOrderPayment").val("");
    $("#UZmOrderBankName").val("");
    $("#UZmOrderChqueNo").val("");
    $("#UZmOrderAssignedTo").val("");
    $("#UZmOrderNotes").val("");
    $("#UZmOrderBankChequeOnlineId").hide();
}