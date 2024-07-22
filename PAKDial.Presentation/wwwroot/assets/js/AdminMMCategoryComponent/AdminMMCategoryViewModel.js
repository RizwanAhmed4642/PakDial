$(function () {
    $('#AdminMainCategory').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminMMCategory/LoadMainCategory",
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
                "data": "categoryTypeName", "name": "categoryTypeName", "orderable": false,
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
            { "width": "10%", "targets": 2 },
            { "width": "10%", "targets": 3 },
            { "width": "15%", "targets": 4 },
            { "width": "15%", "targets": 5 },
            {
                "width": "20%",
                targets: 6,
                render: function (data, type, full, meta) {
                    var Id = full.id;
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateAdminMainCategory">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteAdminMainCategory">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + ' </td>'
                }
            }
        ],

    });
});


//Onclick Add Form Open
$("#AddMMainCategoryModal").on('click', function () {
    var AddMMainCategoryvalidator = $("#MMainCategory_Submit_Form").validate();
    AddMMainCategoryvalidator.resetForm();
    ResetMainCategoryIds();
    AddBindCategoryType();
    $('#AMCtargetBanner').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
    $('#AMCtargetFeature').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
    $('#AWebIconImagetarget').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
    $('#AMobileIconImagetarget').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
    $("#AddMMainCategory").modal('show');
});

//Call Category Type List on Add
function AddBindCategoryType() {

    var BindCategoryTypeAdd = $("#AMCCategoryTypeId");
    BindCategoryTypeAdd.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
    $.ajax({
        url: "/AdminMMCategory/GetAllCategoryType",
        type: "get",
        datatype: "json",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            BindCategoryTypeAdd.empty().append('<option selected="selected" value="">Please select</option>');
            $.each(response, function (i) {
                BindCategoryTypeAdd.append($("<option></option>").val(this['id']).html(this['name']));
            });
        },
        error: function (response) {
            toastr.error(response, "Error");
        }

    });
}

