$(function () {
    $.ajax({
        url: "/AdminDashBoard/LoadZoneManagerDashboard",
        type: "get",
        datatype: "json",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            if (response != null) {
                //ListingOrders

                document.getElementById('ADbYearlyCount').innerHTML =   response.listingCount.yearlyCount;
                document.getElementById('ADbMonthlyCount').innerHTML =  response.listingCount.monthlyCount;
                document.getElementById('ADbWeeklyCount').innerHTML =   response.listingCount.weeklyCount;
                document.getElementById('ADbDayCount').innerHTML =      response.listingCount.dailyCount;
                                                                                
                document.getElementById('ADbTotalCount').innerHTML = response.listingCount.totalCount;

                //Deposit Order
                document.getElementById('ADbDYearlyCount').innerHTML = response.depositedOrderCount.yearlyCount;
                document.getElementById('ADbDMonthlyCount').innerHTML = response.depositedOrderCount.monthlyCount;
                document.getElementById('ADbDWeeklyCount').innerHTML = response.depositedOrderCount.weeklyCount;
                document.getElementById('ADbDDayCount').innerHTML = response.depositedOrderCount.dailyCount;
                                            
                document.getElementById('ADbDTotalCount').innerHTML = response.depositedOrderCount.totalCount;

                //Approved Order
                document.getElementById('ADbAYearlyCount').innerHTML = response.approvedOrderCount.yearlyCount;
                document.getElementById('ADbAMonthlyCount').innerHTML = response.approvedOrderCount.monthlyCount;
                document.getElementById('ADbAWeeklyCount').innerHTML = response.approvedOrderCount.weeklyCount;
                document.getElementById('ADbADayCount').innerHTML = response.approvedOrderCount.dailyCount;

                document.getElementById('ADbATotalCount').innerHTML = response.approvedOrderCount.totalCount;


               
                var ArryItem = [];
                for (var i = 0; i < response.approvedOrderMonthWise.length; i++) {
                    ArryItem.push(response.approvedOrderMonthWise[i].ordersCount);
                }
                //$.each(response.approvedOrderMonthWise, function (i) {
               
                //    ArryItem.push(response.approvedOrderMonthWise(i).ordersCount);
                //});
                var lObjCanvas = $("#column-chart");

                new Chart(document.getElementById("column-chart"), {
                    type: 'bar',
                    data: {
                        labels: ["Jan", "Feb", "March", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec"],
                        datasets: [
                            {
                                backgroundColor: ["#2F4F4F", "#008080", "#2E8B57", "#3CB371", "#90EE90", "#98FB98", "#3CB371", "#9ACD32", "#6B8E23", "#2E8B57", "#3e95cd", "#7FFFD4"],
                                data: ArryItem
                            }
                        ]
                    },
                    options: {
                        legend: { display: false },
                        title: {
                            display: true,
                            text: 'Zone Manager Orders'
                        }
                    }
                });
                new Chart(document.getElementById("simple-pie-chart"), {
                    type: 'pie',
                    data: {
                        labels: ["Jan", "Feb", "March", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec"],
                         datasets: [{
                             backgroundColor: ["#FF99CC", "#4E342E", "#3cba9f", "#D81B60", "#c45850", "#0B5345", "#7D3C98", "#641E16", "#7D6608", "#003399", "#CCFF00", "#990000"],
                             data: ArryItem
                        }]
                    },
                    options: {
                        title: {
                            display: true,
                            text: 'Zone Manager Orders'
                        }
                    }
                });
                new Chart(document.getElementById("simple-doughnut-chart"), {
                    type: 'doughnut',
                    data: {
                        labels: ["Jan", "Feb", "March", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec"],
                        datasets: [{
                            backgroundColor: ["#ffa600", "#ff6361", "#3cba9f", "#9900CC", "#58508d", "#330099", "#8e5ea2", "#3cba9f", "#3e95cd", "#bc5090", "#D35400", "#666600"],
                            data: ArryItem
                        }]
                    },
                    options: {
                        title: {
                            display: true,
                            text: 'Zone Manager Orders'
                        }
                    }
                });

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