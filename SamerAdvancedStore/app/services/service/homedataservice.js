(function () {
    'use strict';

    var factoryId = 'homedataservice';

    angular
        .module('app')
        .service(factoryId, homedataservice);

    homedataservice.$inject = ['$http'
        , 'common',
        'config'];


    function homedataservice($http, common, config) {
        var $q = common.$q;
        var service = {
            getHello: getHello
        
        }

        return service;
        function getHello() {
            var url = common.serviceUrl(config.apiServices.home) + 'getHome/';
            return $q.when( $http.get(url));
        }




    }
})();