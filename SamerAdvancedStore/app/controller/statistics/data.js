(function () {
    'use strict';
    var controllerId = 'statistic';
    angular.module('app').controller(controllerId, ['common', statistic]);

    function statistic(common) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'المبيعات';
        activate();

        function activate() {
            common.activateController([getData()], controllerId)
                .then(function () { });
        }
        function getData() {
            var data2 = [
                {
                    name: 'البضاعة المتاحة',
                    desc: 'بيانات عن مخزون البضاعة و الكميات المتاحة  ',
                    logo: 'fa fa-shopping-cart',
                    link: '#/statistics/invintory',
                    dataclass: 'bblue'
                },
                {
                    name: 'المبيعات',
                    desc: 'تقرير عمليات البيع والربح  ',
                    logo: 'fa fa fa-handshake-o',
                    link: '#/statistics/orders',
                    dataclass: 'borange'
                }

            ];
            console.log(data2);

            vm.data = data2;
        }
    }
})();