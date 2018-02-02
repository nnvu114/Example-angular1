/*
 * @CreateTime: Dec 1, 2017 11:46 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Dec 1, 2017 11:47 PM
 * @Description: Modify Here, Please 
 */
(function() {
    'use strict';
    angular
        .module('app')
        .filter('trustHtml', /* @ngInject */ function($sce) {
            return function(src) {
                return $sce.trustAsHtml(src);
            };
        })
        .filter('unique', function() {
            return function(collection, keyname) {
                var output = [],
                    keys = [];

                angular.forEach(collection, function(item) {
                    var key = item[keyname];
                    if (keys.indexOf(key) === -1) {
                        keys.push(key);
                        output.push(item);
                    }
                });

                return output;
            };
        })
        .filter('ArrayObject2Array', function() {
            return function(arr, key) {
                if (angular.isArray(arr) && arr.length > 0) {
                    arr.map(function(item, i) {
                        if (item[key]) {
                            return item[key];
                        }
                    });
                }
            };
        })
        .filter('randomString', function() {
            return function(str) {
                var res = str ? str : '';
                return res + Math.random().toString(31).substring(3);
            };
        }).filter('randomIntNumberBetween', function() {
            return function(min, max) {
                min = min ? min : 1;
                max = max ? max : 10;
                return Math.floor(Math.random() * (max - min + 1)) + min;
            };
        });
})();
