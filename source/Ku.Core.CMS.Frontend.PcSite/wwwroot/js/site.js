$(document).ready(function () {
    $("[come-soon]").on("click", function () {
        ku.page.msg.tip("页面完善中，请稍后!");
    });

    $(".action-show-wxqrcode").on("click", function () {
        layer.open({
            title: '扫码关注',
            type: 1,
            content: '<div><img src="/images/2code.jpg" /></div>',
            closeBtn: 1,
            shade: 0.8,
            shadeClose :true
        });
    });
});