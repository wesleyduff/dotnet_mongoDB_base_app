(function (angular) {
    'use strict';

    angular.module('app')

    .controller('OffersCtrl', ['$scope', '$offersFactory', '$discountsFactory', function ($scope, $offersFactory, $discountsFactory) {
        $scope.title = "Offers";

        //load discounts for drop down
        (function (discount) {
            $discountsFactory.getDiscounts().then(function (response) {
                if (response.status == "success") {
                    $scope.discountOptions = {
                        availableOptions: response.result,
                        selectedOptions: [response.result[0]]
                    }
                }
            });

            $offersFactory.get().then(function (response) {
                if (response.status == "success") {
                    $scope.offers = response.result;
                }
            });

        })();

        $scope.AddOfferToDistributor = function(distributorId, offerId){
            $offersFactory.addOfferToDistributor(distributorId, offerId).then(function (response) {
                if (response.status === "success") {
                    //from parent controller - distributors controller
                    $scope.updateUIViewVisibility(false);
                    //Call private method on Distributor controller - through events
                    $scope.$emit('updateDistributors');
                }
            });
        }

        $scope.AddOffer = function (postOffer) {
            $offersFactory.create(postOffer).then(function (response) {
                if (response.status === "success") {
                    updateOffersCollection(response.result);
                }
            });
        }

        $scope.DeleteOffer = function (offerId) {
            $offersFactory.delete(offerId).then(function (response) {
                if (response.status === "success") {
                    updateOffersCollection(offerId);
                }
            })
        }



        function updateOffersCollection(offerData) {
            var offers = angular.copy($scope.offers);
            var newOffersList = [];
            if (typeof offerData === 'string') {
                angular.forEach(offers, function (value, key) {
                    if (value.Id !== offerData) {
                        newOffersList.push(value);
                    }
                });
            } else if (typeof offerData === 'object') {
                offers.push(offerData);
                newOffersList = offers;
            }

            $scope.offers = newOffersList;
        }



    }]);

})(angular);