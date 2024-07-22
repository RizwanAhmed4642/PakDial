var TotalResults = 10;
var Loc = 0;
var Gen = 0;
$(function () {
    var GetResult = getUrlVars();
    $("#LastCatesearchbycity").val(GetResult["CtName"]);
    $("#MobLastCatesearchbycity").val(GetResult["CtName"]);
    //Loc = parseInt($("#Loc").val());
    Gen = parseInt($("#Gen").val());
    //if (parseInt($("#ListingPageNumebr").val()) == 1 && Loc == 0) {
    //    setTimeout(function () {
    //        Loc = 1;
    //        SetLocTempData();
    //        $("#Loc").val(Loc);
    //        $("#locationModal").modal("show");
    //    }, 1000);
    //}
    if (parseInt($("#ListingPageNumebr").val()) == 1 && parseInt($("#ListingTotalItem").val()) > 0 && Gen == 0) {
        setTimeout(function () {
            $("#SbCateIdQuery").val($("#SbCId").val());
            $("#SbCateNameQuery").val($("#SbCName").val());
            $("#AreaNameQuery").val($("#ArName").val());
            $("#RProductQuery").val("");
            $("#RnameQuery").val("");
            $("#RnumberQuery").val("");
            $("#ListingIdQuery").val("");
            var Validator = $("#RQueryForm_Submit").validate();
            Validator.resetForm();
            Gen = 1;
            SetGenTempData();
            $("#Gen").val(Gen);
            $("#contactModalQuery").modal("show");
        }, 1500);
    }

    $("#locationModal").modal({
        show: false,
        backdrop: "static"
    });
});

//function SetLocTempData() {
//    $.ajax({
//        type: "POST",
//        url: "/Home/SetLocTempData",
//        data: { Loc:"1"},
//        dataType: "json",
//        success: function () {
//        }
//    });
//}

function SetGenTempData() {
    $.ajax({
        type: "POST",
        url: "/Home/SetGenTempData",
        data: { Gen:"1" },
        dataType: "json",
        success: function () {
        }
    });
}

$("#locationmodalclose").click(function () {
    $("#locationModal").modal("hide");
    if (parseInt($("#ListingPageNumebr").val()) == 1 && parseInt($("#ListingTotalItem").val()) > 0 && Gen == 0) {
        $("#SbCateIdQuery").val($("#SbCId").val());
        $("#SbCateNameQuery").val($("#SbCName").val());
        $("#AreaNameQuery").val($("#ArName").val());
        $("#RProductQuery").val("");
        $("#RnameQuery").val("");
        $("#RnumberQuery").val("");
        $("#ListingIdQuery").val("");
        var Validator = $("#RQueryForm_Submit").validate();
        Validator.resetForm();
        Gen = 1;
        SetGenTempData();
        $("#contactModalQuery").modal("show");
    }
});

//For Web
$('#LastCatesearchbycity').on("click", function () {
    $('#LastCatcitylistdrop').css('display', 'block');
});
//For Mobile
$('#MobileLastCatesearchbycity').on("click", function () {
    $('#MobileLastCatcitylistdrop').css('display', 'block');
});
//For Web
$("ul[id*=LastUserSubLocation] li").click(function () {
    var GetResult = getUrlVars();
    $("#LastCatesearchbycity").val($(this).text());
    var ss = GetResult["SbCId"];
    var location = $("#LastCatesearchbycity").val();
    window.location.href = "/Home/LoadCategoryDescription/?CtName=" + location + "&SbCId=" + GetResult["SbCId"] + "&SbCName=" + GetResult["SbCName"] + "&SortFilter=" + 'topResults' + "&Ratingstatus=" + 'Asc';
    $("#LastCatcitylistdrop").css("display", "none");
});
//For Mobile
$("ul[id*=MobLastUserSubLocation] li").click(function () {
    var GetResult = getUrlVars();
    $("#MobLastCatesearchbycity").val($(this).text());
    var ss = GetResult["SbCId"];
    var location = $("#MobLastCatesearchbycity").val();
    window.location.href = "/Home/LoadCategoryDescription/?CtName=" + location + "&SbCId=" + GetResult["SbCId"] + "&SbCName=" + GetResult["SbCName"] + "&SortFilter=" + 'topResults' + "&Ratingstatus=" + 'Asc';
    $("#MobLastCatcitylistdrop").css("display", "none");
});

