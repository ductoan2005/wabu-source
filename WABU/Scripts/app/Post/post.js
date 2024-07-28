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
    // Direct về modal create post
    function directToPartialViewCreate() {
        $.ajax({
            type: "POST",
            url: "/Posts/DirectToPartialCreateView",
            async: false,
            success: function (data) {
                $('#frmPost').html(data.patialView);
                $("#frmPost").modal("show");
            },
            error: function (xhr, status, error) {
                console.error("Error: " + error);
            }
        });
    }

    //Tạo mới bài viết
    function createPost() {
        if (validForm("frmPost")) {
            var dataForm = $("#frmPost").serialize();

            $.ajax({
                type: "POST",
                url: "/Posts/Create",
                data: dataForm,
                //dataType: "text",
                async: false,
                success: function (resultData) {
                    if (resultData && resultData.succeed === "0") {

                        var authority = $("#Authority").val();
                        switch (authority) {
                            case "0":
                            case "1":
                                changePageOnListAdmin(1, event);
                                break;
                            case "2":
                                changePageOnListInvestor(1, event);
                                break;
                            case "3":
                                changePageOnListContractors(1, event);
                                break;
                            default:
                                break;
                        }

                        toastr.success("Thành Công.");
                        clearFormAfterCreate();
                    }
                    else {
                        toastr.error(resultData);
                    }
                }
            });
        }
    }

    return {
        init: init,
        initPostSearchResult: initPostSearchResult,
        filterPostSearch: filterPostSearch,
        directToPartialViewCreate: directToPartialViewCreate,
        createPost: createPost
    }
}();