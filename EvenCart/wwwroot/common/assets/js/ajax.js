var initAjaxForm = function (formId, options) {
    formId = "#" + formId;
    options = jQuery.extend({},
        {
            extraData: null,
            onError: function () { },
            onSuccess: function () { }
        },
        options);
    jQuery(formId).submit(function (e) {
            e.preventDefault();
        })
        .validate({
            submitHandler: function (form) {
                if (isFunction(options.extraData)) {
                    options.extraData = options.extraData();
                    if (!options.extraData) {
                        if (options.onError)
                            options.onError();
                        return;
                    }
                }
                //get form object
                var object = jQuery(form).serializeObject();
                //add additional parameters
                object = jQuery.extend(object, options.extraData);
                if (options.beforeSubmit) {
                    if (!options.beforeSubmit(object))
                        return;
                }
                var method = jQuery(form).attr("method") || "post";
                var action = jQuery(form).attr("action") || window.location.href;

                var ajaxOptions = {
                    url: action,
                    data: object,
                    method: method,
                    done: function (response) {
                        if (response.success) {
                            if (options.onSuccess)
                                options.onSuccess(response);
                        } else {

                            if (options.onError) {
                                options.onError(response);
                            }
                        }
                    },
                    fail: options.onError
                };
                ajax(ajaxOptions);
            },
            errorPlacement : options.errorPlacement
        });
}

var loadHtml = function(options) {
    options = jQuery.extend({
            method: "GET",
            url: null,
            data: null
        },
        options);

    if (!options.url)
        return null;
    ajax({
        url: options.url,
        method: options.method,
        data: options.data,
        done: function (response) {
            if (!options.replace) {
                jQuery(options.element).html(response);
                return;
            }
            jQuery(options.element).replaceWith(response);
        }
    });
};

var reloadComponent = function(componentName, data, rootElement) {
    var componentUrl = "/component/" + componentName;
    data = data || {};
    loadHtml({
        url: componentUrl,
        data: data,
        element: rootElement,
        replace: true,
        method: "POST"
    });
}

var loadRawView = function(viewPath) {
    var url = "/api/dashboard/rawview";
    ajax({
        url: url,
        data: {
            viewPath : viewPath
        },
        done: function(response) {
            
        },
        method: "GET"
    });
}

var notify = function (type, msg, withclose) {
    var element = jQuery("<div />");
    element.addClass("notification");
    if (type == "error") {
        element.append("<span class='rbicon-x-circle fa fa-times text-danger margin-r-10'></span><strong>Error!</strong>");
    }
    else if (type == "success") {
        element.append("<span class='rbicon-check-circle fa fa-check-circle margin-r-10 text-success'></span><strong>Success</strong>");
    }
    element.append("<div class='msg'>" + msg + "</div>");
    jQuery("body").append(element);
    element.center("fixed");
    element.css("top", 10);
    element.fadeIn();
    var closeNotify = function () {
        element.fadeOut(function () {
            element.remove();
        });

    }
    setTimeout(closeNotify, 5000);
    element.click(closeNotify);
}
var ajaxExtend = function (options) {
    jQuery.extend({
            url: "",
            data: [],
            done: function () { },
            fail: function () { },
            always: function () { },
            method: "GET"
        },
        options);
    return options;
}

var loaderInterval;
//global progress
jQuery(document).ajaxStart(function () {
    jQuery("body").append("<div id='loader'><div class='handle'></div></div>");
    $('#loader').show();
    var position = 0;
    loaderInterval = setInterval(function () {
        jQuery("#loader .handle").css("left", position + "%");
        position++;
        if (position >= 100)
            position = 0;
    }, 10);
});
jQuery(document).ajaxComplete(function () {
    jQuery('#loader').remove();
    clearInterval(loaderInterval);
});

var ajax = function (options) {
    options = ajaxExtend(options);
    var method = options.method.toLowerCase() === "get" ? "get" : "post";
    var jqxhr = jQuery[method](options.url, options.data)
        .done(function (response, status, xhr) {
            var ct = xhr.getResponseHeader("content-type") || "";
            //do the notification only for json response
            if (ct.indexOf("application/json") > -1) {
                if (response.redirect) {
                    //if there is a redirection
                    window.location.href = response.url;
                    return;
                }
                if (!response.success) {
                    var errMsg = "";
                    if (response.errors) {
                        var errors = response.errors;
                        var errorList = "<ul>";
                        errors.forEach(function (err) {
                            errorList += "<li>" + err + "</li>";
                        });
                        errorList += "</ul>";
                        errMsg = errorList;
                    }
                    else if (response.error)
                        errMsg = response.error;
                    else if (response.message)
                        errMsg = response.message;
                    else
                        errMsg = "An error occured while completing operation";
                    notify("error", errMsg);
                    if (options.fail)
                        options.fail(response);
                }
                
            }
            if (options.done)
                options.done(response);
        })
        .fail(function (response) {
            notify("error", "Something didn't go right there", true);
            if (options.fail)
                options.fail(response);
        })
        .always(options.always);
    return jqxhr;
}
var get = function (options) {
    options = ajaxExtend(options);
    options.method = "GET";
    return ajax(options);
}
var post = function (options) {
    options = ajaxExtend(options);
    options.method = "POST";
    return ajax(options);
}
