$(function () {
    ResetCustomerIds();
    $('#AdminCustomers').DataTable({

        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],

        ajax: {
            url: "/AdminCustomer/LoadCustomer",
            type: "POST",
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
                "data": "fullName", "name": "fullName", "orderable": false
            },
            {
                "data": "phoneNumber", "name": "phoneNumber", "orderable": false

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
                "data": "updatedDate", "name": "updatedDate", "orderable": false,
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },

        ],
        columnDefs: [
            { "width": "5%", "targets": 0 },
            { "width": "13%", "targets": 1 },
            { "width": "13%", "targets": 2 },
            { "width": "13%", "targets": 3 },
            { "width": "10%", "targets": 4 },

            {
                "width": "36%",
                targets: 5,
                render: function (data, type, full, meta) {
                 
                    var Id = full.id;
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateCustomerModal">&nbsp;Edit&nbsp;</button>'
                    var ChangePassword = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + full.userId + ' id="UpdateCustomerPasswordModal">Password</button>'
                    return '<td> ' + Edit + " " + ChangePassword + ' </td>'
                }
            }
        ],

    });

    $("#AddCustomer_Submit_Form").validate({
        rules: {
            ACustFirstName: {
                required: true,
                letteronly: true,
            },
            ACustLastName: {
                required: true,
                letteronly: true
            },
            ACustDob: {
                required: true,
            },
            ACustCnic: {
                required: true,
                numberonlyCnic: true
            },
            ACustPhoneNumber: {
                numberonly: true
            },
            ACustEmail: {
                required: true,
                 emailRegex: true
            },

            ACustPassword: {
                required: true,
                 passwordRegex: true
            },
        },
        messages: {
            ACustFirstName: {
                required: "Please Enter First Name."
            },
            ACustLastName: {
                required: "Please Enter Last Name."
            },
            ACustDob: {
                required: "Please Enter Date of Birth."
            },
            ACustCnic: {
                required: "Please Enter CNIC."
            },
            ACustPhoneNumber: {
                required: "Please Enter Phone Number."
            },
            ACustEmail: {
                required: "Please Enter Email."
            },
            ACustPassword: {
                required: "Please Enter Password."
            },
        },

        highlight: function (element) {
            $(element).closest('.form-group').addClass('text-danger');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('text-danger');
        },
        wrapper: 'div',
        errorClass: 'text-danger',
        errorPlacement: function (error, element) {
            error.insertAfter(element);
        },

        submitHandler: function (form) {
      
            $("#AddCustShowLoader").show();
            $("#AddCustShowButtons").hide();
            var Cust = new CreateUpdateCustomer();
            Cust.FirstName = $("#ACustFirstName").val();
            Cust.LastName = $("#ACustLastName").val();
            Cust.DateOfBirth = $("#ACustDob").val();
            Cust.Cnic = $("#ACustCnic").val();
            Cust.PhoneNumber = $("#ACustPhoneNumber").val();
            Cust.Email = $("#ACustEmail").val();
            Cust.Password = $("#ACustPassword").val();
            Cust.RoleId = $("#ACustRole").val();
            $.ajax({
                url: "/AdminCustomer/AddCustomer",
                type: "post",
                datatype: "json",
                data: { Customer: Cust },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response > 0) {
                        var files = $("#Aimagefiles").get(0).files;
                        var formData = new FormData();
                        formData.append('file', files[0]);
                        if (files.length > 0) {
                            $.ajax({
                                url: "/AdminCustomer/UploadProfileImage?Id=" + response,
                                type: "post",
                                datatype: "json",
                                data: formData,
                                async: false,
                                cache: false,
                                contentType: false,
                                processData: false,
                                success: function (response) {
                                    if (response == 1) {
                                        $("#AddCustomer").modal("hide");
                                        $("#AddCustShowLoader").hide();
                                        $("#AddCustShowButtons").show();
                                        toastr.success("Customer Saved Successfully", "Success");
                                        $('#AdminCustomers').DataTable().ajax.reload();
                                    }
                                    else if (response.error == "403") {
                                        location.href = "/Account/AccessDenied";
                                    }
                                    else {
                                        $("#AddCustShowLoader").hide();
                                        $("#AddCustShowButtons").show();
                                        toastr.error("Failed to Upload Image", "Error");
                                    }
                                },
                                error: function (response) {
                                    $("#AddCustShowLoader").hide();
                                    $("#AddCustShowButtons").show();
                                    toastr.error(response, "Error");
                                }
                            });
                        }
                        else {
                            $("#AddCustomer").modal("hide");
                            $("#AddCustShowLoader").hide();
                            $("#AddCustShowButtons").show();
                            toastr.success("Customer Saved Successfully", "Success");
                            $('#AdminCustomers').DataTable().ajax.reload();
                        }
                    }
                    else if (response == -2) {
                        $("#AddCustShowLoader").hide();
                        $("#AddCustShowButtons").show();
                        toastr.error("Cnic Or Email Already Exits", "Error");
                    }
                    else {
                        $("#AddCustShowLoader").hide();
                        $("#AddCustShowButtons").show();
                        toastr.error("Customer Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddCustShowLoader").hide();
                    $("#AddCustShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
     //Onclick Add Form Open
    $("#AddCustomerModal").on('click', function () {
        var AddCustomervalidator = $("#AddCustomer_Submit_Form").validate();
        AddCustomervalidator.resetForm();
        ResetCustomerIds();
        AddBindRole();
        $('#AtargetImg').attr("src", "/app-assets/images/portrait/small/avatar-s-19.jpg");
        $("#AddCustomer").modal('show');
    });

     //-----------------------------------------------------------------------Update Form----------------------------------------------------------------

    $("#AdminCustomers").on('click', '#UpdateCustomerModal', function () {
        
        var Id = $(this).val();
        var UpdateCustomervalidator = $("#UpdateCustomer_Submit_Form").validate();
        UpdateCustomervalidator.resetForm();
        ResetCustomerIds();
        if (Id != "" && Id != null) {
            
            $.ajax({
                url: "/AdminCustomer/GetCustomerById",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                   
                    if (response != null) {
                      
                        $("#UCustId").val(response.customers.id);
                        if (response.customers.imagePath == null) {
                            $('#UtargetImg').attr("src", "/app-assets/images/portrait/small/avatar-s-19.jpg");
                        }
                        else {
                            $('#UtargetImg').attr("src", response.customers.imagePath);
                        }
                        if (response.customers.isActive == true) {
                            $("#UCIsActive").prop('checked', true);
                        }
                        $("#UUserId").val(response.customers.userId);
                        $("#UCustFirstName").val(response.customers.firstName);
                        $("#UCustLastName").val(response.customers.lastName);
                        $("#UCustDob").val(dateFormat(new Date(Date.parse(response.customers.dateOfBirth))));
                        $("#UCustCnic").val(response.customers.cnic);
                        $("#UCustContactNumber").val(response.customers.phoneNumber);
                        $("#UCustPhoneNumber").val(response.customers.phoneNumber);
                        $("#UpdateCustomer").modal('show');
                    }
                    else {
                        toastr.error("Customer Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
    });
    $("#ACustPhoneNumber").focusout(function () {
        var Mobile = $(this).val();
        var Id = $("#UCustId").val();
        if (Mobile != "") {
            $.ajax({
                url: "/AdminCustomer/CheckMobileExiting",
                type: "get",
                datatype: "json",
                data: { phone: Mobile, id: Id},
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 1) {
                        toastr.error("Number Already Exits");
                        $("#ACustPhoneNumber").val("");
                        $("#ACustPhoneNumber").focus();
                    }
                  
                },
                error: function (response) {
                    toastr.error(response);
                }

            });
        }

    }); 
    $("#UCustPhoneNumber").focusout(function () {
        var Mobile = $(this).val();
        var Id = $("#UCustId").val();
        if (Mobile != "") {
            $.ajax({
                url: "/AdminCustomer/CheckMobileExiting",
                type: "get",
                datatype: "json",
                data: { phone: Mobile, id: Id },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response > 0) {
                        toastr.error("Number Already Exits");
                        $("#ACustPhoneNumber").val("");
                        $("#ACustPhoneNumber").focus();
                    }

                },
                error: function (response) {
                    toastr.error(response);
                }

            });
        }

    }); 
    //Update Employees Form Submit
    $("#UpdateCustomer_Submit_Form").validate({
        
        rules: {
            UCustId: {
                required: true,
            },
            UUserId: {
                required: true,
            },
            UCustFirstName: {
                required: true,
                letteronly: true,
            },
            UCustLastName: {
                required: true,
                letteronly: true
            },
            UCustDob: {
                required: true,
            },
            UCustCnic: {
                required: true,
                numberonlyCnic: true
            },
            UCustContactNumber: {
                required: true,
                numberonly: true
            },
            UCustPhoneNumber: {
                numberonly: true
            },
        },
        messages: {
            UCustId: {
                required: "Please provide a valid Employee Passcode."
            },
            UUserId: {
                required: "Please provide a valid Employee Passcode."
            },
            UCustFirstName: {
                required: "Please Enter First Name."
            },
            UCustLastName: {
                required: "Please Enter Last Name."
            },
            UCustDob: {
                required: "Please Enter Date of Birth."
            },
            UCustCnic: {
                required: "Please Enter CNIC."
            },
            UCustContactNumber: {
                required: "Please Enter Contact Number."
            },
        },
       
        highlight: function (element) {
            $(element).closest('.form-group').addClass('text-danger');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('text-danger');
        },
        wrapper: 'div',
        errorClass: 'text-danger',
        errorPlacement: function (error, element) {
            error.insertAfter(element);
        },

        submitHandler: function (form) {
       
            $("#UpdateCustShowLoader").show();
            $("#UpdateCustShowButtons").hide();
            var Cust = new CreateUpdateCustomer();
            Cust.Id = $("#UCustId").val();
            Cust.UserId = $("#UUserId").val();
            Cust.FirstName = $("#UCustFirstName").val();
            Cust.LastName = $("#UCustLastName").val();
            Cust.DateOfBirth = $("#UCustDob").val();
            Cust.Cnic = $("#UCustCnic").val();
            Cust.ContactNo = $("#UCustContactNumber").val();
            Cust.PhoneNumber = $("#UCustContactNumber").val();
            if ($("#UCIsActive").prop('checked') == true) {
                Cust.IsActive = true;
            }
            $.ajax({
                url: "/AdminCustomer/UpdateCustomer",
                type: "post",
                datatype: "json",
                data: { customer: Cust },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response == 1) {
                        var files = $("#Uimagefiles").get(0).files;
                        var formData = new FormData();
                        var Id = $("#UCustId").val();
                        formData.append('file', files[0]);
                        if (files.length > 0) {
                            $.ajax({
                                url: "/AdminCustomer/UploadProfileImage?Id=" + Id,
                                type: "post",
                                datatype: "json",
                                data: formData,
                                async: false,
                                cache: false,
                                contentType: false,
                                processData: false,
                                success: function (response) {
                                    if (response == 1) {
                                        $("#UpdateCustomer").modal("hide");
                                        $("#UpdateCustShowLoader").hide();
                                        $("#UpdateCustShowButtons").show();
                                        toastr.success("Customer Updated Successfully", "Success");
                                        $('#AdminCustomers').DataTable().ajax.reload();
                                    }
                                    else if (response.error == "403") {
                                        location.href = "/Account/AccessDenied";
                                    }
                                    else {
                                        $("#UpdateCustShowLoader").hide();
                                        $("#UpdateCustShowButtons").show();
                                        toastr.error("Failed to Upload Image", "Error");
                                    }
                                },
                                error: function (response) {
                                    $("#UpdateCustShowLoader").hide();
                                    $("#UpdateCustShowButtons").show();
                                    toastr.error(response, "Error");
                                }
                            });
                        }
                        else {
                            $("#UpdateCustomer").modal("hide");
                            $("#UpdateCustShowLoader").hide();
                            $("#UpdateCustShowButtons").show();
                            toastr.success("Customer Updated Successfully", "Success");
                            $('#AdminCustomers').DataTable().ajax.reload();
                        }
                    }
                    else if (response == 2) {
                        $("#UpdateCustShowLoader").hide();
                        $("#UpdateCustShowButtons").show();
                        toastr.error("Cnic Already Exits", "Error");
                    }
                    else {
                        $("#UpdateCustShowLoader").hide();
                        $("#UpdateCustShowButtons").show();
                        toastr.error("Customer Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateCustShowLoader").hide();
                    $("#UpdateCustShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    function AddBindRole() {

         var BindAddRoles = $("#ACustRole");
         BindAddRoles.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
          $.ajax({
              url: "/AdminCustomer/GetRole",
            type: "get",
            datatype: "json",
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
        
                BindAddRoles.empty().append('<option selected="selected" value="">Please select</option>');
                
                //$.each(response, function (i) {
                    BindAddRoles.append($("<option></option>").val(response[0].id).html(response[0].name));
               //});
                
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }
    //----------------------------------UpLoad Image--------------------------------------------------------

    $("#Aimagefiles").change(function () {
        var File = this.files;
        if (File && File[0]) {
            AReadImage(File[0])
        }

    });

    var AReadImage = function (file) {

        var reader = new FileReader;
        var image = new Image;
        reader.readAsDataURL(file);
        if (file.size > 512000) {

            toastr.error("please upload less than 500 kb image");
            return;

        }
        reader.onload = function (_file) {

            image.src = _file.target.result;
            image.onload = function () {

                var height = this.height;
                var width = this.width;
                var type = file.type;
                var size = ~~(file.size / 1024) + "KB";

                $("#AtargetImg").attr('src', _file.target.result);
            }

        }

    }

    $("#Uimagefiles").change(function () {
        var File = this.files;
        if (File && File[0]) {
            UReadImage(File[0]);
        }
    });

    var UReadImage = function (file) {

        var reader = new FileReader;
        var image = new Image;
        reader.readAsDataURL(file);
        if (file.size > 512000) {

            toastr.error("please upload less than 500 kb image");
            return;

        }
        reader.onload = function (_file) {

            image.src = _file.target.result;
            image.onload = function () {

                var height = this.height;
                var width = this.width;
                var type = file.type;
                var size = ~~(file.size / 1024) + "KB";

                $("#UtargetImg").attr('src', _file.target.result);
            }

        }

    }

    //----------------------------------------ResetCustomer---------------------------------------------

    function ResetCustomerIds() {

        $("#ACustFirstName").val('');
        $("#ACustLastName").val('');
        $("#ACustDob").val('');
        $("#ACustCnic").val('');
        $("#ACustContactNumber").val('');
        $("#ACustPhoneNumber").val('');
        $("#Aimagefiles").val('');
        $('#AtargetImg').attr("src", "");
        $('#ACustEmail').val('');

        $("#UCustId").val('')
        $("#UUserId").val('');
        $("#UCustFirstName").val('');
        $("#UCustLastName").val('');
        $("#UCustDob").val('');
        $("#UCustCnic").val('');
        $("#UCustContactNumber").val('');
        $("#UCustPhoneNumber").val('');
        $("#Uimagefiles").val('');
        $('#UtargetImg').attr("src", "");

    }


    $('#ACustCnic,#UCustCnic').keydown(function () {
        //allow  backspace, tab, ctrl+A, escape, carriage return
        if (event.keyCode == 8 || event.keyCode == 9
            || event.keyCode == 27 || event.keyCode == 13
            || (event.keyCode == 65 && event.ctrlKey === true))
            return;
        if ((event.keyCode < 48 && event.keyCode > 57) || (event.keyCode < 96 && event.keyCode > 105))
            event.preventDefault();
        var length = $(this).val().length;
        if (length == 5 || length == 13)
            $(this).val($(this).val() + '-');
    });

    //-----------------------------------------------Changes Password------------------------------------------------------------
    //Onclick Change Password Form Open
    $("#AdminCustomers").on('click', '#UpdateCustomerPasswordModal', function () {

        $("#UCCustUserId").val('');
        $("#UCCustPassword").val('');
        $("#UCCustUserId").val($(this).val());
        $("#UpdateCustChangePassword").modal('show');
    });

    //Update Change Password
    $("#UpdateCustPass_Submit_Form").validate({
        rules: {
            UCCustUserId: {
                required: true,
            },
            UCCustPassword: {
                required: true,
                passwordRegex: true
            },
        },
        messages: {
            UCCustUserId: {
                required: "Customer Passcode Required."
            },
            UCCustPassword: {
                required: "Please Enter Password."
            },
        },

        highlight: function (element) {
            $(element).closest('.form-group').addClass('text-danger');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('text-danger');
        },
        wrapper: 'div',
        errorClass: 'text-danger',
        errorPlacement: function (error, element) {
            error.insertAfter(element);
        },

        submitHandler: function (form) {
            $("#UpdateCustPassShowLoader").show();
            $("#UpdateCustPassShowButtons").hide();
            $.ajax({
                url: "/AdminCustomer/ChangesPassword",
                type: "post",
                datatype: "json",
                data: { UserId: $("#UCCustUserId").val(), newPassword: $("#UCCustPassword").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response > 0) {
                        $("#UpdateCustChangePassword").modal("hide");
                        $("#UpdateCustPassShowLoader").hide();
                        $("#UpdateCustPassShowButtons").show();
                        toastr.success("Password Changes Successfully", "Success");
                        $('#AdminCustomers').DataTable().ajax.reload();
                    }
                    else {
                        $("#UpdateCustPassShowLoader").hide();
                        $("#UpdateCustPassShowButtons").show();
                        toastr.error("Password Not Changed", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateCustPassShowLoader").hide();
                    $("#UpdateCustPassShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

});