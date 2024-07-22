$(function () {
    
    var location = window.location.href.split('/')[4];
    $("#Finalsearchbycity").val(location);   
    $("#MobFinalsearchbycity").val(location);   
    $("#RatingDesc").val('');
  //  $('.gallery-box :lt(8)').show();
    
});

//For Web
$('#Finalsearchbycity').on("click", function () {
    $('#Finalcitylistdrop').css('display', 'block')
});
$("ul[id*=FinalUserLocation] li").click(function () {
   
    $("#Finalsearchbycity").val($(this).text());
    $("#Finalcitylistdrop").css("display", "none");
});

//For Mobile
$('#MobFinalsearchbycity').on("click", function () {
    $('#MobFinalcitylistdrop').css('display', 'block')
});
$("ul[id*=MobFinalUserLocation] li").click(function () {
    $("#MobFinalsearchbycity").val($(this).text());
    $("#MobFinalcitylistdrop").css("display", "none");
});

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
        $('#MobFinalcitylistdrop').css('display', 'none');
        $('#MobFinalCatepdsrchbxdrop').css('display', 'none');
    }
});

//Check Allow For Current Customer
$('#ClientEditListingId').on("click", function () {
    var Id = $(this).data("value");
    if (parseInt($(this).data("value")) > 0) {
        $.ajax({
            url: "/ClientListing/CheckCustomerListing",
            type: "get",
            datatype: "json",
            data: { Id: $(this).data("value")},
            success: function (response) {
                $.each(response.listingRating, function (i) {
                    var SpanHtml = ""
                    for (var i = 1; i <= parseInt(this["rating"]); i++) {
                        SpanHtml = SpanHtml + '<img src="/PakDial-assets/images/starsize-04.png" />'
                    }
                    for (var i = (parseInt(this["rating"]) + 1); i <= 5; i++) {
                        SpanHtml = SpanHtml + '<img src="/PakDial-assets/images/starsize-05.png" />'
                    }
                    var rowratings = '<div class="row rating-box"><div class="rating-box-inner col-12"><div class="rating-box-lft"><div class=""><img class="review-img" width="50px"' +
                        'height="50px" src="/PakDial-assets/images/profile-icon-28-01.png" alt=""></div></div>' +
                        '<div class="rating-box-rgt"><span class="rating-user"><span class="rating-user-name">' + this["name"] + '</span><span class="rating-user-star">' + SpanHtml + '</span>' +
                        '</span><span class="rating-date pull-right">' + dateFormats(this["createdDate"]) + '</span>' +
                        '<div><p class="rating-comments">' + this["ratingDesc"] + '</p></div>' +
                        '<div class="rating-share-section"></div></div></div></div>';
                    $(rowratings).insertBefore("#loadingBtnRating");
                    SpanHtml = "";
                });
                if (response == "OK") {
                    window.location.href = "/ClientListing/EditCompanyListing?Id=" + Id
                }
                else if (response == "InValid") {
                    toastr.warning("Invalid Customer", "Warning");
                }
                else {
                    window.location.href = "/Home/Login?ReturnUrl=%2FClientListing%2FEditCompanyListing%3FId%3D" + Id
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }
        });
    }
});

////////////////////////////////////// Load Category//////////////////////////////////////////////////
//function LoadSubCategory(CatId, CatName) {
//    var location = $("#Finalsearchbycity").val();
//    CatName = CatName.replace(/ /g, "_");
//    window.location.href = "/Product/" + location + "/" + CatId + "/" + CatName;
//}
$(".mainmenu").click(function (e) {
    event.preventDefault();
    var location = $("#Finalsearchbycity").val();
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

function LoadDetailCompanyListing(CityArea, ListingId, Name) {
    var location = $("#Finalsearchbycity").val();
    CityArea = CityArea.replace(/ /g, "_");
    window.location.href = "/ProductDetail/" + location + "/" + CityArea + "/" + ListingId + "/" + Name ;
}

$(".submainmenu").click(function (e) {
    event.preventDefault();
    var location = $("#Finalsearchbycity").val();
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
/////////////////////////////////////////////////////For Search//////////////////////////////////////////////
//Jquery For SearchBox For Web
$("#FinalCatepdsrchbx").keyup(function (event) {
    var SearchVal = $("#FinalCatepdsrchbx").val();
    var SearchLocation = $("#Finalsearchbycity").val();
    var BindSearch = $("#FinalCateauto");
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
                $('#FinalCatepdsrchbxdrop').css('display', 'block');
                $('#FinalCatecross_S').css('display', 'block');
            },
            error: function (response) {
                toastr.error(response, "Error");
            }
        });
    }
});

$('#FinalCateauto').on('click', 'li', function () {
    window.location.href = $('a', this).attr('href');
});
$('#FinalCatecross_S').on("click", function () {
    $('#FinalCatepdsrchbxdrop').css('display', 'none');
    $('#FinalCatecross_S').hide();
});

//Jquery For SearchBox For Mobile
$("#MobFinalCatepdsrchbx").keyup(function (event) {
    var SearchVal = $("#MobFinalCatepdsrchbx").val();
    var SearchLocation = $("#MobFinalsearchbycity").val();
    var BindSearch = $("#MobFinalCateauto");
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
                $('#MobFinalCatepdsrchbxdrop').css('display', 'block');
            },
            error: function (response) {
                toastr.error(response, "Error");
            }
        });
    }
});

