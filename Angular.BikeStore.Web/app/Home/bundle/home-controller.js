(function (angular) {
    'use strict';

    angular.module('app')

    .controller('HomeCtrl', ['$rootScope', '$scope', function ($rootScope, $scope) {
        $scope.title = "Home";

        $rootScope.showLoader = false;
    }]);

})(angular);