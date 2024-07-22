$(function () {
    $('#AdminSubCategory').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminSubMenuCategory/LoadSubCategory",
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
            { "data": "trackNames", "name": "trackNames", "orderable": false },
            //{ "data": "parentCategoryName", "name": "parentCategoryName", "orderable": false },
            { "data": "mainCategoryName", "name": "mainCategoryName", "orderable": false },
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
                "data": "isLastNode", "name": "isLastNode", "orderable": false,
                "render": function (data) {
                    if (data == true) {
                        return '<div class="badge badge-primary">Yes</div>';
                    }
                    else {
                        return '<div class="badge badge-danger">No</div>';
                    }
                }
            },
            { "data": "categoryTypeName", "name": "categoryTypeName", "orderable": false },
            //{
            //    "data": "updatedDate", "name": "updatedDate", "orderable": false,
            //    "render": function (data) {
            //        var date = new Date(data);
            //        var month = date.getMonth() + 1;
            //        return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
            //    }
            //},

        ],
        columnDefs: [
            { "width": "10%", "targets": 0 },
            { "width": "30%", "targets": 1 },
            { "width": "10%", "targets": 2 },
            { "width": "8%", "targets": 3 },
            { "width": "7%", "targets": 4 },
            { "width": "10%", "targets": 5 },
            {
                "width": "25%",
                targets: 6,
                render: function (data, type, full, meta) {
                
                    var Id = full.id;
                    var IsPopular = "";
                    if (full.isPopular == null || full.isPopular == false) {
                        IsPopular = '<button type="button" class="btn mr-1 mb-1 btn-secondary btn-sm" value=' + Id + ' id="NotPopularSubMenuModal">NotPopular</button>'
                    }
                    else {
                        IsPopular = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="IsPopularSubMenuModal">IsPopular</button>'
                    }
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateAdminSubCategory">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteAdminSubCategory">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + "" + IsPopular +'</td>'
                }
            }
        ],

    });
});


