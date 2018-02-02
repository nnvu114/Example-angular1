/*
 * @CreateTime: Jan 5, 2018 10:51 AM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 10, 2018 4:59 PM
 * @Description: Params is an object, similar:
 * {
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
 */
(function () {
    'use strict';

    angular
        .module ('app')
        .directive ('htNotification', htNotification);

    /** @ngInject */
    htNotification.$inject = ['$rootScope', 'NotificationService', '$compile', '$timeout'];
    function htNotification($rootScope, NotificationService, $compile, $timeout) {

        function link(scope, element, attrs) {
            var str = '';
            scope.$watch(function() {
                return NotificationService.data;
            }, function(newVal, oldVal) {
                if (newVal.length > oldVal.length) {
                    scope.showNewNotification(newVal[newVal.length - 1]);
                }
            }, true);

            scope.showNewNotification = function(item) {
                if (scope.$eval(attrs.multiple) === true) {
                    compileTemplate(item);
                } else {
                    NotificationService.cleanAllNotify();
                    NotificationService.cleanAllUndo();
                    element.children('ul.notification-list').children('li').remove();
                    compileTemplate(item);
                }
            };

            scope.closeFor = function(id) { // Call from callback
                NotificationService.cancelled(id);
            };

            scope.undoFor = function(id) {
                removeItemById(id);
                NotificationService.undoMe(id);
            };

            scope.removeMe = function(id) {
                removeItemById(id);
                NotificationService.cancelled(id);
            };

            function removeItemById(id) {
                var me = angular.element(document.getElementById(id));
                var animateClass = me.attr('animate-class');
                me.removeClass(animateClass);
                $timeout(function() {
                    //Check to make sure that destroyAfter does still not destroy the item
                    if (angular.element(document.getElementById(id))) {
                        angular.element(document.getElementById(id)).remove();
                    }
                },100);
            }

            function compileTemplate(item) {
                str =
                    '<li id="' + item.id + '" ' +
                        'class="' + item.data.type + '" ' +
                        'callback="closeFor(\'' + item.id + '\');" ' +
                        'animate-class="show-notify" ' +
                        'destroy-after="' + item.data.timeout + '">' +
                        '<div class="pet-container item">';
                switch (item.data.type) {
                    case 'warning':
                        str += '<i class="notification-icon glyphicon glyphicon-alert"></i>';
                        break;
                    case 'error':
                        str += '<i class="notification-icon glyphicon glyphicon-ban-circle"></i>';
                        break;
                    default:
                        str += '<i class="notification-icon icon icon-pet-i-success"></i>';
                        break;
                }
                str +=  '<div class="text-area">' +
                            '<label>' + item.data.message + '</label>';
                if (item.data.undo !== undefined) {
                    str += '<a href="javascript:void(0)" ' +
                                'ng-click="undoFor(\'' + item.id + '\')">Undo</a>';
                }
                str +=  '</div>' +
                        '<div class="action">' +
                            '<span class="action-item">' +
                                '<i class="icon icon-pet-i-close" ' +
                                    'ng-click="removeMe(\'' + item.id + '\')"></i>' +
                            '</span>' +
                        '</div>' +
                        '</div>' +
                    '</li>';
                element.children('ul.notification-list').prepend(angular.element($compile(str)(scope)));
            }
        }

        return {
            link: link,
            restrict: 'AE',
            replace: true,
            scope: {
                multiple: '@',
                customClass: '@'
            },
            templateUrl: './utilities/directives/notification/notification.html'
        };
    }

} ());