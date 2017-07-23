layui.use(['element', 'form', 'layer'], function () {
    var $ = layui.jquery
        , layer = layui.layer
        , element = layui.element()
        , form = layui.form();

    $(document).on("click", "button.layui-btn:not([lay-submit])", function(e) {
        var action = $(this).data("action") || $(this).attr("action") || "";
        if (action.indexOf("window:") === 0) {
            OpenWindow($(this).attr("title") || "&nbsp;", action.substring(7), null, function () {

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

                                        });
                                } else {
                                    vino.page.msg.tip(reply.message);
                                }
                            });
                    }
                });
        } else {
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