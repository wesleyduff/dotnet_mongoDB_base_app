(function (angular) {

    'use strict';

    angular.module('app')

    .factory('$discountsFactory', ['$resource', '$q', 'APPSETTINGS', function ($resource, $q, APPSETTINGS) {
        var resource = $resource(APPSETTINGS.APISERVERPATH + '/api/Discounts/:discountId',
             {
                 discountId: '@id'
             },
             {
             }
         );

        return {
            getDiscounts: function () {
                var deferred = $q.defer();
                resource.get({}, function (data) {
                    deferred.resolve(data);
                });

                return deferred.promise;
            },
            deleteDiscount: function (discountId) {
                var deferred = $q.defer();
                resource.delete({
                    id: discountId
                }, function (data) {
                    deferred.resolve(data);
                });

                return deferred.promise;
            },
            createDiscount: function (discount) {
                var deferred = $q.defer();
                resource.save(discount, function (data) {
                    deferred.resolve(data);
                });

                return deferred.promise;
            }
            
        }
    }])

})(angular);