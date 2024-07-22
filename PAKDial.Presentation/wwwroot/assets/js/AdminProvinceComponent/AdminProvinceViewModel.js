$(function () {
    $('#AdminProvinces').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminProvince/LoadStates",
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
            { "data": "countryName", "name": "countryName", "orderable": false },
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
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateProvinceModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteProvinceModal">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + ' </td>'
                }
            }
        ],

    });

    // On Click Create Modal Opened
    $("#AddProvinceModal").on('click', function () {
        var AddProvincevalidator = $("#AddProvince_Submit_Form").validate();
        AddProvincevalidator.resetForm();
        ResetProvinceIds();
        AddBindCountries();
        $("#AddProvince").modal('show');

    });
    // On Click Update Modal Opened
    $("#AdminProvinces").on('click', '#UpdateProvinceModal', function () {
        var Id = $(this).val();
        var UpdateProvincevalidator = $("#UpdateProvince_Submit_Form").validate();
        UpdateProvincevalidator.resetForm();
        ResetProvinceIds();
        UpdateBindCountries();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminProvince/GetProvinceById",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response != null) {
                        $("#UProvinceId").val(response.id);
                        //$("#UCountryId").val(response.countryId);
                         $("#UCountryId").find('option[value="' + response.countryId +'"]').attr('selected','selected') 
                        $("#UProvinceName").val(response.name);
                        $("#ULatitude").val(response.latitude);
                        $("#ULongitude").val(response.longitude);
                         $("#UpdateProvince").modal('show');
                     }
                    else {
                        toastr.error("State/Province Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }

    });
    // On Click Delete Modal Opened
    $("#AdminProvinces").on('click', '#DeleteProvinceModal', function () {
        var Id = $(this).val();
        var DeleteProvincevalidator = $("#DeleteProvince_Submit_Form").validate();
        DeleteProvincevalidator.resetForm();
        if (Id != "" && Id != null) {
            $("#DProvinceId").val(Id);
            $("#DeleteProvince").modal('show');
        }
        else {
            toastr.error("State/Province Not Exits", "Error");
        }

    });

    //Add State/Province Form Submit
    $("#AddProvince_Submit_Form").validate({
        rules: {
            ACountryId: {
                required: true,
                numberonly: true,
            },
            AProvinceName: {
                required: true,
                letteronly: true
            }
        },
        messages: {
            ACountryId: {
                required: "Please Select Country."
            },
            AProvinceName: {
                required: "Please provide a State/Province Name."
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
            $("#AddProvinceShowLoader").show();
            $("#AddProvinceShowButtons").hide();
            var state = new StateProvince();
            state.CountryId = $("#ACountryId").val();
            state.Name = $("#AProvinceName").val();
            state.Latitude = $("#ALatitude").val();
            state.Longitude = $("#ALongitude").val();
            $.ajax({
                url: "/AdminProvince/AddProvince",
                type: "post",
                datatype: "json",
                data: { stateProvince: state },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#AddProvince").modal("hide");
                        $("#AddProvinceShowLoader").hide();
                        $("#AddProvinceShowButtons").show();
                        toastr.success("Country Saved Successfully", "Success");
                        $('#AdminProvinces').DataTable().ajax.reload();
                    }
                    else if (response == 2)
                    {
                        $("#AddProvinceShowLoader").hide();
                        $("#AddProvinceShowButtons").show();
                        toastr.error("State/Province Already Exits", "Error");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#AddProvinceShowLoader").hide();
                        $("#AddProvinceShowButtons").show();
                        toastr.error("State/Province Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddProvinceShowLoader").hide();
                    $("#AddProvinceShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //Update State/Province Form Submit
    $("#UpdateProvince_Submit_Form").validate({
        rules: {
            UProvinceId: {
                required: true,
                numberonly: true,
            },
            UCountryId: {
                required: true,
                numberonly: true,
            },
            UProvinceName: {
                required: true,
                letteronly: true
            }
        },
        messages: {
            UProvinceId: {
                required: "Please provide a valid State Passcode."
            },
            UCountryId: {
                required: "Please Select Country."
            },
            UProvinceName: {
                required: "Please provide a State/Province Name."
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
            $("#UpdateProvinceShowLoader").show();
            $("#UpdateProvinceShowButtons").hide();
            var state = new StateProvince();
            state.Id = $("#UProvinceId").val();
            state.CountryId = $("#UCountryId").val();
            state.Name = $("#UProvinceName").val();
            state.Latitude = $("#ULatitude").val();
            state.Longitude = $("#ULongitude").val();

            $.ajax({
                url: "/AdminProvince/UpdateProvince",
                type: "post",
                datatype: "json",
                data: { stateProvince: state },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#UpdateProvince").modal("hide");
                        $("#UpdateProvinceShowLoader").hide();
                        $("#UpdateProvinceShowButtons").show();
                        toastr.success("State/Province Updated Successfully", "Success");
                        $('#AdminProvinces').DataTable().ajax.reload();

                    }
                    else if (response == 2) {
                        $("#UpdateProvinceShowLoader").hide();
                        $("#UpdateProvinceShowButtons").show();
                        toastr.warning("State/Province Already Exists", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#UpdateProvinceShowLoader").hide();
                        $("#UpdateProvinceShowButtons").show();
                        toastr.error("State/Province Not Updated", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateProvinceShowLoader").hide();
                    $("#UpdateProvinceShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //Delete Country Form Submit
    $("#DeleteProvince_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#DeleteProvinceShowLoader").show();
            $("#DeleteProvinceShowButtons").hide();
            $.ajax({
                url: "/AdminProvince/DeleteProvince",
                type: "post",
                datatype: "json",
                data: { Id: $("#DProvinceId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#DeleteProvince").modal("hide");
                        $("#DeleteProvinceShowLoader").hide();
                        $("#DeleteProvinceShowButtons").show();
                        toastr.success("State/Province Deleted Successfully", "Success");
                        $('#AdminProvinces').DataTable().ajax.reload();
                    }
                    else if (response == 2) {
                        $("#DeleteProvinceShowLoader").hide();
                        $("#DeleteProvinceShowButtons").show();
                        toastr.warning("Please Delete its Cities First", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#DeleteProvinceShowLoader").hide();
                        $("#DeleteProvinceShowButtons").show();
                        toastr.error("State/Province Not Deleted", "Error");
                    }
                },
                error: function (response) {
                    $("#DeleteProvinceShowLoader").hide();
                    $("#DeleteProvinceShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //Get All Countries For Binding With Add DropDown List
    function AddBindCountries() {
        var BindAddCountries = $("#ACountryId");
        BindAddCountries.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        $.ajax({
            url: "/AdminCountry/GetAllCountries",
            type: "get",
            datatype: "json",
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                BindAddCountries.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response, function (i) {
                    BindAddCountries.append($("<option></option>").val(this['id']).html(this['name']));
                });
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }
    //Get All Countries For Binding With Edit DropDown List
    function UpdateBindCountries() {
        var BindUpdateCountries = $("#UCountryId");
        BindUpdateCountries.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        $.ajax({
            url: "/AdminCountry/GetAllCountries",
            type: "get",
            datatype: "json",
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                BindUpdateCountries.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response, function (i) {
                    BindUpdateCountries.append($("<option></option>").val(this['id']).html(this['name']));
                });
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }

    function ResetProvinceIds() {
        $("#ACountryId").val('');
        $("#AProvinceName").val('');
        $("#UProvinceId").val('');
        $("#UCountryId").val('');
        $("#UProvinceName").val('');
        $("#DProvinceId").val('');
        $("#ULatitude").val('');
        $("#ULongitude").val('');
        $("#ALatitude").val('');
        $("#ALongitude").val('');

    }
});