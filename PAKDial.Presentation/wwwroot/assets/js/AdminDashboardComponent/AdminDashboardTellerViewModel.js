$(function () {
    $.ajax({
        url: "/AdminDashBoard/LoadTellerDashboard",
        type: "get",
        datatype: "json",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            if (response != null) {
                //DepositedOrder
  
                document.getElementById('ADbTellerYearTotal').innerHTML = response.yearlyDepCount;
                document.getElementById('ADbTellerMontlyTotal').innerHTML = response.monthlyDepCount;
                document.getElementById('ADbTellerWeeklyTotal').innerHTML = response.weeklyDepCount;
                document.getElementById('ADbTellerDailyTotal').innerHTML = response.dailyDepCount;

                document.getElementById('ADbTellerTotal').innerHTML = response.totalDepCount;

                
            }
            else {
                toastr.error(response, "Error");
            }
        },
        error: function (response) {
            toastr.error(response, "Error");
        }

    });

});