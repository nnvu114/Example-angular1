/*
 * @CreateTime: Jan 15, 2018 9:25 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 15, 2018 11:19 PM
 * @Description: Modify Here, Please 
 */
(function() {
    'use strict';

    angular
        .module('app')
        .controller('SamplesCtrl', SamplesCtrl);

    /** @ngInject */
    SamplesCtrl.$inject = ['$scope', 'PetDataService'];
    function SamplesCtrl($scope, PetDataService) {
        var vm = this;

        init();

        function getData(filename) {
            return PetDataService.get('/data/sampledata/' + filename).then(
                function(res) {
                    return res.data.data;
                },
                function(err) {
                    return [];
                }
            );
        }

        function init() {

            getData('sortby.json').then(
                function(res) {
                    vm.sortby = res;
                }
            );
            getData('multiple-selector.json').then(
                function(res) {
                    vm.selector = res;
                }
            );

        }

        vm.smf = {};

        $scope.$watch(function() {
            return vm.smf.selector;
        }, function(newVal) {
            console.log('New Value:', newVal);
        });

        vm.selected = function(item) {
            console.log(item);
        };

    }

}());