////////////////////////////////////// Load Category//////////////////////////////////////////////////
//function LoadSubCategory(CatId, CatName) {
//    var location = $("#LastCatesearchbycity").val();
//    CatName = CatName.replace(/ /g, "_");
//    window.location.href = "/Product/" + location + "/" + CatId + "/" + CatName;
//}

$(".mainmenu").click(function (e) {
    event.preventDefault();
    var location = $("#LastCatesearchbycity").val();
    var control = $(this).attr('data-value');
    var fields = control.split('**');
    var CatId = fields[0];
    var CatName = fields[1];
    CatName = CatName.replace(/ /g, "_");
    if (e.ctrlKey) {
        window.open('/Product/' + location + "/" + CatId + "/" + CatName, '_blank')
    }
    else {
        window.location.href = "/Product/" + location + "/" + CatId + "/" + CatName;
    }
});

//function LoadSubChildCategory(CatId, SubCatId, SubCatName, IsLastNode) {
//    var location = $("#LastCatesearchbycity").val();
//    SubCatName = SubCatName.replace(/ /g, "_");
//    if (IsLastNode == "True") {
//        window.location.href = "/Home/LoadCategoryDescription?CtName=" + location + "&SbCId=" + SubCatId + "&SbCName=" + SubCatName + "&SortFilter=" + 'topResults' + "&Ratingstatus=" + 'Asc';
//    }
//    else {
//        window.location.href = "/Products/" + location + "/" + CatId + "/" + SubCatId + "/" + SubCatName;
//    }
//}
$(".submainmenu").click(function (e) {
    event.preventDefault();
    var location = $("#LastCatesearchbycity").val();
    var control = $(this).attr('data-value');
    var fields = control.split('**');
    var CatId = fields[0];
    var SubCatId = fields[1];
    var SubCatName = fields[2];
    var IsLastNode = fields[3];
    SubCatName = SubCatName.replace(/ /g, "_");

    if (e.ctrlKey) {
        if (IsLastNode == "True") {
            window.open('/Home/LoadCategoryDescription?CtName=' + location + "&SbCId=" + SubCatId + "&SbCName=" + SubCatName + "&SortFilter=" + 'topResults' + "&Ratingstatus=" + 'Asc', '_blank')
        }
        else {
            window.open('/Products/' + location + "/" + CatId + "/" + SubCatId + "/" + SubCatName, '_blank')
        }
    }
    else {
        if (IsLastNode == "True") {
            window.location.href = "/Home/LoadCategoryDescription?CtName=" + location + "&SbCId=" + SubCatId + "&SbCName=" + SubCatName + "&SortFilter=" + 'topResults' + "&Ratingstatus=" + 'Asc';
        }
        else {
            window.location.href = "/Products/" + location + "/" + CatId + "/" + SubCatId + "/" + SubCatName;
        }
    }
});
//Jquery For SortingFilter
$('#Lastsearchcityarea').on("click", function () {
    $('#Lastcityareadrop').css('display', 'block');
});

$("ul[id*=LastUserLocationArea] li").click(function () {
    $("#Lastsearchcityarea").val($(this).text());
    $("#Lastcityareadrop").css("display", "none");
});

function LoadByArea(CtName, SbCId, SbCName) {
    var ArName = $("#Lastsearchcityarea").val();
    SbCName = SbCName.replace("&", "***");
    window.location.href = "/Home/LoadCategoryDescription/?CtName=" + CtName + "&SbCId=" + SbCId + "&SbCName=" + SbCName + "&SortFilter=" + 'location' + "&Ratingstatus=" + 'Asc' + "&ArName=" + ArName;
}

function LoadListingDesc(CtName, ArName, ListingId, CompanyName) {
    CompanyName = CompanyName.replace(/ /g, "_");
    window.location.href = "/ProductDetail/" + CtName + "/" + ArName + "/" + ListingId + "/" + CompanyName;
}

