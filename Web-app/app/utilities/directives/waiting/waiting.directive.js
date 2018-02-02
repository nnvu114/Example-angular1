/*
 * @CreateTime: Jan 9, 2018 12:45 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 9, 2018 2:24 PM
 * @Description: <pet-page-waiting image-url="images/icons/pet-waiting.svg"></pet-page-waiting>
 */
(function () {
    'use strict';

    angular
        .module ('app')
        .directive ('petPageWaiting', petPageWaiting);

    /** @ngInject */
    petPageWaiting.$inject = ['$compile', 'PetPageWaitingService'];
    function petPageWaiting($compile, PetPageWaitingService) {

        function postLink(scope, element) {
            scope.$watch(function() {
                return PetPageWaitingService.waitingSetting.show;
            }, function(newVal) {
                scope.showMe = newVal;
            });
        }

        function preLink(scope, element) {
            var el, me;
            me = document.getElementById('pet-page-waiting-4558768349d');
            var sidemodal = angular.element(me);
            if (!sidemodal || me.parentNode.tagName !== 'BODY') {
                el = $compile(element)(scope);
                angular.element(document.body).append(el);
            }
        }

        return {
            restrict: 'AE',
            scope: {
                imageUrl: '@'
            },
            link: {
                pre: preLink,
                post: postLink
            },
            replace: true,
            template: '<div id="pet-page-waiting-4558768349d" ng-class="{\'show-me\': showMe}" class="pet-page-waiting">' +
                            '<img ng-if="imageUrl" ng-src="{{imageUrl}}">' +
                            '<div ng-if="!imageUrl" class="lds-css">' +
                                '<div style="width:100%;height:100%" class="lds-facebook">' +
                                    '<div></div>' +
                                    '<div></div>' +
                                    '<div></div>' +
                                '</div>' +
                            '</div>' +
                        '</div>'
        };
    }

} ());
