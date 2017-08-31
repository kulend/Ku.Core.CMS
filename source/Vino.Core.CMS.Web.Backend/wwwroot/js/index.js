$(function () {
    var element = layui.element;

    //iframe自适应
    $(window).on('resize', function () {
        windowResize();
    }).resize();

    // 菜单点击
    element.on('nav(index_nav_menu)', _openNavMenu);

    function _openNavMenu() {
        var elem = $(this)
            , url = elem.data('url')
            , id = elem.data('id')
            , $tabTitle = $("#J_index_tab_title");
        // 是否已经存在iframe
        if (!$tabTitle.children("li[lay-id='" + id + "']").length) {
            var title = elem.text() + '<i class="layui-icon layui-unselect layui-tab-close" data-id="' + id + '">&#x1006;</i>'
                , content = '<iframe id="J_iframe_' + id + '" data-id="' + id + '" frameborder="0" src="' + url + '"></iframe>';
            element.tabAdd('F_index_tab', {
                title: title
                , content: content
                , id: id
            });
            //监听Tab关闭事件
            $tabTitle.find('i.layui-tab-close').on('click', function () {
                element.tabDelete('F_index_tab', $(this).data('id'));
            });
        } else {
            $('#J_iframe_' + id).attr('src', url);
        }
        // 切换到当前选项卡
        element.tabChange('F_index_tab', id);
        // 关闭菜单
        $(".layui-side-xsm").hide();
    }

    $("#J_open_nav").on('click', function () {
        $(".layui-side-xsm").toggle();
    });

    //修改密码
    $("#pwd_edit").on("click", function () {
        OpenWindow("修改密码", "Account/PasswordEdit", { area: ['600px', '300px'] });
    });

    //放大缩小
    $(".layui-tab-action .maxwin").on("click", function () {
        var self = $(this);
        if (self.hasClass("layui-layer-max")) {
            //放大
            $(".layui-header").css("z-index", "9998");
            $(".layui-body").animate({
                top: '0px',
                left: '0px',
                bottom:'0px'
            }, "normal", function () {
                self.removeClass("layui-layer-max").addClass("layui-layer-maxmin");
                windowResize();
            });
        } else
        {
            //还原
            $(".layui-body").animate({
                top: '60px',
                left: '200px',
                bottom: '44px'
            }, "normal", function () {
                $(".layui-header").css("z-index", "10000");
                self.removeClass("layui-layer-maxmin").addClass("layui-layer-max");
                windowResize();
            });
        }
    });

});

function windowResize()
{
    var $content = $('.main-index-tab .layui-tab-content')
        , headHeight = $(".frame-header").height()
        , tabHeight = $(".layui-tab-title").height()
        , footHeight = $(".main-index-footer").outerHeight();
    $content.height($(this).height() - headHeight - tabHeight - footHeight);
}

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