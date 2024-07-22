$(function () {

    $('#AdminSocialMediaModes').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminSocialMediaModes/LoadSocialMediaModes",
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
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateSocialMediaModesModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteSocialMediaModesModal">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + ' </td>'
                }
            }
        ],

    });

    //--------------------------------------------- Add Social Media Modes--------------------------------------------------
    $("#AddSocialMediaModesModal").on('click', function () {
        var AddSocialMediaModesvalidator = $("#AddSocialMediaModes_Submit_Form").validate();
        AddSocialMediaModesvalidator.resetForm();
        ResetSocialMediaModesIds();

        $('#AtargetImg').attr("src", "/app-assets/images/portrait/small/avatar-s-19.jpg");

        $("#AddSocialMediaModes").modal('show');
    });

    //Add Payment Mode Form Submit
    $("#AddSocialMediaModes_Submit_Form").validate({
       
        rules: {
            ASocialMediaModesName: {
                required: true,
                letteronly: true
            },
        },
        messages: {
            ASocialMediaModesName: {
                required: "Please provide a Modes Name."
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
            $("#AddSocialMediaModesShowLoader").show();
            $("#AddSocialMediaModesShowButtons").hide();
            var Smm = new SocialMediaModes();
            Smm.Name = $("#ASocialMediaModesName").val();
            Smm.IsActive = true;
            $.ajax({
                url: "/AdminSocialMediaModes/AddSocialMediaModes",
                type: "post",
                datatype: "json",
                data: { socialMediaModes: Smm },
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
                                url: "/AdminSocialMediaModes/UploadProfileImage?Id=" + response,
                                type: "post",
                                datatype: "json",
                                data: formData,
                                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                                async: false,
                                cache: false,
                                contentType: false,
                                processData: false,
                                success: function (response) {
                                    if (response > 0) {
                                        $("#AddSocialMediaModes").modal("hide");
                                        $("#AddSocialMediaModesShowLoader").hide();
                                        $("#AddSocialMediaModesShowButtons").show();
                                        toastr.success("Social Media Modes Successfully", "Success");
                                        $('#AdminSocialMediaModes').DataTable().ajax.reload();
                                    }
                                    else if (response.error == "403") {
                                        location.href = "/Account/AccessDenied";
                                    }
                                    else {
                                        $("#AddSocialMediaModesShowLoader").hide();
                                        $("#AddSocialMediaModesShowButtons").show();
                                        toastr.error("Failed to Upload Image", "Error");
                                    }
                                },
                                error: function (response) {
                                    $("#AddSocialMediaModesShowLoader").hide();
                                    $("#AddSocialMediaModesShowButtons").show();
                                    toastr.error(response, "Error");
                                }
                            });
                        }
                        else {
                            $("#AddSocialMediaModes").modal("hide");
                            $("#AddSocialMediaModesShowLoader").hide();
                            $("#AddSocialMediaModesShowButtons").show();
                            toastr.success("Social Media Modes Successfully", "Success");
                            $('#AdminSocialMediaModes').DataTable().ajax.reload();
                        }
                    }
                    else if (response == -2) {
                        $("#AddSocialMediaModesShowLoader").hide();
                        $("#AddSocialMediaModesShowButtons").show();
                        toastr.warning("Social Media Modes Already Exists", "Warning");
                    }

                    else {
                        $("#AddSocialMediaModesShowLoader").hide();
                        $("#AddSocialMediaModesShowButtons").show();
                        toastr.error("Social Media Modes Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddSocialMediaModesShowLoader").hide();
                    $("#AddSocialMediaModesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });

        }
    });

    //-----------------------------------------------Update Social Media Modes --------------------------------------------------
    // On Click Update Modal Opened
    $("#AdminSocialMediaModes").on('click', '#UpdateSocialMediaModesModal', function () {
        var Id = $(this).val();
        var UpdateSocialMediaModesvalidator = $("#UpdateSocialMediaModes_Submit_Form").validate();
        UpdateSocialMediaModesvalidator.resetForm();
        ResetSocialMediaModesIds();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminSocialMediaModes/GetSocialMediaModesById",
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
                        $("#USocialMediaModesId").val(response.id);
                        $("#USocialMediaModesName").val(response.name);
                        if (response.isActive == true) {
                            $("#USocialMediaModesIsActive").prop('checked', true);
                        }
                        $("#UpdateSocialMediaModes").modal('show');
                    }
                    else {
                        toastr.error("Social Media Modes Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }

    });

    //Update Payment Mode Form Submit
    $("#UpdateSocialMediaModes_Submit_Form").validate({
        rules: {
            USocialMediaModesId: {
                required: true
            },
            USocialMediaModesName: {
                required: true,
                letteronly: true
            },
        },
        messages: {
            USocialMediaModesId: {
                required: "Please provide a valid Mode Passcode."
            },
            USocialMediaModesName: {
                required: "Please provide a Verification Types Name."
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
         
            $("#UpdateSocialMediaModesShowLoader").show();
            $("#UpdateSocialMediaModesShowButtons").hide();
            var Smm = new SocialMediaModes();
            Smm.Id = $("#USocialMediaModesId").val();
            Smm.Name = $("#USocialMediaModesName").val();
            if ($("#USocialMediaModesIsActive").prop('checked') == true) {
                Smm.IsActive = true;
            }
            $.ajax({
                url: "/AdminSocialMediaModes/EditSocialMediaModes",
                type: "post",
                datatype: "json",
                data: { socialMediaModes: Smm },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response >  0) {
                        var files = $("#Uimagefiles").get(0).files;
                        var formData = new FormData();
                        var Id = $("#USocialMediaModesId").val();
                        formData.append('file', files[0]);
                        if (files.length > 0) {
                            $.ajax({
                                url: "/AdminSocialMediaModes/UploadProfileImage?Id=" + Id,
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
                                    else if (response == 1) {
                                        $("#UpdateSocialMediaModes").modal("hide");
                                        $("#UpdateSocialMediaModesShowLoader").hide();
                                        $("#UpdateSocialMediaModesShowButtons").show();
                                        toastr.success("Social Media Modes Updated Successfully", "Success");
                                        $('#AdminSocialMediaModes').DataTable().ajax.reload();
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
                            $("#UpdateSocialMediaModes").modal("hide");
                            $("#UpdateSocialMediaModesShowLoader").hide();
                            $("#UpdateSocialMediaModesShowButtons").show();
                            toastr.success("Social Media Modes Updated Successfully", "Success");
                            $('#AdminSocialMediaModes').DataTable().ajax.reload();
                        }
                    }
                    else if (response == -2) {
                        $("#UpdateSocialMediaModesShowLoader").hide();
                        $("#UpdateSocialMediaModesShowButtons").show();
                        toastr.warning("Social Media Modes Already Exists", "Warning");
                    }

                    else {
                        $("#UpdateSocialMediaModesShowLoader").hide();
                        $("#UpdateSocialMediaModesShowButtons").show();
                        toastr.error("Social Media Modes Not Updated", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateSocialMediaModesShowLoader").hide();
                    $("#UpdateSocialMediaModesShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });


    //------------------------------------------------Delete Payment Modes -------------------------------------------------
    // On Click Delete Modal Opened
    $("#AdminSocialMediaModes").on('click', '#DeleteSocialMediaModesModal', function () {
        var Id = $(this).val();
        var DeleteSocialMediaModesvalidator = $("#DeleteSocialMediaModes_Submit_Form").validate();
        DeleteSocialMediaModesvalidator.resetForm();
        ResetSocialMediaModesIds();
        if (Id != "" && Id != null) {
            $("#DSocialMediaModesId").val(Id);
            $("#DeleteSocialMediaModes").modal('show');
        }
        else {
            toastr.error("Social Media Modes Not Exits", "Error");
        }

    });

    //Delete Module Form Submit
    $("#DeleteSocialMediaModes_Submit_Form").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function (form) {
            $("#DeleteSocialMediaModesShowLoader").show();
            $("#DeleteSocialMediaModesShowButtons").hide();
            $.ajax({
                url: "/AdminSocialMediaModes/DeleteSocialMediaModes",
                type: "post",
                datatype: "json",
                data: { Id: $("#DSocialMediaModesId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        $("#DeleteSocialMediaModes").modal("hide");
                        $("#DeleteSocialMediaModesShowLoader").hide();
                        $("#DeleteSocialMediaModesShowButtons").show();
                        toastr.success("Social Media Modes Deleted Successfully", "Success");
                        $('#AdminSocialMediaModes').DataTable().ajax.reload();
                    }
                    else if (response == -3) {
                        //debugger;
                        $("#DeleteSocialMediaModes").modal("hide");
                        $("#DeleteSocialMediaModesShowLoader").hide();
                        $("#DeleteSocialMediaModesShowButtons").show();
                        toastr.warning("Please Delete its Child Data", "Warning");
                    }
                    else if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else {
                        $("#DeleteSocialMediaModesShowLoader").hide();
                        $("#DeleteSocialMediaModesShowButtons").show();
                        toastr.error("Social Media Modes Not Deleted", "Error");
                    }
                },
                error: function (response) {
                    $("#DeleteSocialMediaModesShowLoader").hide();
                    $("#DeleteSocialMediaModesShowButtons").show();
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
    function ResetSocialMediaModesIds() {
        $("#AVerificationTypesName").val('');
        $("#AVerificationTypesDescrption").val('');

        $("#UVerificationTypesId").val('');
        $("#UVerificationTypesName").val('');
        $("#UVerificationTypesDescrption").val('');

        $("#DVerificationTypesId").val('');
    }

});
