/*
 * @CreateTime: Jan 10, 2018 8:49 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 10, 2018 11:48 PM
 * @Description: Modify Here, Please 
 */
(function () {
    'use strict';

    angular
        .module ('app')
        .directive ('petDropdownBox', petDropdownBox);

    /** @ngInject */
    function petDropdownBox() {

        function link(scope, element, attrs) {
            scope.showme = false;

            scope.deleteMe = function(item) {
                scope.delete({item: item}).then(
                    function(response) {
                        for (var i = 0; i < scope.data.Items.length; i++) {
                            if (scope.data.Items[i].Id === item.Id) {
                                scope.data.Items.splice(i, 1);
                                break;
                            }
                        }
                    }
                );
            };
        }

        return {
            link: link,
            restrict: 'AE',
            scope: {
                data: '<',
                delete: '&'
            },
            templateUrl: './utilities/directives/dropdownbox/dropdownbox.html'
        };
    }

} ());