//--------------------------------------------- Popular and NotPopular SubCategory------------------------------------------------
// On Click Modal Opened and Verify
$("#AdminSubCategory").on('click', '#NotPopularSubMenuModal', function () {
    var Id = $(this).val();
    if (Id != "" && Id != null) {
        $.ajax({
            url: "/AdminSubMenuCategory/IsPopularCategory",
            type: "get",
            datatype: "json",
            async: false,
            data: { Id: Id, name: "Yes" },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else if (response == 1) {
                    toastr.success("Category Popular Successfully", "Success");
                    $('#AdminSubCategory').DataTable().ajax.reload();
                }
                else {
                    toastr.error("Category Not Popular Successfully", "Error");
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }

});

// On Click Modal Opened and UnVerify
$("#AdminSubCategory").on('click', '#IsPopularSubMenuModal', function () {
    var Id = $(this).val();
    if (Id != "" && Id != null) {
        $.ajax({
            url: "/AdminSubMenuCategory/IsPopularCategory",
            type: "get",
            datatype: "json",
            async: false,
            data: { Id: Id, name: "No" },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else if (response == 1) {
                    toastr.success("Category UnPopular Successfully", "Success");
                    $('#AdminSubCategory').DataTable().ajax.reload();

                }
                else {
                    toastr.error("Category Not UnPopular Successfully", "Error");
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }

});

//Onclick Add Form Open
$("#AddSubCategoryModal").on('click', function () {
    var AddSubCategoryvalidator = $("#AddSubCategory_Submit_Form").validate();
    AddSubCategoryvalidator.resetForm();
    ResetSubCategoryIds();
    AddBindSMainCategory();
    $('#ASBStargetWeb').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
    $('#ASBStargetMobile').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
    $('#ASBStargetBanner').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
    $('#ASBStargetFeature').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
    $("#AddSubCategory").modal('show');
});

//Add Sub Category Form Submit
$("#AddSubCategory_Submit_Form").validate({
    rules: {
        ASBMCategoryId: {
            required: true,
            numberonly: true,
        },
        ASBSName: {
            required: true,
        },
        ASBSIcon: {
            required: true,
        },
        ASBSDescription: {
            required: true,
        }
    },
    messages: {
        ASBMCategoryId: {
            required: "Please Select Main Category."
        },
        ASBSName: {
            required: "Please Enter Name."
        },
        ASBSIcon: {
            required: "Please Enter Icon Class"
        },
        ASBSDescription: {
            required: "Please Enter Description."
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
        $("#AddSubCategoryShowLoader").show();
        $("#AddSubCategoryShowButtons").hide();
        var smc = new SubMenuCategory();
        smc.Name = $("#ASBSName").val();
        smc.Icon = $("#ASBSIcon").val();
        smc.Description = $("#ASBSDescription").val();
        smc.MainCategoryId = $("#ASBMCategoryId").val();
        smc.TrackIds = $("#ASBSCategoryId").val();
        smc.TrackNames = $("#ASBSCategoryId").text();
        smc.MetaTitle = $("#ASBSMetaTitle").val();
        smc.MetaKeyword = $("#ASBSMetaKeyword").val();
        smc.MetaDescription = $("#ASBSMetaDescription").val();
        smc.IsActive = true;

        if ($("#ASBIsLastNode").prop('checked') == true) {
            smc.IsLastNode = true;
        }
        $.ajax({
            url: "/AdminSubMenuCategory/AddSubCategory",
            type: "post",
            datatype: "json",
            data: { category: smc },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else if (response > 0) {
                    var NewId = response;
                    var WebIcons = $("#ASBSimageWeb").get(0).files;
                    var WebData = new FormData();
                    WebData.append('file', WebIcons[0]);
                    var MobileIcons = $("#ASBSimageMobile").get(0).files;
                    var MobileData = new FormData();
                    MobileData.append('file', MobileIcons[0]);
                    var banners = $("#ASBSimageBannerfiles").get(0).files;
                    var bannersData = new FormData();
                    bannersData.append('file', banners[0]);
                    var features = $("#ASBSimageFeaturefiles").get(0).files;
                    var featuresData = new FormData();
                    featuresData.append('file', features[0]);
                    if (banners.length > 0) {
                        $.ajax({
                            url: "/AdminSubMenuCategory/UploadSubCategoryBannerImage?Id=" + NewId,
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
                                    $("#AddSubCategoryShowLoader").hide();
                                    $("#AddSubCategoryShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#AddSubCategoryShowLoader").hide();
                                $("#AddSubCategoryShowButtons").show();
                                toastr.error(response, "Error");
                                return false;
                            }
                        });
                    }
                    if (features.length > 0) {
                        $.ajax({
                            url: "/AdminSubMenuCategory/UploadSubCategoryFeatureImage?Id=" + NewId,
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
                                    $("#AddSubCategoryShowLoader").hide();
                                    $("#AddSubCategoryShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#AddSubCategoryShowLoader").hide();
                                $("#AddSubCategoryShowButtons").show();
                                toastr.error(response, "Error");
                                return false;
                            }
                        });
                    }
                    if (WebIcons.length > 0) {
                        $.ajax({
                            url: "/AdminSubMenuCategory/UploadWebIcon?Id=" + NewId,
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
                                    $("#AddSubCategoryShowLoader").hide();
                                    $("#AddSubCategoryShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#AddSubCategoryShowLoader").hide();
                                $("#AddSubCategoryShowButtons").show();
                                toastr.error(response, "Error");
                                return false;
                            }
                        });
                    }
                    if (MobileIcons.length > 0) {
                        $.ajax({
                            url: "/AdminSubMenuCategory/UploadMobileIcon?Id=" + NewId,
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
                                    $("#AddSubCategoryShowLoader").hide();
                                    $("#AddSubCategoryShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#AddSubCategoryShowLoader").hide();
                                $("#AddSubCategoryShowButtons").show();
                                toastr.error(response, "Error");
                                return false;
                            }
                        });
                    }
                    $("#AddSubCategory").modal("hide");
                    $("#AddSubCategoryShowLoader").hide();
                    $("#AddSubCategoryShowButtons").show();
                    toastr.success("Sub Category Saved Successfully", "Success");
                    $('#AdminSubCategory').DataTable().ajax.reload();
                }
                else if (response == -2) {
                    $("#AddSubCategoryShowLoader").hide();
                    $("#AddSubCategoryShowButtons").show();
                    toastr.error("Sub Category Already Exists", "Error");
                }
                else {
                    $("#AddSubCategoryShowLoader").hide();
                    $("#AddSubCategoryShowButtons").show();
                    toastr.error("Sub Category Not Saved", "Error");
                }
            },
            error: function (response) {
                $("#AddSubCategoryShowLoader").hide();
                $("#AddSubCategoryShowButtons").show();
                toastr.error(response, "Error");
            }

        });
    }
});

//-----------------------------------------------------------Update Sub Category-------------------------------------------------------

//Onclick Update Form Open
$("#AdminSubCategory").on('click', '#UpdateAdminSubCategory', function () {
    var Id = $(this).val();
    var UpdateSubCategoryvalidator = $("#UpdateSubCategory_Submit_Form").validate();
    UpdateSubCategoryvalidator.resetForm();
    ResetSubCategoryIds();
    UpdateBindSMainCategory();
    if (Id != "" && Id != null) {
        $.ajax({
            url: "/AdminSubMenuCategory/FindSubCategory",
            type: "get",
            datatype: "json",
            data: { Id: Id },
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response != null) {
                    $("#USBSubCategoryId").val(response.id);
                    if (response.parentSubCategoryId > 0) {
                        $.ajax({
                            url: "/AdminSubMenuCategory/LoadEditSubCategory",
                            type: "get",
                            datatype: "json",
                            data: { CatId: response.parentSubCategoryId },
                            async: false,
                            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                            success: function (responses) {
                                // Create a DOM Option and pre-select by default~
                                var newOption = new Option(responses.text, responses.id, true, true);
                                // Append it to the select
                                $('#USBSCategoryId').append(newOption).trigger('change');

                            },
                            error: function (response) {
                                toastr.error(response, "Error");
                            }

                        });

                    }
                    $("#USBMCategoryId").val(response.mainCategoryId);
                    $("#USBSMetaTitle").val(response.metaTitle);
                    $("#USBSMetaKeyword").val(response.metaKeyword);
                    $("#USBSMetaDescription").val(response.metaDescription);
                    if (response.webDir == null) {
                        $('#USBStargetWeb').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
                    }
                    else {
                        $('#USBStargetWeb').attr("src", response.webDir);
                    }
                    if (response.mobileDir == null) {
                        $('#USBStargetMobile').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
                    }
                    else {
                        $('#USBStargetMobile').attr("src", response.mobileDir);
                    }

                    if (response.subBannerImage == null) {
                        $('#USBStargetBanner').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
                    }
                    else {
                        $('#USBStargetBanner').attr("src", response.subBannerImage);
                    }
                    if (response.subFeatureImage == null) {
                        $('#USBStargetFeature').attr("src", "/app-assets/images/portrait/small/avatar-s-191.jpg");
                    }
                    else {
                        $('#USBStargetFeature').attr("src", response.subFeatureImage);
                    }
                    if (response.isActive == true) {
                        $("#USBIsActive").prop('checked', true);
                    }
                    else {
                        $("#USBIsActive").prop('checked', false);

                    }
                    if (response.isLastNode == true) {
                        $("#USBIsLastNode").prop('checked', true);
                    }
                    else {
                        $("#USBIsLastNode").prop('checked', false);
                    }
                    $("#USBSName").val(response.name);
                    $("#USBSDescription").val(response.description);
                    $("#USBSIcon").val(response.icon);
                    $("#UpdateSubCategory").modal('show');
                }
                else {
                    toastr.error("Sub Category Not Exits", "Error");
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }
});

//Update Sub Category Form Submit
$("#UpdateSubCategory_Submit_Form").validate({
    rules: {
        USBSubCategoryId: {
            required: true,
        },
        USBMCategoryId: {
            required: true,
        },
        USBSName: {
            required: true,
        },
        USBSIcon: {
            required: true,
        },
        USBSDescription: {
            required: true,
        }
    },
    messages: {
        USBSubCategoryId: {
            required: "Please Enter Passcode."
        },
        USBMCategoryId: {
            required: "Please Select Main Category."
        },
        USBSName: {
            required: "Please Enter Name."
        },
        USBSIcon: {
            required: "Please Enter Icon Class"
        },
        USBSDescription: {
            required: "Please Enter Description."
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
        $("#UpdateSubCategoryShowLoader").show();
        $("#UpdateSubCategoryShowButtons").hide();
        var smc = new SubMenuCategory();
        smc.Id = $("#USBSubCategoryId").val();
        smc.Name = $("#USBSName").val();
        smc.Icon = $("#USBSIcon").val();
        smc.Description = $("#USBSDescription").val();
        smc.MainCategoryId = $("#USBMCategoryId").val();
        //smc.ParentSubCategoryId = $("#USBSCategoryId").val();
        smc.TrackIds = $("#USBSCategoryId").val();
        smc.TrackNames = $("#USBSCategoryId").text();
        smc.MetaTitle = $("#USBSMetaTitle").val();
        smc.MetaKeyword = $("#USBSMetaKeyword").val();
        smc.MetaDescription = $("#USBSMetaDescription").val();
        if ($("#USBIsActive").prop('checked') == true) {
            smc.IsActive = true;
        }
        if ($("#USBIsLastNode").prop('checked') == true) {
            smc.IsLastNode = true;
        }
        $.ajax({
            url: "/AdminSubMenuCategory/UpdateSubCategory",
            type: "post",
            datatype: "json",
            data: { category: smc },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else if (response > 0) {
                    var NewId = smc.Id;
                    var WebIcons = $("#USBSimageWeb").get(0).files;
                    var WebData = new FormData();
                    WebData.append('file', WebIcons[0]);
                    var MobileIcons = $("#USBSimageMobile").get(0).files;
                    var MobileData = new FormData();
                    MobileData.append('file', MobileIcons[0]);
                    var banners = $("#USBSimageBannerfiles").get(0).files;
                    var bannersData = new FormData();
                    bannersData.append('file', banners[0]);
                    var features = $("#USBSimageFeaturefiles").get(0).files;
                    var featuresData = new FormData();
                    featuresData.append('file', features[0]);
                    if (banners.length > 0) {
                        $.ajax({
                            url: "/AdminSubMenuCategory/UploadSubCategoryBannerImage?Id=" + NewId,
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
                                    $("#UpdateSubCategoryShowLoader").hide();
                                    $("#UpdateSubCategoryShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#UpdateSubCategoryShowLoader").hide();
                                $("#UpdateSubCategoryShowButtons").show();
                                toastr.error(response, "Error");
                                return false;
                            }
                        });
                    }
                    if (features.length > 0) {
                        $.ajax({
                            url: "/AdminSubMenuCategory/UploadSubCategoryFeatureImage?Id=" + NewId,
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
                                    $("#UpdateSubCategoryShowLoader").hide();
                                    $("#UpdateSubCategoryShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#UpdateSubCategoryShowLoader").hide();
                                $("#UpdateSubCategoryShowButtons").show();
                                toastr.error(response, "Error");
                                return false;
                            }
                        });
                    }
                    if (WebIcons.length > 0) {
                        $.ajax({
                            url: "/AdminSubMenuCategory/UploadWebIcon?Id=" + NewId,
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
                                    $("#UpdateSubCategoryShowLoader").hide();
                                    $("#UpdateSubCategoryShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#UpdateSubCategoryShowLoader").hide();
                                $("#UpdateSubCategoryShowButtons").show();
                                toastr.error(response, "Error");
                                return false;
                            }
                        });
                    }
                    if (MobileIcons.length > 0) {
                        $.ajax({
                            url: "/AdminSubMenuCategory/UploadMobileIcon?Id=" + NewId,
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
                                    $("#UpdateSubCategoryShowLoader").hide();
                                    $("#UpdateSubCategoryShowButtons").show();
                                    toastr.error("Failed to Upload Image", "Error");
                                    return false;
                                }
                            },
                            error: function (response) {
                                $("#UpdateSubCategoryShowLoader").hide();
                                $("#UpdateSubCategoryShowButtons").show();
                                toastr.error(response, "Error");
                                return false;
                            }
                        });
                    }
                    $("#UpdateSubCategory").modal("hide");
                    $("#UpdateSubCategoryShowLoader").hide();
                    $("#UpdateSubCategoryShowButtons").show();
                    toastr.success("Sub Category Updated Successfully", "Success");
                    $('#AdminSubCategory').DataTable().ajax.reload();
                }
                else if (response == -2) {
                    $("#UpdateSubCategoryShowLoader").hide();
                    $("#UpdateSubCategoryShowButtons").show();
                    toastr.error("Sub Category Already Exists", "Error");
                }
                else if (response == -3) {
                    $("#UpdateSubCategoryShowLoader").hide();
                    $("#UpdateSubCategoryShowButtons").show();
                    toastr.error("Sub Category Already in Use", "Error");
                }
                else {
                    $("#UpdateSubCategoryShowLoader").hide();
                    $("#UpdateSubCategoryShowButtons").show();
                    toastr.error("Sub Category Not Saved", "Error");
                }
            },
            error: function (response) {
                $("#UpdateSubCategoryShowLoader").hide();
                $("#UpdateSubCategoryShowButtons").show();
                toastr.error(response, "Error");
            }

        });
    }
});

