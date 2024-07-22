$(function () {
    $('#AdminDesignations').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminDesignation/LoadDesignations",
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
            { "data": "reportingName", "name": "reportingName", "orderable": false },
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
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateDesignationModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteDesignationModal">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + ' </td>'
                }
            }
        ],

    });
    // On Click Create Modal Opened
    $("#AddDesignationModal").on('click', function () {
        var AddDesignationvalidator = $("#AddDesignation_Submit_Form").validate();
        AddDesignationvalidator.resetForm();
        ResetDesignationIds();
        AddBindDesignation();
        $("#AddDesignation").modal('show');
    });
    // On Click Update Modal Opened
    $("#AdminDesignations").on('click', '#UpdateDesignationModal', function () {
        var Id = $(this).val();
        var UpdateDesignationvalidator = $("#UpdateDesignation_Submit_Form").validate();
        UpdateDesignationvalidator.resetForm();
        ResetDesignationIds();
        EditBindDesignation();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminDesignation/GetDesignationById",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response != null) {
                        $("#UDesignationId").val(response.id);
                        $("#UDesignationName").val(response.name);
                        $("#UDesignationReportId").find('option[value="' + response.reportingTo + '"]').attr('selected', 'selected')
                        $("#UpdateDesignation").modal('show');
                    }
                    else {
                        toastr.error("Designation Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }

    });
    // On Click Delete Modal Opened
    $("#AdminDesignations").on('click', '#DeleteDesignationModal', function () {
        var Id = $(this).val();
        var DeleteDesignationvalidator = $("#DeleteDesignation_Submit_Form").validate();
        DeleteDesignationvalidator.resetForm();
        ResetDesignationIds();
        if (Id != "" && Id != null) {
            $("#DDesignationId").val(Id);
            $("#DeleteDesignation").modal('show');
        }
        else {
            toastr.error("Designation Not Exits", "Error");
        }

    });

    //Add Designation Form Submit
    $("#AddDesignation_Submit_Form").validate({
        rules: {
            ADesignationName: {
                required: true,
                letteronly: true
            },
            ADesignationReportId: {
                numberonly: true
            }
        },
        messages: {
            ADesignationName: {
                required: "Please provide a Designation Name."
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
            $("#AddDesignationShowLoader").show();
            $("#AddDesignationShowButtons").hide();
            var designation = new Designation();
            designation.Name = $("#ADesignationName").val();
            designation.ReportingTo = $("#ADesignationReportId").val();
            $.ajax({
                url: "/AdminDesignation/AddDesignation",
                type: "post",
                datatype: "json",
                data: { designation: designation },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                 
                    if (response == 1) {
                        $("#AddDesignation").modal("hide");
                        $("#AddDesignationShowLoader").hide();
                        $("#AddDesignationShowButtons").show();
                        toastr.success("Designation Saved Successfully", "Success");
                        $('#AdminDesignations').DataTable().ajax.reload();
                    }
                    else if (response == -2) {
                        $("#AddDesignationShowLoader").hide();
                        $("#AddDesignationShowButtons").show();
                        toastr.warning("Designation Already Exists", "Warning");
                    }
                    else if (response.error == "403")
                    {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#AddDesignationShowLoader").hide();
                        $("#AddDesignationShowButtons").show();
                        toastr.error("Designation Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddDesignationShowLoader").hide();
                    $("#AddDesignationShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //Update Designation Form Submit
    $("#UpdateDesignation_Submit_Form").validate({
        rules: {
            UDesignationId: {
                required: true
            },
            UDesignationName: {
                required: true,
                letteronly: true
            },
            UDesignationReportId: {
                numberonly: true
            }
        },
        messages: {
            UDesignationId: {
                required: "Please provide a valid Designation Passcode."
            },
            UDesignationName: {
                required: "Please provide a Country Name."
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
            $("#UpdateDesignationShowLoader").show();
            $("#UpdateDesignationShowButtons").hide();
            var designation = new Designation();
            designation.Id = $("#UDesignationId").val();
            designation.Name = $("#UDesignationName").val();
            designation.ReportingTo = $("#UDesignationReportId").val();
            $.ajax({
                url: "/AdminDesignation/UpdateDesignation",
                type: "post",
                datatype: "json",
                data: { designation: designation },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#UpdateDesignation").modal("hide");
                        $("#UpdateDesignationShowLoader").hide();
                        $("#UpdateDesignationShowButtons").show();
                        toastr.success("Designation Updated Successfully", "Success");
                        $('#AdminDesignations').DataTable().ajax.reload();

                    }
                    else if (response == -2) {
                        $("#UpdateDesignationShowLoader").hide();
                        $("#UpdateDesignationShowButtons").show();
                        toastr.warning("Designation Already Exists", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#UpdateDesignationShowLoader").hide();
                        $("#UpdateDesignationShowButtons").show();
                        toastr.error("Designation Not Updated", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateDesignationShowLoader").hide();
                    $("#UpdateDesignationShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //Delete Designation Form Submit
    $("#DeleteDesignation_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#DeleteDesignationShowLoader").show();
            $("#DeleteDesignationShowButtons").hide();
            $.ajax({
                url: "/AdminDesignation/DeleteDesignation",
                type: "post",
                datatype: "json",
                data: { Id: $("#DDesignationId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#DeleteDesignation").modal("hide");
                        $("#DeleteDesignationShowLoader").hide();
                        $("#DeleteDesignationShowButtons").show();
                        toastr.success("Designation Deleted Successfully", "Success");
                        $('#AdminDesignations').DataTable().ajax.reload();
                    }
                    else if (response == -2) {
                        $("#DeleteDesignationShowLoader").hide();
                        $("#DeleteDesignationShowButtons").show();
                        toastr.warning("Please Delete its Employees First Or child designation", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#DeleteDesignationShowLoader").hide();
                        $("#DeleteDesignationShowButtons").show();
                        toastr.error("Designation Not Deleted", "Error");
                    }
                },
                error: function (response) {
                    $("#DeleteDesignationShowLoader").hide();
                    $("#DeleteDesignationShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //Get All Designation For Binding With Add DropDown List
    function AddBindDesignation() {
        var BindDesignation = $("#ADesignationReportId");
        BindDesignation.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        $.ajax({
            url: "/AdminDesignation/GetAllDesignations",
            type: "get",
            datatype: "json",
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                BindDesignation.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response, function (i) {
                    BindDesignation.append($("<option></option>").val(this['id']).html(this['name']));
                });
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }

    //Get All Designation For Binding With Edit DropDown List
    function EditBindDesignation() {
        var BindDesignation = $("#UDesignationReportId");
        BindDesignation.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        $.ajax({
            url: "/AdminDesignation/GetAllDesignations",
            type: "get",
            datatype: "json",
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                BindDesignation.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response, function (i) {
                    BindDesignation.append($("<option></option>").val(this['id']).html(this['name']));
                });
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }

    function ResetDesignationIds() {
        $("#ADesignationName").val('');
        $("#ADesignationReportId").val('');

        $("#UDesignationId").val('');
        $("#UDesignationName").val('');
        $("#UDesignationReportId").val('');

        $("#DDesignationId").val('');
    }
});
