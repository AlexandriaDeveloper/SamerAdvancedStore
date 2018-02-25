(function () {
    'use strict';
    var controllerId = 'invintory';
    angular.module('app')
        .controller(controllerId, ['common', '$route','$location', 'datacontext', invintory]);

    function invintory(common, $route, $location, datacontext) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var logError = getLogFn(controllerId, 'logError');
        var vm = this;

        vm.busyMessage = 'من فضلك أنتظر ...';


        vm.header = { title: 'أمر شراء', logo: 'fa fa-cart-plus' }
        vm.message = "لا يوجد نتائج";


        vm.itemsByPage = 1;

        vm.products = [];
        vm.rowCollection = [];
        vm.nextPage = nextPage;
        vm.createNew = createNew;
        vm.getInvintory = getInvintory;
        vm.saveInvintory = saveInvintory;
        vm.deleteInvintory = deleteInvintory;
        vm.invintory = {};

        activate();

        function activate() {
            common.activateController([getInvintories()], controllerId)
                .then(function () { log('ادارة الأصناف ');});
        }

        function toggleSpinner(on) { vm.isBusy = on; }

        function toggleNew(on) { vm.isNew = on;vm.product= {} }
        function getInvintories() {
            toggleSpinner(true);
            return datacontext.invintorydataservice.getInvintories().then(function (response) {
            
                    vm.message = response.data.message;
                    vm.rowCollection = response.data.invintory;
                        
                    console.log(response);
                    toggleSpinner(false);
                    return response;
                },
                function(error) {
                    logError(error.data.message);
                    return false;
                });
        }

        function getCategories() {
            toggleSpinner(true);
            return datacontext.categorydataservice.getCategories().then(function (response) {
                    
                    vm.categories = response.data.categories;
                    console.log(response);
                    toggleSpinner(false);
                    return response;
                },
                function (error) {
                    logError(error.data.message);
                    return false;
                });
        }

        function createNew() {
            toggleNew(true);
            vm.invintory = {};
        }


        function getInvintory(id) {
            toggleNew(false);
            return datacontext.invintorydataservice.getInvintory(id).then(function (response) {
                vm.invintory = response.data;
                   
                 //   toggleSpinner(false);
                console.log(response.data);
                    return response;
                },
                function (error) {
                    logError(error.data.message);
                    return false;
                });

        }

        function saveInvintory(invintory) {
            console.log(invintory);
            if (vm.isNew) {
                //go to insert
                console.log("save new record");

                datacontext.invintorydataservice.newInvintory(invintory).then(function (response) {
                    console.log(response);
                    common.logger.logSuccess(response.data, true);
                    toggleNew(true);
                    $('#myModal').modal('hide');
                    $location.url("/data/invintory/products/"+response.data);
                }, function (error) {
                    console.log(error.data);
                    logError(error.data.message);
                    return false;});
            } else {
                //go to update
                console.log("edit record");
                datacontext.invintorydataservice.editInvintory(invintory.id, invintory).then(function (response) {
                    common.logger.logSuccess(response.data, true);
                    toggleNew(true);
                    $('#myModal').modal('hide');
                    $route.reload();
                }, function (error) {
                    logError(error.data.message);
                    return false; });
            }
       
        }
        function deleteInvintory(id){
        
            datacontext.invintorydataservice.deleteInvintory(id).then(function (response) {
                common.logger.logSuccess(response.data, true);
                toggleNew(true);
                $('#deleteModal').modal('hide');
                $route.reload();
            }, function (error) {
                logError(error.data.message);
                return false;});
        }

        function nextPage(id) {
            $location.url("/data/invintory/products/" + id);

        }
    }
})();