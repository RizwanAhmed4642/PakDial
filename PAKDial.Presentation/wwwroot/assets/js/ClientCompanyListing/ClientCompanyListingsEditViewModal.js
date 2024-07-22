var btnCLSave = "";
$(document).ready(function () {
    $("#btnSave").prop('disabled', false);
    var Id = $("#ECompanyListingId").val();
    var BindAddListingType = $("#UCLstListingType");
    BindAddListingType.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
    var BindAddMainCategory = $("#UCLstCategory");
    BindAddMainCategory.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
    var BindAddState = $("#UCLstState");
    BindAddState.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
    
    $.ajax({
        url: "/ClientListing/LoadClientCompanyListingList",
        type: "get",
        datatype: "json",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
           
            BindAddListingType.empty().append('<option selected="selected" value="">Please select</option>');
            $.each(response.listingTypes, function (i) {
                BindAddListingType.append($("<option></option>").val(this['id']).html(this['text']));
            });
            BindAddMainCategory.empty().append('<option selected="selected" value="">Please select</option>');
            $.each(response.categories, function (i) {
                BindAddMainCategory.append($("<option></option>").val(this['id']).html(this['text']));
            });
            BindAddState.empty().append('<option selected="selected" value="">Please select</option>');
            $.each(response.states, function (i) {
                BindAddState.append($("<option></option>").val(this['id']).html(this['text']));
            });

            $.each(response.socialMediaModes, function (i) {
                $("#EditSocialMediaModesIds").append('<div class="form row"><div class="form-group col-md-3"><label id="UCLstSocialMediaNodesName' + this['id'] + '"><img src=' + this['imageDir'] + ' />' + this['name'] + '</label></div><div class="form-group col-md-9"><input type="Text" class="form-control" name="UCLstSocialMediaNodes' + this['id'] + '" id="UCLstSocialMediaNodes' + this['id'] + '" placeholder="https://www.' + this['name'] + '.com/"></div></div>');
            });
            if (response.stateId > 0) {
                $("#UCLstState").attr("disabled", true);
                $("#UCLstCity").attr("disabled", true);
            }
        },
        error: function (response) {
            toastr.error(response, "Error");
        }

    });
    
    $("#UCLstCategory").select2({ placeholder: "Please Select" });

    $("#UCLstSubCategory").select2({ placeholder: "Please Select" });

   // $("#UCLstCity").select2({ placeholder: "Please Select" });

    //$("#UCLstCityArea").select2({ placeholder: "Please Select" });
    
    $("#UCLstPaymentsModes").select2({
        placeholder: "Please Select",
        ajax: {
            url: "/ClientListing/LoadPaymentModeList",
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
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;

                return {
                    results: data.paymentModes,
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

    $("#UCLstServicetypes").select2({
        placeholder: "Please Select",
        ajax: {
            url: "/ClientListing/LoadTypeofServiceList",
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
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;

                return {
                    results: data.servicesType,
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

    if (Id != "" && Id != null) {
       
        $.ajax({
            url: "/ClientListing/GetCompanyListingsById",
            type: "get",
            datatype: "json",
            data: { Id: Id },
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                    
                if (response != null) {
                    $("#UCompListingsId").val(response.companyListings.id);
                    $("#UCLstListingType").find('option[value="' + response.companyListings.listingTypeId + '"]').attr('selected', 'selected');
                    $("#UCLstListingType").attr("disabled", true);
                    $("#UCLstCompanyName").val(response.companyListings.companyName);
                    $("#UCLstFirstName").val(response.companyListings.firstName);
                    $("#UCLstLastName").val(response.companyListings.lastName);
                    $("#UCLstWebSite").val(response.companyListings.website);
                    $("#UCLstMetaTitle").val(response.companyListings.metaTitle);
                    $("#UCLstMetaKeyword").val(response.companyListings.metaKeyword);
                    $("#UCLstMetaDescription").val(response.companyListings.metaDescription);
                    $("#UCLstMobileNumber").val(response.listingMobileNo[0].mobileNo);
                    if (response.listingLandlineNo.length > 0) {
                        $("#UCLstLandLineNumber").val(response.listingLandlineNo[0].landlineNumber);
                    }
                    if (response.listingMobileNo.length > 1) {

                        $("#UAddMobileNumber").append('<div class="input-group mb-1" data-repeater-item="" style="" id="RemovedivMobileNumber"><input type="text" placeholder="+92 000 000" class="form-control" id="UCLstMobileNumber1" maxlength="11"><span class="input-group-append" id="button-addon2"><button class="btn btn-danger" id="RemoveMobileNo" onclick="RemoveMobNo()"  type="button" data-repeater-delete=""><i class="ft-minus"></i></button> </span></div>')
                        $("#UCLstMobileNumber1").val(response.listingMobileNo[1].mobileNo);
                    }
                    if (response.listingLandlineNo.length > 1) {
                        $("#UAddLandNumber").append('<div class="input-group mb-1" data-repeater-item="" style="" id="RemovedivLandLine" > <input type="text" placeholder="0423-000000" class="form-control" id="UCLstLandLineNumber1" maxlength="12"><span class="input-group-append" id="button"><button class="btn btn-danger" id="RemoveLandNo"  onclick="RemoveLandLineNo()"  type="button" data-repeater-delete=""> <i class="ft-minus" id="test"></i></button></span></div>')
                        $("#UCLstLandLineNumber1").val(response.listingLandlineNo[1].landlineNumber);
                    }
                    if (response.listingCategory.length > 0) {
                        var newOption = new Option(response.listingCategory[0].mainCategoryName, response.listingCategory[0].mainCategoryId, true, true);
                        // Append it to the select
                        $('#UCLstCategory').append(newOption).trigger('change');


                        $.each(response.listingCategory, function (i) {

                            var newOption = new Option(response.listingCategory[i].subCategoryName, response.listingCategory[i].subCategoryId, true, true);
                            // Append it to the select
                            $('#UCLstSubCategory').append(newOption).trigger('change');

                        });
                    }

                    //Location Information
                    if (response.listingAddress.stateId > 0) {

                        $("#UCLstState").find('option[value="' + response.listingAddress.stateId + '"]').attr('selected', 'selected');
                        var newOption = new Option(response.listingAddress.cityName, response.listingAddress.cityId, true, true);
                        // Append it to the select
                        $('#UCLstCity').append(newOption).trigger('change');

                    }

                    $("#UAddressId").val(response.listingAddress.id);
                    $("#UCLstBuildingAddress").val(response.listingAddress.buildingAddress);
                    $("#UCLstStreetAddress").val(response.listingAddress.streetAddress);
                    $("#UCLstCityArea").val(response.listingAddress.cityAreaId);
                    $("#UCLstLandMark").val(response.listingAddress.landMark);
                    //$("#UCLstArea").val(response.listingAddress.area);
                    $("#UCLstLatitude").val(response.listingAddress.latitude);
                    $("#UCLstLongitude").val(response.listingAddress.longitude);
                    $("#UCLstAddress").val(response.listingAddress.latLogAddress);
                    var j = 1;
                    //OtherInformation
                    for (var i = 0; i < 7; i++) {

                        $("#UCLstFromTimming" + j).val(response.companyListingTimming[i].timeFrom);
                        $("#UCLstToTimming" + j).val(response.companyListingTimming[i].timeTo);
                        $("#UCLstToTimmingClosed" + j).prop('checked', response.companyListingTimming[i].isClosed);
                        if  ($("#UCLstToTimmingClosed" + j).prop('checked') == true) {
                            $("#UCLstFromTimming" + j).attr("disabled", true);
                            $("#UCLstToTimming" + j).attr("disabled", true);
                        }
                        else {
                            $("#UCLstFromTimming" + j).attr("disabled", false);
                            $("#UCLstToTimming" + j).attr("disabled", false);
                        }
                        j += 1;
                    }
                    $("#UCLstYears").val(response.companyListingProfile.yearEstablished);
                    $("#UCompProfileId").val(response.companyListingProfile.id);
                    $("#UCLstAnnualTurnover").val(response.companyListingProfile.annualTurnOver);
                    $("#UCLstNoEmployee").find('option[value="' + response.companyListingProfile.numberofEmployees + '"]').attr('selected', 'selected');
                    $("#UCLstProfAss").val(response.companyListingProfile.professionalAssociation);
                    $("#UCLstCertifications").val(response.companyListingProfile.certification);
                    $("#UCLstBreifAbout").val(response.companyListingProfile.briefAbout);
                    $("#UCLstLocationOverview").val(response.companyListingProfile.locationOverview);
                    $("#UCLstProductAndServices").val(response.companyListingProfile.productAndServices);

                    $.each(response.listingPaymentsMode, function (i) {

                        var newOption = new Option(response.listingPaymentsMode[i].modeName, response.listingPaymentsMode[i].modeId, true, true);
                        // Append it to the select
                        $('#UCLstPaymentsModes').append(newOption).trigger('change');

                    });

                    $.each(response.listingsBusinessTypes, function (i) {

                        var newOption = new Option(response.listingsBusinessTypes[i].text, response.listingsBusinessTypes[i].businessId, true, true);
                        // Append it to the select
                        $('#UCLstBusinessTypes').append(newOption).trigger('change');

                    });

                    $.each(response.listingServices, function (i) {
                        
                        var newOption = new Option(response.listingServices[i].serviceName, response.listingServices[i].serviceTypeId, true, true);
                        // Append it to the select
                        $('#UCLstServicetypes').append(newOption).trigger('change');

                    });
                    var l = 1;
                    $.each(response.socialMediaModes, function (i) {
                        $("#UAddSocialMediaModesIds").append('<div class="form row"><div class="form-col col-md-3"><label id="UCLstSocialMediaNodesName' + l + '"><img src=' + this['imagedir'] + ' />' + this['name'] + '</label></div><div class="form-col col-md-9"><input type="Text" class="form-control" name="UCLstSocialMediaNodes' + l + '" id="UCLstSocialMediaNodes' + l + '" placeholder="https://www.' + this['name'] + '.com/"></div></div>');
                        if (response.socialMediaModes[i].sitePath != null) {
                            $("#UCLstSocialMediaNodes" + l).val(response.socialMediaModes[i].sitePath);
                            $("#UCLstSocialMediaNodesName" + l).text(response.socialMediaModes[i].name);
                    
                        }
                        l += 1;
                    });

                    //Image Gallery
                    var totalimg = 0;
                    if (response.companyListings.bannerImage == null) {                    
                        $("#UtargetImg").attr('src', "/app-assets/images/portrait/small/avatar-s-19.jpg");
                        $("#UtargetImg").attr('value', response.companyListings.id);
                    }
                    else {
                        $("#UtargetImg").attr('src', response.companyListings.bannerImage);
                        $("#UtargetImg").attr('value', response.companyListings.id);
                    }
                    
                    for (var i = 0; i < response.listingGallery.length; i++) {                     
                        totalimg += 1;
                        var Id = "UMtargetImg" + response.listingGallery[i].id;
                        $("#UMImageLink").append('<figure itemprop="associatedMedia" itemscope ="" itemtype="http://schema.org/ImageObject"><div class="parent"><img class="img - thumbnail img - fluid" itemprop="thumbnail" alt="Image description" id="UMtargetImg' + response.listingGallery[i].id + '" style="width:270px; height:138px;" /><button class="close" onclick="RemoveImage(' + Id +')">X</button></div></figure>&nbsp;&nbsp;')

                        $("#UMtargetImg" + response.listingGallery[i].id).attr('src', response.listingGallery[i].uploadDir);
                        $("#UMtargetImg" + response.listingGallery[i].id).attr('value', response.listingGallery[i].id);
                    }
                }
                else {
                    toastr.error("Company Listings Not Exits", "Error");
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }

   

});

$('ul#myTab li').click(function (e) {
    var lObjTabValue = $(this).children('a').text();
    if (lObjTabValue == "Gallery") {

        $('#btnSave').css('visibility', 'visible');
    } else {
        $('#btnSave').css('visibility', 'hidden');
    }
})



$("#EditCompanyListings_Submit_Form").validate({
    ignore: false,
    rules: {

        UCLstListingType: {
            required: true,
            numberonly: true
        },
        UCLstCompanyName: {
            required: true,
            //letteronly: true
        },
        UCLstFirstName: {
            required: true,
            letteronly: true
        },
        UCLstLastName: {
            required: true,
            letteronly: true
        },
        UCLstMobileNumber: {
            required: true,
            numberonly: true,
            minlength: function () {

                var MobileMin = $('#UCLstMobileNumber').val();
                if (MobileMin.length < 11) {
                    return 11;
                }
            },
            maxlength: function () {

                var MobileMax = $('#UCLstMobileNumber').val();
                if (MobileMax.length > 11) {
                    return 11;
                }
            }
        },
        UCLstLandLineNumber: {
            numberonly: true,
            minlength: function () {

                var MobileMin = $('#UCLstLandLineNumber').val();
                if (MobileMin.length < 11) {
                    return 11;
                }
            },
            maxlength: function () {

                var MobileMax = $('#UCLstLandLineNumber').val();
                if (MobileMax.length > 11) {
                    return 11;
                }
            }
        },
        UCLstCity: {
            required: true
        },
        UCLstState: {
            required: true
        },

        UCLstAddress: {
            required: true
        },
        UCLstCityArea: {
            required: true
        },
        UCLstCategory: {
            required: true
        },
        UCLstSubCategory: {
            required: true
        }
    },
    messages: {

        UCLstListingType: {
            required: "Please Select Listing Type."
        },
        UCLstCompanyName: {
            required: "Please Enter Company Name."
        },
        UCLstFirstName: {
            required: "Please Enter First Name"
        },
        UCLstLastName: {
            required: "Please Enter Last Name"
        },
        UCLstMobileNumber: {
            required: "Please enter mobileNo ."
        },
        UCLstCity: {
            required: "Please enter city."
        },
        UCLstState: {
            required: "Please enter state."
        },

        UCLstAddress: {
            required: "Please enter Address",
        },
        UCLstCityArea: {
            required: "Please enter CityArea",

        },
        UCLstCategory: {
            required: "Please select Main Category",
        },
        UCLstSubCategory: {
            required: "Please Select Sub Category",
        }
    },

    highlight: function (element) {
        $(element).closest('.form-group').addClass('text-danger');
        if (btnCLSave == "SaveNew") {

            $("#btnUpdateNew").attr("style", "");
        }
        else {
            $("#btnUpdateClose").attr("style", "");
        }
    },
    unhighlight: function (element) {
        $(element).closest('.form-group').removeClass('text-danger');
    },
    wrapper: 'div',
    errorClass: 'text-danger',
    errorPlacement: function (error, element) {
       
        var name = $(element).parent().attr("id");
        if (name == "MobileNumber") {
            error.insertAfter('#MobileNumber');
        } else if (name == "LandLineNumber") {
            error.insertAfter('#LandLineNumber');
        } else if (name == "RemovedivMobileNumber") {
            error.insertAfter('#RemovedivMobileNumber');
        }
        else if (name == "RemovedivLandLine") {
            error.insertAfter('#RemovedivLandLine');
        }
        else {
            if (name != "MobileNumber" && name != "LandLineNumber" && name != "RemovedivMobileNumber" && name != "RemovedivLandLine") {
                error.insertAfter(element);
            }
        } 
    },
    submitHandler: function (form) {
        $("#btnSave").prop('disabled', true);
        var lObjComList = new AddUpdateCompanyListings();
        var CompanyListingProfileAdd = fillcompListingProfile();
        var CompanyAddressAdd = CompanyListingAddress();
        var CompanyListingTimmingAdd = CompanyTimming();
        var ListingCategoryAdd = CompanyListingCategory();
        var ListingLandlineNoAdd = CompanyListingLandlineNo();
        var ListingMobileNoAdd = CompListingMobileNo();
        var ListingPaymentsModeAdd = CompanyPaymentMode();
        var ListingServicesAdd = CompanyListingServices();
        var ListingSocialMediaAdd = CompanyListingSocialMedia();
        var ListingsBusinessTypesAdd = CompanyListingsBusinessTypes();


        lObjComList.CompanyListings = fillCompanyList();
        lObjComList.CompanyListingProfile = CompanyListingProfileAdd != null ? CompanyListingProfileAdd : null;
        lObjComList.ListingAddress = CompanyAddressAdd != null ? CompanyAddressAdd : null;
        lObjComList.CompanyListingTimming = CompanyListingTimmingAdd.length > 0 ? CompanyListingTimmingAdd : null;
        lObjComList.ListingCategory = ListingCategoryAdd.length > 0 ? ListingCategoryAdd : null;
        lObjComList.ListingLandlineNo = ListingLandlineNoAdd.length > 0 ? ListingLandlineNoAdd : null;
        lObjComList.ListingMobileNo = ListingMobileNoAdd.length > 0 ? ListingMobileNoAdd : null;
        lObjComList.ListingPaymentsMode = ListingPaymentsModeAdd.length > 0 ? ListingPaymentsModeAdd : null;
        lObjComList.ListingServices = ListingServicesAdd.length > 0 ? ListingServicesAdd : null;
        lObjComList.ListingsBusinessTypes = ListingsBusinessTypesAdd.length > 0 ? ListingsBusinessTypesAdd : null;
        lObjComList.ListingSocialMedia = ListingSocialMediaAdd.length > 0 ? ListingSocialMediaAdd : null;

        $.ajax({
            url: "/ClientListing/EditCompanyListing",
            type: "post",
            datatype: "json",
            data: { companyListings: lObjComList },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
             
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                    $("#btnSave").prop('disabled', false);
                }
                else if (response > 0) {
                    
                    var Sfile = $('#Uimagefiles').get(0).files;
                    var Mfile = $('#UMimagefiles').get(0).files;

                    var Sdata = getSelectedImages($('#Uimagefiles'));
                    var Mdata = getSelectedImages($('#UMimagefiles'));

                    if (Sfile.length > 0) {
                        $.ajax({
                            url: "/ClientListing/UpdateBannerImage?Id=" + response,
                            type: "post",
                            datatype: "json",
                            data: Sdata,
                            async: false,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (response) {


                            },
                            error: function (response) {
                                $("#btnSave").prop('disabled', false);
                                toastr.error(response, "Error");
                            }
                        });
                    }

                    if (Mfile.length > 0) {
                        $.ajax({
                            url: "/ClientListing/UpdateGalleryBannerImage?Id=" + response,
                            type: "post",
                            datatype: "json",
                            data: Mdata,
                            async: false,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (response) {


                            },
                            error: function (response) {
                                $("#btnSave").prop('disabled', false);
                                toastr.error(response, "Error");
                            }
                        });
                    }

                    GetButtonValue();
                    btnCLSave = undefined;

                }
                else if (response == 0)
                {
                    $("#btnSave").prop('disabled', false);
                    toastr.error("Company Listings is not Updated Successfully", "Error");
                }
                else {
                    GetButtonValue();
                    btnCLSave = undefined;
                }
            },
            error: function (response) {
                $("#btnSave").prop('disabled', false);
                toastr.error(response, "Error");
            }
        });
    }
});

//------------ Check Mobile No --------------------
$("#UCLstMobileNumber").focusout(function () {

    var MobileNo = $(this).val();
    var Id = $("#ECompanyListingId").val();
    if (MobileNo != "") {
        $.ajax({
            url: "/AdminCommon/CheckMobileNoExits",
            type: "get",
            datatype: "json",
            data: { MobileNo: MobileNo, Id: Id },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == 0) {
                    toastr.error("Number Already Exits.");
                    $("#UCLstMobileNumber").val("");
                    $("#UCLstMobileNumber").focus();
                }
                else {
                    
                }
            },
            error: function (response) {
                toastr.error(response);
            }

        });
    }
});


$(document).on('focusout', '#UCLstMobileNumber1', function () {
    var MobileNo = $(this).val();
    var Id = $("#ECompanyListingId").val();
    if (MobileNo != "") {
        $.ajax({
            url: "/AdminCommon/CheckMobileNoExits",
            type: "get",
            datatype: "json",
            data: { MobileNo: MobileNo, Id: Id },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == 0) {
                    toastr.error("Number Already Exits.");
                    $("#UCLstMobileNumber1").val("");
                    $("#UCLstMobileNumber1").focus();
                }
                else {
                   
                }
            },
            error: function (response) {
                toastr.error(response);
            }

        });
    }
});
//------------ Check Landline No --------------------
$("#UCLstLandLineNumber").focusout(function () {
    var Landline = $(this).val();
    var Id = $("#ECompanyListingId").val();
    if (Landline != "") {
        $.ajax({
            url: "/AdminCommon/CheckLandineNoExits",
            type: "get",
            datatype: "json",
            data: { LandLineNo: Landline, Id: Id },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == 0) {
                    toastr.error("Number Already Exits.");
                    $("#UCLstLandLineNumber").val("");
                    $("#UCLstLandLineNumber").focus();
                }
                else {
                    
                }
            },
            error: function (response) {
                toastr.error(response);
            }

        });
    }
}); 


    $(document).on('focusout', '#UCLstLandLineNumber1', function () {
    var Landline = $(this).val();
    var Id = $("#ECompanyListingId").val();
    if (Landline != "") {
        $.ajax({
            url: "/AdminCommon/CheckLandineNoExits",
            type: "get",
            datatype: "json",
            data: { LandLineNo: Landline, Id: Id },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == 0) {
                    toastr.error("Number Already Exits.");
                    $("#UCLstLandLineNumber1").val("");
                    $("#UCLstLandLineNumber1").focus();
                }
                else {
                    
                }
            },
            error: function (response) {
                toastr.error(response);
            }

        });
    }
}); 


