/*
 * @CreateTime: Dec 2, 2017 6:15 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 15, 2018 10:39 PM
 * @Description: Modify Here, Please 
 */
(function () {
    'use strict';

    angular
        .module ('app')
        .directive ('ngClickout', clickOut)
        .directive ('destroyAfter', destroyAfter)
        .directive ('activeFor', activeFor)
        .directive ('toggleClassFor', ToggleClassFor)
        .directive ('autoFocus', autoFocus)
        .directive ('fixParentWidth', fixParentWidth);

    /** @ngInject */
    function clickOut() {

        function link(scope, element, attrs) {
            element.bind('click', function() {
                var clickedOnMe = false;
                var clickedOutMe = false;
                element.bind('mousedown', function() {
                    clickedOnMe = true;
                });

                element.bind('mouseup', function() {
                    clickedOnMe = false;
                    clickedOutMe = false;
                });

                angular.element(document).bind('mousedown', function() {
                    clickedOutMe = true;
                    if (clickedOnMe === false && clickedOutMe === true) {
                        scope.$apply(function() {
                            scope.ngClickout();
                            angular.element(document).unbind('mousedown');
                            element.unbind('mouseup');
                            element.unbind('mousedown');
                        });
                    }
                });
            });
        }

        return {
            link: link,
            restrict: 'AE',
            scope: {
                ngClickout: '&'
            },
        };
    }

    /** @ngInject */
    destroyAfter.$inject = ['$timeout'];
    function destroyAfter($timeout) {
        return {
            restrict: 'A',
            scope: {
                destroyAfter: '@',
                callback: '&',
                animateClass: '@'
            },
            link: function(scope, element, attrs) {
                if (scope.destroyAfter !== 'infinity') {
                    if (scope.destroyAfter === undefined || parseInt(scope.destroyAfter) === 0) {
                        scope.destroyAfter = 3000;
                    }
                    $timeout(function() {
                        element.removeClass(scope.animateClass);
                    }, parseInt(scope.destroyAfter) - 50);
                    $timeout(function() {
                        element.remove();
                        scope.$apply(function() {
                            scope.callback();
                        });
                    }, parseInt(scope.destroyAfter) + 50);
                }
                $timeout(function() {
                    element.addClass(scope.animateClass);
                }, 50);
            }
        };
    }

    /** @ngInject */
    fixParentWidth.$inject = ['$window'];
    function fixParentWidth($window) {
        return {
            restrict: 'A',
            link: function(scope, element) {
                scope.$watch(function() {
                    return angular.element(element.parent())[0].offsetWidth;
                }, function(newVal) {
                    element.css('width', newVal + 'px');
                });
                angular.element($window).bind('resize', function() {
                    element.css('width', angular.element(element.parent())[0].offsetWidth + 'px');
                    scope.$digest();
                });
            }
        };
    }

    function activeFor() {
        return {
            restrict: 'A',
            scope: {
                activeFor: '@',
                active: '@',
                unActive: '@',
                removeClassAfterCallback: '@',
                callbackFor: '&'
            },
            link: function(scope, element, attrs) {
                element.bind('click', function() {
                    if (scope.active !== undefined && scope.active.trim().length > 0) {
                        if (scope.unActive !== undefined && scope.unActive.length > 0) {
                            angular.element(document.getElementsByClassName(scope.activeFor))
                                .removeClass(scope.unActive);
                            if (scope.removeClassAfterCallback !== undefined && scope.removeClassAfterCallback.length > 0) {
                                angular.element(document.getElementsByClassName(scope.activeFor))
                                    .removeClass(scope.removeClassAfterCallback);
                            }
                        } else {
                            angular.element(document.getElementsByClassName(scope.activeFor))
                                .removeClass(scope.active);
                        }
                        element.addClass(scope.active);
                    } else {
                        angular.element(document.getElementsByClassName(scope.activeFor))
                            .removeClass('active');
                        element.addClass('active');
                    }
                    if (attrs.callbackFor === undefined) {
                        return false;
                    }
                    scope.$apply(function() {
                        scope.callbackFor()
                            .then()
                            .finally(function() {
                                if (scope.removeClassAfterCallback !== undefined && scope.removeClassAfterCallback.length > 0) {
                                    element.removeClass(scope.removeClassAfterCallback);
                                } else {
                                    element.removeClass('waiting');
                                }
                            });
                    });
                });
            }
        };
    }

    autoFocus.$inject = ['$timeout'];
    function autoFocus($timeout) {
        return {
            restrict: 'A',
            scope: {
                autoFocus: '@'
            },
            link: function(scope, element, attrs) {
                scope.$watch(function() {
                    return scope.$eval(attrs.autoFocus);
                }, function(newVal) {
                    if (newVal === true) {
                        $timeout(function() {
                            element.focus();
                        }, 100);
                    }
                });
            }
        };
    }

    /**
     * <a class="sample-class"
            active-for="sample-class"
            active="active waiting"
            un-active="active"
            remove-class-after-callback="waiting"
            callback-for="vm.dosSomethingFor(params)">Click me</a>
     */
    function ToggleClassFor() {
        return {
            restrict: 'A',
            scope: {
                toggleActiveMe: '@'
            },
            link: function(scope, element, attrs) {
                element.bind('click', function() {
                    if (scope.toggleActiveMe !== undefined && scope.toggleActiveMe.trim().length > 0) {
                        element.toggleClass(scope.toggleActiveMe);
                    } else {
                        element.toggleClass('active');
                    }
                });
            }
        };
    }

} ());