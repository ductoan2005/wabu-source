"use strict";

var biddingNewsSearch = function () {

    //expireDay for cookie
    const expireDay = 30;
    //key for uid cookie
    const cuidKey = 'Cuid';
    const recentKeywordsKey = 'RecentKeywords';
    var conditionHistory = "";
    var sortOptionDefault = [{
        "FieldName": "DateUpdated",
        "SortType": "desc"
    }];

    function init() {

    }

    var fillKeyWordsSearchTextbox = function (e) {
        var target = e.target;
        $('input[name="TextSearch"]').val($(target).data('value'));
    }

    var formatListKeyWord = function (newKeyWord) {
        var keyWords = getCookie(recentKeywordsKey);
        var arrKeyWord = new Array();
        if (keyWords != '') {
            arrKeyWord = JSON.parse(keyWords);
            if (arrKeyWord.length > 4) {
                arrKeyWord.shift();
            }
        }
        arrKeyWord.push(newKeyWord);
        return JSON.stringify(arrKeyWord);
    }

    String.prototype.hashCode = function () {
        var hash = 0, i, chr;
        if (this.length === 0) return hash;
        for (i = 0; i < this.length; i++) {
            chr = this.charCodeAt(i);
            hash = ((hash << 5) - hash) + chr;
            hash |= 0; // Convert to 32bit integer
        }
        return hash;
    };

    function generateCuid(id) { return id.hashCode(); }

    function setCookie(cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    }

    function getCookie(cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }

    var clearCookie = function (cname) {
        setCookie(cname, '', expireDay);
    }

    function updateSearchKeyWord(keyWord) {
        if (keyWord != '') {
            var newKeyWord = keyWord;
            keyWord = formatListKeyWord(newKeyWord);
            setCookie(recentKeywordsKey, keyWord, expireDay);
            showAllSearchKeyWord();
        }
    }

    function showSearchKeyWord() {
        var newUid = $('input[name="UserId"]').val();
        var strNewUid = 'cUid' + newUid;
        var cUid = generateCuid(strNewUid);
        var oldUid = getCookie(cuidKey);
        if (cUid == oldUid) {
            showAllSearchKeyWord();
        }
        else {
            clearCookie(oldUid);
            clearCookie(recentKeywordsKey);
        }
        setCookie(cuidKey, cUid, expireDay);
    }

    function appendKeyWord(keyWord, order) {
        var element = '';
        if (order == 0) {
            element = '<a data-value=' + keyWord + ' onclick="biddingNewsSearch.fillKeyWordsSearchTextbox(event)" >' + keyWord + '</a>';
        }
        else {
            element = '</br><a data-value=' + keyWord + ' onclick="biddingNewsSearch.fillKeyWordsSearchTextbox(event)">' + keyWord + '</a>';
        }
        $('#recentKeyWords').append(element);
    }

    function showAllSearchKeyWord() {
        $('#recentKeyWords').children().remove();
        var listKeyWord = getCookie('RecentKeywords').split(',');
        if (listKeyWord != '')
            listKeyWord = JSON.parse(listKeyWord);
        for (var i = 0; i < listKeyWord.length; i++) {
            appendKeyWord(listKeyWord[i], i)
        }
    };

    function updateOrderbyDay() {
        var orderByDayEle = $("#orderByDay");
        if (orderByDayEle.data("value") == "desc")
            orderByDayEle.data("value", "asc");
        else
            orderByDayEle.data("value", "desc");
        return;
    }

    //Init page
    function initBiddingNewsSearchResult(e) {
        e.preventDefault();

        var data = JSON.stringify({
            page: 1,
            "sortOption": sortOptionDefault
        });

        $.ajax({
            url: "/BiddingNews/ReadBiddingNewsToPagingByCondition",
            contentType: 'application/json; charset=utf-8',
            type: "POST",
            dataType: 'json',
            data: data,
            success: function (responsed) {
                $("#biddingSearchResult").html(responsed.patialView);
                updateOrderbyDay();
                //Show recently search
                showSearchKeyWord();
            }
        });
    }

    // Search function
    function search(e) {
        e.preventDefault();

        var areaId = $("select[name='AreaId']").find(":selected").val();
        var status = $("select[name='StatusBidding']").val();
        var authority = $("input[name='Authority']").val();
        var jobPosition = $("input[name='JobPosition']").val();
        var textSearch = $("input[name='TextSearch']").val();
        var fromDate = $("input[name='FromDate']").val();
        var biddingPackageId = $("select[name='BiddingPackageId']").find(":selected").val();
        var numberYearActivityAbilityExp = $("input[name='NumberYearActivityAbilityExp']").val();
        var numberSimilarContractAbilityExp = $("input[name='NumberSimilarContractAbilityExp']").val();
        var turnover2YearAbilityFinance = $("input[name='Turnover2YearAbilityFinance']").val();

        var conditionStr = JSON.stringify({
            AreaId: areaId, StatusBidding: status, Authority: authority, JobPosition: jobPosition,
            TextSearch: textSearch, FromDate: fromDate, BiddingPackageId: parseInt(biddingPackageId),
            NumberYearActivityAbilityExp: numberYearActivityAbilityExp, NumberSimilarContractAbilityExp: numberSimilarContractAbilityExp,
            Turnover2YearAbilityFinance: turnover2YearAbilityFinance
        });

        conditionHistory = conditionStr;

        var data = JSON.stringify({
            page: 1,
            "condition": conditionStr,
            "sortOption": sortOptionDefault
        });

        $.ajax({
            url: "/BiddingNews/ReadBiddingNewsToPagingByCondition",
            contentType: 'application/json; charset=utf-8',
            type: "POST",
            dataType: 'json',
            data: data,
            success: function (responsed) {
                $("#biddingSearchResult").html(responsed.patialView);
                updateOrderbyDay();
                updateSearchKeyWord(textSearch);
            }
        });
    }

    //Paging
    function changePageOnListBidding(pageSelected, e) {
        e.preventDefault();

        sortOptionDefault[0].SortType = $("#orderByDay").data("value");

        //Get condition from history choice
        var data = JSON.stringify({
            page: pageSelected,
            "condition": conditionHistory,
            "sortOption": sortOptionDefault
        });

        $.ajax({
            url: "/BiddingNews/ReadBiddingNewsToPagingByCondition",
            contentType: 'application/json; charset=utf-8',
            type: "POST",
            dataType: 'json',
            data: data,
            success: function (responsed) {
                $("#biddingSearchResult").html(responsed.patialView);
                updateOrderbyDay();
            }
        });
    }

    // Autocomple job position text box 
    function getJobPositionKeyWord(e) {
        e.preventDefault();

        $("#txt_jobPosition").autocomplete({
            source: function (request, response) {
                var param = { keyword: $('#txt_jobPosition').val() };
                $.ajax({
                    url: "/BiddingNews/GetJobPositionKeyWord",
                    data: JSON.stringify(param),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        var itemArray = new Array();
                        for (var i = 0; i < data.length; i++) {
                            itemArray[i] = { label: data[i], value: data[i], data: data[i] }
                        }

                        response(itemArray);
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus);
                    }
                });
            },
            select: function (event, ui) {
                if (ui.item) {
                    //GetCustomerDetails(ui.item.value);
                }
            },
            minLength: 2
        });
    }

    return {
        init: init,
        search: search,
        changePageOnListBidding: changePageOnListBidding,
        getJobPositionKeyWord: getJobPositionKeyWord,
        initBiddingNewsSearchResult: initBiddingNewsSearchResult,
        fillKeyWordsSearchTextbox: fillKeyWordsSearchTextbox
    }
}();