//Add Main Category Form Submit
$("#MMainCategory_Submit_Form").validate({
    rules: {
        AMCName: {
            required: true,
        },
        AMCDescription: {
            required: true,
        },
        AMCCategoryTypeId: {
            required: true,
            numberonly: true
        },
    },
    messages: {
        AMCName: {
            required: "Please Enter Name."
        },
        AMCDescription: {
            required: "Please Enter Description."
        },
        AMCCategoryTypeId: {
            required: "Please Select Category Type."
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
        $("#AddMMainCategoryShowLoader").show();
        $("#AddMMainCategoryShowButtons").hide();
        var mmc = new MainMenuCategory();
        mmc.Name = $("#AMCName").val();
        mmc.Description = $("#AMCDescription").val();
        mmc.MetaTitle = $("#AMetaTitle").val();
        mmc.MetaKeyword = $("#AMetaKeyword").val();
        mmc.MetaDescription = $("#AMetaDescription").val();
        mmc.CategoryTypeId = $("#AMCCategoryTypeId").val();
        mmc.IsActive = true;
        $.ajax({
            url: "/AdminMMCategory/AddMainCategory",
            type: "post",
            datatype: "json",
            data: { category: mmc },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else if (response > 0) {
                    var NewId = response;
                    var WebIcons = $("#AWebIconImage").get(0).files;
                    var WebData = new FormData();
                    WebData.append('file', WebIcons[0]);
                    var MobileIcons = $("#AMobileIconImage").get(0).files;
                    var MobileData = new FormData();
                    MobileData.append('file', MobileIcons[0]);
                    var banners = $("#AimageBannerfiles").get(0).files;
                    var bannersData = new FormData();
                    bannersData.append('file', banners[0]);
                    var features = $("#AimageFeaturefiles").get(0).files;
                    var featuresData = new FormData();
                    featuresData.append('file', features[0]);
                    if (WebIcons.length > 0) {
                        $.ajax({
                            url: "/AdminMMCategory/UploadWebIcon?Id=" + NewId,
                            type: "post",
                            datatype: "json",
                            data: WebData,
                            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
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
                                    $("#AddMMainCategoryShowLoader").hide();
                                    $("#AddMMainCategoryShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#AddMMainCategoryShowLoader").hide();
                                $("#AddMMainCategoryShowButtons").show();
                                toastr.error(response, "Error");
                                return false;
                            }
                        });
                    }
                    if (MobileIcons.length > 0) {
                        $.ajax({
                            url: "/AdminMMCategory/UploadMobileIcon?Id=" + NewId,
                            type: "post",
                            datatype: "json",
                            data: MobileData,
                            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
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
                                    $("#AddMMainCategoryShowLoader").hide();
                                    $("#AddMMainCategoryShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#AddMMainCategoryShowLoader").hide();
                                $("#AddMMainCategoryShowButtons").show();
                                toastr.error(response, "Error");
                                return false;
                            }
                        });
                    }
                    if (banners.length > 0) {
                        $.ajax({
                            url: "/AdminMMCategory/UploadCategoryBannerImage?Id=" + NewId,
                            type: "post",
                            datatype: "json",
                            data: bannersData,
                            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
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
                                    $("#AddMMainCategoryShowLoader").hide();
                                    $("#AddMMainCategoryShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#AddMMainCategoryShowLoader").hide();
                                $("#AddMMainCategoryShowButtons").show();
                                toastr.error(response, "Error");
                                return false;
                            }
                        });
                    }
                    if (features.length > 0)
                    {
                        $.ajax({
                            url: "/AdminMMCategory/UploadCategoryFeatureImage?Id=" + NewId,
                            type: "post",
                            datatype: "json",
                            data: featuresData,
                            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
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
                                    $("#AddMMainCategoryShowLoader").hide();
                                    $("#AddMMainCategoryShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#AddMMainCategoryShowLoader").hide();
                                $("#AddMMainCategoryShowButtons").show();
                                toastr.error(response, "Error");
                                return false;
                            }
                        });
                    }                    
                    $("#AddMMainCategory").modal("hide");
                    $("#AddMMainCategoryShowLoader").hide();
                    $("#AddMMainCategoryShowButtons").show();
                    toastr.success("Main Category Saved Successfully", "Success");
                    $('#AdminMainCategory').DataTable().ajax.reload();
                }
                else if (response == -2) {
                    $("#AddMMainCategoryShowLoader").hide();
                    $("#AddMMainCategoryShowButtons").show();
                    toastr.error("Main Category Already Exists", "Error");
                }
                else {
                    $("#AddMMainCategoryShowLoader").hide();
                    $("#AddMMainCategoryShowButtons").show();
                    toastr.error("Main Category Not Saved", "Error");
                }
            },
            error: function (response) {
                $("#AddMMainCategoryShowLoader").hide();
                $("#AddMMainCategoryShowButtons").show();
                toastr.error(response, "Error");
            }

        });
    }
});


//--------------------------------------------------------------Update Main Category----------------------------------------------

//Call Category Type List on Update
function UpdateBindCategoryType() {
    var BindCategoryTypeUpdate = $("#UMCCategoryTypeId");
    BindCategoryTypeUpdate.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
    $.ajax({
        url: "/AdminMMCategory/GetAllCategoryType",
        type: "get",
        datatype: "json",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            BindCategoryTypeUpdate.empty().append('<option selected="selected" value="">Please select</option>');
            $.each(response, function (i) {
                BindCategoryTypeUpdate.append($("<option></option>").val(this['id']).html(this['name']));
            });
        },
        error: function (response) {
            toastr.error(response, "Error");
        }

    });
}

