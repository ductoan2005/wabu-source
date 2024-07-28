var registerJS = function () {
    function init() {
        $('.i-checks').iCheck({
            checkboxClass: 'icheckbox_square-green',
            radioClass: 'iradio_square-green',
        });
    }

    function checkOnlyAlphaNumeric(inputtxt) {
        var letterNumber = /^[0-9a-zA-Z]+$/;
        if (inputtxt.match(letterNumber))
        {
            return true;
        }
        else {
            return false;
        }
    }

    function registerFunc() {
        //ClearMsgError(); remove mess
        if (validForm("frmRegister")) {
            $("#btnRegister").prop('disabled', true);

            var $userName = $("#UserName");
            if (!checkOnlyAlphaNumeric($userName.val())) {
                toastr.error("Username không được chứa ký tự đặc biệt.");
                $("#btnRegister").prop('disabled', false);
                return;
            }

            if ($("#auth-two-person").is(":checked") === false && $("#auth-two-company").is(":checked") === false && $("#auth-three").is(":checked") === false) {
                toastr.error("Vui lòng chọn quyền hạn người dùng, bạn là ai?");
                $("#btnRegister").prop('disabled', false);
                return;
            }

            if ($("#IsAgreeTerm").is(":checked") === false) {
                toastr.error("Bạn chưa đồng ý với thỏa thuận cả Want to Build?");
                $("#btnRegister").prop('disabled', false);
                return;
            }

            var antiForgeryVal = $('input[name="__RequestVerificationToken"]').val();
            var url = $("#returnURL").val();
            var data = {
                UserName: $("#UserName").val(),
                Password: $("#Password").val(),
                Email: $("#Email").val(),
                Authority: ($("#auth-two-person").is(":checked") === true || $("#auth-two-company").is(":checked") === true) ? $("#auth-two-person").val() : $("#auth-three").val(),
                IsPerson: $("#auth-two-person").is(":checked") !== true ? false : true,
                IsAgreeTerm: $("#IsAgreeTerm").is(":checked"),
                returnURL: url,
                __RequestVerificationToken: antiForgeryVal
            };

            $.post('/Register/Add', data, function (responsed) {
                if (responsed !== null && responsed !== "error") {
                    if (responsed.rs === "0") {
                        if (responsed.returnURL) {
                            location = "/confirmEmail";
                        }
                    } else if (responsed.rs === "1") {
                        toastr.error("Username đã tồn tại, vui lòng nhập lại");
                    } else if (responsed.rs === "2") {
                        toastr.error("Email đã được dùng, vui lòng chọn email khác");
                    } else if (responsed.rs === "3") {
                        toastr.error("Quá trình gửi email thất bại. Vui lòng liên hệ admin");
                    }

                }
            });
        }
        $("#btnRegister").prop('disabled', false);
        hideOverLoad();
    }

    //Create bidding detail
    function addUserInfo() {
        if (validForm("AddUserInformation")) {
            var dataForm = new FormData($("form#AddUserInformation")[0]);

            $.ajax({
                type: "POST",
                url: "/Register/AddUserInformation",
                data: dataForm,
                dataType: 'json',
                contentType: false,
                processData: false,
                success: function (responsed) {
                    if (responsed !== null && responsed !== "error") {
                        if (responsed.rs === "0") {
                            location = '/confirmEmail';
                        }
                        if (responsed.rs === "2") {
                            toastr.error("Username chưa tồn tại, vui lòng nhập lại");
                        }
                        else if (responsed.rs === "3") {
                            toastr.error("Quá trình gửi email thất bại. Vui lòng liên hệ admin");
                        }
                    }
                }
            });
        }
    }

    return {
        init: init,
        registerFunc: registerFunc,
        addUserInfo: addUserInfo
    };
}();

