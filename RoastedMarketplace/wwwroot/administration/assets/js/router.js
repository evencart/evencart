if (!window.initialized) {
    var root = "https://localhost:44387";
    var useHash = false; // Defaults to: false
    var hash = '#!'; // Defaults to: '#'
    var router = new Navigo(root, useHash, hash);
    var liquidEngine = window.Liquid();
    var api = "api";
    window.templates = window.templates || [];
    window.contexts = window.contexts || [];
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
                                var router_url = url.replaceAll("{", ":").replaceAll("}", "");
                                //remove scripts
                                var htmlTemplate = templates[url];
                                window.templates[router_url] = htmlTemplate;
                                routeCollection[router_url] = routeHandler;
                            }
                        }
                        router.on(routeCollection);
                        window.contexts[context] = true;
                    }
                }
            });
        }
    }

    var routeHandler = function (route, params, query) {
        if (window.templates.hasOwnProperty(route)) {
            var template = window.templates[route];
            var currentUrl = window.location.pathname;
            var apiUrl = currentUrl.replace("/admin", "/admin/" + api);
            //request data from api
            get({
                url: apiUrl,
                done: function (response) {
                    liquidEngine
                        .parseAndRender(template, response)
                        .then(function (t) {
                            var newDoc = document.open("text/html", "replace");
                            newDoc.write(t);
                            newDoc.close();
                            setupLinks();
                        });

                },
                fail: function () {
                    //full reload if any error occured
                    window.location.reload();
                }
            });

        }
    }

    var setupLinks = function() {
        jQuery("a").click(function(e) {
            var linkHostName = jQuery(this).prop("hostname");
            if (linkHostName != document.location.hostname) {
                return;
            }
            e.preventDefault();

            var href = jQuery(this).attr("href");
            router.navigate(href, href.startsWith("http://") || href.startsWith("https://") );
        });
        window.onpopstate = function (event) {
            router.navigate(document.location.href, true);
        };
    }
    window.initialized = true;
    
}
setupLinks();

