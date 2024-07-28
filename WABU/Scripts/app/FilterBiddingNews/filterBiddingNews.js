"use strict";

var filterBiddingNews = function () {

    var conditionHistory = "";
    var sortOptionDefault = [{
        "FieldName": "DateUpdated",
        "SortType": "desc"
    }];

    function init() {

    }

    function countDownBidCloseDate() {
        var bidCloseDateVals = document.getElementsByClassName('dateInserted');
        if (!bidCloseDateVals.length > 0) {
            return;
        }
        for (var i = 0; i < bidCloseDateVals.length; i++) {
            var closeDateVal = $(bidCloseDateVals[i]).data('value');
            var closeDate = new Date(closeDateVal);
            closeDate.setHours(closeDate.getHours() + 24);

            var countdownBidCloseDate = $(bidCloseDateVals[i]).parent().find('td.countdownBidCloseDate');
            if (!isNaN(closeDate.getTime())) {
                $(countdownBidCloseDate).countdown(closeDate,
                    function (event) {
                        $(this).html(event.strftime('%H:%M:%S'));
                    });
            } else {
                $(countdownBidCloseDate).html('00:00:00');
            }
        }
    }

    function initFilterBiddingNewsSearchResult(e, isFilterValid) {
        var statusActive = document.getElementById("fiterbiddingP").checked;
        var conditionStr = JSON.stringify({ StatusActive: statusActive });
        var url = "/FilterBiddingNews/ReadBiddingFilterToPagingByCondition";
        var data = JSON.stringify({
            isFilterValidNews: isFilterValid,
            page: 1,
            "condition": conditionStr,
            "sortOption": sortOptionDefault
        });

        $.ajax({
            url: url,
            contentType: 'application/json; charset=utf-8',
            type: "POST",
            dataType: 'json',
            data: data,
            success: function (responsed) {
                $(".table-responsive").html(responsed.patialView);
                $('#filterBiddingTable').DataTable({
                    "drawCallback": function (oSettings) {
                        countDownBidCloseDate();
                    }
                });
                countDownBidCloseDate();
            }
        });
    }

    function filterBiddingNewsSearch(e, isFilterValid) {
        e.preventDefault();

        var fromDate = $("input[name='dateStartP']").val();
        var toDate = $("input[name='dateEndP']").val();
        var dateCreate = $("input[name='dateCreateP']").val();
        var statusActive = document.getElementById("fiterbiddingP").checked;
        var biddingNewsStatus = $("select[name='biddingNewsStatus']").val();
        var conditionStr = JSON.stringify({ FromDate: fromDate, ToDate: toDate, CreatedDate: dateCreate, StatusActive: statusActive, StatusBidding: biddingNewsStatus });

        var data = JSON.stringify({
            isFilterValidNews: isFilterValid,
            page: 1,
            "condition": conditionStr,
            "sortOption": sortOptionDefault
        });
        conditionHistory = conditionStr;

        $.ajax({
            url: "/FilterBiddingNews/ReadBiddingFilterToPagingByCondition",
            contentType: 'application/json; charset=utf-8',
            type: "POST",
            dataType: 'json',
            data: data,
            success: function (responsed) {
                $(".table-responsive").html(responsed.patialView);
                $('#filterBiddingTable').DataTable({
                    "drawCallback": function (oSettings) {
                        countDownBidCloseDate();
                    }
                });
            }
        });
    }

    function updateActiveStatus(e, id) {
        e.preventDefault();

        var data = JSON.stringify({
            id: id,
            isActive: $(e.target).data('value')
        });

        $.ajax({
            contentType: 'application/json; charset=utf-8',
            type: "POST",
            dataType: 'json',
            url: "/FilterBiddingNews/UpdateActiveStatus",
            data: data,
            success: function (responsed) {
                if (responsed.succeed === '0') {
                    toastr.success("Cập nhật trạng thái tin thầu thành công");
                    updateElementUpdateActive(e, responsed.isActive);
                }
                else {
                    toastr.error(responsed);
                }
            }
        });
    }

    function deleteNews(e, id) {
        e.preventDefault();

        var data = JSON.stringify({
            id: id,
        });

        $.ajax({
            contentType: 'application/json; charset=utf-8',
            type: "POST",
            dataType: 'json',
            url: "/FilterBiddingNews/DeleteNews",
            data: data,
            success: function (responsed) {
                if (responsed.succeed === '0') {
                    toastr.success("Xóa tin thầu thành công");
                    filterBiddingNewsSearch(e, false);
                }
                else {
                    toastr.error(responsed);
                }
            }
        });
    }

    function updateElementUpdateActive(e, isActive) {
        var element = e.target;
        if (isActive) {
            $(element).addClass('hidden');
            $(element).next().removeClass('hidden');
            $(element).parent().parent().find('.switchActive').removeClass('fa-toggle-off').addClass('fa-toggle-on');
        }
        else {
            $(element).addClass('hidden');
            $(element).prev().removeClass('hidden');
            $(element).parent().parent().find('.switchActive').removeClass('fa-toggle-on').addClass('fa-toggle-off');
        }
    }

    return {
        init: init,
        initFilterBiddingNewsSearchResult: initFilterBiddingNewsSearchResult,
        filterBiddingNewsSearch: filterBiddingNewsSearch,
        updateActiveStatus: updateActiveStatus,
        deleteNews: deleteNews
    }
}();