$(function () {
    $.ajax({
        url: "/AdminDashBoard/LoadCEODashboard",
        type: "get",
        datatype: "json",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            if (response != null) {
                //ListingOrders

                document.getElementById('ADbYearlyCount').innerHTML = response.listingCount.yearlyCount;
                document.getElementById('ADbMonthlyCount').innerHTML = response.listingCount.monthlyCount;
                document.getElementById('ADbWeeklyCount').innerHTML = response.listingCount.weeklyCount;
                document.getElementById('ADbDayCount').innerHTML = response.listingCount.dailyCount;

                document.getElementById('ADbTotalCount').innerHTML = response.listingCount.totalCount;

                //Approved Order
                document.getElementById('ADbAYearlyCount').innerHTML = response.approvedOrderCount.yearlyCount;
                document.getElementById('ADbAMonthlyCount').innerHTML = response.approvedOrderCount.monthlyCount;
                document.getElementById('ADbAWeeklyCount').innerHTML = response.approvedOrderCount.weeklyCount;
                document.getElementById('ADbADayCount').innerHTML = response.approvedOrderCount.dailyCount;

                document.getElementById('ADbATotalCount').innerHTML = response.approvedOrderCount.totalCount;

                //Revenue Order
                document.getElementById('ADbRYearlyCount').innerHTML = response.approvedOrderRevenueCount.yearlyRevenue;
                document.getElementById('ADbRMonthlyCount').innerHTML = response.approvedOrderRevenueCount.monthlyRevenue;
                document.getElementById('ADbRWeeklyCount').innerHTML = response.approvedOrderRevenueCount.weeklyRevenue;
                document.getElementById('ADbRDayCount').innerHTML = response.approvedOrderRevenueCount.dailyRevenue;

                document.getElementById('ADbRTotalCount').innerHTML = response.approvedOrderRevenueCount.totalRevenue;


                var ArryItem = [];
                for (var i = 0; i < response.orderMonthWiseRevnue.length; i++) {
                    ArryItem.push(response.orderMonthWiseRevnue[i].orderRevenue);
                }

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
                            text: 'CEO Orders'
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
                            text: 'CEO Orders Monthly'
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
                            text: 'CEO Orders Monthly'
                        }
                    }
                });


                ////////////////////////////////////////////////Yearly///////////////////////////////////////
                var YArryItem = [];
                for (var i = 0; i < response.orderYearWiseRevnue.length; i++) {
                    YArryItem.push(response.orderYearWiseRevnue[i].orderRevenue);
                }
                var YNArryItem = [];
                for (var i = 0; i < response.orderYearWiseRevnue.length; i++) {
                    YNArryItem.push(response.orderYearWiseRevnue[i].yearNo);
                }

                new Chart(document.getElementById("Ycolumn-chart"), {
                    type: 'bar',
                    data: {
                        labels: YNArryItem,
                        datasets: [
                            {

                                backgroundColor: ["#2F4F4F", "#008080", "#2E8B57", "#3CB371", "#90EE90", "#98FB98", "#3CB371", "#9ACD32", "#6B8E23", "#2E8B57", "#3e95cd", "#7FFFD4"],
                                data: YArryItem
                            }
                        ]
                    },
                    options: {
                        legend: { display: false },
                        title: {
                            display: true,
                            text: 'CEO Orders'
                        }
                    }
                });

                new Chart(document.getElementById("Ysimple-pie-chart"), {
                    type: 'pie',
                    data: {
                        labels: YNArryItem,
                        datasets: [{
                            backgroundColor: ["#008080", "#F1C40F", "#7B68EE", "#A52A2A", "#210B61"],
                            data: YArryItem
                        }]
                    },
                    options: {
                        title: {
                            display: true,
                            text: 'Yearly Chart'
                        }
                    }
                });

                new Chart(document.getElementById("Ysimple-doughnut-chart"), {
                    type: 'doughnut',
                    data: {
                        labels: YNArryItem,
                        datasets: [{
                            backgroundColor: ["#008080", "#F1C40F", "#7B68EE", "#A52A2A", "#210B61"],
                            data: YArryItem
                        }]
                    },
                    options: {
                        title: {
                            display: true,
                            text: 'Yearly Chart'
                        }
                    }
                });



                /////////////////////////////////////State///////////////////////////////////////////////
                var SArryItem = [];
                for (var i = 0; i < response.stateWiseRevenue.length; i++) {
                    SArryItem.push(response.stateWiseRevenue[i].orderRevenue);
                }
                var SNArryItem = [];
                for (var i = 0; i < response.stateWiseRevenue.length; i++) {
                    SNArryItem.push(response.stateWiseRevenue[i].stateName);
                }
                new Chart(document.getElementById("Ssimple-pie-chart"), {
                    type: 'pie',
                    data: {
                        labels: SNArryItem,
                        datasets: [{
                            backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#F5A9D0"],
                            data: SArryItem
                        }]
                    },
                    options: {
                        title: {
                            display: true,
                            text: 'State  Revenue Chart'
                        }
                    }
                });

                new Chart(document.getElementById("Ssimple-doughnut-chart"), {
                    type: 'doughnut',
                    data: {
                        labels: SNArryItem,
                        datasets: [{
                            backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#F5A9D0"],
                            data: SArryItem
                        }]
                    },
                    options: {
                        title: {
                            display: true,
                            text: 'State  Revenue Chart'
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