$('#btnUpdateNew').click(function () {
   
    btnCLSave = $('#btnUpdateNew').val();
    $("#EditCompanyListings_Submit_Form").valid();
   
    //ResetCompanyListings();
});

$('#btnUpdateClose').click(function () {
  
    btnCLSave = $('#btnUpdateClose').val();
    $("#EditCompanyListings_Submit_Form").valid();
   
    //ResetCompanyListings();
});

function GetButtonValue() {

        $("#btnSave").prop('disabled', false);
        $("#MainSection").hide();
        toastr.success("Company Listings Updated Successfully", "Success");
        window.location.href = "/ClientListing/Index";
        ResetCompanyListings();
    
}

$("#btnClose").click(function () {
    $("#MainSection").hide();
    if ($("#btnClose").val() == "Profile") {

        window.location.href = "/profileUser/Profile";
    }
    else {
        window.location.href = "/ClientListing/Index";
    }
});

$("#EditbtnCrossForm").click(function () {
    $("#MainSection").hide();
    window.location.href = "/ClientListing/Index";
});
//----- fill CompanyListings----------
function fillCompanyList() {
    var cL = new CompanyListings();
    cL.Id = $("#UCompListingsId").val();
    cL.ListingTypeId = $("#UCLstListingType").val();
    cL.CompanyName = $("#UCLstCompanyName").val();
    cL.FirstName = $("#UCLstFirstName").val();
    cL.LastName = $("#UCLstLastName").val();
    cL.Website = $("#UCLstWebSite").val();
    cL.MetaTitle = $("#UCLstMetaTitle").val();
    cL.MetaKeyword = $("#UCLstMetaKeyword").val();
    cL.MetaDescription = $("#UCLstMetaDescription").val();
  
    return cL;
}

