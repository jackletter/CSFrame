var _server = "http://localhost:8080/";

var href = window.location.href;
//调试入口
$(document).keydown(evt => {
    if (evt.key == "F12" && evt.ctrlKey) {
        cef.ShowDevTools();
    }
    if (evt.key == "F5" && evt.ctrlKey) {
        cef.RefreshPage();
    }
});

//页面遮罩的加载和显示
var _load_handle;
function showLoading() {
    if (_load_handle) layer.close(_load_handle);
    _load_handle = layer.load(2);
}
function hideLoading() {
    if (_load_handle) layer.close(_load_handle);
}
function queryPara(para) {
    var url = window.location.search.substr(1);
    var reg = new RegExp("(^|&)" + para + "=([^&]*)(&|$)", 'i');
    var r = url.match(reg);
    return r == null ? null : unescape(r[2]);
}