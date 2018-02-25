(function () {
    'use strict';
    var controllerId = 'staticprofits';
    angular.module('app').controller(controllerId, ['common', '$route', '$location', '$routeParams', 'datacontext', staticprofits]);

    function staticprofits(common, $route, $location, $routeParams, datacontext) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'المبيعات';
        vm.busyMessage = 'من فضلك أنتظر ...';
  
        var id = $routeParams.id;
       
        activate();

        function activate() {
            common.activateController([getData(id)], controllerId)
                .then(function () { });
        }
        function toggleSpinner(on) { vm.isBusy = on; }
        function getData(id) {
            toggleSpinner(true);
            datacontext.statisticsdataservice.getProfits(id).then(function (response) {
      
                common.logger.logSuccess(response.data, true);
                console.log(response.data);
                vm.data = response.data;

                toggleSpinner(false);
 
              
            }, function (error) {
                logError(error);
                toggleSpinner(false);
                return false;
            });
        
        }
    }
})();