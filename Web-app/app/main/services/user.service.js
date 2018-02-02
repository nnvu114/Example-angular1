/*
 * @CreateTime: Jan 18, 2018 3:41 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 18, 2018 3:52 PM
 * @Description: Modify Here, Please
 */
(function() {
    'use strict';

    angular
        .module('app')
        .factory('UserService', UserService);

    /** @ngInject */
    UserService.$inject = [
        'LocalStorage', 'AppConfig'
    ];
    function UserService(
        LC, AC, $interval
    ) {

        return {
            getRole: getRole,
            isAdmin: isAdmin,
            getUserInfo: getUserInfo,
            clearUserInfo: clearUserInfo
        };

        function getRole() {
            if (LC.getObject('user') && LC.getObject('user').Role) {
                return LC.getObject('user').Role;
            }
        }

        function isAdmin() {
            if (LC.getObject('user') && LC.getObject('user').Role === AC.role.admin) {
                return true;
            }
        }

        function getUserInfo() {
            return LC.getObject('user');
        }

        function clearUserInfo() {
            LC.remove('user');
        }

    }

}());