//--------Company registratin---------
function Registration() {
    var Rg = new CCustomerRegistration();
    var Rad = $("input[name='inlineRadioOptions']:checked").val();
    if (Rad == "IsNew") {
        Rg.Email = $("#ACLstEmail").val();
        Rg.Password = $("#ACLstPassword").val();
    }
    return Rg;
}

//----- fill CompanyListings-----------
function fillcompListingProfile() {

   
    var Profile = new CompanyListingProfile()
    Profile.Id = $("#UCompProfileId").val();
    Profile.ListingId = $("#UCompListingsId").val();
    Profile.YearEstablished = $("#UCLstYears").val();
    Profile.AnnualTurnOver = $("#UCLstAnnualTurnover").val();
    Profile.Certification = $("#UCLstCertifications").val();
    Profile.BriefAbout = $("#UCLstBreifAbout").val();
    Profile.LocationOverview = $("#UCLstLocationOverview").val();
    Profile.ProductAndServices = $("#UCLstProductAndServices").val();
    Profile.ProfessionalAssociation = $("#UCLstProfAss").val();
    Profile.NumberofEmployees = $("#UCLstNoEmployee").val();
    return Profile;
}

//----- fill company Address-----------
function CompanyListingAddress() {

    var Address = new ListingAddress();
    Address.Id = $("#UAddressId").val();
    Address.ListingId = $("#UCompListingsId").val();
    Address.StateId = $("#UCLstState").val();
    Address.CityId = $("#UCLstCity").val();
    Address.CityAreaId = $("#UCLstCityArea").val();
    //Address.Area = $("#UCLstArea").val();
    Address.BuildingAddress = $("#UCLstBuildingAddress").val();
    Address.StreetAddress = $("#UCLstStreetAddress").val();
    Address.LandMark = $("#UCLstLandMark").val();
    Address.Latitude = $("#UCLstLatitude").val();
    Address.Longitude = $("#UCLstLongitude").val();
    Address.LatLogAddress = $("#UCLstAddress").val();
    return Address;
}