//Jquery For SearchBox For Web
$("#LastCatepdsrchbx").keyup(function (event) {
    var SearchVal = $("#LastCatepdsrchbx").val();
    var SearchLocation = $("#LastCatesearchbycity").val();
    var BindSearch = $("#LastCateauto");
    var SbCatName = "";
    var CompanyName = "";
    BindSearch.empty();
    if (SearchVal != "") {
        $.ajax({
            url: "/Home/GetAllSbCategory",
            type: "get",
            datatype: "json",
            data: { SbCatName: SearchVal, Location: SearchLocation },
            success: function (response) {
                BindSearch.empty();
                $.each(response, function (i) {
                    if (this["sbCatName"] != null) {
                        SbCatName = this["sbCatName"];
                        SbCatName = SbCatName.replace(/ /g, "_");
                        SbCatName = SbCatName.replace("&", "***");
                        if (this["arName"] != null) {
                            BindSearch.append('<li style="cursor: pointer" class="nav-link"><a href="/Home/LoadCategoryDescription?CtName=' + SearchLocation + '&SbCId=' + this["id"] + '&SbCName=' + SbCatName + '&SortFilter=topResults&Ratingstatus=Asc&ArName=' + this["arName"] + '">' + this["sbCatName"] + ' in ' + this["arName"] + '</a><br><span class="srch-area-rslt">' + this["sbCatName"] + ' in ' + SearchLocation + '</span><span class="srch-area-more"> [+]</span></li>');
                        }
                        else {
                            BindSearch.append('<li style="cursor: pointer" class="nav-link"><a href="/Home/LoadCategoryDescription?CtName=' + SearchLocation + '&SbCId=' + this["id"] + '&SbCName=' + SbCatName + '&SortFilter=topResults&Ratingstatus=Asc">' + this["sbCatName"] + '</a><br><span class="srch-area-rslt">' + this["sbCatName"] + ' in ' + SearchLocation + '</span><span class="srch-area-more"> [+]</span></li>');
                        }
                        //BindSearch.append('<li style="cursor: pointer" class="nav-link"><a href="/Home/LoadCategoryDescription/?CtName=' + SearchLocation + '&SbCId=' + this["id"] + '&SbCName=' + SbCatName + '&SortFilter=topResults&Ratingstatus=Asc">' + this["sbCatName"] + '</a><br><span class="srch-area-rslt">' + this["sbCatName"] + ' in ' + SearchLocation + '</span><span class="srch-area-more"> [+]</span></li>');
                        SbCatName = "";
                    }
                    if (this["companyName"] != null) {
                        var AppendingStars = "";
                        var AvgRating = parseInt(this["avgRating"]);
                        for (var i = 1; i <= AvgRating; i++) {
                            if (i < AvgRating) {
                                AppendingStars += '<img src="/PakDial-assets/images/starsize-04.png" />' + ''
                            }
                            else {
                                AppendingStars += '<img src="/PakDial-assets/images/starsize-04.png" />'
                            }
                        }
                        for (var i = (AvgRating + 1); i <= 5; i++) {
                            if (i == 1) {
                                AppendingStars += '<img src = "/PakDial-assets/images/starsize-05.png" />'
                            }
                            else {
                                AppendingStars += '' + '<img src = "/PakDial-assets/images/starsize-05.png" />'
                            }
                        }
                        CompanyName = this["companyName"];
                        CompanyName = CompanyName.replace(/ /g, "_");
                        BindSearch.append('<li style="cursor: pointer" class="nav-link nav-ratestar"><a href="/ProductDetail/' + this["ctName"] + '/' + this["arName"] + '/' + this["listingId"] + '/' + CompanyName + '">' + this["companyName"] + '     ' + '&nbsp&nbsp(' + this["arName"] + ')&nbsp&nbsp' + '   ' + SearchLocation + '      ' + '</a>&nbsp&nbsp&nbsp' + AppendingStars + '<br><span class="srch-area-rslt">' + this["companyName"] + ' in ' + SearchLocation + '</span></li>');
                        CompanyName = "";
                        AvgRating = "";
                        AppendingStars = "";
                    }

                });
                $('#LastCatepdsrchbxdrop').css('display', 'block');
                $('#LastCatecross_S').css('display', 'block');
            },
            error: function (response) {
                toastr.error(response, "Error");
            }
        });
	}
	$("#locationModal").modal({
		show: false,
		backdrop: 'static'
	});
});

