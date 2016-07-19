(function (angular) {
    'use strict';

    angular.module('app')

    .controller('DistributorsCtrl', ['$scope', '$state', '$distributorsFactory', function ($scope, $state, $distributorsFactory) {
        $scope.disableActionButton = false;
        $scope.title = "Distributors";
        $scope.distributors = [];
        $scope.selectedDistributor = {};
        $scope.showPriceEdit = false;
        
        var receiptTypes = [];
        //Initialize
        (function () {
            $distributorsFactory.get().then(function (response) {
                if (response.status === "success") {
                    $scope.distributors = response.result;
                }
            });

            $('#distributorsReceiptModal').on('shown.bs.modal', function () {
            });

            $distributorsFactory.getReceiptTypes().then(function (response) {
                if (response.status === "success") {
                    receiptTypes = response.result;
                    $scope.receiptTypes = receiptTypes;
                }
            });

        })();

        $scope.updateUIViewVisibility = function (state) {
            
            if (!state) {
                $scope.disableActionButton = false;
            } else {
                $scope.disableActionButton = true;
            }
        }

        $scope.updateReceiptList = function (selectedValue) {
            var postData = {
                distributorId: $scope.distributorsModalData.Id,
                ReceiptList : selectedValue
            };
            $distributorsFactory.updateReceiptList(postData).then(function (response) {
                if (response.status === "success") {
                    $scope.receiptTypesOffered.selectedOptions = response.result;
                    hardUpdateDistributorsCollection();
                }
            });
        }

        $scope.viewReceiptType = function (selectedValue) {
            var stateHook = 'distributors.' + selectedValue.RtypeAsString;
            $state.go(stateHook);
        }
        $scope.GetDistributor = function (distributorId) {
            $distributorsFactory.getDistributor(distributorId).then(function (response) {
                if (response.status === "success") {
                    $scope.distributorsModalData = response.result;
                    $scope.receiptTypesOffered = {
                        availableOptions: receiptTypes,
                        selectedOptions: response.result.ReceiptTypesOffered
                    }
                    var stateHook = 'distributors.' + response.result.ReceiptTypesOffered[0].RtypeAsString;
                    $state.go(stateHook);
                    $scope.receiptTypesOfferedSingle = {
                        availableOptions: response.result.ReceiptTypesOffered,
                        selectedOption: response.result.ReceiptTypesOffered[0]
                    }
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


        $scope.createDistributor = function (postData) {
            $distributorsFactory.create(postData).then(function (response) {
                if (response.status === "success") {
                    $scope.disableActionButton = false;
                    $scope.distributors.push(response.result);
                }
            })
        }

        $scope.deleteDistributor = function (distributorId) {
            $distributorsFactory.deleteDistributor(distributorId).then(function (response) {
                if (response.status === "success") {
                    $scope.distributors.pop(response.result);
                }
            });
        }


       
        $scope.RemoveOfferFromDistributor = function (distributorId, offerId) {
            $distributorsFactory.RemoveOfferFromDistributor(distributorId, offerId).then(function (response) {
                if (response.status === "success") {
                    $scope.updateUIViewVisibility(false);
                    //we do not have the full distributor on the response, a hard refresh of data from the API is needed
                    hardUpdateDistributorsCollection();
                }
            });
        }

        $scope.deleteProductFromInventory = function (distributorId, bikeId) {
            $distributorsFactory.deleteProductFromInventory(distributorId, bikeId).then(function (response) {
                if (response.status === "success") {
                    hardUpdateDistributorsCollection();
                }
            })
        }

        /* EVENT HANDLERS FROM OTHER CONTROLLERS */
        $scope.$on('inventoryUpdate', function () {
            $scope.disableActionButton = false;
            hardUpdateDistributorsCollection();
        });

        $scope.$on('updateDistributors', function () {
            hardUpdateDistributorsCollection();
        });

        function hardUpdateDistributorsCollection() {
            $distributorsFactory.get().then(function (response) {
                if (response.status === "success") {
                    $scope.distributors = response.result;
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