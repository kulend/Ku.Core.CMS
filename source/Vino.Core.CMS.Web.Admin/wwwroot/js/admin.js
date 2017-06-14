layui.use(['element', 'form'], function () {
    var $ = layui.jquery
        , layer = layui.layer
        , element = layui.element()
        , form = layui.form();

    element.on('tab(F_sub_tab)', function (data) {
        window.location.href = $(this).data('url');
    });

    //// 操作按钮组
    //var actions = {
    //    ajaxDelete: function (url) {
    //        jacommon.confirm('确认删除吗，此操作不可逆？', function () {
    //            jacommon.ajaxGet(url, function (res) {
    //                jacommon.success('删除成功', function () {
    //                    window.location.reload();
    //                });
    //            }, function (res) {
    //                jacommon.error('删除失败，' + res.msg + '(错误代码：' + res.errorCode + ')');
    //            })
    //        });
    //    }
    //}
    //$(document).on("click", ".do-action", function (e) {
    //    var type = $(this).data("type")
    //        , url = $(this).data('url');
    //    if (actions[type])
    //        actions[type](url);
    //    else
    //        window.location.href = url;
    //})

    //// 监听switch开关
    //form.on('switch(F_switch)', function (data) {
    //    var hdnId = $(data.elem).data('hdnid');
    //    $(hdnId).val(data.elem.checked ? 1 : 0);
    //});

    //// 监听checkbox
    //form.on('checkbox(F_cbx)', function (data) {
    //    $(data.elem).prop("checked", data.elem.checked);
    //});
    //form.on('checkbox(F_cbx_hdnid)', function (data) {
    //    var hdnId = $(data.elem).data('hdnid');
    //    //$(data.elem).prop("checked", data.elem.checked);
    //    $(hdnId).val(data.elem.checked ? 1 : 0);
    //});

    //// 提交表单
    //form.on('submit(F_do_ajax_submit)', function (data) {
    //    var postUrl = data.form.action
    //        , postData = data.field
    //        , listUrl = $(data.elem).data('listurl')
    //        , $btn = $(data.elem)
    //        , btnText = $btn.html();
    //    $btn.addClass('layui-btn-disabled').text('提交中...');
    //    jacommon.ajaxPost(postUrl, postData, function (res) {
    //        jacommon.success('保存成功', function () {
    //            window.location.href = listUrl;
    //        });
    //    }, function (res) {
    //        jacommon.error('保存失败，' + res.msg + '(错误代码：' + res.errorCode + ')');
    //    }, function () {
    //        $btn.removeClass('layui-btn-disabled').text(btnText);
    //    });
    //    return false;
    //});
    //form.on('submit(F_jquery_ajax_submit)', function (data) {
    //    var postUrl = data.form.action
    //        , postData = $(data.form).serialize()
    //        , listUrl = $(data.elem).data('listurl');
    //    jacommon.ajaxPost(postUrl, postData, function (res) {
    //        jacommon.success('保存成功', function () {
    //            window.location.href = listUrl;
    //        });
    //    }, function (res) {
    //        jacommon.error('保存失败，' + res.msg + '(错误代码：' + res.errorCode + ')');
    //    });
    //    return false;
    //});

});





function alertObj(obj) {
    var description = "";
    for (var i in obj) {
        var property = obj[i];
        description += i + " = " + property + "\n";
    }
    alert(description);
}