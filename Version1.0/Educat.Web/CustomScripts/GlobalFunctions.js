function callServer(actionName, param, success) {
    param = ko.toJSON(param);
    $.ajax({
        url: '/Data/' + actionName,
        type: 'post',
        data: param,
        contentType: 'application/json',
        success: function (data) {
            var temp = data != "" ? $.parseJSON(data) : {};
            success(temp);
        }
    });
};

var delay = (function () {
    var timeoutId = 0;
    return function (callback, ms) {
        clearTimeout(timeoutId);
        timeoutId = setTimeout(callback, ms);
    }
})();