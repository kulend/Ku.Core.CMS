function grid_templet_bool(field) {
    return '<div>{{# if(d.' + field + '){ }} <i class="layui-icon">&#xe605;</i> {{# }else{ }} <i class="layui-icon">&#x1006;</i> {{# } }}</div>';
}

function grid_templet_operate(btns) {
    var html = '<div>';
    for (var i = 0; i < btns.length; i++) {
        html += createOperateBtn(btns[i]);
    }
    html += '</div>';
    return html;
}

function createOperateBtn(btn) {
    var authcode = btn.code;
    if (authcode) {
        if (!authCodes) {
            return "";
        }
        if (authCodes.indexOf('0ec3eaea52dc2a0cbc5008f712a11e25') < 0) {
            //检查权限
            var md5Code = hex_md5(authcode);
            if (authCodes.indexOf(md5Code) < 0) {
                return "";
            }
        }
    }
    var isWarn = false;
    if (authcode && authcode.indexOf("delete") > 0) {
        isWarn = true;
    }
    return `<button type="button" class="layui-btn layui-btn-xs ${isWarn ? 'layui-btn-danger' : ''} ${btn.css}" action="${btn.action}" after="gridReload()" title="${btn.title}">${btn.title}</button>`;
}