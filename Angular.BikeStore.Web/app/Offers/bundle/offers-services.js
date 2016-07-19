(function (angular) {

    'use strict';

    angular.module('app')

    .factory('$offersFactory', ['$resource', '$q', 'APPSETTINGS', function ($resource, $q, APPSETTINGS) {
        var resourceDiscount = $resource(APPSETTINGS.APISERVERPATH + '/api/Offers/:discountId',
             {
                 discountId: '@discountId',
                 offerId: '@offerId',
                 distributorId: '@distributorId'
             },
             {
             }
         );
        var resourceOffer = $resource(APPSETTINGS.APISERVERPATH + '/api/Offers/:offerId',
             {
                 discountId: '@discountId',
                 offerId: '@offerId',
                 distributorId: '@distributorId'
             },
             {
                 addOfferToDistributor: {
                     method: 'GET',
                     url: APPSETTINGS.APISERVERPATH + '/api/Distributor/:distributorId/AddOffer/:offerId'
                 }
             }
         );

        return {
           
            addOfferToDistributor: function(distributorId, offerId){
                var deferred = $q.defer();
                var postData = {
                    distributorId: distributorId,
                    offerId: offerId
                };
                resourceOffer.addOfferToDistributor(postData, function (data) {
                    deferred.resolve(data);
                });
                return deferred.promise;
            },
            get: function () {
                var deferred = $q.defer();
                resourceOffer.get({}, function (data) {
                    deferred.resolve(data);
                });

                return deferred.promise;
            },
            delete: function (offerId) {
                var deferred = $q.defer();
                resourceOffer.delete({
                    offerId: offerId
                }, function (data) {
                    deferred.resolve(data);
                });

                return deferred.promise;
            },
            create: function (postData) {
                var offer = {
                    Title: postData.title,
                    Discounts : postData.selectedOptions
                }
                var deferred = $q.defer();
                resourceOffer.save(offer, function (data) {
                    deferred.resolve(data);
                });

                return deferred.promise;
            }
            

        }
    }])

})(angular);