/*
 * @CreateTime: Jan 11, 2018 10:09 AM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 18, 2018 3:53 PM
 * @Description: Modify Here, Please 
 */
(function () {
    'use strict';

    angular
        .module ('app')
        .directive ('userAction', userAction);

    /** @ngInject */
    userAction.$inject = ['LogoutService', 'UserService'];
    function userAction(LogoutService, UserService) {

        function userActionController() {
            /*jshint validthis: true*/
            var vm = this;

            init();

            function init() {

            }

            vm.UserInfo = UserService.getUserInfo();

            vm.logout = function() {
                LogoutService.logout();
            };
        }

        function link(scope, element, attrs) {
            scope.showme = false;
        }

        return {
            bindToController: true,
            controller: userActionController,
            controllerAs: 'Ctrl',
            link: link,
            restrict: 'AE',
            scope: {},
            templateUrl: './utilities/components/useraction/useraction.html'
        };
    }

} ());