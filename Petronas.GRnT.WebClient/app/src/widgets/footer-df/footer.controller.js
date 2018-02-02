/*
 * @CreateTime: Dec 1, 2017 11:49 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Dec 5, 2017 10:41 AM
 * @Description: Modify Here, Please 
 */
(function() {
    'use strict';

    angular
        .module('app')
        .controller('FooterDfCtrl', FooterDfCtrl);

    /** @ngInject */
    FooterDfCtrl.$inject = ['$scope', '$state'];
    function FooterDfCtrl($scope, $state) {
        /*jshint validthis: true*/
        var vm = this;

        vm.info = 'AA';

        init();

        function init() {
        }

        var d = new Date();
        vm.right = 'All rights reversed by Petronas and FPT software Malaysia @2007 - ' + d.getFullYear();

    }

}());