//------ fill CompanyListingTimming-----
function CompanyTimming() {
    var ArrayTimming = [];
    for (var i = 1; i < 8; i++) {

        var Timming = new CompanyListingTimming();
        Timming.WeekDayNo = i;
        Timming.ListingId = $("#UCompListingsId").val();
        Timming.DaysName = $("#UCLstWeekDaysName" + i).text();
        Timming.TimeFrom = $("#UCLstFromTimming" + i).val();
        Timming.TimeTo = $("#UCLstToTimming" + i).val();
        if ($("#UCLstToTimmingClosed" + i).prop('checked') == true) {
            Timming.IsClosed = true;
        }
        else {
            Timming.IsClosed = false;
        }
        Timming.ListingId = '';
        ArrayTimming.push(Timming);
    }
    return ArrayTimming;
}
//----- fill ListingCategory-------------

function CompanyListingCategory() {
    var ArrayCategory = [];
    var value = $("#UCLstSubCategory").val();
    if (value.length > 0) {
        $('#UCLstSubCategory :selected').each(function () {
            var LC = new ListingCategory();
            LC.ListingId = $("#UCompListingsId").val();
            LC.MainCategoryId = $("#UCLstCategory").val();
            LC.SubCategoryId = $(this).val();
            ArrayCategory.push(LC);
        });

    }
    
    return ArrayCategory;
}

