(function (angular) {

    'use strict';

    angular.module('app', ['ui.router', 'oc.lazyLoad'])
    .config(['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider', function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider) {
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
                    return $ocLazyLoad.load('./app/Offers/bundle/offers-controller.js')
                }]
            }
        })
          

    }]);


    //bootstrap application to module "app"
    angular.bootstrap(document, ['app']);
})(angular)