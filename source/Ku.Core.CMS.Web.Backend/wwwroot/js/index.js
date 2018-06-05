layui.use(['element', 'layer'], function () {
    var element = layui.element;
    var layer = layui.layer;

    //打开首页
    openTab('profile', { id: 'profile', title: '基本资料', url:'/Account/Profile' });

    // 菜单点击
    element.on('nav(layadmin-system-side-menu)', function () {
        _openNavMenu({
            id: $(this).attr('data-id'),
            title: $(this).text(),
            url: $(this).attr('data-url'),
            reload: true
        });
    });

    element.on('tab(F_index_tab)', function () {
        var id = $(this).attr('lay-id');
        var $tabTitle = $("#LAY_app_tabsheader");
        $tabTitle.children("li[lay-id='" + id + "']").addClass("layui-this").siblings().removeClass("layui-this");

        //侧栏选中
        $(`#LAY-system-side-menu .layui-this`).removeClass("layui-this");
        $(`#LAY-system-side-menu [data-id='${id}']`).addClass("layui-this");
    });

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

    //锁屏
    $("a[layadmin-event='lockpage']").on("click", function () {
        ku.ajax.post("/Account/Lock", null, function () {
            ku.page.pageLock();
        });
    });

    //便签
    var note = layui.data('note');
    var content = note.index || "";
    $("#LAY_adminNote textarea").val(content);
    $("a[layadmin-event='note']").on("click", function () {
        _showNote();
    });
});

//打开新的Tab页面
function openTab(tag, options) {
    var opts = { reload:true};
    var $menu = $("#LAY-system-side-menu .layui-nav-item a[data-tag='" + tag + "']");
    if ($menu.length) {
        opts = $.extend(opts, {
            id: $menu.attr('data-id'),
            title: $menu.text(),
            url: $menu.attr('data-url'),
        });
    }
    opts = $.extend(opts, options);

    _openNavMenu(opts);
}

function _openNavMenu(options) {
    var $tabTitle = $("#LAY_app_tabsheader");
    if (options.id) {
        // 是否已经存在
        if (!$tabTitle.children("li[lay-id='" + options.id + "']").length) {
            _addNavTab(options.title, options.id, options.url);
        } else {
            if (options.reload) {
                $('#LAY_app_iframe_' + options.id).attr('src', options.url);
            }
        }

        // 切换到当前选项卡
        layui.element.tabChange('F_index_tab', options.id);
    }
}

function _addNavTab(text, id, url) {
    var $tabTitle = $("#LAY_app_tabsheader");
    var title = `<li lay-id="${id}" class="layui-this"><span>${text}</span><i class="layui-icon layui-unselect layui-tab-close">ဆ</i></li>`;
    var content = `<iframe id="LAY_app_iframe_${id}" data-id="${id}" frameborder="0" src="${url}"></iframe>`;
    var titleHide = text + '<i class="layui-icon layui-unselect layui-tab-close" data-id="' + id + '">&#x1006;</i>';

    layui.element.tabAdd('F_index_tab', {
        title: titleHide
        , content: content
        , id: id
    });

    $tabTitle.append(title);

    //监听Tab点击事件
    $tabTitle.find('li').off("click").on('click', function () {
        var selId = $(this).attr('lay-id');
        // 切换到当前选项卡
        layui.element.tabChange('F_index_tab', selId);
    });

    //监听Tab关闭事件
    $tabTitle.find('i.layui-tab-close').off("click").on('click', function () {
        layui.element.tabDelete('F_index_tab', $(this).closest("li").attr('lay-id'));
        $(this).closest("li").remove();
    });

    layui.element.tabChange('F_index_tab', id);
}

function _showNote() {
    layer.open({
        type: 1,
        shade: false,
        offset: 'rt',
        title: "便签", //不显示标题
        content: $("#LAY_adminNote"),
        cancel: function () {
            layui.data('note', {
                key: 'index'
                , value: $("#LAY_adminNote textarea").val()
            });
        }
    });
}