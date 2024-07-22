$(function () {
    if ($("#EditCutomerId").val() > 0) {

        var Id = $("#EditCutomerId").val();
        if (Id != "" && Id != null) {

            $.ajax({
                url: "/ClientListing/GetCustomerById",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {

                    if (response != null) {
                     
                        $("#UCustId").val(response.id);
                        if (response.imagePath != null && response.imagePath !="" ) {
                            $('#UtargetImg').attr("src", response.imagePath);
                        }
                        else {
                            $('#UtargetImg').attr("src", "/PakDial-assets/images/user-d.png");
                        
                        }
                        if (response.isActive == true) {
                            $("#UCIsActive").prop('checked', true);
                        }
                        $("#UUserId").val(response.userId);
                        $("#UCustFirstName").val(response.firstName);
                        $("#UCustLastName").val(response.lastName);
                        $("#UCustDob").val(dateFormat(new Date(Date.parse(response.dateOfBirth))));
                        $("#UCustCnic").val(response.cnic);
                        $("#UCustContactNumber").val(response.phoneNumber);
                        $("#UCustPhoneNumber").val(response.phoneNumber);
                        //$("#UpdateCustomer").modal('show');
                    }
                    else {
                        toastr.error("Customer Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
    }
});

$("#UpdateCustomer_Submit_Form").validate({

    rules: {
        UCustId: {
            required: true,
        },
        UUserId: {
            required: true,
        },
        UCustFirstName: {
            required: true,
            letteronly: true,
        },
        UCustLastName: {
            required: true,
            letteronly: true
        },
        UCustDob: {
            required: true,
        },
        UCustCnic: {
            required: true,
            numberonlyCnic: true
        },
        UCustContactNumber: {
            required: true,
            numberonly: true
        },
        UCustPhoneNumber: {
            numberonly: true
        },
    },
    messages: {
        UCustId: {
            required: "Please provide a valid Employee Passcode."
        },
        UUserId: {
            required: "Please provide a valid Employee Passcode."
        },
        UCustFirstName: {
            required: "Please Enter First Name."
        },
        UCustLastName: {
            required: "Please Enter Last Name."
        },
        UCustDob: {
            required: "Please Enter Date of Birth."
        },
        UCustCnic: {
            required: "Please Enter CNIC."
        },
        UCustContactNumber: {
            required: "Please Enter Contact Number."
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

        $("#UpdateCustShowLoader").show();
        $("#UpdateCustShowButtons").hide();
        var Cust = new CreateUpdateCustomer();
        Cust.Id = $("#UCustId").val();
        Cust.UserId = $("#UUserId").val();
        Cust.FirstName = $("#UCustFirstName").val();
        Cust.LastName = $("#UCustLastName").val();
        Cust.DateOfBirth = $("#UCustDob").val();
        Cust.Cnic = $("#UCustCnic").val();
        Cust.ContactNo = $("#UCustContactNumber").val();
        Cust.PhoneNumber = $("#UCustContactNumber").val();
       
        $.ajax({
            url: "/ClientListing/UpdateCustomer",
            type: "post",
            datatype: "json",
            data: { customer: Cust },
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
               
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else if (response == 1) {
                    var files = $("#Uimagefiles").get(0).files;
                    var formData = new FormData();
                    var Id = $("#UCustId").val();
                    formData.append('file', files[0]);
                    if (files.length > 0) {
                        $.ajax({
                            url: "/ClientListing/UploadProfileImage?Id=" + Id,
                            type: "post",
                            datatype: "json",
                            data: formData,
                            async: false,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                if (response == 1) {
                                    $("#UpdateCustomer").modal("hide");
                                    $("#UpdateCustShowLoader").hide();
                                    $("#UpdateCustShowButtons").show();
                                    toastr.success("Customer Updated Successfully", "Success");
                                    window.location.href = "/profileUser/Profile";
                                }
                                else if (response.error == "403") {
                                    location.href = "/Account/AccessDenied";
                                }
                                else {
                                    $("#UpdateCustShowLoader").hide();
                                    $("#UpdateCustShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                }
                            },
                            error: function (response) {
                                $("#UpdateCustShowLoader").hide();
                                $("#UpdateCustShowButtons").show();
                                toastr.error(response, "Error");
                            }
                        });
                    }
                    else {
                        $("#UpdateCustomer").modal("hide");
                        $("#UpdateCustShowLoader").hide();
                        $("#UpdateCustShowButtons").show();
                        toastr.success("Customer Updated Successfully", "Success");
                        window.location.href = "/profileUser/Profile";
                    }
                }
                else if (response == 2) {
                    $("#UpdateCustShowLoader").hide();
                    $("#UpdateCustShowButtons").show();
                    toastr.error("Cnic Already Exits", "Error");
                }
                else {
                    $("#UpdateCustShowLoader").hide();
                    $("#UpdateCustShowButtons").show();
                    toastr.error("Customer Not Saved", "Error");
                }
            },
            error: function (response) {
                $("#UpdateCustShowLoader").hide();
                $("#UpdateCustShowButtons").show();
                toastr.error(response, "Error");
            }

        });
    }
});


$("#btnClose").click(function () {
    window.location.href = "/profileUser/Profile";
});
$("#Uimagefiles").change(function () {
    var File = this.files;
    if (File && File[0]) {
        UReadImage(File[0]);
    }
});

var UReadImage = function (file) {

    var reader = new FileReader;
    var image = new Image;
    reader.readAsDataURL(file);
    reader.onload = function (_file) {

        image.src = _file.target.result;
        image.onload = function () {

            var height = this.height;
            var width = this.width;
            var type = file.type;
            var size = ~~(file.size / 1024) + "KB";

            $("#UtargetImg").attr('src', _file.target.result);
        }

    }

}
$('#ACustCnic,#UCustCnic').keydown(function () {
    //allow  backspace, tab, ctrl+A, escape, carriage return
    if (event.keyCode == 8 || event.keyCode == 9
        || event.keyCode == 27 || event.keyCode == 13
        || (event.keyCode == 65 && event.ctrlKey === true))
        return;
    if ((event.keyCode < 48 && event.keyCode > 57) || (event.keyCode < 96 && event.keyCode > 105))
        event.preventDefault();
    var length = $(this).val().length;
    if (length == 5 || length == 13)
        $(this).val($(this).val() + '-');
});

$("#UCustPhoneNumber").focusout(function () {
    var Mobile = $(this).val();
    var Id = $("#UCustId").val();
    if (Mobile != "") {
        $.ajax({
            url: "/ClientListing/CheckMobileExiting",
            type: "get",
            datatype: "json",
            data: { phone: Mobile, id: Id },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response > 0) {
                    toastr.error("Number Already Exits");
                    $("#UCustPhoneNumber").val("");
                    $("#UCustPhoneNumber").focus();
                }

            },
            error: function (response) {
                toastr.error(response);
            }

        });
    }

}); 