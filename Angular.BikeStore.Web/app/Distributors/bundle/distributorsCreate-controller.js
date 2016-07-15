(function (angular) {
    'use strict';

    angular.module('app')

    .controller('CreateDistributorCtrl', ['$scope', '$distributorsFactory', function ($scope, $distributorsFactory) {
        $scope.dubTitle = "Create Distributor";
    }]);

})(angular);