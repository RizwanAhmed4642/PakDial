var Id = $("#QueryListingId").val();
$(function () {
   
    $('#AdminQueryAudit').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminQueryTrack/LoadQueryTrack",
            type: "POST",
            data: { ListingId: Id },
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
            {
                "data": "auditName", "name": "auditName", "orderable": false
            },

        ],
        columnDefs: [
            { "width": "20%", "targets": 0 },
            { "width": "80%", "targets": 1 },
           
        ],

    }); 
 
});

$("#AddLead_Submit_Form").validate({
    rules: {
        
        AFromDate: {
            required: true,
            StartDateFromCurrentDate: function () {
              
                if (($("#AFromDate").val() > $("#AToDate").val() || $("#AFromDate").val() == $("#AToDate").val()) && $("#AToDate").val() !="") {
                    return true;
                }
           
            }
        },
        AToDate: {
            required: true,
            EndDateFromTodayDate: function () {
                if (($("#AToDate").val() < $("#AFromDate").val() || $("#AToDate").val() == $("#AFromDate").val()) && $("#AFromDate").val()!="") {
                    return true;
                }
            }
        },
    },
    messages: {
        
        AFromDate: {
            required: "From Date is Required.",
            StartDateFromCurrentDate: function () {
                if (($("#AFromDate").val() > $("#AToDate").val() || $("#AFromDate").val() == $("#AToDate").val()) && $("#AToDate").val() != "") {
                    return "Start Date Not Be Previous Date";
                }

            },
        },
        AToDate: {
            required: "To Date is Required",
            EndDateFromTodayDate: function () {
                if (($("#AToDate").val() < $("#AFromDate").val() || $("#AToDate").val() == $("#AFromDate").val()) && $("#AFromDate").val() != "") {
                    return "End Date Lesser Then Today";
                }
            }
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
            
            window.location.href = "/Home/GetLeadQueryReport?FromDate=" + $("#AFromDate").val() + "&ToDate=" + $("#AToDate").val() + "&ListingId=" + Id;
            $("#AFromDate").val("");
            $("#AToDate").val("");
           
        }
    
});

$("#btnShowLeadQueryReport").on('click', function (e) {
    var Addvalidator = $("#AddLead_Submit_Form").validate();
    Addvalidator.resetForm();
    $("#AFromDate").val("");
    $("#AToDate").val("");
    $("#ShowLeadQueryReprot").modal('show');

});