//-----------------------------------------------------------Delete Sub Category-------------------------------------------------------
//Onclick Update Form Open
$("#AdminSubCategory").on('click', '#DeleteAdminSubCategory', function () {
    var Id = $(this).val();
    var DeleteSubCategoryvalidator = $("#DeleteSubCategory_Submit_Form").validate();
    DeleteSubCategoryvalidator.resetForm();
    ResetSubCategoryIds();
    if (Id != "" && Id != null) {
        $("#DSBSubCategoryId").val(Id);
        $("#DeleteSubCategory").modal('show');
    }
    else {
        toastr.error("Sub Category Not Exits", "Error");
    }
});

$("#DeleteSubCategory_Submit_Form").validate({
    rules: {

    },
    messages: {

    },
    submitHandler: function (form) {
        $("#DeleteSubCategoryShowLoader").show();
        $("#DeleteSubCategoryShowButtons").hide();
        $.ajax({
            url: "/AdminSubMenuCategory/DeleteSubCategory",
            type: "post",
            datatype: "json",
            data: { Id: $("#DSBSubCategoryId").val() },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == 1) {
                    $("#DeleteSubCategory").modal("hide");
                    $("#DeleteSubCategoryShowLoader").hide();
                    $("#DeleteSubCategoryShowButtons").show();
                    toastr.success("Sub Category Deleted Successfully", "Success");
                    $('#AdminSubCategory').DataTable().ajax.reload();
                }
                else if (response == -2) {
                    $("#DeleteSubCategoryShowLoader").hide();
                    $("#DeleteSubCategoryShowButtons").show();
                    toastr.warning("Please Delete its Sub Category Child", "Warning");
                }
                else if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else {
                    $("#DeleteSubCategoryShowLoader").hide();
                    $("#DeleteSubCategoryShowButtons").show();
                    toastr.error("Sub Category Not Deleted", "Error");
                }
            },
            error: function (response) {
                $("#DeleteSubCategoryShowLoader").hide();
                $("#DeleteSubCategoryShowButtons").show();
                toastr.error(response, "Error");
            }

        });
    }
});
//-----------------------------------------------------------Get Add Update Main Category----------------------------------------------
function AddBindSMainCategory() {
    var BindAddSMainCategory = $("#ASBMCategoryId");
    BindAddSMainCategory.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
    $.ajax({
        url: "/AdminMMCategory/GetAllMainCategory",
        type: "get",
        datatype: "json",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            BindAddSMainCategory.empty().append('<option selected="selected" value="">Please select</option>');
            $.each(response, function (i) {
                BindAddSMainCategory.append($("<option></option>").val(this['id']).html(this['name']));
            });
        },
        error: function (response) {
            toastr.error(response, "Error");
        }

    });
}

