$(function () {
    $('#AdminTypeOfServices').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminTypeOfServices/LoadTypeOfService",
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
            { "data": "name", "name": "name", "orderable": false },
            {
                "data": "isActive", "name": "isActive", "orderable": false,
                "render": function (data) {
                    if (data == true) {
                        return '<div class="badge badge-primary">Active</div>';
                    }
                    else {
                        return '<div class="badge badge-danger">InActive</div>';
                    }
                }
            },
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
            { "width": "20%", "targets": 2 },
            { "width": "15%", "targets": 3 },
            { "width": "15%", "targets": 4 },
            {
                "width": "20%",
                targets: 5,
                render: function (data, type, full, meta) {
                    var Id = full.id;
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateTypeOfServicesModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteTypeOfServicesModal">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + ' </td>'
                }
            }
        ],

    });

    //-----------------------------------------------Add Type Of Services --------------------------------------------------

    // On Click Create Modal Opened
    $("#AddTypeOfServicesModal").on('click', function () {
        var AddTypeOfServicesvalidator = $("#AddTypeOfServices_Submit_Form").validate();
        AddTypeOfServicesvalidator.resetForm();
        ResetTypeOfServicesIds();
        $("#AddTypeOfServices").modal('show');

    });

    //Add Payment Mode Form Submit
    $("#AddTypeOfServices_Submit_Form").validate({
        rules: {
            ATypeOfServicesName: {
                required: true,
                letteronly: true
            },
            ATypeOfServicesDescrption: {
                required: true,
                letteronly: true
            },
        },
        messages: {
            ATypeOfServicesName: {
                required: "Please provide a Type Name."
            },
            ATypeOfServicesDescrption: {
                required: "Please provide a Type Description."
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
            $("#AddTypeOfServicesShowLoader").show();
            $("#AddTypeOfServicesShowButtons").hide();
            var ts = new TypeOfServices();
            ts.Name = $("#ATypeOfServicesName").val();
            ts.Description = $("#ATypeOfServicesDescrption").val();
            ts.IsActive = true;
            $.ajax({
                url: "/AdminTypeOfServices/AddTypeOfServices",
                type: "post",
                datatype: "json",
                data: { typeOfServices: ts },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response> 1) {
                        $("#AddTypeOfServices").modal("hide");
                        $("#AddTypeOfServicesShowLoader").hide();
                        $("#AddTypeOfServicesShowButtons").show();
                        toastr.success("Type Of Services Saved Successfully", "Success");
                        $('#AdminTypeOfServices').DataTable().ajax.reload();
                    }
                    else if (response == -2) {
                        $("#AddTypeOfServicesShowLoader").hide();
                        $("#AddTypeServicesShowButtons").show();
                        toastr.warning("Type Of Services Already Exists", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#AddTypeOfServicesShowLoader").hide();
                        $("#AddTypeOfServicesShowButtons").show();
                        toastr.error("Type Of Services Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddTypeOfServicesShowLoader").hide();
                    $("#AddTypeOfServicesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //-----------------------------------------------Update TypeOf Services --------------------------------------------------
    // On Click Update Modal Opened
    $("#AdminTypeOfServices").on('click', '#UpdateTypeOfServicesModal', function () {
        var Id = $(this).val();
        var UpdateTypeOfServicesvalidator = $("#UpdateTypeOfServices_Submit_Form").validate();
        UpdateTypeOfServicesvalidator.resetForm();
        ResetTypeOfServicesIds();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminTypeOfServices/GetTypeOfServicesById",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response != null) {
                        $("#UTypeOfServicesId").val(response.id);
                        $("#UTypeOfServicesName").val(response.name);
                        $("#UTypeOfServicesDescrption").val(response.description);
                        if (response.isActive == true) {
                            $("#UTypeOfServicesIsActive").prop('checked', true);
                        }
                        $("#UpdateTypeOfServices").modal('show');
                    }
                    else {
                        toastr.error("Type Of Services Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }

    });

    //Update Payment Mode Form Submit
    $("#UpdateTypeOfServices_Submit_Form").validate({
        rules: {
            UTypeOfServicesId: {
                required: true
            },
            UTypeOfServicesName: {
                required: true,
                letteronly: true
            },
            UTypeOfServicesDescrption: {
                required: true,
                letteronly: true
            }
        },
        messages: {
            UTypeOfServicesId: {
                required: "Please provide a valid  Services Passcode."
            },
            UTypeOfServicesName: {
                required: "Please provide a Services Name."
            },
            UTypeOfServicesDescrption: {
                required: "Please provide a Services Description."
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
            $("#UpdateTypeOfServicesShowLoader").show();
            $("#UpdateTypeOfServicesShowButtons").hide();
            var ts = new TypeOfServices();
            ts.Id = $("#UTypeOfServicesId").val();
            ts.Name = $("#UTypeOfServicesName").val();
            ts.Description = $("#UTypeOfServicesDescrption").val();
            if ($("#UTypeOfServicesIsActive").prop('checked') == true) {
                ts.IsActive = true;
            }
            $.ajax({
                url: "/AdminTypeOfServices/EditTypeOfService",
                type: "post",
                datatype: "json",
                data: { typeOfServices: ts },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response > 1) {
                        $("#UpdateTypeOfServices").modal("hide");
                        $("#UpdateTypeOfServicesShowLoader").hide();
                        $("#UpdateTypeOfServicesShowButtons").show();
                        toastr.success("TypeOf Services Updated Successfully", "Success");
                        $('#AdminTypeOfServices').DataTable().ajax.reload();

                    }
                    else if (response == -2) {
                        $("#UpdateTypeOfServicesShowLoader").hide();
                        $("#UpdateTypeOfServicesShowButtons").show();
                        toastr.warning("Type Of Services Already Exists", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#UpdateTypeOfServicesShowLoader").hide();
                        $("#UpdateTypeOfServicesShowButtons").show();
                        toastr.error("Type Of Services Not Updated", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateTypeOfServicesShowLoader").hide();
                    $("#UpdateTypeOfServicesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //------------------------------------------------Delete TypeOf Services -------------------------------------------------
    // On Click Delete Modal Opened
    $("#AdminTypeOfServices").on('click', '#DeleteTypeOfServicesModal', function () {
        var Id = $(this).val();
        var DeleteTypeOfServicesvalidator = $("#DeleteTypeOfServices_Submit_Form").validate();
        DeleteTypeOfServicesvalidator.resetForm();
        ResetTypeOfServicesIds();
        if (Id != "" && Id != null) {
            $("#DTypeOfServicesId").val(Id);
            $("#DeleteTypeOfServices").modal('show');
        }
        else {
            toastr.error("Services Not Exits", "Error");
        }

    });

    //Delete Module Form Submit
    $("#DeleteTypeOfServices_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#DeleteTypeOfServicesShowLoader").show();
            $("#DeleteTypeOfServicesShowButtons").hide();
            $.ajax({
                url: "/AdminTypeOfServices/DeleteTypeOfServices",
                type: "post",
                datatype: "json",
                data: { Id: $("#DTypeOfServicesId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#DeleteTypeOfServices").modal("hide");
                        $("#DeleteTypeOfServicesShowLoader").hide();
                        $("#DeleteTypeOfServicesShowButtons").show();
                        toastr.success("Type Of Services Deleted Successfully", "Success");
                        $('#AdminTypeOfServices').DataTable().ajax.reload();
                    }
                    else if (response == -3) {
                        $("#DeleteTypeOfServices").modal("hide");
                        $("#DeleteTypeOfServicesShowLoader").hide();
                        $("#DeleteTypeOfservicesShowButtons").show();
                        toastr.warning("Please Delete its Child Data", "Warning");
                    }
                  
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#DeleteTypeOfServicesShowLoader").hide();
                        $("#DeleteTypeOfServicesShowButtons").show();
                        toastr.error("Payment Mode Not Deleted", "Error");
                    }
                },
                error: function (response) {
                    $("#DeleteTypeOfServicesShowLoader").hide();
                    $("#DeleteTypeOfServicesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //------------------------------------------------Reset-----------------------------------------------------------------
    function ResetTypeOfServicesIds() {
        $("#ATypeOfServicesName").val('');
        $("#ATypeOfServicesDescrption").val('');

        $("#UTypeOfServicesId").val('');
        $("#UTypeOfServicesName").val('');
        $("#UTypeOfServicesDescrption").val('');

        $("#DTypeOfServicesId").val('');
    }

});