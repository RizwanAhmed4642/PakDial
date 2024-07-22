$(function () {
    $('#AdminVerificationTypes').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminVerificationTypes/LoadVerificationTypes",
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
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateVerificationTypesModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteVerificationTypesModal">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + ' </td>'
                }
            }
        ],

    });
    //--------------------------------------------- Add Verification Types--------------------------------------------------
    $("#AddVerificationTypesModal").on('click', function () {
        var AddVerificationTypesvalidator = $("#AddVerificationTypes_Submit_Form").validate();
        AddVerificationTypesvalidator.resetForm();
        ResetVerificationTypesIds();
        $('#AtargetImg').attr("src", "/app-assets/images/portrait/small/avatar-s-19.jpg");
        $("#AddVerificationTypes").modal('show');
    });

    //Add Payment Mode Form Submit
    $("#AddVerificationTypes_Submit_Form").validate({
        rules: {
            AVerificationTypesName: {
                required: true,
                letteronly: true
            },
            
        },
        messages: {
            AVerificationTypesName: {
                required: "Please provide a Verification Type Name."
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
           
            $("#AddVerificationTypesShowLoader").show();
            $("#AddVerificationTypesShowButtons").hide();
            var vt = new VerificationTypes();
            vt.Name = $("#AVerificationTypesName").val();
           
            vt.IsActive = true;
            $.ajax({
                url: "/AdminVerificationTypes/AddVerificationTypes",
                type: "post",
                datatype: "json",
                data: { verificationTypes: vt },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response > 0) {
                        var files = $("#Aimagefiles").get(0).files;
                        var formData = new FormData();
                        formData.append('file', files[0]);
                        if (files.length > 0) {
                            $.ajax({
                                url: "/AdminVerificationTypes/UploadProfileImage?Id=" + response,
                                type: "post",
                                datatype: "json",
                                data: formData,
                                async: false,
                                cache: false,
                                contentType: false,
                                processData: false,
                                success: function (response) {
                                    if (response >0) {
                                        $("#AddVerificationTypes").modal("hide");
                                        $("#AddVerificationTypesShowLoader").hide();
                                        $("#AddVerificationTypesShowButtons").show();
                                        toastr.success("Verification Types Successfully", "Success");
                                        $('#AdminVerificationTypes').DataTable().ajax.reload();
                                    }
                                    else if (response.error == "403") {
                                        location.href = "/Account/AccessDenied";
                                    }
                                    else {
                                        $("#AddVerificationTypesShowLoader").hide();
                                        $("#AddVerificationTypesShowButtons").show();
                                        toastr.error("Failed to Upload Image", "Error");
                                    }
                                },
                                error: function (response) {
                                    $("#AddVerificationTypesShowLoader").hide();
                                    $("#AddVerificationTypesShowButtons").show();
                                    toastr.error(response, "Error");
                                }
                            });
                        }
                        else {
                            $("#AddVerificationTypes").modal("hide");
                            $("#AddVerificationTypesShowLoader").hide();
                            $("#AddVerificationTypesShowButtons").show();
                            toastr.success("Verification Types Successfully", "Success");
                            $('#AdminVerificationTypes').DataTable().ajax.reload();
                        }
                    }
                    else if (response == -2) {
                        $("#AddVerificationTypesShowLoader").hide();
                        $("#AddVerificationTypesShowButtons").show();
                        toastr.warning("Verification Types Already Exists", "Warning");
                    }
                    
                    else {
                        $("#AddVerificationTypesShowLoader").hide();
                        $("#AddVerificationTypesShowButtons").show();
                        toastr.error("Verification Types Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddVerificationTypesShowLoader").hide();
                    $("#AddVerificationTypesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });

        }
    });

    //-----------------------------------------------Update Verification Types --------------------------------------------------
    // On Click Update Modal Opened
    $("#AdminVerificationTypes").on('click', '#UpdateVerificationTypesModal', function () {
     
        var Id = $(this).val();
        var UpdateVerificationTypesvalidator = $("#UpdateVerificationTypes_Submit_Form").validate();
        UpdateVerificationTypesvalidator.resetForm();
        ResetVerificationTypesIds();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminVerificationTypes/GetVerificationTypesById",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response != null) {
                        
                        if (response.imageDir == null) {
                            $('#UtargetImg').attr("src", "/app-assets/images/portrait/small/avatar-s-19.jpg");
                        }
                        else {
                            $('#UtargetImg').attr("src", response.imageDir);
                        }
                        $("#UVerificationTypesId").val(response.id);
                        $("#UVerificationTypesName").val(response.name);
                        if (response.isActive == true) {
                            $("#UVerificationTypesIsActive").prop('checked', true);
                        }
                        $("#UpdateVerificationTypes").modal('show');
                    }
                    else {
                        toastr.error("Verification Types Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }

    });

    //Update Payment Mode Form Submit
    $("#UpdateVerificationTypes_Submit_Form").validate({
        rules: {
            UVerificationTypesId: {
                required: true
            },
            UVerificationTypesName: {
                required: true,
                letteronly: true
            },
            UVerificationTypesDescrption: {
                required: true,
                letteronly: true
            }
        },
        messages: {
            UVerificationTypesId: {
                required: "Please provide a valid Verification Types Passcode."
            },
            UVerificationTypesName: {
                required: "Please provide a Verification Types Name."
            },
            UPaymentsModesDescrption: {
                required: "Please provide a Verification Types Description."
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
            $("#UpdateVerificationTypesShowLoader").show();
            $("#UpdateVerificationTypesShowButtons").hide();
            var vt = new VerificationTypes();
            vt.Id = $("#UVerificationTypesId").val();
            vt.Name = $("#UVerificationTypesName").val();
            vt.Description = $("#UVerificationTypesDescrption").val();
            if ($("#UVerificationTypesIsActive").prop('checked') == true) {
                vt.IsActive = true;
            }
            $.ajax({
                url: "/AdminVerificationTypes/EditVerificationTypes",
                type: "post",
                datatype: "json",
                data: { verificationTypes: vt },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response > 1) {
                        var files = $("#Uimagefiles").get(0).files;
                        var formData = new FormData();
                        var Id = $("#UVerificationTypesId").val();
                        formData.append('file', files[0]);
                        if (files.length > 0) {
                            $.ajax({
                                url: "/AdminVerificationTypes/UploadProfileImage?Id=" + Id,
                                type: "post",
                                datatype: "json",
                                data: formData,
                                async: false,
                                cache: false,
                                contentType: false,
                                processData: false,
                                success: function (response) {
                                    if (response == 1) {
                                        $("#UpdateVerificationTypes").modal("hide");
                                        $("#UpdateVerificationTypesShowLoader").hide();
                                        $("#UpdateVerificationTypesShowButtons").show();
                                        toastr.success("Verification Types Updated Successfully", "Success");
                                        $('#AdminVerificationTypes').DataTable().ajax.reload();
                                    }
                                    else if (response.error == "403") {
                                        location.href = "/Account/AccessDenied";
                                    }
                                    else {
                                        $("#UpdateVerificationTypesShowLoader").hide();
                                        $("#UpdateVerificationTypesShowButtons").show();
                                        toastr.error("Failed to Upload Image", "Error");
                                    }
                                },
                                error: function (response) {
                                    $("#UpdateVerificationTypesShowLoader").hide();
                                    $("#UpdateVerificationTypesShowButtons").show();
                                    toastr.error(response, "Error");
                                }
                            });
                        }
                        else {
                            $("#UpdateVerificationTypes").modal("hide");
                            $("#UpdateVerificationTypesShowLoader").hide();
                            $("#UpdateVerificationTypesShowButtons").show();
                            toastr.success("Customer Updated Successfully", "Success");
                            $('#AdminVerificationTypes').DataTable().ajax.reload();
                        }
                    }
                    else if (response == -2) {
                        $("#UpdateVerificationTypesShowLoader").hide();
                        $("#UpdateVerificationTypesShowButtons").show();
                        toastr.warning("Verification Types Already Exists", "Warning");
                    }
                   
                    else {
                        $("#UpdateVerificationTypesShowLoader").hide();
                        $("#UpdateVerificationTypesShowButtons").show();
                        toastr.error("Verification Types Not Updated", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateVerificationTypesShowLoader").hide();
                    $("#UpdateVerificationTypesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //------------------------------------------------Delete Verification Types -------------------------------------------------
    // On Click Delete Modal Opened
    $("#AdminVerificationTypes").on('click', '#DeleteVerificationTypesModal', function () {
        var Id = $(this).val();
        var DeleteVerificationTypesvalidator = $("#DeleteVerificationTypes_Submit_Form").validate();
        DeleteVerificationTypesvalidator.resetForm();
        ResetVerificationTypesIds();
        if (Id != "" && Id != null) {
            $("#DVerificationTypesId").val(Id);
            $("#DeleteVerificationTypes").modal('show');
        }
        else {
            toastr.error("VerificationTypes Not Exits", "Error");
        }

    });

    //Delete Module Form Submit
    $("#DeleteVerificationTypes_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#DeleteVerificationTypesShowLoader").show();
            $("#DeleteVerificationTypesShowButtons").hide();
            $.ajax({
                url: "/AdminVerificationTypes/DeleteVerificationTypes",
                type: "post",
                datatype: "json",
                data: { Id: $("#DVerificationTypesId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                 
                    if (response == 1) {
                        $("#DeleteVerificationTypes").modal("hide");
                        $("#DeleteVerificationTypesShowLoader").hide();
                        $("#DeleteVerificationTypesShowButtons").show();
                        toastr.success("Verification Types Deleted Successfully", "Success");
                        $('#AdminVerificationTypes').DataTable().ajax.reload();
                    }
                    else if (response == -3) {
                        $("#DeleteVerificationTypes").modal("hide");
                        $("#DeleteVerificationTypesShowLoader").hide();
                        $("#DeleteVerificationTypesShowButtons").show();
                        toastr.warning("Please Delete its Child Data", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#DeleteVerificationTypesShowLoader").hide();
                        $("#DeleteVerificationTypesShowButtons").show();
                        toastr.error("Verification Types Not Deleted", "Error");
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


    $("#Aimagefiles").change(function () {
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

                $("#AtargetImg").attr('src', _file.target.result);
            }

        }

    }

    $("#Uimagefiles").change(function () {
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

                $("#UtargetImg").attr('src', _file.target.result);
            }

        }

    }

    //------------------------------------------------Reset-----------------------------------------------------------------
    function ResetVerificationTypesIds() {
        $("#AVerificationTypesName").val('');
        $("#AVerificationTypesDescrption").val('');

        $("#UVerificationTypesId").val('');
        $("#UVerificationTypesName").val('');
        $("#UVerificationTypesDescrption").val('');

        $("#DVerificationTypesId").val('');
    }

});