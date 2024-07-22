$(function () {
    $('#AdminListingTypes').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminListingTypes/LoadListingTypes",
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
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateListingTypesModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteListingTypesModal">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + ' </td>'
                }
            }
        ],

    });

    //-------------------------------------------------------Add Listing Types ------------------------------------------------------------

    //On Click Open Model
    $("#AddListingTypesModal").on('click', function () {
        
        var AddListingTypesValidator = $("#AddListingTypes_Submit_Form").validate();
        AddListingTypesValidator.resetForm();
        ResetListingTypesIds();
        $("#AddListingTypes").modal('show');
    });

    //Onclcik Button Add Listing Types
    $("#AddListingTypes_Submit_Form").validate({
        rules: {
            AListingTypesName: {
                required: true,
                    letteronly: true
            },
        },
        messages: {
            APaymentsModesName: {
                required: "Please provide a Listing Types Name."
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
            $("#AddListingTypesShowLoader").show();
            $("#AddListingTypesShowButtons").hide();
            var LT = new ListingTypes();
            LT.Name = $("#AListingTypesName").val();
            LT.IsActive = true;
            $.ajax({
                url: "/AdminListingTypes/AddListingTypes",
                type: "post",
                datatype: "json",
                data: { listingTypes: LT },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#AddListingTypes").modal("hide");
                        $("#AddListingTypesShowLoader").hide();
                        $("#AddListingTypesShowButtons").show();
                        toastr.success("Listing Types Saved Successfully", "Success");
                        $('#AdminListingTypes').DataTable().ajax.reload();
                    }
                    else if (response == -3) {
                        $("#AddListingTypes").modal("hide");
                        $("#AddListingTypesShowLoader").hide();
                        $("#AddListingTypesShowButtons").show();
                        toastr.warning("Listing Types Already Exists", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#AddListingTypesShowLoader").hide();
                        $("#AddListingTypesShowButtons").show();
                        toastr.error("Listing Types Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddListingTypesShowLoader").hide();
                    $("#AddListingTypesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //-----------------------------------------------Update Listing Types --------------------------------------------------
    // On Click Update Modal Opened
    $("#AdminListingTypes").on('click', '#UpdateListingTypesModal', function () {
       
        var Id = $(this).val();
        var UpdateListingTypesvalidator = $("#UpdateListingTypes_Submit_Form").validate();
        UpdateListingTypesvalidator.resetForm();
        ResetListingTypesIds();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminListingTypes/GetListingTypesById",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response != null) {
                      
                        $("#UListingTypesId").val(response.id);
                        $("#UListingTypesName").val(response.name);
                        if (response.isActive == true) {
                            $("#UListingTypesIsActive").prop('checked', true);
                        }
                        $("#UpdateListingTypes").modal('show');
                    }
                    else {
                        toastr.error("Listing Types Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }

    });

    //Update Payment Mode Form Submit
    $("#UpdateListingTypes_Submit_Form").validate({
        rules: {
            UListingTypesId: {
                required: true
            },
            UListingTypesName: {
                required: true,
                letteronly: true
            },
         
        },
        messages: {
            UListingTypesId: {
                required: "Please provide a valid Listing Types Passcode."
            },
            UListingTypesName: {
                required: "Please provide a Types Name."
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
            $("#UpdateListingTypesShowLoader").show();
            $("#UpdateListingTypesShowButtons").hide();
            var LT = new ListingTypes();
            LT.Id = $("#UListingTypesId").val();
            LT.Name = $("#UListingTypesName").val();
             if ($("#UPaymentsModesIsActive").prop('checked') == true) {
                 LT.IsActive = true;
            }
            $.ajax({
                url: "/AdminListingTypes/EditListingTypes",
                type: "post",
                datatype: "json",
                data: { listingTypes: LT },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#UpdateListingTypes").modal("hide");
                        $("#UpdateListingTypesShowLoader").hide();
                        $("#UpdateListingTypesShowButtons").show();
                        toastr.success("Listing Types Updated Successfully", "Success");
                        $('#AdminListingTypes').DataTable().ajax.reload();

                    }
                    else if (response == -2) {
                        $("#UpdateListingTypesShowLoader").hide();
                        $("#UpdateListingTypesShowButtons").show();
                        toastr.warning("Payment Mode Already Exists", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#UpdateListingTypesShowLoader").hide();
                        $("#UpdateListingTypesShowButtons").show();
                        toastr.error("Listing Types Not Updated", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateListingTypesShowLoader").hide();
                    $("#UpdateListingTypesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //------------------------------------------------Delete Listing Types -------------------------------------------------
    // On Click Delete Modal Opened
    $("#AdminListingTypes").on('click', '#DeleteListingTypesModal', function () {
        var Id = $(this).val();
        var DeleteListingTypesvalidator = $("#DeleteListingTypes_Submit_Form").validate();
        DeleteListingTypesvalidator.resetForm();
        ResetListingTypesIds();
        if (Id != "" && Id != null) {
            $("#DListingTypesId").val(Id);
            $("#DeleteListingTypes").modal('show');
        }
        else {
            toastr.error("Listing Types Not Exits", "Error");
        }

    });

    //Delete Module Form Submit
    $("#DeleteListingTypes_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#DeleteListingTypesShowLoader").show();
            $("#DeleteListingTypesShowButtons").hide();
            $.ajax({
                url: "/AdminListingTypes/DeleteListingTypes",
                type: "post",
                datatype: "json",
                data: { Id: $("#DListingTypesId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#DeleteListingTypes").modal("hide");
                        $("#DeleteListingTypesShowLoader").hide();
                        $("#DeleteListingTypesShowButtons").show();
                        toastr.success("Listing Types Deleted Successfully", "Success");
                        $('#AdminListingTypes').DataTable().ajax.reload();
                    }
                    else if (response == -3) {
                        $("#DeleteListingTypesShowLoader").hide();
                        $("#DeleteListingTypesShowButtons").show();
                        toastr.warning("Please Delete its Child Data", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#DeleteListingTypesShowLoader").hide();
                        $("#DeleteListingTypesShowButtons").show();
                        toastr.error("Listing Type Not Deleted", "Error");
                    }
                },
                error: function (response) {
                    $("#DeleteListingTypesShowLoader").hide();
                    $("#DeleteListingTypesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //-----------------------------------------------------------Reset From----------------------------------------------------------------

    function ResetListingTypesIds() {
        $("#AListingTypesName").val('');
      

        $("#UListingTypesId").val('');
        $("#UAListingTypesName").val('');
        $("#DListingTypesId").val('');
    }

});