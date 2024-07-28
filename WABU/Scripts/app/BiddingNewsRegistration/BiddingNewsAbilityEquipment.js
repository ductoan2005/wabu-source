var abilityEquipment = function () {
    function initAbilityEquipment() {
        $("#btn_ctr_NLMM").click(function () {
            LoadDataNLMMToStorage();
            $("#placeholder-NLMM").removeClass("display-none").addClass("display-normal");
            $("#content-NLMM-saved").removeClass("display-inline-grid").addClass("display-none");

            $("#ctr_NLMM").removeClass("bs-transaction");
            $("#ctr_NLMM").addClass("bs-transaction-edit");
            $("#ctr_NLMM").addClass("bs-transaction-edit");
            $("#btn_ctr_NLMM").addClass("hide");
        });

        $("#btn_ctr_NLMM_cancel").click(function () {
            CancelDataNLMMToStorage();
            if ($("#content-NLMM-saved label").length > 0 || $("#content-NLMM-saved table").length > 0) {
                $("#placeholder-NLMM").removeClass("display-normal").addClass("display-none");
                $("#content-NLMM-saved").removeClass("display-none").addClass("display-inline-grid");
            } else {
                $("#placeholder-NLMM").removeClass("display-none").addClass("display-normal");
                $("#content-NLMM-saved").removeClass("display-inline-grid").addClass("display-none");
            }

            $("#ctr_NLMM").addClass("bs-transaction");
            $("#ctr_NLMM").removeClass("bs-transaction-edit");
            $("#btn_ctr_NLMM").removeClass("hide");
        });

        $("#btn_ctr_NLMM_save").click(function () {
            //set data to object
            if (($('#div-chk-IsContract-NLMM .icheckbox_square-green').hasClass("checked") == true
                || $('#div-chk-IsProfile .icheckbox_square-green').hasClass("checked") == true) && dataNLMM.listNLMM.length < 1) {
                toastr.error("Không thể yêu cầu tài liệu chứng minh khi danh sách thiết bị, máy móc trống.");
                return;
            }

            dataNLMM.IsContract = $('#chk-IsContract-NLMM').is(":checked");
            dataNLMM.IsProfile = $('#chk-IsProfile').is(":checked");

            // save data to storage
            window.sessionStorage.setItem("dataNLMM", JSON.stringify(dataNLMM)); //Convert it to JSON

            displaydataNLMMstorage(dataNLMM);

            $("#ctr_NLMM").addClass("bs-transaction");
            $("#ctr_NLMM").removeClass("bs-transaction-edit");
            $("#btn_ctr_NLMM").removeClass("hide");
        })
    }

    // variable common
    var rowIdNLMM = 0;
    var dataNLMM = { listNLMM: [] };
    function AddRowNLMM(e) {
        e.preventDefault();
        if ($('#txt-EquipmentName').val() === "") {
            toastr.error("Vui lòng nhập tên thiết bị muốn thêm!");
            return;
        }

        var jsUrl = '/Scripts/Common/plugins/iCheck/icheck.min.js';
        $.getScript(jsUrl, function () {
            rowIdNLMM++;
            var idRow = "row_NLMM_" + rowIdNLMM;
            var valAccreditation = $('#chk-IsAccreditation').is(":checked");
            var strAccreditation = valAccreditation == true ? "Yêu cầu kiểm định" : "Không y/c kiểm định";
            var trAccept = "<tr id='" + idRow + "' class='text-center'>"
                + "<td><p>" + $('#txt-EquipmentName').val() + "</p></td>"
                + "<td><p>" + $('#txt-PowerEquipment').val() + "</p></td>"
                + "<td><p>" + $('#txt-QuantityEquipment').val() + "</p></td>"
                + "<td><p>" + strAccreditation + "</p></td>"
                + '<td><a href="#" id="btn-delete-row-NLMM" onclick="abilityEquipment.DeleteRowNLMM(\'' + idRow + '\',event);" class="display-normal"><i class="fa fa-minus-square font_25 color-red"></i></a></td></tr > ';

            var trDefault = "<tr id='row-default-NLMM'>"
                + "<td><input type='text' id='txt-EquipmentName' class='form-control' placeholder='Nhập tên thiết bị'/></td>"
                + "<td><input type='text' id='txt-PowerEquipment' class='form-control' placeholder='Nhập C/S thiết bị' /></td>"
                + "<td><input type='number' id='txt-QuantityEquipment' class='form-control' placeholder='...' /></td>"
                + "<td><div class='i-checks text-center pdt5' id='div-chk-IsAccreditation'>"
                + "<input type='checkbox' value='' id='chk-IsAccreditation'>"
                + "</div></td>"
                + "<td class='text-center'><a href='#' id='btn-add-row-NLMM' onclick='abilityEquipment.AddRowNLMM(event);' class='display-normal'><i class='fa fa-plus-square font_25 greenblue'></i></a></td>"
                + "</tr>";

            dataNLMM.listNLMM.push(
                {
                    id: idRow,
                    EquipmentName: $('#txt-EquipmentName').val(),
                    QuantityEquipment: $('#txt-QuantityEquipment').val(),
                    PowerEquipment: $('#txt-PowerEquipment').val(),
                    IsAccreditation: valAccreditation
                });

            // draw tr
            if ($('#tbl-NLMM tbody #row-default-NLMM').length) {
                $('#tbl-NLMM #row-default-NLMM').remove(); //remove tr

                $('#tbl-NLMM tbody').append(trAccept);
                $('#tbl-NLMM tbody').append(trDefault);
            }
        }).done(function () {
            $('#div-chk-IsAccreditation').iCheck({
                checkboxClass: 'icheckbox_square-green',
            });
        });
    }

    function DeleteRowNLMM(idRow, e) {
        e.preventDefault();
        $('#tbl-NLMM tbody tr#' + idRow).remove(); //remove tr
        var data = $.grep(dataNLMM.listNLMM, function (e) {
            return e.id != idRow;
        });

        dataNLMM.listNLMM = data;
    }

    function CancelDataNLMMToStorage() {
        $('#tbl-NLMM tbody tr').remove(); //remove all tr

        $('#chk-IsContract-NLMM').prop('checked', false);
        $('#div-chk-IsContract-NLMM .icheckbox_square-green').removeClass('checked');

        $('#chk-IsProfile').prop('checked', false);
        $('#div-chk-IsProfile .icheckbox_square-green').removeClass('checked');
    }

    function LoadDataNLMMToStorage() {
        var dataNLMM = JSON.parse(window.sessionStorage.getItem("dataNLMM")); //Convert it to JSON
        var tr = "";

        $('#tbl-NLMM tbody tr').remove(); //remove all tr

        if (dataNLMM != null) {
            $('#chk-IsContract-NLMM').prop('checked', dataNLMM.IsContract);
            dataNLMM.IsContract == false ? $('#div-chk-IsContract-NLMM .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsContract-NLMM .icheckbox_square-green').addClass('checked');

            $('#chk-IsProfile').prop('checked', dataNLMM.IsProfile);
            dataNLMM.IsProfile == false ? $('#div-chk-IsProfile .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsProfile .icheckbox_square-green').addClass('checked');

            for (var i = 0; i < dataNLMM.listNLMM.length; i++) {
                var valAccreditationTemp = dataNLMM.listNLMM[i].IsAccreditation;
                var strAccreditationTemp = valAccreditationTemp == true ? "Yêu cầu kiểm định" : "Không y/c kiểm định";
                tr += "<tr id='" + dataNLMM.listNLMM[i].id + "' class='text-center'>"
                    + "<td><p>" + dataNLMM.listNLMM[i].EquipmentName + "</p></td>"
                    + "<td><p>" + dataNLMM.listNLMM[i].PowerEquipment + "</p></td>"
                    + "<td><p>" + dataNLMM.listNLMM[i].QuantityEquipment + "</p></td>"
                    + "<td><p>" + strAccreditationTemp + "</p></td>"
                    + '<td><a href="#" id="btn-delete-row-NLMM" onclick="abilityEquipment.DeleteRowNLMM(\'' + dataNLMM.listNLMM[i].id + '\',event);" class="display-normal"><i class="fa fa-minus-square font_25 color-red"></i></a></td></tr > ';
            }
        }

        var trDefault = "<tr id='row-default-NLMM'>"
            + "<td><input type='text' id='txt-EquipmentName' class='form-control' placeholder= 'Nhập tên thiết bị'/></td>"
            + "<td><input type='text' id='txt-PowerEquipment' class='form-control' placeholder= 'Nhập C/S thiết bị' /></td>"
            + "<td><input type='number' id='txt-QuantityEquipment' class='form-control' placeholder= '...' /></td>"
            + "<td><div class='i-checks text-center pdt5' id='div-chk-IsAccreditation'>"
            + "<input type='checkbox' value='' id='chk-IsAccreditation'>"
            + "</div></td>"
            + "<td class='text-center'><a href='#' id='btn-add-row-NLMM' onclick='abilityEquipment.AddRowNLMM(event);' class='display-normal'><i class='fa fa-plus-square font_25 greenblue'></i></a></td>"
            + "</tr>";

        var jsUrl = '/Scripts/Common/plugins/iCheck/icheck.min.js';
        $.getScript(jsUrl, function () {
            if (tr === "") {
                $('#tbl-NLMM tbody').append(trDefault);
            } else {
                $('#tbl-NLMM tbody').append(tr);
                $('#tbl-NLMM tbody').append(trDefault);
            }
        }).done(function () {
            $('#div-chk-IsAccreditation').iCheck({
                checkboxClass: 'icheckbox_square-green',
            });
        });
    }

    function displaydataNLMMstorage(dataNLMM) {
        var content = "";
        var table = "<table class='table table-bordered'><thead><tr><th class='text-center with_text300'>Tên thiết bị máy móc</th>"
            + "<th class='text-center with_text150'>Công suất thiết bị</th>"
            + "<th class='text-center with_text80'>Số lượng</th>"
            + "<th class='text-center with_text135'>Kiểm định</th></tr></thead><tbody>";

        $("#content-NLMM-saved").empty();
        if (dataNLMM != null) {
            $("#placeholder-NLMM").removeClass("display-normal").addClass("display-none");
            if (dataNLMM.listNLMM.length > 0) {
                for (var i = 0; i < dataNLMM.listNLMM.length; i++) {
                    var valAccreditationTemp = dataNLMM.listNLMM[i].IsAccreditation;
                    var strAccreditationTemp = valAccreditationTemp == true ? "Yêu cầu kiểm định" : "Không y/c kiểm định";
                    table += "<tr class='text-center'>"
                        + "<td><p>" + dataNLMM.listNLMM[i].EquipmentName + "</p></td>"
                        + "<td><p>" + dataNLMM.listNLMM[i].PowerEquipment + "</p></td>"
                        + "<td><p>" + dataNLMM.listNLMM[i].QuantityEquipment + "</p></td>"
                        + "<td><p>" + strAccreditationTemp + "</p></td></tr>";
                }

                table += "</tbody></table>";
                content = table;
            }

            if (dataNLMM.IsContract == true || dataNLMM.IsProfile == true) {
                content += "<br /><label>Tài liệu chứng minh:</label>";
                if (dataNLMM.IsContract == true) {
                    content += "<label>- Hợp đồng mua bán hoặc hợp đồng thuê mượn hoặc hóa đơn mua thiết bị.</label>";
                }

                if (dataNLMM.IsProfile == true) {
                    content += "<label>- Hồ sơ kiểm định của các máy móc (đối với các máy móc phải có yêu cầu kiểm định).</label>";
                }
            }

            if (content != "") {
                $("#content-NLMM-saved").append(content);
                $("#content-NLMM-saved").removeClass("display-none").addClass("display-inline-grid");
            } else {
                $("#placeholder-NLMM").removeClass("display-none").addClass("display-normal");
            }
        } else {
            $("#placeholder-NLMM").removeClass("display-none").addClass("display-normal");
        }
    }

    return {
        initAbilityEquipment: initAbilityEquipment,
        AddRowNLMM: AddRowNLMM,
        DeleteRowNLMM: DeleteRowNLMM
    }
}();