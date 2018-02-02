/*
 * @CreateTime: Dec 7, 2017 9:48 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 9, 2018 2:33 PM
 * @Description: Sometime, in developer mode, the form fires twice time. Will fix later.
 */
(function(){
    'use strict';

    angular
        .module('app')
        .factory('PetPageWaitingService', PetPageWaitingService);

    /** @ngInject */
    function PetPageWaitingService() {

        var deferred, deleteStatus;
        var waitingSetting = {
            show: false
        };

        return {
            openPetsideModal: openPetsideModal,
            waitingSetting: waitingSetting
        };

        function openPetsideModal(show) {
            console.log('SHOW: ', show);
            waitingSetting.show = show;
        }

    }

}());
