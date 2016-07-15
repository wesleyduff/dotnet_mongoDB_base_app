(function (angular) {
    'use strict';

    angular.module('app')

    .controller('DistributorsCtrl', ['$scope', '$distributorsFactory', function ($scope, $distributorsFactory) {
        $scope.disableActionButton = false;
        $scope.title = "Distributors";
        $scope.distributors = [];
        $scope.selectedDistributor = {};
        $scope.showPriceEdit = false;
        //Initialize
        (function () {
            $distributorsFactory.get().then(function (response) {
                if (response.status === "success") {
                    $scope.distributors = response.result;
                }
            });

            $('#distributorsModal').on('shown.bs.modal', function () {
            });

        })();
        $scope.updateUIViewVisibility = function (state) {
            
            if (!state) {
                $scope.disableActionButton = false;
            } else {
                $scope.disableActionButton = true;
            }
        }

        $scope.GetDistributor = function (distributorId) {
           
            $distributorsFactory.getDistributor(distributorId).then(function (response) {
                if (response.status === "success") {
                    $scope.distributorsModalData = response.result;
                }
            });
        }

        $scope.UpdatePrice = function (distributorId, bikeId, newPrice) {
            var postData = {
                distributorId: distributorId,
                BikeId: bikeId,
                NewPrice: {
                    Value: newPrice
                }
            };
            $distributorsFactory.adjustPrice(postData).then(function (response) {
                if (response.status === "success") {
                    $scope.distributorsModalData.Inventory = response.result;
                }
            });
        }


        $scope.createDistributor = function (postData) {
            $distributorsFactory.create(postData).then(function (response) {
                if (response.status === "success") {
                    $scope.disableActionButton = false;
                    $scope.distributors.push(response.result);
                }
            })
        }


        $scope.deleteProductFromInventory = function (distributorId, bikeId) {
            $distributorsFactory.deleteProductFromInventory(distributorId, bikeId).then(function (response) {
                if (response.status === "success") {
                    $scope.distributorsModalData.Inventory = response.result;
                }
            })
        }


        /* EVENT HANDLERS FROM OTHER CONTROLLERS */
        $scope.$on('inventoryUpdate', function () {
            $scope.disableActionButton = false;
        });

        //Update the collection for Distributors
        function updateDistributorsCollection(distributorData) {
            var distributors = angular.copy($scope.distributors);
            var newDistributorsList = [];
            if (typeof distributorData === 'string') {
                angular.forEach(distributors, function (value, key) {
                    if (value.Id !== distributorData) {
                        newDistributorsList.push(value);
                    }
                });
            } else if (typeof distributorData === 'object') {
                distributors.push(distributorData);
                newDistributorsList = distributors;
            }

            $scope.distributors = newDistributorsList;
        }

    }]);

})(angular);