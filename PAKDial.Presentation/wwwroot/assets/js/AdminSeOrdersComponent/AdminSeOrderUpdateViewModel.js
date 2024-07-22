$(function () {
    UpdateLoadPaymentMode();
    UpdateSeOrdersReset();
    $.ajax({
        url: "/AdminSeOrders/GetCollectSaleExOrders",
        type: "get",
        datatype: "json",
        data: { InvoiceId: $("#AUCSeOrderInvoiceId").val() },
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            if (response.error == "403") {
                location.href = "/Account/AccessDenied";
            }
            else if (response != null) {
                $("#UCSeOrderInvoiceId").val(response.id);
                $("#UCSeOrderCombineAreas").val(response.combineAreas);
                $("#UCSeOrderCompanyName").val(response.companyName);
                $("#UCSeOrderPackage").val(response.packageName);
                $("#UCSeOrderPaymentId").val(response.modeId);
                var paymentmode = response.modeName.split(" ").join("");
                var trimpaymentmode = $.trim(paymentmode);
                if (trimpaymentmode.toLowerCase() == PaymentsMode.PayOrder.toLowerCase() || trimpaymentmode.toLowerCase() == PaymentsMode.Cheque.toLowerCase()) {
                    $("#UCSeOrderBankChequeOnlineId").show();
                    $("#UCSeOrderBankName").val(response.bankName);
                    $("#UCSeOrderChqueNo").val(response.chequeNo);
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


    $("#UpdateCollectSeOrder_Submit_Form").validate({
        rules: {
            UCSeOrderInvoiceId: {
                required: true,
                numberonly: true,
            },
            UCSeOrderPaymentId: {
                required: true,
            },
            UCSeOrderBankName: {
                required: function () {
                    var paymentmode = $("#UCSeOrderPaymentId option:selected").text().split(" ").join("");
                    if (paymentmode.toLowerCase() != PaymentsMode.Cash.toLowerCase()) {
                        return true;
                    }
                }
            },
            UCSeOrderChqueNo: {
                required: function () {
                    var paymentmode = $("#UCSeOrderPaymentId option:selected").text().split(" ").join("");
                    if (paymentmode.toLowerCase() == PaymentsMode.Cheque.toLowerCase() || paymentmode.toLowerCase() == PaymentsMode.PayOrder.toLowerCase()) {
                        return true;
                    }
                }
            },
        },
        messages: {
            UCSeOrderInvoiceId: {
                required: "Please Provide Passcode"
            },
            UCSeOrderPaymentId: {
                required: "Please Select Payment Mode"
            },
            UCSeOrderBankName: {
                required: function () {
                    var paymentmode = $("#UCSeOrderPaymentId option:selected").text().split(" ").join("");
                    if (paymentmode.toLowerCase() != PaymentsMode.Cash.toLowerCase()) {
                        return "Please Enter Bank Name";
                    }
                }
            },
            UCSeOrderChqueNo: {
                required: function () {
                    var paymentmode = $("#UCSeOrderPaymentId option:selected").text().split(" ").join("");
                    if (paymentmode.toLowerCase() == PaymentsMode.Cheque.toLowerCase() || paymentmode.toLowerCase() == PaymentsMode.PayOrder.toLowerCase()) {
                        return "Please Enter Cheque No";
                    }
                }
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
            $("#UCSeOrderShowLoader").show();
            $("#UCSeOrderShowButtons").hide();
            var instance = new VMSaleExCollect();
            instance.Id = $("#UCSeOrderInvoiceId").val();
            instance.ModeId = $("#UCSeOrderPaymentId").val();
            instance.BankName = $("#UCSeOrderBankName").val();
            instance.ChequeNo = $("#UCSeOrderChqueNo").val();
            $.ajax({
                url: "/AdminSeOrders/UpdateCollectSaleExOrder",
                type: "post",
                datatype: "json",
                data: { instance: instance },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response.invoiceNo > 0) {
                        $("#UCSeOrderShowLoader").hide();
                        $("#UCSeOrderShowButtons").show();
                        toastr.success(response.processMessage, "Success");
                        window.location.href = "/AdminSeOrders/Index"
                    }
                    else {
                        $("#UCSeOrderShowLoader").hide();
                        $("#UCSeOrderShowButtons").show();
                        toastr.error(response.processMessage, "Error");
                    }
                },
                error: function (response) {
                    $("#UCSeOrderShowLoader").hide();
                    $("#UCSeOrderShowButtons").show();
                    toastr.error(response.processMessage, "Error");
                }

            });
        }
    });

});

//Get Load Payment Mode
function UpdateLoadPaymentMode() {
    var Bind = $("#UCSeOrderPaymentId");
    Bind.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
    $.ajax({
        url: "/AdminSeOrders/GetPaymentModes",
        type: "get",
        datatype: "json",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            Bind.empty().append('<option selected="selected" value="">Please select</option>');
            $.each(response.paymentModeList, function (i) {
                Bind.append($("<option></option>").val(this['id']).html(this['text']));
            });
        },
        error: function (response) {
            toastr.error(response, "Error");
        }

    });
}

$("#UCSeOrderPaymentId").change(function () {
    var paymentmode = $("#UCSeOrderPaymentId option:selected").text().split(" ").join("");
    var trimpaymentmode = $.trim(paymentmode);
    $("#UCSeOrderBankName").val("");
    $("#UCSeOrderChqueNo").val("");
    $("#UCSeOrderBankChequeOnlineId").hide();
    if (trimpaymentmode.toLowerCase() == PaymentsMode.PayOrder.toLowerCase() || trimpaymentmode.toLowerCase() == PaymentsMode.Cheque.toLowerCase()) {
        $("#UCSeOrderBankChequeOnlineId").show();
    }
});

function UpdateSeOrdersReset() {
    $("#UCSeOrderCombineAreas").val("");
    $("#UCSeOrderCompanyName").html("");
    $("#UCSeOrderPackage").val("");
    $("#UCSeOrderPaymentId").val("");
    $("#UCSeOrderBankName").val("");
    $("#UCSeOrderChqueNo").val("");
    $("#UCSeOrderBankChequeOnlineId").hide();
}