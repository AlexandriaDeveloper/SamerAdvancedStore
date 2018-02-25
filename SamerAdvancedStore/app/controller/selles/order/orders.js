(function () {
    'use strict';
    var controllerId = 'orders';
    angular.module('app')
        .controller(controllerId, ['common', '$route', '$location', 'datacontext', orders]);

    function orders(common, $route, $location, datacontext) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var logError = getLogFn(controllerId, 'logError');
        var vm = this;

        vm.busyMessage = 'من فضلك أنتظر ...';


        vm.header = { title: 'مبيعات', logo: 'fa fa-money' }
        vm.message = "لا يوجد نتائج";


        vm.itemsByPage = 1;

        vm.products = [];
        vm.rowCollection = [];
        vm.nextPage = nextPage;
        vm.createNew = createNew;
        vm.getOrder = getOrder;
        vm.saveOrder = saveOrder;
        vm.deleteOrder = deleteOrder;
        vm.order = {};

        activate();

        function activate() {
            common.activateController([getOrders()], controllerId)
                .then(function () { log('ادارة الأصناف ');});
        }

        function toggleSpinner(on) { vm.isBusy = on; }

        function toggleNew(on) { vm.isNew = on;vm.product= {} }
        function getOrders() {
            toggleSpinner(true);
            return datacontext.orderdataservice.getOrders().then(function (response) {
            
                    vm.message = response.data.message;
                    vm.rowCollection = response.data.orders;
                        
                    console.log(response);
                    toggleSpinner(false);
                    return response;
                },
                function(error) {
                    logError(error);
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
                    logError(error);
                    return false;
                });
        }

        function createNew() {
            toggleNew(true);
            vm.order = {};
        }


        function getOrder(id) {
            toggleNew(false);
            return datacontext.orderdataservice.getOrder(id).then(function (response) {
                vm.order = response.data;
                   
                 //   toggleSpinner(false);
                console.log(response.data);
                    return response;
                },
                function (error) {
                    logError(error.data.message);
                    return false;
                });

        }

        function saveOrder(order) {
            console.log(order);
            if (vm.isNew) {
                //go to insert
                console.log("save new record");

                datacontext.orderdataservice.newOrder(order).then(function (response) {
                    console.log(response);
                    common.logger.logSuccess(response.data, true);
                    toggleNew(true);
                    $('#myModal').modal('hide');
                    $location.url("/selles/orders/orderdetails/"+response.data);
                }, function (error) {
                    console.log(error.data);
                    logError(error.data.message);
                    return false;});
            } else {
                //go to update
                console.log("edit record");
                datacontext.orderdataservice.editOrder(order.id, order).then(function (response) {
                    common.logger.logSuccess(response.data, true);
                    toggleNew(true);
                    $('#myModal').modal('hide');
                    $route.reload();
                }, function (error) {
                    logError(error.data.message);
                    return false; });
            }
       
        }
        function deleteOrder(order) {
            console.log(order);
            datacontext.orderdataservice.deleteOrder(order.id).then(function (response) {
                common.logger.logSuccess(response.data, true);
                toggleNew(true);
                $('#deleteModal').modal('hide');
                $route.reload();
            }, function (error) {
                logError(error.data.message);
                return false;});
        }

        function nextPage(id) {
            $location.url("/selles/orders/orderdetails/" + id);

        }
    }
})();