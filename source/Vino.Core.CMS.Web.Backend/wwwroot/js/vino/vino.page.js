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
        tip: function (msg, callback, delay, options) {
            var opts = $.extend({}, options);
            opts.time = delay || 1500;
            layui.use('layer', function () {
                var layer = layui.layer;
                layer.msg(msg, opts, callback);
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
        },
        //询问框
        confirm: function (content, yes, cancel, options) {
            layui.use("layer", function () {
                var layer = layui.layer;
                layer.confirm(content, options, function(index) {
                    layer.close(index);
                    if (typeof (yes) === "function") {
                        yes();
                    }
                }, cancel);
            });
        },
        alert: function (content, yes, options) {
            layui.use("layer", function () {
                var layer = layui.layer;
                layer.alert(content, options, yes);
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

    vino.page.openWindow = function (options) {
        if (self != top) {
            parent.vino.page.openWindow(options);
        } else
        {
            layui.use('layer', function () {
                var layer = layui.layer;
                layer.open(options);
            }); 
        }
    };

    vino.page.fixedFloat = function (value, num) {
        if (!value)
        {
            value = "0";
        }
        return parseFloat(value.replace(/[^\-?\d.]/g, '')).toFixed(num || 2);
    };
})();

(function () {
    var querystring = {
        all: function () {
            if (!location.search) {
                return [];
            }
            var result = location.search.match(new RegExp("[\?\&][^\?\&]+=[^\?\&]+", "g"));
            if (result == null) {
                return "";
            }
            for (var i = 0; i < result.length; i++) {
                result[i] = result[i].substring(1);
            }
            return result;
        },
        get: function (name) {
            if (!location.search) {
                return null;
            }
            var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
            if (result == null || result.length < 1) {
                return null;
            }
            return result[1];
        },
        updateParam: function (url, name, value) {
            var r = url;
            if (r != null && r != 'undefined' && r != "") {
                value = encodeURIComponent(value);
                var reg = new RegExp("(^|)" + name + "=([^&]*)(|$)");
                var tmp = name + "=" + value;
                if (url.match(reg) != null) {
                    r = url.replace(eval(reg), tmp);
                }
                else {
                    if (url.match("[\?]")) {
                        r = url + "&" + tmp;
                    } else {
                        r = url + "?" + tmp;
                    }
                }
            }
            return r;
        }
    };
    vino.page.querystring = querystring;
})();