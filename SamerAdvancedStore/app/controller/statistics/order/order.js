(function () {
    'use strict';
    var controllerId = 'staticorder';
    angular.module('app').controller(controllerId, ['common', '$route', '$location', '$routeParams', 'datacontext', staticorder]);

    function staticorder(common, $route, $location, $routeParams, datacontext) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'المبيعات';

        var id = $routeParams.id;
        vm.from = new Date(2018, 2, 19);
        vm.to = new Date(2018, 2, 20);
        vm.getSelectedRow = getSelectedRow;
        vm.currentRow ;
        activate();

        function activate() {
            common.activateController([getOrders()], controllerId)
                .then(function () { });
        }
        function getOrders() {
            datacontext.statisticsdataservice.getOrders().then(function (response) {
                common.logger.logSuccess(response.data, true);
                console.log(response.data);
                vm.rowCollection = response.data.orderStatisticViewModels;



             
 

            }, function (error) {
                logError(error);
                return false;
            });
        
        }


        function getSelectedRow(row) {
            console.log(row);
            vm.currentRow = row;
        }
    }
})();