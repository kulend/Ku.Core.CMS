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
            return parseFloat(0).toFixed(num);
        }
        var v = parseFloat(value.replace(/[^\-?\d.]/g, '')).toFixed(num);
        if (v === "NaN")
        {
            v = parseFloat(0).toFixed(num);
        }
        return v;
    };

    //锁屏
    vino.page.pageLock = function (callback) {
        if (self != top) {
            parent.vino.page.pageLock(callback);
        } else {
            layui.use('layer', function () {
                var layer = layui.layer;

                //锁屏
                var username = vino.page.cookie.get("user.name") || "&nbsp;";
                var userimage = vino.page.cookie.get("user.headimage") || "/images/user_default.png";
                var formId = "form" + new Date().getTime();
                var PageLockLayerId = layer.open({
                    title: false,
                    type: 1,
                    content: '<div class="admin-header-lock">' +
                        `<div class="admin-header-lock-img"><img src="${userimage}"/></div>` +
                        `<div class="admin-header-lock-name">${username}</div>` +
                        `<div class="input_btn"><form id="${formId}" class="layui-form" action="/Account/PageUnlock" method="post">` +
                        '<input type="password" class="admin-header-lock-input layui-input" lay-verify="required" autocomplete="off" placeholder="请输入密码.." name="Password"/>' +
                        '<button class="layui-btn" lay-submit >解 锁</button>' +
                        '</form></div>' +
                        '</div>',
                    closeBtn: 0,
                    shade: 0.9
                });

                $(".admin-header-lock-input").focus();

                // 解锁
                $("#" + formId).vinoForm({
                    onSuccess: function (reply, options) {
                        if (callback) {
                            callback();
                        }
                        layer.close(PageLockLayerId);
                    }
                });
            });
        }
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

/// <summary>cookie helper.基础jquery.cookie</summary>
(function () {
    var cookie = {
        get: function (key) {
            return $.cookie(key);
        },
        add: function (key, value, options) {
            options = options || {};
            this.remove(key, options);
            if (options) {
                if (typeof options.path == "undefined") {
                    options.path = "/";
                }
            }
            return $.cookie(key, value, options);
        },
        remove: function (key, options) {
            options = options || {};

            if (options) {
                if (typeof options.path == "undefined") {
                    options.path = "/";
                }
            }
            return $.removeCookie(key, options);
        }
    };
    vino.page.cookie = cookie;
})();