layui.use(['element', 'form', 'layer'], function () {
    var $ = layui.jquery
        , layer = layui.layer
        , element = layui.element()
        , form = layui.form();

    //绑定form
    if ($("#inputForm").length) {
        $("#inputForm").vinoForm({
            onAfter: function (reply, options) {
                vino.page.msg.tip('保存成功！', function () {
                    var index = parent.layer.getFrameIndex(window.name);
                    parent.layer.close(index);
                    var fn = parent.winfns[index];
                    if (fn) {
                        fn(reply);
                    }
                    parent.winfns[index] = null;
                });
            }
        });
    }

    //关闭弹出
    $(".acton-close").on("click", function() {
        var index = parent.layer.getFrameIndex(window.name);
        parent.layer.close(index);
        parent.winfns[index] = null;
    });
});

function OpenWindow(title, src, options, onClose) {
    parent.OpenWindow(title, src, options, onClose);
}