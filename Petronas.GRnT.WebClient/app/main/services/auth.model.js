/*
 * @CreateTime: Jan 2, 2018 2:45 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 2, 2018 2:49 PM
 * @Description: To help storage user information into local storage with static key
 */
(function() {
    'use strict';

    angular
        .module('app')
        .factory('TokenModelResponse', function() {
            return function(data, opt1, opt2) {
                data = JSON.parse(data);
                console.log('TokenModelResponse');
                console.log(data);
                var reqData = {};
                if (data && Object.keys(data).length > 0) {
                    reqData.Expires     = data['.expires'];
                    reqData.Issued      = data['.issued'];
                    reqData.Token       = data['access_token'];
                    reqData.ClientId    = data['as:client_id'];
                    reqData.ExpiresIn   = data['expires_in'];
                    reqData.FullName    = data['fullname'];
                    reqData.RefreshToken = data['refresh_token'];
                    reqData.UserName    = data['userName'];
                    reqData.Type        = data['token_type'];
                    reqData.Role        = data['roles'];
                }
                return data;
            };
        });

}());