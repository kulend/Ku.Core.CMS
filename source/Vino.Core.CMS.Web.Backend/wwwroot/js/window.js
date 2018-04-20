layui.use(['element', 'form', 'layer', 'laydate'], function () {
    var $ = layui.jquery
        , layer = layui.layer
        , element = layui.element
        , laydate = layui.laydate
        , form = layui.form;

    //关闭弹窗
    $(".action-close").on("click", function() {
        closeWindow();
    });
});

//关闭弹窗
function closeWindow(data) {
    var index = parent.layui.layer.getFrameIndex(window.name);
    parent.layui.layer.close(index);
    var fn = parent.winfns[index];
    if (fn && data) {
        fn(data);
    }
    parent.winfns[index] = null;
}