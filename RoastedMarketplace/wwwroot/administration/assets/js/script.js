var validationRule = function (options) {
    var element = jQuery("#" + options.element);
    var rule = options.rule;
    setTimeout(function () { //https://github.com/jquery-validation/jquery-validation/issues/1267#issuecomment-119692849
        element.rules("add", rule);
    });
}
var notify = function (type, msg, withclose) {
    var element = jQuery("<div />");
    element.addClass("notification");
    if (type == "error") {
        element.append("<span class='rbicon-x-circle text-danger margin-r-10'></span><strong>Error!</strong>");
    }
    else if (type == "success") {
        element.append("<span class='rbicon-check-circle margin-r-10 text-success'></span><strong>Success</strong>");
    }
    element.append("<div class='msg'>" + msg + "</div>");
    jQuery("body").append(element);
    element.center();
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

var initAjaxForm = function (formId, options) {
    formId = "#" + formId;
    options = jQuery.extend({},
        {
            extraData: null,
            onError: function () { },
            onSuccess: function () { }
        },
        options);
    jQuery(formId).submit(function(e) {
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
                var method = jQuery(form).attr("method") || "post";
                var action = jQuery(form).attr("action") || window.location.href;

                var ajaxOptions = {
                    url: action,
                    data: object,
                    method: method,
                    done: function(response) {
                        if (response.success) {
                            if (options.onSuccess)
                                options.onSuccess(response);
                        } else {
                            if (options.onError) {
                                var errMsg = "";
                                if (response.errors) {
                                    var errors = response.errors;
                                    var errorList = "<ul>";
                                    errors.forEach(function (err) {
                                        errorList += "<li>" + err + "</li>";
                                    });
                                    errorList += "</ul>";
                                }
                                else if (response.error)
                                    errMsg = response.error;
                                else
                                    errMsg = "An error occured while completing operation";
                                notify("error", errMsg);
                                options.onError(response);
                            }
                        }
                    },
                    fail: options.onError
                };
                ajax(ajaxOptions);
            }
        });
}

var windowConfirm = window.confirm;
var confirm = function (msg, action) {
    if (windowConfirm(msg)) {
        if (action)
            action();
        return true;
    }
    return false;
}

var showAsPopup = function (id, ajax, onClose) {
    var overlay = "<div class='overlay'></div>";
    var element = jQuery("#" + id);
    element.addClass("popup");
    jQuery("body").append(overlay);
    jQuery("body").append(element);
    element.center();
    element.show();

    element.find(".close-popup, .popup-close").click(function () {
        hidePopup(id);
    });

    //center element on window resizes
    jQuery(window).resize(function () {
        element.center();
    });

    if (onClose && isFunction(onClose)) {

        element.data("popup.onclose",
            function (result) {
                onClose(result);
            });
    }
    if (ajax) {
        //if this is an ajax call, return a callback that user can call, in which we recenter the popup
        return function () {
            element.center();
            element.find(".close-popup, .popup-close").click(function () {
                hidePopup(id, "cancel");
            });
        }
    }

}

var hidePopup = function (id, result) {
    result = result || "ok";
    jQuery(".overlay").remove();
    var element = jQuery("#" + id);
    element.removeClass("popup");
    element.hide();

    if (element.data("popup.onclose")) {
        element.data("popup.onclose")(result);
    }
}

var reloadGrid = function (name) {
    //reload the grid
    generateGrid({
        element: name,
        reload: true
    });
}
var generateGrid = function (options) {
    if (!options)
        return;
    options = jQuery.extend({}, {
        reload: false,
        method: "GET",
        data: null,
        selection: true,
        multiSelect: true,
        columnSelection: false,
        navigation: true,
        initialData: null

    }, options);
    if (options.reload) {
        jQuery("#" + options.element).bootgrid("reload");
        return;
    }
    var initData = null;
    if (options.initialData) {
        initData = [];
        initData["success"] = true;
        initData[options.responseObject] = options.initialData;
    }
    jQuery("#" + options.element).bootgrid({
        ajax: true,
        ajaxSettings: {
            method: options.method,
            cache: true
        },
        initialData: initData,
        post: options.data,
        url: options.url,
        selection: options.selection,
        multiSelect: options.multiSelect,
        columnSelection: options.columnSelection,
        templates: options.templates,
        formatters: options.formatters,
        navigation: options.navigation,
        responseHandler: function (response) {
            if (response.success) {
                return {
                    current: response.page,
                    total: response.totalResults,
                    rowCount: response.count,
                    rows: response[options.responseObject]
                };
            }
            return null;
        },
        css: {
            iconRefresh: "rbicon-refresh-cw",
            paginationButton: "page-link"
        }
    });
}

var gridFormatters = {
    binary: function (column, row, key) {
        if (row[key])
            return "<span class='rbicon-check text-success'></span>";
        else
            return "<span class='rbicon-x text-danger'></span>";
    }
};
var initFileUploader = function (options) {
    jQuery('#' + options.element).fileupload(options);
}

