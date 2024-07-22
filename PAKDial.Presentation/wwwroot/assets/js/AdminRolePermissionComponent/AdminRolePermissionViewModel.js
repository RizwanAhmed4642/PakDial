$(function () {

    $("#ManageRolePermission_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#ManageRolePermissionShowLoader").show();
            $("#ManageRolePermissionShowButtons").hide();
            var rolePermissions = [];
            $("input:checked").each(function () {
                var object = new RoleBasedPermission();
                if ($(this).val() != "") {
                    object.RoleId = $("#RoleId").val();
                    object.RouteControlId = $(this).attr("id");
                }
                rolePermissions.push(object);
                object = null;
            });
            if (rolePermissions == null || rolePermissions == "") {
                var object = new RoleBasedPermission();
                object.RoleId = $("#RoleId").val();
                object.RouteControlId = 0;
                rolePermissions.push(object);
                object = null;
            }
            $.ajax({
                url: "/AdminRolePermission/ManageRole",
                type: "post",
                datatype: "json",
                data: { rolePermissions: rolePermissions },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response) {
                        toastr.success("Permission Assigned Successfully", "Success");
                        window.location.href = "/AdminRoles/Index";
                    }
                    else {
                        $("#ManageRolePermissionShowLoader").hide();
                        $("#ManageRolePermissionShowButtons").show();
                        toastr.error("Permission Not Assigned Successfully", "Error");
                    }
                },
                error: function (response) {
                    $("#ManageRolePermissionShowLoader").hide();
                    $("#ManageRolePermissionShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });


});
function RedirecttoRole() {
    window.location.href = "/AdminRoles/Index";
}
function checkLength(clsName) {
    var checked = $("." + clsName + ":checked").length;
    var Notchecked = $("." + clsName + ":not(:checked)").length;
    $("." + clsName + ":not(:checked)").prop("checked", true);
    var all = $("." + clsName).length;
    if (all == checked) {
        $("input[name=" + clsName + "]").prop("checked", true)
    }
    if (Notchecked == 0) {
        $("input[name=" + clsName + "]").prop("checked", false)
        $("." + clsName + ":checked").prop("checked", false);
    }
}