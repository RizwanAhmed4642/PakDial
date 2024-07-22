$(function () {
    $('#AdminCityAreas').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminCityAreas/LoadCityAreas",
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
            { "data": "cityName", "name": "cityName", "orderable": false },
            { "data": "stateName", "name": "stateName", "orderable": false },
            { "data": "countryName", "name": "countryName", "orderable": false },
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
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateCityAreaModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteCityAreaModal">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + ' </td>'
                }
            }
        ],

    });

    // On Click Create Modal Opened
    $("#AddCityAreaModal").on('click', function () {
        var AddCityAreavalidator = $("#AddCityArea_Submit_Form").validate();
        AddCityAreavalidator.resetForm();
        ResetCityAreaIds();
        AddBindCountries();
        $("#AProvinceId").empty().append('<option selected="selected" value="" disabled = "disabled">Please Select State/Province</option>');
        $("#ACityId").empty().append('<option selected="selected" value="" disabled = "disabled">Please Select City</option>');
        $("#AddCityArea").modal('show');
    });

    // On Click Update Modal Opened
    $("#AdminCityAreas").on('click', '#UpdateCityAreaModal', function () {
        var Id = $(this).val();
        var UpdateCityAreavalidator = $("#UpdateCityArea_Submit_Form").validate();
        UpdateCityAreavalidator.resetForm();
        ResetCityAreaIds();
        if (Id != "" && Id != null) {
            UpdateBindCountries();
            UpdateBindStatesandCity(Id);
        }
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminCityAreas/GetCityAreaById",
                type: "get",
                datatype: "json",
                async: false,
                data: { Id: Id },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response != null) {
                        $("#UCityAreaId").val(response.id);
                        $("#UCountryId").find('option[value="' + response.countryId + '"]').attr('selected', 'selected');
                        $("#UProvinceId").find('option[value="' + response.stateId + '"]').attr('selected', 'selected');
                        $("#UCityId").find('option[value="' + response.cityId + '"]').attr('selected', 'selected');
                        $("#UCityAreaName").val(response.name);
                        $("#ULatitude").val(response.areaLat);
                        $("#ULongitude").val(response.areaLong);
                        
                        $("#UpdateCityArea").modal('show');
                    }
                    else {
                        toastr.error("City Area Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }

    });

    // On Click Delete Modal Opened
    $("#AdminCityAreas").on('click', '#DeleteCityAreaModal', function () {
        var Id = $(this).val();
        var DeleteCityAreavalidator = $("#DeleteCityArea_Submit_Form").validate();
        DeleteCityAreavalidator.resetForm();
        if (Id != "" && Id != null) {
            $("#DCityAreaId").val(Id);
            $("#DeleteCityArea").modal('show');
        }
        else {
            toastr.error("City Area Not Exits", "Error");
        }

    });

    //Add City Area Form Submit
    $("#AddCityArea_Submit_Form").validate({
        rules: {
            ACountryId: {
                required: true,
                numberonly: true,
            },
            AProvinceId: {
                required: true,
                numberonly: true
            },
            ACityId: {
                required: true,
                numberonly: true
            },
            ACityAreaName: {
                required: true,
            }
        },
        messages: {
            ACountryId: {
                required: "Please Select Country."
            },
            AProvinceId: {
                required: "Please Select State/Province."
            },
            ACityId: {
                required: "Please Select City."
            },
            ACityAreaName: {
                required: "Please provide a City Area Name."
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
            $("#AddCityAreaShowLoader").show();
            $("#AddCityAreaShowButtons").hide();
            var cityarea = new CityArea();
            cityarea.CountryId = $("#ACountryId").val();
            cityarea.StateId = $("#AProvinceId").val();
            cityarea.CityId = $("#ACityId").val();
            cityarea.Name = $("#ACityAreaName").val();
            cityarea.AreaLat = $("#ALatitude").val();
            cityarea.AreaLong = $("#ALongitude").val();
            $.ajax({
                url: "/AdminCityAreas/AddCityArea",
                type: "post",
                datatype: "json",
                data: { cityArea: cityarea },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#AddCityArea").modal("hide");
                        $("#AddCityAreaShowLoader").hide();
                        $("#AddCityAreaShowButtons").show();
                        toastr.success("City Area Saved Successfully", "Success");
                        $('#AdminCityAreas').DataTable().ajax.reload();
                    }
                    else if (response == 2) {
                        $("#AddCityAreaShowLoader").hide();
                        $("#AddCityAreaShowButtons").show();
                        toastr.error("City Area Already Exist", "Error");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#AddCityAreaShowLoader").hide();
                        $("#AddCityAreaShowButtons").show();
                        toastr.error("City Area Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddCityAreaShowLoader").hide();
                    $("#AddCityAreaShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //Update City Area Form Submit
    $("#UpdateCityArea_Submit_Form").validate({
        rules: {
            UCityAreaId: {
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
            UCityId: {
                required: true,
                numberonly: true
            },
            UCityAreaName: {
                required: true,
            }
        },
        messages: {
            UCityAreaId: {
                required: "Please provide a valid State Passcode."
            },
            UCountryId: {
                required: "Please Select Country."
            },
            UProvinceId: {
                required: "Please Select State/Province."
            },
            UCityId: {
                required: "Please Select City."
            },
            UCityAreaName: {
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
            $("#UpdateCityAreaShowLoader").show();
            $("#UpdateCityAreaShowButtons").hide();
            var cityarea = new CityArea();
            cityarea.Id = $("#UCityAreaId").val();
            cityarea.CountryId = $("#UCountryId").val();
            cityarea.StateId = $("#UProvinceId").val();
            cityarea.CityId = $("#UCityId").val();
            cityarea.Name = $("#UCityAreaName").val();
            cityarea.AreaLat = $("#ULatitude").val();
            cityarea.AreaLong = $("#ULongitude").val();
            $.ajax({
                url: "/AdminCityAreas/UpdateCityArea",
                type: "post",
                datatype: "json",
                data: { cityArea: cityarea },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#UpdateCityArea").modal("hide");
                        $("#UpdateCityAreaShowLoader").hide();
                        $("#UpdateCityAreaShowButtons").show();
                        toastr.success("City Area Updated Successfully", "Success");
                        $('#AdminCityAreas').DataTable().ajax.reload();
                    }
                    else if (response == 2) {
                        $("#UpdateCityAreaShowLoader").hide();
                        $("#UpdateCityAreaShowButtons").show();
                        toastr.error("City Area Already Exits", "Error");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#UpdateCityAreaShowLoader").hide();
                        $("#UpdateCityAreaShowButtons").show();
                        toastr.error("City Not Updated", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateCityAreaShowLoader").hide();
                    $("#UpdateCityAreaShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //Delete City Area Form Submit
    $("#DeleteCityArea_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#DeleteCityAreaShowLoader").show();
            $("#DeleteCityAreaShowButtons").hide();
            $.ajax({
                url: "/AdminCityAreas/DeleteCityArea",
                type: "post",
                datatype: "json",
                data: { Id: $("#DCityAreaId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#DeleteCityArea").modal("hide");
                        $("#DeleteCityAreaShowLoader").hide();
                        $("#DeleteCityAreaShowButtons").show();
                        toastr.success("City Area Deleted Successfully", "Success");
                        $('#AdminCityAreas').DataTable().ajax.reload();
                    }
                    else if (response == 2) {
                        $("#DeleteCityAreaShowLoader").hide();
                        $("#DeleteCityAreaShowButtons").show();
                        toastr.warning("Please Delete its Areas First", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#DeleteCityAreaShowLoader").hide();
                        $("#DeleteCityAreaShowButtons").show();
                        toastr.error("City Area Not Deleted", "Error");
                    }
                },
                error: function (response) {
                    $("#DeleteCityAreaShowLoader").hide();
                    $("#DeleteCityAreaShowButtons").show();
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

    //Get All States and Cities For Binding With Update DropDown List
    function UpdateBindStatesandCity(Id) {
        var BindUpdateStates = $("#UProvinceId");
        BindUpdateStates.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        var BindUpdateCities = $("#UCityId");
        BindUpdateCities.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        $.ajax({
            url: "/AdminCityAreas/GetSCByCityAreaId",
            type: "get",
            datatype: "json",
            async: false,
            data: { Id: Id },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                BindUpdateStates.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response.stateProvinces, function (i) {
                    BindUpdateStates.append($("<option></option>").val(this['id']).html(this['name']));
                });
                BindUpdateCities.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response.cities, function (i) {
                    BindUpdateCities.append($("<option></option>").val(this['id']).html(this['name']));
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

    //Get Cities on State Changes Binding With Add DropDown List
    $("#AProvinceId").on('change', function () {
        var StateId = $(this).val();
        var BindAddCities = $("#ACityId");
        BindAddCities.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        if (StateId != "") {
            $.ajax({
                url: "/AdminCity/GetAllCitiesByStateId",
                type: "get",
                datatype: "json",
                data: { Id: StateId },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    BindAddCities.empty().append('<option selected="selected" value="">Please select</option>');
                    $.each(response, function (i) {
                        BindAddCities.append($("<option></option>").val(this['id']).html(this['name']));
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

    //Get Cities on State Changes Binding With Update DropDown List
    $("#UProvinceId").on('change', function () {
        var StateId = $(this).val();
        var BindUpdateCities = $("#UCityId");
        BindUpdateCities.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        if (StateId != "") {
            $.ajax({
                url: "/AdminCity/GetAllCitiesByStateId",
                type: "get",
                datatype: "json",
                data: { Id: StateId },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    BindUpdateCities.empty().append('<option selected="selected" value="">Please select</option>');
                    $.each(response, function (i) {
                        BindUpdateCities.append($("<option></option>").val(this['id']).html(this['name']));
                    });
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
    });

    function ResetCityAreaIds() {
        $("#ACountryId").val('');
        $("#AProvinceId").val('');
        $("#ACityId").val('');
        $("#ACityAreaName").val('');

        $("#UCityAreaId").val('');
        $("#UCountryId").val('');
        $("#UProvinceId").val('');
        $("#UCityId").val('');
        $("#UCityAreaName").val('');

        $("#DCityAreaId").val('');

        $("#ULatitude").val('');
        $("#ULongitude").val('');
        $("#ALatitude").val('');
        $("#ALongitude").val('');
    }
});
