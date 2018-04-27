layui.define(['form'], function (exports) {
    var $ = layui.$;
    var form = layui.form;

    var multiSelect = function (opts) {
        var defaults = {
            api: '',     //数据地址
            filter: "multiSelect" + new Date().getTime(),
            required:true
        };

        var options = $.extend({}, defaults, opts);
        if (!options.api) {
            return;
        }
        var $this = $(opts.elem);
        if (!$this.length) {
            return;
        }
        vino.ajax.get(options.api, options.data, function (reply) {
            var data = reply.data;
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
                    html.push(`<select name="${options.name}" data-level="${level}" ${options.required ? "lay-verify=\"required\"":""} lay-filter="${options.filter}">`);
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
        });

    };

    exports('multiSelect', multiSelect);
});

//(function ($) {
//    $.fn.multiSelect = function (opts) {
//        var defaults = {
//            dataUrl: '/Mall/Product/Category/Index/Json',     //数据地址
//            filter: "aa" + new Date().getTime()
//        };
//        var options = $.extend({}, defaults, opts);
//        return this.each(function () {
//            var $this = $(this);
//            vino.ajax.get(options.dataUrl, {}, function (reply) {
//                var data = reply.data;
//                var renderItem = function (level, pid, values) {
//                    var optionList = [];
//                    var strLevel = level.toString();
//                    if (level == 1) {
//                        for (var v in data[strLevel]) {
//                            optionList.push({ Value: v, Name: data[strLevel][v] });
//                        }
//                    } else {
//                        if (data[strLevel] && data[strLevel][pid]) {
//                            for (var v in data[strLevel][pid]) {
//                                optionList.push({ Value: v, Name: data[strLevel][pid][v] });
//                            }
//                        }
//                    }
//                    var selValue = "";
//                    if (optionList.length || level == 1) {
//                        var html = [];
//                        html.push('<div class="layui-inline" style="width: 130px;">');
//                        html.push(`<select name="a" data-level="${level}" lay-verify="required" lay-filter="${options.filter}">`);
//                        var subs = [];
//                        html.push(`<option value=""></option>`);
//                        for (var i = 0; i < optionList.length; i++) {
//                            if (values && values[strLevel] === optionList[i].Value) {
//                                selValue = optionList[i].Value;
//                                html.push(`<option value="${optionList[i].Value}" selected>${optionList[i].Name}</option>`);
//                            } else {
//                                html.push(`<option value="${optionList[i].Value}">${optionList[i].Name}</option>`);
//                            }
//                        }
//                        html.push('</select>');
//                        html.push('</div>');
//                        $this.append(html.join(""));
//                    }

//                    if (selValue) {
//                        renderItem(++level, selValue, values);
//                    }
//                    layui.form.render();
//                }

//                renderItem(1, null, options.values || {});

//                layui.form.on(`select(${options.filter})`, function (data) {
//                    $(data.elem).closest(".layui-inline").nextAll().remove();
//                    var level = $(data.elem).data("level");
//                    renderItem(++level, data.value, null);
//                });  
//            });
//        });
//    }
//})(jQuery);