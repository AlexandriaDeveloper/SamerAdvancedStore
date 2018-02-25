(function () {
    'use strict';
    var controllerId = 'admin';
    angular.module('app').controller(controllerId, ['common', admin]);

    function admin(common) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'إدخال بيانات';

        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated Admin View2'); });
        }
    }
})();