$('#MobFinalCateauto').on('click', 'li', function () {
    window.location.href = $('a', this).attr('href');
});




/////////////////////////////////////////////////////Load More Ratings//////////////////////////////////////////////

$('#RatingBtn').on("click", function () {
    $("#gifRatingImg").css('display', 'block');
    var Rows = $("#RatingBtnValue").val().split(','); //use .split() to split the character you want to.
    var ShownRows = Rows[0];
    var RowCount = Rows[1];
    var ListingId = $("#ListingId").val();
    if (parseInt(ShownRows) < parseInt(RowCount)) {
        $.ajax({
            url: "/Home/GetMoreRatings",
            type: "get",
            datatype: "json",
            data: { ListingId: ListingId, ShowMore: ShownRows },
            success: function (response) {
                $.each(response.listingRating, function (i) {
                    var SpanHtml = ""
                        for (var i = 1; i <= parseInt(this["rating"]); i++)
                        {
                            SpanHtml = SpanHtml + '<img src="/PakDial-assets/images/starsize-04.png" />'
                        }
                        for (var i = (parseInt(this["rating"]) + 1); i <= 5; i++)
                        {
                            SpanHtml = SpanHtml + '<img src="/PakDial-assets/images/starsize-05.png" />'
                        }
                    var rowratings = '<div class="row rating-box"><div class="rating-box-inner col-12"><div class="rating-box-lft"><div class=""><img class="review-img" width="50px"' +
                        'height="50px" src="/PakDial-assets/images/user-image.png" alt=""></div></div>' +
                        '<div class="rating-box-rgt"><span class="rating-user"><span class="rating-user-name">' + this["name"] + '</span><span class="rating-user-star">' + SpanHtml + '</span>' +
                        '</span><span class="rating-date pull-right">' + dateFormats(this["createdDate"]) + '</span>' +
                        '<div><p class="rating-comments">' + this["ratingDesc"] + '</p></div>' +
                        '<div class="rating-share-section"></div></div></div></div>';
                        $(rowratings).insertBefore("#loadingBtnRating");
                        SpanHtml = "";
                });
                if (response.incrementalCount >= response.rowCount) {
                    $("#gifRatingImg").css('display', 'none');
                    $("#loadingBtnRating").css('display', 'none')
                }
                else {
                    var BindBtn = response.incrementalCount + ',' + response.rowCount;
                    $("#RatingBtnValue").val("");
                    $("#RatingBtnValue").val(BindBtn);
                    $("#gifRatingImg").css('display', 'none');
                    $("#loadingBtnRating").css('display', 'block')
                }
            },
            error: function (response) {
                $("#gifRatingImg").css('display', 'none');
                $("#loadingBtnRating").css('display', 'block')
                toastr.error(response, "Error");
            }
        });
    }
    else {
        $("#loadingBtnRating").css('display', 'none')
        $("#gifRatingImg").css('display', 'none');
    }
});


