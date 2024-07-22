$(function () {
    $('#AdminRoutes').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminRouteControl/LoadRoutes",
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
            //<div class="badge badge-primary">Active</div>
            //<div class="badge badge-danger">InActive</div>
            
            { "data": "controller", "name": "controller", "orderable": false },
            { "data": "action", "name": "action", "orderable": false },
            //{ "data": "status", "name": "status", "orderable": false },
            {
                "data": "status", "name": "status", "orderable": false,
                "render": function (data) {
                    if (data == true) {
                        return '<div class="badge badge-primary">Active</div>';
                    }
                    else {
                        return '<div class="badge badge-danger">InActive</div>';
                    }
                }
            },
            { "data": "module", "name": "module", "orderable": false },
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
            { "width": "7%", "targets": 0 },
            { "width": "23%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "10%", "targets": 3 },
            { "width": "10%", "targets": 4 },
            { "width": "10%", "targets": 5 },
            {
                "width": "25%",
                targets: 6,
                render: function (data, type, full, meta) {
                    var Id = full.id;
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateRouteModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteRouteModal">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + ' </td>'
                }
            }
        ],

    });

     // On Click Create Modal Opened
    $("#AddRouteModal").on('click', function () {
        var AddRoutevalidator = $("#AddRoute_Submit_Form").validate();
        AddRoutevalidator.resetForm();
        ResetRouteIds();
        $("#ARouteStatus").prop('checked', true);
        $("#ARouteIsShown").prop('checked', true);
        AddBindModules();
        $("#AddRoute").modal('show');

    });
    // On Click Update Modal Opened
    $("#AdminRoutes").on('click', '#UpdateRouteModal', function () {
        var Id = $(this).val();
        var UpdateRoutevalidator = $("#UpdateRoute_Submit_Form").validate();
        UpdateRoutevalidator.resetForm();
        ResetRouteIds();
        UpdateBindModules();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminRouteControl/GetRouteById",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response != null) {
                        $("#URouteId").val(response.id);
                        $("#UModuleId").find('option[value="' + response.moduleId + '"]').attr('selected', 'selected') 
                        $("#URouteController").val(response.controller);
                        $("#URouteAction").val(response.action);
                        $("#URouteDescription").val(response.description);
                        $("#URouteMenuName").val(response.menuName);
                        if (response.status == true) {
                            $("#URouteStatus").prop('checked', true);
                        }
                        if (response.isShown == true) {
                            $("#URouteIsShown").prop('checked', true);
                        }
                        $("#UpdateRoute").modal('show');
                     }
                    else {
                        toastr.error("Route Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }

    });
    // On Click Delete Modal Opened
    $("#AdminRoutes").on('click', '#DeleteRouteModal', function () {
        var Id = $(this).val();
        var DeleteRoutevalidator = $("#DeleteRoute_Submit_Form").validate();
        DeleteRoutevalidator.resetForm();
        if (Id != "" && Id != null) {
            $("#DRouteId").val(Id);
            $("#DeleteRoute").modal('show');
        }
        else {
            toastr.error("Route Not Exits", "Error");
        }

    });

    //Add Route Form Submit
    $("#AddRoute_Submit_Form").validate({
        rules: {
            AModuleId: {
                required: true,
                numberonly: true,
            },
            ARouteMenuName: {
                required: true,
            },
            ARouteController: {
                required: true,
                letteronly: true
            },
            ARouteAction: {
                required: true,
                letteronly: true
            },
            ARouteDescription: {
                letteronly: true
            },
        },
        messages: {
            AModuleId: {
                required: "Please Select Module."
            },
            ARouteMenuName: {
                required: "Please provide a Menu Name."
            },
            ARouteController: {
                required: "Please provide a Controller Name."
            },
            ARouteAction: {
                required: "Please provide a Action Name."
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
            $("#AddRouteShowLoader").show();
            $("#AddRouteShowButtons").hide();
            var route = new RouteControls();
            route.ModuleId = $("#AModuleId").val();
            route.Controller = $("#ARouteController").val();
            route.Action = $("#ARouteAction").val();
            route.Description = $("#ARouteDescription").val();
            route.MenuName = $("#ARouteMenuName").val();
            if ($("#ARouteStatus").prop('checked') == true) {
                route.Status = true;
            }
            if ($("#ARouteIsShown").prop('checked') == true) {
                route.IsShown = true;
            }
            $.ajax({
                url: "/AdminRouteControl/AddRoute",
                type: "post",
                datatype: "json",
                data: { route: route },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#AddRoute").modal("hide");
                        $("#AddRouteShowLoader").hide();
                        $("#AddRouteShowButtons").show();
                        toastr.success("Route Saved Successfully", "Success");
                        $('#AdminRoutes').DataTable().ajax.reload();
                    }
                    else if (response == 2)
                    {
                        $("#AddRouteShowLoader").hide();
                        $("#AddRouteShowButtons").show();
                        toastr.error("Route Already Exits", "Error");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#AddRouteShowLoader").hide();
                        $("#AddRouteShowButtons").show();
                        toastr.error("Route Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddRouteShowLoader").hide();
                    $("#AddRouteShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //Update Route Form Submit
    $("#UpdateRoute_Submit_Form").validate({
        rules: {
            URouteId: {
                required: true,
                numberonly: true,
            },
            UModuleId: {
                required: true,
                numberonly: true,
            },
            URouteMenuName: {
                required: true,
            },
            URouteController: {
                required: true,
                letteronly: true
            },
            URouteAction: {
                required: true,
                letteronly: true
            },
            URouteDescription: {
                letteronly: true
            },
        },
        messages: {
            URouteId: {
                required: "Please provice route passcode."
            },
            UModuleId: {
                required: "Please select module."
            },
            URouteMenuName: {
                required: "Please provide a Menu Name."
            },
            URouteController: {
                required: "Please provide a Controller Name."
            },
            URouteAction: {
                required: "Please provide a Action Name."
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
            $("#UpdateRouteShowLoader").show();
            $("#UpdateRouteShowButtons").hide();
            var route = new RouteControls();
            route.Id = $("#URouteId").val();
            route.ModuleId = $("#UModuleId").val();
            route.Controller = $("#URouteController").val();
            route.Action = $("#URouteAction").val();
            route.Description = $("#URouteDescription").val();
            route.MenuName = $("#URouteMenuName").val();
            if ($("#URouteStatus").prop('checked') == true) {
                route.Status = true;
            }
            if ($("#URouteIsShown").prop('checked') == true) {
                route.IsShown = true;
            }
            $.ajax({
                url: "/AdminRouteControl/UpdateRoute",
                type: "post",
                datatype: "json",
                data: { route: route },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#UpdateRoute").modal("hide");
                        $("#UpdateRouteShowLoader").hide();
                        $("#UpdateRouteShowButtons").show();
                        toastr.success("Route Updated Successfully", "Success");
                        $('#AdminRoutes').DataTable().ajax.reload();

                    }
                    else if (response == 2) {
                        $("#UpdateRouteShowLoader").hide();
                        $("#UpdateRouteShowButtons").show();
                        toastr.warning("Route Already Exists", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#UpdateRouteShowLoader").hide();
                        $("#UpdateRouteShowButtons").show();
                        toastr.error("Route Not Updated", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateRouteShowLoader").hide();
                    $("#UpdateRouteShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //Delete Module Form Submit
    $("#DeleteRoute_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#DeleteRouteShowLoader").show();
            $("#DeleteRouteShowButtons").hide();
            $.ajax({
                url: "/AdminRouteControl/DeleteRoute",
                type: "post",
                datatype: "json",
                data: { Id: $("#DRouteId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#DeleteRoute").modal("hide");
                        $("#DeleteRouteShowLoader").hide();
                        $("#DeleteRouteShowButtons").show();
                        toastr.success("Route Deleted Successfully", "Success");
                        $('#AdminRoutes').DataTable().ajax.reload();
                    }
                    else if (response == 2) {
                        $("#DeleteRouteShowLoader").hide();
                        $("#DeleteRouteShowButtons").show();
                        toastr.warning("Please Delete its Permission First", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#DeleteRouteShowLoader").hide();
                        $("#DeleteRouteShowButtons").show();
                        toastr.error("Route Not Deleted", "Error");
                    }
                },
                error: function (response) {
                    $("#DeleteRouteShowLoader").hide();
                    $("#DeleteRouteShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //Get All Modules For Binding With Add DropDown List
    function AddBindModules() {
        var BindAddModules = $("#AModuleId");
        BindAddModules.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        $.ajax({
            url: "/AdminModule/GetAllModules",
            type: "get",
            datatype: "json",
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                BindAddModules.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response, function (i) {
                    BindAddModules.append($("<option></option>").val(this['id']).html(this['name']));
                });
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }
    //Get All Modules For Binding With Edit DropDown List
    function UpdateBindModules() {
        var UpdateBindModules = $("#UModuleId");
        UpdateBindModules.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        $.ajax({
            url: "/AdminModule/GetAllModules",
            type: "get",
            datatype: "json",
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                UpdateBindModules.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response, function (i) {
                    UpdateBindModules.append($("<option></option>").val(this['id']).html(this['name']));
                });
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }

    function ResetRouteIds() {
        $("#AModuleId").val('');
        $("#ARouteController").val('');
        $("#ARouteAction").val('');
        $("#ARouteDescription").val('');
        $("#ARouteStatus").val('');
        $("#ARouteIsShown").val('');
        $("#ARouteMenuName").val('');

        $("#URouteId").val('');
        $("#UModuleId").val('');
        $("#URouteController").val('');
        $("#URouteAction").val('');
        $("#URouteDescription").val('');
        $("#URouteStatus").prop('checked', false);
        $("#URouteIsShown").prop('checked', false);
        $("#URouteMenuName").val('');
        $("#DRouteId").val('');

    }
});
