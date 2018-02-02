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
        .factory('LoginModelReq', LoginModelReqFn)
        .factory('RefreshTokenReg', RefreshTokenFn);

    /** @ngInject */
    LoginModelReqFn.$inject = ['AppConfig', 'md5'];
    function LoginModelReqFn(AppConfig, md5) {

        return function(data, opt1, opt2) {
            var reqData = '';
            if (typeof data === 'string') {
                data = JSON.parse(data);
            }
            if (data !== undefined && Object.keys(data).length > 0) {
                reqData =   'userName=' + data.Username +
                            '&password=' + data.Password +
                            '&grant_type=password' +
                            '&client_id=' + AppConfig.AuthToken.ClientId;
                // reqData =   'userName=' + data.Username +
                //             '&password=' + md5.createHash(data.Password || '') +
                //             '&grant_type=password' +
                //             '&client_id=' + AppConfig.AuthToken.ClientId;
            }
            return reqData;
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