function UpdateBindSMainCategory() {
    var UpdateSMainCategory = $("#USBMCategoryId");
    UpdateSMainCategory.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
    $.ajax({
        url: "/AdminMMCategory/GetAllMainCategory",
        type: "get",
        datatype: "json",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            UpdateSMainCategory.empty().append('<option selected="selected" value="">Please select</option>');
            $.each(response, function (i) {
                UpdateSMainCategory.append($("<option></option>").val(this['id']).html(this['name']));
            });
        },
        error: function (response) {
            toastr.error(response, "Error");
        }

    });
}
//-------------------------------------------------------------Banner Image Upload Add and Update---------------------------------

$("#ASBSimageBannerfiles").change(function () {
    var File = this.files;
    if (File && File[0]) {
        ASBSBannerReadImage(File[0]);
    }
});

var ASBSBannerReadImage = function (file) {

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

            $("#ASBStargetBanner").attr('src', _file.target.result);
        }

    }
}

$("#USBSimageBannerfiles").change(function () {
    var File = this.files;
    if (File && File[0]) {
        USBSBannerReadImage(File[0]);
    }
});

var USBSBannerReadImage = function (file) {

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

            $("#USBStargetBanner").attr('src', _file.target.result);
        }

    }
}

//-------------------------------------------------------------Feature Image Upload Add and Update---------------------------------

