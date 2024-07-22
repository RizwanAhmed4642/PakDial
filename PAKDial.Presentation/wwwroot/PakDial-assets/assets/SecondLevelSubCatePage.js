$(function () {
    var location = window.location.href.split('/')[4];
    $("#SdLlsearchbycity").val(location);
    $("#MobSdLlsearchbycity").val(location);
    //Make Search Empty On Load
    $("#thirdCatepdsrchbx").val("");
    $("#MobthirdCatepdsrchbx").val("");
});

//For Web
$('#SdLlsearchbycity').on("click", function () {
    $('#SdLlcitylistdrop').css('display', 'block')
});
//For Mobile
$('#MobSdLlsearchbycity').on("click", function () {
    $('#MobSdLlcitylistdrop').css('display', 'block')
});
//For Web 
$("ul[id*=SecondUserSubLocation] li").click(function () {
    $("#SdLlsearchbycity").val($(this).text());
    var location = $("#SdLlsearchbycity").val();
    var CatId = window.location.href.split('/')[5];
    var SubCatId = window.location.href.split('/')[6];
    var SubCatName = window.location.href.split('/')[7];
    window.location.href = "/Products/" + location + "/" + CatId + "/" + SubCatId + "/" + SubCatName;
    $("#SdLlcitylistdrop").css("display", "none");
});
//For Mobile 
$("ul[id*=MobSecondUserSubLocation] li").click(function () {
    $("#MobSdLlsearchbycity").val($(this).text());
    var location = $("#MobSdLlsearchbycity").val();
    var CatId = window.location.href.split('/')[5];
    var SubCatId = window.location.href.split('/')[6];
    var SubCatName = window.location.href.split('/')[7];
    window.location.href = "/Products/" + location + "/" + CatId + "/" + SubCatId + "/" + SubCatName;
    $("#MobSdLlcitylistdrop").css("display", "none");
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
        $('#MobSdLlcitylistdrop').css('display', 'none');
        $('#MobthirdCatepdsrchbxdrop').css('display', 'none');
    }
});

////////////////////////////////////// Load Category//////////////////////////////////////////////////
//function LoadSubCategory(CatId, CatName) {
//    var location = $("#SdLlsearchbycity").val();
//    CatName = CatName.replace(/ /g, "_");
//    window.location.href = "/Product/" + location + "/" + CatId + "/" + CatName;
//}

$(".mainmenu").click(function (e) {
    event.preventDefault();
    var location = $("#SdLlsearchbycity").val();
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
//    var location = $("#SdLlsearchbycity").val();
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
    var location = $("#SdLlsearchbycity").val();
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
$("#thirdCatepdsrchbx").keyup(function (event) {
    var SearchVal = $("#thirdCatepdsrchbx").val();
    var SearchLocation = $("#SdLlsearchbycity").val();
    var BindSearch = $("#thirdCateauto");
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
                $('#thirdCatepdsrchbxdrop').css('display', 'block');
                $('#thirdCatecross_S').css('display', 'block');
            },
            error: function (response) {
                toastr.error(response, "Error");
            }
        });
    }
});

$('#thirdCateauto').on('click', 'li', function () {
    window.location.href = $('a', this).attr('href');
});

$('#thirdCatecross_S').on("click", function () {
    $('#thirdCatepdsrchbxdrop').css('display', 'none');
    $('#thirdCatecross_S').hide();
});

//Jquery For SearchBox For Mobile
$("#MobthirdCatepdsrchbx").keyup(function (event) {
    var SearchVal = $("#MobthirdCatepdsrchbx").val();
    var SearchLocation = $("#MobSdLlsearchbycity").val();
    var BindSearch = $("#MobthirdCateauto");
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
                $('#MobthirdCatepdsrchbxdrop').css('display', 'block');
                $('#MobthirdCatecross_S').css('display', 'block');
            },
            error: function (response) {
                toastr.error(response, "Error");
            }
        });
    }
});

$('#MobthirdCateauto').on('click', 'li', function () {
    window.location.href = $('a', this).attr('href');
});