function dateFormats(date) {
    var dates = date.split('-');
    var years = dates[0];
    var month = GetMonthName(dates[1]);
    var day = dates[2].substr(0, 2);
    return day + " " + month + " " + years;
}
function GetMonthName(monthNumber) {
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'July', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    return months[monthNumber - 1];
}

function ShowRequestform(ListingId) {
    $("#ListingIdQuery").val(ListingId);
    $("#SbCateIdQuery").val($("#SbCId1").val());
    $("#SbCateNameQuery").val($("#SbCName1").val());
    $("#AreaNameQuery").val($("#ArName1").val());
    $("#RProductQuery").val("");
    $("#RnameQuery").val("");
    $("#RnumberQuery").val("");
    var Validator = $("#RQueryForm_Submit").validate();
    Validator.resetForm();
    $("#contactModalQuery").modal("show");
}

////////////////////////////////////////////Rating Selection///////////////////////////////////////////
var RatingCounter = 0;
$("#Rb1").hover(
    function () {
        $(".rate-boxes").css('background', '#e4e4e4');
        $(this).css('background', '#DBF1D0');
    },
    function () {
        if (RatingCounter == 1) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
        }
        else if (RatingCounter == 2) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
        }
        else if (RatingCounter == 3) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
            $("#Rb3").css('background', '#94D672');
        }
        else if (RatingCounter == 4) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
            $("#Rb3").css('background', '#94D672');
            $("#Rb4").css('background', '#82D05B');
        }
        else if (RatingCounter == 5) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
            $("#Rb3").css('background', '#94D672');
            $("#Rb4").css('background', '#82D05B');
            $("#Rb5").css('background', '#5EC22C');
        }
    }
);
$("#Rb2").hover(
    function () {
        $(".rate-boxes").css('background', '#e4e4e4');
        $("#Rb1").css('background', '#DBF1D0');
        $(this).css('background', '#B7E4A1');
    },
    function () {
        if (RatingCounter == 1) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
        }
        else if (RatingCounter == 2) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
        }
        else if (RatingCounter == 3) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
            $("#Rb3").css('background', '#94D672');
        }
        else if (RatingCounter == 4) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
            $("#Rb3").css('background', '#94D672');
            $("#Rb4").css('background', '#82D05B');
        }
        else if (RatingCounter == 5) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
            $("#Rb3").css('background', '#94D672');
            $("#Rb4").css('background', '#82D05B');
            $("#Rb5").css('background', '#5EC22C');
        }
    }
);
$("#Rb3").hover(
    function () {
        $(".rate-boxes").css('background', '#e4e4e4');
        $("#Rb1").css('background', '#DBF1D0');
        $("#Rb2").css('background', '#B7E4A1');
        $(this).css('background', '#94D672');
    },
    function () {
        if (RatingCounter == 1) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
        }
        else if (RatingCounter == 2) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
        }
        else if (RatingCounter == 3) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
            $("#Rb3").css('background', '#94D672');
        }
        else if (RatingCounter == 4) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
            $("#Rb3").css('background', '#94D672');
            $("#Rb4").css('background', '#82D05B');
        }
        else if (RatingCounter == 5) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
            $("#Rb3").css('background', '#94D672');
            $("#Rb4").css('background', '#82D05B');
            $("#Rb5").css('background', '#5EC22C');
        }
    }
);
$("#Rb4").hover(
    function () {
        $(".rate-boxes").css('background', '#e4e4e4');
        $("#Rb1").css('background', '#DBF1D0');
        $("#Rb2").css('background', '#B7E4A1');
        $("#Rb3").css('background', '#94D672');
        $(this).css('background', '#82D05B');
    },
    function () {
        if (RatingCounter == 1) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
        }
        else if (RatingCounter == 2) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
        }
        else if (RatingCounter == 3) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
            $("#Rb3").css('background', '#94D672');
        }
        else if (RatingCounter == 4) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
            $("#Rb3").css('background', '#94D672');
            $("#Rb4").css('background', '#82D05B');
        }
        else if (RatingCounter == 5) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
            $("#Rb3").css('background', '#94D672');
            $("#Rb4").css('background', '#82D05B');
            $("#Rb5").css('background', '#5EC22C');
        }
    }
);
$("#Rb5").hover(
    function () {
        $(".rate-boxes").css('background', '#e4e4e4');
        $("#Rb1").css('background', '#DBF1D0');
        $("#Rb2").css('background', '#B7E4A1');
        $("#Rb3").css('background', '#94D672');
        $("#Rb4").css('background', '#82D05B');
        $(this).css('background', '#5EC22C');
    },
    function () {
        if (RatingCounter == 1) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
        }
        else if (RatingCounter == 2) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
        }
        else if (RatingCounter == 3) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
            $("#Rb3").css('background', '#94D672');
        }
        else if (RatingCounter == 4) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
            $("#Rb3").css('background', '#94D672');
            $("#Rb4").css('background', '#82D05B');
        }
        else if (RatingCounter == 5) {
            $(".rate-boxes").css('background', '#e4e4e4');
            $("#Rb1").css('background', '#DBF1D0');
            $("#Rb2").css('background', '#B7E4A1');
            $("#Rb3").css('background', '#94D672');
            $("#Rb4").css('background', '#82D05B');
            $("#Rb5").css('background', '#5EC22C');
        }
    }
);

