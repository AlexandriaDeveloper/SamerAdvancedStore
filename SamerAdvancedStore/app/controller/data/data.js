(function () {
    'use strict';
    var controllerId = 'data';
    angular.module('app').controller(controllerId, ['common', data]);

    function data(common) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'إدخال بيانات';
        vm.data = [];
        activate();

        function activate() {
            common.activateController([getData()], controllerId)
                .then(function () { });
        }


        function getData() {
            var data2 = [
                {
                    name: 'أوامر الشراء',
                    desc: 'إضافة و تعديل البضائع بالمخزن ',
                    logo: 'fa fa-shopping-cart',
                    link: '#/data/invintory',
                    dataclass: 'bblue'
                }, {
                    name: 'المنتجات',
                    desc: 'إضافة و تعديل بيانات المنتجات',

                    logo: 'fa fa-gift',
                    link: '#/data/product',
                    dataclass: 'bgreen'
                },

                {
                    name: 'الأصناف',
                    desc: 'اضافة و تعديل التقسيمات المختلفة ',
                    logo: 'fa fa-eye',
                    link: '#/data/category',
                    dataclass: 'borange'
                }
            ];
            console.log(data2);

            vm.data = data2;
        }
    }
})();