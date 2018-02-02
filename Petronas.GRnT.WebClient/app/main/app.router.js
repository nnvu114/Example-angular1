/*
 * @CreateTime: Dec 1, 2017 11:31 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 18, 2018 6:55 PM
 * @Description: Get some config from app\main\configuration.js
 */

(function() {
    'use strict';

    var $viewPathRoot = 'partials';
    var $mainSrc = 'src';
    angular.module('app')
    .run(['$rootScope', '$state', '$stateParams', 'AuthenticateService', 'PET_ROUTE',
        function($rootScope, $state, $stateParams, AuthenticateService, PET_ROUTE) {
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;
            $rootScope.requiresAuth = false;

            $rootScope.$on('$stateChangeStart',
                function(e, toState, toParams, fromState, fromParams) {
                    var isValidState = $state.current.name;
                    if ($state.is(isValidState)) {
                        if (toState.data && toState.data.requiresAuth === true) {
                            $rootScope.requiresAuth = true;
                            var u = AuthenticateService.isLoggedin();
                            if (!u) {
                                e.preventDefault();
                                $state.go(PET_ROUTE.Login);
                            } else if (fromState.abstract === true) {
                                //Check authorization when access or refresh page.
                                AuthenticateService.checkIfRefreshToken();
                            } else {
                                $rootScope.requiresAuth = false;
                            }
                        }
                    }
                }
            );
        }
    ])
    .config(['AnalyticsProvider', 'AppConfig', AnalyticsProviderFn])
    .run(['Analytics', AnalyticsFn])
    .config(function($httpProvider) {
        $httpProvider.defaults.xsrfHeaderName = 'X-XSRF-TOKEN';
        $httpProvider.defaults.xsrfCookieName = 'XSRF-TOKEN';
    })
    .config(['$stateProvider', '$urlRouterProvider', '$locationProvider', 'PET_ROUTE',
        function($stateProvider, $urlRouterProvider, $locationProvider, PET_ROUTE) {

            $urlRouterProvider.otherwise(function($injector, $location) {
                var state = $injector.get('$state');
                var authsv = $injector.get('AuthenticateService');
                if (authsv.isLoggedin()) {
                    state.go('root.home');
                } else {
                    state.go('default.landing');
                }
                return $location.path();
            });

            $stateProvider
            .state(PET_ROUTE.ErrorAbstract, {
                    url: '',
                    abstract: true,
                    views: {}
                })
                .state(PET_ROUTE.NotFound, {
                    url: '/not-found.html',
                    data: {
                        pageTitle: 'Not found'
                    },
                    views: {
                        '@': {
                            templateUrl: $viewPathRoot + '/error/404.html',
                        }
                    }
                })
                .state(PET_ROUTE.DefaultAbstract, {
                    url: '',
                    abstract: true,
                    views: {
                        'header': {
                            templateUrl: $mainSrc + '/widgets/header-df/header.html',
                            controller: 'HeaderDfCtrl',
                            controllerAs: 'vm'
                        },
                        'footer': {
                            templateUrl: $mainSrc + '/widgets/footer-df/footer.html',
                            controller: 'FooterDfCtrl',
                            controllerAs: 'vm'
                        }
                    }
                })
                .state(PET_ROUTE.Landing, {
                    url: '/landing.html',
                    data: {
                        pageTitle: 'Landing page'
                    },
                    views: {
                        '@': {
                            controller: 'LandingCtrl',
                            controllerAs: 'vm',
                            templateUrl: $mainSrc + '/landing/landing.html'
                        }
                    }
                })
                .state(PET_ROUTE.Login, {
                    url: '/login.html',
                    data: {
                        pageTitle: 'Log in'
                    },
                    views: {
                        '@': {
                            controller: 'LoginCtrl',
                            controllerAs: 'vm',
                            templateUrl: $mainSrc + '/user/login/login.html'
                        }
                    }
                })
                .state(PET_ROUTE.Register, {
                    url: '/register.html',
                    data: {
                        pageTitle: 'Register Account'
                    },
                    views: {
                        '@': {
                            controller: 'RegisterCtrl',
                            controllerAs: 'vm',
                            templateUrl: $mainSrc + '/user/register/register.html'
                        }
                    }
                })
            .state(PET_ROUTE.RootAbstract, {
                    url: '',
                    abstract: true,
                    views: {
                        'header': {
                            templateUrl: $mainSrc + '/widgets/header/header.html',
                            controller: 'HeaderCtrl',
                            controllerAs: 'vm',
                        },
                        'footer': {
                            templateUrl: $mainSrc + '/widgets/footer/footer.html',
                            controller: 'FooterCtrl',
                            controllerAs: 'vm',
                        },
                        'left-aside': {
                            templateUrl: $mainSrc + '/widgets/leftaside/leftaside.html',
                            controller: 'LeftAsideCtrl',
                            controllerAs: 'vm'
                        }
                    },
                    data: {
                        requiresAuth: true,
                        pageTitle: 'Home page'
                    }
                })
                .state(PET_ROUTE.HomePage, {
                    url: '/home.html',
                    data: {
                        pageTitle: 'Sample project'
                    },
                    views: {
                        '@': {
                            controller: 'HomeCtrl',
                            controllerAs: 'vm',
                            templateUrl: $mainSrc + '/home/home.html'
                        }
                    }
                })
                .state(PET_ROUTE.ManagementAbstract, {
                    url: '',
                    abstract: true,
                    views: {
                        'header': {
                            templateUrl: $mainSrc + '/widgets/header/header.html',
                            controller: 'HeaderCtrl',
                            controllerAs: 'vm',
                        },
                        'footer': {
                            templateUrl: $mainSrc + '/widgets/footer/footer.html',
                            controller: 'FooterCtrl',
                            controllerAs: 'vm',
                        },
                        'left-aside': {
                            templateUrl: $mainSrc + '/widgets/leftaside/leftaside.html',
                            controller: 'LeftAsideCtrl',
                            controllerAs: 'vm'
                        }
                    },
                    data: {
                        requiresAuth: true,
                        pageTitle: 'Home page'
                    }
                })
                .state(PET_ROUTE.PACListPage, {
                    url: '/pac-list.html',
                    data: {
                        pageTitle: 'PAC project'
                    },
                    views: {
                        '@': {
                            controller: 'UserCtrl',
                            controllerAs: 'vm',
                            templateUrl: $mainSrc + '/admin/user/user.html'
                        }
                    }
                })
                .state(PET_ROUTE.UserPage, {
                    url: '/user.html',
                    data: {
                        pageTitle: 'Sample project'
                    },
                    views: {
                        '@': {
                            controller: 'UserManagementCtrl',
                            controllerAs: 'vm',
                            templateUrl: $mainSrc + '/admin/user.html'
                        }
                    }
                })
            .state(PET_ROUTE.AboutAbstract, {
                    url: '',
                    abstract: true,
                    views: {
                        'header': {
                            templateUrl: $mainSrc + '/widgets/header/header.html',
                            controller: 'HeaderCtrl',
                            controllerAs: 'vm',
                        },
                        'footer': {
                            templateUrl: $mainSrc + '/widgets/footer/footer.html',
                            controller: 'FooterCtrl',
                            controllerAs: 'vm',
                        }
                    },
                    data: {
                        requiresAuth: true,
                        pageTitle: 'About'
                    }
                })
                .state(PET_ROUTE.AboutUs, {
                    url: '/about-us.html',
                    data: {
                        pageTitle: 'About us'
                    },
                    views: {
                        '@': {
                            controller: 'AboutUsCtrl',
                            controllerAs: 'vm',
                            templateUrl: $mainSrc + '/about/aboutus/aboutus.html'
                        }
                    }
                })
                .state(PET_ROUTE.Samples, {
                    url: '/samples.html',
                    data: {
                        pageTitle: 'Samples'
                    },
                    views: {
                        '@': {
                            controller: 'SamplesCtrl',
                            controllerAs: 'vm',
                            templateUrl: $mainSrc + '/about/samples/samples.html'
                        }
                    }
                });

            //check browser support
            if (window.history && window.history.pushState) {
                $locationProvider.html5Mode({
                    enabled: true,
                    requireBase: false
                });
            }
        }
    ]);

    function AnalyticsFn(Analytics) {
        Analytics.pageView();
        Analytics.trackEvent(true);
        Analytics.trackPage('Web accessing', 'User has just accessed our service or reload web page');
    }

    function AnalyticsProviderFn(AnalyticsProvider, AppConfig) {
        AnalyticsProvider.setAccount(AppConfig.GA.tracker);
        AnalyticsProvider.setDomainName(AppConfig.GA.domain);
        AnalyticsProvider.setCurrency(AppConfig.currency);
        AnalyticsProvider.trackPages(true);
        AnalyticsProvider
            .logAllCalls(true)
            .startOffline(false)
            .useECommerce(true, true);
        AnalyticsProvider.useCrossDomainLinker(true);
    }

}());