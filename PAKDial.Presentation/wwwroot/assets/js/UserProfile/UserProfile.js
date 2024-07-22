$(function () {
    $.ajax({
        url: "/ProfileUser/GetCompanyListingByUserId",
        type: "get",
        datatype: "json",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
          
            $.each(response, function (i) {
            
                $('<div class="listing-detail"> <span class="listing-title">' + response[i].companyName + '</span><span class="edit-listing"><button type="button" value=' + response[i].id + ' name="Button"  class="btn btn-primary-lst"  onclick="EditListing(' + response[i].id + ');" id="btnedit">Edit</button></span></div>').insertBefore("#Divsubmit");
            });
        }
    });  
});

$('#SetCurrentFormate').text(function () {
    var str = $(this).html() + '';
    x = str.split('.');
    x1 = x[0]; x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    $(this).html(x1 + x2);
});

function EditListing(Id) {
   
    if (Id) {
        var Value = "Profile"
        window.location.href = "/ClientListing/EditCompanyListing?Id=" + Id + "&Value=" + Value;
    }  
}

$("#ViewAll").click(function () {
    window.location.href = "/ClientListing/Index";
});



