var validContractor = function () {
    function initContrator() {
        $("#btn_ctr_TCHL").click(function () {
            LoadDataTCHLToStorage();
            $("#placeholder-TCHL").removeClass("display-none").addClass("display-normal");
            $("#content-TCHL-saved").removeClass("display-inline-grid").addClass("display-none");

            $("#ctr_TCHL").removeClass("bs-transaction");
            $("#ctr_TCHL").addClass("bs-transaction-edit");
            $("#ctr_TCHL").addClass("bs-transaction-edit");
            $("#btn_ctr_TCHL").addClass("hide");
        });

        $("#btn_ctr_TCHL_cancel").click(function () {
            CancelDataTCHLToStorage();
            if ($("#content-TCHL-saved label").length > 0) {
                $("#placeholder-TCHL").removeClass("display-normal").addClass("display-none");
                $("#content-TCHL-saved").removeClass("display-none").addClass("display-inline-grid");
            } else {
                $("#placeholder-TCHL").removeClass("display-none").addClass("display-normal");
                $("#content-TCHL-saved").removeClass("display-inline-grid").addClass("display-none");
            }

            $("#ctr_TCHL").addClass("bs-transaction");
            $("#ctr_TCHL").removeClass("bs-transaction-edit");
            $("#btn_ctr_TCHL").removeClass("hide");
        });

        $("#btn_ctr_TCHL_save").click(function () {
            SaveDataTCHLToStorage();
            $("#ctr_TCHL").addClass("bs-transaction");
            $("#ctr_TCHL").removeClass("bs-transaction-edit");
            $("#btn_ctr_TCHL").removeClass("hide");
        });
    }

    function CancelDataTCHLToStorage() {
        $('#chk-IsRegisEstablishment').prop('checked', false);
        $('#div-chk-IsRegisEstablishment .icheckbox_square-green').removeClass('checked');
        $('#chk-IsFinancial').prop('checked', false);
        $('#div-chk-IsFinancial .icheckbox_square-green').removeClass('checked');
        $('#chk-IsDissolutionProcess').prop('checked', false);
        $('#div-chk-IsDissolutionProcess .icheckbox_square-green').removeClass('checked');
        $('#chk-IsBankrupt').prop('checked', false);
        $('#div-chk-IsBankrupt .icheckbox_square-green').removeClass('checked');
    }

    function SaveDataTCHLToStorage() {
        //set data to object
        var dataTCHL = {
            IsRegisEstablishment: $('#chk-IsRegisEstablishment').is(":checked"),
            IsFinancial: $('#chk-IsFinancial').is(":checked"),
            IsDissolutionProcess: $('#chk-IsDissolutionProcess').is(":checked"),
            IsBankrupt: $('#chk-IsBankrupt').is(":checked")
        }

        // save data to storage
        window.sessionStorage.setItem("dataTCHL", JSON.stringify(dataTCHL)); //Convert it to JSON

        displaydataTCHLstorage(dataTCHL);
    }

    function LoadDataTCHLToStorage() {
        //get data from storage, convert to json
        var dataTCHL = JSON.parse(window.sessionStorage.getItem("dataTCHL")); //Convert it to JSON
        if (dataTCHL != null) {
            $('#chk-IsRegisEstablishment').prop('checked', dataTCHL.IsRegisEstablishment);
            dataTCHL.IsRegisEstablishment == false ? $('#div-chk-IsRegisEstablishment .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsRegisEstablishment .icheckbox_square-green').addClass('checked');

            $('#chk-IsFinancial').prop('checked', dataTCHL.IsFinancial);
            dataTCHL.IsFinancial == false ? $('#div-chk-IsFinancial .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsFinancial .icheckbox_square-green').addClass('checked');

            $('#chk-IsDissolutionProcess').prop('checked', dataTCHL.IsDissolutionProcess);
            dataTCHL.IsDissolutionProcess == false ? $('#div-chk-IsDissolutionProcess .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsDissolutionProcess .icheckbox_square-green').addClass('checked');

            $('#chk-IsBankrupt').prop('checked', dataTCHL.IsBankrupt);
            dataTCHL.IsBankrupt == false ? $('#div-chk-IsBankrupt .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsBankrupt .icheckbox_square-green').addClass('checked');
        }
    }

    function displaydataTCHLstorage(dataTCHL) {
        var content = "";
        $("#content-TCHL-saved").empty();
        if (dataTCHL != null) {
            $("#placeholder-TCHL").removeClass("display-normal").addClass("display-none");
            if (dataTCHL.IsRegisEstablishment == true) {
                content += "<label>- Có đăng ký thành lập, hoạt động do cơ quan có thẩm quyền của nước mà nhà thầu, nhà đầu tư đang hoạt động cấp.</label>";
            }

            if (dataTCHL.IsFinancial == true) {
                content += "<label>- Hạch toán tài chính độc lập.</label>";
            }

            if (dataTCHL.IsDissolutionProcess == true) {
                content += "<label>- Không đang trong quá trình giải thể.</label>";
            }

            if (dataTCHL.IsBankrupt == true) {
                content += "<label>- Không bị kết luận đang lâm vào tình trạng phá sản hoặc nợ không có khả năng chi trả theo quy định của pháp luật.</label>";
            }

            if (content != "") {
                $("#content-TCHL-saved").append(content);
                $("#content-TCHL-saved").removeClass("display-none").addClass("display-inline-grid");
            } else {
                $("#placeholder-TCHL").removeClass("display-none").addClass("display-normal");
            }
        } else {
            $("#placeholder-TCHL").removeClass("display-none").addClass("display-normal");
        }
    }

    return {
        initContrator: initContrator,
    };
}();