//--------fill ListingLandlineNo-------
function CompanyListingLandlineNo() {
    var ArraylLLN = [];
    if ($("#UCLstLandLineNumber").val() != '') { 
    var lLLN = new ListingLandlineNo();
    lLLN.LandlineNumber = $("#UCLstLandLineNumber").val();
    lLLN.ListingId = $("#UCompListingsId").val();
    ArraylLLN.push(lLLN);

    for (var i = 1; i < 2; i++) {
        var lLL = new ListingLandlineNo();
        lLL.LandlineNumber = $("#UCLstLandLineNumber" + i).val();
        lLL.ListingId = $("#UCompListingsId").val();
        if (lLL.LandlineNumber != null && lLL.LandlineNumber != undefined) {
            ArraylLLN.push(lLL);
        }

        }
            }

    return ArraylLLN;
}

//---------------fill ListingMobileNo-------------
function CompListingMobileNo() {
    var ArraylMN = [];
    var lMN = new ListingMobileNo();
    lMN.MobileNo = $("#UCLstMobileNumber").val();
    lMN.ListingId = $("#UCompListingsId").val();
    ArraylMN.push(lMN);
    for (var i = 1; i < 2; i++) {
        var lMNN = new ListingMobileNo();
        lMNN.MobileNo = $("#UCLstMobileNumber" + i).val();
        lMNN.ListingId = $("#UCompListingsId").val();
        if (lMNN.MobileNo != null && lMNN.MobileNo != undefined) {
            ArraylMN.push(lMNN);
        }

    };
    return ArraylMN;
}

//-------------------ListingPaymentMode--------------
function CompanyPaymentMode() {

    var Arraylp = [];

    $('#UCLstPaymentsModes :selected').each(function () {
        var lP = new ListingPaymentsMode();
        lP.ModeId = $(this).val();
        lP.ListingId = $("#UCompListingsId").val();
        Arraylp.push(lP);
    });
    return Arraylp;
}

//----------ListingServices---------------------
function CompanyListingServices() {

    var ArraylS = [];
    $('#UCLstServicetypes :selected').each(function () {
        var lS = new ListingServices();
        lS.ServiceTypeId = $(this).val();
        lS.ListingId = $("#UCompListingsId").val();
        ArraylS.push(lS);
    });
    return ArraylS;
}

//----------ListingSocialMedia-----------
function CompanyListingSocialMedia() {

    var ArraylsM = [];

    for (var i = 1; i < 6; i++) {
        var lsM = new ListingSocialMedia();
        lsM.Name = $("#UCLstSocialMediaNodesName" + i).text();
        lsM.SitePath = $("#UCLstSocialMediaNodes" + i).val();
        lsM.ListingId = $("#UCompListingsId").val();
        ArraylsM.push(lsM);

    }

    return ArraylsM;
}

//------------ListingsBusinessTypes---------------
function CompanyListingsBusinessTypes() {
    var ArrayBT = [];

    $('#UCLstBusinessTypes :selected').each(function () {
        var BT = new ListingsBusinessTypes();
        BT.BusinessId = $(this).val();
        BT.ListingId = $("#ECompanyListingId").val();
        ArrayBT.push(BT);
    });
    return ArrayBT;
}
//-------------------------------------------------- Add Phone No and LandNo----------------------------------------------------------------------------------

$("#btnAddMobile").click(function () {

    if ($("#UCLstMobileNumber").val() == "") {
        alert("Please Enter Phone No first.");
        return;
    }

    if ($(":input[id^=UCLstMobileNumber1]").length == 1) {
        alert("Only Allow two Phone Number.");
        return;
    }
    $("#UAddMobileNumber").append('<div class="input-group mb-1" data-repeater-item="" style="" id="RemovedivMobileNumber"><input type="text" placeholder="+92 000 000" class="form-control"  name="UCLstMobileNumber1" id="UCLstMobileNumber1" maxlength="11"><span class="input-group-append" id="button-addon2"><button class="btn btn-danger" id="RemoveMobileNo" onclick="RemoveMobNo()"  type="button" data-repeater-delete=""><i class="ft-minus"></i></button> </span></div>')
    $("#UCLstMobileNumber1").rules("add", {
        required: true,
        numberonly: true,
        messages: {
            required: "Please enter Second Mobile Number",
        }
    });

});

$("#btnAddLandNo").click(function () {
    if ($("#UCLstLandLineNumber").val() == "") {
        alert("Please Enter Land Line No first.");
        return;
    }
    if ($(":input[id^=UCLstLandLineNumber1]").length == 1) {
        alert("Only Allow two Landline Number.");
        return;
    }
    $("#UAddLandNumber").append('<div class="input-group mb-1" data-repeater-item="" style="" id="RemovedivLandLine" > <input type="text" placeholder="0423-000000" class="form-control" name="UCLstLandLineNumber1" id="UCLstLandLineNumber1" maxlength="11"><span class="input-group-append" id="button"><button class="btn btn-danger" id="RemoveLandNo"  onclick="RemoveLandLineNo()"  type="button" data-repeater-delete=""> <i class="ft-minus" id="test"></i></button></span></div>')
    $("#UCLstLandLineNumber1").rules("add", {
        numberonly: true,
        messages: {
            required: "Please enter Second Landline Number",
        }
    });
});

