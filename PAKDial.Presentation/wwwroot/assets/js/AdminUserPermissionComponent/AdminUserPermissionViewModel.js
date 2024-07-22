$(function () {

    $("#ManageUserPermission_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#ManageUserPermissionShowLoader").show();
            $("#ManageUserPermissionShowButtons").hide();
            var userPermissions = [];
            $("input:checked").each(function () {
                var object = new UserBasedPermission();
                if ($(this).val() != "") {
                    object.UserId = $("#UserId").val();
                    object.RouteControlId = $(this).attr("id");
                }
                userPermissions.push(object);
                object = null;
            });
            if (userPermissions == null || userPermissions == "") {
                var object = new UserBasedPermission();
                object.UserId = $("#UserId").val();
                object.RouteControlId = 0;
                userPermissions.push(object);
                object = null;
            }
            $.ajax({
                url: "/AdminUserPermission/ManageUserPermission",
                type: "post",
                datatype: "json",
                data: { userBasedPermissions: userPermissions },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response) {
                        toastr.success("Permission Assigned Successfully", "Success");
                        window.location.href = "/AdminEmployee/Index";
                    } 
                    else {
                        $("#ManageUserPermissionShowLoader").hide();
                        $("#ManageUserPermissionShowButtons").show();
                        toastr.error("Permission Not Assigned Successfully", "Error");
                    }
                },
                error: function (response) {
                    $("#ManageUserPermissionShowLoader").hide();
                    $("#ManageUserPermissionShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
});

function RedirecttoUser() {
    window.location.href = "/AdminEmployee/Index";
}