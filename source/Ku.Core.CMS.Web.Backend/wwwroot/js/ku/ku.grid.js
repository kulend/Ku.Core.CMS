if (!window.ku) {
    window.ku = {};
}

(function ($) {
    var tableInsArr = [];
    function _bind(target) {
        var $target = $(target);
        $target.addClass("ku-grid");
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

        for (var i = 0; i < opts.cols[0].length; i++) {
            if (opts.cols[0][i].enum) {
                opts.cols[0][i].align = opts.cols[0][i].align || 'center';
                opts.cols[0][i].templet = '<div>{{d.' + opts.cols[0][i].field + '.Title}}</div>';
            }
            if (opts.cols[0][i].switch) {
                opts.cols[0][i].align = opts.cols[0][i].align || 'center';
                opts.cols[0][i].templet = '<div>{{# if(d.' +
                    opts.cols[0][i].field +
                    '){ }} <i class="layui-icon">&#xe605;</i> {{# }else{ }} <i class="layui-icon">&#x1006;</i> {{# } }}</div>';
            }
        }

        var table = layui.table;
        var id = $target.attr("id");
        opts.id = id;
        $target.attr("lay-filter", id);

        var where = opts.where || {};
        if (opts.onWhere && typeof opts.onWhere === "function")
        {
            opts.where = $.extend(where, { where: opts.onWhere.call() });
        }

        //执行渲染
        var tableIns = table.render(opts);
        tableInsArr[id] = tableIns;
    }
    
    $.fn.kuGrid = function (options, param) {
        if (typeof options === 'string') {
            return $.fn.kuGrid.methods[options](this, param);
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
                opts = $.extend(opts, $.fn.kuGrid.defaults, options);
                $.data(this, 'options', opts);
            }
            _bind(this);
        });
    };

    $.fn.kuGrid.methods = {
        reload: function (target) {
            target.each(function () {
                var id = $(this).attr("id");
                layui.table.reload(id);
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
        },
        getCheckedIds: function (target) {
            var id = $(target).attr("id");
            var checkStatus = layui.table.checkStatus(id);
            var ids = [];
            for (var i = 0; i < checkStatus.data.length; i++) {
                ids.push(checkStatus.data[i]['Id']);
            }
            return ids;
        }
    };

    $.fn.kuGrid.defaults = {
        method: "post",
        page: true,
        limits: [10, 20, 30, 40, 50],
        limit:10,
        loading: true,
        even: false,
        rownumber: true,
        checkbox: true,
        height:'full',
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