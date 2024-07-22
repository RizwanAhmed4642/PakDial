$(function () {
    ResetEmployeeIds();
    $('#AdminEmployees').DataTable({
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ordering": true,
        "responsive": true,
        "autoWidth": false,
        "order": [[0, "desc"]],
        ajax: {
            url: "/AdminEmployee/LoadEmployees",
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
            { "data": "fullName", "name": "fullName", "orderable": false },
            { "data": "email", "name": "email", "orderable": false },
            { "data": "designationName", "name": "designationName", "orderable": false },
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
            { "width": "10%", "targets": 5 },
            {
                "width": "36%",
                targets: 6,
                render: function (data, type, full, meta) {
                    var Id = full.id;
                    var Edit = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateEmployeeModal">&nbsp;Edit&nbsp;</button>'
                    var Permission = '<button type="button" class="btn mr-1 mb-1 btn-secondary btn-sm" value=' + full.userId +"||"+ full.roleId + ' id="UpdateEmployeePermission">Permission</button>'
                    var ChangePassword = '<button type="button" class="btn mr-1 mb-1 btn-danger btn-sm" value=' + full.userId + ' id="UpdateEmployeePasswordModal">Password</button>'
                    var OtherInfo = '';
                    if (full.designationName.trim().toLowerCase() == 'RegionalManager'.trim().toLowerCase()) {
                        OtherInfo = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateRegionalManagerModel">OtherInfo</button>'
                    }
                    else if (full.designationName.trim().toLowerCase() == 'CategoryManager'.trim().toLowerCase()) {
                        OtherInfo = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateCategoryManagerModel">OtherInfo</button>'
                    }
                    else if (full.designationName.trim().toLowerCase() == 'ZoneManager'.trim().toLowerCase()) {
                        OtherInfo = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateZoneManagerModel">OtherInfo</button>'
                    }
                    else {
                        OtherInfo = '<button type="button" class="btn mr-1 mb-1 btn-primary btn-sm" value=' + Id + ' id="UpdateOtherManagerModel">OtherInfo</button>'
                    }
                    return '<td> ' + Edit + " " + Permission + " " + ChangePassword + " " + OtherInfo + ' </td>'
                }
            }
        ],

    });

    //Onclick Add Form Open
    $("#AddEmployeeModal").on('click', function () {
        
        var AddEmployeevalidator = $("#AddEmployee_Submit_Form").validate();
        AddEmployeevalidator.resetForm();
        ResetEmployeeIds();
        AddBindRoleDesignationCountry();
        $('#AtargetImg').attr("src", "/app-assets/images/portrait/small/avatar-s-19.jpg");
        $("#AddEmployee").modal('show');
    });

    //Add Employees Form Submit
    $("#AddEmployee_Submit_Form").validate({
        rules: {
            AEmpFirstName: {
                required: true,
                letteronly: true,
            },
            AEmpLastName: {
                required: true,
                letteronly: true
            },
            AEmpDob: {
                required: true,
            },
            AEmpCnic: {
                required: true,
                numberonlyCnic: true
            },
            AEmpPassport: {
                letternumberonly: true
            },
            AEmpContactNumber: {
                required: true,
                numberonly: true,
                minlength: 11,
                maxlength: 11,
            },
            AEmpPhoneNumber: {
                numberonly: true,
                minlength: 10,
                maxlength:11
            },
            AEmpCountry: {
                required: true,
                numberonly: true
            },
            AEmpStatus: {
                required: true,
                numberonly: true
            },
            AEmpCity: {
                required: true,
                numberonly: true
            },
            AEmpCityArea: {
                required: true,
                numberonly: true
            },
            AEmpAddress: {
                required: true,
            },
            AEmpRole: {
                required: true,
            },
            AEmpDesignation: {
                required: true,
                numberonly: true
            },
            AEmpEmail: {
                required: true,
                emailRegex: true
            },
            AEmpPassword: {
                required: true,
                passwordRegex: true
            },
        },
        messages: {
            AEmpFirstName: {
                required: "Please Enter First Name."
            },
            AEmpLastName: {
                required: "Please Enter Last Name."
            },
            AEmpDob: {
                required: "Please Enter Date of Birth."
            },
            AEmpCnic: {
                required: "Please Enter CNIC."
            },
            AEmpContactNumber: {
                required: "Please Enter Contact Number."
            },
            AEmpCountry: {
                required: "Please Select Country."
            },
            AEmpStatus: {
                required: "Please Select State."
            },
            AEmpCity: {
                required: "Please Select City."
            },
            AEmpCityArea: {
                required: "Please Select City Area."
            },
            AEmpAddress: {
                required: "Please Select Address."
            },
            AEmpRole: {
                required: "Please Select Role."
            },
            AEmpDesignation: {
                required: "Please Select Designation."
            },
            AEmpEmail: {
                required: "Please Enter Email."
            },
            AEmpPassword: {
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
            $("#AddEmpShowLoader").show();
            $("#AddEmpShowButtons").hide();
            var emp = new CreateUpdateEmployee();
            emp.FirstName = $("#AEmpFirstName").val();
            emp.LastName = $("#AEmpLastName").val();
            emp.DateOfBirth = $("#AEmpDob").val();
            emp.Cnic = $("#AEmpCnic").val();
            emp.PassportNo = $("#AEmpPassport").val();
            emp.DesignationId = $("#AEmpDesignation").val();
            emp.EmpAddress = $("#AEmpAddress").val();
            emp.CountryId = $("#AEmpCountry").val();
            emp.ProvinceId = $("#AEmpStatus").val();
            emp.CityId = $("#AEmpCity").val();
            emp.CityAreaId = $("#AEmpCityArea").val();
            emp.ContactNo = $("#AEmpContactNumber").val();
            emp.PhoneNo = $("#AEmpPhoneNumber").val();
            emp.Email = $("#AEmpEmail").val();
            emp.Password = $("#AEmpPassword").val();
            emp.RoleId = $("#AEmpRole").val();
            $.ajax({
                url: "/AdminEmployee/AddEmployee",
                type: "post",
                datatype: "json",
                data: { employee: emp },
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
                                url: "/AdminEmployee/UploadProfileImage?Id=" + response,
                                type: "post",
                                datatype: "json",
                                data: formData,
                                async: false,
                                cache: false,
                                contentType: false,
                                processData: false,
                                success: function (response) {
                                    if (response == 1) {
                                        $("#AddEmployee").modal("hide");
                                        $("#AddEmpShowLoader").hide();
                                        $("#AddEmpShowButtons").show();
                                        toastr.success("Employee Saved Successfully", "Success");
                                        $('#AdminEmployees').DataTable().ajax.reload();
                                    }
                                    else if (response.error == "403") {
                                        location.href = "/Account/AccessDenied";
                                    }
                                    else {
                                        $("#AddEmpShowLoader").hide();
                                        $("#AddEmpShowButtons").show();
                                        toastr.error("Failed to Upload Image", "Error");
                                    }
                                },
                                error: function (response) {
                                    $("#AddEmpShowLoader").hide();
                                    $("#AddEmpShowButtons").show();
                                    toastr.error(response, "Error");
                                }
                            });
                        }
                        else {
                            $("#AddEmployee").modal("hide");
                            $("#AddEmpShowLoader").hide();
                            $("#AddEmpShowButtons").show();
                            toastr.success("Employee Saved Successfully", "Success");
                            $('#AdminEmployees').DataTable().ajax.reload();
                        }
                    }
                    else if (response == -2) {
                        $("#AddEmpShowLoader").hide();
                        $("#AddEmpShowButtons").show();
                        toastr.error("Cnic Or Email Already Exits", "Error");
                    }
                    else {
                        $("#AddEmpShowLoader").hide();
                        $("#AddEmpShowButtons").show();
                        toastr.error("Employee Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#AddEmpShowLoader").hide();
                    $("#AddEmpShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //Get All Country Designation and Roles For Binding With Add DropDown List
    function AddBindRoleDesignationCountry() {
        var BindAddCountry = $("#AEmpCountry");
        BindAddCountry.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        var BindAddRoles = $("#AEmpRole");
        BindAddRoles.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        var BindAddDesignation = $("#AEmpDesignation");
        BindAddDesignation.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        $.ajax({
            url: "/AdminEmployee/GetCuntryRoleDesig",
            type: "get",
            datatype: "json",
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                BindAddCountry.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response.country, function (i) {
                    BindAddCountry.append($("<option></option>").val(this['id']).html(this['name']));
                });
                BindAddRoles.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response.roles, function (i) {
                    BindAddRoles.append($("<option></option>").val(this['id']).html(this['name']));
                });
                BindAddDesignation.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response.designation, function (i) {
                    BindAddDesignation.append($("<option></option>").val(this['id']).html(this['name']));
                });
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }

    // Add On Get All States By CountryId
    $("#AEmpCountry").change(function () {
        var CountryId = $(this).val();
        $("#AEmpStatus").empty().append('<option selected="selected" value="">Please select</option>');
        $("#AEmpCity").empty().append('<option selected="selected" value="">Please select</option>');
        $("#AEmpCityArea").empty().append('<option selected="selected" value="">Please select</option>');
        if (CountryId != "") {
            var BindAddState = $("#AEmpStatus");
            BindAddState.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
            $.ajax({
                url: "/AdminProvince/GetAllStates",
                type: "get",
                datatype: "json",
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                data: { CountryId: CountryId },
                success: function (response) {
                    BindAddState.empty().append('<option selected="selected" value="">Please select</option>');
                    $.each(response, function (i) {
                        BindAddState.append($("<option></option>").val(this['id']).html(this['name']));
                    });
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
        else {
            $("#AEmpStatus").empty().append('<option selected="selected" value="">Please select</option>');
        }
    });

    // Add On Get All Cities By StateId
    $("#AEmpStatus").change(function () {
        var StateId = $(this).val();
        $("#AEmpCity").empty().append('<option selected="selected" value="">Please select</option>');
        $("#AEmpCityArea").empty().append('<option selected="selected" value="">Please select</option>');
        if (StateId != "") {
            var BindAddCity = $("#AEmpCity");
            BindAddCity.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
            $.ajax({
                url: "/AdminCity/GetAllCitiesByStateId",
                type: "get",
                datatype: "json",
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                data: { Id: StateId },
                success: function (response) {
                    BindAddCity.empty().append('<option selected="selected" value="">Please select</option>');
                    $.each(response, function (i) {
                        BindAddCity.append($("<option></option>").val(this['id']).html(this['name']));
                    });
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
        else {
            $("#AEmpCity").empty().append('<option selected="selected" value="">Please select</option>');
        }
    });

    // Add On Get All Cities By CityId
    $("#AEmpCity").change(function () {
        var CityId = $(this).val();
        $("#AEmpCityArea").empty().append('<option selected="selected" value="">Please select</option>');
        if (CityId != "") {
            var BindAddCityArea = $("#AEmpCityArea");
            BindAddCityArea.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
            $.ajax({
                url: "/AdminCityAreas/GetAllCityAreaByCityId",
                type: "get",
                datatype: "json",
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                data: { CityId: CityId },
                success: function (response) {
                    BindAddCityArea.empty().append('<option selected="selected" value="">Please select</option>');
                    $.each(response, function (i) {
                        BindAddCityArea.append($("<option></option>").val(this['id']).html(this['name']));
                    });
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
        else {
            $("#AEmpCityArea").empty().append('<option selected="selected" value="">Please select</option>');
        }
    });

    //Check Cnic On Focus Out
    $("#AEmpCnic").focusout(function () {
        var Cnic = $(this).val();
        if (Cnic != "") {
            $.ajax({
                url: "/AdminCommon/CheckCNICExits",
                type: "get",
                datatype: "json",
                data: { Cnic: Cnic, EmpId: 0 },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 0) {
                    }
                    else {
                        toastr.error(response + " " + "CNIC Already Exits");
                        $("#AEmpCnic").val("");
                        $("#AEmpCnic").focus();
                    }
                },
                error: function (response) {
                    toastr.error(response);
                }

            });
        }
    });

    //Check Email On Focus Out
    $("#AEmpEmail").focusout(function () {
        if ($('#AddEmployee').is(':visible') == true) {
            var Email = $(this).val();
            if (Email != "") {
                $.ajax({
                    url: "/AdminCommon/CheckEmailExits",
                    type: "get",
                    datatype: "json",
                    data: { Email: Email },
                    headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                    success: function (response) {
                        if (response == 0) {
                        }
                        else {
                            toastr.error(response + " " + "Already Exits");
                            $("#AEmpEmail").val("");
                            $("#AEmpEmail").focus();
                        }
                    },
                    error: function (response) {
                        toastr.error(response);
                    }

                });
            }
        }
    });

    $('#AEmpCnic,#UEmpCnic').keydown(function () {
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

    //--------------------------------------------------------------Update Employee--------------------------------------------------

    //Onclick Edit Form Open
    $("#AdminEmployees").on('click', '#UpdateEmployeeModal', function () {
        var Id = $(this).val();
        var UpdateEmployeevalidator = $("#UpdateEmployee_Submit_Form").validate();
        UpdateEmployeevalidator.resetForm();
        ResetEmployeeIds();
        UpdateBindRoleDesignationCountry();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminEmployee/GetEmployeeById",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response != null) {
                        UpdateBindStateCityCArea(response);
                        $("#UEmpId").val(response.employees.id);
                        if (response.employees.imagePath == null) {
                            $('#UtargetImg').attr("src", "/app-assets/images/portrait/small/avatar-s-19.jpg");
                        }
                        else {
                            $('#UtargetImg').attr("src", response.employees.imagePath);
                        }
                        if (response.employees.isActive == true) {
                            $("#EUIsActive").prop('checked', true);
                        }
                        $("#UUserId").val(response.employees.userId);
                        $("#UEmpFirstName").val(response.employees.firstName);
                        $("#UEmpLastName").val(response.employees.lastName);
                        $("#UEmpDob").val(dateFormat(new Date(Date.parse(response.employees.dateOfBirth))));
                        $("#UEmpCnic").val(response.employees.cnic);
                        $("#UEmpPassport").val(response.employees.passportNo);
                        $("#UEmpContactNumber").val(response.contacts.contactNo);
                        $("#UEmpPhoneNumber").val(response.contacts.phoneNo);
                        $("#UEmpCountry").find('option[value="' + response.addresses.countryId + '"]').attr('selected', 'selected');
                        $("#UEmpStatus").find('option[value="' + response.addresses.provinceId + '"]').attr('selected', 'selected');
                        $("#UEmpCity").find('option[value="' + response.addresses.cityId + '"]').attr('selected', 'selected');
                        $("#UEmpCityArea").find('option[value="' + response.addresses.cityAreaId + '"]').attr('selected', 'selected');
                        $("#UEmpAddress").val(response.addresses.empAddress);
                        $("#UEmpRole").find('option[value="' + response.employees.roleId + '"]').attr('selected', 'selected');
                        $("#UEmpDesignation").find('option[value="' + response.employees.designationId + '"]').attr('selected', 'selected');
                        $("#UpdateEmployee").modal('show');
                    }
                    else {
                        toastr.error("Employee Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //Update Employees Form Submit
    $("#UpdateEmployee_Submit_Form").validate({
        rules: {
            UEmpId: {
                required: true,
            },
            UUserId: {
                required: true,
            },
            UEmpFirstName: {
                required: true,
                letteronly: true,
            },
            UEmpLastName: {
                required: true,
                letteronly: true
            },
            UEmpDob: {
                required: true,
            },
            UEmpCnic: {
                required: true,
                numberonlyCnic: true
            },
            UEmpPassport: {
                letternumberonly: true
            },
            UEmpContactNumber: {
                required: true,
                numberonly: true,
                minlength: 11,
                maxlength: 11
            },
            UEmpPhoneNumber: {
                numberonly: true,
                minlength: 10,
                maxlength: 11
            },
            UEmpCountry: {
                required: true,
                numberonly: true
            },
            UEmpStatus: {
                required: true,
                numberonly: true
            },
            UEmpCity: {
                required: true,
                numberonly: true
            },
            UEmpCityArea: {
                required: true,
                numberonly: true
            },
            UEmpAddress: {
                required: true,
            },
            UEmpRole: {
                required: true,
            },
            UEmpDesignation: {
                required: true,
                numberonly: true
            },
        },
        messages: {
            UEmpId: {
                required: "Please provide a valid Employee Passcode."
            },
            UUserId: {
                required: "Please provide a valid Employee Passcode."
            },
            UEmpFirstName: {
                required: "Please Enter First Name."
            },
            UEmpLastName: {
                required: "Please Enter Last Name."
            },
            UEmpDob: {
                required: "Please Enter Date of Birth."
            },
            UEmpCnic: {
                required: "Please Enter CNIC."
            },
            UEmpContactNumber: {
                required: "Please Enter Contact Number."
            },
            UEmpCountry: {
                required: "Please Select Country."
            },
            UEmpStatus: {
                required: "Please Select State."
            },
            UEmpCity: {
                required: "Please Select City."
            },
            UEmpCityArea: {
                required: "Please Select City Area."
            },
            UEmpAddress: {
                required: "Please Select Address."
            },
            UEmpRole: {
                required: "Please Select Role."
            },
            UEmpDesignation: {
                required: "Please Select Designation."
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
            $("#UpdateEmpShowLoader").show();
            $("#UpdateEmpShowButtons").hide();
            var emp = new CreateUpdateEmployee();
            emp.EmployeeId = $("#UEmpId").val();
            emp.UserId = $("#UUserId").val();
            emp.FirstName = $("#UEmpFirstName").val();
            emp.LastName = $("#UEmpLastName").val();
            emp.DateOfBirth = $("#UEmpDob").val();
            emp.Cnic = $("#UEmpCnic").val();
            emp.PassportNo = $("#UEmpPassport").val();
            emp.DesignationId = $("#UEmpDesignation").val();
            emp.EmpAddress = $("#UEmpAddress").val();
            emp.CountryId = $("#UEmpCountry").val();
            emp.ProvinceId = $("#UEmpStatus").val();
            emp.CityId = $("#UEmpCity").val();
            emp.CityAreaId = $("#UEmpCityArea").val();
            emp.ContactNo = $("#UEmpContactNumber").val();
            emp.PhoneNo = $("#UEmpPhoneNumber").val();
            emp.RoleId = $("#UEmpRole").val();
            if ($("#EUIsActive").prop('checked') == true) {
                emp.IsActive = true;
            }
            $.ajax({
                url: "/AdminEmployee/UpdateEmployee",
                type: "post",
                datatype: "json",
                data: { employee: emp },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response == 1) {
                        var files = $("#Uimagefiles").get(0).files;
                        var formData = new FormData();
                        var Id = $("#UEmpId").val();
                        formData.append('file', files[0]);
                        if (files.length > 0) {
                            $.ajax({
                                url: "/AdminEmployee/UploadProfileImage?Id=" + Id,
                                type: "post",
                                datatype: "json",
                                data: formData,
                                async: false,
                                cache: false,
                                contentType: false,
                                processData: false,
                                success: function (response) {
                                    if (response == 1) {
                                        $("#UpdateEmployee").modal("hide");
                                        $("#UpdateEmpShowLoader").hide();
                                        $("#UpdateEmpShowButtons").show();
                                        toastr.success("Employee Updated Successfully", "Success");
                                        $('#AdminEmployees').DataTable().ajax.reload();
                                    }
                                    else if (response.error == "403") {
                                        location.href = "/Account/AccessDenied";
                                    }
                                    else {
                                        $("#UpdateEmpShowLoader").hide();
                                        $("#UpdateEmpShowButtons").show();
                                        toastr.error("Failed to Upload Image", "Error");
                                    }
                                },
                                error: function (response) {
                                    $("#UpdateEmpShowLoader").hide();
                                    $("#UpdateEmpShowButtons").show();
                                    toastr.error(response, "Error");
                                }
                            });
                        }
                        else {
                            $("#UpdateEmployee").modal("hide");
                            $("#UpdateEmpShowLoader").hide();
                            $("#UpdateEmpShowButtons").show();
                            toastr.success("Employee Updated Successfully", "Success");
                            $('#AdminEmployees').DataTable().ajax.reload();
                        }
                    }
                    else if (response == 2) {
                        $("#UpdateEmpShowLoader").hide();
                        $("#UpdateEmpShowButtons").show();
                        toastr.error("Cnic Already Exits", "Error");
                    }
                    else {
                        $("#UpdateEmpShowLoader").hide();
                        $("#UpdateEmpShowButtons").show();
                        toastr.error("Employee Not Saved", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateEmpShowLoader").hide();
                    $("#UpdateEmpShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    function UpdateBindStateCityCArea(response) {
        $("#UEmpStatus").empty().append('<option selected="selected" value="">Please select</option>');
        $("#UEmpStatus").append($("<option></option>").val(response.states.id).html(response.states.name));

        $("#UEmpCity").empty().append('<option selected="selected" value="">Please select</option>');
        $("#UEmpCity").append($("<option></option>").val(response.cities.id).html(response.cities.name));

        $("#UEmpCityArea").empty().append('<option selected="selected" value="">Please select</option>');
        $("#UEmpCityArea").append($("<option></option>").val(response.cityAreas.id).html(response.cityAreas.name));
    }

    //Get All Country Designation and Roles For Binding With Add DropDown List On Update
    function UpdateBindRoleDesignationCountry() {
        var BindUpdateCountry = $("#UEmpCountry");
        BindUpdateCountry.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        var BindUpdateRoles = $("#UEmpRole");
        BindUpdateRoles.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        var BindUpdateDesignation = $("#UEmpDesignation");
        BindUpdateDesignation.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
        $.ajax({
            url: "/AdminEmployee/GetCuntryRoleDesig",
            type: "get",
            datatype: "json",
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                BindUpdateCountry.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response.country, function (i) {
                    BindUpdateCountry.append($("<option></option>").val(this['id']).html(this['name']));
                });
                BindUpdateRoles.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response.roles, function (i) {
                    BindUpdateRoles.append($("<option></option>").val(this['id']).html(this['name']));
                });
                BindUpdateDesignation.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(response.designation, function (i) {
                    BindUpdateDesignation.append($("<option></option>").val(this['id']).html(this['name']));
                });
            },
            error: function (response) {
                toastr.error(response, "Error");
            }

        });
    }

    // Add On Get All States By CountryId
    $("#UEmpCountry").change(function () {
        var CountryId = $(this).val();
        $("#UEmpStatus").empty().append('<option selected="selected" value="">Please select</option>');
        $("#UEmpCity").empty().append('<option selected="selected" value="">Please select</option>');
        $("#UEmpCityArea").empty().append('<option selected="selected" value="">Please select</option>');
        if (CountryId != "") {
            var BindAddState = $("#UEmpStatus");
            BindAddState.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
            $.ajax({
                url: "/AdminProvince/GetAllStates",
                type: "get",
                datatype: "json",
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                data: { CountryId: CountryId },
                success: function (response) {
                    BindAddState.empty().append('<option selected="selected" value="">Please select</option>');
                    $.each(response, function (i) {
                        BindAddState.append($("<option></option>").val(this['id']).html(this['name']));
                    });
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
        else {
            $("#UEmpStatus").empty().append('<option selected="selected" value="">Please select</option>');
        }
    });

    // Add On Get All Cities By StateId
    $("#UEmpStatus").change(function () {
        var StateId = $(this).val();
        $("#UEmpCity").empty().append('<option selected="selected" value="">Please select</option>');
        $("#UEmpCityArea").empty().append('<option selected="selected" value="">Please select</option>');
        if (StateId != "") {
            var BindUpdateCity = $("#UEmpCity");
            BindUpdateCity.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
            $.ajax({
                url: "/AdminCity/GetAllCitiesByStateId",
                type: "get",
                datatype: "json",
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                data: { Id: StateId },
                success: function (response) {
                    BindUpdateCity.empty().append('<option selected="selected" value="">Please select</option>');
                    $.each(response, function (i) {
                        BindUpdateCity.append($("<option></option>").val(this['id']).html(this['name']));
                    });
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
        else {
            $("#UEmpCity").empty().append('<option selected="selected" value="">Please select</option>');
        }
    });

    // Add On Get All Cities By CityId
    $("#UEmpCity").change(function () {
        var CityId = $(this).val();
        $("#UEmpCityArea").empty().append('<option selected="selected" value="">Please select</option>');
        if (CityId != "") {
            var BindUpdateCityArea = $("#AEmpCityArea");
            BindUpdateCityArea.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
            $.ajax({
                url: "/AdminCityAreas/GetAllCityAreaByCityId",
                type: "get",
                datatype: "json",
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                data: { CityId: CityId },
                success: function (response) {
                    BindUpdateCityArea.empty().append('<option selected="selected" value="">Please select</option>');
                    $.each(response, function (i) {
                        BindUpdateCityArea.append($("<option></option>").val(this['id']).html(this['name']));
                    });
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
        else {
            $("#UEmpCityArea").empty().append('<option selected="selected" value="">Please select</option>');
        }
    });

    //Check Cnic On Focus Out
    $("#UEmpCnic").focusout(function () {
        var Cnic = $(this).val();
        if (Cnic != "") {
            $.ajax({
                url: "/AdminCommon/CheckCNICExits",
                type: "get",
                datatype: "json",
                data: { Cnic: Cnic, EmpId: $("#UEmpId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response == 0) {
                    }
                    else {
                        toastr.error(response + " " + "Number Already Exits");
                        $("#UEmpCnic").val("");
                        $("#UEmpCnic").focus();
                    }
                },
                error: function (response) {
                    toastr.error(response);
                }

            });
        }
    });
    //-------------------------------------------------------------Upload Image on Add and Update-------------------------------------------------------------
    $("#Aimagefiles").change(function () {
        var File = this.files;
        if (File && File[0]) {
            AReadImage(File[0]);
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

    //---------------------------------------------------------Update Managers Infos-------------------------------------------------------------------------------
    //Onclick Update Other Managers
    $("#AdminEmployees").on('click', '#UpdateOtherManagerModel', function () {
        var Id = $(this).val();
        var Updatevalidator = $("#AddUpdateOtherManagers_Submit_Form").validate();
        Updatevalidator.resetForm();
        ResetOtherManagerModel();
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminEmployee/UpdateOtherManager",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response != null) {
                        var Bindings = $("#AUOtherMgerMangerId");
                        Bindings.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
                        Bindings.empty().append('<option selected="selected" value="">Please select</option>');
                        $.each(response.managersList, function (i) {
                            Bindings.append($("<option></option>").val(this['id']).html(this['text']));
                        });
                        $("#AUOtherMgerEmpId").val(response.employeeId);
                        if (response.reportingTo != "" && response.reportingTo > 0) {
                            $("#AUOtherMgerMangerId").find('option[value="' + response.reportingTo + '"]').attr('selected', 'selected');
                        }

                        $("#AddUpdateOtherManagers").modal('show');
                    }
                    else {
                        toastr.error("Employee Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
    });

    //Add Update Other Manager Submit
    $("#AddUpdateOtherManagers_Submit_Form").validate({
        rules: {
            AUOtherMgerEmpId: {
                required: true,
                numberonly: true,
            },
            AUOtherMgerMangerId: {
                required: true,
                numberonly: true
            }
        },
        messages: {
            AUOtherMgerEmpId: {
                required: "Passcode Required."
            },
            AUOtherMgerMangerId: {
                required: "Please Select Manager."
            }
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
            $("#AUOtherMgerShowLoader").show();
            $("#AUOtherMgerShowButtons").hide();
            $.ajax({
                url: "/AdminEmployee/AddUpdateOtherManager",
                type: "post",
                datatype: "json",
                data: { EmployeeId: $("#AUOtherMgerEmpId").val(), ManagerId: $("#AUOtherMgerMangerId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response == 1) {
                        $("#AddUpdateOtherManagers").modal("hide");
                        $("#AUOtherMgerShowLoader").hide();
                        $("#AUOtherMgerShowButtons").show();
                        toastr.success("Other Info Updated Successfully", "Success");
                        $('#AdminEmployees').DataTable().ajax.reload();
                    }
                    else if (response == -2) {
                        $("#AUOtherMgerShowLoader").hide();
                        $("#AUOtherMgerShowButtons").show();
                        toastr.error("Must be Report to Upper Authority", "Error");
                    }
                    else {
                        $("#AUOtherMgerShowLoader").hide();
                        $("#AUOtherMgerShowButtons").show();
                        toastr.error("Other Info Not Updated Successfully", "Error");
                    }
                },
                error: function (response) {
                    $("#AUOtherMgerShowLoader").hide();
                    $("#AUOtherMgerShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    
    function ResetOtherManagerModel() {
        $("#AUOtherMgerEmpId").val('');
        $("#AUOtherMgerMangerId").val('');
    }
    //---------------------------------------------------------Regional Manager Infos------------------------------------------------------------------------------
    //Onclick Update Regional Managers
    $("#AdminEmployees").on('click', '#UpdateRegionalManagerModel', function () {
        var Id = $(this).val();
        var Updatevalidator = $("#AddUpdateRegionalManagers_Submit_Form").validate();
        Updatevalidator.resetForm();
        ResetRegionalManagerModel();
        $("#AURegionalMgerCityId").select2({
            placeholder: "Please Select",
        });
        $('#AURegionalMgerCityId').html('');
        $('#AURegionalMgerCityId').val('').trigger('change');
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminEmployee/UpdateRegionalManager",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response != null) {                      
                        var RegionalBindings = $("#AURegionalMgerMangerId");
                        RegionalBindings.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
                        RegionalBindings.empty().append('<option selected="selected" value="">Please select</option>');
                        $.each(response.managersList, function (i) {
                            RegionalBindings.append($("<option></option>").val(this['id']).html(this['text']));
                        });
                        var StateBindings = $("#AURegionalMgerStateId");
                        StateBindings.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
                        StateBindings.empty().append('<option selected="selected" value="">Please select</option>');
                        $.each(response.stateList, function (i) {
                            StateBindings.append($("<option></option>").val(this['id']).html(this['text']));
                        });
                        $("#AURegionalMgerEmpId").val(response.employeeId);
                        if (response.reportingTo != "" && response.reportingTo > 0) {
                            $("#AURegionalMgerMangerId").find('option[value="' + response.reportingTo + '"]').attr('selected', 'selected');
                        }
                        if (response.stateId != "" && response.stateId > 0) {
                            $("#AURegionalMgerStateId").find('option[value="' + response.stateId + '"]').attr('selected', 'selected');
                            $("#AURegionalMgerStateId").trigger("change");
                            if (response.assignedCityList.length > 0) {
                                $.each(response.assignedCityList, function (i) {
                                    var newOption = new Option(response.assignedCityList[i].text, response.assignedCityList[i].id, true, true);
                                    // Append it to the select
                                    $('#AURegionalMgerCityId').append(newOption).trigger('change');
                                });
                            }
                        }
                        $("#AddUpdateRegionalManagers").modal('show');
                    }
                    else {
                        toastr.error("Employee Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
    });

    $("#AURegionalMgerStateId").change(function () {
        $('#AURegionalMgerCityId').val('').trigger('change');
        if ($("#AURegionalMgerStateId").val() > 0) {

            $("#AURegionalMgerCityId").select2({
                placeholder: "Please Select",
                ajax: {
                    url: "/AdminEmployee/LoadCityList",
                    dataType: 'json',
                    delay: 250,
                    type: "get",
                    async: false,
                    headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                    data: function (params) {
                        return {
                            search: params.term, // search term
                            pageNo: params.page || 1,
                            pageSize: 10,
                            StateId: $("#AURegionalMgerStateId").val(),
                        };
                    },
                    processResults: function (data, params) {
                        params.page = params.page || 1;

                        return {
                            results: data.cityList,
                            pagination: {
                                more: (params.page * 10) < data.rowCount
                            }
                        };
                    },
                    cache: true
                },
                escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
                minimumInputLength: 1,
                multiple: true
            });

        }
    });

    $("#AddUpdateRegionalManagers_Submit_Form").validate({
        rules: {
            AURegionalMgerEmpId: {
                required: true,
                numberonly: true,
            },
            AURegionalMgerMangerId: {
                required: true,
                numberonly: true
            },
            AURegionalMgerStateId: {
                required: true,
                numberonly: true
            }
        },
        messages: {
            AURegionalMgerEmpId: {
                required: "Passcode Required.",
            },
            AURegionalMgerMangerId: {
                required: "Please Select Manager."
            },
            AURegionalMgerStateId : {
                required: "Please Select State."
            }
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
            $("#AURegionalMgerShowLoader").show();
            $("#AURegionalMgerShowButtons").hide();
            var ObjectRegional = new VMAddUpdateRegionalManager();
            var RegionalCities = RegionalManagerCitiesFill();
            ObjectRegional.EmployeeId = $("#AURegionalMgerEmpId").val();
            ObjectRegional.ManagerId = $("#AURegionalMgerMangerId").val();
            ObjectRegional.StateId = $("#AURegionalMgerStateId").val();
            ObjectRegional.AssignedCityList = RegionalCities.length > 0 ? RegionalCities : null;
            $.ajax({
                url: "/AdminEmployee/AddUpdateRegionalManager",
                type: "post",
                datatype: "json",
                data: { response: ObjectRegional },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response.results == 1) {
                        $("#AddUpdateRegionalManagers").modal("hide");
                        $("#AURegionalMgerShowLoader").hide();
                        $("#AURegionalMgerShowButtons").show();
                        toastr.success("Other Info Updated Successfully", "Success");
                        $('#AdminEmployees').DataTable().ajax.reload();
                    }
                    else if (response.results == -2) {
                        $("#AURegionalMgerShowLoader").hide();
                        $("#AURegionalMgerShowButtons").show();
                        toastr.error("Following cities already assigned: </br>" +" "+ response.cityExits, "Error");
                    }
                    else {
                        $("#AURegionalMgerShowLoader").hide();
                        $("#AURegionalMgerShowButtons").show();
                        toastr.error("Other Info Not Updated Successfully", "Error");
                    }
                },
                error: function (response) {
                    $("#AURegionalMgerShowLoader").hide();
                    $("#AURegionalMgerShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    function RegionalManagerCitiesFill() {
        var cities = [];
        $('#AURegionalMgerCityId :selected').each(function () {
            cities.push($(this).val());
        });
        return cities;
    }

    function ResetRegionalManagerModel() {
        $("#AURegionalMgerMangerId").val('');
        $("#AURegionalMgerStateId").val('');
        $("#AURegionalMgerCityId").val('');
    }

    //---------------------------------------------------------Category Manager Infos------------------------------------------------------------------------------
    $("#AUCategoryMgerCatId").select2({
        placeholder: "Please Select",
        ajax: {
            url: "/AdminEmployee/LoadCategoryList",
            dataType: 'json',
            delay: 250,
            type: "get",
            async: false,
            headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
            data: function (params) {
                return {
                    search: params.term, // search term
                    pageNo: params.page || 1,
                    pageSize: 10,
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;

                return {
                    results: data.mainMenuCategories,
                    pagination: {
                        more: (params.page * 10) < data.rowCount
                    }
                };
            },
            cache: true
        },
        escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
        minimumInputLength: 1,
        multiple: true
    });

    $("#AdminEmployees").on('click', '#UpdateCategoryManagerModel', function () {
        var Id = $(this).val();
        var Updatevalidator = $("#AddUpdateCategoryManagers_Submit_Form").validate();
        Updatevalidator.resetForm();
        $("#AUCategoryMgerEmpId").val('');
        $("#AUCategoryMgerMangerId").val('');
        $("#AUCategoryMgerCatId").html(''); 
        $('#AUCategoryMgerCatId').val('').trigger('change');
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminEmployee/UpdateCategoryManager",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response != null) {
                        var CateogryBindings = $("#AUCategoryMgerMangerId");
                        CateogryBindings.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
                        CateogryBindings.empty().append('<option selected="selected" value="">Please select</option>');
                        $.each(response.managersList, function (i) {
                            CateogryBindings.append($("<option></option>").val(this['id']).html(this['text']));
                        });
                        $("#AUCategoryMgerEmpId").val(response.employeeId);
                        if (response.reportingTo != "" && response.reportingTo > 0) {
                            $("#AUCategoryMgerMangerId").find('option[value="' + response.reportingTo + '"]').attr('selected', 'selected');
                        }
                        if (response.assignedCategoryList.length > 0) {
                            $.each(response.assignedCategoryList, function (i) {
                                var newOption = new Option(response.assignedCategoryList[i].text, response.assignedCategoryList[i].id, true, true);
                                // Append it to the select
                                $('#AUCategoryMgerCatId').append(newOption).trigger('change');
                            });
                        }
                        $("#AddUpdateCategoryManagers").modal('show');
                    }
                    else {
                        toastr.error("Employee Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
    });

    $("#AddUpdateCategoryManagers_Submit_Form").validate({
        rules: {
            AUCategoryMgerEmpId: {
                required: true,
                numberonly: true,
            },
            AUCategoryMgerMangerId: {
                required: true,
                numberonly: true
            },
        },
        messages: {
            AUCategoryMgerEmpId: {
                required: "Passcode Required.",
            },
            AUCategoryMgerMangerId: {
                required: "Please Select Manager."
            }
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
            $("#AUCategoryMgerShowLoader").show();
            $("#AUCategoryMgerShowButtons").hide();
            var ObjectCatgeory = new VMAddUpdateCategoryManager();
            var CategoryFills = CategoryManagerCategoryFill();
            ObjectCatgeory.EmployeeId = $("#AUCategoryMgerEmpId").val();
            ObjectCatgeory.ManagerId = $("#AUCategoryMgerMangerId").val();
            ObjectCatgeory.AssignedCategoryList = CategoryFills.length > 0 ? CategoryFills : null;
            $.ajax({
                url: "/AdminEmployee/AddUpdateCategoryManager",
                type: "post",
                datatype: "json",
                data: { response: ObjectCatgeory },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response.results == 1) {
                        $("#AddUpdateCategoryManagers").modal("hide");
                        $("#AUCategoryMgerShowLoader").hide();
                        $("#AUCategoryMgerShowButtons").show();
                        toastr.success("Other Info Updated Successfully", "Success");
                        $('#AdminEmployees').DataTable().ajax.reload();
                    }
                    else if (response.results == -2) {
                        $("#AUCategoryMgerShowLoader").hide();
                        $("#AUCategoryMgerShowButtons").show();
                        toastr.error("Following categories already assigned: </br>" + " " + response.categoryExits, "Error");
                    }
                    else {
                        $("#AUCategoryMgerShowLoader").hide();
                        $("#AUCategoryMgerShowButtons").show();
                        toastr.error("Other Info Not Updated Successfully", "Error");
                    }
                },
                error: function (response) {
                    $("#AUCategoryMgerShowLoader").hide();
                    $("#AUCategoryMgerShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });
    function CategoryManagerCategoryFill() {
        var category = [];
        $('#AUCategoryMgerCatId :selected').each(function () {
            category.push($(this).val());
        });
        return category;
    }

    //---------------------------------------------------------Zone Manager Infos------------------------------------------------------------------------------
    $("#AdminEmployees").on('click', '#UpdateZoneManagerModel', function () {
        var Id = $(this).val();
        var Updatevalidator = $("#AddUpdateZoneManagers_Submit_Form").validate();
        Updatevalidator.resetForm();
        ResetZoneManagerModel();
        $("#AUZoneMgerZoneId").select2({ placeholder:"Please Select" });
        $("#AUZoneMgerZoneId").html(''); 
        $('#AUZoneMgerZoneId').val('').trigger('change');
        if (Id != "" && Id != null) {
            $.ajax({
                url: "/AdminEmployee/UpdateZoneManager",
                type: "get",
                datatype: "json",
                data: { Id: Id },
                async: false,
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response != null) {
                        var ReportingBindings = $("#AUZoneMgerMangerId");
                        ReportingBindings.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
                        ReportingBindings.empty().append('<option selected="selected" value="">Please select</option>');
                        $.each(response.managersList, function (i) {
                            ReportingBindings.append($("<option></option>").val(this['id']).html(this['text']));
                        });
                        var CityBindings = $("#AUZoneMgerCityId");
                        CityBindings.empty().append('<option selected="selected" value="" disabled = "disabled">Loading.....</option>');
                        CityBindings.empty().append('<option selected="selected" value="">Please select</option>');
                        $.each(response.cityList, function (i) {
                            CityBindings.append($("<option></option>").val(this['id']).html(this['text']));
                        });
                        $("#AUZoneMgerEmpId").val(response.employeeId);
                        if (response.reportingTo != "" && response.reportingTo > 0) {
                            $("#AUZoneMgerMangerId").find('option[value="' + response.reportingTo + '"]').attr('selected', 'selected');
                        }
                        if (response.cityId != "" && response.cityId > 0) {
                            $("#AUZoneMgerCityId").find('option[value="' + response.cityId + '"]').attr('selected', 'selected');
                            $("#AUZoneMgerCityId").trigger("change");
                            if (response.assignedZoneList.length > 0) {
                                $.each(response.assignedZoneList, function (i) {
                                    var newOption = new Option(response.assignedZoneList[i].text, response.assignedZoneList[i].id, true, true);
                                    // Append it to the select
                                    $('#AUZoneMgerZoneId').append(newOption).trigger('change');
                                });
                            }
                        }
                        $("#AddUpdateZoneManagers").modal('show');
                    }
                    else {
                        toastr.error("Employee Not Exits", "Error");
                    }
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
    });
    
    $("#AUZoneMgerMangerId").change(function () {
        $('#AUZoneMgerZoneId').val('').trigger('change');
        $('#AUZoneMgerCityId').empty().append('<option selected="selected" value="">Please select</option>');
        if ($("#AUZoneMgerMangerId").val() > 0) {
            $.ajax({
                url: "/AdminEmployee/GetAssignedCities",
                type: "get",
                datatype: "json",
                async: false,
                data: { ManagerId: $("#AUZoneMgerMangerId").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    $('#AUZoneMgerCityId').empty().append('<option selected="selected" value="">Please select</option>');
                    $.each(response, function (i) {
                        $('#AUZoneMgerCityId').append($("<option></option>").val(this['id']).html(this['text']));
                    });
                },
                error: function (response) {
                    toastr.error(response, "Error");
                }

            });
        }
    });
    $("#AUZoneMgerCityId").change(function () {
        $('#AUZoneMgerZoneId').val('').trigger('change');
        if ($("#AUZoneMgerCityId").val() > 0) {
            $("#AUZoneMgerZoneId").select2({
                placeholder: "Please Select",
                allowClear: true,
                ajax: {
                    url: "/AdminEmployee/LoadZoneList",
                    dataType: 'json',
                    delay: 250,
                    type: "get",
                    async: false,
                    headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                    data: function (params) {
                        return {
                            search: params.term, // search term
                            pageNo: params.page || 1,
                            pageSize: 10,
                            CityId: $("#AUZoneMgerCityId").val(),
                        };
                    },
                    processResults: function (data, params) {
                        params.page = params.page || 1;

                        return {
                            results: data.zonesList,
                            pagination: {
                                more: (params.page * 10) < data.rowCount
                            }
                        };
                    },
                    cache: true
                },
                escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
                minimumInputLength: 1,
                multiple: true
            });
        }
    });

    $("#AddUpdateZoneManagers_Submit_Form").validate({
        rules: {
            AUZoneMgerEmpId: {
                required: true,
                numberonly: true,
            },
            AUZoneMgerMangerId: {
                required: true,
                numberonly: true
            },
            AUZoneMgerCityId: {
                required: true,
                numberonly: true
            }
        },
        messages: {
            AUZoneMgerEmpId: {
                required: "Passcode Required.",
            },
            AUZoneMgerMangerId: {
                required: "Please Select Manager."
            },
            AUZoneMgerCityId: {
                required: "Please Select City."
            }
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
            $("#AUZoneMgerShowLoader").show();
            $("#AUZoneMgerShowButtons").hide();
            var ObjectZone = new VMAddUpdateZoneManager();
            var ZoneFills = ZoneManagerZoneFill();
            ObjectZone.EmployeeId = $("#AUZoneMgerEmpId").val();
            ObjectZone.ManagerId = $("#AUZoneMgerMangerId").val();
            ObjectZone.CityId = $("#AUZoneMgerCityId").val();
            ObjectZone.AssignedZoneList = ZoneFills.length > 0 ? ZoneFills : null;
            $.ajax({
                url: "/AdminEmployee/AddUpdateZoneManager",
                type: "post",
                datatype: "json",
                data: { response: ObjectZone },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response.results == 1) {
                        $("#AddUpdateZoneManagers").modal("hide");
                        $("#AUZoneMgerShowLoader").hide();
                        $("#AUZoneMgerShowButtons").show();
                        toastr.success("Other Info Updated Successfully", "Success");
                        $('#AdminEmployees').DataTable().ajax.reload();
                    }
                    else if (response.results == -2) {
                        $("#AUZoneMgerShowLoader").hide();
                        $("#AUZoneMgerShowButtons").show();
                        toastr.error("Following zones already assigned: </br>" + " " + response.zonesExits, "Error");
                    }
                    else {
                        $("#AUZoneMgerShowLoader").hide();
                        $("#AUZoneMgerShowButtons").show();
                        toastr.error("Other Info Not Updated Successfully", "Error");
                    }
                },
                error: function (response) {
                    $("#AUZoneMgerShowLoader").hide();
                    $("#AUZoneMgerShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

    function ZoneManagerZoneFill() {
        var zone = [];
        $('#AUZoneMgerZoneId :selected').each(function () {
            zone.push($(this).val());
        });
        return zone;
    }

    function ResetZoneManagerModel() {
        $("#AUZoneMgerEmpId").val('');
        $("#AUZoneMgerMangerId").val('');
        $("#AUZoneMgerCityId").val('');
    }
    //-----------------------------------------------------------------Reset---------------------------------------------------------------------------------------
    function ResetEmployeeIds() {
        $("#AEmpFirstName").val('');
        $("#AEmpLastName").val('');
        $("#AEmpDob").val('');
        $("#AEmpCnic").val('');
        $("#AEmpPassport").val('');
        $("#AEmpContactNumber").val('');
        $("#AEmpPhoneNumber").val('');
        $("#AEmpCountry").val('');
        $("#AEmpStatus").val('');
        $("#AEmpCity").val('');
        $("#AEmpCityArea").val('');
        $("#AEmpAddress").val('');
        $("#AEmpRole").val('');
        $("#AEmpDesignation").val('');
        $("#AEmpEmail").val('');
        $("#AEmpPassword").val('');
        $("#Aimagefiles").val('');
        $('#AtargetImg').attr("src", "");

        $("#UEmpId").val('')
        $("#UUserId").val('');
        $("#UEmpFirstName").val('');
        $("#UEmpLastName").val('');
        $("#UEmpDob").val('');
        $("#UEmpCnic").val('');
        $("#UEmpPassport").val('');
        $("#UEmpContactNumber").val('');
        $("#UEmpPhoneNumber").val('');
        $("#UEmpCountry").val('');
        $("#UEmpStatus").val('');
        $("#UEmpCity").val('');
        $("#UEmpCityArea").val('');
        $("#UEmpAddress").val('');
        $("#UEmpRole").val('');
        $("#UEmpDesignation").val('');
        $("#Uimagefiles").val('');
        $('#UtargetImg').attr("src", "");


    }
    //--------------------------------------------------------------Redirect To Employee Permission--------------------------------------------------

    //Onclick Redirect to User Permission Controller
    $("#AdminEmployees").on('click', '#UpdateEmployeePermission', function () {
        var Id = $(this).val();
        var url = "/AdminUserPermission/ManagePermission?UserRoleId=" + Id;
        window.location.href = url;
    });

    //--------------------------------------------------------------Changes Password Employee--------------------------------------------------

    //Onclick Change Password Form Open
    $("#AdminEmployees").on('click', '#UpdateEmployeePasswordModal', function () {
        $("#UCEmpUserId").val('');
        $("#UCEmpPassword").val('');
        $("#UCEmpUserId").val($(this).val());
        $("#UpdateEmpChangePassword").modal('show');
    });

    //Update Change Password
    $("#UpdateEmpPass_Submit_Form").validate({
        rules: {
            UCEmpUserId: {
                required: true,
            },
            UCEmpPassword: {
                required: true,
                passwordRegex: true
            },
        },
        messages: {
            UCEmpUserId: {
                required: "Employee Passcode Required."
            },
            UCEmpPassword: {
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
            $("#UpdateEmpPassShowLoader").show();
            $("#UpdateEmpPassShowButtons").hide();
            $.ajax({
                url: "/AdminEmployee/ChangesPassword",
                type: "post",
                datatype: "json",
                data: { UserId: $("#UCEmpUserId").val(), newPassword: $("#UCEmpPassword").val() },
                headers: { RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() },
                success: function (response) {
                    if (response.error == "403") {
                        location.href = "/Account/AccessDenied";
                    }
                    else if (response > 0) {
                        $("#UpdateEmpChangePassword").modal("hide");
                        $("#UpdateEmpPassShowLoader").hide();
                        $("#UpdateEmpPassShowButtons").show();
                        toastr.success("Password Changes Successfully", "Success");
                        $('#AdminEmployees').DataTable().ajax.reload();
                    }
                    else {
                        $("#UpdateEmpPassShowLoader").hide();
                        $("#UpdateEmpPassShowButtons").show();
                        toastr.error("Password Not Changed", "Error");
                    }
                },
                error: function (response) {
                    $("#UpdateEmpPassShowLoader").hide();
                    $("#UpdateEmpPassShowButtons").show();
                    toastr.error(response, "Error");
                }

            });
        }
    });

});
