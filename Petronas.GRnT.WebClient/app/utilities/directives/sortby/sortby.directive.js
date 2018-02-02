/*
 * @CreateTime: Jan 5, 2018 1:05 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 15, 2018 11:50 PM
 * @Description: Modify Here, Please 
 */
(function () {
    'use strict';

    angular
        .module ('app')
        .directive ('htSortby', htSortby);

    /** @ngInject */
    htSortby.$inject = ['$window', '$compile', '$filter', '$timeout'];
    function htSortby($window, $compile, $filter, $timeout) {

        htSortbyController.$inject = ['$scope'];
        function htSortbyController($scope) {
            /*jshint validthis: true*/
            var vm = this;

            init();

            function init() {

            }

            vm.openedSelector = false;
        }

        function link(scope, element, attrs, ctrl) {
            ctrl.selected = function(item) {
                ctrl.selectWith({
                    item: item
                });
            };
        }

        return {
            bindToController: true,
            controller: htSortbyController,
            controllerAs: 'Ctrl',
            link: link,
            restrict: 'AE',
            replace: true,
            scope: {
                data: '<',
                selectWith: '&'
            },
            templateUrl: './utilities/directives/sortby/sortby.html'
        };
    }

} ());