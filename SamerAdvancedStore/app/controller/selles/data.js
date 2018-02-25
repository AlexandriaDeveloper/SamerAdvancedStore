(function () {
    'use strict';
    var controllerId = 'selles';
    angular.module('app').controller(controllerId, ['common', selles]);

    function selles(common) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'المبيعات';
        vm.data = [];
        activate();

        function activate() {
            common.activateController([getData()], controllerId)
                .then(function () { });
        }


        function getData() {
            var data2 = [
                {
                    name: 'البضاعة المتاحة',
                    desc: 'إضافة و تعديل البضائع بالمخزن ',
                    logo: 'fa fa-shopping-cart',
                    link: '#/selles/orders',
                    dataclass: 'bblue'
                }
             
            ];
            console.log(data2);

            vm.data = data2;
        }
    }
})();