(function () {
    'use strict';
    var controllerId = 'product';
    angular.module('app')
        .controller(controllerId, ['common', '$route', 'datacontext', product]);

    function product(common, $route, datacontext) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var logError = getLogFn(controllerId, 'logError');
        var vm = this;

        vm.busyMessage = 'من فضلك أنتظر ...';


        vm.header = { title: 'منتج', logo: 'fa fa-cart-plus' }
        vm.message = "لا يوجد نتائج";


        vm.itemsByPage = 1;

        vm.products = [];
        vm.rowCollection = [];

        vm.createNew = createNew;
        vm.getProduct = getProduct;
        vm.saveProduct = saveProduct;
        vm.deleteProduct = deleteProduct;
    
        vm.product = {};

        activate();

        function activate() {
            common.activateController([getProducts(), getCategories()], controllerId)
                .then(function () { log('ادارة الأصناف ');; });
        }

        function toggleSpinner(on) { vm.isBusy = on; }

        function toggleNew(on) { vm.isNew = on;vm.product= {} }
        function getProducts() {
            toggleSpinner(true);
            return datacontext.productdataservice.getProducts().then(function (response) {
            
                    vm.message = response.data.message;
                    vm.rowCollection = response.data.products;
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
            vm.Product = {};
        }


        function getProduct(id) {
            toggleNew(false);
            return datacontext.productdataservice.getProduct(id).then(function (response) {
                vm.product = response.data;
                    console.log(response.data);
                 //   toggleSpinner(false);
                console.log(response.data);
                    return response;
                },
                function (error) {
                    logError(error.data.message);
                    return false;
                });

        }

        function saveProduct(product) {
            console.log(product);
            if (vm.isNew) {
                //go to insert
                console.log("save new record");

                datacontext.productdataservice.newProduct(product).then(function (response) {
                    console.log(response);
                    common.logger.logSuccess(response.data, true);
                    toggleNew(true);
                    $('#myModal').modal('hide');
                    $route.reload();
                }, function (error) {
                    console.log(error.data);
                    logError(error.data.message);
                    return false;});
            } else {
                //go to update
                console.log("edit record");
                datacontext.productdataservice.editProduct(product.productId, product).then(function (response) {
                    common.logger.logSuccess(response.data, true);
                    toggleNew(true);
                    $('#myModal').modal('hide');
                    $route.reload();
                }, function (error) {
                    logError(error.data.message);
                    return false; });
            }
       
        }
        function  deleteProduct(id){
        
            datacontext.productdataservice.deleteProduct(id).then(function (response) {
                common.logger.logSuccess(response.data, true);
                toggleNew(true);
                $('#deleteModal').modal('hide');
                $route.reload();
            }, function (error) {
                logError(error.data.message);
                return false;});
        }

    
    }
})();