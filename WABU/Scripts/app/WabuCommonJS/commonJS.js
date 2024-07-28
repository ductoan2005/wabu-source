var commonJS = function () {

    //Required file input

    // Validate file type to upload
    function validateSingleInput(oInput, fileType) {
        var validFileExtensions;
        if (fileType === "image") {
            validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
        }
        else if (fileType === "file") {
            validFileExtensions = [".pdf"];
        }
        else if (fileType === "register") {
            validFileExtensions = [".pdf", ".rar", ".zip"];
        }
        if (oInput.type === "file") {
            var sFileName = oInput.value;
            if (sFileName.length > 0) {
                var blnValid = false;
                for (var j = 0; j < validFileExtensions.length; j++) {
                    var sCurExtension = validFileExtensions[j];
                    if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() === sCurExtension.toLowerCase()) {
                        blnValid = true;
                        break;
                    }
                }

                if (!blnValid) {
                    toastr.warning("Bạn chỉ được nhập file dưới định dạng " + validFileExtensions.join(", "));
                    oInput.value = "";
                    return false;
                }
            }
        }
        if (fileType === "image") {
            previewImage(oInput);
        }

        return true;
    }

    function previewImage(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                var imgId = $(input).next("img");
                $(imgId).attr('src', e.target.result);
            };

            reader.readAsDataURL(input.files[0]);
        }

    }

    var formatPriceValue = function (name) {

        //Format value price
        $('input[name = ' + name + ']').on('input', function (e) {
            $(this).val(formatCurrency(this.value.replace(/[, vnđ]/g, '')));
        }).on('keypress', function (e) {
            if (!$.isNumeric(String.fromCharCode(e.which))) e.preventDefault();
        }).on('paste', function (e) {
            var cb = e.originalEvent.clipboardData || window.clipboardData;
            if (!$.isNumeric(cb.getData('text'))) e.preventDefault();
        });
        function formatCurrency(number) {
            var n = number.split('').reverse().join("");
            var n2 = n.replace(/\d\d\d(?!$)/g, "$&,");
            return n2.split('').reverse().join('') + ' vnđ';
        }
    }
    var reFormatPriceValue = (value) => {
        return value.replace(/,/g, '').replace(/vnđ/g, '');
    }

    var appendDayText = function (name) {
        $('input[name = ' + name + ']').on('input', function (e) {
            $(this).val(formatDay(this.value.replace(/[, ngày]/g, '')));
        }).on('keypress', function (e) {
            if (!$.isNumeric(String.fromCharCode(e.which))) e.preventDefault();
        }).on('paste', function (e) {
            var cb = e.originalEvent.clipboardData || window.clipboardData;
            if (!$.isNumeric(cb.getData('text'))) e.preventDefault();
        });

        function formatDay(number) {
            var n = number.split('').reverse().join("");
            var n2 = n.replace(/\d\d\d(?!$)/g, "$&");
            return n2.split('').reverse().join('') + ' ngày';
        }
    }

    var reAppendDayText = (value) => {
        return value.replace(/ ngày/g, '');
    }
 

    return {
        validateSingleInput: validateSingleInput,
        formatPriceValue: formatPriceValue,
        appendDayText: appendDayText,
        reFormatPriceValue: reFormatPriceValue,
        reAppendDayText: reAppendDayText
    };
}();