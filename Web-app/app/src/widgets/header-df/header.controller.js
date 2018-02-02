/*
 * @CreateTime: Dec 1, 2017 11:49 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Dec 2, 2017 3:40 PM
 * @Description: Modify Here, Please 
 */
(function(){
    'use strict';

    angular
        .module('app')
        .controller('HeaderDfCtrl', HeaderDfCtrl);

    /** @ngInject */
    HeaderDfCtrl.$inject = ['$scope'];
    function HeaderDfCtrl($scope) {
        /*jshint validthis: true*/
        var vm = this;

        init();

        function init() {
        }

    }

}());