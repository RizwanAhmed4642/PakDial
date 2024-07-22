$(function () {
    $('#AdminRoles').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminRoles/LoadRoles",
            type: "POST",
            datatype: "json",
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        },
        "columns": [
            {
                "data": "id",
                "name": "id",
                "orderable": true,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            //{ "data": "id", "name": "id", "orderable": true },
            { "data": "name", "name": "name", "orderable": false },
            {
                "data": "createdDate", "name": "createdDate", "orderable": false,
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
            {
                "data": "updatedDate", "name": "updatedDate", "orderable": false,
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },

        ],
        columnDefs: [
            { "width": "10%", "targets": 0 },
            { "width": "20%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "15%", "targets": 3 },
            {
                "width": "40%",
                targets: 4,
                render: function (data, type, full, meta) {
                    var Id = full.id;
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateRoleModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteRoleModal">Delete</button>'
                    var Permission = '<button type="button" class="btn mr-1 mb-1 btn-secondary btn-sm" value=' + Id + ' id="PermissionRoleModal" >Permission</button>'
                    return '<td> ' + Edit + " " + Delete + " " + Permission + ' </td>'
                }
            }
        ],

    });

    // On Click Create Modal Opened
    $("#AddRoleModal").on('click', function () {
        var AddRolevalidator = $("#AddRole_Submit_Form").validate();
        AddRolevalidator.resetForm();
        ResetRoleIds();
        $("#AddRole").modal('show');

    });

    // On Click Update Modal Opened
    $("#AdminRoles").on('click', '#UpdateRoleModal', function () {
        var Id = $(this).val();
        var UpdateRolevalidator = $("#UpdateRole_Submit_Form").validate();
        UpdateRolevalidator.resetForm();
        ResetRoleIds();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminRoles/GetRoleById",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response != null) {
                        $("#URoleId").val(response.id);
                        $("#URoleName").val(response.name);
                        $("#UpdateRole").modal('show');
                    }
                    else {
                        toastr.error("Role Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }

    });

    // On Click Delete Modal Opened
    $("#AdminRoles").on('click', '#DeleteRoleModal', function () {
        var Id = $(this).val();
        var DeleteRolevalidator = $("#DeleteRole_Submit_Form").validate();
        DeleteRolevalidator.resetForm();
        ResetRoleIds();
        if (Id != "" && Id != null) {
            $("#DRoleId").val(Id);
            $("#DeleteRole").modal('show');
        }
        else {
            toastr.error("Role Not Exits", "Error");
        }

    });

    //Add Role Form Submit
    $("#AddRole_Submit_Form").validate({
        rules: {
            ARoleName: {
                required: true,
                letteronly: true
            }
        },
        messages: {
            ARoleName: {
                required: "Please provide a Role Name."
            }
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
            $("#AddRoleShowLoader").show();
            $("#AddRoleShowButtons").hide();
            var role = new ApplicationRole();
            role.Name = $("#ARoleName").val();
            $.ajax({
                url: "/AdminRoles/AddRole",
                type: "post",
                datatype: "json",
                data: { role: role },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#AddRole").modal("hide");
                        $("#AddRoleShowLoader").hide();
                        $("#AddRoleShowButtons").show();
                        toastr.success("Role Saved Successfully", "Success");
                        $('#AdminRoles').DataTable().ajax.reload();
                    }
                    else if (response == 2) {
                        $("#AddRoleShowLoader").hide();
                        $("#AddRoleShowButtons").show();
                        toastr.warning("Role Already Exists", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#AddRoleShowLoader").hide();
                        $("#AddRoleShowButtons").show();
                        toastr.error("Role Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddRoleShowLoader").hide();
                    $("#AddRoleShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //Update Role Form Submit
    $("#UpdateRole_Submit_Form").validate({
        rules: {
            URoleId: {
                required: true
            },
            URoleName: {
                required: true,
                letteronly: true
            }
        },
        messages: {
            URoleId: {
                required: "Please provide a valid role Passcode."
            },
            URoleName: {
                required: "Please provide a role Name."
            }
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
            $("#UpdateRoleShowLoader").show();
            $("#UpdateRoleShowButtons").hide();
            var role = new ApplicationRole();
            role.Id = $("#URoleId").val();
            role.Name = $("#URoleName").val();
            $.ajax({
                url: "/AdminRoles/UpdateRole",
                type: "post",
                datatype: "json",
                data: { role: role },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#UpdateRole").modal("hide");
                        $("#UpdateRoleShowLoader").hide();
                        $("#UpdateRoleShowButtons").show();
                        toastr.success("Role Updated Successfully", "Success");
                        $('#AdminRoles').DataTable().ajax.reload();

                    }
                    else if (response == 2) {
                        $("#UpdateRoleShowLoader").hide();
                        $("#UpdateRoleShowButtons").show();
                        toastr.warning("Role Already Exists", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#UpdateRoleShowLoader").hide();
                        $("#UpdateRoleShowButtons").show();
                        toastr.error("Role Not Updated", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateRoleShowLoader").hide();
                    $("#UpdateRoleShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //Delete Role Form Submit
    $("#DeleteRole_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#DeleteRoleShowLoader").show();
            $("#DeleteRoleShowButtons").hide();
            $.ajax({
                url: "/AdminRoles/DeleteRole",
                type: "post",
                datatype: "json",
                data: { Id: $("#DRoleId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response == 1) {
                        $("#DeleteRole").modal("hide");
                        $("#DeleteRoleShowLoader").hide();
                        $("#DeleteRoleShowButtons").show();
                        toastr.success("Role Deleted Successfully", "Success");
                        $('#AdminRoles').DataTable().ajax.reload();
                    }
                    else if (response == 2) {
                        $("#DeleteRoleShowLoader").hide();
                        $("#DeleteRoleShowButtons").show();
                        toastr.warning("Please Delete its Users First", "Warning");
                    }
                    else {
                        $("#DeleteRoleShowLoader").hide();
                        $("#DeleteRoleShowButtons").show();
                        toastr.error("Role Not Deleted", "Error");
                    }
                },
                error: function (response) {
                    $("#DeleteRoleShowLoader").hide();
                    $("#DeleteRoleShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //Onclick Redirect to permission Controller
    $("#AdminRoles").on('click', '#PermissionRoleModal', function () {
        var Id = $(this).val();
        var url = "/AdminRolePermission/ManagePermission?Id=" + Id;
        window.location.href = url; 
    });
    function ResetRoleIds() {
        $("#ARoleName").val('');
        $("#URoleId").val('');
        $("#URoleName").val('');
        $("#DRoleId").val('');
    }
});
