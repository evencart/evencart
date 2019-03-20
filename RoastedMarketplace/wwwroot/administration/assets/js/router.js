if (!window.initialized) {
   
    var root = "http://localhost:52886";
    var useHash = false; // Defaults to: false
    var hash = '#!'; // Defaults to: '#'
    var router = new Navigo(root, useHash, hash);
    var liquidEngine = window.Liquid();
    var api = "api";
    window.templates = window.templates || [];
    window.contexts = window.contexts || [];
    window.routes = window.routes || [];
    var useContext = function (context, url) {
        if (!window.contexts.hasOwnProperty(context)) {
            //download the routes
            get({
                url: url,
                data: {
                    context: context
                },
                done: function (response) {
                    if (response.success) {
                        var templates = response.templates[context];
                        var routeCollection = [];
                        for (var url in templates) {
                            if (templates.hasOwnProperty(url)) {
                                var routerUrl = url.replaceAll("{", ":").replaceAll("}", "");
                                //remove scripts
                                var htmlTemplate = templates[url];
                                window.templates[routerUrl] = htmlTemplate;
                                routeCollection[routerUrl] = routeHandler;
                            }
                        }
                        router.on(routeCollection);
                        window.contexts[context] = true;
                        window.routes = window.routes.concat(router._routes);
                    }
                }
            });
        }
    }

    var routeHandler = function (route, params, query) {
        loadPage(route,
            true,
            null,
            function (t) {
              
                var newDoc = document.open("text/html", "replace");
                newDoc.write(t);
                newDoc.close();
            },
            function () {
                //full reload if any error occured
                window.location.reload();
            });
    }
    window.initialized = true;

}

var getApiUrl = function (url) {
    return url.replace("/admin", "/admin/" + api + "?storeMeta=currentUser&storeMeta=store");
}
var setupLinks = function () {
    ready(function () {
        router._routes = window.routes;
        jQuery(".client-main-navlinks a").on("click",
            function (e) {
                var linkHostName = jQuery(this).prop("hostname");
                if (linkHostName != document.location.hostname) {
                    return;
                }
                var href = jQuery(this).attr("href");
                if (href) {
                    e.preventDefault();
                    navigate(href, href.startsWith("http://") || href.startsWith("https://"));
                }
                
            });
        window.onpopstate = function (event) {
            navigate(document.location.pathname, null, true);
        };
    });
};

setupLinks();

function navigate(url, absolute, skipHistory) {
    //if we have a route, we'll navigate, else we reload
    if (router.helpers.match(url, router._routes)) {
        router.navigate(url, absolute, skipHistory);

    } else {
        window.location.href = url;
    }

}
/**
 * Loads a page from cache or gets from url caches
 */
function loadPage(url, fallbackToApi, data, done, fail) {
    if (window.templates.hasOwnProperty(url)) {
        var template = window.templates[url];
        url = window.location.pathname;
        if (!fallbackToApi) {
            done(template);
            return;
        }
        //fetch from api
        var apiUrl = getApiUrl(url);
        //request data from api
        get({
            url: apiUrl,
            done: function (response) {
                liquidEngine
                    .parseAndRender(template, response)
                    .then(function (t) {
                        done(t);
                    });

            },
            fail: fail
        });
    } else {
        get({
            url: url,
            data: data,
            done: function (response) {
                done(response);
            },
            fail: fail
        });
    }
}