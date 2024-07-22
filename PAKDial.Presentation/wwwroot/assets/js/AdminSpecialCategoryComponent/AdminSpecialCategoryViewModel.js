$(function () {
    $('#AdminSpecialCategory').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminSpecialCategory/LoadSpecialCategory",
            type: "POST",
            datatype: "json",
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        },
        "columns": [
            {
                "data": "homeSecCat_Id",
                "name": "homeSecCat_Id",
                "orderable": true,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "homeSecCatName", "name": "homeSecCatName", "orderable": false },
            { "data": "mainMenuCatName", "name": "mainMenuCatName", "orderable": false },

        ],
        columnDefs: [
            { "width": "20%", "targets": 0 },
            { "width": "30%", "targets": 1 },
            { "width": "30%", "targets": 2 },
            {
                "width": "20%",
                targets: 3,
                render: function (data, type, full, meta) {
                    var Id = full.homeSecCat_Id + "-" + full.mainMenuCat_Id;
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteSpecialCategoryModal">Delete</button>'
                    return '<td> ' + Delete + ' </td>'
                }
            }
        ],

    });
});

//----------------------------------------------Add Special Category -----------------------------------------------------------------
// On Click Create Modal Opened
$("#AddSpecialCategoryModal").on('click', function () {
    var AddSpecialCategoryvalidator = $("#AddSpecialCategory_Submit_Form").validate();
    AddSpecialCategoryvalidator.resetForm();
    ReseSpecialCategoryIds();
    AddBindHomeCategory();
    AddBindMainCategory();
    $("#AddSpecialCategory").modal('show');

});

//Add Home Section Category Form Submit
$("#AddSpecialCategory_Submit_Form").validate({
    rules: {
        ASCHomeCatId: {
            required: true,
        },
        ASCMainCatId: {
            required: true,
        },
    },
    messages: {
        ASCHomeCatId: {
            required: "Please provide a Home Category."
        },
        ASCMainCatId: {
            required: "Please provide a Main Category."
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
        $("#AddSpecialCategoryShowLoader").show();
        $("#AddSpecialCategoryShowButtons").hide();
        var hscm = new HomeSecMainMenuCat();
        hscm.HomeSecCatId = $("#ASCHomeCatId").val();
        hscm.MainMenuCatId = $("#ASCMainCatId").val();
        $.ajax({
            url: "/AdminSpecialCategory/AddSpecialCategory",
            type: "post",
            datatype: "json",
            data: { homeSecMain: hscm },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == 1) {
                    $("#AddSpecialCategory").modal("hide");
                    $("#AddSpecialCategoryShowLoader").hide();
                    $("#AddSpecialCategoryShowButtons").show();
                    toastr.success("Special Category Saved Successfully", "Success");
                    $('#AdminSpecialCategory').DataTable().ajax.reload();
                }
                else if (response == -2) {
                    $("#AddSpecialCategoryShowLoader").hide();
                    $("#AddSpecialCategoryShowButtons").show();
                    toastr.warning("Special Category Already Exists", "Warning");
                }
                else if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else {
                    $("#AddSpecialCategoryShowLoader").hide();
                    $("#AddSpecialCategoryShowButtons").show();
                    toastr.error("Special Category Not Saved", "Error");
                }
            },
            error: function (response) {
                $("#AddSpecialCategoryShowLoader").hide();
                $("#AddSpecialCategoryShowButtons").show();
                toastr.error(response, "Error");
            }

        });
    }
});

//-----------------------------------------------------Delete Special Category --------------------------------------------------
// On Click Delete Modal Opened
$("#AdminSpecialCategory").on('click', '#DeleteSpecialCategoryModal', function () {
    var Id = $(this).val();
    var DeleteSpecialCategoryvalidator = $("#DeleteSpecialCategory_Submit_Form").validate();
    DeleteSpecialCategoryvalidator.resetForm();
    ReseSpecialCategoryIds();
    if (Id != "" && Id != null) {
        var getstrings = Id.split("-");
        $("#DSCHomeCatId").val(getstrings[0]);
        $("#DSCMainCatId").val(getstrings[1]);
        $("#DeleteSpecialCategory").modal('show');
    }
    else {
        toastr.error("Special Category Not Exits", "Error");
    }
});

//Delete Home Section Form Submit
$("#DeleteSpecialCategory_Submit_Form").validate({
    rules: {

    },
    messages: {

    },
    submitHandler: function (form) {
        $("#DeleteHomeSectionShowLoader").show();
        $("#DeleteSpecialCategoryShowButtons").hide();
        $.ajax({
            url: "/AdminSpecialCategory/DeleteSpecialCategory",
            type: "post",
            datatype: "json",
            data: { MainMenuCatId: $("#DSCMainCatId").val(), HomeSecCatId: $("#DSCHomeCatId").val()},
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == 1) {
                    $("#DeleteSpecialCategory").modal("hide");
                    $("#DeleteSpecialCategoryShowLoader").hide();
                    $("#DeleteSpecialCategoryShowButtons").show();
                    toastr.success("Special Category Deleted Successfully", "Success");
                    $('#AdminSpecialCategory').DataTable().ajax.reload();
                }
                else if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else {
                    $("#DeleteSpecialCategoryShowLoader").hide();
                    $("#DeleteSpecialCategoryShowButtons").show();
                    toastr.error("Special Category Not Deleted", "Error");
                }
            },
            error: function (response) {
                $("#DeleteSpecialCategoryShowLoader").hide();
                $("#DeleteSpecialCategoryShowButtons").show();
                toastr.error(response, "Error");
            }

        });
    }
});

//-----------------------------------------------GetList of Main Category and HomeSec Category--------------------------------------
function AddBindHomeCategory() {
    var BindAddHomeCategory = $("#ASCHomeCatId");
    BindAddHomeCategory.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
    $.ajax({
        url: "/AdminHomeSection/GetAllHomeSection",
        type: "get",
        datatype: "json",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            BindAddHomeCategory.empty().append('<option selected="selected" value="">Please select</option>');
            $.each(response, function (i) {
                BindAddHomeCategory.append($("<option></option>").val(this['id']).html(this['name']));
            });
        },
        error: function (response) {
            toastr.error(response, "Error");
        }

    });
}

//Get All Main Category For Binding With Add DropDown List
function AddBindMainCategory() {
    var BindAddMainCategory = $("#ASCMainCatId");
    BindAddMainCategory.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
    $.ajax({
        url: "/AdminMMCategory/GetAllMainCategory",
        type: "get",
        datatype: "json",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            BindAddMainCategory.empty().append('<option selected="selected" value="">Please select</option>');
            $.each(response, function (i) {
                BindAddMainCategory.append($("<option></option>").val(this['id']).html(this['name']));
            });
        },
        error: function (response) {
            toastr.error(response, "Error");
        }

    });
}

//-----------------------------------------------Reset------------------------------------------------------------------------------
function ReseSpecialCategoryIds() {

    $("#ASCHomeCatId").val('');
    $("#ASCMainCatId").val('');

    $("#DSCHomeCatId").val('');
    $("#DSCMainCatId").val('');
}