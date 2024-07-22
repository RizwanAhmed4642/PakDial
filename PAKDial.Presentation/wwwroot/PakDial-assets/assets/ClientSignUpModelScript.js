$(function () {
    $("#ClientSignUpBtn").on("click", function () {
        var validator = $("#ClientSignUp_Submit").validate();
        validator.resetForm();
        $("#ClientSignUpFirstName").val("");
        //$("#ClientSignUpLastName").val("");
        $("#ClientSignUpEmail").val("");
        $("#ClientSignUpMobile").val("");
        $("#ClientSignUpPassword").val("");
        //$("#ClientSignUpConfirmPassword").val("");
        $("#SignUpSpan").removeClass();
        $("#SignUpSpan").text("");
        $("#SignUpSpan").css('display', 'none');
        $("#ClientSignUpModel").modal("show");
    });
    $("#RegClientSignUpBtn").on("click", function () {
        var validator = $("#ClientSignUp_Submit").validate();
        validator.resetForm();
        $("#ClientSignUpFirstName").val("");
        //$("#ClientSignUpLastName").val("");
        $("#ClientSignUpEmail").val("");
        $("#ClientSignUpMobile").val("");
        $("#ClientSignUpPassword").val("");
        //$("#ClientSignUpConfirmPassword").val("");
        $("#SignUpSpan").removeClass();
        $("#SignUpSpan").text("");
        $("#SignUpSpan").css('display', 'none');
        $("#ClientSignUpModel").modal("show");
    });
});

$("#ClientSignUp_Submit").validate({
    rules: {
        ClientSignUpFirstName: {
            required: true,
        },
        //ClientSignUpLastName: {
        //    required: true,
        //},
        ClientSignUpMobile: {
            required: true,
            numberonly: true,
            minlength: 11,
            maxlength: 11
        },
        ClientSignUpEmail: {
            required: true,
            emailRegex: true,
        },
        ClientSignUpPassword: {
            required: true,
            passwordRegex: true,
        },
        ClientSignUpConcent: {
            required: true
        }
        //ClientSignUpConfirmPassword: {
        //    equalTo: "#ClientSignUpPassword"
        //},

    },
    messages: {
        ClientSignUpFirstName: {
            required: "First Name Required",
        },
        //ClientSignUpLastName: {
        //    required: "Last Name Required",
        //},
        ClientSignUpMobile: {
            required: "Mobile No Required",
        },
        ClientSignUpEmail: {
            required: "Email Required",
        },
        ClientSignUpPassword: {
            required: "Password Required",
        },
        ClientSignUpConcent: {
            required: "You Must Check the Term&Conditions"
        }
        //ClientSignUpConfirmPassword: " Enter Confirm Password Same as Password"
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
        var name = $(element).attr("id");
        if (name == "ClientSignUpConcent") {
            error.insertAfter('.remember-checkbox');
        }
        else {
            error.insertAfter(element);
        }
    },
    submitHandler: function (form) {
    
        var request = new RegisterViewModels();
        request.FirstName = $("#ClientSignUpFirstName").val();
        request.LastName = "";
        request.MobileNo = $("#ClientSignUpMobile").val();
        request.Email = $("#ClientSignUpEmail").val();
        request.Password = $("#ClientSignUpPassword").val();
        $.ajax({
            url: "/Home/RegisterJson",
            type: "post",
            datatype: "json",
            data: { model: request },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                
                if (response.result == 1) {
                    //$("#SignUpSpan").removeClass();
                    //$("#SignUpSpan").text(response.message);
                    //$("#SignUpSpan").addClass('text-primary');
                    //$("#SignUpSpan").css('display', 'block');
                    //$("#ClientSignUpFirstName").val("");
                    ////$("#ClientSignUpLastName").val("");
                    //$("#ClientSignUpEmail").val("");
                    //$("#ClientSignUpMobile").val("");
                    //$("#ClientSignUpPassword").val("");
                    //$("#ClientSignUpConfirmPassword").val("");
                    window.location.href = window.location.href;
                }
                else {
                    $("#SignUpSpan").removeClass();
                    $("#SignUpSpan").text(response.message);
                    $("#SignUpSpan").addClass('text-danger');
                    $("#SignUpSpan").css('display', 'block');
                }
            },
            error: function (response) {
                $("#SignUpSpan").removeClass();
                $("#SignUpSpan").text(response);
                $("#SignUpSpan").addClass('text-danger');
                $("#SignUpSpan").css('display', 'block');
            }

        });
    }
});

