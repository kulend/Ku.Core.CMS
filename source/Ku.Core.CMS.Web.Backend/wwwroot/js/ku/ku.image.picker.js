(function ($) {
    var getEmptyBox = function () {
        var $li = $("<li class=\"empty-box\"></li>");
        $li.html('<div class="pic-box"><a class="img" href="javascript:;"><img src="/images/empty_photo.jpg" align="absmiddle" /></a><div class="pic-remove" style="display: none;">删除</div></div><a class="link-btn"  href="javascript:;">添加图片</a>');
        return $li;
    }

    var upadteFieldValue = function ($element) {
        var opts = $element.data("options") || {};
        var arrPath = [];
        var arrThumb = [];
        var items = [];
        $element.find("li:not(.empty-box)").each(function (index) {
            var path = $(this).attr("data-path");
            var thumb = $(this).attr("data-thumb");
            if (path) {
                arrPath.push(path);
            }
            if (thumb) {
                arrThumb.push(thumb);
            }
            var item = {
                index: index + 1,
                path: $(this).attr("data-path"),
                thumb: $(this).attr("data-thumb"),
                url: $(this).attr("data-url"),
                thumb_url: $(this).attr("data-thumb_url")
            };
            if (opts.onDataParse && typeof (opts.onDataParse) === "function") {
                opts.onDataParse.call(this, item);
            }
            items.push(item);
        });
        $element.find("input.field_path").val(arrPath.join(','));
        $element.find("input.field_thumb").val(arrThumb.join(','));
        $element.find("input.field_data").val(JSON.stringify(items));
    }

    // 绑定事件
    var bindImageEvent = function ($item) {
        $item.find(".pic-remove").on("click", function () {
            var $element = $(this).closest(".image-upload");
            var opts = $element.data("options") || {};
            var maxcount = opts.max_count;
            $(this).parent().parent().remove();
            var currentcount = $element.find("li").length - 1;
            if (currentcount < maxcount) {
                $element.find(".empty-box").show();
            }
            upadteFieldValue($element);
        });

        $item.find("a.img").on("click", function () {
            var $element = $(this).closest(".image-upload");
            var opts = $element.data("options") || {};
            if (opts.onClick && typeof (opts.onClick) === "function") {
                opts.onClick.call(this.parentNode.parentNode);
            }
        });

        $item.mouseover(function () {
            var $element = $(this).closest(".image-upload");
            var opts = $element.data("options") || {};
            if (opts.enable) {
                $(this).find(".pic-remove").show();
            }
        });
        $item.mouseout(function () {
            $(this).find(".pic-remove").hide();
        });
    };

    var addItem = function ($element, file) {
        var opts = $element.data("options") || {};
        var maxcount = opts.max_count;
        var currentcount = $element.find("li").length - 1;
        if (currentcount >= maxcount) {
            alert("无法添加更多图片了.");
            return;
        }

        var $empty = $element.find(".empty-box");
        var $addObj = $empty.clone(true);
        $addObj.removeClass("empty-box");
        $addObj.show();
        $addObj.find("a.link-btn").text("替换图片");
        if (!opts.enable) {
            $addObj.find("a.link-btn").hide();
        }
        $addObj.attr("data-id", new Date().getTime());
        $addObj.attr("data-path", file.path);
        $addObj.attr("data-thumb", file.thumb);
        $addObj.attr("data-url", file.url);
        $addObj.attr("data-thumb_url", file.thumb_url);
        $addObj.find("img").attr("src", file.thumb_url || file.url);
        $empty.before($addObj);
        bindImageEvent($addObj);
        currentcount++;
        if (currentcount >= maxcount) {
            $empty.hide();
        }
        upadteFieldValue($element);
    };

    var loadItem = function ($element, data) {
        var opts = $element.data("options") || {};
        var maxcount = opts.max_count;
        var currentcount = $element.find("li").length - 1;
        if (currentcount >= maxcount) {
            return;
        }

        var $empty = $element.find(".empty-box");
        var $addObj = $empty.clone(true);
        $addObj.removeClass("empty-box");
        $addObj.show();
        $addObj.find("a.link-btn").text("替换图片");
        if (!opts.enable) {
            $addObj.find("a.link-btn").hide();
        }
        $addObj.attr("data-id", new Date().getTime());
        $addObj.attr("data-path", data.path);
        $addObj.attr("data-thumb", data.thumb);
        $addObj.attr("data-url", data.url);
        $addObj.attr("data-thumb_url", data.thumb_url);
        $addObj.find("img").attr("src", data.thumb_url || data.url);

        if (opts.onLoadParse && typeof (opts.onLoadParse) === "function") {
            opts.onLoadParse.call($addObj[0], data);
        }
        $empty.before($addObj);
        bindImageEvent($addObj);
        currentcount++;
        if (currentcount >= maxcount) {
            $empty.hide();
        }
        upadteFieldValue($element);
    };

    var bindButton = function ($element) {
        $element.find(".link-btn").click(function () {
            var $li = $(this).parent();
            var $box = $(this).siblings(".pic-box");
            var $imgItem = $box.find("img");
            var notAdd = $li.attr("data-path") !== undefined;
            var opts = $element.data("options") || {};
            var maxcount = opts.max_count;
            var currentcount = $element.find("li").length - 1;
            if (!notAdd && currentcount >= maxcount) {
                alert("无法添加更多图片了.");
                return;
            }
            var max = 1;
            if (!notAdd) {
                max = maxcount - currentcount;
            }
            OpenWindow("选择图片", `MaterialCenter/Picture/Selector?max=${max}`, { area: ['837px', '468px'], maxmin: false }, function (files) {
                if (!files.length) return;
                if (notAdd) {
                    //如果是替换
                    var file = files[0];
                    $li.attr("data-path", file.path);
                    $li.attr("data-thumb", file.thumb);
                    $li.attr("data-url", file.url);
                    $li.attr("data-thumb_url", file.thumb_url);
                    $imgItem.attr("src", file.thumb_url || file.url);
                } else {
                    //添加
                    for (var i = 0; i < files.length; i++) {
                        addItem($element, files[i]);
                    }
                }
                upadteFieldValue($element);
            });
        });
    };

    function init(element) {
        var $element = $(element);
        var opts = $element.data("options");
        $element.addClass("image-upload");
        var $ul = $("<ul></ul>");
        var $emptyLi = getEmptyBox();
        $ul.append($emptyLi);
        $element.append($ul);
        if (!opts.enable) {
            $emptyLi.hide();
        }

        if (opts.field_path) {
            var $fieldPath = $("<input class='field_path' type=\"hidden\" />");
            $fieldPath.attr("name", opts.field_path);
            $element.append($fieldPath);
        }

        if (opts.field_thumb) {
            var $fieldThumb = $("<input class='field_thumb' type=\"hidden\" />");
            $fieldThumb.attr("name", opts.field_thumb);
            $element.append($fieldThumb);
        }
        if (opts.field_data) {
            var $fieldData = $("<input class='field_data' type=\"hidden\" />");
            $fieldData.attr("name", opts.field_data);
            $element.append($fieldData);
        }
        bindButton($element);
    }

    $.fn.kuImagePicker = function (options, param) {
        if (typeof options === 'string') {
            return $.fn.kuImagePicker.methods[options](this, param);
        }
        options = options || {};
        return this.each(function () {
            var opts = $.data(this, 'options');
            if (opts) {
                opts = $.extend(opts, options);
            } else {
                opts = $.extend({}, $.fn.kuImagePicker.defaults, options);
                $.data(this, 'options', opts);
            }
            init(this);
        });
    };

    $.fn.kuImagePicker.methods = {
        clear: function (target) {
            target.each(function () {
                var $element = $(this);
                var opts = $element.data("options") || {};
                $element.find("li:not(.empty-box)").each(function (index) {
                    $(this).remove();
                });
                var $empty = $element.find(".empty-box");
                $empty.show();
                upadteFieldValue($element);
            });
        },
        add: function (target, file) {
            target.each(function () {
                var $element = $(this);
                addItem($element, file);
            });
        },
        sync: function (target) {
            target.each(function () {
                var $element = $(this);
                upadteFieldValue($element);
            });
        },
        loadData: function (target, data) {
            target.each(function () {
                var $element = $(this);
                if (!data) {
                    return;
                }
                //var opts = $element.data("options") || {};
                var items = JSON.parse(data);
                if (items && items.length > 0) {
                    //加载数据
                    for (var i = 0; i < items.length; i++) {
                        loadItem($element, items[i]);
                    }
                }
            });
        },
        loadImage: function (target, path) {
            target.each(function () {
                var $element = $(this);
                if (!path) {
                    return;
                }
                var item = {
                    path: path
                };
                loadItem($element, item);
            });
        },
        getData: function (target) {
            if (target.length > 1) {
                return null;
            }
            var $element = target;
            var opts = $element.data("options") || {};
            var items = [];
            $element.find("li:not(.empty-box)").each(function (index) {
                var item = {
                    index: index + 1,
                    path: $(this).attr("data-path"),
                    thumb: $(this).attr("data-thumb")
                };
                if (opts.onDataParse && typeof (opts.onDataParse) === "function") {
                    opts.onDataParse.call(this, item);
                }
                items.push(item);
            });
            return items;
        }
    };

    $.fn.kuImagePicker.defaults = {
        max_count: 1,
        enable: true,
        //onClick: function () {
        //},
        //onDataParse: function (item) {
        //},
        //onLoadParse: function (item) {
        //}
    };
})(jQuery);