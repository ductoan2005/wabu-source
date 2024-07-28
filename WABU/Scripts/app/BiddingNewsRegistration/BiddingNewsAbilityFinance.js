var abilityFinance = function () {
    function initAbilityFinance() {
        $("#btn_ctr_NLTC").click(function () {
            LoadDataNLTCToStorage();
            $("#placeholder-NLTC").removeClass("display-none").addClass("display-normal");
            $("#content-NLTC-saved").removeClass("display-inline-grid").addClass("display-none");

            $("#ctr_NLTC").removeClass("bs-transaction");
            $("#ctr_NLTC").addClass("bs-transaction-edit");
            $("#ctr_NLTC").addClass("bs-transaction-edit");
            $("#btn_ctr_NLTC").addClass("hide");
            $('#div-chk-IsTurnover2Year .iCheck-helper').attr('onclick', 'abilityFinance.CheckEnableTurnover2Year();');
            $('#div-chk-IsFinanceSituation .iCheck-helper').attr('onclick', 'abilityFinance.CheckEnableFinanceSituation();');
        })
        $("#btn_ctr_NLTC_cancel").click(function () {
            CancelDataNLTCToStorage();
            if ($("#content-NLTC-saved label").length > 0) {
                $("#placeholder-NLTC").removeClass("display-normal").addClass("display-none");
                $("#content-NLTC-saved").removeClass("display-none").addClass("display-inline-grid");
            } else {
                $("#placeholder-NLTC").removeClass("display-none").addClass("display-normal");
                $("#content-NLTC-saved").removeClass("display-inline-grid").addClass("display-none");
            }

            $("#ctr_NLTC").addClass("bs-transaction");
            $("#ctr_NLTC").removeClass("bs-transaction-edit");
            $("#btn_ctr_NLTC").removeClass("hide");
        })

        $("#btn_ctr_NLTC_save").click(function () {
            if ($('#div-chk-IsTurnover2Year .icheckbox_square-green').hasClass("checked") === true) {
                var numYear = $('#txt-NumYearOfTurnover').val();

                if (numYear === "") {
                    toastr.error("Số năm Doanh thu trung bình không được để trống khi đã chọn.");
                    return;
                } else if ($('#txt-Turnover2Year').val() === "") {
                    toastr.error("Doanh thu trung bình trong " + numYear + " năm không được để trống khi đã chọn.");
                    return;
                }
            }

            if ($('#div-chk-IsFinanceSituation .icheckbox_square-green').hasClass("checked") === true && $('#txt-NumYearFinanceSituation').val() === "") {
                toastr.error("Số năm tài chính không được để trống khi đã chọn.");
                return;
            }

            if (($('#div-chk-IsTurnover2Year .icheckbox_square-green').hasClass("checked") === false
                && $('#div-chk-IsFinanceSituation .icheckbox_square-green').hasClass("checked") === false) && (
                    $('#div-chk-IsProtocol .icheckbox_square-green').hasClass("checked") === true
                    || $('#div-chk-IsDeclaration .icheckbox_square-green').hasClass("checked") === true
                    || $('#div-chk-IsDocument .icheckbox_square-green').hasClass("checked") === true
                    || $('#div-chk-IsReport .icheckbox_square-green').hasClass("checked") === true
                )) {
                toastr.error("Không thể yêu cầu tài liệu chứng minh khi doanh thu hoặc tình hình tài chính không được chọn.");
                return;
            }

            SaveDataNLTCToStorage();
            $("#ctr_NLTC").addClass("bs-transaction");
            $("#ctr_NLTC").removeClass("bs-transaction-edit");
            $("#btn_ctr_NLTC").removeClass("hide");
        })
    };

    function CheckEnableTurnover2Year() {
        if ($('#div-chk-IsTurnover2Year .icheckbox_square-green').hasClass("checked") === true) {
            $('#txt-Turnover2Year').prop('disabled', false);
            $('#chk-IsTurnover2Year').prop('checked', true);
            $('#txt-NumYearOfTurnover').prop('disabled', false);
        } else {
            $('#txt-Turnover2Year').prop('disabled', true);
            $('#chk-IsTurnover2Year').prop('checked', false);
            $('#txt-Turnover2Year').val('');
            $('#txt-NumYearOfTurnover').prop('disabled', true);
            $('#txt-NumYearOfTurnover').val('');
        }
    }

    function CheckEnableTurnover2YearLabel() {
        if ($('#div-chk-IsTurnover2Year .icheckbox_square-green').hasClass("checked") === true) {
            $('#txt-Turnover2Year').prop('disabled', true);
            $('#txt-Turnover2Year').val('');
            $('#txt-NumYearOfTurnover').prop('disabled', true);
            $('#txt-NumYearOfTurnover').val('');
        } else {
            $('#txt-Turnover2Year').prop('disabled', false);
            $('#txt-NumYearOfTurnover').prop('disabled', false);
        }
    }

    function CheckEnableFinanceSituation() {
        if ($('#div-chk-IsFinanceSituation .icheckbox_square-green').hasClass("checked") === true) {
            $('#txt-NumYearFinanceSituation').prop('disabled', false);
        } else {
            $('#txt-NumYearFinanceSituation').prop('disabled', true);
            $('#txt-NumYearFinanceSituation').val('');
        }
    }

    function CheckEnableFinanceSituationLabel() {
        if ($('#div-chk-IsFinanceSituation .icheckbox_square-green').hasClass("checked") === true) {
            $('#txt-NumYearFinanceSituation').prop('disabled', true);
            $('#txt-NumYearFinanceSituation').val('');
        } else {
            $('#txt-NumYearFinanceSituation').prop('disabled', false);
        }
    }

    function CancelDataNLTCToStorage() {
        $('#div-chk-IsTurnover2Year .icheckbox_square-green').removeClass('checked');
        $('#txt-Turnover2Year').val('');
        $('#txt-Turnover2Year').prop('disabled', true);
        $('#txt-NumYearOfTurnover').val('');
        $('#txt-NumYearOfTurnover').prop('disabled', true);

        $('#chk-IsFinanceSituation').prop('checked', false);
        $('#txt-NumYearFinanceSituation').val('');
        $('#txt-NumYearFinanceSituation').prop('checked', true);
        $('#div-chk-IsFinanceSituation .icheckbox_square-green').removeClass('checked');

        $('#chk-IsProtocol').prop('checked', false);
        $('#div-chk-IsProtocol .icheckbox_square-green').removeClass('checked');

        $('#chk-IsDeclaration').prop('checked', false);
        $('#div-chk-IsDeclaration .icheckbox_square-green').removeClass('checked');

        $('#chk-IsDocument').prop('checked', false);
        $('#div-chk-IsDocument .icheckbox_square-green').removeClass('checked');

        $('#chk-IsReport').prop('checked', false);
        $('#div-chk-IsReport .icheckbox_square-green').removeClass('checked');
    }

    function SaveDataNLTCToStorage() {
        //set data to object
        var dataNLTC = {
            Turnover2Year: $('#txt-Turnover2Year').val(),
            IsTurnover2Year: $('#chk-IsTurnover2Year').is(":checked"),
            NumYearOfTurnover: $('#txt-NumYearOfTurnover').val(),
            IsFinanceSituation: $('#chk-IsFinanceSituation').is(":checked"),
            NumYearFinanceSituation: $('#txt-NumYearFinanceSituation').val(),
            IsProtocol: $('#chk-IsProtocol').is(":checked"),
            IsDeclaration: $('#chk-IsDeclaration').is(":checked"),
            IsDocument: $('#chk-IsDocument').is(":checked"),
            IsReport: $('#chk-IsReport').is(":checked"),
        }

        // save data to storage
        window.sessionStorage.setItem("dataNLTC", JSON.stringify(dataNLTC)); //Convert it to JSON

        displaydataNLTCstorage(dataNLTC);
    }

    function LoadDataNLTCToStorage() {
        //get data from storage, convert to json
        var dataNLTC = JSON.parse(window.sessionStorage.getItem("dataNLTC")); //Convert it to JSON
        if (dataNLTC !== null) {
            $('#chk-IsTurnover2Year').prop('checked', dataNLTC.IsTurnover2Year);
            if (dataNLTC.IsTurnover2Year === false) {
                $('#div-chk-IsTurnover2Year .icheckbox_square-green').removeClass('checked');
                $('#txt-Turnover2Year').val('');
                $('#txt-Turnover2Year').prop('disabled', true);
            } else {
                $('#div-chk-IsTurnover2Year .icheckbox_square-green').addClass('checked');
                $('#txt-Turnover2Year').val(dataNLTC.Turnover2Year);
                $('#txt-NumYearOfTurnover').val(dataNLTC.NumYearOfTurnover);
                $('#txt-Turnover2Year').prop('disabled', false);
            }


            $('#chk-IsFinanceSituation').prop('checked', dataNLTC.IsFinanceSituation);
            if (dataNLTC.IsFinanceSituation === false) {
                $('#div-chk-IsFinanceSituation .icheckbox_square-green').removeClass('checked')
            }
            else {
                $('#div-chk-IsFinanceSituation .icheckbox_square-green').addClass('checked');
                $('#txt-NumYearFinanceSituation').val(dataNLTC.NumYearFinanceSituation);
            }

            $('#chk-IsProtocol').prop('checked', dataNLTC.IsProtocol);
            dataNLTC.IsProtocol === false ? $('#div-chk-IsProtocol .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsProtocol .icheckbox_square-green').addClass('checked');

            $('#chk-IsDeclaration').prop('checked', dataNLTC.IsDeclaration);
            dataNLTC.IsDeclaration === false ? $('#div-chk-IsDeclaration .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsDeclaration .icheckbox_square-green').addClass('checked');

            $('#chk-IsDocument').prop('checked', dataNLTC.IsDocument);
            dataNLTC.IsDocument === false ? $('#div-chk-IsDocument .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsDocument .icheckbox_square-green').addClass('checked');

            $('#chk-IsReport').prop('checked', dataNLTC.IsReport);
            dataNLTC.IsReport === false ? $('#div-chk-IsReport .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsReport .icheckbox_square-green').addClass('checked');
        }
    }

    function displaydataNLTCstorage(dataNLTC) {
        var content = "";
        $("#content-NLTC-saved").empty();
        if (dataNLTC !== null) {
            $("#placeholder-NLTC").removeClass("display-normal").addClass("display-none");
            if (dataNLTC.IsTurnover2Year === true) {
                content += "<label>- Doanh thu trung bình trong " + dataNLTC.NumYearOfTurnover + " năm gần nhất >= " + dataNLTC.Turnover2Year.replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " VNĐ.</label>";
            }

            if (dataNLTC.IsFinanceSituation === true) {
                content += "<label>- Tình hình tài chính trong " + dataNLTC.NumYearFinanceSituation + " năm gần nhất không bị lỗ, có xác nhận hợp lệ của cơ quan tài chính cấp trên hoặc kiểm toán độc lập.</label>";
            }

            if (dataNLTC.IsProtocol === true
                || dataNLTC.IsDeclaration === true
                || dataNLTC.IsDocument === true
                || dataNLTC.IsReport === true) {
                content += "<br /><label>Tài liệu chứng minh:</label>";
                if (dataNLTC.IsProtocol === true) {
                    content += "<label>- Biên bản kiểm tra quyết toán của nhà thầu trong các năm tài chính nêu trên.</label>";
                }

                if (dataNLTC.IsDeclaration === true) {
                    content += "<label>- Tờ khai tự quyết toán thuế (thuế GTGT và thuế thu nhập doanh nghiệp), có xác nhận của cơ quan thuế về thời điểm đã nộp tờ khai trong các năm tài chính nêu trên.</label>";
                }

                if (dataNLTC.IsDocument === true) {
                    content += "<label>- Văn bản xác nhận của cơ quan quản lý thuế (xác nhận nộp cả năm) về việc thực hiện nghĩa vụ nộp thuế trong các năm tài chính nêu trên.</label>";
                }

                if (dataNLTC.IsReport === true) {
                    content += "<label>- Báo cáo kiểm toán.</label>";
                }
            }

            if (content !== "") {
                $("#content-NLTC-saved").append(content);
                $("#content-NLTC-saved").removeClass("display-none").addClass("display-inline-grid");
            } else {
                $("#placeholder-NLTC").removeClass("display-none").addClass("display-normal");
            }
        } else {
            $("#placeholder-NLTC").removeClass("display-none").addClass("display-normal");
        }
    }

    return {
        initAbilityFinance: initAbilityFinance,
     
        CheckEnableTurnover2Year: CheckEnableTurnover2Year,
        CheckEnableTurnover2YearLabel: CheckEnableTurnover2YearLabel,
        CheckEnableFinanceSituation: CheckEnableFinanceSituation,
        CheckEnableFinanceSituationLabel: CheckEnableFinanceSituationLabel
    }
}();