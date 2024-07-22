var btnCLSave = "";
$(document).ready(function () {
    var BindAddListingType = $("#ACLstListingType");
    BindAddListingType.empty();
    var BindAddMainCategory = $("#ACLstCategory");
    BindAddMainCategory.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
    var BindAddState = $("#ACLstState");
    BindAddState.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
   
    $("#ACLstState").attr("disabled", false);
    $("#ACLstCity").attr("disabled", false);

    $.ajax({
        url: "/ClientListing/LoadClientCompanyListingList",
        type: "get",
        datatype: "json",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            BindAddListingType.empty();
            $.each(response.listingTypes, function (i) {
                if (this['text'] == 'Free') {

                    BindAddListingType.append($("<option selected='selected'></option>").val(this['id']).html(this['text']));
                }
            });
            BindAddMainCategory.empty().append('<option selected="selected" value="">Please select</option>');
            $.each(response.categories, function (i) {
                BindAddMainCategory.append($("<option></option>").val(this['id']).html(this['text']));
            });
            BindAddState.empty().append('<option selected="selected" value="">Please select</option>');
            $.each(response.states, function (i) {
                BindAddState.append($("<option></option>").val(this['id']).html(this['text']));
            });
            if (response.stateId > 0) {
                var BindAddCityArea = $("#ACLstCityArea");
              
                $("#ACLstState").find('option[value="' + response.stateId + '"]').attr('selected', 'selected');
                $("#ACLstState").trigger('change');
                $("#ACLstCity").find('option[value="' + response.cityId + '"]').attr('selected', 'selected');
                BindAddCityArea.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response.cityAreaKeyValue, function (i) {
                    BindAddCityArea.append($("<option></option>").val(this['id']).html(this['text']));
                });
                $("#ACLstState").attr("disabled", true);
                $("#ACLstCity").attr("disabled", true);
            }
         
            $.each(response.socialMediaModes, function (i) {
                $("#AddSocialMediaModesIds").append('<div class="form row"><div class="form-col col-md-3"><label id="ACLstSocialMediaNodesName' + this['id'] + '"><img src=' + this['imageDir'] + ' />' + this['name'] + '</label></div><div class="form-col col-md-9"><input type="Text" class="form-control" name="ACLstSocialMediaNodes' + this['id'] + '" id="ACLstSocialMediaNodes' + this['id'] + '" placeholder="https://www.' + this['name'] + '.com/"></div></div>');
            });

        },
        error: function (response) {
            toastr.error(response, "Error");
        }

    });

    $("#ACLstCategory").select2({ placeholder: "Please Select" });
    $("#ACLstState").select2({ placeholder: "Please Select" });
    $("#ACLstSubCategory").select2({ placeholder: "Please Select" });
   
    $('#ACLstBusinessTypes').select2({
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
    
    $("#ACLstPaymentsModes").select2({
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

    $("#ACLstServicetypes").select2({
        
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

    $('#AtargetImg').attr("src", "/app-assets/images/portrait/small/avatar-s-19.jpg");
   
});

$('ul#myTab li').click(function (e) {    
    var lObjTabValue = $(this).children('a').text();

    if (lObjTabValue == "Gallery") {
  
        $('#btnsubmit').css('visibility', 'visible');
    } else {
        $('#btnsubmit').css('visibility', 'hidden');
    }
});
function GetCheckBoxValue(Id) {

    var Value = $("#" + Id).val();
    if (Value == "ACLstToTimmingClosed1") {
       
        if ($("#" + Value).prop('checked') == true) {
            $('#ACLstFromTimming1').attr("disabled", true);
            $('#ACLstToTimming1').attr("disabled", true);
            $('#ACLstFromTimming1').val("Closed");
            $('#ACLstToTimming1').val("Closed");
        }
        else {
            $('#ACLstFromTimming1').attr("disabled", false)
            $('#ACLstToTimming1').attr("disabled", false) 
            $('#ACLstFromTimming1').val("Open 24 Hrs");
            $('#ACLstToTimming1').val("Open 24 Hrs");
        }    
    } else if (Value == "ACLstToTimmingClosed2") {
        
        if ($("#" + Value).prop('checked') == true) {
            $("#ACLstFromTimming2").attr("disabled", true) ;
            $("#ACLstToTimming2").attr("disabled", true);
            $('#ACLstFromTimming2').val("Closed");
            $('#ACLstToTimming2').val("Closed");
        }
        else {
            $("#ACLstFromTimming2").attr("disabled", false);
            $("#ACLstToTimming2").attr("disabled", false);
            $('#ACLstFromTimming2').val("Open 24 Hrs");
            $('#ACLstToTimming2').val("Open 24 Hrs");
        }    
    } else if (Value == "ACLstToTimmingClosed3") {
        if ($("#" + Value).prop('checked') == true) {
            $("#ACLstFromTimming3").attr("disabled", true);
            $("#ACLstToTimming3").attr("disabled", true);
            $('#ACLstFromTimming3').val("Closed");
            $('#ACLstToTimming3').val("Closed");
        }
        else {
            $("#ACLstFromTimming3").attr("disabled", false);
            $("#ACLstToTimming3").attr("disabled", false);
            $('#ACLstFromTimming3').val("Open 24 Hrs");
            $('#ACLstToTimming3').val("Open 24 Hrs");
        }    
    } else if (Value == "ACLstToTimmingClosed4") {
        if ($("#" + Value).prop('checked') == true) {
            $("#ACLstFromTimming4").attr("disabled", true);
            $("#ACLstToTimming4").attr("disabled", true);
            $('#ACLstFromTimming4').val("Closed");
            $('#ACLstToTimming4').val("Closed");
        }
        else {
            $("#ACLstFromTimming4").attr("disabled", false);
            $("#ACLstToTimming4").attr("disabled", false);
            $('#ACLstFromTimming4').val("Open 24 Hrs");
            $('#ACLstToTimming4').val("Open 24 Hrs");
        }    
    } else if (Value == "ACLstToTimmingClosed5") {
        if ($("#" + Value).prop('checked') == true) {
            $("#ACLstFromTimming5").attr("disabled", true);
            $("#ACLstToTimming5").attr("disabled", true);
            $('#ACLstFromTimming5').val("Closed");
            $('#ACLstToTimming5').val("Closed");
        }
        else {
            $("#ACLstFromTimming5").attr("disabled", false);
            $("#ACLstToTimming5").attr("disabled", false);
            $('#ACLstFromTimming5').val("Open 24 Hrs");
            $('#ACLstToTimming5').val("Open 24 Hrs");
        }    
    } else if (Value == "ACLstToTimmingClosed6") {
        if ($("#" + Value).prop('checked') == true) {
            $("#ACLstFromTimming6").attr("disabled", true);
            $("#ACLstToTimming6").attr("disabled", true);
            $('#ACLstFromTimming6').val("Closed");
            $('#ACLstToTimming6').val("Closed");
        }
        else {
            $("#ACLstFromTimming6").attr("disabled", false);
            $("#ACLstToTimming6").attr("disabled", false);
            $('#ACLstFromTimming6').val("Open 24 Hrs");
            $('#ACLstToTimming6').val("Open 24 Hrs");
        }    
    } else if (Value == "ACLstToTimmingClosed7") {
        if ($("#" + Value).prop('checked') == true) {
            $("#ACLstFromTimming7").attr("disabled", true);
            $("#ACLstToTimming7").attr("disabled", true);
            $('#ACLstFromTimming7').val("Closed");
            $('#ACLstToTimming7').val("Closed");
        }
        else {
            $("#ACLstFromTimming7").attr("disabled", false);
            $("#ACLstToTimming7").attr("disabled", false);
            $('#ACLstFromTimming7').val("Open 24 Hrs");
            $('#ACLstToTimming7').val("Open 24 Hrs");
        }    
    }

}
//-----------------------------------------------------Selection change event--------------------------------------------------
$("input[name='inlineRadioOptions']").change(function () {
    
    var Rad = $("input[name='inlineRadioOptions']:checked").val();
  var Form=  $("#AddCompanyListings_Submit_Form").validate();
    Form.resetForm();
    ResetCompanyListings();
    if (Rad == "New") {
        $("#ACLstCust").hide();
        $("#ACLstlogInInfo").show();
    }
    else if (Rad == "Exist" || Rad == "Generic") {
        ResetCompanyListings();
        
        $("#ACLstlogInInfo").hide();
        $("#ACLstCust").show();
        $("#ACLstCustomer").val("");
        $("#ACLstCustomer").text("");

        $("#ACLstCustomer").select2({
            placeholder: "Please Select",
            ajax: {
                url: "/AdminCompanyListings/LoadCustomersList",
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
                        isDefault: Rad == "Exist" ? false : true,
                    };
                },
                processResults: function (data, params) {
                    params.page = params.page || 1;

                    return {
                        results: data.customerList,
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
    }
    $("#btnSaveNew").attr("style", "");
    $("#btnSaveClose").attr("style", "");
});
//-----------------------------------------------------End change event--------------------------------------------------

//-----------------------------------------------------Fill sub category on change category -----------------------------------
$("#ACLstCategory").change(function () {

    $("#ACLstSubCategory").val('');
    if ($("#ACLstCategory").val() > 0) {

        $("#ACLstSubCategory").select2({
            placeholder: "Please Select",
            ajax: {
                url: "/ClientListing/LoadClientAddSubMenuCategories",
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
                        CategoryId: $("#ACLstCategory").val(),
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
//-----------------------------------------------------End sub category on change category -----------------------------------

//----------------------------------------------------Fill city on satate change-----------------------------------------------

$("#ACLstState").change(function () {
   
    $("#ACLstBuildingAddress").val('');
    $("#ACLstStreetAddress").val('');
    $("#ACLstLandMark").val('');
    $("#ACLstLatitude").val('');
    $("#ACLstLongitude").val('');
    $("#ACLstAddress").val('');

    $('#ACLstCity').val('').trigger('change');

    var BindAddCity = $("#ACLstCity");
    BindAddCity.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>'); 
    //$("#ACLstCity").select2({ placeholder: "Please Select" });
    if ($("#ACLstState").val() > 0) {
        $.ajax({
            url: "/ClientListing/GetCityListById",
            dataType: 'json',
            delay: 250,
            type: "get",
            data: { Id: $("#ACLstState").val() },
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                //debugger;
                BindAddCity.empty().append('<option selected="selected" value="">Please select</option>');

                $.each(response, function (i) {

                    BindAddCity.append($("<option></option>").val(response[i].id).html(response[i].name));
                });
                $("#ACLstCity").select2({ placeholder: "Please Select" });
            },

            cache: true
        });
    }
    else {
        BindAddCity.empty().append('<option selected="selected" value="">Please select</option>');
    }
});

//----------------------------------------------------End city on satate change-----------------------------------------------

$("#ACLstCity").change(function () {
    var BindAddCityArea = $("#ACLstCityArea");
    BindAddCityArea.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');

    if ($("#ACLstCity").val() > 0) {

        //("#ACLstCityArea").select2({ placeholder: "Please Select" });
        $.ajax({
            url: "/ClientListing/GetCityAreaById",
            dataType: 'json',
            delay: 250,
            type: "get",
            data: { Id: $("#ACLstCity").val() },
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {

                BindAddCityArea.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response, function (i) {

                    BindAddCityArea.append($("<option></option>").val(response[i].id).html(response[i].name));
                });
                $("#ACLstCityArea").select2({ placeholder: "Please Select" });
            },

            cache: true
        });
    }
    else {
        BindAddCityArea.empty().append('<option selected="selected" value="">Please select</option>');
    }
});

//--------------------------------------------------- Add company listings------------------------------------------------------

$("#AddCompanyListings_Submit_Form").validate({

    ignore: false,
    rules: {    
        ACLstListingType: {
            required: true,
            numberonly: true
        },
        ACLstCompanyName: {
            required: true
           // letteronly: true
        },
        ACLstFirstName: {
            required: true,
            letteronly: true
        },
        ACLstLastName: {
            required: true,
            letteronly: true
        },
        ACLstMobileNumber: {
            required: true,
            numberonly: true,
            minlength: function () {
             
                var MobileMin = $('#ACLstMobileNumber').val();
                if (MobileMin.length <11) {
                    return 11;
                } 
            },
            maxlength: function () {
          
                var MobileMax = $('#ACLstMobileNumber').val();
                if (MobileMax.length >11) {
                    return 11;
                } 
            }
        },
        ACLstLandLineNumber: {
            numberonly: true,
            minlength: function () {
       
                var MobileMin = $('#ACLstLandLineNumber').val();
                if (MobileMin.length < 11) {
                    return 11;
                }
            },
            maxlength: function () {
               
                var MobileMax = $('#ACLstLandLineNumber').val();
                if (MobileMax.length > 11) {
                    return 11;
                }
            }
        },   
        ACLstState: {
            required: true
        },
        ACLstCity: {
        required: true
        },
        ACLstCityArea: {
            required: true
        },   
        ACLstAddress:{
        required: true
        },
        ACLstCategory: {
            required: true
        },
        ACLstSubCategory: {
            required: true
        }  
    },
    messages: {
        
        ACLstListingType: {
            required: "Please Select Listing Type."
        },
        ACLstCompanyName: {
            required: "Please Enter Company Name."
        },
        ACLstFirstName: {
            required: "Please Enter First Name"
        },
        ACLstLastName: {
            required: "Please Enter Last Name"
        },
        ACLstMobileNumber: {
            required: "Please enter mobileNo ."
        },      
        ACLstState: {
            required: "Please enter state."
        },
        ACLstCity: {
            required: "Please enter city."
        },
        ACLstCityArea: {
            required: "Please enter CityArea",

        },        
        ACLstAddress: {
            required: "Please enter Address",
        },
        ACLstCategory: {
            required: "Please select Main Category",
        },
        ACLstSubCategory: {
            required: "Please Select Sub Category",
        }          
    },
    highlight: function (element) {
 
            $(element).closest('.form-group').addClass('text-danger');
     
        if (btnCLSave == "SaveNew") {
          
            $("#btnSaveNew").attr("style", "");
        }
        else {
            $("#btnSaveClose").attr("style", "");
        }
    },
    unhighlight: function (element) {
       
            $(element).closest('.form-group').removeClass('text-danger');
      
        if (btnCLSave == "SaveNew") {

            $("#btnSaveNew").attr("style", "");
        }
        else {
            $("#btnSaveClose").attr("style", "");  
        }               
    },
    wrapper: 'div',
    errorClass: 'text-danger',
    errorPlacement: function (error, element) {
        
            var name = $(element).parent().attr("id");
            if (name == "MobileNumber") {
                error.insertAfter('#MobileNumber');
            } else if (name == "LandLineNumber") {
                error.insertAfter('#LandLineNumber');
            } else if (name == "RemovedivMobileNumber")
            {
                error.insertAfter('#RemovedivMobileNumber');
            }
            else if (name == "RemovedivLandLine")
            {
                error.insertAfter('#RemovedivLandLine');
            }
            else if (element.hasClass('select2')) {
                error.insertAfter(element.next('span'));
            }
            else {
                if (name != "MobileNumber" && name != "LandLineNumber" && name != "RemovedivMobileNumber" && name != "RemovedivLandLine") {
                    error.insertAfter(element);
                }
        }             
    },    
    submitHandler: function (form) {
        
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
        var CustomerRegistrationAdd = Registration();


        lObjComList.CompanyListings = fillCompanyList();
        lObjComList.CompanyListingProfile = CompanyListingProfileAdd != null ? CompanyListingProfileAdd : null;
        lObjComList.ListingAddress = CompanyAddressAdd != null ? CompanyAddressAdd : null;
        lObjComList.CompanyListingTimming = CompanyListingTimmingAdd.length > 0 ? CompanyListingTimmingAdd : null;
        lObjComList.ListingCategory = ListingCategoryAdd.length > 0 ? ListingCategoryAdd : null;
        lObjComList.ListingLandlineNo = ListingLandlineNoAdd.length > 0 ? ListingLandlineNoAdd : null;
        lObjComList.ListingMobileNo = ListingMobileNoAdd.length > 0 ? ListingMobileNoAdd : null;
        lObjComList.ListingPaymentsMode = ListingPaymentsModeAdd.length > 0 ? ListingPaymentsModeAdd : null;
        lObjComList.ListingServices = ListingServicesAdd.length > 0 ? ListingServicesAdd : null;
        lObjComList.ListingSocialMedia = ListingSocialMediaAdd.length > 0 ? ListingSocialMediaAdd : null;
        lObjComList.ListingsBusinessTypes = ListingsBusinessTypesAdd.length > 0 ? ListingsBusinessTypesAdd : null;
        lObjComList.CustomerRegistration = CustomerRegistrationAdd != null ? CustomerRegistrationAdd : null;

        $.ajax({
            url: "/ClientListing/AddCompanyListing",
            type: "post",
            datatype: "json",
            data: { companyListings: lObjComList },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
      
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }

                else if (response > 0) {
                 
                    var Sfile = $('#Aimagefiles').get(0).files;
                    var Mfile = $('#AMimagefiles').get(0).files;

                    var Sdata = getSelectedImages($('#Aimagefiles'));
                    var Mdata = getSelectedImages($('#AMimagefiles'));

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
                            success: function (response){

                            },
                            error: function (response) {
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
                                toastr.error(response, "Error");
                            }
                        });
                    }

                    GetButtonValue();
                    btnCLSave = undefined;
                }

                else if (response == 0)
                {
                    toastr.error("Company Listings is not Saved Successfully", "Error");
                }

                else {
                    GetButtonValue();
                    btnCLSave = undefined;
                }
            },
            error: function (response) {

                toastr.error(response, "Error");

            }            
        });
    }   
});

$('#btnSaveNew').click(function () {

    btnCLSave = $('#btnSaveNew').val();
    var AddListvalidator = $("#AddCompanyListings_Submit_Form").validate();
    AddListvalidator.resetForm();
    $("#AddCompanyListings_Submit_Form").valid();
    $(this).attr("style", "");
   
});

$('#btnSaveClose').click(function () {

    btnCLSave = $('#btnSaveClose').val();
    $(this).attr("style", "");
    var AddListvalidator = $("#AddCompanyListings_Submit_Form").validate();
    AddListvalidator.resetForm();
    $("#AddCompanyListings_Submit_Form").valid();
    $(this).attr("style", "");
   
});

function GetButtonValue() {
    //if (btnCLSave == "SaveNew") {
    //    toastr.success("Company Listings Saved Successfully", "Success");
    //    ResetCompanyListings();
    //    $("#AddCompanyListings_Submit_Form").trigger("reset");
    //    //$("#ACLstCustomer").select2("val", "");
       
    //}
    //else if (btnCLSave == "SaveClose") {
        $("#MainSection").hide();
        ResetCompanyListings();
        toastr.success("Company Listings Saved Successfully", "Success");
        window.location.href = "/ClientListing/Index";

       
   // }
}

$("#btnClose").click(function () {    
    $("#MainSection").hide();
    window.location.href = "/ClientListing/Index";
});

$("#btnCrossForm").click(function () {
    $("#MainSection").hide();
    window.location.href = "/ClientListing/Index";
});

//----- fill CompanyListings----------
function fillCompanyList() {
    var cL = new CompanyListings();
    cL.ListingTypeId = $("#ACLstListingType").val();
    cL.CompanyName = $("#ACLstCompanyName").val();
    cL.FirstName = $("#ACLstFirstName").val();
    cL.LastName = $("#ACLstLastName").val();
    cL.Website = $("#ACLstWebSite").val();
    cL.MetaTitle = $("#ACLstMetaTitle").val();
    cL.MetaKeyword = $("#ACLstMetaKeyword").val();
    cL.MetaDescription = $("#ACLstMetaDescription").val();
    var Rad = $("input[name='inlineRadioOptions']:checked").val();
    if (Rad != "New") {
        cL.CustomerId = $("#ACLstCustomer").val();
    }

    return cL;
}

//--------Company registratin---------
function Registration() {
    var Rg = new CCustomerRegistration();
    var Rad = $("input[name='inlineRadioOptions']:checked").val();
    if (Rad == "New") {
        Rg.Email = $("#ACLstEmail").val();
        Rg.Password = $("#ACLstPassword").val();
    }
    return Rg;
}

//----- fill CompanyListings-----------
function fillcompListingProfile() {
    var Profile = new CompanyListingProfile()
    Profile.YearEstablished = $("#ACLstYears").val();
    Profile.AnnualTurnOver = $("#ACLstAnnualTurnover").val();
    Profile.Certification = $("#ACLstCertifications").val();
    Profile.BriefAbout = $("#ACLstBreifAbout").val();
    Profile.LocationOverview = $("#ACLstLocationOverview").val();
    Profile.ProductAndServices = $("#ACLstProductAndServices").val();
    Profile.ProfessionalAssociation = $("#ACLstProfAss").val();
    Profile.NumberofEmployees = $("#ACLstNoEmployee").val();
    return Profile;
}

//----- fill company Address-----------
function CompanyListingAddress() {
    
    var Address = new ListingAddress();
    Address.StateId = $("#ACLstState").val();
    Address.CityId = $("#ACLstCity").val();
    Address.CityAreaId = $("#ACLstCityArea").val();
   // Address.Area = $("#ACLstArea").val();
    Address.BuildingAddress = $("#ACLstBuildingAddress").val();
    Address.StreetAddress = $("#ACLstStreetAddress").val();
    Address.LandMark = $("#ACLstLandMark").val();
    Address.Latitude = $("#ACLstLatitude").val();
    Address.Longitude = $("#ACLstLongitude").val();
    Address.LatLogAddress = $("#ACLstAddress").val();

    return Address;
}

//------ fill CompanyListingTimming-----
function CompanyTimming() {
    var ArrayTimming = [];
    for (var i = 1; i < 8; i++) {

        var Timming = new CompanyListingTimming();
        Timming.WeekDayNo = i;
        Timming.DaysName = $("#ACLstWeekDaysName" + i).text();
        Timming.TimeFrom = $("#ACLstFromTimming" + i).val();
        Timming.TimeTo = $("#ACLstToTimming" + i).val();
        if ($("#ACLstToTimmingClosed" + i).prop('checked') == true) {
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
    $('#ACLstSubCategory :selected').each(function () {
        var LC = new ListingCategory();
        LC.MainCategoryId = $("#ACLstCategory").val();
        LC.SubCategoryId = $(this).val();
        ArrayCategory.push(LC);
    });

    return ArrayCategory;
}

//--------fill ListingLandlineNo-------
function CompanyListingLandlineNo() {
    var ArraylLLN = [];
    if ($("#ACLstLandLineNumber").val() != '') {
        var lLLN = new ListingLandlineNo();
        lLLN.LandlineNumber = $("#ACLstLandLineNumber").val();
        lLLN.ListingId = '';
        ArraylLLN.push(lLLN);

        for (var i = 1; i < 2; i++) {
            var lLL = new ListingLandlineNo();
            lLL.LandlineNumber = $("#ACLstLandLineNumber" + i).val();
            lLL.ListingId = '';
            if (lLL.LandineNumber != "" && lLL.LandineNumber != undefined) {
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
    lMN.MobileNo = $("#ACLstMobileNumber").val();
    lMN.ListingId = '';
    ArraylMN.push(lMN);
    for (var i = 1; i < 2; i++) {
        var lMNN = new ListingMobileNo();
        lMNN.MobileNo = $("#ACLstMobileNumber" + i).val();
        lMNN.ListingId = '';
        if (lMNN.MobileNo != "" && lMNN.MobileNo != undefined) {
            ArraylMN.push(lMNN);
        }

    };
    return ArraylMN;
}

//-------------------ListingPaymentMode--------------
function CompanyPaymentMode() {

    var Arraylp = [];

    $('#ACLstPaymentsModes :selected').each(function () {
        var lP = new ListingPaymentsMode();
        lP.ModeId = $(this).val();
        Arraylp.push(lP);
    });
    return Arraylp;
}

//----------ListingServices---------------------
function CompanyListingServices() {

    var ArraylS = [];
    $('#ACLstServicetypes :selected').each(function () {
        var lS = new ListingServices();
        lS.ServiceTypeId = $(this).val();
        ArraylS.push(lS);
    });
    return ArraylS;
}

//----------ListingSocialMedia-----------
function CompanyListingSocialMedia() {

    var ArraylsM = [];

    for (var i = 1; i < 6; i++) {
        var lsM = new ListingSocialMedia();
        lsM.Name = $("#ACLstSocialMediaNodesName" + i).text();
        lsM.SitePath = $("#ACLstSocialMediaNodes" + i).val();
        ArraylsM.push(lsM);

    }

    return ArraylsM;
}

//----------Fill Business Type-----------
function CompanyListingsBusinessTypes() {
    var ArrayBT = [];

    $('#ACLstBusinessTypes :selected').each(function () {
        var BT = new ListingsBusinessTypes();
        BT.BusinessId = $(this).val();
        ArrayBT.push(BT);
    });
    return ArrayBT;
}

//---------------------------------------------------End company listings------------------------------------------------------

//-------------------------------------------------- Add Phone No and LandNo----------------------------------------------------------------------------------

$("#btnAddMobile").click(function () {

    if ($("#ACLstMobileNumber").val() == "") {
        alert("Please Enter Phone No first.");
        return;
    }

    if ($(":input[id^=ACLstMobileNumber1]").length == 1) {
        alert("Only Allow two Phone Number.");
        return;
    }
    //$("#AddMobileNumber").append('<div class="input-group mb-1" data-repeater-item="" style="" id="RemovedivMobileNumber"><input type="text" placeholder="0000 0000000" class="form-control" id="ACLstMobileNumber1" name="ACLstMobileNumber1" maxlength="11"><span class="input-group-append" id="button-addon2"><button class="btn btn-danger" id="RemoveMobileNo" onclick="RemoveMobNo()"  type="button" data-repeater-delete=""><i class="ft-minus"></i></button> </span></div>')
       
    $("#AddMobileNumber").append('<div class="input-group form-col form-spn-minus" data-repeater-item="" style="" id="RemovedivMobileNumber"><input type="text" placeholder="0000 000 0000" class="form-control" id="ACLstMobileNumber1" name="ACLstMobileNumber" maxlength="11" style="border: 1px solid rgb(194, 202, 216); background-color: white;" aria-invalid="false"><span class="input-group-append" id="button-addon2"><button class="btn btn-add-field-lst" onclick="RemoveMobNo()" id="RemoveMobileNo" type="button" data-repeater-delete=""> <i class="fa fa-minus"></i></button></span></div>')

         
    
    $("#ACLstMobileNumber1").rules("add", {
        required: true,
        numberonly: true,
        messages: {
            required: "Please enter Second Mobile Number",           
        }
    });
});

$("#btnAddLandNo").click(function () {
    if ($("#ACLstLandLineNumber").val() == "") {
        alert("Please Enter Land Line No first.");
        return;
    }
    if ($(":input[id^=ACLstLandLineNumber1]").length == 1) {
        alert("Only Allow two Landline Number.");
        return;
    }
    $("#AddLandNumber").append('<div class="input-group form-col form-spn-minus" data-repeater-item="" style="" id="RemovedivLandLine" > <input type="text" placeholder="04230000000" class="form-control" id="ACLstLandLineNumber1" name="ACLstLandLineNumber1" maxlength="11" style="border: 1px solid rgb(194, 202, 216); background-color: white;" aria-invalid="false"><span class="input-group-append" id="button-addon2"><button class="btn btn-add-field-lst" id="RemoveLandNo"  onclick="RemoveLandLineNo()"  type="button" data-repeater-delete=""> <i class="fa fa-minus" id="test"></i></button></span></div>')
    $("#ACLstLandLineNumber1").rules("add", {
        required: true,
        numberonly: true,
        messages: {
            required: "Please enter Second Landline Number",
        }
    });
});

function RemoveBannerImage() {
    $("#ACLTDeleteBannerImage").children("div").remove();
    $("#ACLTDeleteBannerImage").append('<div class="row"><figure class= "col-lg-12 col-md-12 col-sm-12 col-xs-12" itemprop = "associatedMedia" itemscope itemtype = "http://schema.org/ImageObject"><img class="img-fluid" itemprop="thumbnail" alt="Image description" id="AtargetImg" /><button class="close" onclick="RemoveBannerImage()">X</button></figure ></div >');
    $('#AtargetImg').attr("src", "/app-assets/images/portrait/small/avatar-s-19.jpg");
}
function RemoveLandLineNo() {
    $("#RemovedivLandLine").remove();
}

function RemoveMobNo() {
 
    $("#RemovedivMobileNumber").remove();

}

//---------------------------------------------------End Phone No and LandNo--------------------------------------------------------------

//---------------------------------------------------Update Company Listing---------------------------------------------------------------

//---------------------------------------------------Start Reset Form---------------------------------------------------------------------
function ResetCompanyListings() {
    
    $('#ACLstCustomer').val('').trigger('change');
    $("#ACLstCustomer").val('');
    $("#ACLstListingType").val('');
    $("#ACLstCompanyName").val('');
    $("#ACLstFirstName").val('');
    $("#ACLstLastName").val('');
    $("#ACLstWebSite").val('');
    $("#ACLstMetaTitle").val('');
    $("#ACLstMetaKeyword").val('');
    $("#ACLstMetaDescription").val('');
    $("#ACLstEmail").val('');
    $("#ACLstPassword").val('');
    $("#ACLstLandLineNumber").val('');
    $("#ACLstMobileNumber").val('');
    //-----------------------KeyWord--------------------
    $("#ACLstCategory").val('');
    $("#ACLstSubCategory").val('');
    $('#ACLstCategory').val('').trigger('change');
    $('#ACLstSubCategory').val('').trigger('change');
    $('#ACLstNoEmployee').val('').trigger('change');
    //------------------------Location Infomation-------
    $('#ACLstState').val('').trigger('change');
    $('#ACLstCity').val('').trigger('change');
    $("#ACLstBuildingAddress").val('');
    $("#ACLstStreetAddress").val('');
    $("#ACLstLandMark").val('');
    //$("#ACLstArea").val('');
    $("#ACLstLatitude").val('');
    $("#ACLstLongitude").val('');
    $("#ACLstAddress").val('');
    //-----------------------Other Information----------

    $("#ACLstname").val('');
    $("#ACLstCertifications").val('');
    $("#ACLstBreifAbout").val('');
    $("#ACLstLocationOverview").val('');

    $('#ACLstPaymentsModes').val('').trigger('change');
    $('#ACLstServicetypes').val('').trigger('change');
    $('#ACLstBusinessTypes').val('').trigger('change');

    $('#ACLstFromTimming1').val('Open 24 Hrs').trigger('change');
    $('#ACLstToTimming1').val('Open 24 Hrs').trigger('change');
    $('#ACLstToTimmingClosed1').prop('checked',false);
    //
    $('#ACLstFromTimming2').val('Open 24 Hrs').trigger('change');
    $('#ACLstToTimming2').val('Open 24 Hrs').trigger('change');
    $('#ACLstToTimmingClosed2').prop('checked', false);
    //
    $('#ACLstFromTimming3').val('Open 24 Hrs').trigger('change');
    $('#ACLstToTimming3').val('Open 24 Hrs').trigger('change');
    $('#ACLstToTimmingClosed3').prop('checked', false);
    //
    $('#ACLstFromTimming4').val('Open 24 Hrs').trigger('change');
    $('#ACLstToTimming4').val('Open 24 Hrs').trigger('change');
    $('#ACLstToTimmingClosed4').prop('checked', false);
    //
    $('#ACLstFromTimming5').val('Open 24 Hrs').trigger('change');
    $('#ACLstToTimming5').val('Open 24 Hrs').trigger('change');
    $('#ACLstToTimmingClosed5').prop('checked', false);
    //
    $('#ACLstFromTimming6').val('Open 24 Hrs').trigger('change');
    $('#ACLstToTimming6').val('Open 24 Hrs').trigger('change');
    $('#ACLstToTimmingClosed6').prop('checked', false);
    //
    $('#ACLstFromTimming7').val('Open 24 Hrs').trigger('change');
    $('#ACLstToTimming7').val('Open 24 Hrs').trigger('change');
    $('#ACLstToTimmingClosed7').prop('checked', false);

    $('#ACLstFromTimming1').attr("disabled", false)
    $('#ACLstToTimming1').attr("disabled", false)
    $("#ACLstFromTimming2").attr("disabled", false);
    $("#ACLstToTimming2").attr("disabled", false);
    $("#ACLstFromTimming3").attr("disabled", false);
    $("#ACLstToTimming3").attr("disabled", false);

    $("#ACLstFromTimming4").attr("disabled", false);
    $("#ACLstToTimming4").attr("disabled", false);

    $("#ACLstFromTimming5").attr("disabled", false);
    $("#ACLstToTimming5").attr("disabled", false);

    $("#ACLstFromTimming6").attr("disabled", false);
    $("#ACLstToTimming6").attr("disabled", false);
    $("#ACLstFromTimming7").attr("disabled", false);
    $("#ACLstToTimming7").attr("disabled", false);

    $("#Aimagefiles").val('');
    $('#AtargetImg').attr("src", "/app-assets/images/portrait/small/avatar-s-19.jpg");
    $("#AMImageLink").children("figure").remove();
    $("#AddMobileNumber").children("div").remove();
    $("#AddLandNumber").children("div").remove();

       
}
//---------------------------------------------------End Reset Form-----------------------------------------------------------------------
//document.querySelector('.close').addEventListener('click', function () {
//    alert("Are you want to delete Image?");
//});

//------------ Check Email --------------------
$("#ACLstEmail").focusout(function () {
    var Email = $(this).val();

    if (Email != "") {
        $.ajax({
            url: "/AdminCommon/CheckEmailExits",
            type: "get",
            datatype: "json",
            data: { Email: Email },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == 0) {
                }
                else {
                    toastr.error(response + " " + "Already Exits");
                    $("#ACLstEmail").val("");
                    $("#ACLstEmail").focus();
                }
            },
            error: function (response) {
                toastr.error(response);
            }

        });
    }
});

//------------ Check Mobile No --------------------
$("#ACLstMobileNumber").focusout(function () {

    var MobileNo = $(this).val();
    var SMNo = $("#ACLstMobileNumber1").val();

    if (SMNo != "") {
        if (MobileNo == SMNo) {
            alert("First and Second Number is same.");
            return;
        }
    }
    if (MobileNo != "") {
        $.ajax({
            url: "/AdminCommon/CheckMobileNoExits",
            type: "get",
            datatype: "json",
            data: { MobileNo: MobileNo,Id:0 },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == 0) {
                    toastr.error("Number Already Exits.");
                    $("#ACLstMobileNumber").val("");
                    $("#ACLstMobileNumber").focus();
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
$(document).on('focusout', '#ACLstMobileNumber1', function () {
    var MobileNo = $(this).val();
    var FMN = $("#ACLstMobileNumber").val();
    if (FMN != "") {
        if (MobileNo == FMN) {
            alert("Second and First Number is same.");
            return;
        }
    }
    var Id = 0;
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
                    $("#ACLstMobileNumber1").val("");
                    $("#ACLstMobileNumber1").focus();
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
$(document).on('focusout', '#ACLstLandLineNumber1', function () {
    var Landline = $(this).val();
    var SLLN = $("#ACLstLandLineNumber").val();
    if (SLLN != "") {
        if (Landline == SLLN) {
            alert("First and Second LandLine Number Same.");
            return;
        }
    }
    var Id = 0;
    if (Landline != "") {
        $.ajax({
            url: "/AdminCommon/CheckLandineNoExits",
            type: "get",
            datatype: "json",
            data: { LandLineNo: Landline, Id: Id},
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == 0) {
                    toastr.error("Number Already Exits.");
                    $("#ACLstLandLineNumber1").val("");
                    $("#ACLstLandLineNumber1").focus();
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
$("#ACLstLandLineNumber").focusout(function () {
    var Landline = $(this).val();
    var Id = 0;
    var SLLN = $("#ACLstLandLineNumber1").val();
    if (SLLN != "") {
        if (Landline == SLLN) {
            alert("Second and First LandLine Number is same.");
            return;
        }
    }
    if (Landline != "") {
        $.ajax({
            url: "/AdminCommon/CheckLandineNoExits",
            type: "get",
            datatype: "json",
            data: { LandLineNo: Landline, Id: Id },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == 0) {
                    toastr.error("Number Already Exits");
                    $("#ACLstLandLineNumber").val("");
                    $("#ACLstLandLineNumber").focus();
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

//------------ Load image------------------------------------------------------------------------------------------------------------------

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

//---------------------------------------------------Gallery Images-----------------------------------------------------------------------
//$("#AMimagefiles").change(function () {

//    var File = this.files;
//    if (File && File[0]) {
//        AMReadImage(File)
//    }

   
//});

function handleFileSelect() {
    //Check File API support
    if (window.File && window.FileList && window.FileReader) {

        var files = event.target.files; //FileList object
        var output = document.getElementById("AMImageLink");

        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            //Only pics
            if (!file.type.match('image')) continue;

            var picReader = new FileReader();
            picReader.addEventListener("load", function (event) {
               
                var totalimg = $("#AMImageLink> figure").length;          
                totalimg += 1;
                var Value = ReturnId();
                var Id = "AMtargetImg" + Value;
                var picFile = event.target;
                var div = document.createElement("figure");

                //var clsDiv = document.createAttribute("class");
                //clsDiv.value = "col-lg-12 col-md-12 col-sm-12 col-xs-12";
                //div.setAttributeNode(clsDiv);

                var clsItem = document.createAttribute("itemprop");
                clsItem.value = "associatedMedia";
                div.setAttributeNode(clsItem);

                var clsscopet = document.createAttribute("itemtype");
                clsscopet.value = "http://schema.org/ImageObject";
                div.setAttributeNode(clsscopet);

                var clsscope = document.createAttribute("itemscope");
                clsscope.value = "";
                div.setAttributeNode(clsscope);
                                             
                div.innerHTML = '<div class="parent"><img class="img - thumbnail img - fluid"  itemprop="thumbnail" alt="Image description" value="' + Value + '"  id="' + Id + '" style="width:270px; height:138px;" /><button class="close" id="btnRemoveImage" onclick="RemoveImage(' + Id +')">x</button></div></figure >&nbsp;&nbsp;';                             
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
if (document.getElementById('AMimagefiles') != null) {

    document.getElementById('AMimagefiles').addEventListener('change', handleFileSelect, false);
}
//$("#AMimagefiles").change(function () {

//    if (typeof (FileReader) != "undefined") {
//        var dvPreview = $("#AMImageLink");
//        dvPreview.html("");
        
//        $($(this)[0].files).each(function () {
//            var file = $(this);
//            var reader = new FileReader();
//            reader.onload = function (e) {
//                //var img = $("<img />");
//                //img.attr("style", "width: 150px; height:100px; padding: 10px");
//                //img.attr("src", e.target.result);
//                //dvPreview.append(img);
//                //var Button = $("<button>X</button>");
//                //Button.attr("class", "close");
//                //Button.attr("id", "btnRemoveImage");
//                //dvPreview.append(Button);
              
//                var totalimg = $("#AMImageLink> figure").length;;
//                totalimg += 1;
//                var Id = "AMtargetImg" +totalimg ;
//                $("#AMImageLink").append('<figure class="col-lg-3 col-md-6 col-sm-6 col-xs-12" id="imgfigure' + totalimg +'" itemprop="associatedMedia" itemscope itemtype="http://schema.org/ImageObject"> <div class="parent"><img class="img - thumbnail img - fluid" itemprop="thumbnail" alt="Image description" id="AMtargetImg' + totalimg + '"style="width:270px; height:138px;"/><button class="close"  id="btnRemoveImage" onclick="RemoveImage(' +Id+')">x</button></div></figure>')

//                $("#AMtargetImg" + totalimg).attr('src', e.target.result);
//            }
//            reader.readAsDataURL(file[0]);
//        });
//    } else {
//        alert("This browser does not support HTML5 FileReader.");
//    }
//});
//<a href="app-assets/images/gallery/1.jpg" itemprop="contentUrl" data-size="480x360"></a>
//var AMReadImage = function (file) {

//    var reader = new FileReader;
//    var image = new Image;
//    reader.readAsDataURL(file);
//    reader.onload = function (_file) {

//        image.src = _file.target.result;
//        image.onload = function () {
//            var name = this.name
//            var height = this.height;
//            var width = this.width;
//            var type = file.type;
//            for (var i = 0; i < file.length; i++) {

//                var size = ~~(file.size / 1024) + "KB";
//                var totalimg = $("#AMImageLink> figure").length;;
//                totalimg += 1;
//                $("#AMImageLink").append('<figure class="col-lg-3 col-md-6 col-sm-6 col-xs-12" itemprop="associatedMedia" itemscope itemtype="http://schema.org/ImageObject"><a href="app-assets/images/gallery/1.jpg" itemprop="contentUrl" data-size="480x360"><img class="img - thumbnail img - fluid" itemprop="thumbnail" alt="Image description" id="AMtargetImg' + totalimg + '" /><circle>x</circle></a></figure>')

//                $("#AMtargetImg" + totalimg).attr('src', _file.target.result);

//            }
//        }

//    }

//}
function RemoveImage(Id) {
 
    var ParentID = $(Id).parent().parent();
    var x = confirm("Do you want to delete Gallery image?");
    if (x == true) {
        $(ParentID).remove();
        toastr.success("Gallery Image is deleted successfully", "Success");
    }      
}



