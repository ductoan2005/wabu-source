var areaManageJS = function () {
    function getArea() {
        $.getJSON('/AreaManage/GetAllArea', null, function (lstarea) {
            if (lstarea != null && !jQuery.isEmptyObject(lstarea)) {
                $.each(lstarea,
                    function (index, area) {
                        $("#AreaId").append($('<option/>',
                            {
                                value: area.Value,
                                text: area.Text
                            }));
                    });
            };
        });
    }

    return {
        getArea: getArea
    };
}();