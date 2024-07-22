$(function () {
    $('#AdminOutSourceAdds').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminOutSourceAdds/LoadOutSourceAdds",
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
            { "data": "imageUrl", "name": "imageUrl", "orderable": false },
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
            { "width": "10%", "targets": 1 },
            { "width": "24%", "targets": 2 },
            { "width": "10%", "targets": 3 },
            { "width": "13%", "targets": 4 },
            { "width": "13%", "targets": 5 },
            {
                "width": "20%",
                targets: 6,
                render: function (data, type, full, meta) {
                    var Id = full.id;
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateOutSourceAddsModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteOutSourceAddsModal">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + ' </td>'
                }
            }
        ],

    });
});
//--------------------------------------------------------------Add Adds----------------------------------------------

//Onclick Add Form Open
$("#AddOutSourceAddsModal").on('click', function () {

    var AddOutSourceAdds = $("#AddOutSourceAdds_Submit_Form").validate();
    AddOutSourceAdds.resetForm();
    ResetOutSourceAddsIds();
    $('#AOSAtargetBanner').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
    $('#AOSAMobtargetBanner').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
    $("#AddOutSourceAdds").modal('show');

});


//Add Adds Form Submit
$("#AddOutSourceAdds_Submit_Form").validate({
    rules: {
        AOSAName: {
            required: true,
            letteronly: true
        },
        AOSABtnName: {
            required: true,
        },
        AOSAWebsite: {
            required: true,
        },
        AOSADescription: {
            required: true,
        }
    },
    messages: {
        AOSAName: {
            required: "Please Enter Name",
        },
        AOSABtnName: {
            required: "Please Enter Btn Name",
        },
        AOSAWebsite: {
            required: "Please Enter Website Name",
        },
        AOSADescription: {
            required: "Please Enter Description",
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
        $("#AddOutSourceAddShowLoader").show();
        $("#AddOutSourceAddShowButtons").hide();
        var Osa = new OutSourceAdvertisment();
        Osa.Name = $("#AOSAName").val();
        Osa.ImageBtn = $("#AOSABtnName").val();
        Osa.ImageUrl = $("#AOSAWebsite").val();
        Osa.Description = $("#AOSADescription").val();
        Osa.IsActive = true;
        $.ajax({
            url: "/AdminOutSourceAdds/AddOutSourceAdds",
            type: "post",
            datatype: "json",
            data: { outSource: Osa },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else if (response > 0) {
                    var NewId = response;
                    var files = $("#AOSAimageBannerfiles").get(0).files;
                    var formData = new FormData();
                    formData.append('file', files[0]);
                    var MobileImage = $("#AOSAMobimageBannerfiles").get(0).files;
                    var MobileData = new FormData();
                    MobileData.append('file', MobileImage[0]);

                    if (files.length > 0) {
                        $.ajax({
                            url: "/AdminOutSourceAdds/UploadOutSourceImage?Id=" + NewId,
                            type: "post",
                            datatype: "json",
                            data: formData,
                            async: false,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                if (response.error == "403") {
                                    location.href = "/Account/AccessDenied";
                                    return false;
                                }
                                else if (response == 1) {
                                }
                                else {
                                    $("#AddOutSourceAddShowLoader").hide();
                                    $("#AddOutSourceAddShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#AddOutSourceAddShowLoader").hide();
                                $("#AddOutSourceAddShowButtons").show();
                                toastr.error(response, "Error");
                            }
                        });
                    }
                    if (MobileImage.length > 0)
                    {
                        $.ajax({
                            url: "/AdminOutSourceAdds/MobUploadOutSourceImage?Id=" + NewId,
                            type: "post",
                            datatype: "json",
                            data: MobileData,
                            async: false,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                if (response.error == "403") {
                                    location.href = "/Account/AccessDenied";
                                    return false;
                                }
                                else if (response == 1) {
                                }
                                else {
                                    $("#AddOutSourceAddShowLoader").hide();
                                    $("#AddOutSourceAddShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#AddOutSourceAddShowLoader").hide();
                                $("#AddOutSourceAddShowButtons").show();
                                toastr.error(response, "Error");
                            }
                        });
                    }
                    $("#AddOutSourceAdds").modal("hide");
                    $("#AddOutSourceAddShowLoader").hide();
                    $("#AddOutSourceAddShowButtons").show();
                    toastr.success("Adds Saved Successfully", "Success");
                    $('#AdminOutSourceAdds').DataTable().ajax.reload();
                }
                else if (response == -2) {
                    $("#AddOutSourceAddShowLoader").hide();
                    $("#AddOutSourceAddShowButtons").show();
                    toastr.error("Adds Already Exists", "Error");
                }
                else {
                    $("#AddOutSourceAddShowLoader").hide();
                    $("#AddOutSourceAddShowButtons").show();
                    toastr.error("Adds Not Saved", "Error");
                }
            },
            error: function (response) {
                $("#AddOutSourceAddShowLoader").hide();
                $("#AddOutSourceAddShowButtons").show();
                toastr.error(response, "Error");
            }

        });
    }
});

