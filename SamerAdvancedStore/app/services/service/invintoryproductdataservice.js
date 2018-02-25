(function () {
    'use strict';

    var factoryId = 'invintoryproductdataservice';

    angular
        .module('app')
        .service(factoryId, invintoryproductdataservice);

    invintoryproductdataservice.$inject = ['$http'
        , 'common',
        'config'];


    function invintoryproductdataservice($http, common, config) {
        var $q = common.$q;
        var service = {
            getInvintoryproducts: getInvintoryproducts,
            getInvintoryProduct: getInvintoryProduct,
            postInvintoryProduct: postInvintoryProduct,
            putInvintoryProduct: putInvintoryProduct,
            deleteInvintoryProduct: deleteInvintoryProduct
        
        }

        return service;
        function getInvintoryproducts(id) {
            var url = common.serviceUrl(config.apiServices.invintoryProducts) + 'GetInvintoryProducts/'+id;
            return $q.when( $http.get(url));
        }

        function getInvintoryProduct(id) {
            var url = common.serviceUrl(config.apiServices.invintoryProducts) + 'GetInvintoryProduct/'+id;
            return $q.when($http.get(url));
        }
        function postInvintoryProduct(model) {
            console.log(model);
            var url = common.serviceUrl(config.apiServices.invintoryProducts) + 'PostInvintoryProduct/';
            return $q.when($http.post(url,model));
        }
        function putInvintoryProduct(id,model) {
            var url = common.serviceUrl(config.apiServices.invintoryProducts) + 'PutInvintoryProduct/'+id;
            return $q.when($http.put(url, model));
        }

        function deleteInvintoryProduct(id) {
            var url = common.serviceUrl(config.apiServices.invintoryProducts) + 'DeleteInvintoryProduct/' + id;
            return $q.when($http.delete(url));}
    }
})();