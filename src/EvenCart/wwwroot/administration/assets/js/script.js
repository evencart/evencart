jQuery(document).ready(function () {
    var width = jQuery(".sidenav").width();
    jQuery(".mobile-navigation").on("click",
        function () {
            var currentLeft = jQuery(".sidenav").position().left;
            if (currentLeft == 0) {
                //hide it
                currentLeft -= width;
            } else {
                currentLeft = 0;
            }
            jQuery(".sidenav").animate({ left: currentLeft });
            jQuery(".client-main, .client-main-navlinks, .client-main-content").animate({ left: currentLeft + width });
        });

    //dependent controls
    jQuery("[data-visible]").hide();
    setInterval(function() {
            jQuery("[data-visible]").each(function() {
                var result = eval(jQuery(this).attr("data-visible"));
                if (result) {
                    jQuery(this).slideDown();
                } else {
                    jQuery(this).slideUp();
                }
            });
        },
        100);
});
$(document).on("keypress", 'form', function (e) {
    var code = e.keyCode || e.which;

    if (code == 13) {
        if (!jQuery(e.target).hasClass("trumbowyg-editor") && jQuery(e.target)[0].tagName != "TEXTAREA") {
            e.preventDefault();
            return false;
        }
    }
});

var initTopNav = function (freshRun) {

    var $moreLi = jQuery("#li-secondary-more");
    var $moreLiA = jQuery("#li-secondary-more > a");
    var $secondaryNavigationUl = jQuery("#secondaryNavigationUl");
    var $moreSubMenu = jQuery("#more-sub-menu");
    //move all more to main nav
    while ($moreSubMenu.children("li").length > 0) {
        $moreSubMenu.children("li:first-child").insertBefore($moreLi);
    }

    var maxWidth = jQuery(".client-main-navlinks").outerWidth();
    var saveButtonContainerWidth = jQuery(".save-button-container").is(":visible") ? jQuery(".save-button-container").outerWidth() : 0;
    var secondaryNavWidth = $secondaryNavigationUl.outerWidth();
    var maxAllowedWidth = maxWidth - saveButtonContainerWidth - $moreLi.outerWidth();



    if (maxAllowedWidth <= secondaryNavWidth) {
        //move main nav items to more
        while (secondaryNavWidth >= maxAllowedWidth) {
            var lastChild = $secondaryNavigationUl.children("li.desktop-secondary-navigation").last();
            lastChild.prependTo($moreSubMenu);
            secondaryNavWidth = $secondaryNavigationUl.outerWidth();
        }
    }

    if ($moreSubMenu.children("li").length == 0) {
        $moreLi.hide();
    } else {
        $moreLi.show();
    }
    if (freshRun) {
        $moreLi.on("click",
            function (e) {
                $moreSubMenu.slideDown(100);
                if ($moreSubMenu.offset().left < 0) {
                    $moreSubMenu.css("left", 0);
                } else {
                    $moreSubMenu.css("right", 0);
                }
                e.stopPropagation();
            });
        jQuery("html").click(function (e) {

            if ($moreSubMenu.is(":visible"))
                $moreSubMenu.hide();
        });
    };


};
var showAsPopup = function (id, ajax, onClose, onOpen) {
    var overlay = "<div class='overlay'></div>";
    var element = jQuery("#" + id);
    element.addClass("popup");
    jQuery("body").append(overlay);
    jQuery("body").append(element);
    jQuery(".client-main-content").addClass("active-dialog");
    element.center('absolute');
    element.show();
    if (onOpen)
        onOpen();
    element.find(".close-popup, .popup-close").click(function () {
        hidePopup(id, "cancel");
    });

    //center element on window resizes
    jQuery(window).resize(function () {
        element.center();
    });

    if (onClose && isFunction(onClose)) {

        element.data("popup.onclose",
            function (result, data) {
                onClose(result, data);
            });
    }
    if (ajax) {
        //if this is an ajax call, return a callback that user can call, in which we recenter the popup
        return function () {
            element.hide();
            setTimeout(function () {
                element.center('absolute');
                element.show();
            }, 300);

            element.find(".close-popup, .popup-close").click(function () {
                hidePopup(id, "cancel");
            });
        }
    }

}

