(function () {
    'use strict';

    var factoryId = 'categorydataservice';

    angular
        .module('app')
        .service(factoryId, categorydataservice);

    categorydataservice.$inject = ['$http'
        , 'common',
        'config'];


    function categorydataservice($http, common, config) {
        var $q = common.$q;
        var service = {
            getCategories: getCategories,
            getCategory: getCategory,
            newCategory: newCategory,
            editCategory: editCategory,
            deleteCategory: deleteCategory
        
        }

        return service;
        function getCategories() {
            var url = common.serviceUrl(config.apiServices.categories) + 'getCategories/';
            return $q.when( $http.get(url));
        }

        function getCategory(id) {
            var url = common.serviceUrl(config.apiServices.categories) + 'getCategory/'+id;
            return $q.when($http.get(url));
        }
        function newCategory(model) {
            var url = common.serviceUrl(config.apiServices.categories) + 'PostCategory/';
            return $q.when($http.post(url,model));
        }
        function editCategory(id,model) {
            var url = common.serviceUrl(config.apiServices.categories) + 'PutCategory/'+id;
            return $q.when($http.put(url, model));
        }

        function deleteCategory(id) {
            var url = common.serviceUrl(config.apiServices.categories) + 'DeleteCategory/' + id;
            return $q.when($http.delete(url));}
    }
})();