var displayOrderSortable = function (options) {
    var container = options.container;
    var itemSelector = options.itemSelector;
    jQuery("#" + container + " " + itemSelector).addClass("draggable");
    if (options.refresh) {
        jQuery("#" + container).sortable('refresh');
        return;
    }
    jQuery("#" + container).sortable({
        update: function () {
            //update hidden fields for display order
            jQuery("#" + container + " " + itemSelector + " input[data-type='displayorder']").each(function (index) {
                jQuery(this).val(index);
            });

            if (options.update)
                options.update();
        }
    });
}

var inputTypeahead = function (options) {
    options = jQuery.extend({}, {
        substringMatch: true,
        url: null,
        select: function (selectedItem) { },
        suggestNewAdditions: true,
        clearAfterSelect: false,
        preserveAfterFirstCall: false,
        openOnFocus: false,
        minLength: 3,
        destroy: false,
        multiple: false,
        value: false,
        beforeItemRemoved: function (evt) { },
        itemRemoved: function (evt) { },
        itemAdded: function (evt) { }
    }, options);

    var element = options.element;
    if (options.destroy) {
        if (!options.multiple) {
            jQuery('#' + element).typeahead("destroy");
        } else {
            if (jQuery('#' + element).data("taginit"))
                jQuery('#' + element).tagsinput("destroy");
        }
        jQuery('#' + element).val("");
        return;
    }
    if (options.value) {
        if (!options.multiple) {
            return jQuery('#' + element).typeahead('val');
        } else {
            return jQuery('#' + element).tagsinput("items");
        }
    }
    var dataSearch = function (q, data) {
        // an array that will be populated with substring matches
        var matches = [];

        // regex used to determine if a string contains the substring `q`
        var substrRegex = new RegExp((!options.substringMatch ? "^" : "") + q, 'i');

        // iterate through the pool of strings and for any string that
        // contains the substring `q`, add it to the `matches` array
        jQuery.each(data, function (index, obj) {
            var str = typeof obj == "object" ? obj.text : obj;
            if (substrRegex.test(str)) {
                matches.push(obj);
            }
        });

        if (matches.length == 0 && options.suggestNewAdditions) {
            matches.push({
                id: 0,
                text: q
            });
        }
        return matches;
    }
    var sourceFunction = function (options) {
        return function (q, syncResults, asyncResults) {
            if (!options.initialData && options.url) {
                var url = options.url;
                if (isFunction(url))
                    url = url();
                if (url) {
                    get({
                        url: url,
                        data: { q: q },
                        done: function (r) {
                            if (r.success) {
                                if (options.preserveAfterFirstCall)
                                    options.initialData = r.suggestions;
                                var matches = dataSearch(q, r.suggestions);
                                setTimeout(function () {
                                    asyncResults(matches);
                                },
                                    0);

                            }
                        }
                    });
                    return;
                }
            }
            var matches = dataSearch(q, options.initialData);
            syncResults(matches);
        };
    }

    if (options.multiple) {
        jQuery('#' + element).tagsinput({
            allowDuplicates: false,
            trimValue: true,
            itemText: "text",
            itemValue: "text",
            typeaheadjs: [
                {
                    hint: true,
                    highlight: true,
                    minLength: options.minLength
                }, {
                    source: sourceFunction(options),
                    display: function (selection) {
                        if (selection.id == 0)
                            return "Add " + selection.text;
                        return selection.text;
                    }
                }
            ]
        });
        if (options.initialData) {
            options.initialData.forEach(function (item) {
                jQuery('#' + element).tagsinput('add', item);
            });
        }
        if (options.itemAdded)
            jQuery('#' + element).on("itemAdd", options.itemAdded);
        if (options.beforeItemRemoved)
            jQuery('#' + element).on("beforeItemRemove", options.beforeItemRemoved);
        if (options.itemRemoved)
            jQuery('#' + element).on("itemRemove", options.itemRemoved);

        jQuery('#' + element).data("taginit", true);

    }
    else {

        jQuery('#' + element).typeahead({
            hint: true,
            highlight: true,
            minLength: options.minLength
        },
            {
                source: sourceFunction(options),
                display: function (selection) {
                    if (selection.id == 0)
                        return "Add " + selection.text;
                    return selection.text;
                }
            })
            .bind('typeahead:select',
                function (ev, suggestion) {
                    if (suggestion.id == 0) {
                        jQuery(this).typeahead('val', suggestion.text);
                    }
                    options.select(suggestion);
                    if (options.clearAfterSelect)
                        jQuery(this).typeahead('val', "");
                });

        if (options.openOnFocus) {
            jQuery('#' + element).focus(function () {
                if (jQuery(this).val() == "")
                    jQuery(this).typeahead('open');
            });
        }
    }

}

var ajaxExtend = function (options) {
    jQuery.extend({
        url: "",
        data: [],
        done: function () { },
        fail: function () { },
        always: function () { }
    },
        options);
    return options;
}
var ajax = function (options) {
    var method = options.method.toLowerCase() == "get" ? "get" : "post";
    var jqxhr = jQuery[method](options.url, options.data)
        .done(options.done)
        .fail(options.fail)
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