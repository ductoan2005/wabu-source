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

    //Init page
    function initUserPostsSearchResult(e) {
        e.preventDefault();

        let data = JSON.stringify({
            page: 1,
            "condition": "",
            "sortOption": sortOptionDefault
        });

        $.ajax({
            url: "/UserPosts/GetPostsByFilters",
            contentType: 'application/json; charset=utf-8',
            type: "GET",
            dataType: 'json',
            data: data,
            success: function (responsed) {
                $("#postsSearchResult").html(responsed.patialView);
                //updateOrderbyDay();
                ////Show recently search
                //showSearchKeyWord();
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
                $('#filterPostTable').DataTable({});
            }
        });
    }

    // User filter posts
    function userFilterPostSearch(e) {
        e.preventDefault();

        let title = $("input[name='Title']").val();

        let conditionStr = JSON.stringify({
            Title: title
        });

        let data = JSON.stringify({
            page: 1,
            "condition": conditionStr,
            "sortOption": sortOptionDefault
        });

        $.ajax({
            url: "/UserPosts/GetPostsByFilters",
            contentType: 'application/json; charset=utf-8',
            type: "POST",
            dataType: 'json',
            data: data,
            success: function (responsed) {
                $('#postsSearchResult').html(responsed.patialView);
            }
        });
    }
    // Direct về modal create post
    function directToIndex() {
        $.ajax({
            type: "GET",
            url: "/Posts/Index",
            async: false
        });
    }

    return {
        init: init,
        initPostSearchResult: initPostSearchResult,
        filterPostSearch: filterPostSearch,
        directToIndex: directToIndex,
        initUserPostsSearchResult: initUserPostsSearchResult,
        userFilterPostSearch: userFilterPostSearch
    }
}();