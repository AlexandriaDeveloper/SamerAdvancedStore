(function () {
    'use strict';
    var controllerId = 'category';
    angular.module('app')
        .controller(controllerId, ['common', '$route', 'datacontext', category]);

    function category(common, $route, datacontext) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var logError = getLogFn(controllerId, 'logError');
        var vm = this;

        vm.busyMessage = 'أنتظر من فضلك ...';


        vm.header = { title: 'الأصناف', logo: 'fa fa-eye' }
        vm.message = "لا يوجد نتائج";


        vm.itemsByPage = 1;

        vm.categories = [];
        vm.rowCollection = [];

        vm.createNew = createNew;
        vm.getCategory = getCategory;
        vm.saveCategory = saveCategory;
        vm.deleteCategory = deleteCategory;
        vm.category = {};

        activate();

        function activate() {
            common.activateController([getCategories()], controllerId)
                .then(function () { log('ادارة الأصناف '); });
        }

        function toggleSpinner(on) { vm.isBusy = on; }

        function toggleNew(on) { vm.isNew = on; }
        function getCategories() {
            toggleSpinner(true);
            return datacontext.categorydataservice.getCategories().then(function(response) {
                    vm.message = response.data.message;
                    vm.rowCollection = response.data.categories;
                    console.log(response);
                    toggleSpinner(false);
                    return response;
                },
                function (error) {
                    
                    common.logger.logSuccess(response.data, true);
                    return false;
                });
        }

        function createNew() {
            toggleNew(true);
            vm.category = {};
        }


        function getCategory(id) {
            toggleNew(false);
            return datacontext.categorydataservice.getCategory(id).then(function (response) {
                vm.category = response.data;
                   
                 //   toggleSpinner(false);
                console.log(response.data);
                    return response;
                },
                function (error) {
                    logError(error.data.message);
                    return false;
                });

        }

        function saveCategory(category) {
            console.log(category);
            if (vm.isNew) {
                //go to insert
                console.log("save new record");

                datacontext.categorydataservice.newCategory(category).then(function (response) {
                    common.logger.logSuccess(response.data, true);
                    $('#myModal').modal('hide');
                    $route.reload();
                }, function (error) {
                    logError(error.data.message);
                    return false;});
            } else {
                //go to update
                console.log("edit record");
                datacontext.categorydataservice.editCategory(category.id, category).then(function (response) {
                    common.logger.logSuccess(response.data, true);
                    $('#myModal').modal('hide');
                    $route.reload();
                }, function (error) {
                    console.log(error.data.message);
                    logError(error.data.message);
                    return false; });
            }
            toggleNew(true);
        }
        function  deleteCategory(id){
        
            datacontext.categorydataservice.deleteCategory(id).then(function (response) {
                toggleNew(true);
                common.logger.logSuccess(response.data, true);
                $('#deleteModal').modal('hide');
                $route.reload();
            }, function (error) {
                logError(error.data.message);
                return false;});
        }
    }
})();