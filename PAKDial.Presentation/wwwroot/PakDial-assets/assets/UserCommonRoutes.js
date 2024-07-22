
////////////////////////////////////// Load Category//////////////////////////////////////////////////
$(".mainmenu").click(function (e) {
    event.preventDefault();
    var location = $("#Commonsearchbycity").val();
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

$(".submainmenu").click(function (e) {
    event.preventDefault();
    var location = $("#Commonsearchbycity").val();
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