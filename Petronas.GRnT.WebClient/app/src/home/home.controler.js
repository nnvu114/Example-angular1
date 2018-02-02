/*
 * @CreateTime: Dec 4, 2017 9:59 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 15, 2018 9:47 PM
 * @Description: Modify Here, Please 
 */
(function() {
    'use strict';

    angular
        .module('app')
        .controller('HomeCtrl', HomeCtrl);

    /** @ngInject */
    HomeCtrl.$inject = ['$scope' ,'NotificationService', '$timeout'];
    function HomeCtrl($scope, NotificationService, $timeout) {
        var vm = this;

        init();

        function init() {}

        $scope.switch = true;

        $scope.options = {
            width: '35px',
            barHeight: '14px',
            barRadius: '7px',
            defaultColor: '#e6e6e6',
            activatedColor: '#00A19C',
            diameter: '20px'
        };

        vm.notificationSmS = [
            {
                type: 'success',
                message: 'Successful! The item was changed.',
                timeout: 'infinity',
                undo: {
                    id: 6789,
                },
            },
            {
                type: 'warning',
                message: 'Warning! Something may be wrong.',
                timeout: '5000',
                'can-close': true
            },
            {
                type: 'error',
                message: 'False! The item did not be changed.',
                timeout: '3000',
                'can-close': true
            }
        ];

        vm.openNotification = function(item) {
            NotificationService.showNotify(item);
        };

        function undoItem(item2Undo) {
            //Reset this value to save memory
            item2Undo = null;
            return true;
        }

        //TO undo an item.
        $scope.$watch(function() {
            return NotificationService.undoStack;
        }, function(newVal, oldVal) {
            if (oldVal.length < newVal.length) {
                var item2Undo = angular.copy(newVal[newVal.length - 1].data);
                NotificationService.clearUndo(newVal[newVal.length - 1].id);
                undoItem(item2Undo);
            }
        }, true);

    }

}());