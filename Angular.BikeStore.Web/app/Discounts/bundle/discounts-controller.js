(function (angular) {
    'use strict';

    angular.module('app')

    .controller('DiscountsCtrl', ['$rootScope', '$scope', '$discountsFactory', function ($rootScope, $scope, $discountsFactory) {
        $scope.title = "Discounts";

        $rootScope.showLoader = true; // start spinner

        $discountsFactory.getDiscounts().then(function (response) {
            if (response.status == "success") {
                $scope.discounts = response.result;
                $rootScope.showLoader = false;
            }
        });

        $scope.createDiscount = function (discount) {
            $rootScope.showLoader = true; // start spinner
            $discountsFactory.createDiscount(discount).then(function (response) {
                if (response.status == "success") {
                    updateDiscountsCollection(response.result);

                }
                $rootScope.showLoader = false;
            });
        }

        $scope.deleteDiscount = function (discountId) {
            $rootScope.showLoader = true; // start spinner
            $discountsFactory.deleteDiscount(discountId).then(function (response) {
                if (response.status == "success") {
                    updateDiscountsCollection(discountId);
                }
                $rootScope.showLoader = false;
            });
        }







        function updateDiscountsCollection(discountData) {
            var discounts = angular.copy($scope.discounts);
            var newDiscountList = [];
            if (typeof discountData === 'string') {
                angular.forEach(discounts, function (value, key) {
                    if (value.Id !== discountData) {
                        newDiscountList.push(value);
                    }
                });
            } else if(typeof discountData === 'object') {
                discounts.push(discountData);
                newDiscountList = discounts;
            }
            
            $scope.discounts = newDiscountList;
        }
    }]);

})(angular);