$('#LastCateauto').on('click', 'li', function () {
    window.location.href = $('a', this).attr('href');
});

$('#LastCatecross_S').on("click", function () {
    $('#LastCatepdsrchbxdrop').css('display', 'none');
    $('#LastCatecross_S').hide();
});


//Jquery For SearchBox For Mobile

$('#search-field-input').on("click", function () {
    var value = $('#search-field-input').val();
    if (value == "search") {
        $('#search-field-input').addClass('active');
        $("#search-mobile-id").fadeToggle();
        $('#search-field-input').val("Remove");
    }
    else {
        $('#search-field-input').removeClass('active');
        $('#search-mobile-id').css('display', 'none');
        $('#search-field-input').val("search");
        $('#MobLastCatcitylistdrop').css('display', 'none');
        $('#MobLastCatepdsrchbxdrop').css('display', 'none');
    }
});

$("#MobLastCatepdsrchbx").keyup(function (event) {
    var SearchVal = $("#MobLastCatepdsrchbx").val();
    var SearchLocation = $("#MobLastCatesearchbycity").val();
    var BindSearch = $("#MobLastCateauto");
    var SbCatName = "";
    var CompanyName = "";
    BindSearch.empty();
    if (SearchVal != "") {
        $.ajax({
            url: "/Home/GetAllSbCategory",
            type: "get",
            datatype: "json",
            data: { SbCatName: SearchVal, Location: SearchLocation },
            success: function (response) {
                BindSearch.empty();
                $.each(response, function (i) {
                    if (this["sbCatName"] != null) {
                        SbCatName = this["sbCatName"];
                        SbCatName = SbCatName.replace(/ /g, "_");
                        SbCatName = SbCatName.replace("&", "***");
                        if (this["arName"] != null) {
                            BindSearch.append('<li style="cursor: pointer" class="nav-link"><a href="/Home/LoadCategoryDescription?CtName=' + SearchLocation + '&SbCId=' + this["id"] + '&SbCName=' + SbCatName + '&SortFilter=topResults&Ratingstatus=Asc&ArName=' + this["arName"] + '">' + this["sbCatName"] + ' in ' + this["arName"] + '</a><br><span class="srch-area-rslt">' + this["sbCatName"] + ' in ' + SearchLocation + '</span><span class="srch-area-more"> [+]</span></li>');
                        }
                        else {
                            BindSearch.append('<li style="cursor: pointer" class="nav-link"><a href="/Home/LoadCategoryDescription?CtName=' + SearchLocation + '&SbCId=' + this["id"] + '&SbCName=' + SbCatName + '&SortFilter=topResults&Ratingstatus=Asc">' + this["sbCatName"] + '</a><br><span class="srch-area-rslt">' + this["sbCatName"] + ' in ' + SearchLocation + '</span><span class="srch-area-more"> [+]</span></li>');
                        }
                        //BindSearch.append('<li style="cursor: pointer" class="nav-link"><a href="/Home/LoadCategoryDescription/?CtName=' + SearchLocation + '&SbCId=' + this["id"] + '&SbCName=' + SbCatName + '&SortFilter=topResults&Ratingstatus=Asc">' + this["sbCatName"] + '</a><br><span class="srch-area-rslt">' + this["sbCatName"] + ' in ' + SearchLocation + '</span><span class="srch-area-more"> [+]</span></li>');
                        SbCatName = "";
                    }
                    if (this["companyName"] != null) {
                        var AppendingStars = "";
                        var AvgRating = parseInt(this["avgRating"]);
                        for (var i = 1; i <= AvgRating; i++) {
                            if (i < AvgRating) {
                                AppendingStars += '<img src="/PakDial-assets/images/starsize-04.png" />' + ''
                            }
                            else {
                                AppendingStars += '<img src="/PakDial-assets/images/starsize-04.png" />'
                            }
                        }
                        for (var i = (AvgRating + 1); i <= 5; i++) {
                            if (i == 1) {
                                AppendingStars += '<img src = "/PakDial-assets/images/starsize-05.png" />'
                            }
                            else {
                                AppendingStars += '' + '<img src = "/PakDial-assets/images/starsize-05.png" />'
                            }
                        }
                        CompanyName = this["companyName"];
                        CompanyName = CompanyName.replace(/ /g, "_");
                        BindSearch.append('<li style="cursor: pointer" class="nav-link nav-ratestar"><a href="/ProductDetail/' + this["ctName"] + '/' + this["arName"] + '/' + this["listingId"] + '/' + CompanyName + '">' + this["companyName"] + '     ' + '&nbsp&nbsp(' + this["arName"] + ')&nbsp&nbsp' + '   ' + SearchLocation + '      ' + '</a>&nbsp&nbsp&nbsp' + AppendingStars + '<br><span class="srch-area-rslt">' + this["companyName"] + ' in ' + SearchLocation + '</span></li>');
                        CompanyName = "";
                        AvgRating = "";
                        AppendingStars = "";
                    }

                });
                $('#MobLastCatepdsrchbxdrop').css('display', 'block');
            },
            error: function (response) {
                toastr.error(response, "Error");
            }
        });
    }
});

