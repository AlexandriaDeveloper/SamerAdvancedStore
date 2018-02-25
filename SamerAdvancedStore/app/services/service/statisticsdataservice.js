(function () {
    'use strict';

    var factoryId = 'statisticsdataservice';

    angular
        .module('app')
        .service(factoryId, statisticsdataservice);

    statisticsdataservice.$inject = ['$http'
        , 'common',
        'config'];


    function statisticsdataservice($http, common, config) {
        var $q = common.$q;
        var service = {
            getStocks: getStocks,
            getProfits: getProfits,
            getOrders: getOrders
        }

        return service;
        function getStocks() {
            var url = common.serviceUrl(config.apiServices.statistics) + 'Invintory/' ;
            return $q.when($http.get(url));
        }

        function getProfits(id) {
            var url = common.serviceUrl(config.apiServices.statistics) + 'GetProfits/'+ id;
            return $q.when( $http.get(url));
        }

        function getOrders() {
            var url = common.serviceUrl(config.apiServices.statistics) + 'GetOrders/' ;
            return $q.when($http.get(url));
        }


    }
})();