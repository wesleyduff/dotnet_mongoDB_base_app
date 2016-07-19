(function (angular) {
    'use strict';

    angular.module('app')

    .controller('InventoryCtrl', ['$scope', '$distributorsFactory', function ($scope, $distributorsFactory) {
        $scope.title = "Inventory";

        $scope.addInventoryItem = function (postData, distributorId) {
            
            $distributorsFactory.addItemToInventory(postData, distributorId).then(function (response) {
                if (response.status === "success") {
                    $scope.$emit('inventoryUpdate');
                }
            })
        }


    }]);

})(angular);