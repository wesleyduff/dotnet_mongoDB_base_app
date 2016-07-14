(function (angular) {

    'use strict';

    angular.module('app')

    .factory('$distributorsFactory', ['$resource', '$q', 'APPSETTINGS', function ($resource, $q, APPSETTINGS) {
        var resource = $resource(APPSETTINGS.APISERVERPATH + '/api/Distributor/:distributorId',
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

        return {
            get: function () {
                var deferred = $q.defer();
                resource.get({}, function (data) {
                    deferred.resolve(data);
                });

                return deferred.promise;
            },
            getDistributor: function(distributorId){
                var deferred = $q.defer();
                resource.getDistributor({ id: distributorId }, function (data) {
                    deferred.resolve(data);
                });

                return deferred.promise;
            },
            delete: function (Id) {
                var deferred = $q.defer();
                resource.delete({
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
                resource.save(offer, function (data) {
                    data.result = offer;
                    deferred.resolve(data);
                });

                return deferred.promise;
            }


        }
    }])

})(angular);