$('#MobLastCateauto').on('click', 'li', function () {
    window.location.href = $('a', this).attr('href');
});

function ShowRequestform(ListingId) {
    $("#ListingIdQuery").val(ListingId);
    $("#SbCateIdQuery").val($("#SbCId").val());
    $("#SbCateNameQuery").val($("#SbCName").val());
    $("#AreaNameQuery").val($("#ArName").val());
    $("#RProductQuery").val("");
    $("#RnameQuery").val("");
    $("#RnumberQuery").val("");
    var Validator = $("#RQueryForm_Submit").validate();
    Validator.resetForm();
    $("#contactModalQuery").modal("show");
}

////////////////////////////////////////////////////////CategoryPopularArea///////////////////////////////////////////////////////
function CategoryPopularArea(CtName, SbCId, SbCName, ArName) {
    $.ajax({
        url: "/Home/CategoryPopularArea",
        type: "get",
        datatype: "json",
        data: { CtName: CtName, SbCId: SbCId, SbCName: SbCName, ArName: ArName, TotalRecord: TotalResults },
        success: function (response) {
        
            var TotalRecords = 0;
            if (response.length < 1) {
                $('#ShowMoreAreas').css('display', 'none');
            }
            else {
                $.each(response, function (i) {
                    var rowrating = ''
                    var rowratings = '<li class="nav-link nav-pop-area">' +
                        '<a href="/Home/LoadCategoryDescription?CtName=' + this["ctName"] + '&amp;SbCId=' + this["sbCId"] + '&amp;SbCName=' + this["sbCName"] + '&amp;SortFilter=' + this["sortFilter"] + '&amp;Ratingstatus=Asc&amp;ArName=' + this["arName"] + '">' + this["sbCNameReplace"] + ' in ' + this["arName"] + '</a>' +
                        '</li>';
                    $("#LoadingCityAreaUl").append(rowratings);
                    if (TotalRecords < 1) {
                        TotalRecords = this["totalRecords"];
                        TotalResults = 0;
                        TotalResults = TotalRecords;
                    }
                });
            }
        },
        error: function (response) {
            toastr.error(response, "Error");
        }
    });
}

$('.sort-result').click(function () {
    var filterVal = $('.sort-icon').attr('aria-label');
    if (filterVal == "false") {
        $('.text-category').addClass('active');
        $('.result-container').css('display', 'block');
        $('.sort-icon').attr('aria-label', 'true');
    }
    else {
        $('.text-category').removeClass('active');
        $('.result-container').css('display', 'none');
        $('.sort-icon').attr('aria-label', 'false');
    }
});
function myFunction() {
    var input, filter, ul, li, a, i, txtValue;
    input = document.getElementById("Lastsearchcityarea");
    filter = input.value.toUpperCase();
    ul = document.getElementById("LastUserLocationArea");
    li = ul.getElementsByTagName("li");
    for (i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("a")[0];
        txtValue = a.textContent || a.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }
}