//Onclick Edit Form Open
$("#AdminMainCategory").on('click', '#UpdateAdminMainCategory', function () {
    var Id = $(this).val();
    var UpdateMMainCategoryvalidator = $("#UMMainCategory_Submit_Form").validate();
    UpdateMMainCategoryvalidator.resetForm();
    ResetMainCategoryIds();
    if (Id != "" && Id != null) {
        $.ajax({
            url: "/AdminMMCategory/FindMainCategory",
            type: "get",
            datatype: "json",
            data: { Id: Id },
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response != null) {
                    UpdateBindCategoryType();
                    $("#UMCId").val(response.id);
                    $("#UMCCategoryTypeId").find('option[value="' + response.categoryTypeId + '"]').attr('selected', 'selected');

                    if (response.webDir == null) {
                        $('#UWebIconImagetarget').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
                    }
                    else {
                        $('#UWebIconImagetarget').attr("src", response.webDir);
                    }
                    if (response.mobileDir == null) {
                        $('#UMobileIconImagetarget').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
                    }
                    else {
                        $('#UMobileIconImagetarget').attr("src", response.mobileDir);
                    }

                    if (response.catBannerImage == null) {
                        $('#UMCtargetBanner').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
                    }
                    else {
                        $('#UMCtargetBanner').attr("src", response.catBannerImage);
                    }
                    if (response.catFeatureImage == null) {
                        $('#UMCtargetFeature').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
                    }
                    else {
                        $('#UMCtargetFeature').attr("src", response.catFeatureImage);
                    }
                    if (response.isActive == true) {
                        $("#UMCIsActive").prop('checked', true);
                    }
                    else {
                        $("#UMCIsActive").prop('checked', false);
                    }

                    $("#UMCName").val(response.name);
                    $("#UMCDescription").val(response.description);
                    $("#UMetaTitle").val(response.metaTitle);
                    $("#UMetaKeyword").val(response.metaKeyword);
                    $("#UMetaDescription").val(response.metaDescription);
                    $("#UpdateMMainCategory").modal('show');
                }
                else {
                    toastr.error("Main Category Not Exits", "Error");
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }
});


$("#UMMainCategory_Submit_Form").validate({
    rules: {
        UMCId: {
            required: true,
        },
        UMCName: {
            required: true,
        },
        UMCIcon: {
            required: true,
        },
        UMCDescription: {
            required: true,
        },
        UMCCategoryTypeId: {
            required: true,
            numberonly: true
        },
    },
    messages: {
        UMCId: {
            required: "Please provide a valid Category Passcode."
        },
        UMCName: {
            required: "Please Enter Name."
        },
        UMCIcon: {
            required: "Please Enter Icon Class."
        },
        UMCDescription: {
            required: "Please Enter Description."
        },
        UMCCategoryTypeId: {
            required: "Please Select Category Type."
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
        $("#UpdateMMainCategoryShowLoader").show();
        $("#UpdateMMainCategoryShowButtons").hide();
        var mmc = new MainMenuCategory();
        mmc.Id = $("#UMCId").val();
        mmc.Name = $("#UMCName").val();
        mmc.MetaTitle = $("#UMetaTitle").val();
        mmc.MetaKeyword = $("#UMetaKeyword").val();
        mmc.MetaDescription = $("#UMetaDescription").val();
        mmc.Description = $("#UMCDescription").val();
        mmc.CategoryTypeId = $("#UMCCategoryTypeId").val();

        if ($("#UMCIsActive").prop('checked') == true) {
            mmc.IsActive = true;
        }
        $.ajax({
            url: "/AdminMMCategory/UpdateMainCategory",
            type: "post",
            datatype: "json",
            data: { category: mmc },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
       
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else if (response > 0) {
                    var NewId = mmc.Id;
                    var WebIcons = $("#UWebIconImage").get(0).files;
                    var WebData = new FormData();
                    WebData.append('file', WebIcons[0]);
                    var MobileIcons = $("#UMobileIconImage").get(0).files;
                    var MobileData = new FormData();
                    MobileData.append('file', MobileIcons[0]);

                    var banners = $("#UimageBannerfiles").get(0).files;
                    var bannersData = new FormData();
                    bannersData.append('file', banners[0]);
                    var features = $("#UimageFeaturefiles").get(0).files;
                    var featuresData = new FormData();
                    featuresData.append('file', features[0]);

                    if (WebIcons.length > 0) {
                        $.ajax({
                            url: "/AdminMMCategory/UploadWebIcon?Id=" + NewId,
                            type: "post",
                            datatype: "json",
                            data: WebData,
                            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
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
                                    $("#UpdateMMainCategoryShowLoader").hide();
                                    $("#UpdateMMainCategoryShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#UpdateMMainCategoryShowLoader").hide();
                                $("#UpdateMMainCategoryShowButtons").show();
                                toastr.error("", "Error");
                                return false;
                            }
                        });
                    }
                    if (MobileIcons.length > 0) {
                        $.ajax({
                            url: "/AdminMMCategory/UploadMobileIcon?Id=" + NewId,
                            type: "post",
                            datatype: "json",
                            data: MobileData,
                            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
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
                                    $("#UpdateMMainCategoryShowLoader").hide();
                                    $("#UpdateMMainCategoryShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#UpdateMMainCategoryShowLoader").hide();
                                $("#UpdateMMainCategoryShowButtons").show();
                                toastr.error(response, "Error");
                                return false;
                            }
                        });
                    }
                    if (banners.length > 0) {
                        $.ajax({
                            url: "/AdminMMCategory/UploadCategoryBannerImage?Id=" + NewId,
                            type: "post",
                            datatype: "json",
                            data: bannersData,
                            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
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
                                    $("#UpdateMMainCategoryShowLoader").hide();
                                    $("#UpdateMMainCategoryShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#UpdateMMainCategoryShowLoader").hide();
                                $("#UpdateMMainCategoryShowButtons").show();
                                toastr.error(response, "Error");
                                return false;
                            }
                        });
                    }
                    if (features.length > 0) {
                        $.ajax({
                            url: "/AdminMMCategory/UploadCategoryFeatureImage?Id=" + NewId,
                            type: "post",
                            datatype: "json",
                            data: featuresData,
                            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
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
                                    $("#UpdateMMainCategoryShowLoader").hide();
                                    $("#UpdateMMainCategoryShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#UpdateMMainCategoryShowLoader").hide();
                                $("#UpdateMMainCategoryShowButtons").show();
                                toastr.error(response, "Error");
                                return false;
                            }
                        });
                    }

                    $("#UpdateMMainCategory").modal("hide");
                    $("#UpdateMMainCategoryShowLoader").hide();
                    $("#UpdateMMainCategoryShowButtons").show();
                    toastr.success("Main Category Updated Successfully", "Success");
                    $('#AdminMainCategory').DataTable().ajax.reload();
                }
                else if (response == -2) {
                    $("#UpdateMMainCategoryShowLoader").hide();
                    $("#UpdateMMainCategoryShowButtons").show();
                    toastr.error("Main Category Already Exists", "Error");
                }
                else {
                    $("#UpdateMMainCategoryShowLoader").hide();
                    $("#UpdateMMainCategoryShowButtons").show();
                    toastr.error("Main Category Not Updated", "Error");
                }
            },
            error: function (response) {
                $("#UpdateMMainCategoryShowLoader").hide();
                $("#UpdateMMainCategoryShowButtons").show();
                toastr.error(response, "Error");
            }

        });
    }
});

