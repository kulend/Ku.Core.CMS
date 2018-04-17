layui.use(['element', 'form', 'layer'], function () {
    var $ = layui.jquery
        , layer = layui.layer
        , element = layui.element
        , form = layui.form;

    $(document).on("click", "button.layui-btn:not([lay-submit]), .layui-action", function(e) {
        var action = $(this).data("action") || $(this).attr("action") || "";
        var after = $(this).attr("after");
        _action(this, action, after);
    });
});

function OpenWindow(title, src, options, onClose) {
    parent.OpenWindow(title, src, options, onClose);
}

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
        alert(method);
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
        _action(that, pms[1], after, { id: ids});
    } else if (action) {
        //直接跳转页面
        vino.page.navigateTo(action);
    }
}
