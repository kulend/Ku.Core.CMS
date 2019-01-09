(function () {
    UM.plugins["myimage"] = function () {
        var me = this;
        //执行命令
        me.commands["myimage"] = {
            execCommand: function () {
                OpenWindow("选择图片", `MaterialCenter/Picture/Selector?max=99`, { area: ['837px', '468px'], maxmin: false }, function (files) {
                    if (!files.length) return;
                    var images = [];
                    for (var i = 0; i < files.length; i++) {
                        images.push({ src: files[i].url });
                    }
                    me.execCommand("insertimage", images);
                });
            }
        };
    };

    UM.registerUI('myimage', function (name) {
        var me = this;
        var $btn = $.eduibutton({
            icon: name,
            click: function () {
                me.execCommand(name);
            },
            title: this.getLang('labelMap')[name] || ''
        });

        this.addListener('selectionchange', function () {
            var state = this.queryCommandState(name);
            $btn.edui().disabled(state === -1).active(state === 1);
        });
        return $btn;
    });
})(jQuery)