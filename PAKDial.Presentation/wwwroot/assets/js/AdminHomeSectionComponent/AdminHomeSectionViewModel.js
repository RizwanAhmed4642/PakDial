$(function () {
    $('#AdminHomeSection').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminHomeSection/LoadHomeSection",
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
            { "width": "15%", "targets": 1 },
            { "width": "10%", "targets": 2 },
            { "width": "20%", "targets": 3 },
            { "width": "20%", "targets": 4 },
            {
                "width": "20%",
                targets: 5,
                render: function (data, type, full, meta) {
                    var Id = full.id;
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateHomeSectionModal">&nbsp;Edit&nbsp;</button>'
                    var Delete = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + Id + ' id="DeleteHomeSectionModal">Delete</button>'
                    return '<td> ' + Edit + " " + Delete + ' </td>'
                }
            }
        ],

    });
});

//----------------------------------------------------Add Home Section Category----------------------------------------------------
// On Click Create Modal Opened
$("#AddHomeSectionModal").on('click', function () {
    var AddHomeSectionvalidator = $("#AddHomeSection_Submit_Form").validate();
    AddHomeSectionvalidator.resetForm();
    ResetHomeSectionIds();
    $("#AddHomeSection").modal('show');

});

//Add Home Section Category Form Submit
$("#AddHomeSection_Submit_Form").validate({
    rules: {
        AHSName: {
            required: true,
        },
        AHSDescription: {
            required: true,
        },
    },
    messages: {
        AHSName: {
            required: "Please provide a Name."
        },
        AHSDescription: {
             required: "Please provide a Description."
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
        $("#AddHomeSectionShowLoader").show();
        $("#AddHomeSectionShowButtons").hide();
        var hsc = new HomeSectionCategory();
        hsc.Name = $("#AHSName").val();
        hsc.Description = $("#AHSDescription").val();
        hsc.IsActive = true;
        $.ajax({
            url: "/AdminHomeSection/AddHomeSection",
            type: "post",
            datatype: "json",
            data: { homeSection: hsc },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == 1) {
                    $("#AddHomeSection").modal("hide");
                    $("#AddHomeSectionShowLoader").hide();
                    $("#AddHomeSectionShowButtons").show();
                    toastr.success("Home Category Saved Successfully", "Success");
                    $('#AdminHomeSection').DataTable().ajax.reload();
                }
                else if (response == -2) {
                    $("#AddHomeSectionShowLoader").hide();
                    $("#AddHomeSectionShowButtons").show();
                    toastr.warning("Home Category Already Exists", "Warning");
                }
                else if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else {
                    $("#AddHomeSectionShowLoader").hide();
                    $("#AddHomeSectionShowButtons").show();
                    toastr.error("Home Category Not Saved", "Error");
                }
            },
            error: function (response) {
                $("#AddHomeSectionShowLoader").hide();
                $("#AddHomeSectionShowButtons").show();
                toastr.error(response, "Error");
            }

        });
    }
});

