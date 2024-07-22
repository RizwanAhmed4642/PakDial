$(function () {
    $.ajax({
        url: "/AdminDashBoard/LoadAdminDshboard",
        type: "get",
        datatype: "json",
        async: false,
        headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
        success: function (response) {
            if (response != null) {
               

                document.getElementById('ADbTotalEmployee').innerHTML=    response.spGetAdminResultantCount.totalEmployeeCount
                document.getElementById('ADbTotalCustomer').innerHTML=    response.spGetAdminResultantCount.totalCustomerCount
                document.getElementById('ADbTotalListings').innerHTML=    response.spGetAdminResultantCount.totalListingCount 
                document.getElementById('ADbTotalOrder').innerHTML=  response.spGetAdminResultantCount.totalOrdersCount 
                document.getElementById('ADbTotalApproved').innerHTML= response.spGetAdminResultantCount.totalAppCount
                document.getElementById('ADbTotalRevenue').innerHTML= response.spGetAdminResultantCount.totalRevenue
                
                var ArryItem = [];
                for (var i = 0; i < response.orderYearWise.length; i++) {
                    ArryItem.push(response.orderYearWise[i].ordersCount);
                }

                var YNArryItem = [];
                for (var i = 0; i < response.orderYearWise.length; i++) {
                    YNArryItem.push(response.orderYearWise[i].yearNo);
                }
                new Chart(document.getElementById("YOcolumn-chart"), {
                    type: 'bar',
                    data: {
                        labels: YNArryItem,
                        datasets: [
                            {

                                backgroundColor: ["#2F4F4F", "#008080", "#2E8B57", "#3CB371", "#90EE90"],
                                data: ArryItem
                            }
                        ]
                    },
                    options: {
                        legend: { display: false },
                        title: {
                            display: true,
                            text: 'Admin Orders'
                        }
                    }
                });


                var ORArryItem = [];
                for (var i = 0; i < response.orderYearWiseRevenue.length; i++) {
                    ORArryItem.push(response.orderYearWiseRevenue[i].orderRevenue);
                }

                var OYArryItem = [];
                for (var i = 0; i < response.orderYearWiseRevenue.length; i++) {
                    OYArryItem.push(response.orderYearWiseRevenue[i].yearNo);
                }


                new Chart(document.getElementById("YRcolumn-chart"), {
                    type: 'bar',
                    data: {
                        labels: OYArryItem,
                        datasets: [
                            {

                                backgroundColor: ["#2F4F4F", "#008080", "#2E8B57", "#3CB371", "#90EE90"],
                                data: ORArryItem
                            }
                        ]
                    },
                    options: {
                        legend: { display: false },
                        title: {
                            display: true,
                            text: 'Admin Revenue Orders'
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