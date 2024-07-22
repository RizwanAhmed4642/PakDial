$(function () {

    $('#AdminCountries').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminCountry/LoadCountries",
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
            { "data": "countryCode", "name": "countryCode", "orderable": false },
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
            { "width": "15%", "targets": 1 },
            { "width": "20%", "targets": 2 },
            { "width": "15%", "targets": 3 },
            { "width": "15%", "targets": 4 },
            {
                "width": "20%",
                targets: 5,
                render: function (data, type, full, meta) {
                    var Id = full.id;
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateCountryModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteCountryModal">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + ' </td>'
                }
            }
        ],

    });

    // On Click Create Modal Opened
    $("#AddCountryModal").on('click', function () {
        var AddCountryvalidator = $("#AddCountry_Submit_Form").validate();
        AddCountryvalidator.resetForm();
        ResetCountryIds();
        $("#AddCountry").modal('show');
       
    });

    // On Click Update Modal Opened
    $("#AdminCountries").on('click','#UpdateCountryModal', function () {
        var Id = $(this).val();
        var UpdateCountryvalidator = $("#UpdateCountry_Submit_Form").validate();
        UpdateCountryvalidator.resetForm();
        ResetCountryIds();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminCountry/GetCountryById",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response != null) {
                        $("#UCountryId").val(response.id);
                        $("#UCountryCode").val(response.countryCode);
                        $("#UCountryName").val(response.name);
                        $("#ULatitude").val(response.latitude);
                        $("#ULongitude").val(response.longitude);
                        $("#UpdateCountry").modal('show');
                    }
                    else {
                        toastr.error("Country Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }

    });

    // On Click Delete Modal Opened
    $("#AdminCountries").on('click', '#DeleteCountryModal', function () {
        var Id = $(this).val();
        var DeleteCountryvalidator = $("#DeleteCountry_Submit_Form").validate();
        DeleteCountryvalidator.resetForm();
        ResetCountryIds();
        if (Id != "" && Id != null) {
            $("#DCountryId").val(Id);
            $("#DeleteCountry").modal('show');
        }
        else {
            toastr.error("Country Not Exits", "Error");
        }

    });

    //Check CountryCode On Focus Out
    $("#ACountryCode").focusout(function () {
        var countryCode = $("#ACountryCode").val();
        if (countryCode != "") {
            $.ajax({
                url: "/AdminCountry/CheckCountryCodeExit",
                type: "get",
                datatype: "json",
                data: { countryCode: countryCode },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == null) {
                    }
                    else {
                        toastr.error(response.countryCode + " " + "Code Already Exits");
                        $("#ACountryCode").val("");
                        $("#ACountryCode").focus();
                    }
                },
                error: function (response) {
                    toastr.error(response);
                }

            });
        }
    });

    ///////////////////////////Add Country Form Submit//////////////////////////////////////////////
    $("#AddCountry_Submit_Form").validate({
        rules: {
            ACountryCode: {
                required: true
            },
            ACountryName: {
                required: true,
                letteronly: true
            }
        },
        messages: {
            ACountryCode: {
                required: "Please provide a valid Country Code."
            },
            ACountryName: {
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
            $("#AddCountryShowLoader").show();
            $("#AddCountryShowButtons").hide();
            var country = new Country();
            country.CountryCode = $("#ACountryCode").val();
            country.Name = $("#ACountryName").val();
            country.Latitude = $("#ALatitude").val();
            country.Longitude = $("#ALongitude").val();
            $.ajax({
                url: "/AdminCountry/AddCountries",
                type: "post",
                datatype: "json",
                data: { country: country },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#AddCountry").modal("hide");
                        $("#AddCountryShowLoader").hide();
                        $("#AddCountryShowButtons").show();
                        toastr.success("Country Saved Successfully", "Success");
                        $('#AdminCountries').DataTable().ajax.reload();
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#AddCountryShowLoader").hide();
                        $("#AddCountryShowButtons").show();
                        toastr.error("Country Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddCountryShowLoader").hide();
                    $("#AddCountryShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    /////////////////////////Update Country Form Submit/////////////////////////////////////////////
    $("#UpdateCountry_Submit_Form").validate({
        rules: {
            UCountryId: {
                required: true
            },
            UCountryCode: {
                required: true
            },
            UCountryName: {
                required: true,
                letteronly: true
            }
        },
        messages: {
            UCountryId: {
                required: "Please provide a valid Country Passcode."
            },
            UCountryCode: {
                required: "Please provide a valid Country Code."
            },
            UCountryName: {
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
            $("#UpdateCountryShowLoader").show();
            $("#UpdateCountryShowButtons").hide();
            var country = new Country();
            country.Id = $("#UCountryId").val();
            country.CountryCode = $("#UCountryCode").val();
            country.Name = $("#UCountryName").val();
            country.Latitude = $("#ULatitude").val();
            country.Longitude = $("#ULongitude").val();
            $.ajax({
                url: "/AdminCountry/UpdateCountry",
                type: "post",
                datatype: "json",
                data: { country: country },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#UpdateCountry").modal("hide");
                        $("#UpdateCountryShowLoader").hide();
                        $("#UpdateCountryShowButtons").show();
                        toastr.success("Country Updated Successfully", "Success");
                        $('#AdminCountries').DataTable().ajax.reload();

                    }
                    else if (response == 2)
                    {
                        $("#UpdateCountryShowLoader").hide();
                        $("#UpdateCountryShowButtons").show();
                        toastr.warning("Country Code Already Exists", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#UpdateCountryShowLoader").hide();
                        $("#UpdateCountryShowButtons").show();
                        toastr.error("Country Not Updated", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateCountryShowLoader").hide();
                    $("#UpdateCountryShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //////////////////////////////// Deletion///////////////////////////////////////////////////////
    //Delete Country Form Submit
    $("#DeleteCountry_Submit_Form").validate({
        rules: {
           
        },
        messages: {
           
        },
        submitHandler: function (form) {
            $("#DeleteCountryShowLoader").show();
            $("#DeleteCountryShowButtons").hide();
            $.ajax({
                url: "/AdminCountry/DeleteCountry",
                type: "post",
                datatype: "json",
                data: { Id: $("#DCountryId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#DeleteCountry").modal("hide");
                        $("#DeleteCountryShowLoader").hide();
                        $("#DeleteCountryShowButtons").show();
                        toastr.success("Country Deleted Successfully", "Success");
                        $('#AdminCountries').DataTable().ajax.reload();
                    }
                    else if (response == 2)
                    {
                        $("#DeleteCountryShowLoader").hide();
                        $("#DeleteCountryShowButtons").show();
                        toastr.warning("Please Delete its States/Provinces First", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#DeleteCountryShowLoader").hide();
                        $("#DeleteCountryShowButtons").show();
                        toastr.error("Country Not Deleted", "Error");
                    }
                },
                error: function (response) {
                    $("#DeleteCountryShowLoader").hide();
                    $("#DeleteCountryShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    ///////////////////////////////////ResetForm////////////////////////////////////////////////////
    function ResetCountryIds() {
        $("#ACountryCode").val('');
        $("#ACountryName").val('');
        $("#UCountryId").val('');
        $("#UCountryCode").val('');
        $("#UCountryName").val('');
        $("#DCountryId").val('');
        $("#ULatitude").val('');
        $("#ULongitude").val('');
        $("#ALatitude").val('');
        $("#ALongitude").val('');
    }

});
