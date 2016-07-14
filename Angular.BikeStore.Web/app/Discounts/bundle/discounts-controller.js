(function (angular) {
    'use strict';

    angular.module('app')

    .controller('DiscountsCtrl', ['$scope', '$discountsFactory', function ($scope, $discountsFactory) {
        $scope.title = "Discounts";

        $discountsFactory.getDiscounts().then(function (response) {
            if (response.status == "success") {
                $scope.discounts = response.result;
            }
        });

        $scope.createDiscount = function (discount) {
            $discountsFactory.createDiscount(discount).then(function (response) {
                if (response.status == "success") {
                    updateDiscountsCollection(response.result);
                }
            });
        }

        $scope.deleteDiscount = function (discountId) {
            $discountsFactory.deleteDiscount(discountId).then(function (response) {
                if (response.status == "success") {
                    updateDiscountsCollection(discountId);
                }
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