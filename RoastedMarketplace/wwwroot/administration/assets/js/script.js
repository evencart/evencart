var loadTemplates = function () {

}

var generateGrid = function (options) {
    jQuery("#" + options.element).bootgrid({
        ajax: true,
        ajaxSettings: {
            method: options.method,
            cache: true
        },
        post: options.post,
        url: options.url,
        selection: true,
        multiSelect: true,
        columnSelection: false,
        templates: options.templates,
        formatters: options.formatters,
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
    options = jQuery.extend({
        substringMatch: true,
        url: null,
        select: function (selectedItem) { },
        suggestNewAdditions: true,
        clearAfterSelect: false,
        preserveAfterFirstCall: false
    }, options);

    var element = options.element;

    var sourceFunction = function (options) {
        return function (q, syncResults, asyncResults) {
            var dataSearch = function (data) {
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
            if (!options.initialData && options.url) {
                get({
                    url: options.url,
                    data: { q: q },
                    done: function (r) {
                        if (r.success) {
                            if (options.preserveAfterFirstCall)
                                options.initialData = r.suggestions;
                            var matches = dataSearch(r.suggestions);
                            asyncResults(matches);
                        }
                    }
                });
            } else {
                var matches = dataSearch(options.initialData);
                syncResults(matches);
            }
        };
    }

    jQuery('#' + element).typeahead({
        hint: true,
        highlight: true,
        minLength: 1
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
    var method = options.method == "GET" ? "get" : "post";
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