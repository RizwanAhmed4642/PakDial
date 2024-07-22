$(function () {
    $('#AdminPaymentModes').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminPaymentModes/LoadPaymentModes",
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
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdatePaymentsModesModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeletePaymentsModesModal">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + ' </td>'
                }
            }
        ],

    });

     //-----------------------------------------------Add Payment Modes --------------------------------------------------

    // On Click Create Modal Opened
    $("#AddPaymentModesModal").on('click', function () {
        var AddPaymentModesvalidator = $("#AddPaymentsModes_Submit_Form").validate();
        AddPaymentModesvalidator.resetForm();
        ResetPaymentModesIds();
        $('#APMtargetImg').attr("src", "/app-assets/images/portrait/small/avatar-s-19.jpg");
        $("#AddPaymentsModes").modal('show');

    });

    //Add Payment Mode Form Submit
    $("#AddPaymentsModes_Submit_Form").validate({
        rules: {
            APaymentsModesName: {
                required: true,
                letteronly: true
            },
            APaymentsModesDescrption: {
                required: true,
                letteronly: true
            },
        },
        messages: {
            APaymentsModesName: {
                required: "Please provide a Mode Name."
            },
            APaymentsModesDescrption: {
                required: "Please provide a Mode Description."
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
            $("#AddPaymentsModesShowLoader").show();
            $("#AddPaymentsModesShowButtons").hide();
            var pm = new PaymentModes();
            pm.Name = $("#APaymentsModesName").val();
            pm.Description = $("#APaymentsModesDescrption").val();
            pm.IsActive = true;
            $.ajax({
                url: "/AdminPaymentModes/AddPaymentModes",
                type: "post",
                datatype: "json",
                data: { payment: pm },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response > 0) {
                        var files = $("#APMimagefiles").get(0).files;
                        var formData = new FormData();
                        formData.append('file', files[0]);
                        if (files.length > 0) {
                            $.ajax({
                                url: "/AdminPaymentModes/UploadIcon?Id=" + response,
                                type: "post",
                                datatype: "json",
                                data: formData,
                                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                                async: false,
                                cache: false,
                                contentType: false,
                                processData: false,
                                success: function (response) {
                                    if (response.error == "403") {
                                        location.href = "/Account/AccessDenied";
                                    }
                                    else if (response > 0) {
                                        $("#AddPaymentsModes").modal("hide");
                                        $("#AddPaymentsModesShowLoader").hide();
                                        $("#AddPaymentsModesShowButtons").show();
                                        toastr.success("Payment Mode Saved Successfully", "Success");
                                        $('#AdminPaymentModes').DataTable().ajax.reload();
                                    }
                                    else {
                                        $("#AddPaymentsModesShowLoader").hide();
                                        $("#AddPaymentsModesShowButtons").show();
                                        toastr.error("Failed to Upload Image", "Error");
                                    }
                                },
                                error: function (response) {
                                    $("#AddPaymentsModesShowLoader").hide();
                                    $("#AddPaymentsModesShowButtons").show();
                                    toastr.error(response, "Error");
                                }
                            });
                        }
                        else {
                            $("#AddPaymentsModes").modal("hide");
                            $("#AddPaymentsModesShowLoader").hide();
                            $("#AddPaymentsModesShowButtons").show();
                            toastr.success("Payment Mode Saved Successfully", "Success");
                            $('#AdminPaymentModes').DataTable().ajax.reload();
                        }
                    }
                    else if (response == -2) {
                        $("#AddPaymentsModesShowLoader").hide();
                        $("#AddPaymentsModesShowButtons").show();
                        toastr.warning("Payment Mode Already Exists", "Warning");
                    }
                    else {
                        $("#AddPaymentsModesShowLoader").hide();
                        $("#AddPaymentsModesShowButtons").show();
                        toastr.error("Payment Mode Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddPaymentsModesShowLoader").hide();
                    $("#AddPaymentsModesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //-----------------------------------------------Update Payment Modes --------------------------------------------------
    // On Click Update Modal Opened
    $("#AdminPaymentModes").on('click', '#UpdatePaymentsModesModal', function () {
        var Id = $(this).val();
        var UpdatePaymentModesvalidator = $("#UpdatePaymentsModes_Submit_Form").validate();
        UpdatePaymentModesvalidator.resetForm();
        ResetPaymentModesIds();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminPaymentModes/GetPaymentModesById",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response != null) {
                        if (response.imageDir == null) {
                            $('#UPMtargetImg').attr("src", "/app-assets/images/portrait/small/avatar-s-19.jpg");
                        }
                        else {
                            $('#UPMtargetImg').attr("src", response.imageDir);
                        }

                        $("#UPaymentsModesId").val(response.id);
                        $("#UPaymentsModesName").val(response.name);
                        $("#UPaymentsModesDescrption").val(response.description);
                        if (response.isActive == true) {
                            $("#UPaymentsModesIsActive").prop('checked', true);
                        }
                        $("#UpdatePaymentsModes").modal('show');
                    }
                    else {
                        toastr.error("Payment Mode Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }

    });

    //Update Payment Mode Form Submit
    $("#UpdatePaymentsModes_Submit_Form").validate({
        rules: {
            UPaymentsModesId: {
                required: true
            },
            UPaymentsModesName: {
                required: true,
                letteronly: true
            },
            UPaymentsModesDescrption: {
                required: true,
                letteronly: true
            }
        },
        messages: {
            UPaymentsModesId: {
                required: "Please provide a valid Payment Mode Passcode."
            },
            UPaymentsModesName: {
                required: "Please provide a Mode Name."
            },
            UPaymentsModesDescrption: {
                required: "Please provide a Mode Description."
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
            $("#UpdatePaymentsModesShowLoader").show();
            $("#UpdatePaymentsModesShowButtons").hide();
            var payment = new PaymentModes();
            payment.Id = $("#UPaymentsModesId").val();
            payment.Name = $("#UPaymentsModesName").val();
            payment.Description = $("#UPaymentsModesDescrption").val();
            if ($("#UPaymentsModesIsActive").prop('checked') == true) {
                payment.IsActive = true;
            }
            $.ajax({
                url: "/AdminPaymentModes/UpdatePaymentModes",
                type: "post",
                datatype: "json",
                data: { payment: payment },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response == 1) {
                        var files = $("#UPMimagefiles").get(0).files;
                        var formData = new FormData();
                        var Id = $("#UPaymentsModesId").val();
                        formData.append('file', files[0]);
                        if (files.length > 0) {
                            $.ajax({
                                url: "/AdminPaymentModes/UploadIcon?Id=" + Id,
                                type: "post",
                                datatype: "json",
                                data: formData,
                                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                                async: false,
                                cache: false,
                                contentType: false,
                                processData: false,
                                success: function (response) {
                                    if (response.error == "403") {
                                        location.href = "/Account/AccessDenied";
                                    }
                                    else if (response > 0) {
                                        $("#UpdatePaymentsModes").modal("hide");
                                        $("#UpdatePaymentsModesShowLoader").hide();
                                        $("#UpdatePaymentsModesShowButtons").show();
                                        toastr.success("Payment Mode Updated Successfully", "Success");
                                        $('#AdminPaymentModes').DataTable().ajax.reload();
                                    }
                                    else {
                                        $("#UpdateSocialMediaModesShowLoader").hide();
                                        $("#UpdateSocialMediaModesShowButtons").show();
                                        toastr.error("Failed to Upload Image", "Error");
                                    }
                                },
                                error: function (response) {
                                    $("#UpdateSocialMediaModesShowLoader").hide();
                                    $("#UpdateSocialMediaModesShowButtons").show();
                                    toastr.error(response, "Error");
                                }
                            });
                        }
                        else {
                            $("#UpdatePaymentsModes").modal("hide");
                            $("#UpdatePaymentsModesShowLoader").hide();
                            $("#UpdatePaymentsModesShowButtons").show();
                            toastr.success("Payment Mode Updated Successfully", "Success");
                            $('#AdminPaymentModes').DataTable().ajax.reload();
                        }

                    }
                    else if (response == -2) {
                        $("#UpdatePaymentsModesShowLoader").hide();
                        $("#UpdatePaymentsModesShowButtons").show();
                        toastr.warning("Payment Mode Already Exists", "Warning");
                    }
                    else {
                        $("#UpdatePaymentsModesShowLoader").hide();
                        $("#UpdatePaymentsModesShowButtons").show();
                        toastr.error("Payment Mode Not Updated", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdatePaymentsModesShowLoader").hide();
                    $("#UpdatePaymentsModesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //------------------------------------------------Delete Payment Modes -------------------------------------------------
    // On Click Delete Modal Opened
    $("#AdminPaymentModes").on('click', '#DeletePaymentsModesModal', function () {
        var Id = $(this).val();
        var DeletePaymentsModesvalidator = $("#DeletePaymentModes_Submit_Form").validate();
        DeletePaymentsModesvalidator.resetForm();
        ResetPaymentModesIds();
        if (Id != "" && Id != null) {
            $("#DPaymentsModesId").val(Id);
            $("#DeletePaymentsModes").modal('show');
        }
        else {
            toastr.error("Payment Mode Not Exits", "Error");
        }

    });

    //Delete Module Form Submit
    $("#DeletePaymentModes_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#DeletePaymentsModesShowLoader").show();
            $("#DeletePaymentsModesShowButtons").hide();
            $.ajax({
                url: "/AdminPaymentModes/DeletePaymentModes",
                type: "post",
                datatype: "json",
                data: { Id: $("#DPaymentsModesId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#DeletePaymentsModes").modal("hide");
                        $("#DeletePaymentsModesShowLoader").hide();
                        $("#DeletePaymentsModesShowButtons").show();
                        toastr.success("Payment Mode Deleted Successfully", "Success");
                        $('#AdminPaymentModes').DataTable().ajax.reload();
                    }
                    else if (response == -2) {
                        $("#DeletePaymentsModesShowLoader").hide();
                        $("#DeletePaymentsModesShowButtons").show();
                        toastr.warning("Please Delete its Child Data", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#DeletePaymentsModesShowLoader").hide();
                        $("#DeletePaymentsModesShowButtons").show();
                        toastr.error("Payment Mode Not Deleted", "Error");
                    }
                },
                error: function (response) {
                    $("#DeletePaymentsModesShowLoader").hide();
                    $("#DeletePaymentsModesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    //----------------------------------UpLoad Image--------------------------------------------------------


    $("#APMimagefiles").change(function () {
        var File = this.files;
        if (File && File[0]) {
            AReadImage(File[0])
        }

    });

    var AReadImage = function (file) {

        var reader = new FileReader;
        var image = new Image;
        reader.readAsDataURL(file);
        reader.onload = function (_file) {

            image.src = _file.target.result;
            image.onload = function () {

                var height = this.height;
                var width = this.width;
                var type = file.type;
                var size = ~~(file.size / 1024) + "KB";

                $("#APMtargetImg").attr('src', _file.target.result);
            }

        }

    }

    $("#UPMimagefiles").change(function () {
        var File = this.files;
        if (File && File[0]) {
            UReadImage(File[0]);
        }
    });

    var UReadImage = function (file) {

        var reader = new FileReader;
        var image = new Image;
        reader.readAsDataURL(file);
        reader.onload = function (_file) {

            image.src = _file.target.result;
            image.onload = function () {

                var height = this.height;
                var width = this.width;
                var type = file.type;
                var size = ~~(file.size / 1024) + "KB";

                $("#UPMtargetImg").attr('src', _file.target.result);
            }
        }
    }
    //------------------------------------------------Reset-----------------------------------------------------------------
    function ResetPaymentModesIds() {
        $("#APaymentsModesName").val('');
        $("#APaymentsModesDescrption").val('');

        $("#UPaymentsModesId").val('');
        $("#UPaymentsModesName").val('');
        $("#UPaymentsModesDescrption").val('');

        $("#DPaymentsModesId").val('');
    }

});