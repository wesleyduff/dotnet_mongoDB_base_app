(function (angular) {
    'use strict';

    angular.module('app')

    .controller('HomeCtrl', ['$scope', function ($scope) {
        $scope.title = "Home";
    }]);

})(angular);