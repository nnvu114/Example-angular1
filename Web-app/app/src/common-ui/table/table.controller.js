/*
 * @CreateTime: Feb 1, 2018 12:00 PM
 * @Author: Thuan Nguyen
 * @Contact: thuannd15@fsoft.com.vn
 * @Last Modified By:
 * @Last Modified Time:
 * @Description:
 */
(function () {
    'use strict';

    angular
        .module('app')
        .controller('TableCtrl', TableCtrl);

    /** @ngInject */
    TableCtrl.$inject = ['$scope', '$filter', '$uibModal', '$log', 'PetDataService', 'uiGridConstants'];
    function TableCtrl($scope, $filter, $uibModal, $log, PetDataService, uiGridConstants) {
        var vm = this;

        init();

        function init() {

            $scope.data = [{
                    EmployeeID: "EmployeeID",
                    FirstName: "FirstName",
                    LastName: "LastName",
                    City: "City",
                    Region: "Region",
                    PostalCode: "PostalCode",
                    Country: "Country",
                    Notes: "Notes",
                },
                {
                    EmployeeID: "EmployeeID1",
                    FirstName: "FirstName1",
                    LastName: "LastName1",
                    City: "City1",
                    Region: "Region1",
                    PostalCode: "PostalCode1",
                    Country: "Country1",
                    Notes: "Notes1",
                },
                {
                    EmployeeID: "EmployeeID2",
                    FirstName: "FirstName2",
                    LastName: "LastName3",
                    City: "City4",
                    Region: "Region3",
                    PostalCode: "PostalCode3",
                    Country: "Country1",
                    Notes: "Notes1",
                }
            ];

            //Pagination varibles      
            var paginationOptions = {
                paginationPageSizes: [15, 25, 50, 75, 100],
                pageNumber: 1,
                pageSize: 10,
                sort: {
                    columnName: 'EmployeeID',
                    isAscending: false,
                }
            };

            $scope.currentPage = 1;
            $scope.pageSize = paginationOptions.pageSize;

            $scope.gridOptions = {};
            $scope.loadData = function () {
                $scope.gridOptions.totalItems = 100;

                $scope.totalPage = Math.ceil($scope.gridOptions.totalItems / $scope.pageSize);

                const Temp = {
                    EmployeeID: "EmployeeID 1",
                    FirstName: "FirstName1",
                    LastName: "LastName1",
                    City: "City1",
                    Region: "Region1",
                    PostalCode: "PostalCode1",
                    Country: "Country1",
                    Notes: "Notes1",
                };

                $scope.data.push(Temp);
                $scope.gridOptions.data = $scope.data;
            };

            $scope.loadData();

            vm.gridOptions = {
                enableRowSelection: true,
                selectionRowHeaderWidth: 35,
                enableRowHeaderSelection: false,
                //Added for custom paging      
                paginationPageSizes: [15, 25, 50, 75, 100],
                paginationPageSize: paginationOptions.pageSize,
                useExternalPagination: true, // custom      
                useExternalSorting: true, // custom      
                useExternalFiltering: true, // custom      
                enableSorting: true,
                columnDefs: [{
                        name: 'Edit',
                        field: 'EmployeeID',
                        enableSorting: false,
                        enableCellEdit: false
                    },
                    {
                        name: 'First Name',
                        field: 'FirstName',
                        headerCellClass: 'tablesorter-header-inner',
                        enableFiltering: true,
                        enableCellEdit: true
                    },
                    {
                        name: 'Last Name',
                        field: 'LastName'
                    },
                    {
                        name: 'City',
                        field: 'City'
                    },
                    {
                        name: 'Region',
                        field: 'Region'
                    },
                    {
                        name: 'Postal Code',
                        field: 'PostalCode'
                    },
                    {
                        name: 'Country',
                        field: 'Country'
                    },
                    {
                        name: 'Notes',
                        field: 'Notes',
                        width: '20%',
                        enableCellEdit: false,
                        headerCellClass: 'tablesorter-header-inner',
                        enableFiltering: true
                    }
                ],
                enableGridMenu: true,
                enableSelectAll: true,
                exporterMenuPdf: false,
                enableFiltering: true,
                exporterCsvFilename: 'EmployeeList_' + $filter('date')(new Date(), 'MM/dd/yyyy') + '.csv',
                exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        var msg = 'row selected ' + row.isSelected;
                        $log.log(msg);
                        console.log(msg);
                        //$window.alert(msg);      
                    });
                    gridApi.selection.on.rowSelectionChangedBatch($scope, function (rows) {
                        var msg = 'rows changed ' + rows.length;
                        $log.log(msg);
                        // $window.alert(msg);      
                        console.log(msg);
                    });
                    //Added for custom paging      
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        paginationOptions.pageNumber = newPage;
                        paginationOptions.pageSize = pageSize;
                        $scope.pageSize = pageSize;
                        $scope.currentPage = newPage;
                        $scope.totalPage = Math.ceil($scope.gridOptions.totalItems / $scope.pageSize);
                        $scope.loadData();
                    });
                    //custom sort      
                    $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                        if (sortColumns.length == 0) {
                            paginationOptions.sort = null;
                        } else {
                            paginationOptions.sort = sortColumns[0].sort.direction;
                        }
                        $scope.loadData();
                    });
                },
                data: $scope.data,
                totalItems: 100
            };
        }
    }
}());