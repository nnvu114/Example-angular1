/*
 * @CreateTime: Jan 4, 2018 10:25 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 5, 2018 11:45 PM
 * @Description: Modify Here, Please 
 */
(function() {
    'use strict';

    angular
        .module('app')
        .factory('NotificationService', NotificationService);

    /** @ngInject */
    NotificationService.$inject = ['$rootScope', '$filter'];
    function NotificationService($rootScope, $filter) {

        var notifyStack = [];
        var undoStack = [];

        return {
            showNotify: showNotify,
            data: notifyStack,
            undoStack: undoStack,
            undoMe: undoMe,
            cancelled: cancelled,
            clearUndo: clearUndo,
            cleanAllNotify: cleanAllNotify,
            cleanAllUndo: cleanAllUndo
        };

        function setShowNotification(item) {
            notifyStack.push(item);
        }

        function cleanAllNotify() {
            notifyStack.length = 0;
        }

        function cleanAllUndo() {
            undoStack.length = 0;
        }

        function clearNotifyStack(id) {
            for (var i = 0; i < notifyStack.length; i++) {
                if (notifyStack[i].id === id) {
                    notifyStack.splice(i,1);
                    break;
                }
            }
        }

        function moveToUndo(id) {
            for (var i = 0; i < notifyStack.length; i++) {
                if (notifyStack[i].id === id) {
                    var n2m = notifyStack.splice(i,1);
                    undoStack.push(n2m);
                    break;
                }
            }
        }

        function showNotify(item) {
            var id = $filter('randomString')();
            setShowNotification({
                id: id,
                data: item
            });
        }

        function undoMe(item) {
            moveToUndo(item);
        }

        function cancelled(item) {
            clearNotifyStack(item);
        }

        function clearUndo(id) {
            for (var i = 0; i < undoStack.length; i++) {
                if (undoStack[i].id === id) {
                    undoStack.splice(i,1);
                    break;
                }
            }
        }
    }

}());