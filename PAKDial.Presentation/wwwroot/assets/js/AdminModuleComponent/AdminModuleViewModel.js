$(function () {
    $('#AdminModules').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminModule/LoadModules",
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
            { "width": "25%", "targets": 1 },
            { "width": "20%", "targets": 2 },
            { "width": "20%", "targets": 3 },
            {
                "width": "20%",
                targets: 4,
                render: function (data, type, full, meta) {
                    var Id = full.id;
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateModuleModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteModuleModal">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + ' </td>'
                }
            }
        ],

    });

    // On Click Create Modal Opened
    $("#AddModuleModal").on('click', function () {
        var AddModulevalidator = $("#AddModule_Submit_Form").validate();
        AddModulevalidator.resetForm();
        ResetModuleIds();
        $("#AddModule").modal('show');

    });

    // On Click Update Modal Opened
    $("#AdminModules").on('click', '#UpdateModuleModal', function () {
        var Id = $(this).val();
        var UpdateModulevalidator = $("#UpdateModule_Submit_Form").validate();
        UpdateModulevalidator.resetForm();
        ResetModuleIds();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminModule/GetModuleById",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response != null) {
                        $("#UModuleId").val(response.id);
                        $("#UModuleName").val(response.name);
                        $("#UModuleClassName").val(response.className);
                        $("#UpdateModule").modal('show');
                    }
                    else {
                        toastr.error("Module Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }

    });
    // On Click Delete Modal Opened
    $("#AdminModules").on('click', '#DeleteModuleModal', function () {
        var Id = $(this).val();
        var DeleteModulevalidator = $("#DeleteModule_Submit_Form").validate();
        DeleteModulevalidator.resetForm();
        ResetModuleIds();
        if (Id != "" && Id != null) {
            $("#DModuleId").val(Id);
            $("#DeleteModule").modal('show');
        }
        else {
            toastr.error("Module Not Exits", "Error");
        }

    });

    //Add Module Form Submit
    $("#AddModule_Submit_Form").validate({
        rules: {
            AModuleName: {
                required: true,
                letteronly: true
            },
        },
        messages: {
            AModuleName: {
                required: "Please provide a Module Name."
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
            $("#AddModuleShowLoader").show();
            $("#AddModuleShowButtons").hide();
            var module = new Modules();
            module.Name = $("#AModuleName").val();
            module.ClassName = $("#AModuleClassName").val();
            $.ajax({
                url: "/AdminModule/AddModule",
                type: "post",
                datatype: "json",
                data: { module: module },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#AddModule").modal("hide");
                        $("#AddModuleShowLoader").hide();
                        $("#AddModuleShowButtons").show();
                        toastr.success("Module Saved Successfully", "Success");
                        $('#AdminModules').DataTable().ajax.reload();
                    }
                    else if (response == 2) {
                        $("#AddModuleShowLoader").hide();
                        $("#AddModuleShowButtons").show();
                        toastr.warning("Module Already Exists", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#AddModuleShowLoader").hide();
                        $("#AddModuleShowButtons").show();
                        toastr.error("Module Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddModuleShowLoader").hide();
                    $("#AddModuleShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //Update Module Form Submit
    $("#UpdateModule_Submit_Form").validate({
        rules: {
            UModuleId: {
                required: true
            },
            UModuleName: {
                required: true,
                letteronly: true
            }
        },
        messages: {
            UModuleId: {
                required: "Please provide a valid Module Passcode."
            },
            UModuleName: {
                required: "Please provide a Module Name."
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
            $("#UpdateModuleShowLoader").show();
            $("#UpdateModuleShowButtons").hide();
            var module = new Modules();
            module.Id = $("#UModuleId").val();
            module.Name = $("#UModuleName").val();
            module.ClassName = $("#UModuleClassName").val();
            $.ajax({
                url: "/AdminModule/UpdateModule",
                type: "post",
                datatype: "json",
                data: { module: module },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#UpdateModule").modal("hide");
                        $("#UpdateModuleShowLoader").hide();
                        $("#UpdateModuleShowButtons").show();
                        toastr.success("Module Updated Successfully", "Success");
                        $('#AdminModules').DataTable().ajax.reload();

                    }
                    else if (response == 2) {
                        $("#UpdateModuleShowLoader").hide();
                        $("#UpdateModuleShowButtons").show();
                        toastr.warning("Module Already Exists", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#UpdateModuleShowLoader").hide();
                        $("#UpdateModuleShowButtons").show();
                        toastr.error("Module Not Updated", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateModuleShowLoader").hide();
                    $("#UpdateModuleShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //Delete Module Form Submit
    $("#DeleteModule_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#DeleteModuleShowLoader").show();
            $("#DeleteModuleShowButtons").hide();
            $.ajax({
                url: "/AdminModule/DeleteModule",
                type: "post",
                datatype: "json",
                data: { Id: $("#DModuleId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#DeleteModule").modal("hide");
                        $("#DeleteModuleShowLoader").hide();
                        $("#DeleteModuleShowButtons").show();
                        toastr.success("Module Deleted Successfully", "Success");
                        $('#AdminModules').DataTable().ajax.reload();
                    }
                    else if (response == 2) {
                        $("#DeleteModuleShowLoader").hide();
                        $("#DeleteModuleShowButtons").show();
                        toastr.warning("Please Delete its Users First", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#DeleteModuleShowLoader").hide();
                        $("#DeleteModuleShowButtons").show();
                        toastr.error("Module Not Deleted", "Error");
                    }
                },
                error: function (response) {
                    $("#DeleteModuleShowLoader").hide();
                    $("#DeleteModuleShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });


    function ResetModuleIds() {
        $("#AModuleName").val('');
        $("#AModuleClassName").val('')
        $("#UModuleId").val('');
        $("#UModuleName").val('');
        $("#DModuleId").val('');
        $("#UModuleClassName").val('')
    }
});
