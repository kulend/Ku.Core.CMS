if (!window.vino) {
    window.vino = {};
}

(function ($) {
    var tableInsArr = [];
    function _bind(target) {
        var $target = $(target);
        $target.addClass("vino-grid");
        var opts = $target.data("options");
        opts.elem = $target;
        var table = layui.table;
        var id = $target.attr("id");
        //执行渲染
        var tableIns = table.render(opts);
        tableInsArr[id] = tableIns;
    }
    
    $.fn.vinoGrid = function (options, param) {
        if (typeof options == 'string') {
            return $.fn.vinoGrid.methods[options](this, param);
        }
        options = options || {};
        return this.each(function () {
            var opts = $.data(this, 'options');
            if (opts) {
                opts = $.extend(opts, options);
            } else {
                opts = {};
                if ($(this).attr("data-opts")) {
                    opts = eval("(" + $(this).attr("data-opts") + ")");
                }
                opts = $.extend(opts, $.fn.vinoGrid.defaults, options);
                $.data(this, 'options', opts);
            }
            _bind(this);
        });
    };

    $.fn.vinoGrid.methods = {
        reload: function (target, options) {
            target.each(function () {
                //if (options) {
                //    var opts = $.data(this, 'options');
                //    opts = $.extend(opts, options);
                //}
                //$(this).trigger("reloadGrid");
                var id = $(this).attr("id");
                var ins = tableInsArr[id];
                if (ins)
                {
                    ins.reload();
                }
            });
        }
    };

    $.fn.vinoGrid.defaults = {
        method: "get",
        page: true,
        limits: [10, 20, 30, 40, 50, 70, 80, 90],
        loading: true,
        even: true
    };

})(jQuery);