$(".rate-box").mouseleave(function () {
    if (RatingCounter == 0) {
        $(".rate-boxes").css('background', '#e4e4e4');
    }
    else if (RatingCounter == 1) {
        $(".rate-boxes").css('background', '#e4e4e4');
        $("#Rb1").css('background', '#DBF1D0');
    }
    else if (RatingCounter == 2) {
        $(".rate-boxes").css('background', '#e4e4e4');
        $("#Rb1").css('background', '#DBF1D0');
        $("#Rb2").css('background', '#B7E4A1');
    }
    else if (RatingCounter == 3) {
        $(".rate-boxes").css('background', '#e4e4e4');
        $("#Rb1").css('background', '#DBF1D0');
        $("#Rb2").css('background', '#B7E4A1');
        $("#Rb3").css('background', '#94D672');
    }
    else if (RatingCounter == 4) {
        $(".rate-boxes").css('background', '#e4e4e4');
        $("#Rb1").css('background', '#DBF1D0');
        $("#Rb2").css('background', '#B7E4A1');
        $("#Rb3").css('background', '#94D672');
        $("#Rb4").css('background', '#82D05B');
    }
    else if (RatingCounter == 5) {
        $(".rate-boxes").css('background', '#e4e4e4');
        $("#Rb1").css('background', '#DBF1D0');
        $("#Rb2").css('background', '#B7E4A1');
        $("#Rb3").css('background', '#94D672');
        $("#Rb4").css('background', '#82D05B');
        $("#Rb5").css('background', '#5EC22C');
    }
})

$("#Rb1").on('click', function () {
    RatingCounter = parseInt($(this).attr("data-value"));
    $(".rate-boxes").css('background', '#e4e4e4');
    $(this).css('background', '#DBF1D0');
});
$("#Rb2").on('click', function () {
    RatingCounter = parseInt($(this).attr("data-value"));
    $(".rate-boxes").css('background', '#e4e4e4');
    $("#Rb1").css('background', '#DBF1D0');
    $(this).css('background', '#B7E4A1');
});
$("#Rb3").on('click', function () {
    RatingCounter = parseInt($(this).attr("data-value"));
    $(".rate-boxes").css('background', '#e4e4e4');
    $("#Rb1").css('background', '#DBF1D0');
    $("#Rb2").css('background', '#B7E4A1');
    $(this).css('background', '#94D672');
});
$("#Rb4").on('click', function () {
    RatingCounter = parseInt($(this).attr("data-value"));
    $(".rate-boxes").css('background', '#e4e4e4');
    $("#Rb1").css('background', '#DBF1D0');
    $("#Rb2").css('background', '#B7E4A1');
    $("#Rb3").css('background', '#94D672');
    $(this).css('background', '#82D05B');
});
$("#Rb5").on('click', function () {
    RatingCounter = parseInt($(this).attr("data-value"));
    $(".rate-boxes").css('background', '#e4e4e4');
    $("#Rb1").css('background', '#DBF1D0');
    $("#Rb2").css('background', '#B7E4A1');
    $("#Rb3").css('background', '#94D672');
    $("#Rb4").css('background', '#82D05B');
    $(this).css('background', '#5EC22C');
});


