layui.define(['form', 'layer'], function (exports) {
    var $ = layui.$;
    var form = layui.form;
    var layer = layui.layer;

    var multiSelectBox = function (opts) {
        var defaults = {
            winUrl: '',     //数据选择页面地址
            label: " ",
            filter: "multiSelectBox" + new Date().getTime(),
            required: true,
            winTitle: "选择",
            textField: "name",
            valueField: "id",
            name: "",
            limit: 99
        };

        var options = $.extend({}, defaults, opts);
        if (!options.winUrl) {
            return;
        }
        var $this = $(options.elem);
        if (!$this.length) {
            return;
        }

        var html = [];
        html.push(`<label class="layui-form-label">${options.label}</label>`);
        html.push(`<div class="layui-input-block multi-box">`);
        html.push(`<div class="multi-box-container">`);
        html.push(`</div>`);
        html.push(`<div class="multi-box-action">`);
        html.push(`<span class="multi-box-action-tip">已选0个，可选0个</span>`);
        html.push(`<div class="layui-btn-group">`);
        html.push(`<a href="javascript:;" class="multi-box-action-add"><i class="layui-icon">&#xe654;</i></a>`);
        html.push(`<a href="javascript:;" class="multi-box-action-clear"><i class="layui-icon">&#xe640;</i></a>`);
        html.push(`</div>`);
        html.push(`</div>`);

        $this.append(html.join(""));

        $this.find("a.multi-box-action-add").on("click", function () {
            OpenWindow(options.winTitle, options.winUrl, null, function (data) {
                loadItems(data);
            });
        });

        var currentIds = [];
        var updateTip = function () {
            $this.find(".multi-box-action-tip").text(`已选${currentIds.length}个，可选${options.limit}个`);
        };
        updateTip();

        var loadItems = function (data) {
            var $container = $this.find(".multi-box-container");
            if (data && data.length) {
                for (var i = 0; i < data.length; i++) {
                    if (currentIds.length >= options.limit) {
                        layer.msg('超过最大可选数');
                        break;
                    }
                    var value = data[i][options.valueField];
                    if ($.inArray(value, currentIds) < 0) {
                        currentIds.push(value);
                        var itemHtml = `<a class="multi-box-item" href="javascript:;" data-value="${value}"><span>${data[i][options.textField]}</span><i></i>`;
                        itemHtml += `<input type="hidden" name="${options.name}" value="${value}" /></a>`;
                        $container.append(itemHtml);
                    }
                }
            }

            updateTip();

            $container.find(".multi-box-item i").off("click").on("click", function () {
                var value = $(this).closest(".multi-box-item").attr("data-value");
                var index = $.inArray(value, currentIds);
                if (index >= 0) {
                    currentIds.splice(index, 1);
                }
                $(this).closest(".multi-box-item").remove();
                updateTip();
            });
        };

        $this.find("a.multi-box-action-clear").on("click", function () {
            currentIds = [];
            $this.find(".multi-box-container").empty();
            updateTip();
        });
    };

    exports('multiSelectBox', multiSelectBox);
});