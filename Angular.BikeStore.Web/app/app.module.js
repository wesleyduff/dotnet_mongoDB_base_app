(function (angular) {

    'use strict';

    angular.module('app', ['ui.router', 'oc.lazyLoad', 'ngResource', 'angularSpinners'])
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


    }])

    //Adding MainController 
    .controller('MainController', ['$rootScope', '$scope', function ($rootScope, $scope) {
        //showSpinner
        $rootScope.showLoader = false;
    }])

    // Adding loading directive
    .directive('loader', function (spinnerService) {
        return {
            restrict: 'E',
            replace: true,
            transclude: true,
            scope: {
                name: '@?',
                group: '@?',
                show: '=',
                imgSrc: '@?',
                register: '@?',
                onLoaded: '&?',
                onShow: '&?',
                onHide: '&?'
            },
            template: [
                ' <div ng-show="show" class="spinner">',
                '   <div class="bounce1">',
                '   </div>',
                '   <div class="bounce2"></div>',
                '   <div class="bounce3"></div>',
                ' </div>',
                '</div>'
                
            ].join(''),
            controller: function ($scope, spinnerService) {

                // register should be true by default if not specified.
                if (!$scope.hasOwnProperty('register')) {
                    $scope.register = true;
                } else {
                    $scope.register = !!$scope.register;
                }

                // Declare a mini-API to hand off to our service so the 
                // service doesn't have a direct reference to this
                // directive's scope.
                var api = {
                    name: $scope.name,
                    group: $scope.group,
                    show: function () {
                        $scope.show = true;
                    },
                    hide: function () {
                        $scope.show = false;
                    },
                    toggle: function () {
                        $scope.show = !$scope.show;
                    }
                };

                // Register this spinner with the spinner service.
                if ($scope.register === true) {
                    spinnerService._register(api);
                }

                // If an onShow or onHide expression was provided,
                // register a watcher that will fire the relevant
                // expression when show's value changes.
                if ($scope.onShow || $scope.onHide) {
                    $scope.$watch('show', function (show) {
                        if (show && $scope.onShow) {
                            $scope.onShow({
                                spinnerService: spinnerService,
                                spinnerApi: api
                            });
                        } else if (!show && $scope.onHide) {
                            $scope.onHide({
                                spinnerService: spinnerService,
                                spinnerApi: api
                            });
                        }
                    });
                }

                // This spinner is good to go.
                // Fire the onLoaded expression if provided.
                if ($scope.onLoaded) {
                    $scope.onLoaded({
                        spinnerService: spinnerService,
                        spinnerApi: api
                    });
                }
            }
        };
    });


    //bootstrap application to module "app"
    angular.bootstrap(document, ['app']);
})(angular)