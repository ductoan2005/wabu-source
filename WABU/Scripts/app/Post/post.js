"use strict";

var postManagement = function () {

    var sortOptionDefault = [{
        "FieldName": "DateUpdated",
        "SortType": "desc"
    }];

    function init() {

    }

    function initPostSearchResult(e) {
        e.preventDefault();

        var data = JSON.stringify({
            page: 1,
            "condition": "",
            "sortOption": sortOptionDefault
        });

        $.ajax({
            url: "/Posts/GetPostsByFilters",
            contentType: 'application/json; charset=utf-8',
            type: "POST",
            dataType: 'json',
            data: data,
            success: function (responsed) {
                $(".table-responsive").html(responsed.patialView);
                $('#filterPostTable').DataTable({
                });
            }
        });
    }

    function filterPostSearch(e) {
        e.preventDefault();

        var title = $("input[name='Title']").val();
        var username = $("input[name='Username']").val();
        var isEnable = document.getElementById("IsEnableP").checked;
        //var isEnable = document.getElementById("IsEnable").checked;
        var createdDate = $("input[name='PostCreatedDatetime']").val();
        var lastUpdatedDate = $("input[name='PostUpdatedDatetime']").val();

        var conditionStr = JSON.stringify({
            Title: title, Username: username, IsEnable: isEnable, CreatedDate: createdDate, LastUpdatedDate: lastUpdatedDate
        });

        var data = JSON.stringify({
            page: 1,
            "condition": conditionStr,
            "sortOption": sortOptionDefault
        });
        //var conditionHistory = conditionStr;

        $.ajax({
            url: "/Posts/GetPostsByFilters",
            contentType: 'application/json; charset=utf-8',
            type: "POST",
            dataType: 'json',
            data: data,
            success: function (responsed) {
                $(".table-responsive").html(responsed.patialView);
                $('#filterPostTable').DataTable({
                });
            }
        });
    }

    return {
        init: init,
        initPostSearchResult: initPostSearchResult,
        filterPostSearch: filterPostSearch,
    }
}();