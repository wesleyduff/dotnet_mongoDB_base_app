(function (angular) {
    'use strict';

    angular.module('app')

    .controller('DistributorsCtrl', ['$scope', '$distributorsFactory', function ($scope, $distributorsFactory) {
        $scope.actionButtonTitle = "Add New Distributor";
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

        var state = 0;
        $scope.manageActionButtonTitle = function () {
            if (state === 0) {
                state = 1;
                $scope.actionButtonTitle = "Save Distributor";
            } else if (state === 1) {
                state = 0;
                $scope.actionButtonTitle = "Add New Distributor";
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