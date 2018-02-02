/*
 * @CreateTime: Feb 1, 2018 12:00 PM
 * @Author: Thuan Nguyen
 * @Contact: thuannd15@fsoft.com.vn
 * @Last Modified By:
 * @Last Modified Time:
 * @Description:
 */
(function () {
    'use strict';

    angular
        .module('app')
        .controller('RegistrationCtrl', RegistrationCtrl);

    /** @ngInject */
    RegistrationCtrl.$inject = ['$scope'];

    function RegistrationCtrl($scope) {
        var vm = this;

        init();

        function init() {
            $('.carousel').carousel({
                interval: 3000
            });
        }

    }

}());