/*
 * @CreateTime: Dec 1, 2017 11:49 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 11, 2018 12:08 AM
 * @Description: Modify Here, Please 
 */
(function(){
    'use strict';

    angular
        .module('app')
        .controller('HeaderCtrl', HeaderCtrl);

    /** @ngInject */
    HeaderCtrl.$inject = [
        '$scope', 'NotificationService', 'PetDataService', 'AppValue',
        'API', 'PET_ROUTE', '$timeout', '$q'];
    function HeaderCtrl($scope, NotificationService, PetDataService, AppValue,
        API, PET_ROUTE, $timeout, $q) {
        /*jshint validthis: true*/
        var vm = this;

        init();

        function init() {
            vm.loading = true;
            PetDataService.get(API.RightTopMenu[AppValue.RunMode]).then(
                function(res) {
                    vm.headerAction = res.data.data;
                },
                function(err) {
                    vm.headerAction = [];
                }
            ).finally(function() {
                vm.loading = false;
            });
        }

        vm.logoRoute = PET_ROUTE.HomePage;

        var deferred = [];
        vm.deleteFn = function(item) {
            deferred.push({
                id: item.Id,
                defer: $q.defer()
            });
            deferred[deferred.length - 1].defer.notify('Getting data');
            var currentDef;
            return $timeout(function() {
                for (var i = 0; i < deferred.length; i++) {
                    if (deferred[i].id === item.Id) {
                        currentDef = deferred.splice(i, 1);
                        if (currentDef) {
                            currentDef[0].defer.resolve('Deleted');
                            break;
                        }
                    }
                }
                NotificationService.showNotify({
                    type: 'success',
                    message: 'Successful! The item was deleted.',
                    timeout: '2000'
                });
                return currentDef[0].defer.promise;
            }, 1000);
        };

    }

}());