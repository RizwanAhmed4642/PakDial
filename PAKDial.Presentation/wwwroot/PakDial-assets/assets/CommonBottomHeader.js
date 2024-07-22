$(function () {
    $("#CommonCatepdsrchbx").val("");   
    $("#MobCommonCatepdsrchbx").val("");   

});
//For Web
$('#Commonsearchbycity').on("click", function () {
    $('#Commoncitylistdrop').css('display', 'block')
});
$("ul[id*=CommonUserLocation] li").click(function () {
   
    $("#Commonsearchbycity").val($(this).text());
    $("#Commoncitylistdrop").css("display", "none");
});
//Fro Mobile
$('#MobCommonsearchbycity').on("click", function () {
    $('#MobCommoncitylistdrop').css('display', 'block')
});
$("ul[id*=MobCommonUserLocation] li").click(function () {

    $("#MobCommonsearchbycity").val($(this).text());
    $("#MobCommoncitylistdrop").css("display", "none");
});

////////////////////////////////////// Load Category//////////////////////////////////////////////////

function LoadDetailCompanyListing(CityArea, ListingId, Name) {
    var location = $("#Commonsearchbycity").val();
    CityArea = CityArea.replace(/ /g, "_");
    window.location.href = "/ProductDetail/" + location + "/" + CityArea + "/" + ListingId + "/" + Name ;
}

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
        $("#MobCommoncitylistdrop").css("display", "none");
        $('#MobCommonCatepdsrchbxdrop').css('display', 'none');
    }
});

/////////////////////////////////////////////////////For Search//////////////////////////////////////////////
//Jquery For SearchBox For Web
$("#CommonCatepdsrchbx").keyup(function (event) {
    var SearchVal = $("#CommonCatepdsrchbx").val();
    var SearchLocation = $("#Commonsearchbycity").val();
    var BindSearch = $("#CommonCateauto");
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
                $('#CommonCatepdsrchbxdrop').css('display', 'block');
                $('#CommonCatecross_S').css('display', 'block');
            },
            error: function (response) {
                toastr.error(response, "Error");
            }
        });
    }
});

$('#CommonCateauto').on('click', 'li', function () {
    window.location.href = $('a', this).attr('href');
});

$('#CommonCatecross_S').on("click", function () {
    $('#CommonCatepdsrchbxdrop').css('display', 'none');
    $('#CommonCatecross_S').hide();
});


//Jquery For SearchBox
$("#MobCommonCatepdsrchbx").keyup(function (event) {
    var SearchVal = $("#MobCommonCatepdsrchbx").val();
    var SearchLocation = $("#MobCommonsearchbycity").val();
    var BindSearch = $("#MobCommonCateauto");
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
                $('#MobCommonCatepdsrchbxdrop').css('display', 'block');
                $('#MobCommonCatecross_S').css('display', 'block');
            },
            error: function (response) {
                toastr.error(response, "Error");
            }
        });
    }
});

$('#MobCommonCateauto').on('click', 'li', function () {
    window.location.href = $('a', this).attr('href');
});



