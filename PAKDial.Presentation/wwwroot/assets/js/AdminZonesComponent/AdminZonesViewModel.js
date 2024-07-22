$(function () {
    $('#AdminZones').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminZones/LoadZones",
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
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateZonesModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteZonesModal">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + ' </td>'
                }
            }
        ],

    });

    // On Click Create Modal Opened
    $("#AddZonesModal").on('click', function () {
        var AddZonesvalidator = $("#AddZones_Submit_Form").validate();
        AddZonesvalidator.resetForm();
        ResetZonesIds();
        $("#AddZones").modal('show');
    });
    // On Click Update Modal Opened
    $("#AdminZones").on('click', '#UpdateZonesModal', function () {
        var Id = $(this).val();
        var Updatevalidator = $("#UpdateZones_Submit_Form").validate();
        Updatevalidator.resetForm();
        ResetZonesIds();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminZones/GetZonesById",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response != null) {
                        $("#UZoneId").val(response.id);
                        $("#UZoneName").val(response.name);
                        $("#UpdateZones").modal('show');
                    }
                    else {
                        toastr.error("Zone Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }

    });
    // On Click Delete Modal Opened
    $("#AdminZones").on('click', '#DeleteZonesModal', function () {
        var Id = $(this).val();
        var Deletevalidator = $("#DeleteZones_Submit_Form").validate();
        Deletevalidator.resetForm();
        ResetZonesIds();
        if (Id != "" && Id != null) {
            $("#DZoneId").val(Id);
            $("#DeleteZones").modal('show');
        }
        else {
            toastr.error("Zone Not Exits", "Error");
        }

    });

    //Add Zone Form Submit
    $("#AddZones_Submit_Form").validate({
        rules: {
            AZoneName: {
                required: true,
            }
        },
        messages: {
            AZoneName: {
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
            $("#AddZoneShowLoader").show();
            $("#AddZoneShowButtons").hide();
            var zone = new Zones();
            zone.Name = $("#AZoneName").val();
            $.ajax({
                url: "/AdminZones/AddZone",
                type: "post",
                datatype: "json",
                data: { instance: zone },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response == 1) {
                        $("#AddZones").modal("hide");
                        $("#AddZoneShowLoader").hide();
                        $("#AddZoneShowButtons").show();
                        toastr.success("Zone Saved Successfully", "Success");
                        $('#AdminZones').DataTable().ajax.reload();
                    }
                    else if (response == -2) {
                        $("#AddZoneShowLoader").hide();
                        $("#AddZoneShowButtons").show();
                        toastr.warning("Zone Already Exists", "Warning");
                    } 
                    else {
                        $("#AddZoneShowLoader").hide();
                        $("#AddZoneShowButtons").show();
                        toastr.error("Zone Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddZoneShowLoader").hide();
                    $("#AddZoneShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //Update Zone Form Submit
    $("#UpdateZones_Submit_Form").validate({
        rules: {
            UZoneId: {
                required: true
            },
            UZoneName: {
                required: true,
            }
        },
        messages: {
            UZoneId: {
                required: "Please provide a valid Passcode."
            },
            UZoneName: {
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
            $("#UpdateZoneShowLoader").show();
            $("#UpdateZoneShowButtons").hide();
            var zone = new Zones();
            zone.Id = $("#UZoneId").val();
            zone.Name = $("#UZoneName").val();
            $.ajax({
                url: "/AdminZones/UpdateZone",
                type: "post",
                datatype: "json",
                data: { instance: zone },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response == 1) {
                        $("#UpdateZones").modal("hide");
                        $("#UpdateZoneShowLoader").hide();
                        $("#UpdateZoneShowButtons").show();
                        toastr.success("Zone Updated Successfully", "Success");
                        $('#AdminZones').DataTable().ajax.reload();
                    }
                    else if (response == -2) {
                        $("#UpdateZoneShowLoader").hide();
                        $("#UpdateZoneShowButtons").show();
                        toastr.warning("Zone Already Exists", "Warning");
                    }
                    else {
                        $("#UpdateZoneShowLoader").hide();
                        $("#UpdateZoneShowButtons").show();
                        toastr.error("Zone Not Updated", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateZoneShowLoader").hide();
                    $("#UpdateZoneShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //Delete Zone Form Submit
    $("#DeleteZones_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#DeleteZoneShowLoader").show();
            $("#DeleteZoneShowButtons").hide();
            $.ajax({
                url: "/AdminZones/DeleteZone",
                type: "post",
                datatype: "json",
                data: { Id: $("#DZoneId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response == 1) {
                        $("#DeleteZones").modal("hide");
                        $("#DeleteZoneShowLoader").hide();
                        $("#DeleteZoneShowButtons").show();
                        toastr.success("Zone Deleted Successfully", "Success");
                        $('#AdminZones').DataTable().ajax.reload();
                    }
                    else if (response == -2) {
                        $("#DeleteZoneShowLoader").hide();
                        $("#DeleteZoneShowButtons").show();
                        toastr.warning("Please Delete its Active Zone", "Warning");
                    }
                    else {
                        $("#DeleteZoneShowLoader").hide();
                        $("#DeleteZoneShowButtons").show();
                        toastr.error("Zone Not Deleted", "Error");
                    }
                },
                error: function (response) {
                    $("#DeleteZoneShowLoader").hide();
                    $("#DeleteZoneShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    function ResetZonesIds() {
        $("#AZoneName").val('');
        $("#UZoneId").val('');
        $("#UZoneName").val('');
        $("#DZoneId").val('');
    }
});