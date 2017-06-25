if (!window.vino) {
    window.vino = {};
}
/*通用方法*/
if (!vino.page) {
    vino.page = {};
}

(function () {
    var loadIndex;
    function _getLoading() {
        var $loading = $("#loading");
        if (!$loading.length) {
            $loading = $("<div class='loading' id='loading'><div class='spinner'><div class='bounce1'></div><div class='bounce2'></div><div class='bounce3'></div></div></div>");
            $(document.body).append($loading);
        }
        return $loading;
    }

    vino.page.msg = {
        tip: function (msg, icon, delay, callback) {
            var pm = {};
            if (icon) {
                pm.icon = icon;
            }
            pm.time = delay || 2000;
            layui.use('layer', function () {
                var layer = layui.layer;
                layer.msg(msg, pm, callback);
            });   
        },
        showLoad: function () {
            layui.use('layer', function () {
                var layer = layui.layer;
                loadIndex = layer.load(0);
            });
        },
        hideLoad: function () {
            layui.use('layer', function () {
                var layer = layui.layer;
                loadIndex = layer.close(loadIndex);
            });
        }
    };

    /**
     * 页面跳转
     * @param {} url 
     * @param {} replace 是否去除后退记录
     * @returns {} 
     */
    vino.page.navigateTo = function (url, replace) {
        replace = replace || false;
        if (replace) {
            window.location.replace(url);
        } else {
            window.location.href = url;
        }
    };
})();