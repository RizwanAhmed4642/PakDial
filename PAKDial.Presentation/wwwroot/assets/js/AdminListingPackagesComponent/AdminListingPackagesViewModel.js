$(function () {
    $('#AdminListingPackages').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminListingPackages/LoadListingPackages",
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
            { "data": "months", "name": "months", "orderable": false },
            { "data": "price", "name": "price", "orderable": false },
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
            { "width": "15%", "targets": 1 },
            { "width": "5%", "targets": 2 },
            { "width": "15%", "targets": 3 },
            { "width": "10%", "targets": 4 },
            { "width": "10%", "targets": 5 },
            { "width": "10%", "targets": 6 },
            {
                "width": "25%",
                targets: 7,
                render: function (data, type, full, meta) {
                    var Id = full.id;
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateListingPackagesModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteListingPackagesModal">Delete</button>'
                    var Prices = '<button type="button" class="btn mr-1 mb-1 btn-secondary btn-sm" value=' + Id + ' id="PackagesPriceshref">Prices</button>'
                    return '<td> ' + Edit + " " + Delete + " " + Prices +' </td>'
                }
            }
        ],

    });

    //-----------------------------------------------Add Listing Packages --------------------------------------------------

    // On Click Create Modal Opened
    $("#AddListingPackagesModal").on('click', function () {
        var AddListingPackagesvalidator = $("#AddListingPackages_Submit_Form").validate();
        AddListingPackagesvalidator.resetForm();
        ResetListingPackagesIds();
        $("#AddListingPackages").modal('show');
    });

    //Add Listing Package Form Submit
    $("#AddListingPackages_Submit_Form").validate({
        rules: {
            AListingPackagesName: {
                required: true,
                letteronly: true
            },
            AListingPackagesDescrption: {
                required: true,
                letteronly: true
            },
            AListingPackagesMonths: {
                required: true,
                numberonly: true
            },
            AListingPackagesPrice: {
                required: true,
                numberonly: true
            },
        },
        messages: {
            APaymentsModesName: {
                required: "Please provide a Package Name."
            },
            APaymentsModesDescrption: {
                required: "Please provide a Package Description."
            },
            AListingPackagesMonths: {
                required: "Please provide a Months."
            },
            AListingPackagesPrice: {
                required: "Please provide a Package Price."
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
            $("#AddListingPackagesShowLoader").show();
            $("#AddListingPackagesShowButtons").hide();
            var Lp = new ListingPackageViewModel();
            Lp.Name = $("#AListingPackagesName").val();
            Lp.Description = $("#AListingPackagesDescrption").val();
            Lp.Months = $("#AListingPackagesMonths").val();
            Lp.NewPrice = $("#AListingPackagesPrice").val();
            Lp.IsActive = true;
            $.ajax({
                url: "/AdminListingPackages/AddListingPackages",
                type: "post",
                datatype: "json",
                data: { instance: Lp },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 2) {
                        $("#AddListingPackages").modal("hide");
                        $("#AddListingPackagesShowLoader").hide();
                        $("#AddListingPackagesShowButtons").show();
                        toastr.success("Listing Packages Saved Successfully", "Success");
                        $('#AdminListingPackages').DataTable().ajax.reload();
                    }
                    else if (response == -2) {
                        $("#AddListingPackagesShowLoader").hide();
                        $("#AddListingPackagesShowButtons").show();
                        toastr.warning("Listing Packages Already Exists", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#AddListingPackagesShowLoader").hide();
                        $("#AddListingPackagesShowButtons").show();
                        toastr.error("Listing Packages Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddListingPackagesShowLoader").hide();
                    $("#AddListingPackagesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //-----------------------------------------------Update Listing Packages------------------------------------------------
    // On Click Update Modal Opened
    $("#AdminListingPackages").on('click', '#UpdateListingPackagesModal', function () {
        var Id = $(this).val();
        var UpdateListingPackagesvalidator = $("#UpdateListingPackages_Submit_Form").validate();
        UpdateListingPackagesvalidator.resetForm();
        ResetListingPackagesIds();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminListingPackages/GetListingPackageById",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response != null) {
                        $("#UListingPackagesId").val(response.id);
                        $("#UPackagesPriceId").val(response.priceId);
                        $("#UListingPackagesName").val(response.name);
                        $("#UListingPackagesDescrption").val(response.description);
                        $("#UListingPackagesMonths").val(response.months);
                        $("#UListingPackagesPrice").val(response.activePrice);

                        if (response.isActive == true) {
                            $("#UListingPackagesIsActive").prop('checked', true);
                        }
                        $("#UpdateListingPackages").modal('show');
                    }
                    else {
                        toastr.error("Listing Packages Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }

    });

    //Add Listing Package Form Submit
    $("#UpdateListingPackages_Submit_Form").validate({
        rules: {
            UListingPackagesId: {
                required: true,
                numberonly: true
            },
            UPackagesPriceId: {
                required: true,
                numberonly: true
            },
            UListingPackagesName: {
                required: true,
                letteronly: true
            },
            UListingPackagesDescrption: {
                required: true,
                letteronly: true
            },
            UListingPackagesMonths: {
                required: true,
                numberonly: true
            },
            UListingPackagesNewPrice: {
                numberonly: true
            },
        },
        messages: {
            UListingPackagesId: {
                required: "Please provide a Passcode."
            },
            UPackagesPriceId: {
                required: "Please provide a Passcode."
            },
            UListingPackagesName: {
                required: "Please provide a Package Name."
            },
            UListingPackagesMonths: {
                required: "Please provide a Months."
            },
            UListingPackagesDescrption: {
                required: "Please provide a Package Description."
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
            $("#UpdateListingPackagesShowLoader").show();
            $("#UpdateListingPackagesShowButtons").hide();
            var Lp = new ListingPackageViewModel();
            Lp.Id = $("#UListingPackagesId").val();
            Lp.Name = $("#UListingPackagesName").val();
            Lp.Description = $("#UListingPackagesDescrption").val();
            Lp.Months = $("#UListingPackagesMonths").val();
            Lp.ActivePrice = $("#UListingPackagesPrice").val();
            Lp.PriceId = $("#UPackagesPriceId").val();
            Lp.NewPrice = $("#UListingPackagesNewPrice").val();
            if ($("#UListingPackagesIsActive").prop('checked') == true) {
                Lp.IsActive = true;
            }
            $.ajax({
                url: "/AdminListingPackages/UpdateListingPackages",
                type: "post",
                datatype: "json",
                data: { instance: Lp },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#UpdateListingPackages").modal("hide");
                        $("#UpdateListingPackagesShowLoader").hide();
                        $("#UpdateListingPackagesShowButtons").show();
                        toastr.success("Listing Packages Updated Successfully", "Success");
                        $('#AdminListingPackages').DataTable().ajax.reload();
                    }
                    else if (response == -2) {
                        $("#UpdateListingPackagesShowLoader").hide();
                        $("#UpdateListingPackagesShowButtons").show();
                        toastr.warning("Listing Packages Already Exists", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#UpdateListingPackagesShowLoader").hide();
                        $("#UpdateListingPackagesShowButtons").show();
                        toastr.error("Listing Packages Not Updated", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateListingPackagesShowLoader").hide();
                    $("#UpdateListingPackagesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //-----------------------------------------------Delete Listing Package--------------------------------------------------
    // On Click Delete Modal Opened
    $("#AdminListingPackages").on('click', '#DeleteListingPackagesModal', function () {
        var Id = $(this).val();
        var DeleteListingPackagesvalidator = $("#DeleteListingPackages_Submit_Form").validate();
        DeleteListingPackagesvalidator.resetForm();
        ResetListingPackagesIds();
        if (Id != "" && Id != null) {
            $("#DListingPackagesId").val(Id);
            $("#DeleteListingPackages").modal('show');
        }
        else {
            toastr.error("Packages Not Exits", "Error");
        }

    });

    //Delete Module Form Submit
    $("#DeleteListingPackages_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#DeletePaymentsModesShowLoader").show();
            $("#DeleteListingPackagesShowButtons").hide();
            $.ajax({
                url: "/AdminListingPackages/DeleteListingPackages",
                type: "post",
                datatype: "json",
                data: { Id: $("#DListingPackagesId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#DeleteListingPackages").modal("hide");
                        $("#DeleteListingPackagesShowLoader").hide();
                        $("#DeleteListingPackagesShowButtons").show();
                        toastr.success("Listing Package Deleted Successfully", "Success");
                        $('#AdminListingPackages').DataTable().ajax.reload();
                    }
                    else if (response == -2) {
                        $("#DeleteListingPackagesShowLoader").hide();
                        $("#DeleteListingPackagesShowButtons").show();
                        toastr.warning("Please Delete its Child Data", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#DeleteListingPackagesShowLoader").hide();
                        $("#DeleteListingPackagesShowButtons").show();
                        toastr.error("Listing Package Not Deleted", "Error");
                    }
                },
                error: function (response) {
                    $("#DeleteListingPackagesShowLoader").hide();
                    $("#DeleteListingPackagesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //------------------------------------------------Redirect Package Price ------------------------------------------------

    $("#AdminListingPackages").on('click', '#PackagesPriceshref', function () {
        var Id = $(this).val();
        window.location.href = "/AdminListingPackages/PackagesPricesList?Id=" + Id;
    });

    //------------------------------------------------Reset------------------------------------------------------------------
    function ResetListingPackagesIds() {
        $("#AListingPackagesName").val('');
        $("#AListingPackagesDescrption").val('');
        $("#AListingPackagesPrice").val('');
        $("#AListingPackagesMonths").val('');

        $("#UListingPackagesId").val('');
        $("#UPackagesPriceId").val('');
        $("#UListingPackagesName").val('');
        $("#UListingPackagesDescrption").val('');
        $("#UListingPackagesPrice").val('');
        $("#UListingPackagesNewPrice").val('');
        $("#UListingPackagesMonths").val('');

        $("#DListingPackagesId").val('');
    }
});