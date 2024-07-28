"use strict";

var biddingNewsBookmark = function () {

    var conditionStr = "";
    var sortOptionDefault = [{
        "FieldName": "DateUpdated",
        "SortType": "desc"
    }];

    //[START]-----------------Search Function-----------------

    function searchBiddingNewsBookmark(e) {
        e.preventDefault();
        var fromDate = $("input[name='FromDate']").val();
        var toDate = $("input[name='ToDate']").val();
        var status = $("select[name='Status']").val();
        var contructionName = $("select[name='ContructionName']").find(":selected").text();
        var biddingPackageType = $("select[name='BiddingPackageName']").find(":selected").val();
        //var authority = $("input[name='Authority']").val();
        conditionStr = JSON.stringify({ FromDate: fromDate, ToDate: toDate, StatusBidding: status, ContructionName: contructionName, BiddingPackageType: biddingPackageType });
        var data = {
            page: 1,
            condition: conditionStr
        };
        $.ajax({
            type: "POST",
            url: "/BiddingNewsBookmark/ReadPagingBiddingNewsBookmarkList",
            data: data,
            success: function (responsed) {
                $("#tblPackageDetail").html(responsed.patialView);
            }
        });
    }

    //[END]-----------------Search Function-----------------

    //[STR]-----------------Paging BiddingNewsBookmark List-----------------

    function changePageOnListBiddingNewsBookmark(pageSelected, e) {
        e.preventDefault();
        var data = {
            page: pageSelected,
            condition: conditionStr
        };

        $.ajax({
            type: "POST",
            url: "/BiddingNewsBookmark/ReadPagingBiddingNewsBookmarkList",
            data: data,
            success: function (responsed) {
                $("#tblPackageDetail").html(responsed.patialView);
            }
        });
    }

    //[END]-----------------Paging BiddingNewsBookmark List-----------------


    return {
        changePageOnListBiddingNewsBookmark: changePageOnListBiddingNewsBookmark,
        searchBiddingNewsBookmark: searchBiddingNewsBookmark
    };
}();