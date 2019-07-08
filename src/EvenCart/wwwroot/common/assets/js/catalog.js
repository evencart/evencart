var root = "http://localhost:52886";
var useHash = false; // Defaults to: false
var hash = '#!'; // Defaults to: '#'
var router = new Navigo(root, useHash, hash);


var productSearch = {
    set: function (key, value) {
        productSearch.data[key] = value;
        searchProducts();
    },
    setPage: function (page, search) {
        productSearch.data["page"] = page;
        if (search) {
            searchProducts();
        }
    },
    setUrl: function(url) {
        productSearch.url = url;
        this.setPage(1);
        searchProducts();
    },
   
    setSort: function(column, order) {
        productSearch.data["sortColumn"] = column;
        productSearch.data["sortOrder"] = order;
        this.setPage(1);
        searchProducts();
    },
    setPrices: function(from, to) {
        productSearch.data["fromPrice"] = from;
        productSearch.data["toPrice"] = to;
        this.setPage(1);
        searchProducts();
    },
    setFilter: function(key, value, skipLoad) {
        productSearch.filters[key] = productSearch.filters[key] || [];
        if (!productSearch.filters[key].includes(value))
            productSearch.filters[key].push(value);
        if (!skipLoad) {
            this.setPage(1);
            searchProducts();
        }
    },
    resetFilter: function (key, value) {
        productSearch.filters[key] = productSearch.filters[key] || [];
        for (var i = 0; i < productSearch.filters[key].length; i++) {
            if (productSearch.filters[key][i] == value) {
                productSearch.filters[key].splice(i, 1);
                break;
            }
        }
        this.setPage(1);
        searchProducts();
    },
    clearFilter: function (key) {
        if (!key) {
            productSearch.filters = {};
        } else {
            productSearch.filters[key] = [];
        }
        this.setPage(1);
        searchProducts();
    },
    data: {
    },
    url: window.location.pathname,
    filters: {}
};

var searchProducts = function () {
    var filterString = "";
    for (var key in productSearch.filters) {
        if (productSearch.filters.hasOwnProperty(key) && productSearch.filters[key].length > 0) {
            filterString += key + ':"';
            for (var i = 0; i < productSearch.filters[key].length; i++) {
                filterString += productSearch.filters[key][i] + ",";
            }
            filterString = filterString.substr(0, filterString.length - 1);
            filterString += '" ';
        }
    }
    filterString = filterString.trim();

    if (filterString != "")
        productSearch.data.filters = filterString;
    else
        productSearch.data.filters = null;
    ajax({
        url: productSearch.url,
        method: "get",
        data: productSearch.data,
        done: function (response) {
            jQuery("#product-list-container").replaceWith(response);
            var params = jQuery.param(productSearch.data);
            router.navigate(productSearch.url + "?" + params);
            
        }
    });
}

var queryStringToJSON = function() {
    var pairs = location.search.slice(1).split('&');

    var result = {};
    pairs.forEach(function(pair) {
        pair = pair.split('=');
        result[pair[0]] = decodeURIComponent(pair[1] || '');
    });

    return JSON.parse(JSON.stringify(result));
};


ready(function() {
    productSearch.data = jQuery.extend(productSearch.data, queryStringToJSON());
});

window.onpopstate = function (event) {
    if (window._currentPage == "ProductsList")
        productSearch.setUrl(document.location.pathname);
};