/*
 * @CreateTime: Dec 4, 2017 10:06 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Dec 4, 2017 10:06 PM
 * @Description: Modify Here, Please 
 */
(function() {
    'use strict';

    angular.module('underscore', [])
        .factory('_', underScoreFn);

    underScoreFn.$inject = ['$window'];
    function underScoreFn($window) {
        return $window._;
    }

}());