/////////////////////////////////////////////////////--Company Rating--//////////////////////////////////////////////

$("#reviewRatingSbmit").validate({
    rules: {
        RatingName: {
            required: true,
        },
        RatingMobile: {
            required: true,
            numberonly: true,
            minlength: 10,
            maxlength: 10,
        },
        RatingEmail: {
            emailRegex: true,
        },
        RatingDesc: {
            required: true,
        },
        ListingId: {
            required: true,
        }
    },
    messages: {
        RatingName: {
            required: "Please Enter Name",
        },
        RatingMobile: {
            required: "Mobile No Required",
        },
        RatingDesc: {
            required: "Description Required",
        },
        ListingId: {
            required: "Passcode Required",
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
        var cr = new CompanyRatings();
        cr.Name = $("#RatingName").val();
        cr.MobileNo = $("#RatingMobile").val();
        cr.EmailAddress = $("#RatingEmail").val();
        cr.Rating = RatingCounter;
        cr.RatingDesc = $("#RatingDesc").val();
        cr.ListingId = $("#ListingId").val();
        $.ajax({
            url: "/Home/CompanyPostRating",
            type: "post",
            datatype: "json",
            data: { instance: cr },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response.id > 0) {
                    if (response.message != null) {
                        $("#RatingName").val("");
                        $("#RatingMobile").val("");
                        $("#RatingEmail").val("");
                        RatingCounter = 0;
                        $("#RatingDesc").val('');
                        $("#RatingId").val(response.id);
                        $("#RatingOtp").val("");
                        $(".rate-boxes").css('background', '#e4e4e4');
                        toastr.error(response.message, "Error");
                    }
                    else {
                        $("#RatingName").val("");
                        $("#RatingMobile").val("");
                        $("#RatingEmail").val("");
                        RatingCounter = 0;
                        $("#RatingDesc").val('');
                        $("#RatingId").val(response.id);
                        $("#RatingOtp").val("");
                        $(".rate-boxes").css('background', '#e4e4e4');
                        $("#RatingOtpModel").modal('show');
                    }
                }
                else {
                    $("#RatingName").val("");
                    $("#RatingMobile").val("");
                    $("#RatingEmail").val("");
                    RatingCounter = 0;
                    $("#RatingDesc").val('');
                    $("#RatingId").val(response.id);
                    $("#RatingOtp").val("");
                    $(".rate-boxes").css('background', '#e4e4e4');
                    toastr.error(response.message, "Error");
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }
});


$("#RatingOtp_Submit").validate({
    rules: {
        RatingId: {
            required: true,
        },
        RatingOtp: {
            required: true,
            numberonly: true,
            minlength: 4,
            maxlength: 4,
        }
    },
    messages: {
        RatingId: {
            required: "Passcode Required",
        },
        RatingOtp: {
            required: "Mobile No Required",
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
   
        var cr = new CompanyRatingsOTP();
        cr.Id = $("#RatingId").val();
        cr.Otp = $("#RatingOtp").val();
        $.ajax({
            url: "/Home/PostRatingOtp",
            type: "post",
            datatype: "json",
            data: { instance: cr },
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                if (response.id == 1) {
                    toastr.success(response.message, "Success");
                    $("#RatingId").val("");
                    $("#RatingOtp").val("");
                    $("#RatingOtpModel").modal('hide');
                }
                else if (response.id == 2)
                {
                    $("#RatingOtp").val("");
                    toastr.error(response.message, "Error");
                }
                else {
                    toastr.error(response.message, "Error");
                    $("#RatingId").val("");
                    $("#RatingOtp").val("");
                    $("#RatingOtpModel").modal('hide');
                }
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }
    
});
