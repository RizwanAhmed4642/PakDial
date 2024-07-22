$(function () {


    var Id = $("#clientAssignAPkge").val();
    if (Id > 0) {

        $('#ClientAssignedListPackages').DataTable({
            "processing": true,
            "serverSide": true,
            "destroy": true,
            "ordering": true,
            "responsive": true,
            "autoWidth": false,
            "order": [[0, "desc"]],
            ajax: {
                url: "/ClientListing/LoadAssignedListingPackages",
                type: "POST",
                data: { ListingId: Id },
                datatype: "json",
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            },
            "columns": [
                {
                    "data": "id",
                    "name": "id",
                    "orderable": true,
                    render: function (data, type, row, meta) {
                        return meta.row + meta.settings._iDisplayStart + 1;
                    }
                },
                {
                    "data": "companyName", "name": "companyName", "orderable": false
                },
                {
                    "data": "packageName", "name": "packageName", "orderable": false
                },
                {
                    "data": "price", "name": "price", "orderable": false
                },
                {
                    "data": "listingFrom", "name": "listingFrom", "orderable": false,
                    "render": function (data) {
                        var date = new Date(data);
                        var month = date.getMonth() + 1;
                        return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                    }
                },
                {
                    "data": "listingTo", "name": "listingTo", "orderable": false,
                    "render": function (data) {
                        var date = new Date(data);
                        var month = date.getMonth() + 1;
                        return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                    }
                },
                {
                    "data": "isActive", "name": "isActive", "orderable": false,
                    "render": function (data) {
                        if (data == true) {
                            return '<div class="badge badge-primary">Active</div>';
                        }
                        else {
                            return '<div class="badge badge-danger">InActive</div>';
                        }
                    }
                },
                {
                    "data": "statusName", "name": "statusName", "orderable": false
                },

            ],
            columnDefs: [
                { "width": "10%", "targets": 0 },
                { "width": "15%", "targets": 1 },
                { "width": "15%", "targets": 2 },
                { "width": "10%", "targets": 3 },
                { "width": "15%", "targets": 4 },
                { "width": "15%", "targets": 5 },
                { "width": "10%", "targets": 6 },
                { "width": "10%", "targets": 7 },

            ],

        });
    }
    

});