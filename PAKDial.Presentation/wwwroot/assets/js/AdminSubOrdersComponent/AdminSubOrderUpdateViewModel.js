$(function () {
    $.ajax({
        url: "/AdminSubOrders/GetSubOrdersById",
        type: "get",
        datatype: "json",
        data: { Id: $("#AUSubOrderInvoiceId").val() },
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            UpdateSubOrdersReset();
            if (response.error == "403") {
                location.href = "/Account/AccessDenied";
            }
            else if (response != null) {
                $("#USubOrderInvoiceId").val(response.id);
                $("#USubOrderCombineAreas").val(response.combineAreas);
                $("#USubOrderCompanyName").val(response.companyName);
                $("#USubOrderPackage").val(response.packageName);
                $("#USubOrderPayment").val(response.modeName);
                var paymentmode = response.modeName.split(" ").join("");
                var trimpaymentmode = $.trim(paymentmode);
                if (trimpaymentmode.toLowerCase() == PaymentsMode.PayOrder.toLowerCase() || trimpaymentmode.toLowerCase() == PaymentsMode.Cheque.toLowerCase()) {
                    $("#USubOrderBankChequeOnlineId").show();
                    $("#USubBankOnlineId").hide();
                    $("#USubChequeId").show();
                    $("#USubOrderBankName").val(response.bankName);
                    $("#USubOrderChqueNo").val(response.chequeNo);
                }
                else if (trimpaymentmode.toLowerCase() == PaymentsMode.OnlinePayment.toLowerCase()) {
                    $("#USubOrderBankChequeOnlineId").show();
                    $("#USubBankOnlineId").show();
                    $("#USubChequeId").hide();
                    $("#USubOrderBankName").val(response.bankName);
                    $("#USubOrderAccNo").val(response.accountNo);
                }
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


$("#UpdateSubOrder_Submit_Form").validate({
    rules: {
        USubOrderInvoiceId: {
            required: true,
            numberonly: true,
        },
        USubOrderStatusId: {
            required: true,
        }
    },
    messages: {
        USubOrderInvoiceId: {
            required: "Please Provide Passcode"
        },
        USubOrderStatusId: {
            required: "Please Select Status."
        }
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
        $("#AddSubOrdersShowLoader").show();
        $("#AddSubOrdersShowButtons").hide();
        var instance = new SubAdminOrders();
        instance.Id = $("#USubOrderInvoiceId").val();
        instance.StatusName = $("#USubOrderStatusId").val();
        $.ajax({
            url: "/AdminSubOrders/UpdateOrder",
            type: "post",
            datatype: "json",
            data: { instance: instance },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else if (response.invoiceNo > 0) {
                    $("#AddSubOrdersShowLoader").hide();
                    $("#AddSubOrdersShowButtons").show();
                    toastr.success(response.processMessage, "Success");
                    window.location.href = "/AdminSubOrders/Index"
                }
                else {
                    $("#AddSubOrdersShowLoader").hide();
                    $("#AddSubOrdersShowButtons").show();
                    toastr.error(response.processMessage, "Error");
                }
            },
            error: function (response) {
                $("#AddSubOrdersShowLoader").hide();
                $("#AddSubOrdersShowButtons").show();
                toastr.error(response.processMessage, "Error");
            }

        });
    }
});

function UpdateSubOrdersReset() {
    $("#USubOrderInvoiceId").val("");
    $("#USubOrderCompanyName").val("");
    $("#USubOrderCombineAreas").val("");
    $("#USubOrderPackage").val("");
    $("#USubOrderPayment").val("");
    $("#USubOrderBankName").val("");
    $("#USubOrderChqueNo").val("");
    $("#USubOrderAccNo").val("");
    $("#USubOrderStatusId").val("");
    $("#USubOrderBankChequeOnlineId").hide();
    $("#USubBankOnlineId").hide();
    $("#USubChequeId").hide();
}