var hidePopup = function (id, result, data) {
    result = result || "ok";
    jQuery(".overlay").remove();
    var element = jQuery("#" + id);
    element.removeClass("popup");
    element.hide();
    jQuery(".client-main-content").removeClass("active-dialog");
    if (element.data("popup.onclose")) {
        element.data("popup.onclose")(result, data);
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
        url: null,
        reload: false,
        method: "GET",
        data: null,
        selection: true,
        multiSelect: true,
        columnSelection: false,
        navigation: true,
        initialData: null,
        keepSelection: false,
        done: null,
        events: null
    }, options);
    if (options.reload) {
        jQuery("#" + options.element).bootgrid("reload");
        return;
    }

    var dataFormatter = function (response) {
        var rows = null;
        if (typeof options.responseObject == "function") {
            rows = options.responseObject(response);
        } else {
            if (response[options.responseObject])
                rows = response[options.responseObject];
        }
        return rows;
    }
    if (options.initialData && options.initialData[options.responseObject]) {

        options.initialData["success"] = true;
    }

    var grid = jQuery("#" + options.element).bootgrid({
        ajax: options.url != null,
        ajaxSettings: {
            method: options.method,
            cache: true,
            traditional: true
        },
        rowCount: [15, 30, 50, 100],
        initialData: options.initialData,
        url: options.url,
        selection: options.selection,
        multiSelect: options.multiSelect,
        columnSelection: options.columnSelection,
        templates: options.templates,
        formatters: options.formatters,
        navigation: options.navigation,
        keepSelection: options.keepSelection,
        requestHandler: function (request) {
            if (options.data)
                request = jQuery.extend(request, typeof options.data == "function" ? options.data() : "");
            return request;
        },
        responseHandler: function (response) {
            if (response.success) {
                if (options.done)
                    options.done(response);
                var rows = dataFormatter(response);
                return {
                    current: response.current,
                    total: response.total,
                    rowCount: response.rowCount,
                    rows: rows
                };
            }
            return null;
        },
        css: {
            iconRefresh: "rbicon-refresh-cw",
            paginationButton: "page-link"
        }
    });
    if (options.events) {
        for (var event in options.events) {

            if (options.events.hasOwnProperty(event)) {
                var callback = options.events[event];
                if (callback) {
                    grid.on(event + ".rs.jquery.bootgrid", callback);
                }
            }

        }
    }
}

var getGridRows = function (id) {
    var currentRows = jQuery("#" + id).bootgrid("getCurrentRows");
    return currentRows;
}

var getGridSelections = function (id) {
    var currentRows = jQuery("#" + id).bootgrid("getCurrentRows");
    var selectedRowIds = jQuery("#" + id).bootgrid("getSelectedRows");
    var resultRows = [];
    selectedRowIds.forEach(function (rowId) {
        currentRows.forEach(function (row) {
            if (row.id == rowId) {
                resultRows.push(row);
            }
        });
    });
    return resultRows;
}

var addRowsToGrid = function (id, rows) {
    jQuery("#" + id).bootgrid("append", rows);
}

var removeRowFromGrid = function (id, rowId) {
    jQuery("#" + id).bootgrid("remove", [rowId]);
}
var clearGrid = function (id) {
    jQuery("#" + id).bootgrid("clear");
}

var gridFormatters = {
    binary: function (column, row, key) {
        key = key || column.id;
        if (row[key])
            return "<span class='rbicon-check text-success'></span>";
        else
            return "<span class='rbicon-x text-danger'></span>";
    },
    formatted: function (column, row) {
        return row.formatted[column.id];
    }
};

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

