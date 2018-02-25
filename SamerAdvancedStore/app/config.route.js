(function () {
    'use strict';

    var app = angular.module('app');

    // Collect the routes
    app.constant('routes', getRoutes());

    // Configure the routes and route resolvers
    app.config(['$routeProvider', 'routes', routeConfigurator]);
    function routeConfigurator($routeProvider, routes) {
        console.log(routes);
        routes.forEach(function (r) {
            $routeProvider.when(r.url, r.config);
            console.log(r);
        });
        $routeProvider.otherwise({ redirectTo: '/' });
    }

    // Define the routes 
    function getRoutes() {
        return [
            {
                url: '/',
                config: {
                    templateUrl: 'app/controller/dashboard/dashboard.html',
                    title: 'dashboard',
                    settings: {
                     
                    }
                }
            }, {
                url: '/selles/',
                config: {
                    title: 'المبيعات',
                    templateUrl: 'app/controller/selles/data.html',
                    settings: {
                        nav: 2,
                        content: '<i class="fa fa-money"></i> الفواتير'
                    }
                }
            }, {
                url: '/data/',
                config: {
                    title: 'إدخال بيانات',
                    templateUrl: 'app/controller/data/data.html',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-list-alt"></i> إدخال بيانات'
                    }
                }
            }, {
                url: '/data/category',
                config: {
                    title: 'إدخال بيانات',
                    templateUrl: 'app/controller/data/category/category.html',
                    settings: {

                    }
                }
            }, {
                url: '/data/product',
                config: {
                    title: 'إدخال بيانات',
                    templateUrl: 'app/controller/data/product/product.html',
                    settings: {

                    }
                }
            }
            , {
                url: '/data/invintory',
                config: {
                    title: 'إدخال بيانات',
                    templateUrl: 'app/controller/data/invintory/invintory.html',
                    settings: {

                    }
                }
            }, {
                url: '/data/invintory/products/:id',
                config: {
                    title: 'إدخال بيانات',
                    templateUrl: 'app/controller/data/invintory/invintoryProduct/invintoryProduct.html',
                    settings: {

                    }
                }
            }, {
                url: '/selles/orders',
                config: {
                    title: 'المبيعات',
                    templateUrl: 'app/controller/selles/order/orders.html',
                    settings: {
                    }
                }
            },
            {
                url: '/selles/orders/orderdetails/:id',
                config:
                {
                    title: 'المبيعات',
                    templateUrl: 'app/controller/selles/order/orderDetails/orderDetails.html',
                    settings:
                    {
                    }
                }
            },
            {
                url: '/statistics',
                config:
                {
                    title: 'الإحصائيات',
                    templateUrl: 'app/controller/statistics/data.html',
                    settings:
                    {
                        nav: 3,
                        content: '<i class="fa fa-dashboard"></i> إحصائيات'
                    }
                }
            },
            {
                url: '/statistics/invintory',
                config:
                {
                    title: 'الإحصائيات',
                    templateUrl: 'app/controller/statistics/invintory/invintory.html',
                    settings:
                    {
                    }
                }
            },
            {
                url: '/statistics/profit/:id',
                config:
                {
                    title: 'الإحصائيات',
                    templateUrl: 'app/controller/statistics/profits/profits.html',
                    settings:
                    {
                    }
                }
            },
            {
                url: '/statistics/orders/',
                config:
                {
                    title: 'الإحصائيات',
                    templateUrl: 'app/controller/statistics/order/order.html',
                    settings:
                    {
                    }
                }
            }
         

        ];
    }
})();