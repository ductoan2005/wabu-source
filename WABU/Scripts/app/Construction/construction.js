var constructionJS = function () {
    function init() { }

    function getConstructionDetail(id) {
        $("#EditConstruction").html('');
        var data = {
            id: id
        };

        $.ajax({
            type: "POST",
            url: "/Construction/GetConstructionDetailById",
            data: data,
            success: function (responsed) {
                if (responsed) {
                    $("#EditConstruction").append(responsed);
                    $("#EditConstruction").modal("show");
                    $('#EditBuildingPermitDateTime').datepicker({
                        todayBtn: "linked",
                        keyboardNavigation: false,
                        forceParse: false,
                        calendarWeeks: false,
                        autoclose: true,
                        format: "dd/mm/yyyy"
                    });
                }
            }
        });
    }

    function changePageOnListConstruction(pageSelected, e) {
        e.preventDefault();
        var data = {
            page: pageSelected
        };

        $.ajax({
            type: "POST",
            url: "/Construction/GetPagingConstructionList",
            data: data,
            success: function (responsed) {
                $("#tableConstruction").html(responsed.patialView);
            }
        });
    }

    function createConstruction() {
        if (validForm("frmAddConstruction")) {
            var dataForm = new FormData($("form#frmAddConstruction")[0]);
            $.ajax({
                type: "POST",
                url: "/Construction/AddNewConstruction",
                data: dataForm,
                dataType: 'json',
                contentType: false,
                processData: false,
                success: function (resultData) {
                    if (resultData && resultData.succeed === "0") {
                        $("#CreateConstruction").modal("hide");
                        toastr.success("Thêm công trình thành công.");
                        changePageOnListConstruction(1, event);
                    }
                    else {
                        toastr.error(resultData);
                    }
                }
            });
        }
    }

    function updateConstruction() {
        if (validForm("frmEditConstruction")) {
            var dataForm = new FormData($("form#frmEditConstruction")[0]);
            $.ajax({
                type: "POST",
                url: "/Construction/UpdateConstruction",
                data: dataForm,
                dataType: 'json',
                contentType: false,
                processData: false,
                success: function (resultData) {
                    if (resultData && resultData.succeed === "0") {
                        $("#EditConstruction").modal("hide");
                        toastr.success("Cập nhật công trình thành công.");
                        changePageOnListConstruction(1, event);
                    }
                    else {
                        toastr.error(resultData);
                    }
                }
            });
        }
    }

    function deleteConstruction(id) {
        var data = {
            id: id
        };
        if (confirm("Bạn muốn xóa công trình đang chọn?")) {
            $.ajax({
                type: "POST",
                url: "/Construction/DeleteConstructionById",
                data: data,
                async: false,
                success: function (responsed) {
                    if (responsed && responsed.succeed === "0") {
                        toastr.success("Xóa công trình thành công.");
                        changePageOnListConstruction(1, event);
                    }
                    if (responsed && responsed.succeed === "1") {
                        toastr.warning("Công trình này đã được đấu thầu");
                    }
                }
            });
        }
    }

    return {
        init: init,
        getConstructionDetail: getConstructionDetail,
        changePageOnListConstruction: changePageOnListConstruction,
        createConstruction: createConstruction,
        deleteConstruction: deleteConstruction,
        updateConstruction: updateConstruction
    };
}();