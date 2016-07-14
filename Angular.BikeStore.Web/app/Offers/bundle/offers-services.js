(function (angular) {

    'use strict';

    angular.module('app')

    .factory('$offersFactory', ['$resource', '$q', 'APPSETTINGS', function ($resource, $q, APPSETTINGS) {
        var resource = $resource(APPSETTINGS.APISERVERPATH + '/api/Offers/:discountId',
             {
                 discountId: '@id'
             },
             {
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
                var offer = {
                    Title: postData.title,
                    Discounts : postData.selectedOptions
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