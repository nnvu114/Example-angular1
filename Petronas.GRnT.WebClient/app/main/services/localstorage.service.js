/*
 * @CreateTime: Dec 4, 2017 10:05 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Dec 4, 2017 10:05 PM
 * @Description: Modify Here, Please 
 */

(function() {
    'use strict';

    angular
        .module('app')
        .factory('LocalStorage', localstorageFn);

    /* @ngInject */
    function localstorageFn($window) {
        var service = {
            set: set,
            get: get,
            setObject: setObject,
            getObject: getObject,
            clear: clear,
            remove: remove
        };

        return service;

        function set(key, value) {
            $window.localStorage[key] = value;
        }

        function get(key, defaultValue) {
            return $window.localStorage[key] || defaultValue;
        }

        function setObject(key, value) {
            $window.localStorage[key] = JSON.stringify(value);
        }

        function getObject(key) {
            return JSON.parse($window.localStorage[key] || '{}');
        }

        function clear() {
            $window.localStorage.clear();
        }

        function remove(key) {
            $window.localStorage.removeItem(key);
        }
    }
})();