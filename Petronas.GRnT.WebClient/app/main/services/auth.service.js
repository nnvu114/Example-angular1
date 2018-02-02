/*
 * @CreateTime: Dec 4, 2017 10:06 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 18, 2018 7:07 PM
 * @Description: Modify Here, Please 
 */

(function() {
    'use strict';

    angular
        .module('app')
        .factory('AuthenticateService', AuthenticateService);

    /** @ngInject */
    AuthenticateService.$inject = [
        'LocalStorage', '$interval', 'RefreshTokenService', 'CheckingToken',
        'LogoutService', 'UserService', '$q', '$rootScope', '$filter'
    ];
    function AuthenticateService(
        LocalStorage, $interval, RefreshTokenService, CheckingToken,
        LogoutService, UserService, $q, $rootScope, $filter
    ) {

        var APIToken = {
            refreshIn: 0,
            refreshTokenTrigger: undefined,
            isGettingToken: false,
            tokenStillValid: false,
            firstLoad: 0,
            bufferTime: 0
        };

        return {
            isLoggedin: isLoggedin,
            checkIfRefreshToken: checkIfRefreshToken,
            triggerRenewToken: triggerRenewToken,
            getValidToken: getValidToken
        };

        function isLoggedin() {
            if (!Object.isObjectEmpty(LocalStorage.getObject('user'))) {
                return true;
            } else {
                return false;
            }
        }

        function getValidToken() {
            var deferred = $q.defer();
            deferred.notify('Retrying data');
            var tokenIsValid = RefreshTokenService.checkStatusToken();
            $rootScope.$watch(function() {
                return APIToken;
            }, function(newVal) {
                if (newVal.isGettingToken === false && APIToken.tokenStillValid === true) {
                    deferred.resolve(UserService.getUserInfo().Token);
                }
            }, true);
            return deferred.promise;
        }

        function checkIfRefreshToken() {
            //When page reload
            var tokenIsValid = RefreshTokenService.checkStatusToken();
            if (tokenIsValid === 0) {
                APIToken.tokenStillValid = false;
            } else {
                APIToken.tokenStillValid = true;
                if (APIToken.firstLoad === 0) {
                    APIToken.firstLoad = 1;
                    $rootScope.$broadcast('AUTH:TOKEN_STILL_VALID');
                }
            }
            //Need to refresh token immidiately
            if (tokenIsValid === 0 || tokenIsValid === 1) {
                RefreshTokenService.refreshToken().then(
                    function(response) {
                        if (response.status === 400) {
                            stopTrigger();
                            LogoutService.logout();
                        }
                        if (APIToken.firstLoad === 0) {
                            APIToken.firstLoad = 1;
                            $rootScope.$broadcast('AUTH:TOKEN_STILL_VALID');
                        }
                        triggerRenewToken(response.ExpiresIn);
                    }, function(error) {
                        stopTrigger();
                        LogoutService.logout();
                    }
                );
            } else {
                //Set new trigger in
                triggerRenewToken(tokenIsValid / 1000);
            }
        }

        function refreshToken() {
            if (!isLoggedin()) {
                stopTrigger();
                return false;
            }
            if (RefreshTokenService.checkingIsRequestingToken(APIToken.bufferTime) === false) {
                RefreshTokenService.refreshToken().then(
                    function(response) {
                        if (response.status === 400) {
                            stopTrigger();
                            LogoutService.logout();
                        }
                        triggerRenewToken(response.ExpiresIn);
                    }, function(error) {
                        stopTrigger();
                        LogoutService.logout();
                    }
                );
            }
        }

        // For first loggin
        function triggerRenewToken(expiresIn) {
            stopTrigger();
            var randomIntNumber = $filter('randomIntNumberBetween')(1, 10);
            APIToken.refreshIn = parseInt(expiresIn) - parseInt(0.05 * expiresIn);
            if (APIToken.refreshIn - randomIntNumber > 10) {
                APIToken.refreshIn =  APIToken.refreshIn - randomIntNumber;
            }
            APIToken.bufferTime = parseInt(expiresIn) - APIToken.refreshIn;
            if (!angular.isDefined(APIToken.refreshTokenTrigger) && !isNaN(APIToken.refreshIn)) {
                APIToken.refreshTokenTrigger = $interval(function() {
                    refreshToken();
                }, APIToken.refreshIn * 1000);
            }
        }

        function stopTrigger() {
            if (angular.isDefined(APIToken.refreshTokenTrigger)) {
                $interval.cancel(APIToken.refreshTokenTrigger);
                APIToken.refreshTokenTrigger = undefined;
            }
        }

    }

}());