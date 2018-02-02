/*
 * @CreateTime: Dec 1, 2017 11:49 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Dec 2, 2017 10:47 PM
 * @Description: Modify Here, Please 
 */

(function () {
    'use strict';
    angular
        .module('app')
        .controller('UserManagementCtrl', UserManagementCtrl);

    /** @ngInject */
    UserManagementCtrl.$inject = ['$scope', '$filter', '$uibModal', '$log', 'PetDataService', 'uiGridConstants'];
    function UserManagementCtrl($scope, $filter, $uibModal, $log, PetDataService, uiGridConstants) {
        var vm = this;
        init();

        function init() {
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

            $scope.data = [
                {
                    EmployeeID : "EmployeeID",
                    FirstName : "FirstName",
                    LastName:"LastName",
                    City:"City",
                    Region:"Region",
                    PostalCode :"PostalCode",
                    Country:"Country",
                    Notes: "Notes",
                },
                {
                    EmployeeID : "EmployeeID1",
                    FirstName : "FirstName1",
                    LastName:"LastName1",
                    City:"City1",
                    Region:"Region1",
                    PostalCode :"PostalCode1",
                    Country:"Country1",
                    Notes: "Notes1",
                },  
                {
                    EmployeeID : "EmployeeID2",
                    FirstName : "FirstName2",
                    LastName:"LastName3",
                    City:"City4",
                    Region:"Region3",
                    PostalCode :"PostalCode3",
                    Country:"Country1",
                    Notes: "Notes1",
                }
            ]
            $scope.gridOptions = {};
            //end here      
            $scope.loadData = function () {
                  $scope.gridOptions.totalItems = 1000;
                  $scope.totalPage = Math.ceil($scope.gridOptions.totalItems / $scope.pageSize);
                  $scope.gridOptions.data = $scope.data;
            };

            $scope.loadData();

            $scope.gridOptions = {
                enableRowSelection: true,
                selectionRowHeaderWidth: 35,
                enableRowHeaderSelection: false,
                //Added for custom paging      
                paginationPageSizes: [$scope.pageSize, $scope.pageSize * 2, $scope.pageSize * 3],
                paginationPageSize: paginationOptions.pageSize,
                useExternalPagination: true, // custom      
                useExternalSorting: true, // custom      
                useExternalFiltering: true, // custom      
                enableSorting: true,
                columnDefs: [{
                    name: 'Edit',
                    field: 'EmployeeID',
                    width: '10%',
                    enableColumnMenu: false,
                    cellTemplate: '<button title="Edit" class="btn btn-xs btn-primary fa fa-edit" ng-click="grid.appScope.editEmployee(row)">Edit </button>',
                    width: 50,
                    pinnedLeft: false,
                    enableHiding: false,
                    exporterSuppressExport: true,
                    enableSorting: false,
                    enableFiltering: false
                }, {
                    name: 'First Name',
                    field: 'FirstName',
                    headerCellClass: 'tablesorter-header-inner',
                    enableFiltering: true,
                    enableCellEdit: true,
                }, {
                    name: 'Last Name',
                    field: 'LastName',
                    headerCellClass: 'tablesorter-header-inner',
                    enableFiltering: true,
                    enableCellEdit: true,
                }, {
                    name: 'City',
                    field: 'City',
                    headerCellClass: 'tablesorter-header-inner',
                    enableFiltering: true,
                    enableCellEdit: true,
                }, {
                    name: 'Region',
                    field: 'Region',
                    enableCellEdit: false,
                    headerCellClass: 'tablesorter-header-inner',
                    enableFiltering: true
                }, {
                    name: 'Postal Code',
                    field: 'PostalCode',
                    enableCellEdit: false,
                    headerCellClass: 'tablesorter-header-inner',
                    enableFiltering: true
                }, {
                    name: 'Country',
                    field: 'Country',
                    enableCellEdit: false,
                    headerCellClass: 'tablesorter-header-inner',
                    enableFiltering: true
                }, {
                    name: 'Notes',
                    field: 'Notes',
                    width: '20%',
                    enableCellEdit: false,
                    headerCellClass: 'tablesorter-header-inner',
                    enableFiltering: true
                }],
                //This code used for export grid data in csv file      
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
                data: $scope.gridOptions.data      
            };
        }
        //Delete employee      
        $scope.deleteSelected = function () {
            angular.forEach($scope.gridApi.selection.getSelectedRows(), function (data, index) {
                $scope.employees.splice($scope.employees.lastIndexOf(data), 1);
            });
        }

        //refresh grid data after save of update      
        $scope.RefreshGridData = function () {
            $scope.loadData();
        };

        //-----------------------------------------------------------------------------------------
        vm.pac = {
            demo: 'HELLO WORLD ',
            isActive: ''
        };

        $scope.update = function (values) {
            console.log('xxxxxxxx');
        }

        vm.status = true;

        vm.open = function (size, modalId) {
            var modalInstance = $uibModal.open({
                templateUrl: modalId,
                controller: function ($uibModalInstance, $scope, values) {
                    $scope.model = values;
                    $scope.update = function (values) {
                        $scope.model = {};
                        $uibModalInstance.close(values);
                    }

                    $scope.cancel = function () {
                        $uibModalInstance.dismiss('cancel');
                    };
                },
                size: size,
                resolve: {
                    values: function () {
                        if (vm.status == true) {
                            return vm.pac;
                        } else {
                            return {};
                        }

                    }
                }
            });

            modalInstance.result.then(function (selectedItem) {
                console.log('selectedItem');
                console.log(selectedItem);
                $scope.selected = selectedItem;
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        };

        // PETRONAS USER REGION
    };
}());
