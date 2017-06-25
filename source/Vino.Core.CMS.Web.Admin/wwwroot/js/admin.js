layui.use(['element', 'form'], function () {
    var $ = layui.jquery
        , layer = layui.layer
        , element = layui.element()
        , form = layui.form();

    element.on('tab(F_sub_tab)', function (data) {
        window.location.href = $(this).data('url');
    });

    //绑定form
    if ($("#inputForm").length) {
        $("#inputForm").vinoForm({
            onAfter: function (reply, options) {
                vino.page.msg.tip('保存成功！', null, 2000, function () {
                    vino.page.navigateTo("Index", true);
                });
            }
        });
    }

    $(document).on("click", "button[data-action]", function(e) {
        var action = $(this).data("action"), actionName = $(this).text(), url = $(this).data('url');
        var method = $(this).data("method") || "post";
        var msg = "确定要进行" + actionName + "操作？";
        layer.confirm(msg, function (index) {
            if ("GET" === method || "get" === method) {
                vino.ajax.get(url,
                    null,
                    function (reply) {
                        if (reply.code == 0) {
                            vino.page.msg.tip("操作成功！",
                                null,
                                2000,
                                function () {
                                    location.reload();
                                });
                        } else {
                            vino.page.msg.tip(reply.message);
                        }
                    });
            } else {
                vino.ajax.post(url,
                    null,
                    function (reply) {
                        if (reply.code == 0) {
                            vino.page.msg.tip("操作成功！",
                                null,
                                2000,
                                function () {
                                    location.reload();
                                });
                        } else {
                            vino.page.msg.tip(reply.message);
                        }
                    });
            }
        });
    });
});





function alertObj(obj) {
    var description = "";
    for (var i in obj) {
        var property = obj[i];
        description += i + " = " + property + "\n";
    }
    alert(description);
}