(function (angular) {

    'use strict';

    angular.module('app', ['ui.router', 'oc.lazyLoad', 'ngResource'])
    .constant('APPSETTINGS', {
        APISERVERPATH: 'http://trainerroadapplyapi.azurewebsites.net'
    })
    .config(['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider', '$httpProvider', function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider, $httpProvider) {
        //$httpProvider.defaults.withCredentials = true;
                 
        //$httpProvider.defaults.useXDomain = true;
        //$httpProvider.defaults.headers.common["Access-Control-Allow-Origin: http://http://trainerroaddemo.azurewebsites.net/"];
       // $httpProvider.defaults.headers.common["Accept"] = "application/json";
        //$httpProvider.defaults.headers.common["Content-Type"] = "application/json"       
        // For any unmatched url, redirect to /state1
        $urlRouterProvider.otherwise("/Home");
        // Now set up the states
        $stateProvider
          //editable states
            
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
                        './app/Distributors/bundle/distributors-services.js',
                        './app/Distributors/Views/inventory.html'
                    ])
                }]
            }
        })
        .state('distributors.createDistributor', {
            views: {
                "EditCreateView": {
                    controller: 'CreateDistributorCtrl',
                    templateUrl: "./app/Distributors/Views/createDistributor.html",
                }
            },
            resolve: {
                loadMainCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        "./app/Distributors/bundle/distributorsCreate-controller.js",
                        "./app/Distributors/Views/createDistributor.html"
                    ])
                }]
            }
        })
        .state('distributors.addItemToInventory', {
            views: {
                "EditCreateView": {
                    controller: 'InventoryCtrl',
                    templateUrl: "./app/Distributors/Views/addInventoryItem.html",
                }
            },
            resolve: {
                loadMainCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        "./app/Distributors/bundle/inventory-controller.js",
                        "./app/Distributors/Views/addInventoryItem.html"
                        //inventory already called so the file has already been lazy loaded. No need to check
                    ])
                }]
            }
        })
        .state('distributors.addOfferToDistributor', {
            views: {
                "EditCreateView": {
                    controller: 'OffersCtrl',
                    templateUrl: "./app/Offers/Views/addOfferItem.html",
                }
            },
            resolve: {
                loadMainCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        "./app/Offers/bundle/offers-controller.js",
                        "./app/Offers/bundle/offers-services.js",
                        './app/Discounts/bundle/discounts-services.js',
                        "./app/Offers/Views/addOfferItem.html"
                    ])
                }]
            }
        })
        /*
        Receipt Views 
        */
        .state('distributors.SummaryHtml', {
            views: {
                "ReceiptView": {
                    controller: 'receiptCtrl',
                    templateUrl: "./app/Distributors/Views/SummaryHtml.html",
                }
            },
            resolve: {
                loadMainCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        "./app/Distributors/bundle/receipt-controller.js",
                         "./app/Distributors/Views/SummaryHtml.html"
                    ])
                }]
            }
        })
        .state('distributors.FullHtml', {
            views: {
                "ReceiptView": {
                    controller: 'receiptCtrl',
                    templateUrl: "./app/Distributors/Views/FullHtml.html"
                }
            },
            resolve: {
                loadMainCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        "./app/Distributors/bundle/receipt-controller.js",
                        "./app/Distributors/Views/FullHtml.html"
                    ])
                }]
            }
        })
        .state('distributors.Text', {
            views: {
                "ReceiptView": {
                    controller: 'receiptCtrl',
                    templateUrl: "./app/Distributors/Views/Text.html",
                }
            },
            resolve: {
                loadMainCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        "./app/Distributors/bundle/receipt-controller.js",
                         "./app/Distributors/Views/Text.html"
                    ])
                }]
            }
        })
          

    }]);


    //bootstrap application to module "app"
    angular.bootstrap(document, ['app']);
})(angular)