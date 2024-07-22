function ForgotPasswordViewModel() {
    this.Email = '';
}

$(function () {
    $("#ClientForgetBtn").on("click", function () {
        var validator = $("#ClientforgotModal_Submit").validate();
        validator.resetForm();
        $("#ClientLoginEmail").val("");
        $("#ClientLoginPassword").val("");
        $("#ClientLoginremember").prop('checked', false);
        $("#LoginWrongpassword").css('display', 'none');
        $("#ClientLoginsModel").modal("hide");
        $("#ClientConfirmMsg").css('display', 'none');
        $("#ClientForgotEmail").val("");
        $("#ClientforgotModal").modal("show");
    });
});

$("#ClientforgotModal_Submit").validate({
    rules: {
        ClientForgotEmail: {
            required: true,
            emailRegex: true,
        },
    },
    messages: {
        ClientLoginOtpEmail: {
            required: "Email Required",
        },
        ClientLoginPassword: {
            required: "Password Required",
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
        var request = new ForgotPasswordViewModel();
        request.Email = $("#ClientForgotEmail").val();
        $.ajax({
            url: "/Home/ForgotPassword",
            type: "post",
            datatype: "json",
            data: { model: request },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == true) {
                    $("#ClientConfirmMsg").css('display', 'block');
                    $("#ClientForgotEmail").val("");
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }
});
