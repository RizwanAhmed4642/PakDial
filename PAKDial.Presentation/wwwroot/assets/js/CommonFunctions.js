$(function () {
    //Validation Start
    jQuery.validator.addMethod("letteronly", function (value, element) {
        return this.optional(element) || /^[A-Za-z ]+$/i.test(value);
    }, "Enter only alphabetical characters");
    jQuery.validator.addMethod("numberonly", function (value, element) {
        return this.optional(element) || /^[0-9]+$/i.test(value);
    }, "Number Only");
    jQuery.validator.addMethod("numberonlyCnic", function (value, element) {
        return this.optional(element) || /^[0-9-]+$/i.test(value);
    }, "Number Only");
    jQuery.validator.addMethod("letternumberonly", function (value, element) {
        return this.optional(element) || /^[a-zA-Z0-9 ]+$/i.test(value);
    }, "Number and Alphabets Only");
    jQuery.validator.addMethod("emailRegex", function (value, element) {
        return this.optional(element) || /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/i.test(value);
    }, "Please Provide Correct Email");
     // should contain at least one digit
     // should contain at least one lower case
     // should contain at least one upper case
     // should contain at least 8 from the mentioned characters
    jQuery.validator.addMethod("passwordRegex", function (value, element) {
        return this.optional(element) || /([^a-zA-Z\d])+([a-zA-Z\d])+|([a-zA-Z\d])+([^a-zA-Z\d])+/i.test(value);
    }, "Password Must Contain AlphaNumeric With Special Character Like Etc_121");

    jQuery.validator.addMethod("StartDateFromCurrentDate", function (value, element) {
        var q = new Date();
        var MyDate = new Date(Date.parse(value));
        return this.optional(element) || MyDate > q ||
            (q.getMonth() + 1 == MyDate.getMonth() + 1 && q.getDate() == MyDate.getDate() && q.getFullYear() == MyDate.getFullYear());
    }, "Start Date Not Be Previous Date");

    jQuery.validator.addMethod("EndDateFromTodayDate", function (value, element) {
        return this.optional(element) || new Date(Date.parse(value)) > new Date();
    }, "End Date Lesser Then Today");

    jQuery.validator.addMethod("greaterThan",
        function (value, element, params) {
           
            var target = $(params).val();
            if (!/Invalid|NaN/.test(new Date(value))) {
                return new Date(value) > new Date(target);
            }

            return isNaN(value) && isNaN(target)
                || (Number(value) > Number(target));
        }, 'Must be greater than StartDate.');

    //For Focus In 
    $('input').focusin(
        function () {
            $(this).css({ 'border': '1px solid ', 'border-color': '#3b4781', 'background-color': '#cacfe7' });
        });

    $('select').focusin(
        function () {
            $(this).css({ 'border': '1px solid ', 'border-color': '#3b4781', 'background-color': '#cacfe7' });
        });

    $('textarea').focusin(
        function () {
            $(this).css({ 'border': '1px solid ', 'border-color': '#3b4781', 'background-color': '#cacfe7' });
        });

    //For Focus Out
    $('input').focusout(
        function () {
            $(this).css({ 'border-color': '#c2cad8', 'background-color': 'white' });
        });

    $('select').focusout(
        function () {
            $(this).css({ 'border-color': '#c2cad8', 'background-color': 'white' });
        });

    $('textarea').focusout(
        function () {
            $(this).css({ 'border-color': '#c2cad8', 'background-color': 'white' });
        });
});
function dateFormat(date) {
    var day = ("0" + date.getDate()).slice(-2);
    var month = ("0" + (date.getMonth() + 1)).slice(-2);
    var today = date.getFullYear() + "-" + (month) + "-" + (day);
    return today;
}
function getSelectedImages(element) {
    var file = element.get(0).files;
    var data = new FormData();
    for (var i = 0; i < file.length; i++) {
        data.append(file[i].name, file[i]);
    }
    return data;
}

var PaymentsMode = {
    Cash: 'Cash',
    PayOrder: 'PayOrder',
    OnlinePayment: 'OnlinePayment',
    Cheque: 'Cheque',
};