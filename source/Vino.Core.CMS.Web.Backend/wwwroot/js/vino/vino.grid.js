﻿if (!window.vino) {
    window.vino = {};
}

(function ($) {
    var tableInsArr = [];
    function _bind(target) {
        var $target = $(target);
        $target.addClass("vino-grid");
        var opts = $target.data("options");
        opts.elem = $target;
        if (opts.checkbox)
        {
            opts.cols[0].unshift(
                { type: 'checkbox', fixed: true, align: 'center' }
            );
        }
        if (opts.rownumber)
        {
            opts.cols[0].unshift(
                { type: 'numbers', fixed: true, align: 'center' }
            );
        }
        var table = layui.table;
        var id = $target.attr("id");
        opts.id = id;

        var where = opts.where || {};
        if (opts.onWhere && typeof opts.onWhere === "function")
        {
            opts.where = $.extend(where, { where: opts.onWhere.call() });
        }

        //执行渲染
        var tableIns = table.render(opts);
        tableInsArr[id] = tableIns;
    }
    
    $.fn.vinoGrid = function (options, param) {
        if (typeof options === 'string') {
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
        reload: function (target) {
            target.each(function () {
                var id = $(this).attr("id");
                layui.table.reload(id);
                //var ins = tableInsArr[id];
                //if (ins)
                //{
                //    ins.reload();
                //}
            });
        },
        search: function (target, options) {
            target.each(function () {
                var id = $(this).attr("id");
                var opts = $(this).data("options");
                var where = opts.where || {};
                if (opts.onWhere && typeof opts.onWhere === "function") {
                    opts.where = $.extend(where, { where: opts.onWhere.call() }, options);
                }
                opts.page = { curr: 1};
                layui.table.reload(id, opts);
            });
        }
    };

    $.fn.vinoGrid.defaults = {
        method: "post",
        page: true,
        limits: [10, 20, 30, 40, 50],
        limit:10,
        loading: true,
        even: false,
        rownumber: true,
        checkbox: true,
        height:'full-65',
        request: {
            pageName: 'page' //页码的参数名称，默认：page
            , limitName: 'rows' //每页数据量的参数名，默认：limit
        },
        response: {
            statusName: 'code' //数据状态的字段名称，默认：code
            , statusCode: 0 //成功的状态码，默认：0
            , msgName: 'msg' //状态信息的字段名称，默认：msg
            , countName: 'count' //数据总数的字段名称，默认：count
            , dataName: 'data' //数据列表的字段名称，默认：data
        } 
    };

})(jQuery);