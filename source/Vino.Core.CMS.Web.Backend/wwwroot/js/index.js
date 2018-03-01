layui.use('element', function () {
    var element = layui.element;

    _addNavTab("首页", 'home', '/Account/PasswordEdit');

    // 菜单点击
    element.on('nav(layadmin-system-side-menu)', _openNavMenu);

    element.on('tab(F_index_tab)', function () {
        var id = $(this).attr('lay-id');
        var $tabTitle = $("#LAY_app_tabsheader");
        $tabTitle.children("li[lay-id='" + id + "']").addClass("layui-this").siblings().removeClass("layui-this");

        //侧栏选中
        $(`#LAY-system-side-menu .layui-this`).removeClass("layui-this");
        $(`#LAY-system-side-menu [data-id='${id}']`).addClass("layui-this");
    });

    function _addNavTab(text, id, url) {
        var $tabTitle = $("#LAY_app_tabsheader");
        var title = `<li lay-id="${id}" class="layui-this"><span>${text}</span><i class="layui-icon layui-unselect layui-tab-close">ဆ</i></li>`;
        var content = `<iframe id="LAY_app_iframe_${id}" data-id="${id}" frameborder="0" src="${url}"></iframe>`;
        var titleHide = text + '<i class="layui-icon layui-unselect layui-tab-close" data-id="' + id + '">&#x1006;</i>';

        element.tabAdd('F_index_tab', {
            title: titleHide
            , content: content
            , id: id
        });

        $tabTitle.append(title);

        //监听Tab点击事件
        $tabTitle.find('li').off("click").on('click', function () {
            var selId = $(this).attr('lay-id');
            // 切换到当前选项卡
            element.tabChange('F_index_tab', selId);
        });

        //监听Tab关闭事件
        $tabTitle.find('i.layui-tab-close').off("click").on('click', function () {
            element.tabDelete('F_index_tab', $(this).closest("li").attr('lay-id'));
            $(this).closest("li").remove();
        });

        element.tabChange('F_index_tab', id);
    }

    function _openNavMenu() {
        var elem = $(this)
            , url = elem.data('url')
            , id = elem.attr('data-id')
            , $tabTitle = $("#LAY_app_tabsheader");
        // 是否已经存在
        if (!$tabTitle.children("li[lay-id='" + id + "']").length) {
            _addNavTab(elem.text(), id, url);
        } else {
            $('#LAY_app_iframe_' + id).attr('src', url);
        }

        // 切换到当前选项卡
        element.tabChange('F_index_tab', id);
    }

    //侧边伸缩
    $("a[layadmin-event='flexible']").on("click", function () {
        var w = $(window).width();
        if (w > 992) {
            $("#LAY_app").toggleClass("layadmin-side-shrink");
        } else {
            $("#LAY_app").toggleClass("layadmin-side-spread-sm");
        }
        $("#LAY_app_flexible").toggleClass("layui-icon-shrink-right layui-icon-spread-left");
    });

    $(window).resize(function () {
        var w = $(window).width();
        //var left = $(".layui-side-menu").offset().left;
        if (w <= 992 && $("#LAY_app").hasClass("layadmin-side-shrink")) {
            $("#LAY_app").removeClass("layadmin-side-shrink");
        }
        if (w <= 992 && !$("#LAY_app").hasClass("layadmin-side-spread-sm") && $("#LAY_app_flexible").hasClass("layui-icon-shrink-right")) {
            $("#LAY_app_flexible").toggleClass("layui-icon-shrink-right layui-icon-spread-left");
        }
        if (w > 992 && !$("#LAY_app").hasClass("layadmin-side-shrink") && $("#LAY_app_flexible").hasClass("layui-icon-spread-left")) {
            $("#LAY_app_flexible").toggleClass("layui-icon-shrink-right layui-icon-spread-left");
        }
    });

    //侧边栏hover绑定
    $(".layui-nav a[lay-tips]").hover(function () {
        if ($("#LAY_app").hasClass("layadmin-side-shrink"))
        {
            var that = this;
            var msg = $(this).attr("lay-tips");
            layer.tips(msg, that);
        }
    }, function () {
        layer.closeAll('tips');
    });

    $("#LAY-system-side-menu .layui-nav-item a").on("click", function () {
        if ($("#LAY_app").hasClass("layadmin-side-shrink")) {
            $(this).closest("li.layui-nav-item").addClass("layui-nav-itemed").siblings().removeClass("layui-nav-itemed");
            $("#LAY_app").toggleClass("layadmin-side-shrink");
            $("#LAY_app_flexible").toggleClass("layui-icon-shrink-right layui-icon-spread-left");
        }
    });

    //标签页处理
    element.on('nav(layadmin-pagetabs-nav)', function () {
        var event = $(this).attr("layadmin-event");
        if (event == "closeThisTabs")
        {
            //关闭当前页面
            let li = $("#LAY_app_tabsheader").children("li.layui-this");
            let id = li.attr("lay-id");
            element.tabDelete('F_index_tab', id);
            li.remove();
        }
        else if (event == "closeOtherTabs") {
            //关闭其他页面
            $("#LAY_app_tabsheader").children("li.layui-this").siblings().not("[lay-id='home']").each(function () {
                let id = $(this).attr("lay-id");
                element.tabDelete('F_index_tab', id);
                $(this).remove();
            });
        }
        else if (event == "closeAllTabs") {
            //关闭全部页面
            $("#LAY_app_tabsheader").children("li").siblings().not("[lay-id='home']").each(function () {
                let id = $(this).attr("lay-id");
                element.tabDelete('F_index_tab', id);
                $(this).remove();
            });
        }
        $(this).parent().removeClass("layui-show");
    });

    //绑定事件
    $(document).on("click", ".layui-action", function (e) {
        var action = $(this).data("action") || $(this).attr("action") || "";
        var after = $(this).attr("after");
        if (action.indexOf("window:") === 0) {
            OpenWindow($(this).attr("title") || $(this).text() || "&nbsp;", action.substring(7), null, function (value) {
                eval("var $data;");
                $data = value;
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
                function () {
                    if ("get" === method) {
                        vino.ajax.get(url, null, function (reply) {
                            if (reply.code === 0) {
                                vino.page.msg.tip("操作成功！",
                                    function () {
                                        eval("var $data;");
                                        $data = reply;
                                        if (after) {
                                            eval(after);
                                        }
                                    });
                            } else {
                                vino.page.msg.tip(reply.message);
                            }
                        });
                    } else {
                        vino.ajax.post(url, null, function (reply) {
                            if (reply.code === 0) {
                                vino.page.msg.tip("操作成功！",
                                    function () {
                                        eval("var $data;");
                                        $data = reply;
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