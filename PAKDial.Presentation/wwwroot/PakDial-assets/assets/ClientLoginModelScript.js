//$(function () {
//    $("#ClientLoginsBtn").on("click", function () {
        
//        var validator = $("#ClientLogin_Submit").validate();
//        validator.resetForm();
//        $("#ClientLoginEmail").val("");
//        $("#ClientLoginPassword").val("");
//        $("#ClientLoginremember").prop('checked', false);
//        $("#LoginWrongpassword").css('display', 'none');
//        $("#ClientLoginsModel").modal("show");
//    });
//    $("#RegClientLogins").on("click", function () {
       
//        var validator = $("#ClientLogin_Submit").validate();
//        validator.resetForm();
//        $("#ClientLoginEmail").val("");
//        $("#ClientLoginPassword").val("");
//        $("#ClientLoginremember").prop('checked', false);
//        $("#LoginWrongpassword").css('display', 'none');
//        $("#ClientLoginsModel").modal("show");
//    });
//});

//$("#ClientLogin_Submit").validate({
//    rules: {
//        ClientLoginEmail: {
//            required: true,
//            emailRegex: true,
//        },
//        ClientLoginPassword: {
//            required: true,
//        },

//    },
//    messages: {
//        ClientLoginOtpEmail: {
//            required: "Email Required",
//        },
//        ClientLoginPassword: {
//            required: "Password Required",
//        },
//    },
//    highlight: function (element) {
//        $(element).closest('.col form-col').addClass('text-danger');
//    },
//    unhighlight: function (element) {
//        $(element).closest('.col form-col').removeClass('text-danger');
//    },
//    errorElement: 'span',
//    errorClass: 'text-danger',
//    errorPlacement: function (error, element) {
//        error.insertAfter(element);
//    },
//    submitHandler: function (form) {
//        var request = new LoginViewModel();
//        request.Email = $("#ClientLoginEmail").val();
//        request.Password = $("#ClientLoginPassword").val();
//        if ($("#ClientLoginremember").prop('checked') == true) {
//            request.RememberMe = true;
//        }
//        else {
//            request.RememberMe = false;
//        }
//        $.ajax({
//            url: "/Home/AccountsLogin",
//            type: "post",
//            datatype: "json",
//            data: { model: request },
//            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
//            success: function (response) {
//                if (response.results == true) {
//                    $("#LoginWrongpassword").css('display', 'none');
//                    $("#ClientLoginEmail").val();
//                    $("#ClientLoginPassword").val();
//                    $("#ClientLoginsModel").modal("hide");
//                    if (window.location.href.indexOf("Login") > -1) {
//                        window.location.href = "/Home/Index";
//                    }
//                    else {
//                        window.location.href = window.location.href;
//                    }
//                }
//                else if (response.results == false) {
//                    $("#LoginWrongpassword").css('display', 'block');
//                }
//            },
//            error: function (response) {
//                toastr.error(response, "Error");
//            }

//        });
//    }
//});

$(function () {
   
    $("#ClientLoginsBtn").on("click", function () {
        $("#ClientLoginOtpEmail").val("");
        $("#ClientLoginOtpNo").val("");
        $("#ClientOtpCodeNo").val("");
        $("#Wrongpassword").css('display', 'none');
        $("#Otpwrongpassword").css('display', 'none');
        $("#ClientLoginOtpModel").modal("show");
    });
});