//--------------------------------------------------------------Delete Main Category----------------------------------------------
// On Click Delete Modal Opened
$("#AdminMainCategory").on('click', '#DeleteAdminMainCategory', function () {
    var Id = $(this).val();
    var DeleteMMainCategoryvalidator = $("#DeleteMMainCategory_Submit_Form").validate();
    DeleteMMainCategoryvalidator.resetForm();
    ResetMainCategoryIds();
    if (Id != "" && Id != null) {
        $("#DMCId").val(Id);
        $("#DeleteMMainCategory").modal('show');
    }
    else {
        toastr.error("Main Category Not Exits", "Error");
    }
});

$("#DeleteMMainCategory_Submit_Form").validate({
    rules: {

    },
    messages: {

    },
    submitHandler: function (form) {
        $("#DeleteMMainCategoryShowLoader").show();
        $("#DeleteMMainCategoryShowButtons").hide();
        $.ajax({
            url: "/AdminMMCategory/DeleteMainCategory",
            type: "post",
            datatype: "json",
            data: { Id: $("#DMCId").val() },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == 1) {
                    $("#DeleteMMainCategory").modal("hide");
                    $("#DeleteMMainCategoryShowLoader").hide();
                    $("#DeleteMMainCategoryShowButtons").show();
                    toastr.success("Main Category Deleted Successfully", "Success");
                    $('#AdminMainCategory').DataTable().ajax.reload();
                }
                else if (response == 2) {
                    $("#DeleteDesignationShowLoader").hide();
                    $("#DeleteDesignationShowButtons").show();
                    toastr.warning("Please Delete its Sub Category First", "Warning");
                }
                else if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else {
                    $("#DeleteMMainCategoryShowLoader").hide();
                    $("#DeleteMMainCategoryShowButtons").show();
                    toastr.error("Main Category Not Deleted", "Error");
                }
            },
            error: function (response) {
                $("#DeleteMMainCategoryShowLoader").hide();
                $("#DeleteMMainCategoryShowButtons").show();
                toastr.error(response, "Error");
            }

        });
    }
});

