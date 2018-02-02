/*
 * @CreateTime: Dec 2, 2017 6:36 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 18, 2018 3:29 PM
 * @Description: Modify Here, Please 
 */
(function() {
    'use strict';

    angular
        .module('app')
        .controller('LoginCtrl', LoginCtrl);

    /** @ngInject */
    LoginCtrl.$inject = ['$scope', 'PetDataService', 'LocalStorage',
        '$state', 'LoginModelReq', 'TokenModelResponse', 'AuthenticateService',
        'PET_ROUTE', 'NotificationService', 'AppValue', 'API', 'RefreshTokenReg',
        'LogoutService', 'PetPageWaitingService'
    ];

    function LoginCtrl($scope, PetDataService, LocalStorage,
        $state, LoginModelReq, TokenModelResponse, AuthenticateService,
        PET_ROUTE, NotificationService, AppValue, API, RefreshTokenReg,
        LogoutService, PetPageWaitingService
    ) {

        var vm = this;
        vm.loginData = {};
        vm.signingIn = false;
        vm.signedInFalse = false;
        vm.opuDataLoading = false;
        vm.opuList = [];

        init();

        function init() {
            var isLoggedIn = AuthenticateService.isLoggedin();
            if (isLoggedIn) {
                $state.go(PET_ROUTE.HomePage);
            }
            vm.opuDataLoading = true;
            PetDataService.get(API.Organization[AppValue.RunMode]).then(
                function(response) {
                    vm.opuList = response.data.data;
                },
                function(err) {
                    NotificationService.showNotify({
                        type: 'error',
                        message: 'False! Cannot load your organization, please try again.',
                        timeout: AppValue.NotifyTimer,
                        'can-close': true
                    });
                }
            ).finally(function() {
                vm.opuDataLoading = false;
            });
        }

        vm.signin = function() {
            vm.signingIn = true;
            PetPageWaitingService.openPetsideModal(true);
            if (vm.loginData.Username.length > 0 && vm.loginData.Password.length > 0) {
                PetDataService.login(
                    API.Login[AppValue.RunMode],
                    vm.loginData,
                    LoginModelReq,
                    TokenModelResponse
                )
                .then(
                    function(response) {
                        console.log(response);
                        response.ExpiresIn = 'HELLO WORLD';
                        
                        LocalStorage.setObject('user', response);
                        if (response.ExpiresIn) {
                            AuthenticateService.triggerRenewToken(response.ExpiresIn);
                        }
                        $state.go(PET_ROUTE.HomePage);
                    },
                    function(error) {
                        vm.signedInFalse = false;
                        NotificationService.showNotify({
                            type: 'error',
                            message: 'False! Please check your information then try again.',
                            timeout: AppValue.NotifyTimer,
                            'can-close': true
                        });
                    }
                )
                .finally(function() {
                    vm.signingIn = false;
                    PetPageWaitingService.openPetsideModal(false);
                });
            }
        };

        $scope.$watch(function() {
            return vm.loginData;
        }, function(newVal) {
            vm.signedInFalse = false;
        }, true);
    }

}());