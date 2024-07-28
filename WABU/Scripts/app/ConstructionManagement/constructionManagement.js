"use strict";

var constructionManagement = function () {

    var conditionHistory = "";
    var sortOptionDefault = [{
        "FieldName": "DateUpdated",
        "SortType": "desc"
    }];

    function init() {

    }

    function initConstructionSearchResult(e) {
        e.preventDefault();

        var data = JSON.stringify({
            page: 1,
            "condition": "",
            "sortOption": sortOptionDefault
        });

        $.ajax({
            url: "/ConstructionManagement/ReadConstructionPagingByCondition",
            contentType: 'application/json; charset=utf-8',
            type: "POST",
            dataType: 'json',
            data: data,
            success: function (responsed) {
                $(".table-responsive").html(responsed.patialView);
                $('#filterConstructionTable').DataTable({
                });
            }    
        });
    }

    function filterConstructionSearch(e) {
        e.preventDefault();

        var constructionName = $("input[name='ConstructionName']").val();
        var investorName = $("input[name='InvestorName']").val();
        var addressBuild = $("input[name='AddressBuild']").val();
        var areaId = $("select[name='AreaId']").val();
        var scale = $("input[name='Scale']").val();
        var basement = $("input[name='Basement']").val();
        var acreageBuild = $("input[name='AcreageBuild']").val();
        var constructionForm = $("select[name='ConstructionForm']").val();
        var buildingPermitDateTime = $("input[name='BuildingPermitDateTime']").val();
        var email = $("input[name='Email']").val();
        var contactName = $("input[name='ContactName']").val();
        var contactPhoneNumber = $("input[name='ContactPhoneNumber']").val();

        var areaId = $("select[name='AreaId']").val();

        var conditionStr = JSON.stringify({
            ConstructionName: constructionName, InvestorName: investorName, AddressBuild: addressBuild, StrAreaId: areaId, StrScale: scale, StrBasement: basement,
            StrAcreageBuild: acreageBuild, StrConstructionForm: constructionForm, BuildingPermitDateTime: buildingPermitDateTime, ContactEmail: email,
            ContactName: contactName, ContactPhoneNumber: contactPhoneNumber
        });
     
        var data = JSON.stringify({
            page: 1,
            "condition": conditionStr,
            "sortOption": sortOptionDefault
        });
        conditionHistory = conditionStr;

        $.ajax({
            url: "/ConstructionManagement/ReadConstructionPagingByCondition",
            contentType: 'application/json; charset=utf-8',
            type: "POST",
            dataType: 'json',
            data: data,
            success: function (responsed) {
                $(".table-responsive").html(responsed.patialView);
                $('#filterConstructionTable').DataTable({
                });
            }
        });
    }

    return {
        init: init,
        initConstructionSearchResult: initConstructionSearchResult,
        filterConstructionSearch: filterConstructionSearch,
    }
}();