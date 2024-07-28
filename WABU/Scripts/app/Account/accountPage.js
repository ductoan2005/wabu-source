var accountPage = function () {
    var conditionStr = "";

    function init() {

    }
    function toastr_edit() {
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
    }
    //[STR]-----------------Update information account Function-----------------
    function updateUser() {
        //toastr_edit();
        var dataForm = new FormData($("form#frmEditUser")[0]);
        var boolcheck = 0;
        if ($("#Password").val() == $("#Password-Confirm").val()) {
            boolcheck = 1;
        }
        else {
            boolcheck = 0;
            if ($("#msg-err-Password-Confirm").length > 0) {
                $("#msg-err-Password-Confirm").html("Nhập password xác nhận không trùng nhau");
            }
            else {
                $('#Password-Confirm-Mess').append('<span id="msg-err-Password-Confirm" class="">Nhập password xác nhận không trùng nhau</span>');
            }
        }
        if (boolcheck == 1) {
            $.ajax({
                type: "POST",
                url: "/Account/UpdateInformation",
                data: dataForm,
                dataType: 'json',
                contentType: false,
                processData: false,
                success: function (resultData) {
                    $("#Password-Confirm").val("");
                    //$("#msg-err-Password-Confirm").html("");
                    $("#msg-err-Password").html("");
                    $('.box_avatar img').attr('src', '/' + resultData.data.AvatarPath);
                    toastr.success("Cập nhật thành công");
                }
            });
        }
    }
    //[STR]-----------------Create Function-----------------

    // Validate file type to upload
    function validateSingleInput(oInput, fileType) {
        var validFileExtensions;
        if (fileType == "image") {
            validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
        }
        else if (fileType == "file") {
            validFileExtensions = [".pdf"];
        }

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
        if (fileType == "image") {
            previewImage(oInput);
        }

        return true;
    }

    function previewImage(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {

                var imgId = $(input).next("img");
                $(imgId).attr('src', e.target.result);
            };

            reader.readAsDataURL(input.files[0]);
        }

    }

    //[END]-----------------Create Function-----------------

    //[STR]-----------------Paging Function-----------------

    function changePageOnListPackage(pageSelected, e) {
        e.preventDefault();
        var data = {
            page: pageSelected,
            condition: conditionStr
        };

        $.ajax({
            type: "POST",
            url: "/Account/ReadPagingPackageList",
            data: data,
            success: function (responsed) {
                $("#tblPackageDetail").html(responsed.patialView);
            }
        });
    }

    function changePageOnListContract(pageSelected, e) {
        e.preventDefault();
        var data = {
            page: pageSelected
        };

        $.ajax({
            type: "POST",
            url: "/Account/ReadPagingContractList",
            data: data,
            success: function (responsed) {
                $("#tblContract").html(responsed.patialView);
            }
        });
    }

    //[END]-----------------Delete Function-----------------

    //[STR]-----------------Get Select List Function-----------------

 

    //function get

    //[END]-----------------Get Select List Function-----------------

    //[START]-----------------Search Function-----------------

    function searchPagePackage(e) {
        e.preventDefault();
        var fromDate = $("input[name='FromDate']").val();
        var toDate = $("input[name='ToDate']").val();
        var status = $("select[name='Status']");
        var contructionName = $("select[name='ContructionName']").find(":selected").text();
        var biddingPackageType = $("select[name='BiddingPackageName']").find(":selected").val();
        var authority = $("input[name='Authority']").val();
        conditionStr = JSON.stringify({ FromDate: fromDate, ToDate: toDate, StatusBidding: status.val(), ContructionName: contructionName, BiddingPackageType: biddingPackageType, Authority: authority });
        var data = {
            page: 1,
            condition: conditionStr
        };
        $.ajax({
            type: "POST",
            url: "/Account/ReadPagingPackageList",
            data: data,
            success: function (responsed) {
                $("#tblPackageDetail").html(responsed.patialView);
            }
        });
    }

    //[END]-----------------Search Function-----------------

    

    //[END]-----------------Check box Xuat Ho So PageContracBid Function-----------------

    return {
        init: init,
        updateUser: updateUser,
        changePageOnListPackage: changePageOnListPackage,
        searchPagePackage: searchPagePackage,
        changePageOnListContract: changePageOnListContract,
        validateSingleInput: validateSingleInput
    };
}();