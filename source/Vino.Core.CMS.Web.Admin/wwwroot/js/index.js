layui.use(['layer'], function () {
    var $ = layui.jquery, layer = layui.layer;
});

if (!window.winfns) {
    //用于iframe页面调用js脚本
    window.winfns = {};
}

function OpenWindow(title, src, options, onClose) {
    var defaults = {
        area: ['700px', '500px']
        , shade: 0.3
        , maxmin: true
    }
    var opts = $.extend({}, defaults, options);
    layui.use(['layer'], function () {
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