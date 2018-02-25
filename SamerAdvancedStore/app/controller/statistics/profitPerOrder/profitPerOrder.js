(function () {
    'use strict';
    var controllerId = 'profitPerOrder';
    angular.module('app').controller(controllerId, ['common', '$route', '$location', '$routeParams', 'datacontext', profitPerOrder]);

    function profitPerOrder(common, $route, $location, $routeParams, datacontext) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'المبيعات';

        var id = $routeParams.id;

       
        activate();

        function activate() {
            common.activateController([getData(id)], controllerId)
                .then(function () { });
        }
        function getData(id) {
            datacontext.statisticsdataservice.getProfits(id).then(function (response) {
                common.logger.logSuccess(response.data, true);
                console.log(response.data);
                vm.data = response.data;


                angular.forEach(response.data.products, function(data) {
                    console.log(data);
                 //   vm.myData.push(data.quantity);
              
                });
 

            }, function (error) {
                logError(error);
                return false;
            });
        
        }
    }
})();