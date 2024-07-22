$(function () {
    $('#AdminCities').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminCity/LoadCities",
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
            { "data": "stateName", "name": "stateName", "orderable": false },
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
            { "width": "8%", "targets": 0 },
            { "width": "15%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "15%", "targets": 3 },
            { "width": "10%", "targets": 4 },
            { "width": "10%", "targets": 5 },
            {
                "width": "20%",
                targets: 6,
                render: function (data, type, full, meta) {
                    var Id = full.id;
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateCityModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteCityModal">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + ' </td>'
                }
            }
        ],

    });

    // On Click Create Modal Opened
    $("#AddCityModal").on('click', function () {
        var AddCityvalidator = $("#AddCity_Submit_Form").validate();
        AddCityvalidator.resetForm();
        ResetCityIds();
        AddBindCountries();
        $("#AProvinceId").empty().append('<option selected="selected" value="" disabled = "disabled">Please Select State/Province</option>');
        $("#AddCity").modal('show');
    });

    // On Click Update Modal Opened
    $("#AdminCities").on('click', '#UpdateCityModal', function () {
        var Id = $(this).val();
        var UpdateCityvalidator = $("#UpdateCity_Submit_Form").validate();
        UpdateCityvalidator.resetForm();
        ResetCityIds();
        if (Id != "" && Id != null) {
            UpdateBindCountries();
            UpdateBindStates(Id);
        }
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminCity/GetCityById",
                type: "get",
                datatype: "json",
                async: false,
                data: { Id: Id },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response != null) {
                        $("#UCityId").val(response.id);
                        $("#UCountryId").find('option[value="' + response.countryId + '"]').attr('selected', 'selected')
                        $("#UProvinceId").find('option[value="' + response.stateId + '"]').attr('selected', 'selected')
                        $("#UCityName").val(response.name);
                        $("#ULatitude").val(response.cityLat);
                        $("#ULongitude").val(response.cityLog);
                        $("#UpdateCity").modal('show');
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
    $("#AdminCities").on('click', '#DeleteCityModal', function () {
        var Id = $(this).val();
        var DeleteCityvalidator = $("#DeleteCity_Submit_Form").validate();
        DeleteCityvalidator.resetForm();
        if (Id != "" && Id != null) {
            $("#DCityId").val(Id);
            $("#DeleteCity").modal('show');
        }
        else {
            toastr.error("City Not Exits", "Error");
        }

    });

    //Add City Form Submit
    $("#AddCity_Submit_Form").validate({
        rules: {
            ACountryId: {
                required: true,
                numberonly: true,
            },
            AProvinceId: {
                required: true,
                numberonly: true
            },
            ACityName: {
                required: true,
                letteronly: true
            }
        },
        messages: {
            ACountryId: {
                required: "Please Select Country."
            },
            AProvinceId: {
                required: "Please Select State/Province."
            },
            ACityName: {
                required: "Please provide a City Name."
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
            $("#AddCityShowLoader").show();
            $("#AddCityShowButtons").hide();
            var city = new City();
            city.CountryId = $("#ACountryId").val();
            city.StateId = $("#AProvinceId").val();
            city.Name = $("#ACityName").val();
            city.CityLat = $("#ALatitude").val();
            city.CityLog = $("#ALongitude").val();
            $.ajax({
                url: "/AdminCity/AddCity",
                type: "post",
                datatype: "json",
                data: { city: city },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#AddCity").modal("hide");
                        $("#AddCityShowLoader").hide();
                        $("#AddCityShowButtons").show();
                        toastr.success("City Saved Successfully", "Success");
                        $('#AdminCities').DataTable().ajax.reload();
                    }
                    else if (response == 2) {
                        $("#AddCityShowLoader").hide();
                        $("#AddCityShowButtons").show();
                        toastr.error("City Already Exits", "Error");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#AddCityShowLoader").hide();
                        $("#AddCityShowButtons").show();
                        toastr.error("City Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddCityShowLoader").hide();
                    $("#AddCityShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //Update City Form Submit
    $("#UpdateCity_Submit_Form").validate({
        rules: {
            UCityId: {
                required: true,
                numberonly: true,
            },
            UCountryId: {
                required: true,
                numberonly: true,
            },
            UProvinceId: {
                required: true,
                numberonly: true
            },
            UCityName: {
                required: true,
                letteronly: true
            }
        },
        messages: {
            UCityId: {
                required: "Please provide a valid State Passcode."
            },
            UCountryId: {
                required: "Please Select Country."
            },
            UProvinceId: {
                required: "Please Select State/Province."
            },
            UCityName: {
                required: "Please provide a City Name."
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
            $("#UpdateCityShowLoader").show();
            $("#UpdateCityShowButtons").hide();
            var city = new City();
            city.Id = $("#UCityId").val();
            city.CountryId = $("#UCountryId").val();
            city.StateId = $("#UProvinceId").val();
            city.Name = $("#UCityName").val();
            city.CityLat = $("#ULatitude").val();
            city.CityLog = $("#ULongitude").val();
            $.ajax({
                url: "/AdminCity/UpdateCity",
                type: "post",
                datatype: "json",
                data: { city: city },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#UpdateCity").modal("hide");
                        $("#UpdateCityShowLoader").hide();
                        $("#UpdateCityShowButtons").show();
                        toastr.success("City Updated Successfully", "Success");
                        $('#AdminCities').DataTable().ajax.reload();
                    }
                    else if (response == 2) {
                        $("#UpdateCityShowLoader").hide();
                        $("#UpdateCityShowButtons").show();
                        toastr.error("City Already Exits", "Error");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#UpdateCityShowLoader").hide();
                        $("#UpdateCityShowButtons").show();
                        toastr.error("City Not Updated", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateCityShowLoader").hide();
                    $("#UpdateCityShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //Delete Country Form Submit
    $("#DeleteCity_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#DeleteCityShowLoader").show();
            $("#DeleteCityShowButtons").hide();
            $.ajax({
                url: "/AdminCity/DeleteCity",
                type: "post",
                datatype: "json",
                data: { Id: $("#DCityId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#DeleteCity").modal("hide");
                        $("#DeleteCityShowLoader").hide();
                        $("#DeleteCityShowButtons").show();
                        toastr.success("City Deleted Successfully", "Success");
                        $('#AdminCities').DataTable().ajax.reload();
                    }
                    else if (response == 2) {
                        $("#DeleteCityShowLoader").hide();
                        $("#DeleteCityShowButtons").show();
                        toastr.warning("Please Delete its Areas First", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#DeleteCityShowLoader").hide();
                        $("#DeleteCityShowButtons").show();
                        toastr.error("City Not Deleted", "Error");
                    }
                },
                error: function (response) {
                    $("#DeleteCityShowLoader").hide();
                    $("#DeleteCityShowButtons").show();
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

    //Get All Countries For Binding With Update DropDown List
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

    //Get All States For Binding With Update DropDown List
    function UpdateBindStates(Id) {
        var BindUpdateStates = $("#UProvinceId");
        BindUpdateStates.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        $.ajax({
            url: "/AdminCity/GetStateByCityId",
            type: "get",
            datatype: "json",
            async: false,
            data: { Id: Id },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                BindUpdateStates.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response, function (i) {
                    BindUpdateStates.append($("<option></option>").val(this['id']).html(this['name']));
                });
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }

    //Get States on Country Changes Binding With Add DropDown List
    $("#ACountryId").on('change', function () {
        var CountryId = $(this).val();
        var BindAddStates = $("#AProvinceId");
        BindAddStates.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        if (CountryId != "") {
            $.ajax({
                url: "/AdminProvince/GetAllStates",
                type: "get",
                datatype: "json",
                data: { CountryId: CountryId },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    BindAddStates.empty().append('<option selected="selected" value="">Please select</option>');
                    $.each(response, function (i) {
                        BindAddStates.append($("<option></option>").val(this['id']).html(this['name']));
                    });
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //Get States on Country Changes Binding With Update DropDown List
    $("#UCountryId").on('change', function () {
        var CountryId = $(this).val();
        var BindUpdateStates = $("#UProvinceId");
        BindUpdateStates.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        if (CountryId != "") {
            $.ajax({
                url: "/AdminProvince/GetAllStates",
                type: "get",
                datatype: "json",
                data: { CountryId: CountryId },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    BindUpdateStates.empty().append('<option selected="selected" value="">Please select</option>');
                    $.each(response, function (i) {
                        BindUpdateStates.append($("<option></option>").val(this['id']).html(this['name']));
                    });
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
    });

    function ResetCityIds() {

        $("#ACountryId").val('');
        $("#AProvinceId").val('');
        $("#ACityName").val('');
        $("#UCityId").val('');
        $("#UCountryId").val('');
        $("#UProvinceId").val('');
        $("#UCityName").val('');
        $("#DCityId").val('');
        $("#ULatitude").val('');
        $("#ULongitude").val('');
        $("#ALatitude").val('');
        $("#ALongitude").val('');

    }
});