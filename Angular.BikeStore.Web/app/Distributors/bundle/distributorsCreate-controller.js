(function (angular) {
    'use strict';

    angular.module('app')

    .controller('CreateDistributorCtrl', ['$scope', '$distributorsFactory', function ($scope, $distributorsFactory) {
        $scope.title = "Create Distributor";
    }]);

})(angular);