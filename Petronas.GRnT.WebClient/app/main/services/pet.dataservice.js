/*
 * @CreateTime: Nov 20, 2017 9:47 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 18, 2018 5:38 PM
 * @Description: Modify Here, Please 
 */

(function() {
    'use strict';

    angular
        .module('app')
        .factory('PetDataService', PetDataService);

    /** @ngInject */
    PetDataService.$inject = [
        '$http', '$q', 'AppConfig', 'AppValue',
        'UserService', '$rootScope', '$injector',
        'NotificationService', 'CheckingToken'
    ];
    function PetDataService(
        $http, $q, AppConfig, AppValue,
        UserService, $rootScope, $injector,
        NotificationService, CheckingToken
    ) {
        var deferred = [];
        var hasValidToken = false;
        var getFn = function(path, params, transformRes) {
            return checkValidAuth().then(
                function() {
                    var actualPath = getApiServer();
                    actualPath += path;
                    if (typeof params === 'undefined') {
                        params = {};
                    }
                    var newDefer = {
                        id: actualPath,
                        action: path,
                        defer: $q.defer()
                    };
                    deferred.push(newDefer);
                    newDefer.defer.notify('Getting data...');
                    return $http({
                        method: 'GET',
                        params: params,
                        url: actualPath,
                        transformResponse: transformRes,
                        headers:  getApiHeader()
                    }).then(successFn, errorFn);
                }
            );
        };

        var putFn = function(path, data, transformReq, transformRes) {
            return checkValidAuth().then(
                function() {
                    var actualPath = getApiServer();
                    actualPath += path;
                    deferred.push({
                        id: actualPath,
                        action: path,
                        defer: $q.defer()
                    });

                    if (typeof data === 'undefined') {
                        return false;
                    }

                    return $http({
                        method: 'PUT',
                        data: JSON.parse(JSON.stringify(data)),
                        url: actualPath,
                        transformRequest: transformReq,
                        transformResponse: transformRes,
                        headers:  getApiHeader()
                    }).then(successFn, errorFn);
                }
            );
        };

        var postFn = function(path, data, transformReq, transformRes) {
            return checkValidAuth().then(
                function() {
                    var actualPath = getApiServer();
                    actualPath += path;
                    deferred.push({
                        id: actualPath,
                        action: path,
                        defer: $q.defer()
                    });

                    if (typeof data === 'undefined') {
                        data = {};
                    }

                    return $http({
                        method: 'POST',
                        url: actualPath,
                        data: data,
                        transformRequest: transformReq,
                        transformResponse: transformRes,
                        headers:  getApiHeader()
                    }).then(successFn, errorFn);
                }
            );
        };

        var deleteFn = function(path, data) {
            return checkValidAuth().then(
                function() {
                    var actualPath = getApiServer();
                    actualPath += path;
                    deferred.push({
                        id: actualPath,
                        action: path,
                        defer: $q.defer()
                    });

                    if (typeof data === 'undefined') {
                        data = {};
                    }

                    return $http({
                        method: 'DELETE',
                        url: actualPath,
                        data: data,
                        headers:  getApiHeader()
                    }).then(successFn, errorFn);
                }
            );
        };

        function loginFn(path, data, transformReq, transformRes) {
            var actualPath = AppConfig.Dev.host + ':' + AppConfig.Dev.port;
            actualPath += path;
            deferred.push({
                id: actualPath,
                defer: $q.defer()
            });
            return $http({
                method: 'GET',
                url: actualPath,
                data: data,
                transformRequest: transformReq,
                transformResponse: transformRes,
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
            }).then(successFn, errorFn);
        }

        $rootScope.$on('AUTH:TOKEN_STILL_VALID', function(e, token) {
            e.preventDefault();
            hasValidToken = true;
        });

        function checkValidAuth() {
            var tokenDefer = $q.defer();
            tokenDefer.notify('Checking token');
            if ($rootScope.requiresAuth === true) {
                var validToken = CheckingToken.IsValid();
                if (hasValidToken === true || validToken === true) {
                    tokenDefer.resolve(true);
                } else {
                    $rootScope.$watch(function() {
                        return hasValidToken;
                    }, function(newVal) {
                        if (newVal === true) {
                            tokenDefer.resolve(true);
                        }
                    });
                }
            } else {
                tokenDefer.resolve(true);
            }
            return tokenDefer.promise;
        }

        return {
            get: getFn,
            post: postFn,
            put: putFn,
            delete: deleteFn,
            login: loginFn
        };

        /**** PRIVATE METHODS. ****/
        function getApiHeader() {
            var accessToken = '';
            var user = UserService.getUserInfo();
            if (!Object.isObjectEmpty(user) && user[AppConfig.AuthToken.TokenKey]) {
                if (user[AppConfig.AuthToken.TypeKey] !== undefined) {
                    accessToken = user[AppConfig.AuthToken.TypeKey] + ' ';
                }
                accessToken += user[AppConfig.AuthToken.TokenKey];
            }
            return {
                'Authorization': accessToken,
                'Content-Type': 'application/json'
            };
        }

        function getApiServer() {
            var actualPath = AppConfig[AppValue.RunMode].host;
            if (AppConfig[AppValue.RunMode].port) {
                actualPath += ':' + AppConfig[AppValue.RunMode].port;
            }
            return actualPath;
        }

        function errorFn(response, httpStatusCode) {
            var currentDef = null;
            if (response.statusText) {
                for (var i = 0; i < deferred.length; i++) {
                    if (response.config.url === deferred[i].id) {
                        currentDef = deferred.splice(i, 1);
                        break;
                    }
                }
                showNotifyMessage(response);
                if (currentDef !== null) {
                    currentDef[0].defer.reject(response);
                    return currentDef[0].defer.promise;
                }
            } else {
                //Unknow error
                for (var j = 0; j < deferred.length; j++) {
                    if (response.config.url === deferred[j].id) {
                        currentDef = deferred.splice(j, 1);
                        break;
                    }
                }
                showNotifyMessage(response);
                if (currentDef !== null) {
                    currentDef[0].defer.reject('An unknown error occurred, and its status code is ' + response.status + '.');
                    return currentDef[0].defer.promise;
                }
            }
        }

        function convertStringToJsonString(str) {
            if (str.charAt(0) === '"') {
                str = str.substring(1);
            }
            if (str.charAt(str.length - 1) === '"') {
                str = str.substring(0, str.length - 1);
            }
            return str;
        }

        function successFn(response, httpStatusCode) {

            var currentDef = null;
            for (var i = 0; i < deferred.length; i++) {
                if (response.config.url === deferred[i].id) {
                    currentDef = deferred.splice(i, 1);
                    break;
                }
            }
            if (currentDef !== null) {
                var res;
                if (typeof response.data === 'object') {
                    res = response.data;
                } else if (typeof response.data === 'string') {
                    response.data = convertStringToJsonString(response.data);
                    res = JSON.parse(response.data);
                } else if (response.data === undefined && typeof response === 'string') {
                    response = convertStringToJsonString(response);
                    res = JSON.parse(response);
                } else {
                    res = response;
                }
                currentDef[0].defer.resolve(res);
                return currentDef[0].defer.promise;
            }
        }

        function showNotifyMessage(response) {
            switch (response.status) {
                case 401:
                    NotificationService.showNotify(
                        {
                            type: 'error',
                            message: 'False! You are not authorized, please contact your administrator.',
                            timeout: AppValue.NotifyTimer,
                            'can-close': true
                        }
                    );
                    break;
                case -1:
                    NotificationService.showNotify(
                        {
                            type: 'error',
                            message: 'False! Can not connect to server, please contact your administrator.',
                            timeout: AppValue.NotifyTimer,
                            'can-close': true
                        }
                    );
                    break;
            }
        }
    }
}());