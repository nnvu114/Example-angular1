/*
 * @CreateTime: Jan 2, 2018 2:45 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 2, 2018 2:49 PM
 * @Description: Modify Here, Please 
 */
(function() {
    'use strict';

    angular
        .module('app')
        .factory('userModelRes', function() {
            return function(data, opt1, opt2) {
                data = JSON.parse(data);
                if (data.length && data !== undefined) {
                    data = _.map(data, function(repo) {
                        return {
                            username: repo.FirstName + ' ' + repo.LastName,
                            phoneno: repo.Phone
                        };
                    });
                }
                return data;
            };
        })
        .factory('userModelReq', function() {
            return function(data, opt1, opt2) {
                data = JSON.parse(data);
                var reqData = {};
                if (data !== undefined && data.length) {
                    reqData = {
                        FirstName: data.firstname,
                        LastName: data.lastname,
                        Phone: data.phoneno
                    };
                }
                return data;
            };
        });

}());