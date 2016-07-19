(function (angular) {
    'use strict';

    angular.module('app')

    .controller('receiptCtrl', ['$scope', '$distributorsFactory', function ($scope, $distributorsFactory) {
        $scope.title = "Print - ";

        (function () {
            var distributorId = document.querySelector('#distributorsReceiptModal').dataset.distributorid;
            $distributorsFactory.getReceiptData(distributorId).then(function (response) {
                if (response.status == "success") {
                    response.Title = "Here is your receipt view: " + response.Company;
                    $scope.ReceiptData = {
                        SubtotalString: response.SubtotalString,
                        TaxString: response.TaxString,
                        TotalString: response.TotalString,
                        Inventory : response.Inventory,
                        Company: response.Company,
                        TotalItems: response.TotalItems,
                        TotalDiscount: response.TotalDiscount,
                        BeforeDiscount: response.BeforeDiscount
                    }
                }
            });
        })();

    }]);

})(angular);