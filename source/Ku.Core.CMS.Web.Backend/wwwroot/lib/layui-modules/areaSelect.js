/**
 * 地区选择插件
 */
layui.define(['form', 'layer'], function (exports) {
    var $ = layui.$;
    var form = layui.form;
    var layer = layui.layer;

    var areaSelect = function (parameter, getApi) {
        if (typeof parameter == 'function') { //重载
            getApi = parameter;
            parameter = {};
        } else {
            parameter = parameter || {};
            getApi = getApi || function () { };
        }
        var defaults = {
            dataApi: 'http://passer-by.com/data_location/list.json',     //数据接口地址
            crossDomain: true,        //是否开启跨域
            dataType: 'json',          //数据库类型:'json'或'jsonp'
            label: "地区",
            filter: "areaSelect" + new Date().getTime(),
            provinceField: 'province', //省份字段名
            cityField: 'city',         //城市字段名
            regionField: 'region',         //地区字段名
            valueType: 'code',         //下拉框值的类型,code行政区划代码,name地名
            required: true,
            code: 0,                   //地区编码
            province: 0,               //省份,可以为地区编码或者名称
            city: 0,                   //城市,可以为地区编码或者名称
            region: 0,                   //地区,可以为地区编码或者名称
        };

        var options = $.extend({}, defaults, parameter);

        return $(options.elem).each(function () {
            var _api = {};
            var $this = $(this);

            var html = [];
            html.push('<div class="layui-inline" style="width: 130px;">');
            html.push(`<select name="${options.provinceField}" ${options.required ? "lay-verify=\"required\"" : ""} lay-filter="${options.filter}">`);
            html.push(`<option value="">请选择省份</option>`);
            html.push('</select>');
            html.push('</div>');
            $this.append(html.join(""));

            var $province = $this.find('select[name="' + options.provinceField + '"]');
            var $city;
            var $region;
            $.ajax({
                url: options.dataApi,
                type: 'GET',
                crossDomain: options.crossDomain,
                dataType: options.dataType,
                jsonpCallback: 'jsonp_location',
                success: function (data) {
                    var province, city, region, hasCity;
                    if (options.code) {   //如果设置地区编码，则忽略单独设置的信息
                        var c = options.code - options.code % 1e4;
                        if (data[c]) {
                            options.province = c;
                        }
                        c = options.code - (options.code % 1e4 ? options.code % 1e2 : options.code);
                        if (data[c]) {
                            options.city = c;
                        }
                        c = options.code % 1e2 ? options.code : 0;
                        if (data[c]) {
                            options.region = c;
                        }
                    }

                    var updateData = function () {
                        province = {}, city = {}, region = {};
                        hasCity = false;       //判断是非有地级城市
                        for (var code in data) {
                            if (!(code % 1e4)) {     //获取所有的省级行政单位
                                province[code] = data[code];
                                if (options.required && !options.province) {
                                    if (options.city && !(options.city % 1e4)) {  //省未填，并判断为直辖市
                                        options.province = options.city;
                                    } else {
                                        options.province = code;
                                    }
                                } else if (isNaN(options.province) && data[code].indexOf(options.province) > -1) {
                                    options.province = code;
                                }
                            } else {
                                var p = code - options.province;
                                if (options.province && p > 0 && p < 1e4) {    //同省的城市或地区
                                    if (!(code % 100)) {
                                        hasCity = true;
                                        city[code] = data[code];
                                        if (options.required && !options.city) {
                                            options.city = code;
                                        } else if (isNaN(options.city) && data[code].indexOf(options.city) > -1) {
                                            options.city = code;
                                        }
                                    } else if (p > 8000) {                 //省直辖县级行政单位
                                        city[code] = data[code];
                                        if (options.required && !options.city) {
                                            options.city = code;
                                        } else if (isNaN(options.city) && data[code].indexOf(options.city) > -1) {
                                            options.city = code;
                                        }
                                    } else if (hasCity) {                  //非直辖市
                                        var c = code - options.city;
                                        if (options.city && c > 0 && c < 100) {     //同个城市的地区
                                            region[code] = data[code];
                                            if (options.required && !options.region) {
                                                options.region = code;
                                            } else if (isNaN(options.region) && data[code].indexOf(options.region) > -1) {
                                                options.region = code;
                                            }
                                        }
                                    } else {
                                        region[code] = data[code];            //直辖市
                                        if (options.required && !options.region) {
                                            options.region = code;
                                        } else if (isNaN(options.region) && data[code].indexOf(options.region) > -1) {
                                            options.region = code;
                                        }
                                    }
                                }
                            }
                        }
                    };
                    var format = {
                        province: function () {
                            $province.empty();
                            $province.append('<option value="">请选择省份</option>');
                            for (var i in province) {
                                $province.append('<option value="' + (options.valueType == 'code' ? i : province[i]) + '" data-code="' + i + '">' + province[i] + '</option>');
                            }
                            if (options.province) {
                                var value = options.valueType == 'code' ? options.province : province[options.province];
                                $province.val(value);
                            }
                            this.city();
                        },
                        city: function () {
                            $this.find('select[name="' + options.cityField + '"]').closest(".layui-inline").remove();
                            if (hasCity) {
                                var chtml = [];
                                chtml.push('<div class="layui-inline" style="width: 130px;">');
                                chtml.push(`<select name="${options.cityField}" ${options.required ? "lay-verify=\"required\"" : ""} lay-filter="${options.filter}">`);
                                chtml.push(`<option value="">请选择城市</option>`);
                                for (var i in city) {
                                    chtml.push('<option value="' + (options.valueType == 'code' ? i : city[i]) + '" data-code="' + i + '">' + city[i] + '</option>');
                                }
                                chtml.push('</select>');
                                chtml.push('</div>');
                                $province.closest(".layui-inline").after(chtml.join(""));
                                $city = $this.find('select[name="' + options.cityField + '"]');
                                if (options.city) {
                                    var value = options.valueType == 'code' ? options.city : city[options.city];
                                    $city.val(value);
                                } else if (options.area) {
                                    var value = options.valueType == 'code' ? options.area : city[options.area];
                                    $city.val(value);
                                }
                            }
                            this.region();
                        },
                        region: function () {
                            $this.find('select[name="' + options.regionField + '"]').closest(".layui-inline").remove();
                            var rhtml = [];
                            rhtml.push('<div class="layui-inline" style="width: 130px;">');
                            rhtml.push(`<select name="${options.regionField}" ${options.required ? "lay-verify=\"required\"" : ""} lay-filter="${options.filter}">`);
                            rhtml.push(`<option value="">请选择地区</option>`);
                            for (var i in region) {
                                rhtml.push('<option value="' + (options.valueType == 'code' ? i : region[i]) + '" data-code="' + i + '">' + region[i] + '</option>');
                            }
                            rhtml.push('</select>');
                            rhtml.push('</div>');
                            $this.append(rhtml.join(""));
                            $region = $this.find('select[name="' + options.regionField + '"]');
                            if (options.region) {
                                var value = options.valueType == 'code' ? options.region : region[options.region];
                                $region.val(value);
                            }
                        }
                    };
                    //初始化
                    updateData();
                    format.province();
                    form.render();

                    form.on(`select(${options.filter})`, function (selectData) {
                        var name = $(selectData.elem).attr("name");
                        if (name == options.provinceField) {
                            options.province = selectData.value || 0;
                            options.city = 0;
                            options.region = 0;
                            updateData();
                            format.city();
                        } else if (name == options.cityField) {
                            options.city = selectData.value || 0;
                            options.region = 0;
                            updateData();
                            format.region();
                        } else {
                            options.region = selectData.value || 0;
                        }
                        form.render();
                    });
                }
            });
        });
    };

    exports('areaSelect', areaSelect);
});