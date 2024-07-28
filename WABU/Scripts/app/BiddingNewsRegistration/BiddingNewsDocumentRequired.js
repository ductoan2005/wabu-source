var documentRequired = function () {
    function initDocumentRequired() {
        $("#btn_ctr_MKT").click(function () {
            LoadDataMKTToStorage();
            $("#placeholder-MKT").removeClass("display-none").addClass("display-normal");
            $("#content-MKT-saved").removeClass("display-inline-grid").addClass("display-none");

            $("#ctr_MKT").removeClass("bs-transaction");
            $("#ctr_MKT").addClass("bs-transaction-edit");
            $("#ctr_MKT").addClass("bs-transaction-edit");
            $("#btn_ctr_MKT").addClass("hide");
            $('#div-chk-IsTechnicalOther .iCheck-helper').attr('onclick', 'documentRequired.CheckEnableIsTechnicalOther();');

            if (dataMKT.listMKT < 1) {
                $('#txt-TechnicalOther').prop('disabled', true);
            }
        });

        $("#btn_ctr_MKT_cancel").click(function () {
            CancelDataMKTToStorage();
            if ($("#content-MKT-saved label").length > 0) {
                $("#placeholder-MKT").removeClass("display-normal").addClass("display-none");
                $("#content-MKT-saved").removeClass("display-none").addClass("display-inline-grid");
            } else {
                $("#placeholder-MKT").removeClass("display-none").addClass("display-normal");
                $("#content-MKT-saved").removeClass("display-inline-grid").addClass("display-none");
            }

            $("#ctr_MKT").addClass("bs-transaction");
            $("#ctr_MKT").removeClass("bs-transaction-edit");
            $("#btn_ctr_MKT").removeClass("hide");
        });

        $("#btn_ctr_MKT_save").click(function () {
            SaveDataMKTToStorage();
            $('#ctr_MKT #btn-accept-row-MKT').removeClass('display-normal').addClass('display-none');

            $("#ctr_MKT").addClass("bs-transaction");
            $("#ctr_MKT").removeClass("bs-transaction-edit");
            $("#btn_ctr_MKT").removeClass("hide");
        });
    }

    // variable common
    var dataMKT = { listMKT: [] };
    function CheckEnableIsTechnicalOther() {
        if ($('#div-chk-IsTechnicalOther .icheckbox_square-green').hasClass("checked") == true) {
            $('#txt-TechnicalOther').prop('disabled', false);
            $('#chk-IsTechnicalOther').prop('checked', true);
            //$('#ctr_MKT #btn-accept-row-MKT').removeClass('display-none').addClass('display-normal');
            $('#btn-accept-row-MKT').removeClass('display-none').addClass('display-normal');
        } else {
            $('#txt-TechnicalOther').prop('disabled', true);
            $('#chk-IsTechnicalOther').prop('checked', false);
            $('#txt-TechnicalOther').val('');
            //$('#ctr_MKT #btn-accept-row-MKT').removeClass('display-normal').addClass('display-none');
            $('#btn-accept-row-MKT').removeClass('display-normal').addClass('display-none');
            dataMKT.listMKT = [];
            $('#div-show-TechnicalOther').empty();
        }
    }

    var idItem = 0;
    function AddTechnicalOther(e) {
        e.preventDefault();
        $("#txt-TechnicalOther").focus();

        var idStr = "";

        idItem++;
        idStr = "item_" + idItem;

        var strItem = $('#txt-TechnicalOther').val();
        $('#txt-TechnicalOther').val('');
        if (strItem !== "") {
            var strHtml = "<div class='row' style='margin-top: 5px;' id='" + idStr + "'><div style='float: left;'><a href='#' id='btn-cancel-row-MKT' onclick='documentRequired.DeleteTechnicalOther(\"" + idStr + "\", event);' class='display-normal'><i class='pull-left fa fa-minus-square font_30 color-red'></i></a></div><div style='margin-top: 6px; float: left;'>" + strItem + "</div></div>"
            $('#div-show-TechnicalOther').append(strHtml);
            dataMKT.listMKT.push(
                {
                    id: idStr,
                    TechnicalOtherName: strItem
                });
        }

        window.sessionStorage.setItem("dataMKT", JSON.stringify(dataMKT)); //Convert it to JSON
    }

    function DeleteTechnicalOther(idStr, e) {
        e.preventDefault();

        $("#div-show-TechnicalOther div#" + idStr).empty();

        var data = $.grep(dataMKT.listMKT, function (e) {
            return e.id != idStr;
        });

        dataMKT.listMKT = data;

        //for (var i = 0; i < dataMKT.listMKT.length; i++) {
        //    var idTemp = dataMKT.listMKT[i].id;

        //    if (idTemp === idStr) {
        //        $("#div-show-TechnicalOther #" + idTemp).remove();
        //        dataMKT.listMKT.splice(i, 1);
        //    }
        //}

        if (dataMKT.listMKT.length == 0) {
            $("#div-show-TechnicalOther").empty();
        }
    }

    function CancelDataMKTToStorage() {
        $('#chk-IsProgressSchedule').prop('checked', false);
        $('#div-chk-IsProgressSchedule .icheckbox_square-green').removeClass('checked');

        $('#chk-IsQuotation').prop('checked', false);
        $('#div-chk-IsQuotation .icheckbox_square-green').removeClass('checked');

        $('#chk-IsMaterialsUse').prop('checked', false);
        $('#div-chk-IsMaterialsUse .icheckbox_square-green').removeClass('checked');

        $('#chk-IsDrawingConstruction').prop('checked', false);
        $('#div-chk-IsDrawingConstruction .icheckbox_square-green').removeClass('checked');

        $('#chk-IsWorkSafety').prop('checked', false);
        $('#div-chk-IsWorkSafety .icheckbox_square-green').removeClass('checked');

        $('#chk-IsEnvironmentalSanitation').prop('checked', false);
        $('#div-chk-IsEnvironmentalSanitation .icheckbox_square-green').removeClass('checked');

        $('#chk-IsFireProtection').prop('checked', false);
        $('#div-chk-IsFireProtection .icheckbox_square-green').removeClass('checked');

        $("#div-show-TechnicalOther").empty();
        $('#txt-TechnicalOther').prop('disabled', true);
        $('#chk-IsTechnicalOther').prop('checked', false);
        $('#txt-TechnicalOther').val('');
        $('#div-chk-IsTechnicalOther .icheckbox_square-green').removeClass('checked');
        $('#ctr_MKT #btn-accept-row-MKT').removeClass('display-normal').addClass('display-none');
    }

    function SaveDataMKTToStorage() {
        //set data to object
        dataMKT.IsProgressSchedule = $('#chk-IsProgressSchedule').is(":checked");
        dataMKT.IsQuotation = $('#chk-IsQuotation').is(":checked");
        dataMKT.IsMaterialsUse = $('#chk-IsMaterialsUse').is(":checked");
        dataMKT.IsDrawingConstruction = $('#chk-IsDrawingConstruction').is(":checked");
        dataMKT.IsWorkSafety = $('#chk-IsWorkSafety').is(":checked");
        dataMKT.IsEnvironmentalSanitation = $('#chk-IsEnvironmentalSanitation').is(":checked");
        dataMKT.IsFireProtection = $('#chk-IsFireProtection').is(":checked");

        // save data to storage
        window.sessionStorage.setItem("dataMKT", JSON.stringify(dataMKT)); //Convert it to JSON

        displaydataMKTstorage(dataMKT);
    }

    function LoadDataMKTToStorage() {
        $('#txt-TechnicalOther').val('');
        var dataMKTTemp = JSON.parse(window.sessionStorage.getItem("dataMKT")); //Convert it to JSON
        if (dataMKTTemp != null) {
            $('#chk-IsProgressSchedule').prop('checked', dataMKTTemp.IsProgressSchedule);
            dataMKTTemp.IsProgressSchedule == false ? $('#div-chk-IsProgressSchedule .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsProgressSchedule .icheckbox_square-green').addClass('checked');

            $('#chk-IsQuotation').prop('checked', dataMKTTemp.IsQuotation);
            dataMKTTemp.IsQuotation == false ? $('#div-chk-IsQuotation .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsQuotation .icheckbox_square-green').addClass('checked');

            $('#chk-IsMaterialsUse').prop('checked', dataMKTTemp.IsMaterialsUse);
            dataMKTTemp.IsMaterialsUse == false ? $('#div-chk-IsMaterialsUse .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsMaterialsUse .icheckbox_square-green').addClass('checked');

            $('#chk-IsDrawingConstruction').prop('checked', dataMKTTemp.IsDrawingConstruction);
            dataMKTTemp.IsDrawingConstruction == false ? $('#div-chk-IsDrawingConstruction .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsDrawingConstruction .icheckbox_square-green').addClass('checked');

            $('#chk-IsWorkSafety').prop('checked', dataMKTTemp.IsWorkSafety);
            dataMKTTemp.IsWorkSafety == false ? $('#div-chk-IsWorkSafety .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsWorkSafety .icheckbox_square-green').addClass('checked');

            $('#chk-IsEnvironmentalSanitation').prop('checked', dataMKTTemp.IsEnvironmentalSanitation);
            dataMKTTemp.IsEnvironmentalSanitation == false ? $('#div-chk-IsEnvironmentalSanitation .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsEnvironmentalSanitation .icheckbox_square-green').addClass('checked');

            $('#chk-IsFireProtection').prop('checked', dataMKTTemp.IsFireProtection);
            dataMKTTemp.IsFireProtection == false ? $('#div-chk-IsFireProtection .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsFireProtection .icheckbox_square-green').addClass('checked');

            $("#div-show-TechnicalOther").empty();
            if (dataMKTTemp.listMKT.length == 0) {
                $('#chk-IsTechnicalOther').prop('checked', false);
                $('#div-chk-IsTechnicalOther .icheckbox_square-green').removeClass('checked');
            }
            else {
                var contentItem = "";
                for (var i = 0; i < dataMKTTemp.listMKT.length; i++) {
                    var contentItemId = dataMKTTemp.listMKT[i].id;
                    var contentItemStr = dataMKTTemp.listMKT[i].TechnicalOtherName;
                    contentItem += "<div class='row' style='margin-top: 5px;' id='" + contentItemId + "'><div style='float: left;'><a href='#' id='btn-cancel-row-MKT' onclick='documentRequired.DeleteTechnicalOther(\"" + contentItemId + "\", event);' class='display-normal'><i class='pull-left fa fa-minus-square font_30 color-red'></i></a></div><div style='margin-top: 6px; float: left;'>" + contentItemStr + "</div></div>"
                }

                $('#txt-TechnicalOther').prop('disabled', false);
                $('#chk-IsTechnicalOther').prop('checked', true);
                $('#div-chk-IsTechnicalOther .icheckbox_square-green').addClass('checked');
                $('#ctr_MKT #btn-accept-row-MKT').removeClass('display-none').addClass('display-normal');
                $('#div-show-TechnicalOther').append(contentItem);
            }

            dataMKT = dataMKTTemp;
        }
    }

    function displaydataMKTstorage(dataMKT) {
        var content = "";
        $("#content-MKT-saved").empty();
        if (dataMKT != null) {
            $("#placeholder-MKT").removeClass("display-normal").addClass("display-none");
            //if (dataMKT.IsProgressSchedule == true) {
            //    content += "<label>- Bảng tiến độ thực hiện.</label>";
            //}

            if (dataMKT.IsQuotation == true) {
                content += "<label>- Bảng báo giá.</label>";
            }

            if (dataMKT.IsMaterialsUse == true) {
                content += "<label>- Bảng đề xuất vật tư sử dụng.</label>";
            }

            //if (dataMKT.IsDrawingConstruction == true) {
            //    content += "<label>- Bản vẽ và thuyết minh biện pháp thi công.</label>";
            //}

            //if (dataMKT.IsWorkSafety == true) {
            //    content += "<label>- Thuyết minh phương án đảm bảo an toàn lao động.</label>";
            //}

            //if (dataMKT.IsEnvironmentalSanitation == true) {
            //    content += "<label>- Thuyết minh biện pháp đảm bảo vệ sinh môi trường.</label>";
            //}

            //if (dataMKT.IsFireProtection == true) {
            //    content += "<label>- Thuyết minh phòng chống cháy nổ trên công trường.</label>";
            //}

            if (dataMKT.listMKT.length > 0) {
                for (var i = 0; i < dataMKT.listMKT.length; i++) {
                    content += "<label>- " + dataMKT.listMKT[i].TechnicalOtherName + ".</label>";
                }
            }

            if (content != "") {
                $("#content-MKT-saved").append(content);
                $("#content-MKT-saved").removeClass("display-none").addClass("display-inline-grid");
            } else {
                $("#placeholder-MKT").removeClass("display-none").addClass("display-normal");
            }
        } else {
            $("#placeholder-MKT").removeClass("display-none").addClass("display-normal");
        }
    }

    return {
        initDocumentRequired: initDocumentRequired,
        CheckEnableIsTechnicalOther: CheckEnableIsTechnicalOther,
        AddTechnicalOther: AddTechnicalOther,
        DeleteTechnicalOther: DeleteTechnicalOther
    }
}();
