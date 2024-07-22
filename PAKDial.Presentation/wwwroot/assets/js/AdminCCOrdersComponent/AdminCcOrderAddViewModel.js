$(function () {
    var BindPackages = $("#ACcOrderPackageId");
    BindPackages.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
    var BindPaymentMode = $("#ACcOrderPaymentId");
    BindPaymentMode.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
    $.ajax({
        url: "/AdminCcOrders/GetPaymentModeandPackage",
        type: "get",
        datatype: "json",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            if (response != null) {
                BindPackages.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response.packagesList, function (i) {
                    BindPackages.append($("<option></option>").val(this['id']).html(this['text']));
                });
                BindPaymentMode.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response.paymentModeList, function (i) {
                    BindPaymentMode.append($("<option></option>").val(this['id']).html(this['text']));
                });
            }
            else {
                toastr.error("Data Not Exists", "Error");
            }
        },
        error: function (response) {
            toastr.error(response, "Error");
        }

    });

    //------------------------------------------------Combine City Area Search-------------------------------------------------------
    $("#ACcOrderCombineAreasId").select2({
        placeholder: "Please Select",
        ajax: {
            url: "/AdminCcOrders/SearchCombineCityArea",
            dataType: 'json',
            delay: 250,
            type: "get",
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            data: function (params) {
                return {
                    search: params.term, // search term
                    pageNo: params.page || 1,
                    pageSize: 20,
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;

                return {
                    results: data.combineCityAreas,
                    pagination: {
                        more: (params.page * 10) < data.rowCount
                    }
                };
            },
            cache: true
        },
        escapeMarkup: function (markup) { return markup; },
        minimumInputLength: 1,
    });

    //------------------------------------------------Company List in Search-------------------------------------------------------

    $("#ACcOrderCompanyNameId").select2({
        placeholder: "Please Select",
        ajax: {
            url: "/AdminCcOrders/SearchCompanyByArea",
            dataType: 'json',
            delay: 250,
            type: "get",
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            data: function (params) {
                return {
                    search: params.term, // search term
                    pageNo: params.page || 1,
                    pageSize: 20,
                    CityAreaId: $("#ACcOrderCombineAreasId").val()
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;

                return {
                    results: data.areasBasedCompany,
                    pagination: {
                        more: (params.page * 10) < data.rowCount
                    }
                };
            },
            cache: true
        },
        escapeMarkup: function (markup) { return markup; },
        minimumInputLength: 1,
    });

    //------------------------------------------------Add Cc Sale Order--------------------------------------------------------------------
    $("#AddCcOrder_Submit_Form").validate({
        rules: {
            ACcOrderCombineAreasId: {
                required: true,
                numberonly: true,
            },
            ACcOrderCompanyNameId: {
                required: true,
                numberonly: true,
            },
            ACcOrderPackageId: {
                required: true,
                numberonly: true,
            },
            ACcOrderPaymentId: {
                required: true,
                numberonly: true,
            },
            ACcOrderBankName: {
                required: function () {
                    var paymentmode = $("#ACcOrderPaymentId option:selected").text().split(" ").join("");
                    if (paymentmode.toLowerCase() != PaymentsMode.Cash.toLowerCase()) {
                        return true;
                    }
                }
            },
            ACcOrderChqueNo: {
                required: function () {
                    var paymentmode = $("#ACcOrderPaymentId option:selected").text().split(" ").join("");
                    if (paymentmode.toLowerCase() == PaymentsMode.Cheque.toLowerCase() || paymentmode.toLowerCase() == PaymentsMode.PayOrder.toLowerCase()) {
                        return true;
                    }
                }
            },
            ACcOrderAccNo: {
                required: function () {
                    var paymentmode = $("#ACcOrderPaymentId option:selected").text().split(" ").join("");
                    if (paymentmode.toLowerCase() == PaymentsMode.OnlinePayment.toLowerCase()) {
                        return true;
                    }
                }
            },
        },
        messages: {
            ACcOrderCombineAreasId: {
                required: "Please Select Area."
            },
            ACcOrderCompanyNameId: {
                required: "Please Select Company."
            },
            ACcOrderPackageId: {
                required: "Please Select Package."
            },
            ACcOrderPaymentId: {
                required: "Please Select Payment Mode."
            },
            ACcOrderBankName: {
                required: function () {
                    var paymentmode = $("#ACcOrderPaymentId option:selected").text().split(" ").join("");
                    if (paymentmode.toLowerCase() != PaymentsMode.Cash.toLowerCase()) {
                        return "Please Enter Bank Name";
                    }
                }
            },
            ACcOrderChqueNo: {
                required: function () {
                    var paymentmode = $("#ACcOrderPaymentId option:selected").text().split(" ").join("");
                    if (paymentmode.toLowerCase() == PaymentsMode.Cheque.toLowerCase() || paymentmode.toLowerCase() == PaymentsMode.PayOrder.toLowerCase()) {
                        return "Please Enter Cheque No";
                    }
                }
            },
            ACcOrderAccNo: {
                required: function () {
                    var paymentmode = $("#ACcOrderPaymentId option:selected").text().split(" ").join("");
                    if (paymentmode.toLowerCase() == PaymentsMode.OnlinePayment.toLowerCase()) {
                        return "Please Enter Account No";
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
            if (element.attr("name") == "ACcOrderCombineAreasId" || element.attr("name") == "ACcOrderCompanyNameId") {
                error.insertAfter(element.next());
            }
            else {
                error.insertAfter(element);
            }
        },

        submitHandler: function (form) {
            $("#AddCcOrderShowLoader").show();
            $("#AddCcOrderShowButtons").hide();
            var instance = new VMCallCenterExCreate();
            instance.BankName = $("#ACcOrderBankName").val();
            instance.AccountNo = $("#ACcOrderAccNo").val();
            instance.ChequeNo = $("#ACcOrderChqueNo").val();
            instance.ListingId = $("#ACcOrderCompanyNameId").val();
            instance.PackageId = $("#ACcOrderPackageId").val();
            instance.ModeId = $("#ACcOrderPaymentId").val();
            $.ajax({
                url: "/AdminCcOrders/AddSaleOrderCcPost",
                type: "post",
                datatype: "json",
                data: { instance: instance },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response.invoiceNo > 0) {
                        $("#AddCcOrderShowLoader").hide();
                        $("#AddCcOrderShowButtons").show();
                        toastr.success(response.processMessage, "Success");
                        window.location.href = "/AdminCcOrders/SaleCcReport?InvoiceId=" + response.invoiceNo
                    }
                    else {
                        $("#AddCcOrderShowLoader").hide();
                        $("#AddCcOrderShowButtons").show();
                        toastr.error(response.processMessage, "Error");
                    }
                },
                error: function (response) {
                    $("#AddCcOrderShowLoader").hide();
                    $("#AddCcOrderShowButtons").show();
                    toastr.error(response.processMessage, "Error");
                }

            });
        }
    });


});


$("#ACcOrderCombineAreasId").change(function () {
    $("#ACcOrderCompanyNameId").html("");
});

$("#ACcOrderPaymentId").change(function () {
    var paymentmode = $("#ACcOrderPaymentId option:selected").text().split(" ").join("");
    var trimpaymentmode = $.trim(paymentmode);
    $("#ACcOrderBankName").val("");
    $("#ACcOrderChqueNo").val("");
    $("#ACcOrderAccNo").val("");
    $("#ACcOrderBankChequeOnlineId").hide();
    $("#ACcOrderSubBankChequeId").hide();
    $("#ACcOrderSubBankOnlineId").hide();
    if (trimpaymentmode.toLowerCase() == PaymentsMode.PayOrder.toLowerCase() || trimpaymentmode.toLowerCase() == PaymentsMode.Cheque.toLowerCase()) {
        $("#ACcOrderBankChequeOnlineId").show();
        $("#ACcOrderSubBankChequeId").show();
        $("#ACcOrderSubBankOnlineId").hide();
    }
    else if (trimpaymentmode.toLowerCase() == PaymentsMode.OnlinePayment.toLowerCase()) {
        $("#ACcOrderBankChequeOnlineId").show();
        $("#ACcOrderSubBankOnlineId").show();
        $("#ACcOrderSubBankChequeId").hide();
    }
});

function AddCcOrdersReset() {
    $("#ACcOrderCompanyNameId").html("");
    $("#ACcOrderCombineAreasId").html("");
    $("#ACcOrderPackageId").val("");
    $("#ACcOrderPaymentId").val("");
    $("#ACcOrderBankName").val("");
    $("#ACcOrderChqueNo").val("");
    $("#ACcOrderAccNo").val("");
    $("#ACcOrderBankChequeOnlineId").hide();
    $("#ACcOrderSubBankChequeId").hide();
    $("#ACcOrderSubBankOnlineId").hide();
}