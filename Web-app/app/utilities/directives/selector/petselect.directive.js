/*
 * @CreateTime: Dec 8, 2017 1:25 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 16, 2018 12:14 AM
 * @Description: Modify Here, Please
 * <petselect
        ng-model="Ctrl.smf.subcategories"
        placeholder="Please select a category"
        value-of-option="Id"
        label-of-value="Name"
        child-key="Children"
        default-value="{{''}}"
        name="Ctrl.sideModalForm.subcategories"
        ng-disabled="Ctrl.smf.categories == ''"
        options-data="Ctrl.subCategories"></petselect>
 */
(function () {
    'use strict';

    angular
        .module ('app')
        .directive ('petselect', petselect)
        .directive ('petSelectorMultipleLevel', petSelectorMultipleLevel);

    /** @ngInject */
    petselect.$inject = ['$rootScope', '$timeout', '$filter', '$compile'];
    function petselect($rootScope, $timeout, $filter, $compile) {

        function petselectController() {
            /*jshint validthis: true*/
            var vm = this;

            init();

            function init() {

            }
        }

        function postLinkFn(scope, element, attrs, ctrl) {

            //Initialize layout
            scope.$watch(function() {
                return angular.element(element.children('div.petselect-container')).attr('class');
            }, function(newVal) {
                if (newVal.indexOf('focus-add') !== -1 && typeof ctrl.optionHeight === 'string') {
                    element.children('div.petselect-container')
                        .children('.opions-container')
                        .css('max-height', ctrl.optionHeight);
                } else if (newVal.indexOf('focus-remove') !== -1) {
                    element.children('div.petselect-container')
                        .children('.opions-container')
                        .css('max-height', '');
                }
            });

            ctrl.name = {
                $pristine: true
            };
            var foundMe = {};
            // Check options' data
            ctrl.required = attrs.required;
            function initData() {
                if (typeof(ctrl.optionsData) === 'object') {
                    if (ctrl.optionsData.constructor.toString().indexOf('Array') === -1) {
                        ctrl.options = [];
                    } else {
                        ctrl.options = ctrl.optionsData;
                    }
                } else {
                    ctrl.options = [];
                }
            }
            scope.$watch(function() {
                return ctrl.optionsData;
            }, function(newVal) {
                initData();
            });
            // Select a option.
            ctrl.selected = function(item) {
                ctrl.showOption = false;
                element.children().removeClass('focus');
                ctrl.name.$invalid = false;
                if (item === null) {
                    ctrl.label = ctrl.placeholder;
                    if (ctrl.defaultValue) {
                        ctrl.ngModel = ctrl.defaultValue;
                    } else {
                        ctrl.ngModel = null;
                    }
                } else {
                    ctrl.label = item[ctrl.labelOfValue];
                    if (ctrl.valueOfOption) {
                        ctrl.ngModel = item[ctrl.valueOfOption];
                    } else {
                        ctrl.ngModel = item;
                    }
                }
                ctrl.callback({item: ctrl.ngModel});
            };
            // Show options to shoose one of them.
            ctrl.letShowOption = function() {
                if (attrs.disabled) {
                    return false;
                }
                ctrl.name.$pristine = false;
                ctrl.name.$focus = true;
                ctrl.showOption = true;
            };
            // validate or not, base on create a focus event
            ctrl.loseFocus = function() {
                // Not required, default is true; if not check if user is focusing on the element or not to validate it.
                if (!ctrl.required) {
                    ctrl.name.$valid = true;
                } else if (ctrl.name.$focus) {
                    if (ctrl.ngModel) {
                        ctrl.name.$valid = true;
                    } else if (!ctrl.ngModel)  {
                        ctrl.name.$invalid = true;
                    }
                }
                ctrl.name.$focus = false;
                ctrl.showOption = false;
            };

            scope.$watch(function() {
                return ctrl.ngModel;
            }, function(newVal) {
                ctrl.name.$pristine = true;
                selectedValue();
            });

            function selectedValue() {
                foundMe = {};
                if (!ctrl.ngModel) {
                    ctrl.label = ctrl.placeholder;
                } else {
                    if (typeof ctrl.ngModel === 'object') {
                        ctrl.label = ctrl.ngModel[ctrl.labelOfValue];
                    } else {
                        var selected = selectedTheOption(ctrl.ngModel, ctrl.optionsData);
                        if (Object.keys(selected).length !== 0) {
                            ctrl.label = selected[ctrl.labelOfValue];
                        } else {
                            ctrl.label = ctrl.placeholder;
                        }
                    }
                }
            }

            function selectedTheOption(val, inOptions) {
                inOptions.map(function(f, i, arr) {
                    if (f.Id.toString() === val.toString()) {
                        foundMe = f;
                        return f;
                    } else if (f.Children !== null && f.Children.length > 0) {
                        selectedTheOption(val, f.Children);
                    }
                });
                return foundMe;
            }
        }

        function prelinkFn(scope, element) {
            var agent = navigator.userAgent;
            if (isMobile()) {
                var el, me, id;
                id = 'mobile-dropdown-' + $filter('randomString')();
                element.attr('id', id);
                element.addClass('mobile-dropdown');
                me = document.getElementById(id);
                var mobiledropdown = angular.element(me);
                if (!mobiledropdown || me.parentNode.tagName !== 'BODY') {
                    $timeout(function() {
                        el = $compile(me)(scope);
                        angular.element(document.body).append(el);
                    }, 0);
                }
            }
            function isMobile() {
                // return true;
                return (agent.indexOf('iPhone') !== -1) || (agent.indexOf('iPod') !== -1) || agent.indexOf('Android') !== -1;
            }
        }

        return {
            bindToController: true,
            controller: petselectController,
            controllerAs: 'Ctrl',
            link: {
                pre: prelinkFn,
                post: postLinkFn
            },
            restrict: 'AE',
            scope: {
                placeholder: '@',
                ngModel: '=',
                name: '=',
                optionsData: '<',
                valueOfOption: '@',
                labelOfValue: '@',
                childKey: '@',
                defaultValue: '@',
                optionHeight: '@',
                callback: '&'
            },
            templateUrl: './utilities/directives/selector/petselect.html'
        };
    }

    function petSelectorMultipleLevel() {
        return {
            restrict: 'E',
            scope: true,
            replace: true,
            template: '<article ng-include="templateUrl"></article>',
            link: function(scope, element, attrs) {
                scope.$watch(function() {
                    scope.ngModelItem = scope.$eval(attrs.ngModel);
                    scope.getView = scope.$eval(attrs.template);
                    return scope.ngModelItem;
                }, function(newValue, oldValue) {
                    scope.templateUrl = scope.getView;
                });
            }
        };
    }

} ());
