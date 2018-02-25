(function () {
    'use strict';

    var factoryId = 'productdataservice';

    angular
        .module('app')
        .service(factoryId, productdataservice);

    productdataservice.$inject = ['$http'
        , 'common',
        'config'];


    function productdataservice($http, common, config) {
        var $q = common.$q;
        var service = {
            getProducts: getProducts,
            getProduct: getProduct,
            getProductByCode: getProductByCode,
            newProduct: newProduct,
            editProduct: editProduct,
            deleteProduct: deleteProduct
        
        }

        return service;
        function getProducts() {
            var url = common.serviceUrl(config.apiServices.products) + 'GetProducts/';
            return $q.when( $http.get(url));
        }

        function getProduct(id) {
            var url = common.serviceUrl(config.apiServices.products) + 'GetProduct/'+id;
            return $q.when($http.get(url));
        }

        function getProductByCode(code) {
            var url = common.serviceUrl(config.apiServices.products) + 'GetProductByCode/' + code;
            return $q.when($http.get(url));
        }
        function newProduct(model) {
            var url = common.serviceUrl(config.apiServices.products) + 'PostProduct/';
            return $q.when($http.post(url,model));
        }
        function editProduct(id,model) {
            var url = common.serviceUrl(config.apiServices.products) + 'PutProduct/'+id;
            return $q.when($http.put(url, model));
        }

        function deleteProduct(id) {
            var url = common.serviceUrl(config.apiServices.products) + 'DeleteProduct/' + id;
            return $q.when($http.delete(url));}
    }
})();