if (!window.vino) {
    window.vino = {};
}

(function ($) {
    function _handleMessage(reply, options) {
        vino.page.msg.hideLoad();
        if (!reply)
        { return false; }
        var success = reply.code === 0;
        if (success) {
            if (options.onAfter && typeof options.onAfter == "function") {
                return options.onAfter(reply, options);
            }
            return true;
        } else {
            vino.page.msg.tip(reply.message);
            return false;
        }
    }

    function _bind(target) {
        jQuery.getScript("/lib/jquery-form/jquery.form.js").done(function () {
            var $target = $(target);
            var opts = $target.data("options");
            var options = {
                type: opts.type,
                dataType: opts.dataType,
                beforeSubmit: function (arr, $from, options) {
                    var isValid = false;
                    if (opts.onBefore && typeof opts.onBefore == "function") {
                        isValid = opts.onBefore(arr, $from, opts);
                        if (!isValid) {
                            return false;
                        }
                    }
                    //isValid = $from.validate({
                    //    onfocusout: false,
                    //    errorPlacement: function (error, element) {//错误提示，错误对象
                    //        alert(error);
                    //        layui.use('layer', function () {
                    //            var layer = layui.layer;
                    //            layer.tips(error[0].innerText, element, {//1.错误信息，2提示位置，3同时提示多个错误
                    //                tipsMore: true//错误信息可以同时提示多个，...
                    //            });
                    //        });
                    //    }
                    //}).valid();
                    //if (!isValid) {
                    //    return false;
                    //}
                    vino.page.msg.showLoad();
                    return true;
                },
                error: function (reply) {
                    _handleMessage(reply, opts);
                },
                success: function (reply) {
                    _handleMessage(reply, opts);
                }
            };
            $target.ajaxForm(options);
            //$target.validate({
            //    errorPlacement: function (error, element) {//错误提示，错误对象
            //        alert(error);
            //        layui.use('layer', function () {
            //            var layer = layui.layer;
            //            layer.tips(error[0].innerText, element, {//1.错误信息，2提示位置，3同时提示多个错误
            //                tipsMore: true//错误信息可以同时提示多个，...
            //            });
            //        });
            //    }
            //});
        }).fail(function () {
            alert("脚本加载出错,请刷新页面重试!");
        });
    }
    
    $.fn.vinoForm = function (options, param) {
        if (typeof options == 'string') {
            return $.fn.vinoForm.methods[options](this, param);
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
                opts = $.extend(opts, $.fn.vinoForm.defaults, options);
                $.data(this, 'options', opts);
            }
            _bind(this);
        });
    };

    $.fn.vinoForm.methods = {
        submit: function (target, options) {
            target.each(function () {
                if (options) {
                    var opts = $.data(this, 'options');
                    opts = $.extend(opts, options);
                }
                $(this).submit();
            });
        }
    };

    $.fn.vinoForm.defaults = {
        type: 'POST',
        dataType: 'json'
    };

})(jQuery);