$(document).ready(function () {
    if ($(window).width() > 600) {
        $('#active-tab-1').addClass("active");
    }
    else {
        $('#active-tab-1').removeClass("active");
    }
    if ($(window).width() < 600) {
    }

});

$activeTabs = $('.listingpage-navtab li a').click(function () {
    if ($(window).width() > 600) {
        $activeTabs.removeClass("active");
        $(this).addClass("active");
    }
    else {
        $activeTabs.removeClass("active");
    }
});

$('#website-lst').tooltipster({ trigger: 'click' });

$("#cllTo").on("click", function () {
    $('#overlay-pd-mask').css('display', 'block')
    $("#pf-js-model").append('<div> <div class="calpop1"> <div class="calwpr1 dt"><div class="tbcalWpr"><span class="tbcaltxt">Tap to Call</span></div> <ul class="calnbrwp"> <li>09152758850</li> <li>07798636030</li> <li>08888886436</li> <li>08879424312</li> </ul> </div><span class="cancelbtn dt" id="btn-modal-cancel" oncl ick="Hide()">Cancel</span> </div> </div> </div>');
});

$("#payPlan-lst").on("click", function () {
    $('#overlay-pd-mask').css('display', 'block')
    $("#pf-js-model").append('<div> <div class="calpop1"> <div class="calwpr1 dt"><div class="tbcalWpr"></div> <ul class="calnbrwp">  <li><span class="icnwdt"><img src="images/cash.png"></span><span class="sharepopupcell font15">Cash</span></li> <li><span class="icnwdt"><img src="images/cheque.png"></span><span class="sharepopupcell font15">Cheques</span></li> <li><span class="icnwdt"><img src="images/PayOrder.png"></span><span class="sharepopupcell font15">Pay Order</span></li> <li><span class="icnwdt"><img src="images/OnlinePayment.png"></span><span class="sharepopupcell font15">Online Payment</span></li> <li><span class="icnwdt"><img src="images/debit.png"></span><span class="sharepopupcell font15">Debit Card</span></li> <li><span class="icnwdt"><img src="images/master.png"></span><span class="sharepopupcell font15">Master Card</span></li> <li><span class="icnwdt"><img src="images/credit.png"></span><span class="sharepopupcell font15">Credit Card</span></li> <li><span class="icnwdt"><img src="images/visa.png"></span><span class="sharepopupcell font15">Visa Card</span></li></ul> </div></div> </div> </div>');
});

$("#timing-lst").on("click", function () {
    $('#overlay-pd-mask').css('display', 'block')
    $("#pf-js-model").append('<div> <div class="calpop1"> <div class="calwpr1 dt"><div class="tbcalWpr"></div> <ul class="calnbrwp"><li><span class="sharepopupcell font15"><strong>Monday</strong> 9:30 am - 9:30 pm</span></li><li><span class="sharepopupcell font15"><strong>Monday</strong> 9:30 am - 9:30 pm</span></li><li><span class="sharepopupcell font15"><strong>Monday</strong> 9:30 am - 9:30 pm</span></li><li><span class="sharepopupcell font15"><strong>Monday</strong> 9:30 am - 9:30 pm</span></li><li><span class="sharepopupcell font15"><strong>Monday</strong> 9:30 am - 9:30 pm</span></li><li><span class="sharepopupcell font15"><strong>Monday</strong> 9:30 am - 9:30 pm</span></li><li><span class="sharepopupcell font15"><strong>Monday</strong> 9:30 am - 9:30 pm</span></li></ul> </div></div> </div> </div>');
});

$("#share-lst").on("click", function () {
    $('#overlay-pd-mask').css('display', 'block')
    $("#pf-js-model").append('<div> <div class="calpop1"> <div class="calwpr1 dt"><div class="tbcalWpr"></div> <ul class="calnbrwp">  <li><span class="icnwdt"><i class="fab fa-facebook-f"></i></span><span class="sharepopupcell font15">Facebook</span></li> <li><span class="icnwdt"><i class="fab fa-twitter"></i></span><span class="sharepopupcell font15">Twitter</span></li> <li><span class="icnwdt"><i class="fa fa-copy"></i></span><span class="sharepopupcell font15">Copy</span></li> </ul> </div></div> </div> </div>');
});

function Hide() {
    $('#pf-js-model').empty();
}

$("body").mouseup(function () {
    $('#overlay-pd-mask').css('display', 'none');
    $('#pf-js-model').empty();
    $('.collapse').collapse('hide');
});