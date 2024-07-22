$(function () {
    //Make Search Empty On Load
    $("#FirstCatepdsrchbx").val("");
    $("#MobFirstCatepdsrchbx").val("");
});
///////////////////////////////////////Change Location////////////////////////////////////////////////

///For Desktop
$("ul[id*=UserLocation] li").click(function () {

    $("#search-by-city").val($(this).text());
    $("#city-list-drop").css('display','none');
});
///For Mobile
$("ul[id*=MobUserLocation] li").click(function () {
    $("#Mobsearch-by-city").val($(this).text());
    $("#Mobcity-list-drop").css('display', 'none');
});

//For Desktop
$('#search-by-city').on("click", function () {
    $('#city-list-drop').css('display', 'block');
});
//For Mobile
$('#Mobsearch-by-city').on("click", function () {
    $('#Mobcity-list-drop').css('display', 'block');
});

////////////////////////////////////// Load Category//////////////////////////////////////////////////
//function LoadSubCategory(CatId,CatName) {
//    var location = $("#search-by-city").val();
//    CatName = CatName.replace(/ /g, "_");
//    //var Url = "/Product/" + location + "/" + Id + "/" + Name;
//    //$(this).attr("href", Url);
//    window.location.href = "/Product/" + location + "/" + CatId + "/" + CatName; 
//}

$(".mainmenu").click(function (e) {
    event.preventDefault();
    var location = $("#search-by-city").val();
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
//    var location = $("#search-by-city").val();
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
    var location = $("#search-by-city").val();
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

//$('#search-field-input').on("click", function () {
//    var value = $('#search-field-input').val();
//    if (value == "search") {
//        $('#search-field-input').addClass('active');
//        $("#search-mobile-id").fadeToggle();
//        $('#search-field-input').val("Remove");
//    }
//    else {
//        $('#search-field-input').removeClass('active');
//        $('#search-mobile-id').css('display', 'none');
//        $('#search-field-input').val("search");
//        $('#Mobcity-list-drop').css('display', 'none');
//        $('#MobFirstCatepdsrchbxdrop').css('display', 'none');
//    }
//});


$("body").mouseup(function () {
            $('.collapse').collapse('hide');
});

/////////////////////////////////////////////////////For Search//////////////////////////////////////////////
//Jquery For SearchBox
$("#FirstCatepdsrchbx").keyup(function (event) {
    var SearchVal = $("#FirstCatepdsrchbx").val();
    var SearchLocation = $("#search-by-city").val();
    var BindSearch = $("#FirstCateauto");
    var SbCatName = "";
    var CompanyName = "";
    BindSearch.empty();
    if (SearchVal != "") {
        $.ajax({
            url: "/Home/GetAllSbCategory",
            type: "get",
            datatype: "json",
            data: { SbCatName: SearchVal, Location: SearchLocation},
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
                        SbCatName = "";
                    }
                    if (this["companyName"] != null) {
                        var AppendingStars = "";
                        var AvgRating = parseInt(this["avgRating"]);
                        for (var i = 1; i <= AvgRating; i++) {
                            if (i < AvgRating) {
                                AppendingStars += '<img src="/PakDial-assets/images/starsize-04.png" />'+''
                            }
                            else {
                                AppendingStars += '<img src="/PakDial-assets/images/starsize-04.png" />'
                            }
                        }
                        for (var i = (AvgRating + 1); i <= 5; i++) {
                            if (i == 1) {
                                AppendingStars += '<img src = "/PakDial-assets/images/starsize-05.png" />'
                            }
                            else{
                                AppendingStars += ''+'<img src = "/PakDial-assets/images/starsize-05.png" />'
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
                $('#FirstCatepdsrchbxdrop').css('display', 'block');
                $('#FirstCatecross_S').css('display', 'block');
            },
            error: function (response) {
                toastr.error(response, "Error");
            }
        });
    }
});
/////////////////////////////////////////////////////For Search Mobile//////////////////////////////////////////////
$("#MobFirstCatepdsrchbx").keyup(function (event) {
    var SearchVal = $("#MobFirstCatepdsrchbx").val();
    var SearchLocation = $("#Mobsearch-by-city").val();
    var BindSearch = $("#MobFirstCateauto");
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
                $('#MobFirstCatepdsrchbxdrop').css('display', 'block');
            },
            error: function (response) {
                toastr.error(response, "Error");
            }
        });
    }
});


//For Web
$('#FirstCateauto').on('click', 'li', function () {
    window.location.href = $('a', this).attr('href');
});
//For Mobile
$('#MobFirstCateauto').on('click', 'li', function () {
    window.location.href = $('a', this).attr('href');
});

//$('li.nav-link a').on('click', function (e) {
//    e.preventDefault();
//    e.stopPropagation();
//    console.log($(this).text());
//});
//$('li.nav-link').on('click', 'a', function (e) {
//    $(this).children('a').click();
//});
//$('li.nav-link').on('click', 'a', function (e) {
//    $(this).children('a').click();
//});

$('#FirstCatecross_S').on("click", function () {
    $('#FirstCatepdsrchbxdrop').css('display', 'none');
    $('#FirstCatecross_S').hide();
});