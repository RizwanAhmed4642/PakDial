$(function () {
    var BindPackages = $("#ASeOrderPackageId");
    $("#ASeOrderIsDiscount").prop("checked", false);
    $("#DiscountElements").find("*").prop('disabled', true);
    //$("#DiscountElements").children().prop('disabled', false);
    BindPackages.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
    var BindPaymentMode = $("#ASeOrderPaymentId");
    BindPaymentMode.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
    $.ajax({
        url: "/AdminSeOrders/GetPaymentModeandPackage",
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
    $("#ASeOrderCombineAreasId").select2({
        placeholder: "Please Select",
        ajax: {
            url: "/AdminSeOrders/SearchCombineCityArea",
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

    $("#ASeOrderCompanyNameId").select2({
        placeholder: "Please Select",
        ajax: {
            url: "/AdminSeOrders/SearchCompanyByArea",
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
                    CityAreaId: $("#ASeOrderCombineAreasId").val()
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

    //------------------------------------------------Add SE Sale Order--------------------------------------------------------------------
    $("#AddSeOrder_Submit_Form").validate({
        rules: {
            ASeOrderCombineAreasId: {
                required: true,
                numberonly: true,
            },
            ASeOrderCompanyNameId: {
                required: true,
                numberonly: true,
            },
            ASeOrderPackageId: {
                required: true,
                numberonly: true,
            },
            ASeOrderPaymentId: {
                required: true,
                numberonly: true,
            },
            ASeOrderBankName: {
                required: function () {
                    var paymentmode = $("#ASeOrderPaymentId option:selected").text().split(" ").join("");
                    if (paymentmode.toLowerCase() != PaymentsMode.Cash.toLowerCase()) {
                        return true;
                    }
                }
            },
            ASeOrderChqueNo: {
                required: function () {
                    var paymentmode = $("#ASeOrderPaymentId option:selected").text().split(" ").join("");
                    if (paymentmode.toLowerCase() == PaymentsMode.Cheque.toLowerCase() || paymentmode.toLowerCase() == PaymentsMode.PayOrder.toLowerCase()) {
                        return true;
                    }
                }
            },
            ASeOrderAccNo: {
                required: function () {
                    var paymentmode = $("#ASeOrderPaymentId option:selected").text().split(" ").join("");
                    if (paymentmode.toLowerCase() == PaymentsMode.OnlinePayment.toLowerCase()) {
                        return true;
                    }
                }
            },
            ASeOrderDiscout: {
                required: function () {
                    if ($("#ASeOrderIsDiscount").prop('checked') == true) {
                        return true;
                    }
                },
                numberonly: function () {
                    if ($("#ASeOrderIsDiscount").prop('checked') == true) {
                        return true;
                    }
                }

            },
        },
        messages: {
            ASeOrderCombineAreasId: {
                required: "Please Select Area."
            },
            ASeOrderCompanyNameId: {
                required: "Please Select Company."
            },
            ASeOrderPackageId: {
                required: "Please Select Package."
            },
            ASeOrderPaymentId: {
                required: "Please Select Payment Mode."
            },
            ASeOrderBankName: {
                required: function () {
                    var paymentmode = $("#ASeOrderPaymentId option:selected").text().split(" ").join("");
                    if (paymentmode.toLowerCase() != PaymentsMode.Cash.toLowerCase()) {
                        return "Please Enter Bank Name";
                    }
                }
            },
            ASeOrderChqueNo: {
                required: function () {
                    var paymentmode = $("#ASeOrderPaymentId option:selected").text().split(" ").join("");
                    if (paymentmode.toLowerCase() == PaymentsMode.Cheque.toLowerCase() || paymentmode.toLowerCase() == PaymentsMode.PayOrder.toLowerCase()) {
                        return "Please Enter Cheque No";
                    }
                }
            },
            ASeOrderAccNo: {
                required: function () {
                    var paymentmode = $("#ASeOrderPaymentId option:selected").text().split(" ").join("");
                    if (paymentmode.toLowerCase() == PaymentsMode.OnlinePayment.toLowerCase()) {
                        return "Please Enter Account No";
                    }
                }
            },
            ASeOrderDiscout: {
                required: function () {
                    if ($("#ASeOrderIsDiscount").prop('checked') == true) {
                        return "Please enter Discount";
                    }
                },
                numberonly: function () {
                    if ($("#ASeOrderIsDiscount").prop('checked') == true) {
                        return "please enter only number";
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
            if (element.attr("name") == "ASeOrderCombineAreasId" || element.attr("name") == "ASeOrderCompanyNameId") {
                error.insertAfter(element.next());
            }
            else {
                error.insertAfter(element);
            }
        },

        submitHandler: function (form) {
           
            $("#AddSeOrderShowLoader").show();
            $("#AddSeOrderShowButtons").hide();
            var instance = new VMSaleExCreate();
            instance.BankName = $("#ASeOrderBankName").val();
            instance.AccountNo = $("#ASeOrderAccNo").val();
            instance.ChequeNo = $("#ASeOrderChqueNo").val();
            instance.ListingId = $("#ASeOrderCompanyNameId").val();
            instance.PackageId = $("#ASeOrderPackageId").val();
            instance.ModeId = $("#ASeOrderPaymentId").val();
            instance.Discount = $("#ASeOrderDiscout").val();
            instance.DiscountType = $("input[name='inlineDiscountOption']:checked").val();            
            if ($("#ASeOrderIsDiscount").prop('checked') == true) {
                instance.IsDiscount = true;
            }
            $.ajax({
                url: "/AdminSeOrders/AddSaleExOrder",
                type: "post",
                datatype: "json",
                data: { instance: instance },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response.invoiceNo > 0) {
                        $("#AddSeOrderShowLoader").hide();
                        $("#AddSeOrderShowButtons").show();
                        toastr.success(response.processMessage, "Success");
                        window.location.href = "/AdminSeOrders/SaleExReport?InvoiceId=" + response.invoiceNo
                    }
                    else {
                        $("#AddSeOrderShowLoader").hide();
                        $("#AddSeOrderShowButtons").show();
                        toastr.error(response.processMessage, "Error");
                    }
                },
                error: function (response) {
                    $("#AddSeOrderShowLoader").hide();
                    $("#AddSeOrderShowButtons").show();
                    toastr.error(response.processMessage, "Error");
                }

            });
        }
    });


     
});

$("#ASeOrderIsDiscount").change(function () {
    if (this.checked) {
        $("#DiscountElements").find("*").prop('disabled', false);
    }
    else {

        $("#DiscountElements").find("*").prop('disabled', true);        
    }
});
  


$("#ASeOrderCombineAreasId").change(function () {
    $("#ASeOrderCompanyNameId").html("");
});

$("#ASeOrderPaymentId").change(function () {
    var paymentmode = $("#ASeOrderPaymentId option:selected").text().split(" ").join("");
    var trimpaymentmode = $.trim(paymentmode);
    $("#ASeOrderBankName").val("");
    $("#ASeOrderChqueNo").val("");
    $("#ASeOrderAccNo").val("");
    $("#ASeOrderBankChequeOnlineId").hide();
    $("#ASeOrderSubBankChequeId").hide();
    $("#ASeOrderSubBankOnlineId").hide();
    if (trimpaymentmode.toLowerCase() == PaymentsMode.PayOrder.toLowerCase() || trimpaymentmode.toLowerCase() == PaymentsMode.Cheque.toLowerCase()) {
        $("#ASeOrderBankChequeOnlineId").show();
        $("#ASeOrderSubBankChequeId").show();
        $("#ASeOrderSubBankOnlineId").hide();
    }
    else if (trimpaymentmode.toLowerCase() == PaymentsMode.OnlinePayment.toLowerCase()) {
        $("#ASeOrderBankChequeOnlineId").show();
        $("#ASeOrderSubBankOnlineId").show();
        $("#ASeOrderSubBankChequeId").hide();
    }
});

function AddSeOrdersReset() {
    $("#ASeOrderCompanyNameId").html("");
    $("#ASeOrderCombineAreasId").html("");
    $("#ASeOrderPackageId").val("");
    $("#ASeOrderPaymentId").val("");
    $("#ASeOrderBankName").val("");
    $("#ASeOrderChqueNo").val("");
    $("#ASeOrderAccNo").val("");
    $("#ASeOrderBankChequeOnlineId").hide();
    $("#ASeOrderSubBankChequeId").hide();
    $("#ASeOrderSubBankOnlineId").hide();
}