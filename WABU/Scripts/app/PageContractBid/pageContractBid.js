"use strict";

var pageContractBid = function () {

    //[START]-----------------Init pageContracBid Function-----------------

    function initCompanyAbilityExpList() {
        changePageOnListCompanyAbilityExp(1, event);
    }

    function initCompanyAbilitHrList() {
        changePageOnListCompanyAbilityHr(1, event);
    }

    function initCompanyAbilityEquipmentList() {
        changePageOnListCompanyAbilityEquipment(1, event);
    }

    function initCompanyAbilityFinanceList() {
        changePageOnListCompanyAbilityFinance(1, event);
    }

    //[END]-----------------Init pageContracBid Function-----------------

    //[START]-----------------Clear Form Function-----------------

    function clearAddCompanyAbilityExpForm() {

        $('#frmAddCompanyAbilityExp').find("input[type=text], input[type=number], input[type=file], textarea").val('');
        $('.fileinput-filename').html('');
        var spanError = $('#frmAddCompanyAbilityExp').find(".help-block.m-b-none.text-danger");
        for (var i = 0; i < spanError.length; i++) {
            $(spanError).find("span").html("");
        }
    }

    function clearAddCompanyAbilityHrForm() {

        $('#frmAddCompanyAbilityHr').find("input[type=text], input[type=number], input[type=file], textarea").val('');
        $('.fileinput-filename').html('');
        var spanError = $('#frmAddCompanyAbilityHr').find(".help-block.m-b-none.text-danger");
        for (var i = 0; i < spanError.length; i++) {
            $(spanError).find("span").html("");
        }

        $('.additionalRow').remove();
    }

    function clearAddCompanyAbilityEquipmentForm() {

        $('#frmAddCompanyAbilityEquipment').find("input[type=text], input[type=number], input[type=file], textarea").val('');
        $('.fileinput-filename').html('');
        var spanError = $('#frmAddCompanyAbilityEquipment').find(".help-block.m-b-none.text-danger");
        for (var i = 0; i < spanError.length; i++) {
            $(spanError).find("span").html("");
        }
    }

    function clearAddCompanyAbilityFinanceForm() {

        $('#frmAddCompanyAbilityFinance').find("input[type=text], input[type=number], input[type=file], textarea").val('');
        $('.fileinput-filename').html('');
        var spanError = $('#frmAddCompanyAbilityFinance').find(".help-block.m-b-none.text-danger");
        for (var i = 0; i < spanError.length; i++) {
            $(spanError).find("span").html("");
        }
    }

    //[END]-----------------Clear Form Function-----------------

    //[START]-----------------Check box Xuat Ho So PageContracBid Function-----------------

    var initListTempXhs = () => {
        window.List_Temp_XHS_NLKN = [];
        window.List_Temp_XHS_NLNS = [];
        window.List_Temp_XHS_NLTB = [];
        window.List_Temp_XHS_NLTC = [];
    }

    function checkBoxInit(list_XHS_NLKN, list_XHS_NLNS, list_XHS_NLTB, list_XHS_NLTC) {
        window.List_XHS_NLKN = list_XHS_NLKN;
        window.List_XHS_NLNS = list_XHS_NLNS;
        window.List_XHS_NLTB = list_XHS_NLTB;
        window.List_XHS_NLTC = list_XHS_NLTC;

        initListTempXhs();

        var allClass_XHS = ["XHS_NLKN", "XHS_NLNS", "XHS_NLTB", "XHS_NLTC"];
        var allClass_XHS_len = allClass_XHS.length;
        for (var i = 0; i < allClass_XHS_len; i++) {
            $("." + allClass_XHS[i]).each(function (index, obj) {
                var re = $(obj).closest(".icheckbox_square-green");
                re.removeClass("checked");
            });
        }
    }

    //store check box value to window variable
    function storeCheckBoxVal(className) {
        var list_XHS = "List_" + className;
        var list_Temp_XHS = "List_Temp_" + className;

        // Check if value has already been added to the array and if not - add it
        function itemExistsChecker(cboxValue, cboxArray) {

            var len = cboxArray.length;

            if (len > 0) {
                for (var i = 0; i < len; i++) {
                    if (cboxArray[i] == cboxValue) {
                        return true;
                    }
                }
            }

            cboxArray.push(cboxValue);
        }

        $("." + className).each(function () {

            var cboxValue = $(this).val();
            // On checkbox change add/remove the vehicle value from the array based on the choice
            $(this).on("ifChanged", function () {
                if ($(this).hasClass("Modal_" + className)) {
                    if ($(this).is(':checked')) {
                        itemExistsChecker(cboxValue, window[list_Temp_XHS]);
                    } else {

                        // Delete the vehicle value from the array if its checkbox is unchecked
                        var cboxValueIndexListTemp = window[list_Temp_XHS].indexOf(cboxValue);

                        if (cboxValueIndexListTemp >= 0) {
                            window[list_Temp_XHS].splice(cboxValueIndexListTemp, 1);
                        }
                    }
                } else {

                    if ($(this).is(':checked')) {
                        itemExistsChecker(cboxValue, window[list_XHS]);
                    } else {

                        //Delete the vehicle value from the array if its checkbox is unchecked
                        var cboxValueIndex = window[list_XHS].indexOf(cboxValue);

                        if (cboxValueIndex >= 0) {
                            window[list_XHS].splice(cboxValueIndex, 1);
                        }
                    }
                }
            });

        });

    }

    //Convert items in array from int to string 
    function convertArrayItems(arr) {
        var len = arr.length;
        var result = new Array();
        if (len > 0) {
            for (var i = 0; i < len; i++) {
                result.push(arr[i].toString());
            }
        }

        return result;
    }

    //Convert array to string format to post to server
    function handelArrFormat(arr) {
        var len = arr.length;
        var arrVal = "";
        if (len > 0) {
            for (var i = 0; i < len; i++) {
                arrVal += arr[i] + ",";
            }
        }
        if (arrVal.substr(arrVal.length - 1) == ",") {
            arrVal = arrVal.substr(0, arrVal.length - 1);
        }
        return arrVal;
    }

    // Post value to server export profile
    function exportProfile(event) {
        event.preventDefault();

        var data = {
            Id: $("input[name='ProfileId']").val(),
            NameProfile: $("input[name='NameProfile']").val(),
            AbilityEquipmentsId: handelArrFormat(window.List_XHS_NLTB),
            AbilityHRsId: handelArrFormat(window.List_XHS_NLNS),
            AbilityExpsId: handelArrFormat(window.List_XHS_NLKN),
            AbilityFinancesId: handelArrFormat(window.List_XHS_NLTC)
        };
        if (validForm("frmExportProfile")) {
            $.ajax({
                type: "POST",
                url: "/PageContractBid/Export_Profile",
                data: data,
                success: function (resultData) {
                    if (resultData && resultData.Data.code === "0") {
                        toastr.success(resultData.Data.message);
                        initCompanyAbilityFinanceList();
                        $("#btn_ctr_NLTC_out").trigger("click");
                    }
                    else {
                        toastr.error(resultData.Data.message);
                    }
                }
            });
        }
    }

    //Update check box is checked when paging modal
    function updateCheckBoxStatus(className) {
        var list_XHS = className.indexOf("Modal") > -1 ? "List_Temp" + className.substring(5, className.length) : "List_" + className;
        var valueIsChecked = window[list_XHS];
        if (typeof valueIsChecked !== "undefined") {
            var valueIsCheckedLen = valueIsChecked.length;
            //Case uncheck all checkbox
            if (valueIsCheckedLen == 0) {
                $(".i-checks." + className).each(function (index, obj) {
                    $(obj).prop('checked', false);
                    var re = $(obj).closest(".icheckbox_square-green");
                    re.removeClass("checked");
                });
            }
            //Case update uncheck checkbox in list outside modal
            $(".i-checks." + className).each(function (index, obj) {
                var isUnchecked = true;
                for (var i = 0; i < valueIsCheckedLen; i++) {
                    if ($(obj).val() == valueIsChecked[i]) {
                        isUnchecked = false;
                        break;
                    }
                }
                if (isUnchecked) {
                    $(obj).prop('checked', false);
                    var re = $(obj).closest(".icheckbox_square-green");
                    re.removeClass("checked");
                }
            });
            //Case update checked checkbox
            for (var i = 0; i < valueIsCheckedLen; i++) {
                $(".i-checks." + className).each(function (index, obj) {
                    if ($(obj).val() == valueIsChecked[i]) {
                        $(obj).prop('checked', true);
                        var re = $(obj).closest(".icheckbox_square-green");
                        re.addClass("checked");
                    }
                });
            }
        }
    }

    //Update check box status when open modal
    function updateCheckBoxStatus_WhenOpenModal(id) {
        $('#' + id + "_MODAL_Btn").on('click',
            function () {
                initListTempXhs();
                var className = "XHS_" + id;
                var list_XHS = "List_" + className;
                var list_Temp_XHS = "List_Temp_" + className;
                if (window[list_XHS].length > 0) {
                    $.each(window[list_XHS], function (index, val) {
                        window[list_Temp_XHS].push(val);
                    });
                }
                updateCheckBoxStatus(className);
            });
    }

    //Click Choose button on modal
    function chooseButtonEventModal(e, className) {
        e.preventDefault();
        var list_XHS = "List_" + className;
        var list_Temp_XHS = "List_Temp_" + className;
        window[list_XHS] = [];
        if (window[list_Temp_XHS].length > 0) {
            $.each(window[list_Temp_XHS], function (index, val) {
                window[list_XHS].push(val);
            });
        }
        updateCheckBoxStatus(className);
        initListTempXhs();
    }

    //Click exit button on modal
    function exitButtonEventModal(e) {
        e.preventDefault();
        initListTempXhs();
    }

    //[END]-----------------Check box Xuat Ho So PageContracBid Function-----------------

    //[START]-----------------Create Company Ability Function-----------------

    //createCompanyAbilityExp
    function createCompanyAbilityExp(event, formId) {
        event.preventDefault();

        var dataForm = new FormData($("form#" + formId)[0]);
        $.ajax({
            type: "POST",
            url: "/PageContractBid/AddNewCompanyAbilityExp",
            data: dataForm,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (resultData) {
                if (resultData && resultData.Data.code === "0") {
                    toastr.success(resultData.Data.message);
                    initCompanyAbilityExpList();
                    $(".btn_ctr_NLKN_out").trigger("click");
                    closeAllPopUpEdit();
                }
                else if (resultData.Data.code === "2") {
                    toastr.warning(resultData.Data.message);
                    setInterval(function () {
                        let url = '/taikhoan/capnhat';
                        window.location.href = url;
                    }, 3000);
                }
                else {
                    toastr.error(resultData.Data.message);
                }
            }
        });
    }

    //createCompanyAbilityFinance
    function createCompanyAbilityFinance(event, formId) {
        event.preventDefault();
        $("#CompanyAbilityFinanceId").val("");
        var dataForm = new FormData($("#" + formId)[0]);

        $.ajax({
            type: "POST",
            url: "/PageContractBid/AddNewCompanyAbilityFinance",
            data: dataForm,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (resultData) {
                if (resultData && resultData.Data.code === "0") {
                    toastr.success(resultData.Data.message);
                    initCompanyAbilityFinanceList();
                    $(".btn_ctr_NLTC_out").trigger("click");
                }
                else if (resultData.Data.code === "2") {
                    toastr.warning(resultData.Data.message);
                    setInterval(function () {
                        let url = '/taikhoan/capnhat';
                        window.location.href = url;
                    }, 3000);
                }
                else if (resultData && resultData.Data.code === "3") {
                    showConfirmUpdateBox(event, formId);
                    $("#CompanyAbilityFinanceId").val(resultData.Data.Id);
                }
                else {
                    toastr.error(resultData.Data.message);
                }
            }
        });
    }

    //createCompanyAbilityHr
    function createCompanyAbilityHr(event, formId) {
        event.preventDefault();
        var dataForm = new FormData($("#" + formId)[0]);
        var companyAbilityHrDetailLen = dataForm.getAll("ProjectSimilar").length;
        for (var i = 0; i < companyAbilityHrDetailLen; i++) {
            var projectSimilarVal = dataForm.getAll("ProjectSimilar")[i];
            if (projectSimilarVal == "")
                continue;
            let fromYear = dataForm.getAll("FromYear")[i];
            if (typeof dataForm.getAll("FromYear")[i] === "undefined") {
                fromYear = null;
            }
            let toYear = dataForm.getAll("ToYear")[i];
            if (typeof dataForm.getAll("ToYear")[i] === "undefined") {
                toYear = null;
            }
            dataForm.append("CompanyAbilityHRDetails[" + i + "].Id", dataForm.getAll("HRDetail_Id")[i]);
            dataForm.append("CompanyAbilityHRDetails[" + i + "].ProjectSimilar", dataForm.getAll("ProjectSimilar")[i]);
            dataForm.append("CompanyAbilityHRDetails[" + i + "].FromYearString", fromYear);
            dataForm.append("CompanyAbilityHRDetails[" + i + "].ToYearString", toYear);
            dataForm.append("CompanyAbilityHRDetails[" + i + "].PositionSimilar", dataForm.getAll("PositionSimilar")[i]);
        }

        $.ajax({
            type: "POST",
            url: "/PageContractBid/AddNewCompanyAbilityHr",
            data: dataForm,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (resultData) {
                if (resultData && resultData.Data.code === "0") {
                    toastr.success(resultData.Data.message);
                    initCompanyAbilitHrList();
                    $(".btn_ctr_NLNS_out").trigger("click");
                    closeAllPopUpEdit();
                }
                else if (resultData.Data.code === "2") {
                    toastr.warning(resultData.Data.message);
                    setInterval(function () {
                        let url = '/taikhoan/capnhat';
                        window.location.href = url;
                    }, 3000);
                }
                else {
                    toastr.error(resultData.Data.message);
                }
            }
        });
    }

    //createCompanyAbilityEquipment
    function createCompanyAbilityEquipment(event, formId) {
        event.preventDefault();

        var dataForm = new FormData($("#" + formId)[0]);

        $.ajax({
            type: "POST",
            url: "/PageContractBid/AddNewCompanyAbilityEquipment",
            data: dataForm,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (resultData) {
                if (resultData && resultData.Data.code === "0") {
                    toastr.success(resultData.Data.message);
                    initCompanyAbilityEquipmentList();
                    $(".btn_ctr_NLTB_out").trigger("click");
                    closeAllPopUpEdit();
                }
                else if (resultData.Data.code === "2") {
                    toastr.warning(resultData.Data.message);
                    setInterval(function () {
                        let url = '/taikhoan/capnhat';
                        window.location.href = url;
                    }, 3000);
                }
                else {
                    toastr.error(resultData.Data.message);
                }
            }
        });
    }

    //[END]-----------------Create Company Ability Function-----------------

    //updateCompanyAbilityFinance
    function updateCompanyAbilityFinance(event, formId) {
        event.preventDefault();

        var dataForm = new FormData($("#" + formId)[0]);

        $.ajax({
            type: "POST",
            url: "/PageContractBid/UpdateCompanyAbilityFinance",
            data: dataForm,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (resultData) {
                if (resultData && resultData.Data.code === "0") {
                    toastr.success(resultData.Data.message);
                    initCompanyAbilityFinanceList();
                    $(".btn_ctr_NLTC_out").trigger("click");
                }
                else {
                    toastr.error(resultData.Data.message);
                }
            }
        });
    }

    //showConfirmUpdateBox
    function showConfirmUpdateBox(event, formId, action) {
        event.preventDefault();
        $('#confirmUpdateBox button[data-bb-handler="confirm"][type="button"]').unbind("click");

        if (action == "edit") {
            triggerValidateEventForm(formId);
        }
        if (validForm(formId)) {
            $("#openConfirmUpdateBox").click();
            $('#confirmUpdateBox button[data-bb-handler="confirm"][type="button"]').click(function (event) {
                updateCompanyAbilityFinance(event, formId);
            });
        }
    }

    //changePageOnListCompanyAbilityExp
    function changePageOnListCompanyAbilityExp(pageSelected, e) {
        e.preventDefault();

        var data = {
            page: pageSelected
        };

        $.ajax({
            type: "POST",
            url: "/PageContractBid/ReadPagingCompanyAbilityExp",
            data: data,
            success: function (responsed) {
                $("#tblNLKN").html(responsed.patialView);
                $("#NLKN_MODAL").html(responsed.partialModalView);
                if (window.IsEditPage == true) {
                    $('.btn-Modal-Delete').hide();
                }
            }
        });
    }

    //changePageOnListCompanyAbilityEquipment
    function changePageOnListCompanyAbilityEquipment(pageSelected, e) {
        e.preventDefault();
        var data = {
            page: pageSelected
        };

        $.ajax({
            type: "POST",
            url: "/PageContractBid/ReadPagingCompanyAbilityEquipment",
            data: data,
            success: function (responsed) {
                $("#tblNLTB").html(responsed.patialView);
                $("#NLTB_MODAL").html(responsed.partialModalView);
                if (window.IsEditPage == true) {
                    $('.btn-Modal-Delete').hide();
                }
            }
        });
    }

    //changePageOnListCompanyAbilityHr
    function changePageOnListCompanyAbilityHr(pageSelected, e) {
        e.preventDefault();
        var data = {
            page: pageSelected
        };

        $.ajax({
            type: "POST",
            url: "/PageContractBid/ReadPagingCompanyAbilityHr",
            data: data,
            success: function (responsed) {
                $("#tblNLNS").html(responsed.patialView);
                $("#NLNS_MODAL").html(responsed.partialModalView);
                if (window.IsEditPage == true) {
                    $('.btn-Modal-Delete').hide();
                }
            }
        });
    }

    //changePageOnListCompanyAbilityFinance
    function changePageOnListCompanyAbilityFinance(pageSelected, e) {
        e.preventDefault();
        var data = {
            page: pageSelected
        };

        $.ajax({
            type: "POST",
            url: "/PageContractBid/ReadPagingCompanyAbilityFinance",
            data: data,
            success: function (responsed) {
                $("#tblNLTC").html(responsed.patialView);
                $("#NLTC_MODAL").html(responsed.partialModalView);
                if (window.IsEditPage == true) {
                    $('.btn-Modal-Delete').hide();
                }
            }
        });
    }

    function popupAddNlnsEvent() {

        $("input[name='PositionSimilar']").on('change', (e) => {
            var projectSimilarVal = $(e.target).closest('tr').find("input[name='ProjectSimilar']").val();
            if (projectSimilarVal != '')
                return;
            toastr.warning("Vui lòng nhập dự án tham gia");
        });

        $("input[name='FromYear']").on('change', (e) => {
            var projectSimilarVal = $(e.target).closest('tr').find("input[name='ProjectSimilar']").val();
            if (projectSimilarVal != '')
                return;
            toastr.warning("Vui lòng nhập dự án tham gia");
        });

        $("input[name='ToYear']").on('change', (e) => {
            var projectSimilarVal = $(e.target).closest('tr').find("input[name='ProjectSimilar']").val();
            if (projectSimilarVal != '')
                return;
            toastr.warning("Vui lòng nhập dự án tham gia");
        });

    }

    // If lastProjectSimilarVal is null return false else true
    function checkLastProjectSimilarVal(tableId) {
        var lastProjectSimilarVal = $('#' + tableId + " tr:last").find("input[name='ProjectSimilar']").val();
        return lastProjectSimilarVal != "" ? true : false;
    }

    function getCompanyAbilityExpDetail(id) {
        closeAllPopUpEdit();

        $("#PopUp_Edit_NLKN").html('');
        var data = {
            id: id
        };

        $.ajax({
            type: "POST",
            url: "/PageContractBid/GetCompanyAbilityExpDetailById",
            data: data,
            success: function (responsed) {
                if (responsed) {
                    $("#PopUp_Edit_NLKN").append(responsed);
                    showPopUpEditNLKN();

                    $('.ContractStartDate').datepicker({
                        todayBtn: "linked",
                        format: 'dd/mm/yyyy',
                        keyboardNavigation: false,
                        forceParse: false,
                        calendarWeeks: false,
                        autoclose: true
                    });
                    $('.ContractEndDate').datepicker({
                        todayBtn: "linked",
                        format: 'dd/mm/yyyy',
                        keyboardNavigation: false,
                        forceParse: false,
                        calendarWeeks: false,
                        autoclose: true
                    });
                }
            }
        });
    }

    function showPopUpEditNLKN() {
        $("#ctr_Edit_NLKN").removeClass("bs-transaction");
        $("#ctr_Edit_NLKN").addClass("bs-transaction-edit");
        $("#btn_ctr_NLKN").addClass("hide");

        $("#ctr_NLTC").removeClass("bs-transaction-edit");
        $("#ctr_NLTC").addClass("bs-transaction");
        $("#ctr_NLTB").removeClass("bs-transaction-edit");
        $("#ctr_NLTB").addClass("bs-transaction");
        $("#ctr_NLNS").removeClass("bs-transaction-edit");
        $("#ctr_NLNS").addClass("bs-transaction");

        $("#btn_ctr_NLTB").removeClass("hide");
        $("#btn_ctr_NLTC").removeClass("hide");
        $("#btn_ctr_NLNS").removeClass("hide");
        closeAllPopUpAdd();
    }

    function getCompanyAbilityFinanceDetail(id) {
        closeAllPopUpEdit();

        $("#PopUp_Edit_NLTC").html('');
        var data = {
            id: id
        };

        $.ajax({
            type: "POST",
            url: "/PageContractBid/GetCompanyAbilityFinanceDetailById",
            data: data,
            success: function (responsed) {
                if (responsed) {
                    $("#PopUp_Edit_NLTC").append(responsed);
                    showPopUpEditNLTC();
                }
            }
        });
    }

    function showPopUpEditNLTC() {
        $("#ctr_Edit_NLTC").removeClass("bs-transaction");
        $("#ctr_Edit_NLTC").addClass("bs-transaction-edit");
        $("#btn_ctr_NLTC").addClass("hide");

        $("#ctr_NLKN").removeClass("bs-transaction-edit");
        $("#ctr_NLKN").addClass("bs-transaction");
        $("#ctr_NLTB").removeClass("bs-transaction-edit");
        $("#ctr_NLTB").addClass("bs-transaction");
        $("#ctr_NLNS").removeClass("bs-transaction-edit");
        $("#ctr_NLNS").addClass("bs-transaction");

        $("#btn_ctr_NLTB").removeClass("hide");
        $("#btn_ctr_NLKN").removeClass("hide");
        $("#btn_ctr_NLNS").removeClass("hide");
        closeAllPopUpAdd();
    }

    function getCompanyAbilityHrDetail(id) {
        closeAllPopUpEdit();

        $("#PopUp_Edit_NLNS").html('');
        var data = {
            id: id
        };

        $.ajax({
            type: "POST",
            url: "/PageContractBid/GetCompanyAbilityHrDetailById",
            data: data,
            success: function (responsed) {
                if (responsed) {
                    $("#PopUp_Edit_NLNS").append(responsed);
                    showPopUpEditNLNS();
                    bindDatePickerEvent();
                }
            }
        });
    }

    function showPopUpEditNLNS() {
        $("#ctr_Edit_NLNS").removeClass("bs-transaction");
        $("#ctr_Edit_NLNS").addClass("bs-transaction-edit");
        $("#btn_ctr_NLKS").addClass("hide");

        $("#ctr_NLTC").removeClass("bs-transaction-edit");
        $("#ctr_NLTC").addClass("bs-transaction");
        $("#ctr_NLTB").removeClass("bs-transaction-edit");
        $("#ctr_NLTB").addClass("bs-transaction");
        $("#ctr_NLKN").removeClass("bs-transaction-edit");
        $("#ctr_NLKN").addClass("bs-transaction");

        $("#btn_ctr_NLTB").removeClass("hide");
        $("#btn_ctr_NLTC").removeClass("hide");
        $("#btn_ctr_NLKN").removeClass("hide");

        closeAllPopUpAdd();
    }

    function getCompanyAbilityEquipmentDetail(id) {
        closeAllPopUpEdit();

        $("#PopUp_Edit_NLTB").html('');
        var data = {
            id: id
        };

        $.ajax({
            type: "POST",
            url: "/PageContractBid/GetCompanyAbilityEquipmentDetailById",
            data: data,
            success: function (responsed) {
                if (responsed) {
                    $("#PopUp_Edit_NLTB").append(responsed);
                    showPopUpEditNLTB();
                }
            }
        });
    }

    function showPopUpEditNLTB() {
        $("#ctr_Edit_NLTB").removeClass("bs-transaction");
        $("#ctr_Edit_NLTB").addClass("bs-transaction-edit");
        $("#btn_ctr_NLTB").addClass("hide");

        $("#ctr_NLTC").removeClass("bs-transaction-edit");
        $("#ctr_NLTC").addClass("bs-transaction");
        $("#ctr_NLKN").removeClass("bs-transaction-edit");
        $("#ctr_NLKN").addClass("bs-transaction");
        $("#ctr_NLNS").removeClass("bs-transaction-edit");
        $("#ctr_NLNS").addClass("bs-transaction");

        $("#btn_ctr_NLKN").removeClass("hide");
        $("#btn_ctr_NLTC").removeClass("hide");
        $("#btn_ctr_NLNS").removeClass("hide");
        closeAllPopUpAdd();
    }

    function closeAllPopUpEdit() {
        $("#ctr_Edit_NLKN").addClass("bs-transaction");
        $("#ctr_Edit_NLKN").removeClass("bs-transaction-edit");
        $("#btn_ctr_NLKN").removeClass("hide");
        clearAddCompanyAbilityExpForm();

        $("#ctr_Edit_NLNS").addClass("bs-transaction");
        $("#ctr_Edit_NLNS").removeClass("bs-transaction-edit");
        $("#btn_ctr_NLNS").removeClass("hide");
        clearAddCompanyAbilityHrForm();

        $("#ctr_Edit_NLTC").addClass("bs-transaction");
        $("#ctr_Edit_NLTC").removeClass("bs-transaction-edit");
        $("#btn_ctr_NLTC").removeClass("hide");
        clearAddCompanyAbilityFinanceForm();

        $("#ctr_Edit_NLTB").addClass("bs-transaction");
        $("#ctr_Edit_NLTB").removeClass("bs-transaction-edit");
        $("#btn_ctr_NLTB").removeClass("hide");
        clearAddCompanyAbilityEquipmentForm();
    }

    function closeAllPopUpAdd() {
        $("#ctr_NLKN").addClass("bs-transaction");
        $("#ctr_NLKN").removeClass("bs-transaction-edit");
        $("#btn_ctr_NLKN").removeClass("hide");

        $("#ctr_NLNS").addClass("bs-transaction");
        $("#ctr_NLNS").removeClass("bs-transaction-edit");
        $("#btn_ctr_NLNS").removeClass("hide");

        $("#ctr_NLTC").addClass("bs-transaction");
        $("#ctr_NLTC").removeClass("bs-transaction-edit");
        $("#btn_ctr_NLTC").removeClass("hide");

        $("#ctr_NLTB").addClass("bs-transaction");
        $("#ctr_NLTB").removeClass("bs-transaction-edit");
        $("#btn_ctr_NLTB").removeClass("hide");
    }

    //show confirm update profile box
    function showConfirmUpdateProfileBox(event) {
        event.preventDefault();

        if (validForm("frmExportProfile")) {
            $('#confirmUpdateProfileBox').modal('show');
        }
    }

    //show confirm Add profile box
    function showConfirmProfileBox(event) {
        event.preventDefault();

        if (validForm("frmExportProfile")) {
            $('#confirmAddProfileBox').modal('show');
        }
    }

    //Clear required message
    function clearRequiredMessage(element) {
        $(element).next('span').children().html('');
    }

    //Append required message
    function appendRequiredMessage(element) {
        var message = $(element).attr('data-val-required');
        $(element).next('span').children().append(message);
    }

    //Check input file required
    function checkInputFileRequired(formId) {
        var inputElements = $('#' + formId).find('input[type=file]');
        var isValid = true;
        $.each(inputElements, function (i, ele) {
            if (ele.hasAttribute('data-val-required')) {
                if ($(ele).val() == '') {
                    clearRequiredMessage(ele);
                    appendRequiredMessage(ele);
                    isValid = false;
                }
            }
        });

        return isValid;
    }

    //show confirm Add box
    function showConfirmAddBox(event, formId, action) {
        event.preventDefault();
        $('#confirmAddBox button[data-bb-handler="confirm"][type="button"]').unbind("click");

        if (action == "edit") {
            triggerValidateEventForm(formId);
        }

        if (validForm(formId)) {

            $('#confirmAddBox').modal('show');
            $('#confirmAddBox button[data-bb-handler="confirm"][type="button"]').click(function (event) {
                checkFormIdToCreateFunc(event, formId);
            });
        }
    }

    //show confirm Update HSNL box
    function showConfirmHSNLBox(event, formId, action) {
        event.preventDefault();
        $('#confirmUpdateHSNLBox button[data-bb-handler="confirm"][type="button"]').unbind("click");

        if (action == "edit") {
            triggerValidateEventForm(formId);
        }

        if (validForm(formId)) {
            $('#confirmUpdateHSNLBox').modal('show');
            $('#confirmUpdateHSNLBox button[data-bb-handler="confirm"][type="button"]').click(function (event) {
                checkFormIdToCreateFunc(event, formId);
            });
        }
    }

    function checkFormIdToCreateFunc(event, formId) {
        if (formId.indexOf('CompanyAbilityExp') > -1)
            return createCompanyAbilityExp(event, formId);
        else if (formId.indexOf('CompanyAbilityHr') > -1)
            return createCompanyAbilityHr(event, formId);
        else if (formId.indexOf('CompanyAbilityFinance') > -1)
            return createCompanyAbilityFinance(event, formId);
        else if (formId.indexOf('CompanyAbilityEquipment') > -1)
            return createCompanyAbilityEquipment(event, formId);
    }

    function bindClickButtonEvent() {
        //NLNS
        $("#btn_ctr_NLNS").click(function () {
            pageContractBid.closeAllPopUpEdit();
            $("#ctr_NLNS").removeClass("bs-transaction");
            $("#ctr_NLNS").addClass("bs-transaction-edit");
            $("#btn_ctr_NLNS").addClass("hide");

            $("#ctr_NLTC").removeClass("bs-transaction-edit");
            $("#ctr_NLTC").addClass("bs-transaction");
            $("#ctr_NLTB").removeClass("bs-transaction-edit");
            $("#ctr_NLTB").addClass("bs-transaction");
            $("#ctr_NLKN").removeClass("bs-transaction-edit");
            $("#ctr_NLKN").addClass("bs-transaction");

            $("#btn_ctr_NLTB").removeClass("hide");
            $("#btn_ctr_NLTC").removeClass("hide");
            $("#btn_ctr_NLKN").removeClass("hide");
            //$('tbody#NLNS_Detail > tr').not(':first').remove();

            $('html, body').animate({
                scrollTop: $("#content-NLNS").offset().top
            }, 1000);
        });
        $("body").on('click', '.btn_ctr_NLNS_out', function () {
            $("#ctr_NLNS").addClass("bs-transaction");
            $("#ctr_NLNS").removeClass("bs-transaction-edit");
            $("#btn_ctr_NLNS").removeClass("hide");
            pageContractBid.closeAllPopUpEdit();
            //$('tbody#NLNS_Detail > tr').not(':first').remove();
        });
        //NLTB
        $("#btn_ctr_NLTB").click(function () {
            pageContractBid.closeAllPopUpEdit();
            $("#ctr_NLTB").removeClass("bs-transaction");
            $("#ctr_NLTB").addClass("bs-transaction-edit");
            $("#btn_ctr_NLTB").addClass("hide");

            $("#ctr_NLTC").removeClass("bs-transaction-edit");
            $("#ctr_NLTC").addClass("bs-transaction");
            $("#ctr_NLNS").removeClass("bs-transaction-edit");
            $("#ctr_NLNS").addClass("bs-transaction");
            $("#ctr_NLKN").removeClass("bs-transaction-edit");
            $("#ctr_NLKN").addClass("bs-transaction");

            $("#btn_ctr_NLTC").removeClass("hide");
            $("#btn_ctr_NLKN").removeClass("hide");
            $("#btn_ctr_NLNS").removeClass("hide");

            $('html, body').animate({
                scrollTop: $("#content-NLTB").offset().top
            }, 1000);
        });
        $("body").on('click', '.btn_ctr_NLTB_out', function () {
            $("#ctr_NLTB").addClass("bs-transaction");
            $("#ctr_NLTB").removeClass("bs-transaction-edit");
            $("#btn_ctr_NLTB").removeClass("hide");
            pageContractBid.closeAllPopUpEdit();
        });
        //NLKN
        $("#btn_ctr_NLKN").click(function () {
            //content-NLKN
            pageContractBid.closeAllPopUpEdit();

            $("#ctr_NLKN").removeClass("bs-transaction");
            $("#ctr_NLKN").addClass("bs-transaction-edit");
            $("#btn_ctr_NLKN").addClass("hide");

            $("#ctr_NLTC").removeClass("bs-transaction-edit");
            $("#ctr_NLTC").addClass("bs-transaction");
            $("#ctr_NLTB").removeClass("bs-transaction-edit");
            $("#ctr_NLTB").addClass("bs-transaction");
            $("#ctr_NLNS").removeClass("bs-transaction-edit");
            $("#ctr_NLNS").addClass("bs-transaction");

            $("#btn_ctr_NLTB").removeClass("hide");
            $("#btn_ctr_NLTC").removeClass("hide");
            $("#btn_ctr_NLNS").removeClass("hide");

            $('html, body').animate({
                scrollTop: $("#content-NLKN").offset().top
            }, 1000);
        });
        $("body").on('click', '.btn_ctr_NLKN_out', function () {
            $("#ctr_NLKN").addClass("bs-transaction");
            $("#ctr_NLKN").removeClass("bs-transaction-edit");
            $("#btn_ctr_NLKN").removeClass("hide");
            pageContractBid.closeAllPopUpEdit();
        });
        //NLTC
        $("#btn_ctr_NLTC").click(function () {
            pageContractBid.closeAllPopUpEdit();
            $("#ctr_NLTC").removeClass("bs-transaction");
            $("#ctr_NLTC").addClass("bs-transaction-edit");
            $("#btn_ctr_NLTC").addClass("hide");

            $("#ctr_NLTB").removeClass("bs-transaction-edit");
            $("#ctr_NLTB").addClass("bs-transaction");
            $("#ctr_NLNS").removeClass("bs-transaction-edit");
            $("#ctr_NLNS").addClass("bs-transaction");
            $("#ctr_NLKN").removeClass("bs-transaction-edit");
            $("#ctr_NLKN").addClass("bs-transaction");

            $("#btn_ctr_NLTB").removeClass("hide");
            $("#btn_ctr_NLKN").removeClass("hide");
            $("#btn_ctr_NLNS").removeClass("hide");

            $('html, body').animate({
                scrollTop: $("#content-NLTC").offset().top
            }, 1000);
        });
        $("body").on('click', '.btn_ctr_NLTC_out', function () {
            $("#ctr_NLTC").addClass("bs-transaction");
            $("#ctr_NLTC").removeClass("bs-transaction-edit");
            $("#btn_ctr_NLTC").removeClass("hide");
            pageContractBid.closeAllPopUpEdit();
        });
    }

    function bindDatePickerEvent() {

        $('.FromYear').datepicker({
            todayBtn: "linked",
            format: 'dd/mm/yyyy',
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: false,
            autoclose: true
        });
        $('.ToYear').datepicker({
            todayBtn: "linked",
            format: 'dd/mm/yyyy',
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: false,
            autoclose: true
        });
        $('#ContractStartDate').datepicker({
            todayBtn: "linked",
            format: 'dd/mm/yyyy',
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: false,
            autoclose: true
        });
        $('#ContractEndDate').datepicker({
            todayBtn: "linked",
            format: 'dd/mm/yyyy',
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: false,
            autoclose: true
        });
    }

    //Append add row NLNS detail
    function appendRowNLNSDetail(event, tableId) {
        event.preventDefault();

        if (checkLastProjectSimilarVal(tableId) != true) {
            toastr.warning("Vui lòng nhập dự án tham gia");
            return;
        }
        var newRowEle = '<tr class="footable-even additionalRow"><td class="footable-visible"><input type="text" placeholder="Dự án tham gia" class="form-control" name="ProjectSimilar"></td>'
            + '<td class="footable-visible footable-first-column"><div class="input-group date"><span class="input-group-addon"><i class="fa fa-calendar color-date"></i></span>'
            + '<input type="text" class="form-control datepicker FromYear" placeholder="dd/MM/yyyy" value="" name="FromYear"></div></td>'
            + '<td class="footable-visible"><div class="input-group date"><span class="input-group-addon"><i class="fa fa-calendar color-date"></i></span>'
            + '<input type="text" class="form-control datepicker ToYear" placeholder="dd/MM/yyyy" value="" name="ToYear"></div></td>'
            + '<td class="footable-visible"> <input type="text" placeholder="Vị trí" class="form-control" name="PositionSimilar"></td></tr> ';
        $(newRowEle).appendTo($('#' + tableId));
        bindDatePickerEvent();
    }

    //Hide delete button in modal
    function hideDeleteButtonModal() {
        $('body').on('shown.bs.modal', function (e) {
            // do something...
            $('.btn-Modal-Delete').hide();
        });
    }

    return {
        createCompanyAbilityExp: createCompanyAbilityExp,
        createCompanyAbilityFinance: createCompanyAbilityFinance,
        createCompanyAbilityHr: createCompanyAbilityHr,
        createCompanyAbilityEquipment: createCompanyAbilityEquipment,

        changePageOnListCompanyAbilityExp: changePageOnListCompanyAbilityExp,
        changePageOnListCompanyAbilityEquipment: changePageOnListCompanyAbilityEquipment,
        changePageOnListCompanyAbilityHr: changePageOnListCompanyAbilityHr,
        changePageOnListCompanyAbilityFinance: changePageOnListCompanyAbilityFinance,

        initCompanyAbilityExpList: initCompanyAbilityExpList,
        initCompanyAbilitHrList: initCompanyAbilitHrList,
        initCompanyAbilityEquipmentList: initCompanyAbilityEquipmentList,
        initCompanyAbilityFinanceList: initCompanyAbilityFinanceList,

        clearAddCompanyAbilityExpForm: clearAddCompanyAbilityExpForm,
        clearAddCompanyAbilityHrForm: clearAddCompanyAbilityHrForm,
        clearAddCompanyAbilityEquipmentForm: clearAddCompanyAbilityEquipmentForm,
        clearAddCompanyAbilityFinanceForm: clearAddCompanyAbilityFinanceForm,

        checkBoxInit: checkBoxInit,
        storeCheckBoxVal: storeCheckBoxVal,
        exportProfile: exportProfile,

        updateCheckBoxStatus: updateCheckBoxStatus,
        updateCompanyAbilityFinance: updateCompanyAbilityFinance,
        updateCheckBoxStatus_WhenOpenModal: updateCheckBoxStatus_WhenOpenModal,

        chooseButtonEventModal: chooseButtonEventModal,
        exitButtonEventModal: exitButtonEventModal,
        popupAddNlnsEvent: popupAddNlnsEvent,
        convertArrayItems: convertArrayItems,

        getCompanyAbilityExpDetail: getCompanyAbilityExpDetail,
        getCompanyAbilityFinanceDetail: getCompanyAbilityFinanceDetail,
        getCompanyAbilityHrDetail: getCompanyAbilityHrDetail,
        getCompanyAbilityEquipmentDetail: getCompanyAbilityEquipmentDetail,

        closeAllPopUpEdit: closeAllPopUpEdit,
        showConfirmAddBox: showConfirmAddBox,
        showConfirmHSNLBox: showConfirmHSNLBox,
        showConfirmProfileBox: showConfirmProfileBox,
        showConfirmUpdateProfileBox: showConfirmUpdateProfileBox,
        showConfirmUpdateBox: showConfirmUpdateBox,

        bindClickButtonEvent: bindClickButtonEvent,
        bindDatePickerEvent: bindDatePickerEvent,
        appendRowNLNSDetail: appendRowNLNSDetail,
        hideDeleteButtonModal: hideDeleteButtonModal
    };
}();