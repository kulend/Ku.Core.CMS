layui.define(['form', 'layer'], function (exports) {
    var $ = layui.$;
    var form = layui.form;
    var layer = layui.layer;

    var multiSelect = function (opts) {
        var defaults = {
            url: '',     //数据地址
            type: "get",
            filter: "multiSelect" + new Date().getTime(),
            required:true
        };

        var options = $.extend({}, defaults, opts);
        if (!options.url) {
            return;
        }
        var $this = (opts.elem instanceof jQuery) ? opts.elem : $(opts.elem);
        if (!$this.length) {
            return;
        }
        $.ajax({
            url: options.url,
            data: options.data,
            dataType: 'json',
            type: options.method,
            success: function (result) {
                var data = result;
                var max = 1;
                var keytypeIsStr = false;
                for (var k in data) {
                    if (typeof (k) == "number" && k > max) max = k;
                    if (typeof (k) == "string" && parseInt(k) > max) {
                        max = parseInt(k);
                        keytypeIsStr = true;
                    };
                }
                var values = {};
                if (options.value) {
                    var searchValue = options.value;
                    for (var i = max; i >= 1; i--) {
                        var key = keytypeIsStr ? i.toString() : i;
                        for (var j in data[key]) {
                            if (i == 1) {
                                if (searchValue == j) {
                                    values[key] = searchValue;
                                    break;
                                }
                            } else {
                                for (var k in data[key][j]) {
                                    if (searchValue == k) {
                                        values[key] = searchValue;
                                        searchValue = j;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                options.values = values;

                var renderItem = function (level, pid, values) {
                    var optionList = [];
                    var levelKey = keytypeIsStr ? level.toString() : level;
                    if (level == 1) {
                        for (var v in data[levelKey]) {
                            optionList.push({ Value: v, Name: data[levelKey][v] });
                        }
                    } else {
                        if (data[levelKey] && data[levelKey][pid]) {
                            for (var v in data[levelKey][pid]) {
                                optionList.push({ Value: v, Name: data[levelKey][pid][v] });
                            }
                        }
                    }
                    var selValue = "";
                    if (optionList.length || level == 1) {
                        $this.find("select").removeAttr("name");
                        var html = [];
                        html.push('<div class="layui-inline" style="width: 130px;">');
                        html.push(`<select name="${options.name}" data-level="${level}" ${options.required ? "lay-verify=\"required\"" : ""} lay-filter="${options.filter}">`);
                        var subs = [];
                        html.push(`<option value=""></option>`);
                        for (var i = 0; i < optionList.length; i++) {
                            if (values && values[levelKey] == optionList[i].Value) {
                                selValue = optionList[i].Value;
                                html.push(`<option value="${optionList[i].Value}" selected>${optionList[i].Name}</option>`);
                            } else {
                                html.push(`<option value="${optionList[i].Value}">${optionList[i].Name}</option>`);
                            }
                        }
                        html.push('</select>');
                        html.push('</div>');
                        $this.append(html.join(""));
                    }

                    if (selValue) {
                        renderItem(++level, selValue, values);
                    }
                    form.render();
                }

                renderItem(1, null, options.values || {});

                form.on(`select(${options.filter})`, function (selectData) {
                    $(selectData.elem).closest(".layui-inline").nextAll().remove();
                    $(selectData.elem).attr("name", options.name);
                    var level = $(selectData.elem).data("level");
                    renderItem(++level, selectData.value, null);
                });
            },
            error: function (xhr, status, errorThrow) {
                layer.alert(`获取数据出错：{${xhr.status}}${status}`, { icon: 5 }, null);
            }
        });
    };

    exports('multiSelect', multiSelect);
});