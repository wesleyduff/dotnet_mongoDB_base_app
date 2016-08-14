(function (angular) {
    'use strict';

    angular.module('app')

    .controller('OffersCtrl', ['$rootScope', '$scope', '$offersFactory', '$discountsFactory', function ($rootScope, $scope, $offersFactory, $discountsFactory) {
        $scope.title = "Offers";
        $rootScope.showLoader = true;
        //load discounts for drop down
        (function (discount) {
            var isLoaded = [];
            $discountsFactory.getDiscounts().then(function (response) {
                if (response.status == "success") {
                    $scope.discountOptions = {
                        availableOptions: response.result,
                        selectedOptions: [response.result[0]]
                    }
                }
                $rootScope.showLoader = false;
            });

            $offersFactory.get().then(function (response) {
                if (response.status == "success") {
                    $scope.offers = response.result;
                }
            });

            

        })();

        $scope.AddOfferToDistributor = function (distributorId, offerId) {
            $rootScope.showLoader = true;
            $offersFactory.addOfferToDistributor(distributorId, offerId).then(function (response) {
                if (response.status === "success") {
                    //from parent controller - distributors controller
                    $scope.updateUIViewVisibility(false);
                    //Call private method on Distributor controller - through events
                    $scope.$emit('updateDistributors');
                }
                $rootScope.showLoader = false;
            });
        }

        $scope.AddOffer = function (postOffer) {
            $rootScope.showLoader = true;
            $offersFactory.create(postOffer).then(function (response) {
                if (response.status === "success") {
                    updateOffersCollection(response.result);
                }
                $rootScope.showLoader = false;
            });
        }

        $scope.DeleteOffer = function (offerId) {
            $rootScope.showLoader = true;
            $offersFactory.delete(offerId).then(function (response) {
                if (response.status === "success") {
                    updateOffersCollection(offerId);
                }
                $rootScope.showLoader = false;
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