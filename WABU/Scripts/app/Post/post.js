"use strict";

let postManagement = function () {

    let sortOptionDefault = [{
        "FieldName": "DateUpdated",
        "SortType": "desc"
    }];

    function init() {
    }

    function initPostSearchResult(e) {
        e.preventDefault();

        let data = JSON.stringify({
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

        let title = $("input[name='Title']").val();
        let username = $("input[name='Username']").val();
        let isEnable = document.getElementById("IsEnableP").checked;
        let createdDate = $("input[name='PostCreatedDatetime']").val();
        let lastUpdatedDate = $("input[name='PostUpdatedDatetime']").val();

        let conditionStr = JSON.stringify({
            Title: title, Username: username, IsEnable: isEnable, CreatedDate: createdDate, LastUpdatedDate: lastUpdatedDate
        });

        let data = JSON.stringify({
            page: 1,
            "condition": conditionStr,
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
    // Direct về modal create post
    function directToPartialViewCreate() {
        $.ajax({
            type: "GET",
            url: "/Posts/Create",
            async: false
        });
    }

    //Tạo mới bài viết
    function createPost() {
        if (validForm("frmPost")) {
            let dataForm = $("#frmPost").serialize();

            $.ajax({
                type: "POST",
                url: "/Posts/Create",
                data: dataForm,
                //dataType: "text",
                async: false,
                success: function (resultData) {
                    if (resultData && resultData.succeed === "0") {

                        let authority = $("#Authority").val();
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