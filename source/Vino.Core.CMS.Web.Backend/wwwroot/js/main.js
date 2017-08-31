layui.use(['element', 'form', 'layer'], function () {
    var $ = layui.jquery
        , layer = layui.layer
        , element = layui.element
        , form = layui.form;

    $(document).on("click", "button.layui-btn:not([lay-submit])", function(e) {
        var action = $(this).data("action") || $(this).attr("action") || "";
        var after = $(this).attr("after");
        if (action.indexOf("window:") === 0) {
            OpenWindow($(this).attr("title") || "&nbsp;", action.substring(7), null, function () {
                if (after) {
                    eval(after);
                }
            });
        } else if (action.indexOf("javascript:") === 0 || action.indexOf("js:") === 0) {
            var js = action.substring(action.indexOf(":") + 1);
            eval(js);
        } else if (action.toLowerCase().indexOf("post:") === 0 || action.toLowerCase().indexOf("get:") === 0) {
            var method = action.substring(0, action.indexOf(":")).toLowerCase();
            var url = action.substring(action.indexOf(":") + 1);
            var confirmMsg = `确定要进行${$(this).attr("title") || "当前"}操作？`;
            vino.page.msg.confirm(confirmMsg,
                function() {
                    if ("get" === method) {
                        vino.ajax.get(url,
                            null,
                            function(reply) {
                                if (reply.code === 0) {
                                    vino.page.msg.tip("操作成功！",
                                        function() {
                                            if (after) {
                                                eval(after);
                                            }
                                        });
                                } else {
                                    vino.page.msg.tip(reply.message);
                                }
                            });
                    } else {
                        vino.ajax.post(url,
                            null,
                            function(reply) {
                                if (reply.code === 0) {
                                    vino.page.msg.tip("操作成功！",
                                        function() {
                                            if (after) {
                                                eval(after);
                                            }
                                        });
                                } else {
                                    vino.page.msg.tip(reply.message);
                                }
                            });
                    }
                });
        } else if (action) {
            //直接跳转页面
            vino.page.navigateTo(action);
        }
    });
});

function OpenWindow(title, src, options, onClose) {
    parent.OpenWindow(title, src, options, onClose);
}

function gridReload() {
    $(".vino-grid").vinoGrid("reload");
}

function grid_templet_bool(field) {
    return '<div>{{# if(d.' + field + '){ }} <i class="layui-icon">&#xe605;</i> {{# }else{ }} <i class="layui-icon">&#x1006;</i> {{# } }}</div>';
}

function grid_templet_operate(btns) {
    var html = '<div>';
    for (var i = 0; i < btns.length; i++) {
        html += createOperateBtn(btns[i]);
    }
    html += '</div>';
    return html;
}

function createOperateBtn(btn) {
    var authcode = btn.code;
    if (authcode) {
        if (!authCodes) {
            return "";
        }
        if (authCodes.indexOf('0ec3eaea52dc2a0cbc5008f712a11e25') < 0) {
            //检查权限
            var md5Code = hex_md5(authcode);
            if (authCodes.indexOf(md5Code) < 0) {
                return "";
            }  
        }
    }
    var isWarn = false;
    if (authcode && authcode.indexOf("delete") > 0) {
        isWarn = true;
    }
    return `<button class="layui-btn layui-btn-mini ${isWarn ? 'layui-btn-danger' : ''} ${btn.css}" action="${btn.action}" after="gridReload()">${btn.title}</button>`;
}