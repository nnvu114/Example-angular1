/*
 * @CreateTime: Dec 4, 2017 9:59 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Dec 4, 2017 9:59 PM
 * @Description: Modify Here, Please 
 */
(function() {
    'use strict';

    angular
        .module('app')
        .controller('LandingCtrl', LandingCtrl);

    /** @ngInject */
    LandingCtrl.$inject = ['$scope'];
    function LandingCtrl($scope) {
        var vm = this;

        init();

        function init() {}

    }

}());