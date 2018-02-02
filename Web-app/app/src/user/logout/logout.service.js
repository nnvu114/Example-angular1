/*
 * @CreateTime: Jan 4, 2018 8:31 AM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 4, 2018 8:42 AM
 * @Description: Modify Here, Please 
 */
(function() {
    'use strict';

    angular
        .module('app')
        .factory('LogoutService', LogoutService);

    /** @ngInject */
    LogoutService.$inject = ['LocalStorage', '$state', 'PET_ROUTE', 'UserService'];

    function LogoutService(LocalStorage, $state, PET_ROUTE, UserService) {

        return {
            logout: logoutFn
        };

        function logoutFn() {
            UserService.clearUserInfo();
            $state.go(PET_ROUTE.Login);
        }

    }

}());