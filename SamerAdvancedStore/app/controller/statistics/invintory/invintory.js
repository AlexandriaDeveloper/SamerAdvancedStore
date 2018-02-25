(function () {
    'use strict';
    var controllerId = 'staticInvintroy';
    angular.module('app').controller(controllerId, ['common','datacontext', staticInvintroy]);

    function staticInvintroy(common, datacontext) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var vm = this;
        vm.title = 'المبيعات';
        vm.quantityPredicate = 'productQuantity';
        vm.namePredicate = 'productName';
        vm.codePredicate = 'productCode';
        vm.busyMessage = 'أنتظر من فضلك';  
        activate();

        function activate() {
            common.activateController([getData()], controllerId)
                .then(function () { });
        }
        function toggleSpinner(on) { vm.isBusy = on; }
        function getData() {
            toggleSpinner(true);
            datacontext.statisticsdataservice.getStocks().then(function (response) {               
                common.logger.logSuccess(response.data, true);
                console.log(response.data);
                vm.rowCollection = response.data.stocks;
                toggleSpinner(false);
            }, function (error) {
                logError(error);
                toggleSpinner(false);
                return false;
            });
        
        }
    }
})();