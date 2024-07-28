var abilityExp = function () {
    function initAbilityExp() {
        $("#btn_ctr_NLKN").click(function () {
            LoadDataNLKNToStorage();
            $("#placeholder-NLKN").removeClass("display-none").addClass("display-normal");
            $("#content-NLKN-saved").removeClass("display-inline-grid").addClass("display-none");

            $("#ctr_NLKN").removeClass("bs-transaction");
            $("#ctr_NLKN").addClass("bs-transaction-edit");
            $("#ctr_NLKN").addClass("bs-transaction-edit");
            $("#btn_ctr_NLKN").addClass("hide");
            $('#div-chk-NumberYearActivity .iCheck-helper').attr('onclick', 'abilityExp.CheckEnableNumberYearActivity();');
            $('#div-chk-NumberSimilarContract .iCheck-helper').attr('onclick', 'abilityExp.CheckEnableNumberSimilarContract();');
        })

        $("#btn_ctr_NLKN_cancel").click(function () {
            CancelDataNLKNToStorage();
            if ($("#content-NLKN-saved label").length > 0) {
                $("#placeholder-NLKN").removeClass("display-normal").addClass("display-none");
                $("#content-NLKN-saved").removeClass("display-none").addClass("display-inline-grid");
            } else {
                $("#placeholder-NLKN").removeClass("display-none").addClass("display-normal");
                $("#content-NLKN-saved").removeClass("display-inline-grid").addClass("display-none");
            }

            $("#ctr_NLKN").addClass("bs-transaction");
            $("#ctr_NLKN").removeClass("bs-transaction-edit");
            $("#btn_ctr_NLKN").removeClass("hide");
        })

        $("#btn_ctr_NLKN_save").click(function () {
            if ($('#div-chk-NumberYearActivity .icheckbox_square-green').hasClass("checked") === true
                && $('#txt-NumberYearActivity').val() === "") {
                toastr.error("Số năm hoạt động không được để trống khi đã chọn.");
                return;
            }

            if ($('#div-chk-NumberSimilarContract .icheckbox_square-green').hasClass("checked") === true
                && $('#txt-NumberSimilarContract').val() === "") {
                toastr.error("Số lượng các hợp đồng tương tự đã thực hiện không được để trống khi đã chọn.");
                return;
            }

            if (($('#div-chk-IsContract .icheckbox_square-green').hasClass("checked") === true
                || $('#div-chk-IsLiquidation .icheckbox_square-green').hasClass("checked") === true
                || $('#div-chk-IsBuildingPermit .icheckbox_square-green').hasClass("checked") === true)
                && ($('#div-chk-NumberYearActivity .icheckbox_square-green').hasClass("checked") === false
                    && $('#div-chk-NumberSimilarContract .icheckbox_square-green').hasClass("checked") === false)) {
                toastr.error("Không thể yêu cầu tài liệu chứng minh khi số năm hoạt động hoặc số lượng hợp đồng tương tự không được chọn");
                return;
            }

            SaveDataNLKNToStorage();
            $("#ctr_NLKN").addClass("bs-transaction");
            $("#ctr_NLKN").removeClass("bs-transaction-edit");
            $("#btn_ctr_NLKN").removeClass("hide");
        })
    }

    function CheckEnableNumberYearActivity() {
        if ($('#div-chk-NumberYearActivity .icheckbox_square-green').hasClass("checked") === true) {
            $('#txt-NumberYearActivity').prop('disabled', false);
            $('#txt-NumberYearActivity').val('1');
            $('#chk-NumberYearActivity').prop('checked', true);
        } else {
            $('#txt-NumberYearActivity').prop('disabled', true);
            $('#chk-NumberYearActivity').prop('checked', false);
            $('#txt-NumberYearActivity').val('');
        }
    }

    function CheckEnableNumberSimilarContract() {
        if ($('#div-chk-NumberSimilarContract .icheckbox_square-green').hasClass("checked") === true) {
            $('#txt-NumberSimilarContract').prop('disabled', false);
            $('#chk-NumberSimilarContract').prop('checked', true);
        } else {
            $('#txt-NumberSimilarContract').prop('disabled', true);
            $('#chk-NumberSimilarContract').prop('checked', false);
            $('#txt-NumberSimilarContract').val('');
        }
    }

    function CheckEnableNumberYearActivityLabel() {
        if ($('#div-chk-NumberYearActivity .icheckbox_square-green').hasClass("checked") === true) {
            $('#txt-NumberYearActivity').prop('disabled', true);
            $('#txt-NumberYearActivity').val('');
        } else {
            $('#txt-NumberYearActivity').prop('disabled', false);
            $('#txt-NumberYearActivity').val('1');
        }
    }

    function CheckEnableNumberSimilarContractLabel() {
        if ($('#div-chk-NumberSimilarContract .icheckbox_square-green').hasClass("checked") === true) {
            $('#txt-NumberSimilarContract').prop('disabled', true);
            $('#txt-NumberSimilarContract').val('');
        } else {
            $('#txt-NumberSimilarContract').prop('disabled', false);
        }
    }

    function CancelDataNLKNToStorage() {
        $('#div-chk-NumberYearActivity .icheckbox_square-green').removeClass('checked');
        $('#txt-NumberYearActivity').val('');
        $('#txt-NumberYearActivity').prop('disabled', true);

        $('#div-chk-NumberSimilarContract .icheckbox_square-green').removeClass('checked');
        $('#txt-NumberSimilarContract').val('');
        $('#txt-NumberSimilarContract').prop('disabled', true);

        $('#chk-IsContract').prop('checked', false);
        $('#div-chk-IsContract .icheckbox_square-green').removeClass('checked');

        $('#chk-IsLiquidation').prop('checked', false);
        $('#div-chk-IsLiquidation .icheckbox_square-green').removeClass('checked');

        $('#chk-IsBuildingPermit').prop('checked', false);
        $('#div-chk-IsBuildingPermit .icheckbox_square-green').removeClass('checked');
    }

    function SaveDataNLKNToStorage() {
        //set data to object
        var dataNLKN = {
            NumberYearActivity: $('#txt-NumberYearActivity').val(),
            NumberSimilarContract: $('#txt-NumberSimilarContract').val(),
            IsNumberYearActivity: $('#chk-NumberYearActivity').is(":checked"),
            IsNumberSimilarContract: $('#chk-NumberSimilarContract').is(":checked"),
            IsContractNLKN: $('#chk-IsContract').is(":checked"),
            IsLiquidation: $('#chk-IsLiquidation').is(":checked"),
            IsBuildingPermit: $('#chk-IsBuildingPermit').is(":checked")
        }

        // save data to storage
        window.sessionStorage.setItem("dataNLKN", JSON.stringify(dataNLKN)); //Convert it to JSON

        displaydataNLKNstorage(dataNLKN);
    }

    function LoadDataNLKNToStorage() {
        //get data from storage, convert to json
        var dataNLKN = JSON.parse(window.sessionStorage.getItem("dataNLKN")); //Convert it to JSON
        if (dataNLKN !== null) {
            $('#chk-NumberYearActivity').prop('checked', dataNLKN.IsNumberYearActivity);
            if (dataNLKN.IsNumberYearActivity === false) {
                $('#div-chk-NumberYearActivity .icheckbox_square-green').removeClass('checked');
                $('#txt-NumberYearActivity').val('');
                $('#txt-NumberYearActivity').prop('disabled', true);
            } else {
                $('#div-chk-NumberYearActivity .icheckbox_square-green').addClass('checked');
                $('#txt-NumberYearActivity').val(dataNLKN.NumberYearActivity);
                $('#txt-NumberYearActivity').prop('disabled', false);
            }

            $('#chk-NumberSimilarContract').prop('checked', dataNLKN.IsNumberSimilarContract);
            if (dataNLKN.IsNumberSimilarContract === false) {
                $('#div-chk-NumberSimilarContract .icheckbox_square-green').removeClass('checked');
                $('#txt-NumberSimilarContract').val('');
                $('#txt-NumberSimilarContract').prop('disabled', true);
            } else {
                $('#div-chk-NumberSimilarContract .icheckbox_square-green').addClass('checked');
                $('#txt-NumberSimilarContract').val(dataNLKN.NumberSimilarContract);
                $('#txt-NumberSimilarContract').prop('disabled', false);
            }

            $('#chk-IsContract').prop('checked', dataNLKN.IsContractNLKN);
            dataNLKN.IsContractNLKN === false ? $('#div-chk-IsContract .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsContract .icheckbox_square-green').addClass('checked');

            $('#chk-IsLiquidation').prop('checked', dataNLKN.IsLiquidation);
            dataNLKN.IsLiquidation === false ? $('#div-chk-IsLiquidation .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsLiquidation .icheckbox_square-green').addClass('checked');

            $('#chk-IsBuildingPermit').prop('checked', dataNLKN.IsBuildingPermit);
            dataNLKN.IsBuildingPermit === false ? $('#div-chk-IsBuildingPermit .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsBuildingPermit .icheckbox_square-green').addClass('checked');
        }
    }

    function displaydataNLKNstorage(dataNLKN) {
        var content = "";
        $("#content-NLKN-saved").empty();
        if (dataNLKN != null) {
            $("#placeholder-NLKN").removeClass("display-normal").addClass("display-none");
            if (dataNLKN.IsNumberYearActivity === true) {
                content += "<label>- Số năm hoạt động trong lĩnh vực mời thầu: " + dataNLKN.NumberYearActivity + " năm.</label>";
            }

            if (dataNLKN.IsNumberSimilarContract === true) {
                content += "<label>- Số lượng các hợp đồng tương tự đã thực hiện trong những năm qua: " + dataNLKN.NumberSimilarContract + " hợp đồng.</label>";
            }

            if (dataNLKN.IsContractNLKN === true || dataNLKN.IsLiquidation === true || dataNLKN.IsBuildingPermit === true) {
                content += "<br /><label>Tài liệu chứng minh:</label>";
                if (dataNLKN.IsContractNLKN === true) {
                    content += "<label>- Hợp đồng.</label>";
                }

                if (dataNLKN.IsLiquidation === true) {
                    content += "<label>- Bảng thanh lý hợp đồng hoặc biên bản nghiệm thu bàn giao công trình.</label>";
                }

                if (dataNLKN.IsBuildingPermit === true) {
                    content += "<label>- Giấy phép xây dựng hoặc văn bản phê duyệt dự án.</label>";
                }
            }

            if (content !== "") {
                $("#content-NLKN-saved").append(content);
                $("#content-NLKN-saved").removeClass("display-none").addClass("display-inline-grid");
            } else {
                $("#placeholder-NLKN").removeClass("display-none").addClass("display-normal");
            }
        } else {
            $("#placeholder-NLKN").removeClass("display-none").addClass("display-normal");
        }
    }

    return {
        initAbilityExp: initAbilityExp,
        CheckEnableNumberYearActivity: CheckEnableNumberYearActivity,
        CheckEnableNumberYearActivityLabel: CheckEnableNumberYearActivityLabel,
        CheckEnableNumberSimilarContractLabel: CheckEnableNumberSimilarContractLabel,
        CheckEnableNumberSimilarContract: CheckEnableNumberSimilarContract
    };
}();