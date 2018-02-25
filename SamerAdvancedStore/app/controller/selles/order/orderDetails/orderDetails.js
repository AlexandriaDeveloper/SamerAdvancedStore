(function () {
    'use strict';
    var controllerId = 'orderDetails';
    angular.module('app')
        .controller(controllerId, ['common', '$route', '$location', '$routeParams', 'datacontext', orderDetails]);

    function orderDetails(common, $route, $location, $routeParams, datacontext) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var logError = getLogFn(controllerId, 'logError');
        var vm = this;

        vm.busyMessage = 'من فضلك أنتظر ...';


        vm.header = { title: 'امر بيع', logo: 'fa fa-cart-plus' }
        vm.message = "لا يوجد نتائج";


        vm.itemsByPage = 1;
        vm.product = {}


        vm.products = [];
        vm.rowCollection = [];


        vm.getProductByCode = getProductByCode;

        vm.createNew = createNew;
        vm.getProductData = getProductData;

        vm.getInvintoryProduct = getInvintoryProduct;
        vm.saveOrderDetails = saveOrderDetails;
        vm.deleteOrderDetails = deleteOrderDetails;
        vm.close = close;
        vm.orderDetails = {
            quantity: 0,
            pricePricePerUnit: 0,
            discount: 0,
            sold: 0
        };
        var id = $routeParams.id;
        activate();
        function activate() {
            common.activateController([getOrderDetailies()], controllerId)
                .then(function () { log('ادارة الأصناف '); });
        }
        function toggleSpinner(on) { vm.isBusy = on; }
        function toggleNew(on) { vm.isNew = on; vm.product = {} }
        function getOrderDetailies() {
            toggleSpinner(true);
            return datacontext.orderdetailsdataservice.getOrderDetailies(id).then(function (response) {

                //vm.message = response.data.message;
                    console.log(response);
                vm.data = response.data;
                vm.rowCollection = response.data.order.orderDetails;
                vm.order = response.data.order;

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

                vm.orderDetails.price = response.data.price;
                vm.orderDetails.soldPrice = response.data.price;
            

                vm.product = response.data;
                vm.product.productId = vm.product.productId;
                toggleSpinner(false);
                return response;
            },
                function (error) {

                    logError(error.data.message);
                    return false;
                });
        }
        function getProductData(orderDetails) {
            console.log(orderDetails);
            vm.orderDetails = orderDetails;
            initilizOrderDetails(orderDetails);
            vm.code = orderDetails.productCode;
            getProductByCode(vm.code);
            vm.product.quantity = orderDetails.quantity;
            vm.orderDetails.price = orderDetails.pricePerUnit;
            vm.orderDetails.discount = orderDetails.discount;
            vm.orderDetails.quantity = orderDetails.productOut;
        
        }
        function createNew() {
            toggleNew(true);
            vm.invintory = {};
            vm.orderDetails = {};
            vm.code = "";

        }
        function getInvintoryProduct(id) {
            toggleNew(false);
            return datacontext.invintoryproductdataservice.getInvintoryProduct(id).then(function (response) {

                vm.product = response.data;
                vm.code = response.data.productCode;
                //   toggleSpinner(false);hi, 

                return response;
            },
                function (error) {
                    logError(error);
                    return false;
                });

        }
        function saveOrderDetails(orderDetails) {
            orderDetails = initilizOrderDetails(orderDetails);
            orderDetails.state = "OUT";
            orderDetails.productOut = orderDetails.quantity;
            orderDetails.sold = orderDetails.net;
            orderDetails.productId = vm.product.productId;
            orderDetails.pricePerUnit = orderDetails.price;

            if (vm.isNew) {
                //go to insert
                console.log("save new record");
                console.log(orderDetails);
                datacontext.orderdetailsdataservice.newOrderDetails(orderDetails).then(function (response) {

                    common.logger.logSuccess(response.data, true);
                    toggleNew(true);
                    $('#myModal').modal('hide');
                    $route.reload();
                    //  $location.url("/employee/searchemp/");
                }, function (error) {

                    logError(error.data.message);
                    return false;
                });
            } else {
                //go to update
                console.log("edit record");
                datacontext.orderdetailsdataservice.editOrderDetails(orderDetails.id, orderDetails).then(function (response) {
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
        function deleteOrderDetails(id) {

            datacontext.orderdetailsdataservice.deleteOrderDetails(id).then(function (response) {
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
            vm.order.open = false;

            datacontext.orderdataservice.editOrder(vm.order.id, vm.order).then(function (response) {
                common.logger.logSuccess(response.data, true);
                toggleNew(true);
                $('#deleteModal').modal('hide');
                $route.reload();
            }, function (error) {
                logError(error);
                return false;
            });
        }
        function initilizOrderDetails(orderDetails) {

            if (orderDetails.discount === "undefined") {
                orderDetails.discount = 0;
            }
            orderDetails.net = (orderDetails.price * orderDetails.quantity) - orderDetails.discount;
            orderDetails.orderId = id;
            orderDetails.productId = vm.product.id;

            return orderDetails;
        }
    }
})();