$("#ASBSimageFeaturefiles").change(function () {
    var File = this.files;
    if (File && File[0]) {
        ASBSFeatureReadImage(File[0]);
    }
});

var ASBSFeatureReadImage = function (file) {

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

            $("#ASBStargetFeature").attr('src', _file.target.result);
        }
    }
}

$("#USBSimageFeaturefiles").change(function () {
    var File = this.files;
    if (File && File[0]) {
        USBSFeatureReadImage(File[0]);
    }
});

var USBSFeatureReadImage = function (file) {

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

            $("#USBStargetFeature").attr('src', _file.target.result);
        }
    }
}

//-------------------------------------------------------------Web Icon Upload Add and Update---------------------------------

$("#ASBSimageWeb").change(function () {
    var File = this.files;
    if (File && File[0]) {
        ASBSimageWeb(File[0]);
    }
});

var ASBSimageWeb = function (file) {

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

            $("#ASBStargetWeb").attr('src', _file.target.result);
        }

    }
}

$("#USBSimageWeb").change(function () {
    var File = this.files;
    if (File && File[0]) {
        USBSimageWeb(File[0]);
    }
});

var USBSimageWeb = function (file) {

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

            $("#USBStargetWeb").attr('src', _file.target.result);
        }

    }
}

//-------------------------------------------------------------Mobile Icon Upload Add and Update---------------------------------