$("#ClientLoginOtp_Submit").validate({
    rules: {
        ClientLoginOtpEmail: {
            required: true,
            emailRegex: true,
        },
        ClientLoginOtpNo: {
            required: true,
            numberonly: true,
            minlength: 11,
            maxlength: 11,
        },

    },
    messages: {
        ClientLoginOtpEmail: {
            required: "Email Required",
        },
        ClientLoginOtpNo: {
            required: "Mobile No Required",
        },
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
      
        var request = new ClientLogin();
        request.UserName = $("#ClientLoginOtpEmail").val();
        request.Number = $("#ClientLoginOtpNo").val();
        $.ajax({
            url: "/Home/AccountLogin",
            type: "post",
            datatype: "json",
            data: { login: request },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == true) {
                    $("#temp").html(request.Number);
                    $("#Otpwrongpassword").css('display', 'none');
                    $("#Wrongpassword").css('display', 'none');
                    $("#ClientLoginOtpEmail").val("");
                    $("#ClientLoginOtpNo").val("");
                    $("#ClientOtpCodeNo").val("");
                    $("#ClientLoginOtpModel").modal("hide");
                    
                    $("#ClientOtpCodeModel").modal("show");
                }
                else {
                    $("#Wrongpassword").css('display', 'block');
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }
});

$("#ClientChangeNo_Submit").validate({
    rules: {
        ClienchngOtpEmail: {
            required: true,
            emailRegex: true,
        },
        ClientchngOtpNo: {
            required: true,
            numberonly: true,
            minlength: 11,
            maxlength: 11,
        },
        ClientchngOtpNewNo: {
            required: true,
            numberonly: true,
            minlength: 11,
            maxlength: 11,
        },
    },
    messages: {
        ClienchngOtpEmail: {
            required: "User Name  Required",
        },
        ClientchngOtpNo: {
            required: "Old Mobile No Required",
        },
        ClientchngOtpNewNo: {
            required: "New Mobile No Required",
        },
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
     
        var request = new ClientchangeNumber();
        request.UserName = $("#ClienchngOtpEmail").val();
        request.Number = $("#ClientchngOtpNo").val();
        request.NewNumber = $("#ClientchngOtpNewNo").val();
        $.ajax({
            url: "/Home/ChangeNumber",
            type: "post",
            datatype: "json",
            data: { login: request },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
               
                if (response == 1) {
                    $("#temp").html(request.NewNumber);
                    $("#Otpwrongpassword").css('display', 'none');
                    $("#Wrongpassword").css('display', 'none');
                    $("#ClientLoginOtpEmail").val("");
                    $("#ClientLoginOtpNo").val("");
                    $("#ClientOtpCodeNo").val("");
                    $("#ClientChangeNoModel").modal("hide");

                    $("#ClientOtpCodeModel").modal("show");
                } else if (response == 2)
                {
                    toastr.error("already Exists Number.","Error");
                }
                else {
                    $("#NbrWrongpassword").css('display', 'block');
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }
});

$("#ChangeNumber").click(function (e) {
   
    $("#ClientOtpCodeModel").modal("hide");
    $("#ClientChangeNoModel").modal("show");
});

$("#ResendCode").click(function (e) {

    var no = $("#temp").text();
    $.ajax({
        url: "/Home/ResendCode",
        type: "post",
        datatype: "json",
        data: { No: no },
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
           
        },
        error: function (response) {
            toastr.error(response, "Error");
        }

    });
});
$(".btnfacebook").click(function (e) {
    debugger;
   // var no = $("#temp").text();
    var _provider = "Facebook";
    $.ajax({
        url: "/Home/ExternalLogin",
        type: "post",
        datatype: "json",
        data: { provider: _provider },
        //headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {

        },
        error: function (response) {
            toastr.error(response, "Error");
        }

    });
});
$("#ResendCode").click(function (e) {

    var no = $("#temp").text();
    $.ajax({
        url: "/Home/ResendCode",
        type: "post",
        datatype: "json",
        data: { No: no },
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {

        },
        error: function (response) {
            toastr.error(response, "Error");
        }

    });
});


$("#ClientOtpCode_Submit").validate(
    {
    rules: {
        ClientOtpCodeNo: {
            required: true,
            numberonly: true,
            minlength: 4,
            maxlength: 4,
        },

    },
    messages: {
        ClientOtpCodeNo: {
            required: "OTP Code Required",
        },
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
        var Value = $("#ClientOtpCodeNo1").val() + $("#ClientOtpCodeNo2").val() + $("#ClientOtpCodeNo3").val() + $("#ClientOtpCodeNo4").val()
        debugger
        $.ajax({
            url: "/Home/LoginOtp",
            type: "post",
            datatype: "json",
            data: { OtpCode: Value },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response.results == true) {
                    $("#Otpwrongpassword").css('display', 'none');
                    $("#ClientOtpCodeNo").val("");
                    $("#ClientOtpCodeModel").modal("hide");
                    window.location.href = window.location.href;
                }
                else if (response.results == false && response.wrongCount < 2) {
                    $("#OptWrongpassword").css('display', 'block');
                }
                else if (response.results == false && response.wrongCount == 2) {
                    $("#OptWrongpassword").css('display', 'none');
                    $("#ClientOtpCodeModel").modal("hide");
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
        }

});