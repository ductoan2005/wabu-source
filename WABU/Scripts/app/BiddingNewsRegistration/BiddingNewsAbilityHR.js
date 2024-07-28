var abilityHR = function () {
    function initAbilityHR() {
        $("#btn_ctr_NLNS").click(function () {
            LoadDataNLNSToStorage();
            $("#placeholder-NLNS").removeClass("display-none").addClass("display-normal");
            $("#content-NLNS-saved").removeClass("display-inline-grid").addClass("display-none");

            $("#ctr_NLNS").removeClass("bs-transaction");
            $("#ctr_NLNS").addClass("bs-transaction-edit");
            $("#ctr_NLNS").addClass("bs-transaction-edit");
            $("#btn_ctr_NLNS").addClass("hide");
        })
        $("#btn_ctr_NLNS_cancel").click(function () {
            CancelDataNLNSToStorage();
            if ($("#content-NLNS-saved label").length > 0 || $("#content-NLNS-saved table").length > 0) {
                $("#placeholder-NLNS").removeClass("display-normal").addClass("display-none");
                $("#content-NLNS-saved").removeClass("display-none").addClass("display-inline-grid");
            } else {
                $("#placeholder-NLNS").removeClass("display-none").addClass("display-normal");
                $("#content-NLNS-saved").removeClass("display-inline-grid").addClass("display-none");
            }

            $("#ctr_NLNS").addClass("bs-transaction");
            $("#ctr_NLNS").removeClass("bs-transaction-edit");
            $("#btn_ctr_NLNS").removeClass("hide");
        })
        $("#btn_ctr_NLNS_save").click(function () {
            SaveDataNLNSToStorage();
            $("#ctr_NLNS").addClass("bs-transaction");
            $("#ctr_NLNS").removeClass("bs-transaction-edit");
            $("#btn_ctr_NLNS").removeClass("hide");
        })
    }

    // variable common
    var rowIdNLNS = 0;
    var dataNLNS = { listNLNS: [] };
    function AddRowNLNS(e) {
        e.preventDefault();
        if ($('#txt-JobPosition').val() === "") {
            toastr.error("Vui lòng nhập vị trí công việc muốn thêm.");
            return;
        }

        rowIdNLNS++;
        var idRow = "row_NLNS_" + rowIdNLNS;
        var trAccept = "<tr id='" + idRow + "' class='text-center'>"
            + "<td><p>" + $('#txt-JobPosition').val() + "</p></td>"
            + "<td><p>" + $('#txt-QualificationRequired').val() + "</p></td>"
            + "<td><p>" + $('#txt-YearExp').val() + "</p></td>"
            + "<td><p>" + $('#txt-NumberRequest').val() + "</p></td>"
            + "<td><p>" + $('#txt-SimilarProgram').val() + "</p></td>"
            + '<td><a href="#" id="btn-delete-row-NLNS" onclick="abilityHR.DeleteRowNLNS(\'' + idRow + '\',event);" class="display-normal"><i class="fa fa-minus-square font_25 color-red"></i></a></td></tr > ';

        var trDefault = "<tr id='row-default-NLNS'>"
            + "<td><input type='text' id='txt-JobPosition' class='form-control' placeholder='Nhập vị trí công việc'/></td>"
            + "<td><input type='text' id='txt-QualificationRequired' class='form-control' placeholder='Bằng cấp'/></td>"
            + "<td><input type='number' id='txt-YearExp' class='form-control' placeholder='Số năm'/></td>"
            + "<td><input type='number' id='txt-NumberRequest' class='form-control' placeholder='Số lượng người'/></td>"
            + "<td><input type='number' id='txt-SimilarProgram' class='form-control' placeholder='Số lượng công trình'/></td>"
            + "<td class='text-center'><a href='#' id='btn-add-row-NLNS' onclick='abilityHR.AddRowNLNS(event);' class='display-normal'><i class='fa fa-plus-square font_25 greenblue'></i></a></td>"
            + "</tr>";

        dataNLNS.listNLNS.push(
            {
                id: idRow,
                JobPosition: $('#txt-JobPosition').val(),
                QualificationRequired: $('#txt-QualificationRequired').val(),
                YearExp: $('#txt-YearExp').val(),
                NumberRequest: $('#txt-NumberRequest').val(),
                SimilarProgram: $('#txt-SimilarProgram').val()
            });

        // draw tr
        if ($('#tbl-NLNS tbody #row-default-NLNS').length) {
            $('#tbl-NLNS #row-default-NLNS').remove(); //remove tr

            $('#tbl-NLNS tbody').append(trAccept);
            $('#tbl-NLNS tbody').append(trDefault);
        }
    }

    function DeleteRowNLNS(idRow, e) {
        e.preventDefault();
        $('#tbl-NLNS tbody tr#' + idRow).remove(); //remove tr
        var data = $.grep(dataNLNS.listNLNS, function (e) {
            return e.id != idRow;
        });

        dataNLNS.listNLNS = data;
    }

    function CancelDataNLNSToStorage() {
        $('#tbl-NLNS tbody tr').remove(); //remove all tr

        $('#chk-IsLaborContract').prop('checked', false);
        $('#div-chk-IsLaborContract .icheckbox_square-green').removeClass('checked');

        $('#chk-IsDocumentRequest').prop('checked', false);
        $('#div-chk-IsDocumentRequest .icheckbox_square-green').removeClass('checked');

        $('#chk-IsDecision').prop('checked', false);
        $('#div-chk-IsDecision .icheckbox_square-green').removeClass('checked');
    }

    function SaveDataNLNSToStorage() {
        //set data to object
        dataNLNS.IsLaborContract = $('#chk-IsLaborContract').is(":checked");
        dataNLNS.IsDocumentRequest = $('#chk-IsDocumentRequest').is(":checked");
        dataNLNS.IsDecision = $('#chk-IsDecision').is(":checked");

        // save data to storage
        window.sessionStorage.setItem("dataNLNS", JSON.stringify(dataNLNS)); //Convert it to JSON

        displaydataNLNSstorage(dataNLNS);
    }

    function LoadDataNLNSToStorage() {
        var dataNLNS = JSON.parse(window.sessionStorage.getItem("dataNLNS")); //Convert it to JSON
        if (dataNLNS != null) {
            $('#chk-IsLaborContract').prop('checked', dataNLNS.IsLaborContract);
            dataNLNS.IsLaborContract == false ? $('#div-chk-IsLaborContract .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsLaborContract .icheckbox_square-green').addClass('checked');

            $('#chk-IsDocumentRequest').prop('checked', dataNLNS.IsDocumentRequest);
            dataNLNS.IsDocumentRequest == false ? $('#div-chk-IsDocumentRequest .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsDocumentRequest .icheckbox_square-green').addClass('checked');

            $('#chk-IsDecision').prop('checked', dataNLNS.IsDecision);
            dataNLNS.IsDecision == false ? $('#div-chk-IsDecision .icheckbox_square-green').removeClass('checked') : $('#div-chk-IsDecision .icheckbox_square-green').addClass('checked');

            $('#tbl-NLNS tbody tr').remove(); //remove all tr
            var tr = "";
            for (var i = 0; i < dataNLNS.listNLNS.length; i++) {
                tr += "<tr id='" + dataNLNS.listNLNS[i].id + "' class='text-center'>"
                    + "<td><p>" + dataNLNS.listNLNS[i].JobPosition + "</p></td>"
                    + "<td><p>" + dataNLNS.listNLNS[i].QualificationRequired + "</p></td>"
                    + "<td><p>" + dataNLNS.listNLNS[i].YearExp + "</td>"
                    + "<td><p>" + dataNLNS.listNLNS[i].NumberRequest + "</p></td>"
                    + "<td><p>" + dataNLNS.listNLNS[i].SimilarProgram + "</p></td>"
                    + '<td><a href="#" id="btn-delete-row-NLNS" onclick="abilityHR.DeleteRowNLNS(\'' + dataNLNS.listNLNS[i].id + '\',event);" class="display-normal"><i class="fa fa-minus-square font_25 color-red"></i></a></td></tr > ';
            }

            var trDefault = "<tr id='row-default-NLNS'>"
                + "<td><input type='text' id='txt-JobPosition' class='form-control' placeholder='Nhập vị trí công việc'/></td>"
                + "<td><input type='text' id='txt-QualificationRequired' class='form-control' placeholder='Bằng cấp'/></td>"
                + "<td><input type='number' id='txt-YearExp' class='form-control' placeholder='Số năm'/></td>"
                + "<td><input type='number' id='txt-NumberRequest' class='form-control' placeholder='Số lượng người'/></td>"
                + "<td><input type='text' id='txt-SimilarProgram' class='form-control' placeholder='Số lượng công trình'/></td>"
                + "<td class='text-center'><a href='#' id='btn-add-row-NLNS' onclick='abilityHR.AddRowNLNS(event);' class='display-normal'><i class='fa fa-plus-square font_25 greenblue'></i></a></td>"
                + "</tr>";

            if (tr === "") {
                $('#tbl-NLNS tbody').append(trDefault);
            } else {
                $('#tbl-NLNS tbody').append(tr);
                $('#tbl-NLNS tbody').append(trDefault);
            }
        }
    }

    function displaydataNLNSstorage(dataNLNS) {
        var content = "";
        var table = "<table class='table table-bordered'><thead><tr><th class='text-center with_text250'>Vị trí công việc</th>"
            + "<th class='text-center with_text100'>Bằng cấp yêu cầu</th>"
            + "<th class='text-center with_text80' title='Số năm kinh nghiệm'>Số năm K/N</th>"
            + "<th class='text-center with_text100'>Số lượng</th>"
            + "<th class='text-center with_text200'>Chương trình tương tự đã tham gia</th></tr></thead><tbody>";

        $("#content-NLNS-saved").empty();
        if (dataNLNS != null) {
            $("#placeholder-NLNS").removeClass("display-normal").addClass("display-none");
            if (dataNLNS.listNLNS.length > 0) {
                for (var i = 0; i < dataNLNS.listNLNS.length; i++) {
                    table += "<tr class='text-center'>"
                        + "<td><p>" + dataNLNS.listNLNS[i].JobPosition + "</p></td>"
                        + "<td><p>" + dataNLNS.listNLNS[i].QualificationRequired + "</p></td>"
                        + "<td><p>" + dataNLNS.listNLNS[i].YearExp + "</p></td>"
                        + "<td><p>" + dataNLNS.listNLNS[i].NumberRequest + "</p></td>"
                        + "<td><p>" + dataNLNS.listNLNS[i].SimilarProgram + "</p></td></tr>";
                }

                table += "</tbody></table>";
                content = table;
            }

            if (dataNLNS.IsLaborContract == true || dataNLNS.IsDocumentRequest == true || dataNLNS.IsDecision == true) {
                content += "<br /><label>Tài liệu chứng minh:</label>";
                if (dataNLNS.IsLaborContract == true) {
                    content += "<label>- Hợp đồng lao động.</label>";
                }

                if (dataNLNS.IsDocumentRequest == true) {
                    content += "<label>- Văn bằng yêu cầu.</label>";
                }

                if (dataNLNS.IsDecision == true) {
                    content += "<label>- Quyết định bổ nhiệm nhân sự (đối với những vị trí yêu cầu về số lượng công trình tương tự đã tham gia thực hiện).</label>";
                }
            }

            if (content != "") {
                $("#content-NLNS-saved").append(content);
                $("#content-NLNS-saved").removeClass("display-none").addClass("display-inline-grid");
            } else {
                $("#placeholder-NLNS").removeClass("display-none").addClass("display-normal");
            }
        } else {
            $("#placeholder-NLNS").removeClass("display-none").addClass("display-normal");
        }
    }

    return {
        initAbilityHR: initAbilityHR,
        AddRowNLNS: AddRowNLNS,
        DeleteRowNLNS: DeleteRowNLNS
    };
}();