function RemoveLandLineNo() {
    $("#RemovedivLandLine").remove();
}

function RemoveMobNo() {
    $("#RemovedivMobileNumber").remove();
}


//------------ Load image------------------------------------------------------------------------------------------------------------------
function RemoveBannerImage() {
    
    var Url = $("#UtargetImg");
    var Sdata = getSelectedImages($('#Uimagefiles'));
    $.ajax({
        url: "/ClientListing/DeleteBannerImage?Id=" + $("#ECompanyListingId").val(),
        type: "post",
        datatype: "json",
        data: Sdata,
        async: false,
        cache: false,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response > 0) {
                toastr.success("Banner Image is deleted successfully", "Success");
            }
            else {
                toastr.error(response, "Error");
            }
            
        },
        error: function (response) {
            toastr.error(response, "Error");
        }
    });
  
    $("#UCLTDeleteBannerImage").children("div").remove();
    $("#UCLTDeleteBannerImage").append('<div class="row"><figure class= "col-lg-12 col-md-12 col-sm-12 col-xs-12" itemprop = "associatedMedia" itemscope itemtype = "http://schema.org/ImageObject"><img class="img-fluid" itemprop="thumbnail" alt="Image description" id="UtargetImg" /><button class="close" onclick="RemoveBannerImage()">X</button></figure ></div >');
    $('#UtargetImg').attr("src", "/app-assets/images/portrait/small/avatar-s-19.jpg");

}
$("#Uimagefiles").change(function () {

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

            $("#UtargetImg").attr('src', _file.target.result);
        }

    }

}

//---------------------------------------------------Gallery Images-----------------------------------------------------------------------
//$("#UMimagefiles").change(function () {

//    var File = this.files;
//    if (File && File[0]) {
//        AMReadImage(File[0])
//    }

//});

//$("#UMimagefiles").change(function () {

//    if (typeof (FileReader) != "undefined") {
//        var dvPreview = $("#AMImageLink");
//        dvPreview.html("");
//        $($(this)[0].files).each(function () {
//            var file = $(this);
//            var reader = new FileReader();
//            reader.onload = function (e) {

//                var totalimg = $("#UMImageLink> figure").length;;
//                totalimg += 1;
//                var Id = "UMtargetImg" + totalimg;
//                $("#UMImageLink").append('<figure class="col-lg-3 col-md-6 col-sm-6 col-xs-12" id="Uimgfigure' + totalimg + '" itemprop="associatedMedia" itemscope itemtype="http://schema.org/ImageObject"> <div class="parent"><img class="img - thumbnail img - fluid" itemprop="thumbnail" alt="Image description" id="UMtargetImg' + totalimg + '"style="width:270px; height:138px;"/><button class="close" onclick="RemoveImage(' + Id + ')">x</button></div></figure>')

//                $("#UMtargetImg" + totalimg).attr('src', e.target.result);
//            }
//            reader.readAsDataURL(file[0]);
//        });
//    } else {
//        alert("This browser does not support HTML5 FileReader.");
//    }
//});

var AMReadImage = function (file) {

    var reader = new FileReader;
    var image = new Image;
    reader.readAsDataURL(file);
    reader.onload = function (_file) {

        image.src = _file.target.result;
        image.onload = function () {
            var name = this.name
            var height = this.height;
            var width = this.width;
            var type = file.type;
            var size = ~~(file.size / 1024) + "KB";
            var totalimg = $("#UMImageLink> figure").length;;
            totalimg += 1;
            $("#UMImageLink").append('<figure class="col-lg-3 col-md-6 col-sm-6 col-xs-12" itemprop="associatedMedia" itemscope itemtype="http://schema.org/ImageObject"><a href="app-assets/images/gallery/1.jpg" itemprop="contentUrl" data-size="480x360"><img class="img - thumbnail img - fluid" itemprop="thumbnail" alt="Image description" id="AMtargetImg' + totalimg + '" /></a></figure>')

            $("#UMtargetImg" + totalimg).attr('src', _file.target.result);
        }

    }

}

function GetCheckBoxValue(Id) {

    var Value = $("#" + Id).val();
    if (Value == "UCLstToTimmingClosed1") {

        if ($("#" + Value).prop('checked') == true) {
            $('#UCLstFromTimming1').attr("disabled", true);
            $('#UCLstToTimming1').attr("disabled", true);
            $('#UCLstFromTimming1').val("Closed");
            $('#UCLstToTimming1').val("Closed");
        }
        else {
            $('#UCLstFromTimming1').attr("disabled", false)
            $('#UCLstToTimming1').attr("disabled", false)
            $('#UCLstFromTimming1').val("Open 24 Hrs");
            $('#UCLstToTimming1').val("Open 24 Hrs");
        }
    } else if (Value == "UCLstToTimmingClosed2") {

        if ($("#" + Value).prop('checked') == true) {
            $("#UCLstFromTimming2").attr("disabled", true);
            $("#UCLstToTimming2").attr("disabled", true);
            $('#UCLstFromTimming2').val("Closed");
            $('#UCLstToTimming2').val("Closed");
        }
        else {
            $("#UCLstFromTimming2").attr("disabled", false);
            $("#UCLstToTimming2").attr("disabled", false);
            $('#UCLstFromTimming2').val("Open 24 Hrs");
            $('#UCLstToTimming2').val("Open 24 Hrs");
        }
    } else if (Value == "UCLstToTimmingClosed3") {
        if ($("#" + Value).prop('checked') == true) {
            $("#UCLstFromTimming3").attr("disabled", true);
            $("#UCLstToTimming3").attr("disabled", true);
            $('#UCLstFromTimming3').val("Closed");
            $('#UCLstToTimming3').val("Closed");
        }
        else {
            $("#UCLstFromTimming3").attr("disabled", false);
            $("#UCLstToTimming3").attr("disabled", false);
            $('#UCLstFromTimming3').val("Open 24 Hrs");
            $('#UCLstToTimming3').val("Open 24 Hrs");
        }
    } else if (Value == "UCLstToTimmingClosed4") {
        if ($("#" + Value).prop('checked') == true) {
            $("#UCLstFromTimming4").attr("disabled", true);
            $("#UCLstToTimming4").attr("disabled", true);
            $('#UCLstFromTimming4').val("Closed");
            $('#UCLstToTimming4').val("Closed");
        }
        else {
            $("#UCLstFromTimming4").attr("disabled", false);
            $("#UCLstToTimming4").attr("disabled", false);
            $('#UCLstFromTimming4').val("Open 24 Hrs");
            $('#UCLstToTimming4').val("Open 24 Hrs");
        }
    } else if (Value == "UCLstToTimmingClosed5") {
        if ($("#" + Value).prop('checked') == true) {
            $("#UCLstFromTimming5").attr("disabled", true);
            $("#UCLstToTimming5").attr("disabled", true);
            $('#UCLstFromTimming5').val("Closed");
            $('#UCLstToTimming5').val("Closed");
        }
        else {
            $("#UCLstFromTimming5").attr("disabled", false);
            $("#UCLstToTimming5").attr("disabled", false);
            $('#UCLstFromTimming5').val("Open 24 Hrs");
            $('#UCLstToTimming5').val("Open 24 Hrs");
        }
    } else if (Value == "UCLstToTimmingClosed6") {
        if ($("#" + Value).prop('checked') == true) {
            $("#UCLstFromTimming6").attr("disabled", true);
            $("#UCLstToTimming6").attr("disabled", true);
            $('#UCLstFromTimming6').val("Closed");
            $('#UCLstToTimming6').val("Closed");
        }
        else {
            $("#UCLstFromTimming6").attr("disabled", false);
            $("#UCLstToTimming6").attr("disabled", false);
            $('#UCLstFromTimming6').val("Open 24 Hrs");
            $('#UCLstToTimming6').val("Open 24 Hrs");
        }
    } else if (Value == "UCLstToTimmingClosed7") {
        if ($("#" + Value).prop('checked') == true) {
            $("#UCLstFromTimming7").attr("disabled", true);
            $("#UCLstToTimming7").attr("disabled", true);
            $('#UCLstFromTimming7').val("Closed");
            $('#UCLstToTimming7').val("Closed");
        }
        else {
            $("#UCLstFromTimming7").attr("disabled", false);
            $("#UCLstToTimming7").attr("disabled", false);
            $('#UCLstFromTimming7').val("Open 24 Hrs");
            $('#UCLstToTimming7').val("Open 24 Hrs");
        }
    }

}