$("#ASBSimageMobile").change(function () {
    var File = this.files;
    if (File && File[0]) {
        ASBSimageMobile(File[0]);
    }
});

var ASBSimageMobile = function (file) {

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

            $("#ASBStargetMobile").attr('src', _file.target.result);
        }
    }
}


$("#USBSimageMobile").change(function () {
    var File = this.files;
    if (File && File[0]) {
        USBSimageMobile(File[0]);
    }
});

var USBSimageMobile = function (file) {

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

            $("#USBStargetMobile").attr('src', _file.target.result);
        }
    }
}

//-------------------------------------------------------Load and Update ParentCategory------------------------------------------------
$("#ASBSCategoryId").select2({
    placeholder: "Please Select",
    ajax: {
        url: "/AdminSubMenuCategory/LoadListSubCategory",
        dataType: 'json',
        delay: 250,
        type: "get",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        data: function (params) {
            return {
                search: params.term, // search term
                pageNo: params.page || 1,
                pageSize: 10,
                CatId: '0',
                MainCatId: $("#ASBMCategoryId").val()
            };
        },
        processResults: function (data, params) {
            // parse the results into the format expected by Select2
            // since we are using custom formatting functions we do not need to
            // alter the remote JSON data, except to indicate that infinite
            // scrolling can be used
            params.page = params.page || 1;

            return {
                results: data.subCategoy,
                pagination: {
                    more: (params.page * 10) < data.rowCount
                }
            };
        },
        cache: true
    },
    escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
    minimumInputLength: 1,
});

