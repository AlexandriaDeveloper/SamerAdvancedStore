(function () {
    'use strict';
    var controllerId = 'invintoryProduct';
    angular.module('app')
        .controller(controllerId, ['common', '$route', '$location', '$routeParams', 'datacontext', invintoryProduct]);

    function invintoryProduct(common, $route, $location, $routeParams, datacontext) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var logError = getLogFn(controllerId, 'logError');
        var vm = this;

        vm.busyMessage = 'من فضلك أنتظر ...';


        vm.header = { title: 'أمر شراء', logo: 'fa fa-cart-plus' }
        vm.message = "لا يوجد نتائج";


        vm.itemsByPage = 1;
        vm.product = {}
        vm.product.coastPerUnit = vm.product.coast / vm.product.quantity;

        vm.products = [];


        vm.rowCollection = [];
    
        vm.getProductByCode = getProductByCode;

        vm.createNew = createNew;
        vm.getInvintoryProduct = getInvintoryProduct;
        vm.saveInvintoryProduct = saveInvintoryProduct;
        vm.deleteInvintoryProduct = deleteInvintoryProduct;
        vm.close = close;
        vm.invintory = {};
        var id = $routeParams.id;
        activate();

        function activate() {
            common.activateController([getInvintoryproducts()], controllerId)
                .then(function () { log('ادارة الأصناف '); });
        }

        function toggleSpinner(on) { vm.isBusy = on; }

        function toggleNew(on) {
            vm.isNew = on;
            vm.product = {};
            vm.code = "";
        }
        function getInvintoryproducts() {
            toggleSpinner(true);
            return datacontext.invintoryproductdataservice.getInvintoryproducts(id).then(function (response) {
                vm.data = response.data;
                vm.rowCollection = response.data.transaction;
                console.log(response);
                toggleSpinner(false);
                return response;
            },
                function (error) {
                    logError(error);
                    return false;
                });
        }
        function getProductByCode(code) {

            //toggleSpinner(true);
            return datacontext.productdataservice.getProductByCode(code).then(function (response) {
                  
                vm.product.productName = response.data.productName;
                vm.product.code = response.data.productCode;
                vm.product.model = response.data.model;
                vm.product.size = response.data.size;
                vm.product.color = response.data.color;
                vm.product.price = response.data.price;
                vm.product.productId = response.data.productId;
                vm.product.productIn = response.data.productIn ? response.data.productIn : 0;
                vm.product.coast = response.data.coast ? response.data.coast : 0;
                console.log(response.data);
                toggleSpinner(false);
                return response;
            },
                function (error) {
                    console.log(error);
                    logError(error.data.message);
                    return false;
                });
        }
        function createNew() {
            toggleNew(true);
            vm.invintory = {};
        }
        function getInvintoryProduct(id) {
            toggleNew(false);
            return datacontext.invintoryproductdataservice.getInvintoryProduct(id).then(function (response) {
                console.log(response.data);
                vm.product = response.data;
                vm.code = response.data.productCode;
                //   toggleSpinner(false);hi, 
                console.log(response.data);
                return response;
            },
                function (error) {
                    logError(error);
                    return false;
                });

        }
        function saveInvintoryProduct(product) {

            product.invintoryId = id;
            product.productId = vm.product.productId;

            product.state = "IN";
            product.pricePerUnit = vm.product.coast / vm.product.productIn;

            if (vm.isNew) {
                //go to insert
                console.log("Save New");
                console.log(product);

                datacontext.invintoryproductdataservice.postInvintoryProduct(product).then(function (response) {

                    common.logger.logSuccess(response.data, true);
                    toggleNew(true);
                    $('#myModal').modal('hide');
                    $route.reload();
                    //  $location.url("/employee/searchemp/");
                }, function (error) {
                    console.log(error.data);
                    logError(error.data.message);
                    return false;
                });
            } else {
                //go to update
                console.log("edit record");
                datacontext.invintoryproductdataservice.putInvintoryProduct(product.id, product).then(function (response) {
                    common.logger.logSuccess(response.data, true);
                    toggleNew(true);
                    $('#myModal').modal('hide');
                    $route.reload();
                }, function (error) {
                    logError(error.data.message);
                    return false;
                });
            }

        }
        function deleteInvintoryProduct(id) {

            datacontext.invintoryproductdataservice.deleteInvintoryProduct(id).then(function (response) {
                common.logger.logSuccess(response.data, true);
                toggleNew(true);
                $('#deleteModal').modal('hide');
                $route.reload();
            }, function (error) {
                logError(error);
                return false;
            });
        }

        function close() {
            vm.data.invintory.open = false;
            vm.data.invintory.id = id;

            console.log(vm.data.invintory);
            datacontext.invintorydataservice.editInvintory(id, vm.data.invintory).then(function (response) {

                common.logger.logSuccess(response.data, true);
                toggleNew(true);
                $('#deleteModal').modal('hide');
                $route.reload();
            }, function (error) {
                logError(error);
                return false;
            });
        }
    }
})();