//-------------------------------------------------------------Banner Image Upload Add and Update---------------------------------

$("#AimageBannerfiles").change(function () {
    var File = this.files;
    if (File && File[0]) {
        AMMCBannerReadImage(File[0]);
    }
});

var AMMCBannerReadImage = function (file) {

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

            $("#AMCtargetBanner").attr('src', _file.target.result);
        }

    }
}

$("#UimageBannerfiles").change(function () {
    var File = this.files;
    if (File && File[0]) {
        UMMCBannerReadImage(File[0]);
    }
});

var UMMCBannerReadImage = function (file) {

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

            $("#UMCtargetBanner").attr('src', _file.target.result);
        }

    }
}


//-------------------------------------------------------------Feature Image Upload Add and Update---------------------------------

$("#AimageFeaturefiles").change(function () {
    var File = this.files;
    if (File && File[0]) {
        AMMCFeatureReadImage(File[0]);
    }
});

var AMMCFeatureReadImage = function (file) {

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

            $("#AMCtargetFeature").attr('src', _file.target.result);
        }
    }
}

$("#UimageFeaturefiles").change(function () {
    var File = this.files;
    if (File && File[0]) {
        UMMCFeatureReadImage(File[0]);
    }
});

var UMMCFeatureReadImage = function (file) {

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

            $("#UMCtargetFeature").attr('src', _file.target.result);
        }
    }
}

//-------------------------------------------------------------Web Icon Upload Add and Update---------------------------------

$("#AWebIconImage").change(function () {
    var File = this.files;
    if (File && File[0]) {
        AWebIconReadImage(File[0]);
    }
});

var AWebIconReadImage = function (file) {

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

            $("#AWebIconImagetarget").attr('src', _file.target.result);
        }

    }
}

$("#UWebIconImage").change(function () {
    var File = this.files;
    if (File && File[0]) {
        UWebIconReadImage(File[0]);
    }
});

var UWebIconReadImage = function (file) {

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

            $("#UWebIconImagetarget").attr('src', _file.target.result);
        }

    }
}

//-------------------------------------------------------------Mobile Icon Upload Add and Update---------------------------------

$("#AMobileIconImage").change(function () {
    var File = this.files;
    if (File && File[0]) {
        AMobileIconReadImage(File[0]);
    }
});

var AMobileIconReadImage = function (file) {

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

            $("#AMobileIconImagetarget").attr('src', _file.target.result);
        }

    }
}

$("#UMobileIconImage").change(function () {
    var File = this.files;
    if (File && File[0]) {
        UMobileIconReadImage(File[0]);
    }
});

var UMobileIconReadImage = function (file) {

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

            $("#UMobileIconImagetarget").attr('src', _file.target.result);
        }

    }
}

//------------------------------------------------------------------------Reset--------------------------------------------------------------------------------------

function ResetMainCategoryIds() {
    $("#AMCName").val('');
    $("#AMCDescription").val('');
    $("#AMetaTitle").val('');
    $("#AMetaKeyword").val('');
    $("#AMetaDescription").val('');

    $("#AMCtargetBanner").attr("src", "");
    $('#AMCtargetFeature').attr("src", "");
    $("#AimageBannerfiles").val("");
    $('#AimageFeaturefiles').val("");

    $("#AWebIconImagetarget").attr("src", "");
    $('#AMobileIconImagetarget').attr("src", "");
    $("#AWebIconImage").val("");
    $('#AMobileIconImage').val("");

    $("#AMCCategoryTypeId").val('');


    $("#UMCId").val('');
    $("#UMCName").val('');
    $("#UMCDescription").val('');
    $("#UMetaTitle").val('');
    $("#UMetaKeyword").val('');
    $("#UMetaDescription").val('');
    $("#UMCtargetBanner").attr("src", "");
    $('#UMCtargetFeature').attr("src", "");
    $("#UimageBannerfiles").val("");
    $('#UimageFeaturefiles').val("");

    $("#UWebIconImagetarget").attr("src", "");
    $('#UMobileIconImagetarget').attr("src", "");
    $("#UWebIconImage").val("");
    $('#UMobileIconImage').val("");

    $("#UMCCategoryTypeId").val('');


    $("#DMCId").val('');
}