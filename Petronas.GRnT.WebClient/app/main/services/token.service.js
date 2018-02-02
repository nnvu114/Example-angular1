/*
 * @CreateTime: Jan 18, 2018 3:28 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 18, 2018 7:03 PM
 * @Description: Modify Here, Please 
 */

(function() {
    'use strict';

    angular
        .module('app')
        .factory('RefreshTokenService', RefreshTokenService)
        .factory('CheckingToken', CheckingToken);

    /** @ngInject */
    RefreshTokenService.$inject = [
        'RefreshTokenReg', 'TokenModelResponse', '$state', 'PET_ROUTE',
        'PetDataService', 'API', 'AppValue', 'LocalStorage', 'UserService'
    ];
    function RefreshTokenService(
        RefreshTokenReg, TokenModelResponse, $state, PET_ROUTE,
        PetDataService, API, AppValue, LocalStorage, UserService
    ) {

        var proccessing = {};

        function checkStatusToken() {
            var user = UserService.getUserInfo();
            var expiresIn = new Date(user.Expires).getTime();
            var currentT = new Date().getTime();
            var expireTime = parseInt(user.ExpiresIn);
            var diffTime = expiresIn - currentT;
            if (diffTime < 0 || isNaN(diffTime)) {
                //Expired token
                return 0;
            } else if (diffTime / (expireTime * 1000) <= 0.05 || diffTime < 10000) {
                //Still valid, but refresh token immidiately.
                return 1;
            } else {
                //Expire in, feel free to enjoy a cup of tea.
                return diffTime;
            }
        }

        function refreshToken() {
            var now = new Date().getTime();
            proccessing.status = 'processing';
            proccessing.history = now;
            LocalStorage.setObject('tokenProcessing', proccessing);
            return PetDataService.login(
                API.SignIn[AppValue.RunMode],
                null,
                RefreshTokenReg,
                TokenModelResponse
            )
            .then(
                function(response) {
                    LocalStorage.setObject('user', response);
                    return response;
                },
                function(error) {
                    $state.go(PET_ROUTE.Unauthorize);
                    return error;
                }
            ).finally(function() {
                LocalStorage.remove('tokenProcessing');
            });
        }

        function checkingIsRequestingToken(bufferTime) {
            var user = UserService.getUserInfo();
            var now = new Date().getTime();
            var expiresIn = new Date(user.Expires).getTime();
            var remainingTime = expiresIn - now;
            if (remainingTime / 1000 > bufferTime) {
                return true;
            }
            proccessing = LocalStorage.getObject('tokenProcessing');
            if (Object.keys(proccessing).length === 0) {
                //Don't have any request to get new token;
                return false;
            }
            if (now - proccessing.history >= 3000) {
                //Some one requested for a long time, just reset and you can continue;
                LocalStorage.remove('tokenProcessing');
                return false;
            } else {
                // New token is processing, so you can ignore your plan then wait for a moment.
                return true;
            }
        }

        return {
            refreshToken: refreshToken,
            checkStatusToken: checkStatusToken,
            checkingIsRequestingToken: checkingIsRequestingToken
        };

    }

    CheckingToken.$inject = ['UserService'];
    function CheckingToken(UserService) {
        function isValid() {
            var user = UserService.getUserInfo();
            var expiresIn = new Date(user.Expires).getTime();
            var currentT = new Date().getTime();
            var expireTime = parseInt(user.ExpiresIn);
            var diffTime = expiresIn - currentT;
            if (diffTime < 0 || isNaN(diffTime)) {
                return false;
            } else {
                return true;
            }
        }

        return {
            IsValid: isValid
        };
    }

}());