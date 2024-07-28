var fncAlertForStatus403;

$(document).ready(function () {
    /*
    .ajaxSuccess( handler )handler
    Type: Function( Event event, jqXHR jqXHR, PlainObject ajaxOptions, PlainObject data
    */
    $(document).ajaxSuccess(function (event, jqXHR, ajaxOptions, data) {
        //alert("$(document).ajaxSuccess !!!");
        //alert(request.getResponseHeader('some_header'));
        // did not authorized to perform task
        if (403 == jqXHR.status) {
            if (typeof fncAlertForStatus403 == "function") {
                fncAlertForStatus403();

                //event.preventDefault();
                //event.stopPropagation();
            }
        }
    });

    $(document).ajaxComplete(function (event, jqXHR, ajaxOptions) {
        // did not authenticate
        if (401 == jqXHR.status) {
            //window.location = '/Login/TimeOutNotify';

            // did not authorized to perform task
        } else if (403 == jqXHR.status) {
            if (typeof fncAlertForStatus403 == "function") {
                fncAlertForStatus403();
            }
        }
    });

    $(document).ajaxError(function (event, jqXHR, settings, thrownError) {
        hideOverLoad();
        // show error info
        // alert(jqXHR.responseText);
    });

    //Ajax start execute
    $(document).ajaxStart(function () {
        // show overlay icon
        ShowOverLoad("body");
    });

    //Ajax end execute
    $(document).ajaxStop(function () {
        hideOverLoad();
    });
});

/*===========================Show Loading============================*/
//selector = selector used to get element is overlayed
function ShowOverLoad(selector) {
    if ($("#ajaxLoad").length < 1) {
        $(selector).append($('<div id="ajaxLoad">' +
            '<img src="/images/system/processing.GIF" class="ajax-loader"/>' +
            '</div>'));
    }
    else {
        $("#ajaxLoad").css("display", "block");
    }
}
function hideOverLoad() {
    $("#ajaxLoad").css("display", "none");
}

// define function fncAlertForStatus403 (fncAlertForStatus403  la bien duoc khai bao o tren)
fncAlertForStatus403 = function () {
    // display message
    toastr.warning("Bạn không có quyền hạn để thực hiện tác vụ này, vui lòng kiểm tra lại!");
    // set timeout to reload page
    setTimeout(function () {
        window.location.reload(true);
    }, 1000);
}
/*================================End================================*/

/*===========================Validate Form============================*/
// validate form
function validForm(formId) {
    return $('#' + formId).valid();
}

// Show message via pop-up
function validFormShowPopUp(formId) {
    $("#" + formId).validate({
        showErrors: function (errorMap, errorList) {

            // Clean up any tooltips for valid elements
            $.each(this.validElements(), function (index, element) {
                var $element = $(element);
                $element.data("title", "") // Clear the title - there is no error associated anymore
                //.removeClass("error")
                $element.tooltip("destroy");
            });

            // Create new tooltips for invalid elements
            $.each(errorList, function (index, error) {
                var $element = $(error.element);
                //$element.tooltip("destroy") // Destroy any pre-existing tooltip so we can repopulate with new tooltip content
                $element.data("title", error.message)
                    //.addClass("error")
                    .tooltip('show'); // Create a new tooltip based on the error messsage we just set in the title
            });
        }
    });

    return $('#' + formId).valid();
}

function triggerValidateEventForm(formId) {
    $("#" + formId).removeData("validator");
    $("#" + formId).removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse("#" + formId);
}

/*================================End================================*/
