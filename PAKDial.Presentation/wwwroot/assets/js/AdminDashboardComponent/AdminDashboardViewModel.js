$(function () {
    $.ajax({
        url: "/AdminDashBoard/LodESCCompanyListings",
        type: "get",
        datatype: "json",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            if (response != null) {
            //Listings
               
                document.getElementById('ADbYearlyCount').innerHTML = response.listingCount.yearlyCount;
                document.getElementById('ADbMonthlyCount').innerHTML = response.listingCount.monthlyCount;
                document.getElementById('ADbWeeklyCount').innerHTML = response.listingCount.weeklyCount;
                document.getElementById('ADbDayCount').innerHTML = response.listingCount.dailyCount;

                document.getElementById('ADbTotalCount').innerHTML = response.listingCount.totalCount;

                //Status
         
                document.getElementById('ADbPendingCount').innerHTML = response.escOrder.pendingCount;
                document.getElementById('ADbProcessCount').innerHTML = response.escOrder.processCount;
                document.getElementById('ADbApprovedCount').innerHTML = response.escOrder.approvedCount;
                document.getElementById('ADbRejectedCount').innerHTML = response.escOrder.rejectedCount;
                document.getElementById('ADbExpiredCount').innerHTML = response.escOrder.expiredCount;

                document.getElementById('ADbtotalOrders').innerHTML = response.escOrder.totalStatusCount;

                //Deposit
     
                document.getElementById('ADbDailyCount').innerHTML = response.escDepOrder.dailyDepCount;//response.escDepOrder.dailyDepCount;
                document.getElementById('ADbDYearlyCount').innerHTML = response.escDepOrder.yearlyDepCount;
                document.getElementById('ADbDMonthlyCount').innerHTML = response.escDepOrder.monthlyDepCount;
                document.getElementById('ADbDWeeklyCount').innerHTML = response.escDepOrder.weeklyDepCount;
                
                
                document.getElementById('ADbDtotalCount').innerHTML = response.escDepOrder.totalDepCount;
                
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

