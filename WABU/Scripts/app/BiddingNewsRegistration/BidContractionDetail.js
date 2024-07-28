var bidContractionDetail = function () {

    //[STR]-----------------Store price and upload file to bidding-----------------

    //Create bidding detail
    function printInfoToBidding(e) {

        var companyProfileId = $('input.printBiddingInfo:checked').val();
        var nameProfile = $('input.printBiddingInfo:checked').parent().parent().parent().next().html();
        if (companyProfileId == "" || typeof companyProfileId == 'undefined') {
            toastr.warning("Vui lòng chọn hồ sơ");
            return;
        }
        if (validForm("PrintInfoBidding")) {
            var dataForm = new FormData($("form#PrintInfoBidding")[0]);
            dataForm.set('Price', parseInt(reFormatPriceValue(dataForm.get('Price'))));
            dataForm.set('NumberOfDaysImplement', parseInt(reAppendDayText(dataForm.get('NumberOfDaysImplement'))));
            if (!checkValFormIsNull(dataForm, false) || !checkFileNameDuplicate(dataForm)) {
                return;
            }
            else {

                dataForm.append("CompanyProfileId", companyProfileId);
                dataForm.append("NameProfile", nameProfile);
                var otherFiles = $("form#PrintInfoBidding").find('input[name="OtherFiles"]');
                for (var i = 0; i < otherFiles.length; i++) {
                    var technicalRequirementName = '';
                    technicalRequirementName = $(otherFiles[i]).data('filenametype');
                    var file = $(otherFiles[i])[0].files[0];
                    dataForm.append('OtherFiles[' + i + ']', file);
                    dataForm.append('TechnicalRequirementNameList[' + i + ']', technicalRequirementName);
                    var technicalOtherId = $(otherFiles[i]).data('technicalotherid');
                    dataForm.append('TechnicalOtherIdList[' + i + ']', parseInt(technicalOtherId));
                }

                $.ajax({
                    type: "POST",
                    url: "/BidContractionDetail/PrintInfoToBidding",
                    data: dataForm,
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    success: function (resultData) {
                        if (resultData && resultData.succeed === "0") {
                            window.location.reload();
                        }
                        else if (resultData && resultData.succeed === "-1") {
                            toastr.error(resultData.message);
                            setTimeout(function () { window.location = "/Account/ProfileManagement"; }, 3000);
                        }
                        else {
                            toastr.error(resultData);
                        }
                    }
                });
            }
        }
    }

    //Check price or files do not input
    function checkValFormIsNull(dataForm, isUpdated) {
        for (let key of dataForm.entries()) {
            if (key[0] === "BiddingNewsId")
                continue;
            else if (key[1].constructor === File && key[1].size == 0 && isUpdated == false) {
                printWarningMessage(key[0]);
                return false;
            }
            else if (key[1].constructor !== File && (key[0] == "Price") && (key[1] == "" || key[1] == 'NaN')) {
                printPriceWarningMessage();
                return false;
            }
            else if (key[1].constructor !== File && (key[0] == "NumberOfDaysImplement") && (key[1] == "" || key[1] == 'NaN')) {
                printNumberOfDaysImplementWarningMessage();
                return false;
            }
        }

        return true;
    }

    //Check price or files do not input
    function checkValUpdateFormIsNull(dataForm) {
        for (let key of dataForm.entries()) {
            if (key[0] === "BiddingNewsId")
                continue;
            else if (key[1].constructor !== File && (key[0] == "Price") && (key[1] == "" || key[1] == 'NaN')) {
                printPriceWarningMessage();
                return false;
            }
            else if (key[1].constructor !== File && (key[0] == "NumberOfDaysImplement") && (key[1] == "" || key[1] == 'NaN')) {
                printNumberOfDaysImplementWarningMessage();
                return false;
            }
        }

        return true;
    }

    //Check file name is duplicate
    function checkFileNameDuplicate(dataForm) {
        let fileNames = [];
        for (let key of dataForm.entries()) {
            if (key[1].constructor === File) {
                fileNames.push(key[1].name);
            }
        }

        let result = fileNames.filter((item, index) => fileNames.indexOf(item) != index).length > 0;
        if (result) {
            printFileNameDuplicateMessage();
            return false;
        }

        return true;
    }

    function printFileNameDuplicateMessage() {
        toastr.warning("Bạn đã upload trùng file.");
    }

    function printPriceWarningMessage() {
        toastr.warning("Vui lòng nhập giá");
    }

    function printNumberOfDaysImplementWarningMessage() {
        toastr.warning("Vui lòng nhập số ngày thực hiện");
    }

    function printWarningMessage(element) {
        var message = $("input[name=" + element + "]").parent().parent().prev("label").text();
        toastr.warning("Vui lòng nhập " + message);
    }

    //get Bidding Info Detail to edit
    function getBiddingInfoDetail(id, biddingNewsId) {
        $("#pop-up-edit-biddingInfo").html('');
        var data = {
            id: id,
            biddingNewsId: biddingNewsId
        };

        $.ajax({
            type: "POST",
            url: "/BidContractionDetail/GetBiddingInfoDetailById",
            data: data,
            success: function (responsed) {
                $("#pop-up-edit-biddingInfo").append(responsed);
                $("#pop-up-edit-biddingInfo").modal("show");
                triggerIcheck();
                formatCurrency($('input[name="Price"]').val());
                formatPriceValue('Price');
                appendDayText('NumberOfDaysImplement');
            }
        });
    }

    //Update Bidding Info Detail
    function updateBiddingInfoDetail(e) {
        var companyProfileId = $('input.printBiddingInfo:checked').val();
        var nameProfile = $('input.printBiddingInfo:checked').parent().parent().parent().next().html();
        if (companyProfileId == "" || typeof companyProfileId == 'undefined') {
            toastr.warning("Vui lòng chọn hồ sơ");
            return;
        }
        if (validForm("EditPrintInfoBidding")) {
            const dataForm = new FormData($("form#EditPrintInfoBidding")[0]);
            if (checkValUpdateFormIsNull(dataForm) == false) {
                return;
            }
            else {
                dataForm.set('Price', parseInt(reFormatPriceValue(dataForm.get('Price'))));
                dataForm.set('NumberOfDaysImplement', parseInt(reAppendDayText(dataForm.get('NumberOfDaysImplement'))));
                dataForm.append("CompanyProfileId", companyProfileId);
                dataForm.append("NameProfile", nameProfile);
                var otherFiles = $("form#EditPrintInfoBidding").find('input[name="OtherFiles"]');
                for (var i = 0; i < otherFiles.length; i++) {
                    var technicalRequirementName = '';
                    technicalRequirementName = $(otherFiles[i]).data('filenametype');
                    var file = $(otherFiles[i])[0].files[0];
                    dataForm.append('OtherFiles[' + i + ']', file);
                    dataForm.append('TechnicalRequirementNameList[' + i + ']', technicalRequirementName);
                    var technicalOtherId = $(otherFiles[i]).data('technicalotherid');
                    dataForm.append('TechnicalOtherIdList[' + i + ']', parseInt(technicalOtherId));
                }
                $.ajax({
                    type: "POST",
                    url: "/BidContractionDetail/UpdateBiddingInfoDetail",
                    data: dataForm,
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    success: function (resultData) {
                        if (resultData && resultData.succeed === "0") {
                            toastr.success("Cập Nhật Thành Công.");
                            window.location.reload();
                        }
                        else {
                            toastr.error(resultData);
                        }
                    }
                });
            }
        }
    }

    //delete Bidding Info Detail
    function deleteBiddingInfoDetail() {
        var data = {
            id: $("#currentBiddingDetailId").val()
        };
        $.ajax({
            type: "POST",
            url: "/BidContractionDetail/DeleteBiddingDetailById",
            data: data,
            success: function (responsed) {
                if (responsed && responsed.succeed === "0") {
                    toastr.success("Hủy bỏ đấu thầu Thành Công.");
                    window.location.reload();
                }
            }
        });
    }

    // Validate file type to upload
    function validateSingleInput(oInput) {
        var validFileExtensions = [".pdf"];

        if (oInput.type == "file") {
            var sFileName = oInput.value;
            if (sFileName.length > 0) {
                var blnValid = false;
                for (var j = 0; j < validFileExtensions.length; j++) {
                    var sCurExtension = validFileExtensions[j];
                    if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                        blnValid = true;
                        break;
                    }
                }

                if (!blnValid) {
                    toastr.warning("Bạn chỉ được nhập file dưới định dạng " + validFileExtensions.join(", "));
                    oInput.value = "";
                    return false;
                }
            }
        }

        return true;
    }

    //reinstall icheck
    var triggerIcheck = function () {
        $('.i-checks').iCheck({
            checkboxClass: 'icheckbox_square-green',
            radioClass: 'iradio_square-green'
        });
    }

    var reFormatPriceValue = (value) => {
        return value.replace(/,/g, '').replace(/vnđ/g, '');
    }

    var formatPriceValue = function (name) {

        //Format value price
        $('input[name = ' + name + ']').on('input', function (e) {
            $(this).val(formatCurrency(this.value.replace(/[, vnđ]/g, '')));
        }).on('keypress', function (e) {
            if (!$.isNumeric(String.fromCharCode(e.which))) e.preventDefault();
        }).on('paste', function (e) {
            var cb = e.originalEvent.clipboardData || window.clipboardData;
            if (!$.isNumeric(cb.getData('text'))) e.preventDefault();
        });
    }

    function formatCurrency(number) {
        var n = number.split('').reverse().join("");
        var n2 = n.replace(/\d\d\d(?!$)/g, "$&,");
        return n2.split('').reverse().join('') + ' vnđ';
    }

    var reAppendDayText = (value) => {
        return value.replace(/ ngày/g, '');
    }

    var appendDayText = function (name) {
        $('input[name = ' + name + ']').on('input', function (e) {
            $(this).val(formatDay(this.value.replace(/[ ngày]/g, '')));
        }).on('keypress', function (e) {
            if (!$.isNumeric(String.fromCharCode(e.which))) e.preventDefault();
        }).on('paste', function (e) {
            var cb = e.originalEvent.clipboardData || window.clipboardData;
            if (!$.isNumeric(cb.getData('text'))) e.preventDefault();
        });

        function formatDay(number) {
            var n = number.split('').reverse().join("");
            var n2 = n.replace(/\d\d\d(?!$)/g, "$&");
            return n2.split('').reverse().join('') + ' ngày';
        }
    }

    //[END]-----------------Store price and upload file to bidding-----------------

    //[STR]-----------------Download file biddingNews-----------------

    var downloadDocument = () => {
        $('.downloadDocument').on('click', (e) => {
            e.preventDefault();

            var authority = $('#downloadDocumentForm').find('input[name="authority"]').val();
            if (authority != "3") {
                toastr.warning("Chỉ có Nhà Thầu mới có thể tải tài liệu này");
                return;
            }
            var pathForm = $('#downloadDocumentForm').find('input[name="path"]');
            var dataPath = $(e.currentTarget).data('path');
            pathForm.val(dataPath);
            document.getElementById('downloadDocumentForm').submit();
        });
    }

    //[END]-----------------Download file biddingNews-----------------

    //[STR]-----------------Save BiddingNewsBookmark-----------------

    var saveBiddingBookmark = (e) => {
        e.preventDefault();

        if ($('input[name=BiddingNewsId]').val() != "") {
            var data = {
                biddingNewsId: $('input[name=BiddingNewsId]').val()
            };

            $.ajax({
                type: "POST",
                url: "/BidContractionDetail/SaveBiddingNewsBookMark",
                data: data,
                success: function (responsed) {
                    if (responsed && responsed.succeed === "0") {
                        toastr.success("Lưu tin thầu thành công");
                        $('#btn_SaveBiddingBookmark').addClass('hidden');
                    }
                    else {
                        toastr.error(responsed);
                    }
                }
            });
        }
        else {
            toastr.error("Có lỗi xảy ra vui lòng liên hệ admin");
        }
    }

    //[END]-----------------Save BiddingNewsBookmark-----------------

    return {
        printInfoToBidding: printInfoToBidding,
        getBiddingInfoDetail: getBiddingInfoDetail,
        updateBiddingInfoDetail: updateBiddingInfoDetail,
        deleteBiddingInfoDetail: deleteBiddingInfoDetail,
        validateSingleInput: validateSingleInput,
        downloadDocument: downloadDocument,
        saveBiddingBookmark: saveBiddingBookmark
    };
}();