function handleFileSelect() {
    //Check File API support
    if (window.File && window.FileList && window.FileReader) {

        var files = event.target.files; //FileList object
        var output = document.getElementById("UMImageLink");

        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            //Only pics
            if (!file.type.match('image')) continue;

            var picReader = new FileReader();
            picReader.addEventListener("load", function (event) {
                var totalimg = $("#UMImageLink> figure").length;
                totalimg += 1;
                var Value = ReturnId();
                var Id = "UMtargetImg" + Value;
                var picFile = event.target;
                var div = document.createElement("figure");
                var clsItem = document.createAttribute("itemprop");
                clsItem.value = "associatedMedia";
                div.setAttributeNode(clsItem);

                var clsscopet = document.createAttribute("itemtype");
                clsscopet.value = "http://schema.org/ImageObject";
                div.setAttributeNode(clsscopet);

                var clsscope = document.createAttribute("itemscope");
                clsscope.value = "";
                div.setAttributeNode(clsscope);
                div.innerHTML = '<div class="parent"><img class="img - thumbnail img - fluid"  itemprop="thumbnail" alt="Image description" value="' + Value + '"  id="' + Id + '" style="width:270px; height:138px;" /><button class="close" id="btnRemoveImage" onclick="RemoveImage(' + Id + ')">x</button></div></figure > &nbsp;&nbsp;';
                output.insertBefore(div, null);
                $("#" + Id).attr('src', picFile.result);
            });
            //Read the image
            picReader.readAsDataURL(file);
        }
    } else {
        console.log("Your browser does not support File API");
    }
}

function ReturnId() {
    var J = 1;
    var IDs = [];
    $(".parent").find("img").each(function () { IDs.push(this.attributes.value.value); });

    for (var i = 0; i < IDs.length; i++) {

        var Id = jQuery.inArray(J.toString(), IDs);
        if (Id == -1) {
            return J;
        }
        else {
            J += 1;
        }
    }
    return J;

}
if (document.getElementById('UMimagefiles') != null) {
    document.getElementById('UMimagefiles').addEventListener('change', handleFileSelect, false);
}

//-----------------------------------------------------End sub category on change category -----------------------------------
$("#UCLstState").change(function () {
    $('#UCLstCity').val('').trigger('change');
    $("#UCLstBuildingAddress").val('');
    $("#UCLstStreetAddress").val('');
    $("#UCLstLandMark").val('');
    $("#UCLstLatitude").val('');
    $("#UCLstLongitude").val('');
    $("#UCLstAddress").val('');
    var BindAddCity = $("#UCLstCity");
    BindAddCity.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');

    if ($("#UCLstState").val() > 0) {

        
        $.ajax({
            url: "/ClientListing/GetCityListById",
            dataType: 'json',
            delay: 250,
            type: "get",
            data: { Id: $("#UCLstState").val() },
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {

                BindAddCity.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response, function (i) {

                    BindAddCity.append($("<option></option>").val(response[i].id).html(response[i].name));
                });
            },

            cache: true
        });
    }
    else {
        BindAddCity.empty().append('<option selected="selected" value="">Please select</option>');
    }
});

$("#UCLstCity").change(function () {
    $("#UCLstBuildingAddress").val('');
    $("#UCLstStreetAddress").val('');
    $("#UCLstLandMark").val('');
    $("#UCLstLatitude").val('');
    $("#UCLstLongitude").val('');
    $("#UCLstAddress").val('');
    var BindAddCityArea = $("#UCLstCityArea");
    BindAddCityArea.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');

    if ($("#UCLstCity").val() > 0) {

        
        $.ajax({
            url: "/ClientListing/GetCityAreaById",
            dataType: 'json',
            delay: 250,
            type: "get",
            data: { Id: $("#UCLstCity").val() },
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {

                BindAddCityArea.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response, function (i) {

                    BindAddCityArea.append($("<option></option>").val(response[i].id).html(response[i].name));
                });
            },

            cache: true
        });
    }
    else {
        BindAddCityArea.empty().append('<option selected="selected" value="">Please select</option>');
      }
});

