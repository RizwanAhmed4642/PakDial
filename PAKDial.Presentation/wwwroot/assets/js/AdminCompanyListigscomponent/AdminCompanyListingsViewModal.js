$(function () {
    $('#AdminCompanyListings').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminCompanyListings/LoadCompanyListings",
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
            { "data": "companyName", "name": "companyName", "orderable": false },
            { "data": "fullName", "name": "fullName", "orderable": false },
            { "data": "listingType", "name": "listingType", "orderable": false },
            {
                "data": "listingStatus", "name": "listingStatus", "orderable": false,
                "render": function (data) {
                    if (data == true) {
                        return '<div class="badge badge-primary">Active</div>';
                    }
                    else {
                        return '<div class="badge badge-danger">InActive</div>';
                    }
                }
            },
            
            { "data": "mobileNumber", "name": "MobileNumber", "orderable": false },
        ],
        columnDefs: [
            { "width": "5%", "targets": 0 },
            { "width": "15%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "8%", "targets": 3 },
            { "width": "8%", "targets": 4 },
            { "width": "10%", "targets": 5 },
            {
                "width": "19%",
                targets: 6,
                render: function (data, type, full, meta) {
                    var Id = full.id;
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateCompanyListingsModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteCompanyListingsModal">Delete</button>'
                    var IsVerified = "";
                    var IsPremium = "";
                    var IsDefaultCustomer = "";
                    var Active = "";
                    var Queries = "";
                    if (full.verifiedList == 0) {
                        IsVerified = '<button type="button" class="btn mr-1 mb-1 btn-secondary btn-sm" value=' + Id + ' id="VerifyCompanyListingsModal">Verify</button>'
                    }
                    else {
                        IsVerified = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="VerifiedCompanyListingsModal">Verified</button>'
                    }
                    //Need Some Changes Start 
                    IsPremium = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateUnPaidModal">Payments</button>'
                    Queries = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateQueriesModal">Queries</button>'
                    //Need Some Changes Done End
                    if (full.defaultCustomer == true) {
                        IsDefaultCustomer = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateTransferOwnershipModel">Transfer</button>'
                    }

                    if (full.listingStatus == true) {
                        Active = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="InActiveCompanyListingsModal">InActive</button>'
                    }
                    else {
                        Active = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="ActiveCompanyListingsModal">Active</button>'

                    }
                    return '<td> ' + Edit + " " + Delete + "" + IsVerified + "" + IsPremium + " " + IsDefaultCustomer + "" + Active + "" + Queries +' </td>'
                }
            }
        ],

    });

    //Load Customers
    $("#UTransferCutIdId").select2({
        placeholder: "Please Select",
        allowClear: true,
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
                    isDefault: false,
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
});