var inputTypeahead = function(options) {
    options = jQuery.extend({},
        {
            substringMatch: true,
            url: null,
            select: function(selectedItem) {},
            suggestNewAdditions: true,
            clearAfterSelect: false,
            preserveAfterFirstCall: false,
            openOnFocus: false,
            minLength: 1,
            destroy: false,
            multiple: false,
            value: false,
            beforeItemRemoved: function(evt) {},
            itemRemoved: function(evt) {},
            itemAdded: function(evt) {},
            data: [],
            stringTags: false,
            newAdditionId: function(q) {
                return q;
            }
        },
        options);

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
    var dataSearch = function(q, data) {
        // an array that will be populated with substring matches
        var matches = [];

        // regex used to determine if a string contains the substring `q`
        var substrRegex = new RegExp((!options.substringMatch ? "^" : "") + q, 'i');

        // iterate through the pool of strings and for any string that
        // contains the substring `q`, add it to the `matches` array
        jQuery.each(data,
            function(index, obj) {
                var str = typeof obj === "object" ? obj.text : obj;
                if (substrRegex.test(str)) {
                    matches.push(obj);
                }
            });

        if (matches.length == 0 && options.suggestNewAdditions) {
            if (options.stringTags) {
                matches.push(q);
            } else {
                matches.push({
                    id: options.newAdditionId(q),
                    text: q
                });
            }
        }
        return matches;
    };
    var sourceFunction = function(options) {
        return function(q, syncResults, asyncResults) {
            if (!options.initialData && options.url) {
                var url = options.url;
                if (isFunction(url))
                    url = url();
                if (url) {
                    get({
                        url: url,
                        data: { q: q },
                        done: function(r) {
                            if (r.success) {
                                if (options.preserveAfterFirstCall)
                                    options.initialData = r.suggestions;
                                var matches = dataSearch(q, r.suggestions);
                                setTimeout(function() {
                                        asyncResults(matches);
                                    },
                                    0);

                            }
                        }
                    });
                    return;
                }
            }
            var data = options.data || options.initialData;
            var matches = dataSearch(q, data);
            syncResults(matches);
        };
    };

    if (options.multiple) {
        var inputOptions = {
            allowDuplicates: false,
            trimValue: true,
            freeInput: options.suggestNewAdditions,
            confirmKeys: [13, 9],
            typeaheadjs: [
                {
                    hint: true,
                    highlight: true,
                    minLength: options.minLength
                }, {
                    source: sourceFunction(options),
                    display: function(selection) {
                        if (options.stringTags)
                            return selection;
                        if (selection.id == 0)
                            return "+ " + selection.text;
                        return selection.text;
                    }
                }
            ]
        };
        if (!options.stringTags) {
            inputOptions.itemText = "text";
            inputOptions.itemValue = "id";
        } else {
            inputOptions.stringTags = true;
        }
        jQuery('#' + element).tagsinput(inputOptions);
        if (options.initialData) {
            options.initialData.forEach(function(item) {
                jQuery('#' + element).tagsinput('add', item);
            });
        }
        if (options.itemAdded)
            jQuery('#' + element).on("itemAdded", options.itemAdded);
        if (options.beforeItemRemoved)
            jQuery('#' + element).on("beforeItemRemove", options.beforeItemRemoved);
        if (options.itemRemoved)
            jQuery('#' + element).on("itemRemoved", options.itemRemoved);

        jQuery('#' + element).data("taginit", true);

    } else {

        jQuery('#' + element).typeahead({
                    hint: true,
                    highlight: true,
                    minLength: options.minLength
                },
                {
                    source: sourceFunction(options),
                    display: function(selection) {
                        if (options.stringTags)
                            return "+ " + selection;
                        if (selection.id == 0) {
                            return "+ " + selection.text;
                        }
                        return selection.text;
                    }
                })
            .bind('typeahead:select',
                function(ev, suggestion) {
                    if (suggestion.id == 0) {
                        jQuery(this).typeahead('val', suggestion.text);
                    }
                    options.select(suggestion);
                    if (options.clearAfterSelect)
                        jQuery(this).typeahead('val', "");
                });
        jQuery('#' + element).on('blur',
            function(e) {
                var val = jQuery(this).val();
                if (val.startsWith("+ "))
                    jQuery(this).val(val.substring(2));

            });
        if (options.openOnFocus) {
            jQuery('#' + element).focus(function() {
                if (jQuery(this).val() == "")
                    jQuery(this).typeahead('open');
            });
        }
    }

};


var initAreYouSure = function () {
    //init are you sure
    jQuery('form').areYouSure();
};