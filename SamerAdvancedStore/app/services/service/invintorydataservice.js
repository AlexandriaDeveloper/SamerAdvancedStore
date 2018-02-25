(function () {
    'use strict';

    var factoryId = 'invintorydataservice';

    angular
        .module('app')
        .service(factoryId, invintorydataservice);

    invintorydataservice.$inject = ['$http'
        , 'common',
        'config'];


    function invintorydataservice($http, common, config) {
        var $q = common.$q;
        var service = {
            getInvintories: getInvintories,
            getInvintory: getInvintory,
            newInvintory: newInvintory,
            editInvintory: editInvintory,
            deleteInvintory: deleteInvintory
        
        }

        return service;
        function getInvintories() {
            var url = common.serviceUrl(config.apiServices.invintorey) + 'GetInvintories/';
            return $q.when( $http.get(url));
        }

        function getInvintory(id) {
            var url = common.serviceUrl(config.apiServices.invintorey) + 'GetInvintorey/'+id;
            return $q.when($http.get(url));
        }
        function newInvintory(model) {
            var url = common.serviceUrl(config.apiServices.invintorey) + 'PostInvintorey/';
            return $q.when($http.post(url,model));
        }
        function editInvintory(id,model) {
            var url = common.serviceUrl(config.apiServices.invintorey) + 'PutInvintorey/'+id;
            return $q.when($http.put(url, model));
        }

        function deleteInvintory(id) {
            var url = common.serviceUrl(config.apiServices.invintorey) + 'DeleteInvintorey/' + id;
            return $q.when($http.delete(url));}
    }
})();