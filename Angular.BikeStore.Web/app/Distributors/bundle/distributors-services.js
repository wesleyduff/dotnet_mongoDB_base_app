(function (angular) {

    'use strict';

    angular.module('app')

    .factory('$distributorsFactory', ['$resource', '$q', 'APPSETTINGS', function ($resource, $q, APPSETTINGS) {
        var distributorResource = $resource(APPSETTINGS.APISERVERPATH + '/api/Distributor/:distributorId',
             {
                 distributorId: '@distributorId'
             },
             {
                 getDistributor: {
                     method: 'GET',
                     distributorId: '@distributorId'
                 }
             }
         );

        var inventoryResource = $resource(APPSETTINGS.APISERVERPATH + '/api/Inventory/',
             {
                 distributorId: '@distributorId'
             },
             {
                 adjustPrice: {
                     method: 'POST',
                     url: APPSETTINGS.APISERVERPATH + '/api/Inventory/AdjustPrice'
                 }
             }
         );

        return {
            //todo:// Adjust Price failing on post
            adjustPrice: function(postData){
                var deferred = $q.defer();
                inventoryResource.adjustPrice(postData, function (data) {
                    deferred.resolve(data);
                });

                return deferred.promise;
            },
            get: function () {
                var deferred = $q.defer();
                distributorResource.get({}, function (data) {
                    deferred.resolve(data);
                });

                return deferred.promise;
            },
            getDistributor: function(distributorId){
                var deferred = $q.defer();
                distributorResource.getDistributor({ id: distributorId }, function (data) {
                    deferred.resolve(data);
                });

                return deferred.promise;
            },
            delete: function (Id) {
                var deferred = $q.defer();
                distributorResource.delete({
                    id: Id
                }, function (data) {
                    deferred.resolve(data);
                });

                return deferred.promise;
            },
            create: function (postData) {
                var Distributor = {

                }
                var deferred = $q.defer();
                distributorResource.save(offer, function (data) {
                    data.result = offer;
                    deferred.resolve(data);
                });

                return deferred.promise;
            }


        }
    }])

})(angular);