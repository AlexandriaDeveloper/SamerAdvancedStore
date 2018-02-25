(function () {
    'use strict';

    var factoryId = 'orderdetailsdataservice';

    angular
        .module('app')
        .service(factoryId, orderdetailsdataservice);

    orderdetailsdataservice.$inject = ['$http'
        , 'common',
        'config'];


    function orderdetailsdataservice($http, common, config) {
        var $q = common.$q;
        var service = {
            getOrderDetailies: getOrderDetailies,
            getOrderDetails: getOrderDetails,
            newOrderDetails: newOrderDetails,
            editOrderDetails: editOrderDetails,
            deleteOrderDetails: deleteOrderDetails
        
        }

        return service;
        function getOrderDetailies(id) {
            var url = common.serviceUrl(config.apiServices.orderDetails) + 'GetOrderDetalsies/'+id;
            return $q.when( $http.get(url));
        }

        function getOrderDetails(id) {
            var url = common.serviceUrl(config.apiServices.orderDetails) + 'GetOrderDetails/'+id;
            return $q.when($http.get(url));
        }
        function newOrderDetails(model) {
            var url = common.serviceUrl(config.apiServices.orderDetails) + 'PostOrderDetails/';
            return $q.when($http.post(url,model));
        }
        function editOrderDetails(id,model) {
            var url = common.serviceUrl(config.apiServices.orderDetails) + 'PutOrderDetails/'+id;
            return $q.when($http.put(url, model));
        }

        function deleteOrderDetails(id) {
            var url = common.serviceUrl(config.apiServices.orderDetails) + 'DeleteOrderDetails/' + id;
            return $q.when($http.delete(url));}
    }
})();