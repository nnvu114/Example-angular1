/*
 * @CreateTime: Dec 1, 2017 11:49 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 10, 2018 8:17 PM
 * @Description: Modify Here, Please 
 */
(function() {
    'use strict';

    angular
        .module('app')
        .controller('FooterCtrl', FooterCtrl);

    /** @ngInject */
    FooterCtrl.$inject = ['$scope'];
    function FooterCtrl($scope) {
        /*jshint validthis: true*/
        var vm = this;

        var d = new Date();

        $scope.right = 'Copyright 2017 - ' + d.getFullYear() + ' PETRONAS ICT SDN BHD.';

    }

}());