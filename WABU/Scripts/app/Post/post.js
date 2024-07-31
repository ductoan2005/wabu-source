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
                $('#filterPostTable').DataTable({});
                toastr.success("Thành Công.");
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

    //Tạo mới bài viết
    function createPost() {
        if (validForm("frmPost")) {

            let title = $("input[name='Title']").val();
            let username = $("input[name='Username']").val();
            let content = $("textarea[name='Content']").val();
            let isEnable = document.getElementById("IsDeleted").checked ?? false;

            let conditionStr = JSON.stringify({
                Title: title, Username: username, IsDeleted: isEnable, Content: content
            });

            let dataForm = $("#frmPost").serialize();

            dataForm.IsDeleted = isEnable;
            $.ajax({
                type: "POST",
                url: "/Posts/Create",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: conditionStr,
                async: false,
                success: function (resultData) {
                    directToIndex();
                    toastr.success("Thành Công.");
                    //else {
                    //    toastr.error(resultData);
                    //}
                }
            });
        }
    }

    //Chỉnh sửa bài viết
    function updatePost(id) {
        if (validForm("frmPost")) {

            let title = $("input[name='Title']").val();
            let username = $("input[name='Username']").val();
            let content = $("textarea[name='Content']").val();
            let isEnable = document.getElementById("IsDeleted").checked ?? false;

            let conditionStr = JSON.stringify({
                Title: title, Username: username, IsDeleted: isEnable, Content: content, Id: id
            });

            let dataForm = $("#frmPost").serialize();

            dataForm.IsDeleted = isEnable;
            $.ajax({
                type: "POST",
                url: "/Posts/Edit",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: conditionStr,
                async: false,
                success: function (resultData) {
                    //directToIndex();
                    toastr.success("Thành Công.");
                    //if (resultData && resultData.succeed === "0") {

                        
                    //}
                    //else {
                    //    toastr.error(resultData);
                    //}
                }
            });
        }
    }

    //Xóa bài viết
    function deletePost(id) {
        if (validForm("frmPost")) {
            let conditionStr = JSON.stringify({
                Id: id
            });

            let dataForm = $("#frmPost").serialize();

            dataForm.IsDeleted = isEnable;
            $.ajax({
                type: "DELETE",
                url: "/Posts/Delete",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: conditionStr,
                async: false,
                success: function (resultData) {
                    if (resultData && resultData.succeed === "0") {

                        directToIndex();
                        toastr.success("Thành Công.");
                        }
                    else {
                        toastr.error(resultData);
                    }
                }
            });
        }
    }

    //showConfirmDeleteBox
    function showConfirmDeleteBox(id) {
        $('#confirmUpdateBox button[data-bb-handler="confirm"][type="button"]').unbind("click");
        if (validForm(formId)) {
            $("#openConfirmUpdateBox").click();
            $('#confirmUpdateBox button[data-bb-handler="confirm"][type="button"]').click(function (id) {
                updateUser(id);
            });
        }
    }

    return {
        init: init,
        initPostSearchResult: initPostSearchResult,
        filterPostSearch: filterPostSearch,
        directToIndex: directToIndex,
        createPost: createPost,
        updatePost: updatePost,
        showConfirmDeleteBox: showConfirmDeleteBox,
        deletePost: deletePost
    }
}();