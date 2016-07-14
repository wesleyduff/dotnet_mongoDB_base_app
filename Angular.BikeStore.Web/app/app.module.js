(function (angular) {

    'use strict';

    angular.module('app', ['ui.router', 'oc.lazyLoad', 'ngResource'])
    .constant('APPSETTINGS', {
        APISERVERPATH: 'http://localhost:26639'
    })
    .config(['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider', '$httpProvider', function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider, $httpProvider) {

        //Setting up for CORS
        $httpProvider.defaults.useXDomain = true;
        delete $httpProvider.defaults.headers.common["X-Requested-With"];
        $httpProvider.defaults.headers.common["Accept"] = "application/json";
        $httpProvider.defaults.headers.common["Content-Type"] = "application/json";


        // For any unmatched url, redirect to /state1
        $urlRouterProvider.otherwise("/Home");

        // Now set up the states
        $stateProvider
          .state('home', {
              url: "/Home",
              views: {
                  "lazyLoadView": {
                      controller: 'HomeCtrl',
                      templateUrl: "./app/Home/views/index.html"
                  }
              },
              resolve: {
                  loadMainCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                      return $ocLazyLoad.load('./app/Home/bundle/home-controller.js')
                  }]
              }
          })
        .state('offers', {
            url: "/Offers",
            views: {
                "lazyLoadView": {
                    controller: 'OffersCtrl',
                    templateUrl: "./app/Offers/views/index.html"
                }
            },
            resolve: {
                loadMainCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        './app/Offers/bundle/offers-controller.js',
                        './app/Offers/bundle/offers-services.js',
                        './app/Discounts/bundle/discounts-services.js'
                    ])
                }]
            }
        })
        .state('discounts', {
            url: "/Discounts",
            views: {
                "lazyLoadView": {
                    controller: 'DiscountsCtrl',
                    templateUrl: "./app/Discounts/Views/index.html"
                }
            },
            resolve: {
                loadMainCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        './app/Discounts/bundle/discounts-controller.js',
                        './app/Discounts/bundle/discounts-services.js'
                    ])
                }]
            }
         })
        .state('distributors', {
            url: "/Distributors",
            views: {
                "lazyLoadView": {
                    controller: 'DistributorsCtrl',
                    templateUrl: "./app/Distributors/Views/index.html"
                }
            },
            resolve: {
                loadMainCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        './app/Distributors/bundle/distributors-controller.js',
                        './app/Distributors/bundle/distributors-services.js'
                    ])
                }]
            }
        })
          

    }]);


    //bootstrap application to module "app"
    angular.bootstrap(document, ['app']);
})(angular)