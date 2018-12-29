/**
 * Replace all for string
 */
String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.split(search).join(replacement);
};

jQuery.fn.center = function () {
    this.css("position", "absolute");
    this.css("top", (jQuery(window).height() - this.height()) / 2 + jQuery(window).scrollTop() + "px");
    this.css("left", (jQuery(window).width() - this.width()) / 2 + jQuery(window).scrollLeft() + "px");
    return this;
}

jQuery.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

var isFunction = function (variable) {
    return variable && typeof (variable) == "function";
}
var validationRule = function (options) {
    var element = jQuery("#" + options.element);
    var rule = options.rule;
    setTimeout(function () { //https://github.com/jquery-validation/jquery-validation/issues/1267#issuecomment-119692849
        element.rules("add", rule);
    }, 0);
}

var windowConfirm = windowConfirm || window.confirm;
var confirm = function (msg, action) {
    if (windowConfirm(msg)) {
        if (action)
            action();
        return true;
    }
    return false;
}
