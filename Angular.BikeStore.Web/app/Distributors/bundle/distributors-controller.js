(function (angular) {
    'use strict';

    angular.module('app')

    .controller('DistributorsCtrl', ['$scope', '$distributorsFactory', function ($scope, $distributorsFactory) {

        $scope.title = "Distributors";
        $scope.distributors = [];
        $scope.selectedDistributor = {};

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


        $scope.GetDistributor = function (distributorId) {
            $distributorsFactory.getDistributor(distributorId).then(function (response) {
                if (response.status === "success") {
                    $scope.distributorsModalData = response.result;
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