$("#USBSCategoryId").select2({
    placeholder: "Please Select",
    ajax: {
        url: "/AdminSubMenuCategory/LoadListSubCategory",
        dataType: 'json',
        delay: 250,
        type: "get",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        data: function (params) {
            return {
                search: params.term, // search term
                pageNo: params.page || 1,
                pageSize: 10,
                CatId: $("#USBSubCategoryId").val(),
                MainCatId: $("#USBMCategoryId").val()
            };
        },
        processResults: function (data, params) {
            // parse the results into the format expected by Select2
            // since we are using custom formatting functions we do not need to
            // alter the remote JSON data, except to indicate that infinite
            // scrolling can be used
            params.page = params.page || 1;

            return {
                results: data.subCategoy,
                pagination: {
                    more: (params.page * 10) < data.rowCount
                }
            };
        },
        cache: true
    },
    escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
    minimumInputLength: 1,
});

//------------------------------------------------------------------------Reset--------------------------------------------------------------------------------------

function ResetSubCategoryIds() {
    $("#ASBMCategoryId").val('');
    $("#ASBSCategoryId").val('');
    $("#ASBSCategoryId").text('');
    $("#ASBSName").val('');
    $("#ASBSIcon").val('');
    $("#ASBSDescription").val('');
    $("#ASBStargetBanner").attr("src", "");
    $('#ASBStargetFeature').attr("src", "");
    $("#ASBSimageBannerfiles").val("");
    $('#ASBSimageFeaturefiles').val("");
    $("#ASBSMetaTitle").val('');
    $("#ASBSMetaKeyword").val('');
    $("#ASBSMetaDescription").val('');

    $("#ASBStargetWeb").attr("src", "");
    $('#ASBStargetMobile').attr("src", "");
    $("#ASBSimageWeb").val("");
    $('#ASBSimageMobile').val("");

    $("#ASBIsLastNode").prop('checked', false);

    $("#USBSubCategoryId").val('');
    $("#USBMCategoryId").val('');
    $("#USBSCategoryId").val('');
    $("#USBSCategoryId").text('');
    $("#USBSName").val('');
    $("#USBSIcon").val('');
    $("#USBSDescription").val('');
    $("#USBStargetBanner").attr("src", "");
    $('#USBStargetFeature').attr("src", "");
    $("#USBSimageBannerfiles").val("");
    $('#USBSimageFeaturefiles').val("");
    $("#USBSMetaTitle").val('');
    $("#USBSMetaKeyword").val('');
    $("#USBSMetaDescription").val('');

    $("#USBStargetWeb").attr("src", "");
    $('#USBStargetMobile').attr("src", "");
    $("#USBSimageWeb").val("");
    $('#USBSimageMobile').val("");

    $("#DSBSubCategoryId").val('');

}