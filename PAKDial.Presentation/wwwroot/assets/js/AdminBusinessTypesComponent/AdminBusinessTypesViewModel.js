$(function () {
    $('#AdminBusinessTypes').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminBusinessTypes/LoadBusinessTypes",
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
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateBusinessTypesModal">&nbsp;Edit&nbsp;</button>'
                   /// var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteBusinessTypesModal">Delete</button>'
                    return '<td> ' + Edit +' </td>'
                }
            }
        ],

    });


    // On Click Create Modal Opened
    $("#AddBusinessTypesModal").on('click', function () {
        var Addvalidator = $("#AddBusinessTypes_Submit_Form").validate();
        Addvalidator.resetForm();
        ResetBusinessTypesIds();
        $("#AddBusinessTypes").modal('show');
    });
    // On Click Update Modal Opened
    $("#AdminBusinessTypes").on('click', '#UpdateBusinessTypesModal', function () {
        var Id = $(this).val();
        var Updatevalidator = $("#UpdateBusinessTypes_Submit_Form").validate();
        Updatevalidator.resetForm();
        ResetBusinessTypesIds();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminBusinessTypes/GetBusinessTypesById",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response != null) {
                        $("#UBusinessTypesId").val(response.id);
                        $("#UBusinessTypesName").val(response.name);
                        $("#UpdateBusinessTypes").modal('show');
                    }
                    else {
                        toastr.error("Business Type Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }

    });
    // On Click Delete Modal Opened
    $("#AdminBusinessTypes").on('click', '#DeleteBusinessTypesModal', function () {
        var Id = $(this).val();
        var Deletevalidator = $("#DeleteBusinessTypes_Submit_Form").validate();
        Deletevalidator.resetForm();
        ResetBusinessTypesIds();
        if (Id != "" && Id != null) {
            $("#DBusinessTypesId").val(Id);
            $("#DeleteBusinessTypes").modal('show');
        }
        else {
            toastr.error("Business Type Not Exits", "Error");
        }

    });

    //Add Business Type Form Submit
    $("#AddBusinessTypes_Submit_Form").validate({
        rules: {
            ABusinessTypesName: {
                required: true,
            }
        },
        messages: {
            ABusinessTypesName: {
                required: "Please provide a Business Type Name."
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
            $("#AddBusinessTypesShowLoader").show();
            $("#AddBusinessTypesShowButtons").hide();
            var bs = new BusinessTypes();
            bs.Name = $("#ABusinessTypesName").val();
            $.ajax({
                url: "/AdminBusinessTypes/AddBusinessTypes",
                type: "post",
                datatype: "json",
                data: { instance: bs },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response == 1) {
                        $("#AddBusinessTypes").modal("hide");
                        $("#AddBusinessTypesShowLoader").hide();
                        $("#AddBusinessTypesShowButtons").show();
                        toastr.success("Business Type Saved Successfully", "Success");
                        $('#AdminBusinessTypes').DataTable().ajax.reload();
                    }
                    else if (response == -2) {
                        $("#AddBusinessTypesShowLoader").hide();
                        $("#AddBusinessTypesShowButtons").show();
                        toastr.warning("Business Type Already Exists", "Warning");
                    }
                    else {
                        $("#AddBusinessTypesShowLoader").hide();
                        $("#AddBusinessTypesShowButtons").show();
                        toastr.error("Business Type Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddBusinessTypesShowLoader").hide();
                    $("#AddBusinessTypesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //Update Business Type Form Submit
    $("#UpdateBusinessTypes_Submit_Form").validate({
        rules: {
            UBusinessTypesId: {
                required: true
            },
            UBusinessTypesName: {
                required: true,
            }
        },
        messages: {
            UBusinessTypesId: {
                required: "Please provide a valid Passcode."
            },
            UBusinessTypesName: {
                required: "Please provide a Zone Name."
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
            $("#UpdateBusinessTypesShowLoader").show();
            $("#UpdateBusinessTypesShowButtons").hide();
            var bs = new BusinessTypes();
            bs.Id = $("#UBusinessTypesId").val();
            bs.Name = $("#UBusinessTypesName").val();
            $.ajax({
                url: "/AdminBusinessTypes/UpdateBusinessTypes",
                type: "post",
                datatype: "json",
                data: { instance: bs },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response == 1) {
                        $("#UpdateBusinessTypes").modal("hide");
                        $("#UpdateBusinessTypesShowLoader").hide();
                        $("#UpdateBusinessTypesShowButtons").show();
                        toastr.success("Business Type Updated Successfully", "Success");
                        $('#AdminBusinessTypes').DataTable().ajax.reload();
                    }
                    else if (response == -2) {
                        $("#UpdateBusinessTypesShowLoader").hide();
                        $("#UpdateBusinessTypesShowButtons").show();
                        toastr.warning("Business Type Already Exists", "Warning");
                    }
                    else {
                        $("#UpdateBusinessTypesShowLoader").hide();
                        $("#UpdateBusinessTypesShowButtons").show();
                        toastr.error("Business Type Not Updated", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateBusinessTypesShowLoader").hide();
                    $("#UpdateBusinessTypesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //Delete Business Type Form Submit
    $("#DeleteBusinessTypes_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#DeleteBusinessTypesShowLoader").show();
            $("#DeleteBusinessTypesShowButtons").hide();
            $.ajax({
                url: "/AdminBusinessTypes/DeleteBusinessTypes",
                type: "post",
                datatype: "json",
                data: { Id: $("#DBusinessTypesId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response == 1) {
                        $("#DeleteBusinessTypes").modal("hide");
                        $("#DeleteBusinessTypesShowLoader").hide();
                        $("#DeleteBusinessTypesShowButtons").show();
                        toastr.success("Business Type Deleted Successfully", "Success");
                        $('#AdminBusinessTypes').DataTable().ajax.reload();
                    }
                    else if (response == -2) {
                        $("#DeleteBusinessTypesShowLoader").hide();
                        $("#DeleteBusinessTypesShowButtons").show();
                        toastr.warning("Please Delete its Listing Business", "Warning");
                    }
                    else {
                        $("#DeleteBusinessTypesShowLoader").hide();
                        $("#DeleteBusinessTypesShowButtons").show();
                        toastr.error("Business Type Not Deleted", "Error");
                    }
                },
                error: function (response) {
                    $("#DeleteBusinessTypesShowLoader").hide();
                    $("#DeleteBusinessTypesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    function ResetBusinessTypesIds() {
        $("#ABusinessTypesName").val('');
        $("#UBusinessTypeId").val('');
        $("#UBusinessTypesName").val('');
        $("#DBusinessTypesId").val('');
    }
});