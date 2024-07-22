$("#RQueryForm_Submit").validate({
    rules: {
        RnameQuery: {
            required: true,
        },
        RnumberQuery: {
            required: true,
            numberonly: true,
            minlength: 11,
            maxlength: 11,
        },
        RProductQuery: {
            required: true,
        }
        
    },
    messages: {
        RnameQuery: {
            required: "Please Enter Name",
        },
        RnumberQuery: {
            required: "Mobile No Required",
        },
        RProductQuery: {
            required: "Please add name of Product",
        }  
    },
    highlight: function (element) {
        $(element).closest('.col form-col').addClass('text-danger');
    },
    unhighlight: function (element) {
        $(element).closest('.col form-col').removeClass('text-danger');
    },
    errorElement: 'span',
    errorClass: 'text-danger',
    errorPlacement: function (error, element) {
        error.insertAfter(element);
    },
    submitHandler: function (form) {
        var LQR = new ListingQueryRequest();
        LQR.RequestName = $("#RnameQuery").val();
        var RequestNo = $("#RnumberQuery").val();
        LQR.RequestMobNo = RequestNo.substring(1);
        LQR.SubCatId = $("#SbCateIdQuery").val();
        LQR.AreaName = $("#AreaNameQuery").val();
        LQR.ListingId = $("#ListingIdQuery").val();
        LQR.CityName = $("#LastCatesearchbycity").val();
        LQR.ProductName = $("#RProductQuery").val();
        LQR.SubCatName = $("#SbCateNameQuery").val();
        $("#contactModalQuery").modal("hide");
        toastr.success("Your request has been send successfully.", "success");
        $.ajax({
            url: "/Home/GetBulkQueryFormSubmittion",
            type: "post",
            datatype: "json",
            async: true,
            data: { request: LQR },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response > 0) {
                    $("#RProductQuery").val("");
                    $("#RnameQuery").val("");
                    $("#RnumberQuery").val("");
                    $("#SbCateIdQuery").val("");
                    $("#AreaNameQuery").val("");
                    $("#ListingIdQuery").val("");
                    //toastr.success("Your request has been send successfully.", "success");
                    $("#contactModalQuery").modal("hide");
                }
               
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }
});