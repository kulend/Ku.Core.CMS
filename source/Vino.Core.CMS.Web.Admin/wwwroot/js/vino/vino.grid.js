if (!window.vino) {
    window.vino = {};
}

(function ($) {
    function _bind(target) {
        var $target = $(target);
        $target.addClass("vino-grid");
        var opts = $target.data("options");
        $target.jqGrid(opts);
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
                if (options) {
                    var opts = $.data(this, 'options');
                    opts = $.extend(opts, options);
                }
                $(this).trigger("reloadGrid");
            });
        }
    };

    $.fn.vinoGrid.defaults = {
        datatype: "json",
        styleUI: 'Bootstrap',
        viewrecords: true,
        rowNum: 10,
        rowList: [10, 20, 50],
        rownumbers: true,
        rownumWidth: 30,
        autowidth: true,
        height: 'auto',
        multiselect: true,
        pager: "#pager",
        jsonReader: {
            root: "data.items",
            page: "data.pager.page",
            total: "data.pager.total_page",
            records: "data.pager.total"
        },
        prmNames: {
            page: "page",
            rows: "rows",
            order: "order"
        },
        gridComplete: function () {
            $(this).closest(".ui-jqgrid-bdiv").css({ 'overflow-x': 'hidden' });
        }
    };

})(jQuery);