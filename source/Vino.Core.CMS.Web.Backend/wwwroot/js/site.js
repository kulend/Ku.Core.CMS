layui.use(['element', 'form', 'layer', 'laydate'], function () {
    var $ = layui.jquery
        , layer = layui.layer
        , element = layui.element
        , form = layui.form;

    //layui-action
    $(document).on("click", "button.layui-btn:not([lay-submit]), .layui-action", function (e) {
        var action = $(this).data("action") || $(this).attr("action") || "";
        var after = $(this).attr("after");
        _action(this, action, after);
    });

    //绑定form
    $("form[auto-bind=true]").each(function () {
        _bindForm($(this));
    });

    //日期空间
    $(".layui-input.laydate").each(function () {
        var self = $(this);
        var type = self.data("type") || 'date';
        var format = self.data("format") || 'yyyy-MM-dd';
        laydate.render({
            elem: self[0],
            type: type,
            format: format
        });
    });
});

function _action(that, action, after, data) {
    var act = action.indexOf(":") > 0 ? action.substring(0, action.indexOf(":")) : "";
    var method = action.indexOf(":") > 0 ? action.substring(action.indexOf(":") + 1) : "";
    var pms = [];
    if (method.indexOf("[") >= 0) {
        pms = eval(method.substring(method.indexOf("["), method.indexOf("]") + 1));
        method = method.substring(0, method.indexOf("["));
    }
    if (act === "window") {
        OpenWindow($(that).attr("title") || "&nbsp;", method, null, function (value) {
            if (after) {
                _action(that, after, null, value);
            }
        });
    } else if (act === "javascript" || act === "js") {
        eval("var $this;");
        eval("var $data;");
        $this = that;
        $data = data;

        eval(method);
    } else if (act === "post" || act === "get") {
        var url = action.indexOf(":") > 0 ? action.substring(action.indexOf(":") + 1) : "";
        var confirmMsg = `确定要进行${$(that).attr("title") || "当前"}操作？`;
        vino.page.msg.confirm(confirmMsg,
            function () {
                if ("get" === act) {
                    vino.ajax.get(url, data || null, function (reply) {
                        if (reply.code === 0) {
                            vino.page.msg.tip("操作成功！",
                                function () {
                                    if (after) {
                                        _action(that, after, null, reply);
                                    }
                                });
                        } else {
                            vino.page.msg.tip(reply.message);
                        }
                    });
                } else {
                    vino.ajax.post(url, data || null, function (reply) {
                        if (reply.code === 0) {
                            vino.page.msg.tip("操作成功！",
                                function () {
                                    if (after) {
                                        _action(that, after, null, reply);
                                    }
                                });
                        } else {
                            vino.page.msg.tip(reply.message);
                        }
                    });
                }
            });
    } else if (act === "grid") {
        //数据表格处理
        if ("reload" === method) {
            if (pms.length > 0) {
                $(pms[0]).vinoGrid("reload");
            } else {
                $(".vino-grid").vinoGrid("reload");
            }
        }
    } else if (act === "batch") {
        //批量处理
        var tableId = pms[0];
        var ids = $(tableId).vinoGrid("getCheckedIds");
        if (!ids || ids.length == 0) {
            vino.page.msg.tip("请至少选择一项！");
            return;
        }
        _action(that, pms[1], after, { id: ids });
    } else if (act === "tab") {
        //打开tab页面
        var tag = pms[0];
        if (pms.length === 1) {
            _openTab(tag);
        }
        var opts = {
            id: tag,
            title: pms[1],
            url: pms[2],
            reload: pms[3],
        };
        _openTab(tag, opts);
    } else if (action) {
        //直接跳转页面
        vino.page.navigateTo(action);
    }
}

//打开Tab页
function _openTab(tag, options) {
    if (self != top) {
        parent._openTab(tag, options);
    } else {
        openTab(tag, options);
    }
}
 
function _bindForm($from, options) {
    var options = $.extend({}, {
        onSuccess: function (reply, options) {
            vino.page.msg.tip('保存成功！', function () {
                if (_isWindow()) {
                    var index = parent.layer.getFrameIndex(window.name);
                    parent.layer.close(index);
                    var fn = parent.winfns[index];
                    if (fn) {
                        fn(reply);
                    }
                    parent.winfns[index] = null;
                } else {
                    //刷新页面
                    location.reload();
                }
            });
        }
    }, options);

    $from.vinoForm(options);
}

function _isWindow() {
    return $("body :first").hasClass("layui-layout-window");
}

if (!window.winfns) {
    //用于iframe页面调用js脚本
    window.winfns = {};
}
function OpenWindow(title, src, options, onClose) {
    if (self != top) {
        parent.OpenWindow(title, src, options, onClose);
    } else {
        var defaults = {
            area: ['900px', '550px']
            , shade: 0.3
            , maxmin: true
        }
        var opts = $.extend({}, defaults, options);
        layui.use(['layer'], function () {
            var layer = layui.layer;
            var index = layer.open({
                type: 2
                , title: title
                , area: opts.area
                , shade: opts.shade
                , maxmin: opts.maxmin
                , content: src
                , zIndex: layer.zIndex
                , success: function (layero) {
                    layer.setTop(layero);
                }
            });
            window.winfns[index] = onClose;
        });
    }   
}