/**
 * 地区选择插件
 */
layui.define(['form', 'layer'], function (exports) {
    var $ = layui.$;
    var form = layui.form;
    var layer = layui.layer;

    var areaSelect = function (parameter) {
        var defaults = {
            dataApi: 'http://passer-by.com/data_location/list.json',     //数据接口地址
            streetDataApi: function (region) {
                return 'http://passer-by.com/data_location/town/' + region + '.json';
            },
            crossDomain: true,        //是否开启跨域
            dataType: 'json',          //数据库类型:'json'或'jsonp'
            filter: "areaSelect" + new Date().getTime(),
            hasStreet:true,
            provinceField: 'province', //省份字段名
            cityField: 'city',         //城市字段名
            regionField: 'region',     //地区字段名
            streetField: 'street',     //街道字段名
            codeField: 'areacode',     //Code字段名
            required: true,
            code: 0,                   //地区编码
            province: 0,               //省份,可以为地区编码或者名称
            city: 0,                   //城市,可以为地区编码或者名称
            region: 0,                 //地区,可以为地区编码或者名称
            street: 0, 
            provinceNameField: '', //省份字段名name
            cityNameField: '',         //城市字段名name
            regionNameField: '',     //地区字段名name
            streetNameField: '',     //街道字段名name
            onChange: function (selectData) { }
        };

        var options = $.extend({}, defaults, parameter);

        if (options.code && typeof options.code == "string") options.code = parseInt(options.code);
        //if (options.province && typeof options.province == "string") options.province = parseInt(options.province);
        //if (options.city && typeof options.city == "string") options.city = parseInt(options.city);
        //if (options.region && typeof options.region == "string") options.region = parseInt(options.region);
        //if (options.street && typeof options.street == "string") options.street = parseInt(options.street);

        return $(options.elem).each(function () {
            var $this = $(this);

            var html = [];
            html.push('<div class="layui-inline" style="width: 130px;">');
            html.push(`<input type="hidden" name="${options.codeField}" value="${options.code}" />`);
            if (options.provinceNameField) {
                html.push(`<input type="hidden" name="${options.provinceNameField}" />`);
            }
            html.push(`<select name="${options.provinceField}" ${options.required ? "lay-verify=\"required\"" : ""} lay-filter="${options.filter}" lay-search>`);
            html.push(`<option value="">请选择省份</option>`);
            html.push('</select>');
            html.push('</div>');
            $this.append(html.join(""));

            var $province = $this.find('select[name="' + options.provinceField + '"]');
            var $city;
            var $region;
            var $street;
            $.ajax({
                url: options.dataApi,
                type: 'GET',
                crossDomain: options.crossDomain,
                dataType: options.dataType,
                jsonpCallback: 'jsonp_location',
                success: function (data) {
                    var province, city, region, hasCity;
                    if (options.code) {   //如果设置地区编码，则忽略单独设置的信息
                        if (options.code >= 100000000) {
                            //options.code为街道Code
                            var pcrCode = parseInt(options.code / 1000);
                            var c = pcrCode - pcrCode % 1e4;
                            if (data[c]) {
                                options.province = c;
                            }
                            c = pcrCode - (pcrCode % 1e4 ? pcrCode % 1e2 : pcrCode);
                            if (data[c]) {
                                options.city = c;
                            }
                            c = pcrCode % 1e2 ? pcrCode : 0;
                            if (data[c]) {
                                options.region = c;
                            }
                            options.street = options.code;
                        } else {
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
                            options.street = 0;
                        }
                    }

                    var updateData = function () {
                        province = {}, city = {}, region = {};
                        hasCity = false;       //判断是非有地级城市
                        for (var code in data) {
                            if (!(code % 1e4)) {     //获取所有的省级行政单位
                                province[code] = data[code];
                                if (isNaN(options.province) && data[code].indexOf(options.province) > -1) {
                                    options.province = code;
                                }
                            } else {
                                var p = code - options.province;
                                if (options.province && p > 0 && p < 1e4) {    //同省的城市或地区
                                    if (!(code % 100)) {
                                        hasCity = true;
                                        city[code] = data[code];
                                        if (isNaN(options.city) && data[code].indexOf(options.city) > -1) {
                                            options.city = code;
                                        }
                                    } else if (p > 8000) {                 //省直辖县级行政单位
                                        city[code] = data[code];
                                        if (isNaN(options.city) && data[code].indexOf(options.city) > -1) {
                                            options.city = code;
                                        }
                                    } else if (hasCity) {                  //非直辖市
                                        var c = code - options.city;
                                        if (options.city && c > 0 && c < 100) {     //同个城市的地区
                                            region[code] = data[code];
                                            if (isNaN(options.region) && data[code].indexOf(options.region) > -1) {
                                                options.region = code;
                                            }
                                        }
                                    } else {
                                        region[code] = data[code];            //直辖市
                                        if (isNaN(options.region) && data[code].indexOf(options.region) > -1) {
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
                                $province.append('<option value="' + i + '">' + province[i] + '</option>');
                            }
                            if (options.province) {
                                $province.val(options.province);
                                $this.find('input[name="' + options.provinceNameField + '"]').val($province.find("option:selected").text());
                            }
                            if ($province.val()) {
                                this.city();
                            }
                        },
                        city: function () {
                            $this.find('select[name="' + options.cityField + '"]').closest(".layui-inline").remove();
                            if (hasCity) {
                                var chtml = [];
                                chtml.push('<div class="layui-inline" style="width: 130px;">');
                                if (options.cityNameField) {
                                    chtml.push(`<input type="hidden" name="${options.cityNameField}" />`);
                                }
                                chtml.push(`<select name="${options.cityField}" ${options.required ? "lay-verify=\"required\"" : ""} lay-filter="${options.filter}">`);
                                chtml.push(`<option value="">请选择城市</option>`);
                                for (var i in city) {
                                    chtml.push(`<option value="${i}">${city[i]}</option>`);
                                }
                                chtml.push('</select>');
                                chtml.push('</div>');
                                $province.closest(".layui-inline").after(chtml.join(""));
                                $city = $this.find('select[name="' + options.cityField + '"]');
                                if (options.city) {
                                    $city.val(options.city);
                                    $this.find('input[name="' + options.cityNameField + '"]').val($city.find("option:selected").text());
                                } else if (options.region) {
                                    $city.val(options.region);
                                    $this.find('input[name="' + options.cityNameField + '"]').val($city.find("option:selected").text());
                                }
                                if ($city.val()) {
                                    this.region();
                                }
                            } else {
                                $city = null;
                                this.region();
                            }
                        },
                        region: function () {
                            $this.find('select[name="' + options.regionField + '"]').closest(".layui-inline").remove();
                            var rhtml = [];
                            rhtml.push('<div class="layui-inline" style="width: 130px;">');
                            if (options.regionNameField) {
                                rhtml.push(`<input type="hidden" name="${options.regionNameField}" />`);
                            }
                            rhtml.push(`<select name="${options.regionField}" ${options.required ? "lay-verify=\"required\"" : ""} lay-filter="${options.filter}">`);
                            rhtml.push(`<option value="">请选择地区</option>`);
                            for (var i in region) {
                                rhtml.push(`<option value="${i}">${region[i]}</option>`);
                            }
                            rhtml.push('</select>');
                            rhtml.push('</div>');
                            if ($city) {
                                $city.closest(".layui-inline").after(rhtml.join(""));
                            } else {
                                $province.closest(".layui-inline").after(rhtml.join(""));
                            }
                            $region = $this.find('select[name="' + options.regionField + '"]');
                            if (options.region) {
                                $region.val(options.region);
                                $this.find('input[name="' + options.regionNameField + '"]').val($region.find("option:selected").text());
                            }
                            if ($region.val()) {
                                this.street();
                            }
                        },
                        street: function () {
                            if (!options.hasStreet) {
                                return;
                            }
                            $this.find('select[name="' + options.streetField + '"]').closest(".layui-inline").remove();
                            var regionCode = options.region;
                            if (regionCode >= 710000) {
                                //香港、澳门、台湾地区
                                return;
                            }
                            //取得街道数据
                            $.ajax({
                                url: options.streetDataApi(regionCode),
                                dataType: 'json',
                                success: function (town) {
                                    var rhtml = [];
                                    rhtml.push('<div class="layui-inline" style="width: 130px;">');
                                    if (options.streetNameField) {
                                        rhtml.push(`<input type="hidden" name="${options.streetNameField}" />`);
                                    }
                                    rhtml.push(`<select name="${options.streetField}" ${options.required ? "lay-verify=\"required\"" : ""} lay-filter="${options.filter}">`);
                                    rhtml.push(`<option value="">请选择街道</option>`);

                                    var street = {};
                                    for (i in town) {
                                        street[i] = town[i];
                                        rhtml.push(`<option value="${i}">${town[i]}</option>`);
                                    }

                                    rhtml.push('</select>');
                                    rhtml.push('</div>');
                                    $region.closest(".layui-inline").after(rhtml.join(""));

                                    $street = $this.find('select[name="' + options.streetField + '"]');
                                    if (options.street) {
                                        $street.val(options.street);
                                        $this.find('input[name="' + options.streetNameField + '"]').val($street.find("option:selected").text());
                                    }
                                    $street.closest(".layui-inline").removeClass("layui-hide");
                                    form.render();
                                }
                            });
                        }
                    };
                    //初始化
                    updateData();
                    format.province();
                    form.render();

                    form.on(`select(${options.filter})`, function (selectData) {
                        var name = $(selectData.elem).attr("name");
                        if (name == options.provinceField) {
                            $this.find('input[name="' + options.provinceNameField + '"]').val($province.find("option:selected").text());
                            $this.find('select[name="' + options.cityField + '"]').closest(".layui-inline").remove();
                            $this.find('select[name="' + options.regionField + '"]').closest(".layui-inline").remove();
                            $this.find('select[name="' + options.streetField + '"]').closest(".layui-inline").remove();
                            if (selectData.value) {
                                options.province = selectData.value || 0;
                                options.city = 0;
                                options.region = 0;
                                options.street = 0;
                                updateData();
                                format.city();
                            } else {
                                options.city = 0;
                                options.region = 0;
                                options.street = 0;
                            }
                        } else if (name == options.cityField) {
                            $this.find('input[name="' + options.cityNameField + '"]').val($city.find("option:selected").text());
                            $this.find('select[name="' + options.regionField + '"]').closest(".layui-inline").remove();
                            $this.find('select[name="' + options.streetField + '"]').closest(".layui-inline").remove();
                            if (selectData.value) {
                                options.city = selectData.value || 0;
                                options.region = 0;
                                options.street = 0;
                                updateData();
                                format.region();
                            } else {
                                options.region = 0;
                                options.street = 0;
                            }
                        } else if (name == options.regionField) {
                            $this.find('input[name="' + options.regionNameField + '"]').val($region.find("option:selected").text());
                            $this.find('select[name="' + options.streetField + '"]').closest(".layui-inline").remove();
                            if (selectData.value) {
                                options.region = selectData.value || 0;
                                options.street = 0;
                                format.street();
                            } else {
                                options.street = 0;
                            }
                        } else {
                            $this.find('input[name="' + options.streetNameField + '"]').val($street.find("option:selected").text());
                            options.street = selectData.value || 0;
                        }
                        form.render();
                        $this.find('input[name="' + options.codeField + '"]').val(options.street || options.region || options.city || options.province);
                        options.onChange(selectData);
                    });
                }
            });
        });
    };

    exports('areaSelect', areaSelect);
});