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
                 distributorId: '@distributorId',
                 bikeId: '@bikeId'
             },
             {
                 adjustPrice: {
                     method: 'POST',
                     url: APPSETTINGS.APISERVERPATH + '/api/Inventory/AdjustPrice'
                 },
                 NewLine: {
                     method: 'POST',
                     url: APPSETTINGS.APISERVERPATH + '/api/Inventory/NewLine'
                 },
                 DeleteLine: {
                     url: APPSETTINGS.APISERVERPATH + '/api/Distributor/Bike'
                 }
             }
         );

        return {
            deleteProductFromInventory: function (distributorsId, bikeId) {
                var deferred = $q.defer();
                var postData = {
                    distributorId: distributorsId,
                    bikeId: bikeId
                };
                inventoryResource.DeleteLine(postData, function (data) {
                    deferred.resolve(data);
                });

                return deferred.promise;
            },
            adjustPrice: function(postData){
                var deferred = $q.defer();
                inventoryResource.adjustPrice(postData, function (data) {
                    deferred.resolve(data);
                });

                return deferred.promise;
            },
            addItemToInventory: function (postData, distributorId) {
                var _postData = {
                    Bike: {
                        Brand: {
                            Name: postData.Bike.Brand
                        },
                        Model: {
                            Year: postData.Bike.Model.Year,
                            Name: postData.Bike.Model.Name
                        },
                        Price: {
                            Value: postData.Bike.Price.Value
                        }
                    },
                    Quantity : postData.Quantity,
                    distributorId: distributorId
                };
                var deferred = $q.defer();
                inventoryResource.NewLine(_postData, function (data) {
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
                distributorResource.save(postData, function (data) {
                    deferred.resolve(data);
                });

                return deferred.promise;
            }


        }
    }])

})(angular);