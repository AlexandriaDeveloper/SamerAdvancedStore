(function () {
    'use strict';

    var factoryId = 'orderdataservice';

    angular
        .module('app')
        .service(factoryId, orderdataservice);

    orderdataservice.$inject = ['$http'
        , 'common',
        'config'];


    function orderdataservice($http, common, config) {
        var $q = common.$q;
        var service = {
            getOrders: getOrders,
            getOrder: getOrder,
            newOrder: newOrder,
            editOrder: editOrder,
            deleteOrder: deleteOrder
        
        }

        return service;
        function getOrders() {
            var url = common.serviceUrl(config.apiServices.order) + 'GetOrders/';
            return $q.when( $http.get(url));
        }

        function getOrder(id) {
            var url = common.serviceUrl(config.apiServices.order) + 'GetOrder/'+id;
            return $q.when($http.get(url));
        }
        function newOrder(model) {
            var url = common.serviceUrl(config.apiServices.order) + 'PostOrder/';
            return $q.when($http.post(url,model));
        }
        function editOrder(id,model) {
            var url = common.serviceUrl(config.apiServices.order) + 'PutOrder/'+id;
            return $q.when($http.put(url, model));
        }

        function deleteOrder(id) {
            console.log(id);
            var url = common.serviceUrl(config.apiServices.order) + 'DeleteOrder/' + id;
            return $q.when($http.delete(url));}
    }
})();