//--------------------------------------------------------------Update Adds----------------------------------------------
//Onclick Edit Form Open
$("#AdminOutSourceAdds").on('click', '#UpdateOutSourceAddsModal', function () {
    var Id = $(this).val();
    var UpdateOutSourceAdds = $("#UpdateOutSourceAdds_Submit_Form").validate();
    UpdateOutSourceAdds.resetForm();
    ResetOutSourceAddsIds();
    if (Id != "" && Id != null) {
        $.ajax({
            url: "/AdminOutSourceAdds/GetOutSourceAddsById",
            type: "get",
            datatype: "json",
            data: { Id: Id },
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response != null) {
                    $("#UOSAId").val(response.id);
                    if (response.imagePath == null) {
                        $('#UOSAtargetBanner').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
                    }
                    else {
                        $('#UOSAtargetBanner').attr("src", response.imagePath);
                    }
                    if (response.mobImagePath == null) {
                        $('#UOSAMobtargetBanner').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
                    }
                    else {
                        $('#UOSAMobtargetBanner').attr("src", response.mobImagePath);
                    }
                    if (response.isActive == true) {
                        $("#UOSAIsActive").prop('checked', true);
                    }
                    $("#UOSAName").val(response.name);
                    $("#UOSABtnName").val(response.imageBtn);
                    $("#UOSAWebsite").val(response.imageUrl);
                    $("#UOSADescription").val(response.description);

                    $("#UpdateOutSourceAdds").modal('show');
                }
                else {
                    toastr.error("Adds Not Exits", "Error");
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }
});

//Add Adds Form Submit
$("#UpdateOutSourceAdds_Submit_Form").validate({
    rules: {
        UOSAId: {
            required: true,
        },
        UOSAName: {
            required: true,
            letteronly: true
        },
        UOSABtnName: {
            required: true,
        },
        UOSAWebsite: {
            required: true,
        },
        UOSADescription: {
            required: true,
        }
    },
    messages: {
        UOSAId: {
            required: "Please Provide Passcode",
        },
        UOSAName: {
            required: "Please Enter Name",
        },
        UOSABtnName: {
            required: "Please Enter Btn Name",
        },
        UOSAWebsite: {
            required: "Please Enter Website Name",
        },
        UOSADescription: {
            required: "Please Enter Description",
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
        $("#UpdateOutSourceAddShowLoader").show();
        $("#UpdateOutSourceAddShowButtons").hide();
        var Osa = new OutSourceAdvertisment();
        Osa.Id = $("#UOSAId").val();
        Osa.Name = $("#UOSAName").val();
        Osa.ImageBtn = $("#UOSABtnName").val();
        Osa.ImageUrl = $("#UOSAWebsite").val();
        Osa.Description = $("#UOSADescription").val();
        if ($("#UOSAIsActive").prop('checked') == true) {
            Osa.IsActive = true;
        }
        $.ajax({
            url: "/AdminOutSourceAdds/UpdateOutSourceAdds",
            type: "post",
            datatype: "json",
            data: { outSource: Osa },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else if (response > 0) {
                    var NewId = Osa.Id;
                    var files = $("#UOSAimageBannerfiles").get(0).files;
                    var formData = new FormData();
                    formData.append('file', files[0]);
                    var MobileImage = $("#UOSAMobimageBannerfiles").get(0).files;
                    var MobileData = new FormData();
                    MobileData.append('file', MobileImage[0]);

                    if (files.length > 0) {
                        $.ajax({
                            url: "/AdminOutSourceAdds/UploadOutSourceImage?Id=" + NewId,
                            type: "post",
                            datatype: "json",
                            data: formData,
                            async: false,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                if (response == 1) {
                                    $("#UpdateOutSourceAdds").modal("hide");
                                    $("#UpdateOutSourceAddShowLoader").hide();
                                    $("#UpdateOutSourceAddShowButtons").show();
                                    toastr.success("Adds Updated Successfully", "Success");
                                    $('#AdminOutSourceAdds').DataTable().ajax.reload();
                                }
                                else if (response.error == "403") {
                                    location.href = "/Account/AccessDenied";
                                }
                                else {
                                    $("#UpdateOutSourceAddShowLoader").hide();
                                    $("#UpdateOutSourceAddShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                }
                            },
                            error: function (response) {
                                $("#UpdateOutSourceAddShowLoader").hide();
                                $("#UpdateOutSourceAddShowButtons").show();
                                toastr.error(response, "Error");
                            }
                        });
                    }
                    if (MobileImage.length > 0) {
                        $.ajax({
                            url: "/AdminOutSourceAdds/MobUploadOutSourceImage?Id=" + NewId,
                            type: "post",
                            datatype: "json",
                            data: MobileData,
                            async: false,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                if (response.error == "403") {
                                    location.href = "/Account/AccessDenied";
                                    return false;
                                }
                                else if (response == 1) {
                                }
                                else {
                                    $("#AddOutSourceAddShowLoader").hide();
                                    $("#AddOutSourceAddShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#AddOutSourceAddShowLoader").hide();
                                $("#AddOutSourceAddShowButtons").show();
                                toastr.error(response, "Error");
                            }
                        });
                    }

                    $("#UpdateOutSourceAdds").modal("hide");
                    $("#UpdateOutSourceAddShowLoader").hide();
                    $("#UpdateOutSourceAddShowButtons").show();
                    toastr.success("Adds Updated Successfully", "Success");
                    $('#AdminOutSourceAdds').DataTable().ajax.reload();
                }
                else if (response == -2) {
                    $("#UpdateOutSourceAddShowLoader").hide();
                    $("#UpdateOutSourceAddShowButtons").show();
                    toastr.error("Adds Already Exists", "Error");
                }
                else {
                    $("#UpdateOutSourceAddShowLoader").hide();
                    $("#UpdateOutSourceAddShowButtons").show();
                    toastr.error("Adds Not Updated", "Error");
                }
            },
            error: function (response) {
                $("#UpdateOutSourceAddShowLoader").hide();
                $("#UpdateOutSourceAddShowButtons").show();
                toastr.error(response, "Error");
            }

        });
    }
});

//--------------------------------------------------------------Delete Main Category----------------------------------------------
// On Click Delete Modal Opened
$("#AdminOutSourceAdds").on('click', '#DeleteOutSourceAddsModal', function () {
    var Id = $(this).val();
    var DeleteOutSourceAdds = $("#DeleteOutSourceAdds_Submit_Form").validate();
    DeleteOutSourceAdds.resetForm();
    ResetOutSourceAddsIds();
    if (Id != "" && Id != null) {
        $("#DOSAId").val(Id);
        $("#DeleteOutSourceAdds").modal('show');
    }
    else {
        toastr.error("Adds Not Exits", "Error");
    }
});

$("#DeleteOutSourceAdds_Submit_Form").validate({
    rules: {

    },
    messages: {

    },
    submitHandler: function (form) {
        $("#DeleteOutSourceAddShowLoader").show();
        $("#DeleteOutSourceAddShowButtons").hide();
        $.ajax({
            url: "/AdminOutSourceAdds/DeleteOutSourceAdds",
            type: "post",
            datatype: "json",
            data: { Id: $("#DOSAId").val() },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == 1) {
                    $("#DeleteOutSourceAdds").modal("hide");
                    $("#DeleteOutSourceAddShowLoader").hide();
                    $("#DeleteOutSourceAddShowButtons").show();
                    toastr.success("Adds Deleted Successfully", "Success");
                    $('#AdminOutSourceAdds').DataTable().ajax.reload();
                }
                else if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else {
                    $("#DeleteOutSourceAddShowLoader").hide();
                    $("#DeleteOutSourceAddShowButtons").show();
                    toastr.error("Adds Not Deleted", "Error");
                }
            },
            error: function (response) {
                $("#DeleteOutSourceAddShowLoader").hide();
                $("#DeleteOutSourceAddShowButtons").show();
                toastr.error(response, "Error");
            }

        });
    }
});

//-------------------------------------------------------------Web Banner Image Upload Add and Update---------------------------------

$("#AOSAimageBannerfiles").change(function () {
    var File = this.files;
    if (File && File[0]) {
        AOSABannerReadImage(File[0]);
    }
});

var AOSABannerReadImage = function (file) {

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

            $("#AOSAtargetBanner").attr('src', _file.target.result);
        }

    }
}

