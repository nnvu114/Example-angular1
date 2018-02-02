/*
 * @CreateTime: Dec 1, 2017 11:49 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Dec 2, 2017 10:47 PM
 * @Description: Modify Here, Please 
 */

(function() {
    'use strict';

    var $viewPathRoot = 'partials';
    angular.module('app', [
        'ui.router',
        'ui.router.state.events',
        'ui.bootstrap',
        'LocalStorageModule',
        'underscore',
        'angular-google-analytics',
        'angular-md5',
        'ui.grid',                  //data grid for AngularJS
        'ui.grid.pagination',       //data grid Pagination
        'ui.grid.resizeColumns',    //data grid Resize column
        'ui.grid.moveColumns',      //data grid Move column
        'ui.grid.pinning',          //data grid Pin column Left/Right
        'ui.grid.selection',        //data grid Select Rows
        'ui.grid.autoResize',       //data grid Enabled auto column Size
        'ui.grid.exporter',         //data grid Export Data
        'ui.grid.edit'
    ]);

}());