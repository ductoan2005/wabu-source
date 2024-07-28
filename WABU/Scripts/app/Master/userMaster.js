var userMaster = function () {

    //[STR]-----------------Common Variable-----------------
    var selectedTab = "1"; // default select admin(admin: 1, investor: 2, contractor:3)
    var conditionToSearchAdmin = ""; //save condition when click button search
    var conditionToSearchInvestor = "";
    var conditionToSearchContractors = "";
    //[END]-----------------Common Variable-----------------

    function init() {
    }

    //[STR]-----------------Common Function-----------------
    function clearFormAfterCreate() {
        $("#FullName").val("");
        $("#Authority").val("");
        $("#UserName").val("");
        $("#Password").val("");
        $("#Password-Confirm").val("");
        $("#DateOfBirth").val("");
        $("#Gender").val("");
        $("#PhoneNumber").val("");
        $("#Address").val("");
        $("#Email").val("");
        $("#Image").val("");
        //$('#IsActive').attr('checked', false);
    }

    function selectTab(indexListTab) {
        selectedTab = indexListTab;
    }
    //[END]-----------------Common Function-----------------

    //[STR]-----------------Create Function-----------------
    function createUser() {
        if (validForm("frmDetailUser")) {
            var dataForm = $("#frmDetailUser").serialize();

            $.ajax({
                type: "POST",
                url: "/UserMaster/Create",
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
    //[END]-----------------Create Function-----------------

    //[STR]-----------------Search Function-----------------
    function searchUser(pageSelected, e) {
        e.preventDefault();
        var conditionToSearch = "";
        switch (selectedTab) {
            case "1":
                conditionToSearchAdmin = conditionToSearch = $("#txtCondition-Search").val(); //save condition to paging
                //conditionToSearch = conditionToSearchAdmin;
                break;
            case "2":
                conditionToSearchInvestor = conditionToSearch = $("#txtCondition-Search").val();
                break;
            case "3":
                conditionToSearchContractors = conditionToSearch = $("#txtCondition-Search").val();
                break;
            default:
                break;
        }

        var data = {
            page: pageSelected,
            selectedTab: selectedTab,
            condition: conditionToSearch
        }

        $.ajax({
            type: "POST",
            url: "/UserMaster/ReadUsersToPagingByCondition",
            data: data,
            success: function (responsed) {
                switch (selectedTab) {
                    case "1":
                        $("#tab-admin").html(responsed.patialView);

                        break;
                    case "2":
                        $("#tab-investor").html(responsed.patialView);
                        break;
                    case "3":
                        $("#tab-contractors").html(responsed.patialView);
                        break;
                    default:
                        break;
                }
            }
        });
    }
    //[END]-----------------Search Function-----------------

    //[STR]-----------------Paging Function-----------------
    //paging admin
    function changePageOnListAdmin(pageSelected, e) {
        e.preventDefault();

        var data = {
            page: pageSelected,
            condition: conditionToSearchAdmin
        }

        $.ajax({
            type: "POST",
            url: "/UserMaster/ReadUsersToPagingAdmin",
            data: data,
            success: function (responsed) {
                $("#tab-admin").html(responsed.patialView);
            }
        });
    }

    //paging chu dau tu
    function changePageOnListInvestor(pageSelected, e) {
        e.preventDefault();

        var data = {
            page: pageSelected,
            condition: conditionToSearchInvestor
        }

        $.ajax({
            type: "POST",
            url: "/UserMaster/ReadUsersToPagingInvestor",
            data: data,
            success: function (responsed) {
                $("#tab-investor").html(responsed.patialView);
            }
        });
    }

    //paging nha thau
    function changePageOnListContractors(pageSelected, e) {
        e.preventDefault();

        var data = {
            page: pageSelected,
            condition: conditionToSearchContractors
        }

        $.ajax({
            type: "POST",
            url: "/UserMaster/ReadUsersToPagingContractors",
            data: data,
            success: function (responsed) {
                $("#tab-contractors").html(responsed.patialView);
            }
        });
    }
    //[END]-----------------Paging Function-----------------

    //[STR]-----------------Delete Function-----------------
    function deleteUser(idRow) {
        var idItemRow;
        switch (selectedTab) {
            case "1":
                idItemRow = jsonDataUserAdminJS[idRow].Id;
                break;
            case "2":
                idItemRow = jsonDataUserInvestorJS[idRow].Id;
                break;
            case "3":
                idItemRow = jsonDataUserContractorsJS[idRow].Id;
                break;
            default:
                break;
        }

        var dataDelete = {
            id: idItemRow
        }

        if (confirm("Bạn muốn xóa User đang chọn?")) {
            $.ajax({
                type: "POST",
                url: "/UserMaster/Delete",
                data: dataDelete,
                success: function (responsed) {
                    if (responsed && responsed.succeed) {
                        toastr.success("Thành Công.");
                        switch (selectedTab) {
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
                    }
                    else {
                        toastr.warning(responsed);
                    }
                }
            });
        }
    }
    //[END]-----------------Delete Function-----------------

    //[STR]-----------------Display account detail Function-----------------
    function displayAccountDetail(e, userId) {
        e.preventDefault();
        var data = { userId };
        $.ajax({
            type: "POST",
            url: "/UserMaster/DisplayAccountDetail",
            data: data,
            success: function (responsed) {
                if (responsed.succeed === "0") {
                    $("#tab-detailUser").html(responsed.patialView);
                }
            }
        });
    }
    //[END]-----------------Display account detail Function-----------------

    function parseJsonDate(jsonDateString) {
        return new Date(parseInt(jsonDateString.replace("/Date(", "")));
    }

    //[STR]-----------------Display Edit User Function-----------------

    function displayEditUser(userId) {
        var data = {
            userId: userId
        };

        $.ajax({
            type: "POST",
            url: "/UserMaster/DisplayEditUserPopup",
            data: data,
            success: function (responsed) {
                $("#popup-edit-user").html(responsed.patialView);
                $("#popup-edit-user").modal("show");
                $('#EditDateOfBirth').datepicker({
                    todayBtn: "linked",
                    keyboardNavigation: false,
                    forceParse: false,
                    calendarWeeks: false,
                    autoclose: true,
                    format: "dd/mm/yyyy"
                });
            }
        });
    }

    //[END]-----------------Display Edit User Function-----------------

    function updateUser() {
        var confirmPassword = $('#Password-Confirm').val();
        if (confirmPassword !== '') {
            var password = $('.new-password');
            if (password.val() !== confirmPassword) {
                $('#msg-err-Password-Confirm').html('');
                $('#msg-err-Password-Confirm').append('Password confirm và password bạn nhập không trùng nhau.');
                return;
            }

        }

        var dataForm = new FormData($("form#frmEditUser")[0]);
        var userId = dataForm.get('Id');

        $.ajax({
            type: "POST",
            url: "/UserMaster/Update",
            data: dataForm,
            dataType: 'json',
            contentType: false,
            processData: false,
            cache: false,
            success: function (resultData) {

                if (resultData && resultData.succeed === "0") {

                    var authority = resultData.authority.toString();
                    switch (authority) {
                        case "0":
                            break;
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

                    displayAccountDetail(event, userId);

                    $("#popup-edit-user").modal("hide");
                    toastr.success("Cập nhật thành Công.");
                    // clearFormAfterCreate();
                }
                else {
                    toastr.error(resultData);
                }
            }
        });
    }


    //showConfirmUpdateBox
    function showConfirmUpdateBox(event, formId, action) {
        event.preventDefault();
        $('#confirmUpdateBox button[data-bb-handler="confirm"][type="button"]').unbind("click");
        if (action === "edit") {
            triggerValidateEventForm(formId);
        }
        if (validForm(formId)) {
            $("#openConfirmUpdateBox").click();
            $('#confirmUpdateBox button[data-bb-handler="confirm"][type="button"]').click(function (event) {
                updateUser();
            });
        }
    }

    return {
        init: init,
        selectTab: selectTab,
        createUser: createUser,
        searchUser: searchUser,
        deleteUser: deleteUser,
        changePageOnListAdmin: changePageOnListAdmin,
        changePageOnListInvestor: changePageOnListInvestor,
        changePageOnListContractors: changePageOnListContractors,
        displayAccountDetail: displayAccountDetail,
        displayEditUser: displayEditUser,
        updateUser: updateUser,
        showConfirmUpdateBox: showConfirmUpdateBox
    }
}();