$("#UOSAimageBannerfiles").change(function () {
    var File = this.files;
    if (File && File[0]) {
        UOSABannerReadImage(File[0]);
    }
});

var UOSABannerReadImage = function (file) {

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

            $("#UOSAtargetBanner").attr('src', _file.target.result);
        }

    }
}

//-------------------------------------------------------------Mobile Banner Image Upload Add and Update---------------------------------

$("#AOSAMobimageBannerfiles").change(function () {
    var File = this.files;
    if (File && File[0]) {
        AOSAMobBannerReadImage(File[0]);
    }
});

var AOSAMobBannerReadImage = function (file) {

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

            $("#AOSAMobtargetBanner").attr('src', _file.target.result);
        }

    }
}

$("#UOSAMobimageBannerfiles").change(function () {
    var File = this.files;
    if (File && File[0]) {
        UOSAMobBannerReadImage(File[0]);
    }
});

var UOSAMobBannerReadImage = function (file) {

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

            $("#UOSAMobtargetBanner").attr('src', _file.target.result);
        }

    }
}
//------------------------------------------------------------------------Reset--------------------------------------------------------------------------------------

function ResetOutSourceAddsIds() {
    $("#AOSAName").val('');
    $("#AOSABtnName").val('');
    $("#AOSAWebsite").val('');
    $("#AOSADescription").val('');
    $("#AOSAtargetBanner").attr("src", "");
    $('#AOSAimageBannerfiles').val("");
    $("#AOSAMobtargetBanner").attr("src", "");
    $('#AOSAMobimageBannerfiles').val("");

    $("#UOSAId").val('');
    $("#UOSAName").val('');
    $("#UOSABtnName").val('');
    $("#UOSAWebsite").val('');
    $("#UOSADescription").val('');
    $('#UOSAIsActive').val('');
    $("#UOSAtargetBanner").attr("src", "");
    $('#UOSAimageBannerfiles').val("");

    $("#DOSAId").val('');
}