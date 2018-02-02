/*
 * @CreateTime: Jan 10, 2018 4:16 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 10, 2018 4:17 PM
 * @Description: Modify Here, Please 
 */
(function(){
    'use strict';

    angular
        .module('app')
        .controller('LeftAsideCtrl', LeftAsideCtrl);

    /** @ngInject */
    LeftAsideCtrl.$inject = ['$scope', 'PetDataService','PET_ROUTE', 'API', 'AppValue'];
    function LeftAsideCtrl($scope, PetDataService, PET_ROUTE, API, AppValue) {
        /*jshint validthis: true*/
        var vm = this;
        vm.loading = false;

        init();

        function init() {
            vm.loading = true;
        }

        PetDataService.get(API.LeftAside[AppValue.RunMode]).then(
            function(response) {
                vm.listMenuItems = response.data.data;
            }, function (error) {
                vm.listMenuItems = [];
            }
        ).finally(function() {
            vm.loading = false;
        });

    }

}());