//----------------------------------------------------Update Home Section Category----------------------------------------------------
// On Click Update Modal Opened
$("#AdminHomeSection").on('click', '#UpdateHomeSectionModal', function () {
    var Id = $(this).val();
    var UpdateHomeSectionvalidator = $("#UpdateHomeSection_Submit_Form").validate();
    UpdateHomeSectionvalidator.resetForm();
    ResetHomeSectionIds();
    if (Id != "" && Id != null) {
        $.ajax({
            url: "/AdminHomeSection/GetHomeSectionById",
            type: "get",
            datatype: "json",
            data: { Id: Id },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response != null) {
                    $("#UHSId").val(response.id);
                    $("#UHSName").val(response.name);
                    $("#UHSDescription").val(response.description);
                    if (response.isActive == true) {
                        $("#UHSIsActive").prop('checked', true);
                    }
                    $("#UpdateHomeSection").modal('show');
                }
                else {
                    toastr.error("Home Category Not Exits", "Error");
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }

});

//Update Home Section Category Form Submit
$("#UpdateHomeSection_Submit_Form").validate({
    rules: {
        UHSId: {
            required: true,
        },
        AHSName: {
            required: true,
        },
        AHSDescription: {
            required: true,
        },
    },
    messages: {
        UHSId: {
            required: "Please provide Passcode."
        },
        AHSName: {
            required: "Please provide a Name."
        },
        AHSDescription: {
            required: "Please provide a Description."
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
        $("#UpdateHomeSectionShowLoader").show();
        $("#UpdateHomeSectionShowButtons").hide();
        var hsc = new HomeSectionCategory();
        hsc.Id = $("#UHSId").val();
        hsc.Name = $("#UHSName").val();
        hsc.Description = $("#UHSDescription").val();
        if ($("#UHSIsActive").prop('checked') == true) {
            hsc.IsActive = true;
        }
        $.ajax({
            url: "/AdminHomeSection/UpdateHomeSection",
            type: "post",
            datatype: "json",
            data: { homeSection: hsc },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == 1) {
                    $("#UpdateHomeSection").modal("hide");
                    $("#UpdateHomeSectionShowLoader").hide();
                    $("#UpdateHomeSectionShowButtons").show();
                    toastr.success("Home Category Updated Successfully", "Success");
                    $('#AdminHomeSection').DataTable().ajax.reload();
                }
                else if (response == -2) {
                    $("#UpdateHomeSectionShowLoader").hide();
                    $("#UpdateHomeSectionShowButtons").show();
                    toastr.warning("Home Category Already Exists", "Warning");
                }
                else if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else {
                    $("#UpdateHomeSectionShowLoader").hide();
                    $("#UpdateHomeSectionShowButtons").show();
                    toastr.error("Home Category Not Updated", "Error");
                }
            },
            error: function (response) {
                $("#UpdateHomeSectionShowLoader").hide();
                $("#UpdateHomeSectionShowButtons").show();
                toastr.error(response, "Error");
            }

        });
    }
});

//-----------------------------------------------------Delete Home Section Category --------------------------------------------------
// On Click Delete Modal Opened
$("#AdminHomeSection").on('click', '#DeleteHomeSectionModal', function () {
    var Id = $(this).val();
    var DeleteHomeSectionvalidator = $("#DeleteHomeSection_Submit_Form").validate();
    DeleteHomeSectionvalidator.resetForm();
    ResetHomeSectionIds();
    if (Id != "" && Id != null) {
        $("#DHSId").val(Id);
        $("#DeleteHomeSection").modal('show');
    }
    else {
        toastr.error("Home Section Not Exits", "Error");
    }
});

//Delete Home Section Form Submit
$("#DeleteHomeSection_Submit_Form").validate({
    rules: {

    },
    messages: {

    },
    submitHandler: function (form) {
        $("#DeleteHomeSectionShowLoader").show();
        $("#DeleteHomeSectionShowButtons").hide();
        $.ajax({
            url: "/AdminHomeSection/DeleteHomeSection",
            type: "post",
            datatype: "json",
            data: { Id: $("#DHSId").val() },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response == 1) {
                    $("#DeleteHomeSection").modal("hide");
                    $("#DeleteHomeSectionShowLoader").hide();
                    $("#DeleteHomeSectionShowButtons").show();
                    toastr.success("Home Section Deleted Successfully", "Success");
                    $('#AdminHomeSection').DataTable().ajax.reload();
                }
                else if (response == 2) {
                    $("#DeleteHomeSectionShowLoader").hide();
                    $("#DeleteHomeSectionShowButtons").show();
                    toastr.warning("Please Delete its Childs", "Warning");
                }
                else if (response.error == "403") {
                    location.href = "/Account/AccessDenied";
                }
                else {
                    $("#DeleteHomeSectionShowLoader").hide();
                    $("#DeleteHomeSectionShowButtons").show();
                    toastr.error("Home Section Not Deleted", "Error");
                }
            },
            error: function (response) {
                $("#DeleteHomeSectionShowLoader").hide();
                $("#DeleteHomeSectionShowButtons").show();
                toastr.error(response, "Error");
            }

        });
    }
});
//----------------------------------------------------------Reset---------------------------------------------------------------------
function ResetHomeSectionIds() {
    $("#AHSName").val('');
    $("#AHSDescription").val('')

    $("#UHSId").val('');
    $("#UHSName").val('');
    $("#UHSDescription").val('');
    $("#UHSIsActive").val('')

    $("#DHSId").val('');
}