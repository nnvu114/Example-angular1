/*
 * @CreateTime: Jan 2, 2018 2:45 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 2, 2018 2:49 PM
 * @Description: Modify Here, Please 
 */
(function(){
    'use strict';

    angular
        .module('app')
        .factory('RegisterModelReq', RegisterModelReq)
        .factory('RefreshTokenReg', RefreshTokenFn);

    /** @ngInject */
    RegisterModelReq.$inject = ['AppConfig', 'md5'];
    function RegisterModelReq(AppConfig, md5) {
        return function(data, opt1, opt2) {
            return JSON.stringify(data);
        };
    }

    //http://localhost:8081/token?grant_type=refresh_token&refresh_token=e829133c88e44fcaa29d348a5906970e&client_id=8C6BB6D2-94FA-E711-9BD9-9CD21E367EC4
    RefreshTokenFn.$inject = ['AppConfig', 'LocalStorage'];
    function RefreshTokenFn(AppConfig, LocalStorage) {
        return function(data, opt1, opt2) {
            var reqData = '';
            reqData =   'refresh_token=' + LocalStorage.getObject('user').RefreshToken +
                        '&grant_type=refresh_token' +
                        '&client_id=' + AppConfig.AuthToken.ClientId;
            return reqData;
        };
    }

}());