//-----------------------------------------------------Fill sub category on change category -----------------------------------
$("#UCLstCategory").change(function () {

    if ($("#UCLstCategory").val() > 0) {
        $("#UCLstSubCategory").val('');
        $("#UCLstSubCategory").select2({
            placeholder: "Please Select",
            ajax: {
                url: "/ClientListing/LoadAddSubMenuCategories",
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
                        CategoryId: $("#UCLstCategory").val(),
                    };
                },
                processResults: function (data, params) {
                    params.page = params.page || 1;

                    return {
                        results: data.subMenuCategoryList,
                        pagination: {
                            more: (params.page * 10) < data.rowCount
                        }
                    };
                },
                cache: true
            },
            escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
            minimumInputLength: 1,
            multiple: true,
        });
    }

});


$('#UCLstBusinessTypes').select2({
    placeholder: "Please Select",
    ajax: {
        url: '/ClientListing/LoadBussinessType',
        dataType: 'json',
        delay: 250,
        type: "get",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        processResults: function (data) {

            // Transforms the top-level key of the response object from 'items' to 'results'
            return {
                results: data
            };
        }
    },
    minimumInputLength: 1,
});
    
function RemoveImage(Id) {

    var ParentAttri = $(Id).parent().parent();
  
    var x = confirm("Do you want to delete Gallery image?");
    if (x == true) {
        var Pathcheck = Id.attributes.src.value;
        var check = Pathcheck.includes("data:image/");
        if (check == false) {
            $.ajax({
                url: "/ClientListing/DeleteGalleryImage",
                type: "Get",
                datatype: "json",
                async: false,
                data: { Url:Id.attributes.src.value,ImgId: Id.attributes.value.value},
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    
                    if (response > 0) {
                        
                        toastr.success("Gallery Image is deleted successfully", "Success");
                    }
                },
                error: function (response) {
                    
                    toastr.error(response, "Error");
                }
            });
        }
        $(ParentAttri).remove();
        
    }
}

function ResetCompanyListings() {

    $('#UCLstCustomer').val('').trigger('change');
    $("#UCLstCustomer").val('');
    $("#UCLstListingType").val('');
    $("#UCLstCompanyName").val('');
    $("#UCLstFirstName").val('');
    $("#UCLstLastName").val('');
    $("#UCLstMobileNumber").val('');
    $("#UCLstLandLineNumber").val('');    
    $("#UCLstWebSite").val('');
    $("#UCLstMetaTitle").val('');
    $("#UCLstMetaKeyword").val('');
    $("#UCLstMetaDescription").val('');
    $("#UCLstEmail").val('');
    $("#UCLstPassword").val('');
    //----------------------KeyWord--------------------
    $("#UCLstCategory").val('');
    $("#UCLstSubCategory").val('');
    $('#UCLstCategory').val('').trigger('change');
    $('#UCLstSubCategory').val('').trigger('change');
    $('#UCLstNoEmployee').val('').trigger('change');
    //-----------------------Location Infomation-------
    $('#UCLstState').val('').trigger('change');
    $('#UCLstCity').val('').trigger('change');
    $('#UCLstCityArea').val('').trigger('change');
    $("#UCLstBuildingAddress").val('');
    $("#UCLstStreetAddress").val('');
    $("#UCLstLandMark").val('');
    //$("#UCLstArea").val('');
    $("#UCLstLatitude").val('');
    $("#UCLstLongitude").val('');
    $("#UCLstAddress").val('');
    //----------------------Other Information----------
        
    $("#UCLstname").val('');
    $("#UCLstCertifications").val('');
    $("#UCLstBreifAbout").val('');
    $("#UCLstLocationOverview").val('');
        
    $('#UCLstPaymentsModes').val('').trigger('change');
    $('#UCLstServicetypes').val('').trigger('change');
    $('#UCLstBusinessTypes').val('').trigger('change');
        
    $('#UCLstFromTimming1').val('Open 24 Hrs').trigger('change');
    $('#UCLstToTimming1').val('Open 24 Hrs').trigger('change');
    $('#UCLstToTimmingClosed1').prop('checked', false);
    //  
    $('#UCLstFromTimming2').val('Open 24 Hrs').trigger('change');
    $('#UCLstToTimming2').val('Open 24 Hrs').trigger('change');
    $('#UCLstToTimmingClosed2').prop('checked', false);
    //  
    $('#UCLstFromTimming3').val('Open 24 Hrs').trigger('change');
    $('#UCLstToTimming3').val('Open 24 Hrs').trigger('change');
    $('#UCLstToTimmingClosed3').prop('checked', false);
    //  
    $('#UCLstFromTimming4').val('Open 24 Hrs').trigger('change');
    $('#UCLstToTimming4').val('Open 24 Hrs').trigger('change');
    $('#UCLstToTimmingClosed4').prop('checked', false);
    //  
    $('#UCLstFromTimming5').val('Open 24 Hrs').trigger('change');
    $('#UCLstToTimming5').val('Open 24 Hrs').trigger('change');
    $('#UCLstToTimmingClosed5').prop('checked', false);
    //  
    $('#UCLstFromTimming6').val('Open 24 Hrs').trigger('change');
    $('#UCLstToTimming6').val('Open 24 Hrs').trigger('change');
    $('#UCLstToTimmingClosed6').prop('checked', false);
    //  
    $('#UCLstFromTimming7').val('Open 24 Hrs').trigger('change');
    $('#UCLstToTimming7').val('Open 24 Hrs').trigger('change');
    $('#UCLstToTimmingClosed7').prop('checked', false);

    $('#UCLstFromTimming1').attr("disabled", false)
    $('#UCLstToTimming1').attr("disabled", false)
    $("#UCLstFromTimming2").attr("disabled", false);
    $("#UCLstToTimming2").attr("disabled", false);
    $("#UCLstFromTimming3").attr("disabled", false);
    $("#UCLstToTimming3").attr("disabled", false);

    $("#UCLstFromTimming4").attr("disabled", false);
    $("#UCLstToTimming4").attr("disabled", false);

    $("#UCLstFromTimming5").attr("disabled", false);
    $("#UCLstToTimming5").attr("disabled", false);

    $("#UCLstFromTimming6").attr("disabled", false);
    $("#UCLstToTimming6").attr("disabled", false);
    $("#UCLstFromTimming7").attr("disabled", false);
    $("#UCLstToTimming7").attr("disabled", false);

    $("#Uimagefiles").val('');
    $('#UtargetImg').attr("src", "/app-assets/images/portrait/small/avatar-s-19.jpg");

    $("#UMImageLink").children("figure").remove();
    $("#UAddMobileNumber").children("div").remove();
    $("#UAddLandNumber").children("div").remove();
    
}


