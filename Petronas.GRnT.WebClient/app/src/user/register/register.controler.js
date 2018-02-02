/*
 * @CreateTime: Dec 2, 2017 6:36 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 18, 2018 3:29 PM
 * @Description: Modify Here, Please 
 */
(function () {
    'use strict';

    angular
        .module('app')
        .controller('RegisterCtrl', RegisterCtrl);

    /** @ngInject */
    RegisterCtrl.$inject = ['$scope', 'PetDataService', 'LocalStorage',
        '$state', 'RegisterModelReq', 'TokenModelResponse',
        'PET_ROUTE', 'NotificationService', 'AppValue', 'API', 'PetPageWaitingService', 'vcRecaptchaService'
    ];

    function RegisterCtrl($scope, PetDataService, LocalStorage,
        $state, RegisterModelReq, TokenModelResponse,
        PET_ROUTE, NotificationService, AppValue, API, PetPageWaitingService, vcRecaptchaService) {

        var vm = this;
        $scope.businessTypes = [{
            FruitId: 1,
            Name: 'Oil and Gas Services',
            Selected: false
        }, {
            FruitId: 2,
            Name: 'Research Institution',
            Selected: false
        }, {
            FruitId: 3,
            Name: 'Manufacturer',
            Selected: false
        }, {
            FruitId: 4,
            Name: 'Technology Company',
            Selected: true
        }];

        vm.response = null;
        vm.widgetId = null;

        vm.setResponse = function (response) {
            vm.response = response;
            vm.model.captchaResponse = vm.response;
        };
        vm.setWidgetId = function (widgetId) {
            vm.widgetId = widgetId;
        };

        vm.cbExpiration = function () {
            vcRecaptchaService.reload(vm.widgetId);
            vm.response = null;
        };

        init();

        function init() {
            vm.model = {
                userRegisterInfo: {
                    userName: "",
                    password: "",
                    confirmPassword: "",
                    email: ""
                },
                companyName: "",
                contactPersonName: "",
                address: "",
                website: "",
                contactNumber: "",
                companyNumber: "",
                businessType: [
                    ""
                ],
                isPetronasLicensedCompany: "",
                registrationSWECCode: "",
                key: '6LfKaEIUAAAAACQsoGZXRqn0mGKMqDSpQzaZIa59',
                captchaResponse: ""
            };
        }

        vm.registerAccount = function (model) {
            if (model.confirmPassword != model.password) {
                return;
            }
            vm.model = model;
            vm.businessType = [];

            for (var i = 0; i < $scope.businessTypes.length; i++) {
                if ($scope.businessTypes[i].Selected) {
                    const businessName = $scope.businessTypes[i].Name;
                    vm.businessType.push(businessName);
                }
            }
            if (model.businessTypesOther) {
                vm.businessType.push(model.businessTypesOtherDes);
            }

            vm.model.businessType = vm.businessType;

            PetDataService.post(
                API.Register[AppValue.RunMode],
                vm.model,
                RegisterModelReq,
                TokenModelResponse
            ).then( function (response) {

                },
                function (error) {
                    vm.signedInFalse = false;
                    NotificationService.showNotify({
                        type: 'success',
                        message: 'You registed is Successfully',
                        timeout: AppValue.NotifyTimer,
                        'can-close': true
                    });
                }
                )
                .finally(function () {
                    vcRecaptchaService.reload(vm.widgetId);
                    init();
                });
        }

    }
}());