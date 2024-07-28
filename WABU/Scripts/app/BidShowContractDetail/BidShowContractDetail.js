"use strict";

var bidShowContractDetail = function () {

    function dropcompanyAbilityExp() {
        if ($("#select-Project").find(":selected").val() == '') {
            return;
        }

        $.ajax({
            method: "GET",
            url: "/BidShowContractDetail/companyAbilityExp?Id=" + $("#select-Project").val()
        }).done(function (data) {
            $("#InvestorName").html(data.result.InvestorName);
            $("#InvestorAddress").html(data.result.InvestorAddress);
            $("#InvestorPhoneNumber").html(data.result.InvestorPhoneNumber);
            $("#ContructionType").html(data.result.ContructionType);
            $("#ProjectScale").html(data.result.ProjectScale);
            $("#ContractName").html(data.result.ContractName);
            $("#ContractSignDate").html(data.result.ContractSignDate);
            $("#ContractCompleteDate").html(data.result.ContractCompleteDate);
            $("#ContractPrices").html(data.result.ContractPrices);
            $("#ProjectDescription").html(data.result.ProjectDescription);
            if (data.EvidenceContractJson == "" || data.EvidenceContractJson == null) {
                $("#tab-sub-1").html("<div class='panel-body'><p>Đang cập nhật tài liệu</p></div>");
            } else {
                $("#tab-sub-1")
                    .html("<div class='panel-body'><object width='100%' height='100%' data='/" + data.EvidenceContractJson + "' type='application/pdf' class='pdfobject-container'></object></div>");
            }
            if (data.EvidenceBuildingPermitJson == "" || data.EvidenceBuildingPermitJson == null) {
                $("#tab-sub-2").html("<div class='panel-body'><p>Đang cập nhật tài liệu</p></div>");
            } else {
                $("#tab-sub-2")
                    .html("<div class='panel-body'><object width='100%' height='100%' data='/" + data.EvidenceBuildingPermitJson.toLocaleString() + "' type='application/pdf' class='pdfobject-container'></object></div>");
            }
            if (data.EvidenceContractLiquidationJson == "" || data.EvidenceContractLiquidationJson == null) {
                $("#tab-sub-3").html("<div class='panel-body'><p>Đang cập nhật tài liệu</p></div>");
            } else {
                $("#tab-sub-3")
                    .html("<div class='panel-body'><object width='100%' height='100%' data='/" + data.EvidenceContractLiquidationJson + "' type='application/pdf' class='pdfobject-container'></object></div>");
            }
        });
    };

    function dropcompanyAbilityFinance() {
        if ($("#select-Finance").find(":selected").val() == '') {
            return;
        }
        $.ajax({
            method: "GET",
            url: "/BidShowContractDetail/companyAbilityFinances?Id=" + $("#select-Finance").val(),
        }).done(function (data) {
            $("#TotalAssets").html(data.result.TotalAssets);
            $("#TotalLiabilities").html(data.result.TotalLiabilities);
            $("#ShortTermAssets").html(data.result.ShortTermAssets);
            $("#TotalCurrentLiabilities").html(data.result.TotalCurrentLiabilities);
            $("#Revenue").html(data.result.Revenue);
            $("#ProfitBeforeTax").html(data.result.ProfitBeforeTax);
            $("#ProfitAfterTax").html(data.result.ProfitBeforeTax);
            if (data.EvidenceCheckSettlementJson == "" || data.EvidenceCheckSettlementJson == null) {
                $("#tab-sub-tc-1").html("<div class='panel-body'><p>Đang cập nhật tài liệu</p></div>");
            } else {
                $("#tab-sub-tc-1")
                    .html("<div class='panel-body'><object width='100%' height='100%' data='/" + data.EvidenceCheckSettlementJson + "' type='application/pdf' class='pdfobject-container'></object></div>");
            }
            if (data.EvidenceDeclareTaxJson == "" || data.EvidenceDeclareTaxJson == null) {
                $("#tab-sub-tc-2").html("<div class='panel-body'><p>Đang cập nhật tài liệu</p></div>");
            } else {
                $("#tab-sub-tc-2")
                    .html("<div class='panel-body'><object width='100%' height='100%' data='/" + data.EvidenceDeclareTaxJson + "' type='application/pdf' class='pdfobject-container'></object></div>");
            }
            if (data.EvidenceCertificationTaxJson == "" || data.EvidenceCertificationTaxJson == null) {
                $("#tab-sub-tc-3").html("<div class='panel-body'><p>Đang cập nhật tài liệu</p></div>");
            } else {
                $("#tab-sub-tc-3")
                    .html("<div class='panel-body'><object width='100%' height='100%' data='/" + data.EvidenceCertificationTaxJson + "' type='application/pdf' class='pdfobject-container'></object></div>");
            }
            if (data.EvidenceAuditReportJson == "" || data.EvidenceAuditReportJson == null) {
                $("#tab-sub-tc-4").html("<div class='panel-body'><p>Đang cập nhật tài liệu</p></div>");
            } else {
                $("#tab-sub-tc-4")
                    .html("<div class='panel-body'><object width='100%' height='100%' data='/" + data.EvidenceAuditReportJson + "' type='application/pdf' class='pdfobject-container'></object></div>");
            }
        });
    };

    function dropcompanyAbilityHr() {
        if ($("#select-Hr").find(":selected").val() == "") {
            return;
        }

        $.ajax({
            method: "GET",
            url: "/BidShowContractDetail/companyAbilityHr?Id=" + $("#select-Hr").val(),
        }).done(function (data) {
            $("#Title").html(data.result.Title);
            $("#Certificate").html(data.result.Certificate);
            $("#School").html(data.result.School);
            $("#Branch").html(data.result.Branch);
            $("#Address").html(data.result.Address);
            $("#PhoneNumber").html(data.result.PhoneNumber);
            if (data.companyabilityHRDetails == "") {
                $(".scroll_content").html("Chưa có dữ liệu.");
            } else {
                $(".scroll_content").html(data.companyabilityHRDetails);
            }

            if (data.EvidenceLaborContractJson == "" || data.EvidenceLaborContractJson == null) {
                $("#tab-sub-ns-1").html("<div class='panel-body'><p>Đang cập nhật tài liệu</p></div>");
            } else {
                $("#tab-sub-ns-1")
                    .html(
                        "<div class='panel-body'><object width='100%' height='100%' data='/" +
                        data.EvidenceLaborContractJson +
                        "' type='application/pdf' class='pdfobject-container'></object></div>");
            }
            if (data.EvidenceSimilarCertificatesJson == "" || data.EvidenceSimilarCertificatesJson == null) {
                $("#tab-sub-ns-2").html("<div class='panel-body'><p>Đang cập nhật tài liệu</p></div>");
            } else {
                $("#tab-sub-ns-2")
                    .html(
                        "<div class='panel-body'><object width='100%' height='100%' data='/" +
                        data.EvidenceSimilarCertificatesJson +
                        "' type='application/pdf' class='pdfobject-container'></object></div>");
            }
            if (data.EvidenceAppointmentStaffJson == "" || data.EvidenceAppointmentStaffJson == null) {
                $("#tab-sub-ns-3").html("<div class='panel-body'><p>Đang cập nhật tài liệu</p></div>");
            } else {
                $("#tab-sub-ns-3")
                    .html(
                        "<div class='panel-body'><object width='100%' height='100%' data='/" +
                        data.EvidenceAppointmentStaffJson +
                        "' type='application/pdf' class='pdfobject-container'></object></div>");
            }
        });
    };

    function dropcompanyAbilityEquipment() {
        if ($("#select-Equipment").find(":selected").val() == '') {
            return;
        }
        $.ajax({
            method: "GET",
            url: "/BidShowContractDetail/companyAbilityEquipment?Id=" + $("#select-Equipment").val(),
        }).done(function (data) {

            $("#EquipmentType").html(data.result.EquipmentType);
            $("#Quantity").html(data.result.Quantity);
            $("#Capacity").html(data.result.Capacity);
            $("#Functiondata").html(data.result.Function);
            $("#NationalProduction").html(data.result.NationalProduction);
            $("#YearManufacture").html(data.result.YearManufacture);
            $("#Source").html(data.result.Source);

            if (data.EvidenceSaleContractJson == "" || data.EvidenceSaleContractJson == null) {
                $("#tab-sub-tb-1").html("<div class='panel-body'><p>Đang cập nhật tài liệu</p></div>");
            } else {
                $("#tab-sub-tb-1")
                    .html(
                        "<div class='panel-body'><object width='100%' height='100%' data='/" +
                        data.EvidenceSaleContractJson +
                        "' type='application/pdf' class='pdfobject-container'></object></div>");
            }
            if (data.EvidenceInspectionRecordsJson == "" || data.EvidenceInspectionRecordsJson == null) {
                $("#tab-sub-tb-2").html("<div class='panel-body'><p>Đang cập nhật tài liệu</p></div>");
            } else {
                $("#tab-sub-tb-2")
                    .html(
                        "<div class='panel-body'><object width='100%' height='100%' data='/" +
                        data.EvidenceInspectionRecordsJson +
                        "' type='application/pdf' class='pdfobject-container'></object></div>");
            }
        });
    };

    var downloadRequiredDocument = () => {
        $('.downloadRequiredDocument').on('click', (e) => {
            e.preventDefault();

            var pathForm = $('#downloadRequiredDocumentForm').find('input[name="path"]');
            var dataPath = $(e.currentTarget).data('path');
            pathForm.val(dataPath);
            document.getElementById('downloadRequiredDocumentForm').submit();
        });
    }

    return {
        dropcompanyAbilityExp: dropcompanyAbilityExp,
        dropcompanyAbilityFinance: dropcompanyAbilityFinance,
        dropcompanyAbilityHr: dropcompanyAbilityHr,
        dropcompanyAbilityEquipment: dropcompanyAbilityEquipment,
        downloadRequiredDocument: downloadRequiredDocument
    };
}();