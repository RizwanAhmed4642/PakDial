$(function () {
    $('#AdminActiveZones').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminActiveZone/LoadActiveZone",
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
            { "data": "stateName", "name": "stateName", "orderable": false },
            { "data": "cityName", "name": "cityName", "orderable": false },
            { "data": "cityAreaName", "name": "cityAreaName", "orderable": false },
            { "data": "zoneName", "name": "zoneName", "orderable": false },
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
            { "width": "15%", "targets": 2 },
            { "width": "15%", "targets": 3 },
            { "width": "15%", "targets": 4 },
            { "width": "10%", "targets": 5 },
            {
                "width": "20%",
                targets: 6,
                render: function (data, type, full, meta) {
                    var Id = full.id;
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateActiveZonesModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteActiveZonesModal">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + ' </td>'
                }
            }
        ],

    });

    // On Click Create Modal Opened
    $("#AddActiveZonesModal").on('click', function () {
        var Addvalidator = $("#AddActiveZones_Submit_Form").validate();
        Addvalidator.resetForm();
        ResetActiveZonesIds();
        BindStatesZones();
        $("#AActiveZoneCityId").empty().append('<option selected="selected" value="">Please select</option>');
        $("#AActiveZoneAreaId").empty().append('<option selected="selected" value="">Please select</option>');
        $("#AddActiveZones").modal('show');
    });
    // On Click Update Modal Opened
    $("#AdminActiveZones").on('click', '#UpdateActiveZonesModal', function () {
        var Id = $(this).val();
        var Updatevalidator = $("#UpdateActiveZones_Submit_Form").validate();
        Updatevalidator.resetForm();
        ResetActiveZonesIds();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminActiveZone/GetWrapActiveZoneById",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response != null) {
                        BindModelOnUpdate(response);
                        $("#UActiveZoneId").val(response.activeZones.id);
                        $("#UActiveZoneStateId").find('option[value="' + response.activeZones.stateId + '"]').attr('selected', 'selected');
                        $("#UActiveZoneCityId").find('option[value="' + response.activeZones.cityId + '"]').attr('selected', 'selected');
                        $("#UActiveZoneAreaId").find('option[value="' + response.activeZones.cityAreaId + '"]').attr('selected', 'selected');
                        $("#UActiveZoneZoneId").find('option[value="' + response.activeZones.zoneId + '"]').attr('selected', 'selected');
                        $("#UpdateActiveZones").modal('show');
                    }
                    else {
                        toastr.error("Active Zone Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }

    });
    // On Click Delete Modal Opened
    $("#AdminActiveZones").on('click', '#DeleteActiveZonesModal', function () {
        var Id = $(this).val();
        var Deletevalidator = $("#DeleteActiveZones_Submit_Form").validate();
        Deletevalidator.resetForm();
        ResetActiveZonesIds();
        if (Id != "" && Id != null) {
            $("#DActiveZoneId").val(Id);
            $("#DeleteActiveZones").modal('show');
        }
        else {
            toastr.error("Active Zones Not Exits", "Error");
        }

    });


    //Add Active Zone Form Submit
    $("#AddActiveZones_Submit_Form").validate({
        rules: {
            AActiveZoneStateId: {
                required: true,
                numberonly: true
            },
            AActiveZoneCityId: {
                required: true,
                numberonly: true
            },
            AActiveZoneAreaId: {
                required: true,
                numberonly: true
            },
            AActiveZoneZoneId: {
                required: true,
                numberonly: true
            }
        },
        messages: {
            AActiveZoneStateId: {
                required: "Please select state."
            },
            AActiveZoneCityId: {
                required: "Please select city."
            },
            AActiveZoneAreaId: {
                required: "Please select area."
            },
            AActiveZoneZoneId: {
                required: "Please select zone."
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
            $("#AddActiveZonesShowLoader").show();
            $("#AddActiveZonesShowButtons").hide();
            var activeZone = new ActiveZone();
            activeZone.StateId = $("#AActiveZoneStateId").val();
            activeZone.CityId = $("#AActiveZoneCityId").val();
            activeZone.CityAreaId = $("#AActiveZoneAreaId").val();
            activeZone.ZoneId = $("#AActiveZoneZoneId").val();
            $.ajax({
                url: "/AdminActiveZone/AddActiveZone",
                type: "post",
                datatype: "json",
                data: { instance: activeZone },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response == 1) {
                        $("#AddActiveZones").modal("hide");
                        $("#AddActiveZonesShowLoader").hide();
                        $("#AddActiveZonesShowButtons").show();
                        toastr.success("Active Zone Saved Successfully", "Success");
                        $('#AdminActiveZones').DataTable().ajax.reload();
                    }
                    else if (response == -2) {
                        $("#AddActiveZonesShowLoader").hide();
                        $("#AddActiveZonesShowButtons").show();
                        toastr.warning("Active Zone Already Exists", "Warning");
                    }
                    else {
                        $("#AddActiveZonesShowLoader").hide();
                        $("#AddActiveZonesShowButtons").show();
                        toastr.error("Active Zone Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddActiveZonesShowLoader").hide();
                    $("#AddActiveZonesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //Update Active Zone Form Submit
    $("#UpdateActiveZones_Submit_Form").validate({
        rules: {
            UActiveZoneId: {
                required: true
            },
            UActiveZoneStateId: {
                required: true,
                numberonly: true
            },
            UActiveZoneCityId: {
                required: true,
                numberonly: true
            },
            UActiveZoneAreaId: {
                required: true,
                numberonly: true
            },
            UActiveZoneZoneId: {
                required: true,
                numberonly: true
            }
        },
        messages: {
            UActiveZoneId: {
                required: "Please provide a valid Passcode."
            },
            UActiveZoneStateId: {
                required: "Please select state."
            },
            UActiveZoneCityId: {
                required: "Please select city."
            },
            UActiveZoneAreaId: {
                required: "Please select area."
            },
            UActiveZoneZoneId: {
                required: "Please select zone."
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
            $("#UpdateActiveZonesShowLoader").show();
            $("#UpdateActiveZonesShowButtons").hide();
            var activeZone = new ActiveZone();
            activeZone.Id = $("#UActiveZoneId").val();
            activeZone.StateId = $("#UActiveZoneStateId").val();
            activeZone.CityId = $("#UActiveZoneCityId").val();
            activeZone.CityAreaId = $("#UActiveZoneAreaId").val();
            activeZone.ZoneId = $("#UActiveZoneZoneId").val();
            $.ajax({
                url: "/AdminActiveZone/UpdateActiveZone",
                type: "post",
                datatype: "json",
                data: { instance: activeZone },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response == 1) {
                        $("#UpdateActiveZones").modal("hide");
                        $("#UpdateActiveZonesShowLoader").hide();
                        $("#UpdateActiveZonesShowButtons").show();
                        toastr.success("Active Zone Updated Successfully", "Success");
                        $('#AdminActiveZones').DataTable().ajax.reload();
                    }
                    else if (response == -2) {
                        $("#UpdateActiveZonesShowLoader").hide();
                        $("#UpdateActiveZonesShowButtons").show();
                        toastr.warning("Active Zone Already Exists", "Warning");
                    }
                    else {
                        $("#UpdateActiveZonesShowLoader").hide();
                        $("#UpdateActiveZonesShowButtons").show();
                        toastr.error("Active Zone Not Updated", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateActiveZonesShowLoader").hide();
                    $("#UpdateActiveZonesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //Delete Active Zone Form Submit
    $("#DeleteActiveZones_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#DeleteActiveZonesShowLoader").show();
            $("#DeleteActiveZonesShowButtons").hide();
            $.ajax({
                url: "/AdminActiveZone/DeleteActiveZone",
                type: "post",
                datatype: "json",
                data: { Id: $("#DActiveZoneId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response == 1) {
                        $("#DeleteActiveZones").modal("hide");
                        $("#DeleteActiveZonesShowLoader").hide();
                        $("#DeleteActiveZonesShowButtons").show();
                        toastr.success("Active Zone Deleted Successfully", "Success");
                        $('#AdminActiveZones').DataTable().ajax.reload();
                    }
                    else if (response == -2) {
                        $("#DeleteActiveZonesShowLoader").hide();
                        $("#DeleteActiveZonesShowButtons").show();
                        toastr.warning("Please Delete its Child Record", "Warning");
                    }
                    else {
                        $("#DeleteActiveZonesShowLoader").hide();
                        $("#DeleteActiveZonesShowButtons").show();
                        toastr.error("Active Zone Not Deleted", "Error");
                    }
                },
                error: function (response) {
                    $("#DeleteActiveZonesShowLoader").hide();
                    $("#DeleteActiveZonesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    function BindStatesZones() {
        var BindAddState = $("#AActiveZoneStateId");
        var BindAddZones = $("#AActiveZoneZoneId");
        BindAddState.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        BindAddZones.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        $.ajax({
            url: "/AdminActiveZone/GetAllStatesZones",
            type: "get",
            datatype: "json",
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                BindAddState.empty().append('<option selected="selected" value="">Please select</option>');
                BindAddZones.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response.getStates, function (i) {
                    BindAddState.append($("<option></option>").val(this['id']).html(this['text']));
                });
                $.each(response.getZones, function (i) {
                    BindAddZones.append($("<option></option>").val(this['id']).html(this['text']));
                });
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }
    //Get Cities on State Changes Binding With Add DropDown List
    $("#AActiveZoneStateId").on('change', function () {
        var StateId = $(this).val();
        var BindCities = $("#AActiveZoneCityId");
        BindCities.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        if (StateId != "") {
            $.ajax({
                url: "/AdminActiveZone/GetAllCityByStates",
                type: "get",
                datatype: "json",
                data: { StateId: StateId },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    BindCities.empty().append('<option selected="selected" value="">Please select</option>');
                    $.each(response, function (i) {
                        BindCities.append($("<option></option>").val(this['id']).html(this['text']));
                    });
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //Get Areas on City Changes Binding With Add DropDown List
    $("#AActiveZoneCityId").on('change', function () {
      
        var CityId = $(this).val();
        var BindAreas = $("#AActiveZoneAreaId");
        BindAreas.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        if (CityId != "") {
            $.ajax({
                url: "/AdminActiveZone/GetAllCityAreaByCityId",
                type: "get",
                datatype: "json",
                data: { CityId: CityId },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    BindAreas.empty().append('<option selected="selected" value="">Please select</option>');
                    $.each(response, function (i) {
                        BindAreas.append($("<option></option>").val(this['id']).html(this['text']));
                    });
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //Get Cities on State Changes Binding With Update DropDown List
    $("#UActiveZoneStateId").on('change', function () {
        var StateId = $(this).val();
        var BindCities = $("#UActiveZoneCityId");
        BindCities.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        if (StateId != "") {
            $.ajax({
                url: "/AdminActiveZone/GetAllCityByStates",
                type: "get",
                datatype: "json",
                data: { StateId: StateId },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    BindCities.empty().append('<option selected="selected" value="">Please select</option>');
                    $.each(response, function (i) {
                        BindCities.append($("<option></option>").val(this['id']).html(this['text']));
                    });
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //Get Areas on City Changes Binding With Update DropDown List
    $("#UActiveZoneCityId").on('change', function () {
    
        var CityId = $(this).val();
        var BindAreas = $("#UActiveZoneAreaId");
        BindAreas.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        if (CityId != "") {
            $.ajax({
                url: "/AdminActiveZone/GetAllCityAreaByCityId",
                type: "get",
                datatype: "json",
                data: { CityId: CityId },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    BindAreas.empty().append('<option selected="selected" value="">Please select</option>');
                    $.each(response, function (i) {
                        BindAreas.append($("<option></option>").val(this['id']).html(this['text']));
                    });
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
    });

    function BindModelOnUpdate(response) {
        var BindAddState = $("#UActiveZoneStateId");
        var BindAddCity = $("#UActiveZoneCityId");
        var BindAddCityArea = $("#UActiveZoneAreaId");
        var BindAddZones = $("#UActiveZoneZoneId");

        BindAddState.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        BindAddCity.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        BindAddCityArea.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        BindAddZones.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');

        BindAddState.empty().append('<option selected="selected" value="">Please select</option>');
        $.each(response.getStates, function (i) {
            BindAddState.append($("<option></option>").val(this['id']).html(this['text']));
        });
        BindAddCity.empty().append('<option selected="selected" value="">Please select</option>');
        $.each(response.getCities, function (i) {
            BindAddCity.append($("<option></option>").val(this['id']).html(this['text']));
        });
        BindAddCityArea.empty().append('<option selected="selected" value="">Please select</option>');
        $.each(response.getAreas, function (i) {
            BindAddCityArea.append($("<option></option>").val(this['id']).html(this['text']));
        });
        BindAddZones.empty().append('<option selected="selected" value="">Please select</option>');
        $.each(response.getZones, function (i) {
            BindAddZones.append($("<option></option>").val(this['id']).html(this['text']));
        });
    }
    function ResetActiveZonesIds() {
        $("#AActiveZoneStateId").val('');
        $("#AActiveZoneCityId").val('');
        $("#AActiveZoneAreaId").val('');
        $("#AActiveZoneAreaId").val('');

        $("#UActiveZoneId").val('');
        $("#UActiveZoneStateId").val('');
        $("#UActiveZoneCityId").val('');
        $("#UActiveZoneAreaId").val('');
        $("#UActiveZoneZoneId").val('');

        $("#DActiveZoneId").val('');
    }
});