//--------------------------------------------- Verify and Verified Listing ------------------------------------------------
// On Click Modal Opened and Verify
$("#AdminCompanyListings").on('click', '#VerifyCompanyListingsModal', function () {
    var Id = $(this).val();
    if (Id != "" && Id != null) {
        $.ajax({
            url: "/AdminCompanyListings/VerifyUnVerifyListing",
            type: "get",
            datatype: "json",
            async: false,
            data: { Id: Id, name: "Verify" },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else if (response == 1) {
                    toastr.success("Listing Verified Successfully", "Success");
                    $('#AdminCompanyListings').DataTable().ajax.reload();
                }
                else {
                    toastr.error("Company Listing Not Deleted", "Error");
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }

});

// On Click Modal Opened and UnVerify
$("#AdminCompanyListings").on('click', '#VerifiedCompanyListingsModal', function () {
    var Id = $(this).val();
    if (Id != "" && Id != null) {
        $.ajax({
            url: "/AdminCompanyListings/VerifyUnVerifyListing",
            type: "get",
            datatype: "json",
            async: false,
            data: { Id: Id, name: "UnVerify" },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else if (response == 1) {
                    toastr.success("Listing UnVerified Successfully", "Success");
                    $('#AdminCompanyListings').DataTable().ajax.reload();
                    
                }
                else {
                    toastr.error("Company Listing Not Unverified", "Error");
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }

});

//--------------------------------------InActive------------------------------------------------------------------------------
$("#AdminCompanyListings").on('click', '#InActiveCompanyListingsModal', function () {
    
    var Id = $(this).val();
    if (Id != "" && Id != null) {
        $.ajax({
            url: "/AdminCompanyListings/ActiveInActiveList",
            type: "get",
            datatype: "json",
            async: false,
            data: { Id: Id, Status: "Active" },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else if (response > 0) {
                   
                    toastr.success("Listing InActive Successfully", "Success");
                    $('#AdminCompanyListings').DataTable().ajax.reload();
                    //$("#InActiveCompanyListingsModal").val('Active');
                }
                else {
                    toastr.error("Company Listing Not Active", "Error");
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }
        });
    }
});

//-------------------------------------Active----------------------------------------------------------------------------------
$("#AdminCompanyListings").on('click', '#ActiveCompanyListingsModal', function () {
    var Id = $(this).val();
    if (Id != "" && Id != null) {
        $.ajax({
            url: "/AdminCompanyListings/ActiveInActiveList",
            type: "get",
            datatype: "json",
            async: false,
            data: { Id: Id, Status: "InActive" },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
             
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else if (response > 0) {
                    toastr.success("Listing Active Successfully", "Success");
                    $('#AdminCompanyListings').DataTable().ajax.reload();
                    //$("#InActiveCompanyListingsModal").val('InActive');
                }
                else {
                    toastr.error("Company Listing Not Active", "Error");
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }
        });
    }
});

//--------------------------------------------TransferCustomerOwnerShip--------------------------------------------------------

// On Click Modal Opened and Verify
$("#AdminCompanyListings").on('click', '#UpdateTransferOwnershipModel', function () {
    var Id = $(this).val();
    if (Id != "" && Id != null) {
        $.ajax({
            url: "/AdminCompanyListings/GetOwnerShip",
            type: "get",
            datatype: "json",
            async: false,
            data: { ListingId: Id },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else if (response.id > 0) {
                    $('#UTransferCutIdId').val('').trigger('change');
                    $("#UTransferListingsId").val(response.id);
                    $("#UTransferCompanyName").val(response.companyName);
                    $("#UTransferFullName").val(response.firstName + response.lastName);
                    $("#UpdateTransferOwnerShip").modal('show');
                }
                else {
                    toastr.error("Listing Not Availible for Transfer", "Error");
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }

});

// Customer Load without Default



$("#UpdateTransferOwnership_Submit_Form").validate({
    rules: {
        UTransferCutIdId: {
            required: true,
            numberonly: true
        }
    },
    messages: {
        UTransferCutIdId: {
            required: "Please Select Customer"
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
        $("#UpdateTransferOwnershipShowLoader").show();
        $("#UpdateTransferOwnershipShowButtons").hide();
        $.ajax({
            url: "/AdminCompanyListings/TransferCustomerOwnerShip",
            type: "post",
            datatype: "json",
            data: { CustomerId: $("#UTransferCutIdId").val(), ListingId: $("#UTransferListingsId").val() },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else if (response == 1) {
                    $("#UpdateTransferOwnerShip").modal("hide");
                    $("#UpdateTransferOwnershipShowLoader").hide();
                    $("#UpdateTransferOwnershipShowButtons").show();
                    toastr.success("Company Listings Transfer Successfully", "Success");
                    $('#AdminCompanyListings').DataTable().ajax.reload();
                }
                else {
                    $("#UpdateTransferOwnershipShowLoader").hide();
                    $("#UpdateTransferOwnershipShowButtons").show();
                    toastr.error("Company Listing Not Transfer", "Error");
                }
            },
            error: function (response) {
                $("#UpdateTransferOwnershipShowLoader").hide();
                $("#UpdateTransferOwnershipShowButtons").show();
                toastr.error(response, "Error");
            }

        });
    }
});

//---------------------------------------------------------------------------------------------------------------------

$("#AdminCompanyListings").on('click', '#UpdateCompanyListingsModal', function (e) {
    var Id = $(this).val();
    if (e.ctrlKey){
        var herf = "/AdminCompanyListings/EditCompanyListing?Id=" + Id;
           
        window.open(herf);
    }
    else {
        
        window.location.href = "/AdminCompanyListings/EditCompanyListing?Id=" + Id;

    }
 
});

$("#AdminCompanyListings").on('click', '#UpdateUnPaidModal', function () {
    var Id = $(this).val();
    window.location.href = "/AdminAssigningPackages/Index?Id=" + Id;
});

$("#AdminCompanyListings").on('click', '#UpdateQueriesModal', function () {
    var Id = $(this).val();
    window.location.href = "/AdminQueryTrack/Index?Id=" + Id;
});

//-------------------------------Delete Company------------------------------------------------------------------------------------------
$("#AdminCompanyListings").on('click', '#DeleteCompanyListingsModal', function () {
    var Id = $(this).val();
    var DeleteCompListingvalidator = $("#DeleteCompanyListings_Submit_Form").validate();
    DeleteCompListingvalidator.resetForm();

    if (Id != "" && Id != null) {
        $("#DCompanyListingsId").val(Id);
        $("#DeleteCompanyListings").modal('show');
    }
    else {
        toastr.error("Company Listings Not Exits", "Error");
    }

});


$("#DeleteCompanyListings_Submit_Form").validate({
    rules: {

    },
    messages: {

    },
    submitHandler: function (form) {
    
        $("#DeleteListingTypesShowLoader").show();
        $("#DeleteListingTypesShowButtons").hide();
        $.ajax({
            url: "/AdminCompanyListings/DeleteCompanyListing",
            type: "post",
            datatype: "json",
            data: { Id: $("#DCompanyListingsId").val() },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response > 1) {
                 
                    $("#DeleteCompanyListings").modal("hide");
                    $("#DeleteCompanyListingsShowLoader").hide();
                    $("#DeleteCompanyListingsShowButtons").show();
                    toastr.success("Company Listings Deleted Successfully", "Success");
                    $('#AdminCompanyListings').DataTable().ajax.reload();
                }
                else if (response == -2) {
                    $("#DeleteCompanyListingsShowLoader").hide();
                    $("#DeleteCompanyListingsShowButtons").show();
                    toastr.warning("Please Delete its Child Data", "Warning");
                }
                else if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else {
                    $("#DeleteCompanyListingsShowLoader").hide();
                    $("#DeleteCompanyListingsShowButtons").show();
                    toastr.error("Company Listing Not Deleted", "Error");
                }
            },
            error: function (response) {
                $("#DeleteCompanyListingsShowLoader").hide();
                $("#DeleteCompanyListingsShowButtons").show();
                toastr.error(response, "Error");
            }

        });
    }
});


//----------------------------------UpLoad Image--------------------------------------------------------


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



function ResetCompanyListings() {

    $("#UCLstCustomer").val('');
    $("#UCLstListingType").val('');
    $("#UCLstCompanyName").val('');
    $("#UCLstFirstName").val('');
    $("#UCLstLastName").val('');
    $("#UCLstWebSite").val('');
    $("#UCLstMetaTitle").val('');
    $("#UCLstMetaKeyword").val('');
    $("#UCLstMetaDescription").val('');
    $("#UCLstEmail").val('');
    $("#UCLstPassword").val('');
    //----------------------KeyWord--------------------
    $("#UCLstCategory").val('');
    $("#UCLstSubCategory").val('');

    //-----------------------Location Infomation-------
    $("#UCLstState").val('');
    $("#UCLstCity").val('');
    $("#UCLstBuildingAddress").val('');
    $("#UCLstStreetAddress").val('');
    $("#UCLstLandMark").val('');
    $("#UCLstArea").val('');
    $("#UCLstLatitude").val('');
    $("#UCLstLongitude").val('');
    $("#UCLstAddress").val('');
    //----------------------Other Information----------

    $("#UCLstname").val('');
    $("#UCLstCertifications").val('');
    $("#UCLstBreifAbout").val('');
    $("#UCLstLocationOverview").val('');
    $("#UCLstPaymentsModes").val('');
    $("#UCLstServicetypes").val('');

    $("#Uimagefiles").val('');
    $('#UtargetImg').attr("src", "");
}