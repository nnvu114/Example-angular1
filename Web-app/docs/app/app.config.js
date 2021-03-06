(function() {
    'use strict';

    // app.config.js
    function config($locationProvider, $stateProvider, API_DATA, GUIDE_DATA, $urlRouterProvider) {

        // Set HTML5 Mode
        $locationProvider.html5Mode(true);

        // Configure URL Router to redirect to /api
        // if state doesn't exist
        $urlRouterProvider.otherwise('/api');

        // Defining our template for the sidebar
        // Could've been in a partial, but it's simple
        // enough that it can be a string template
        var sidebarTemplate = '<h4>Contents</h4>' +
            '<ol class="list-unstyled">' +
            '<li ng-repeat="page in ctrl.allPages">' +
            '<a href="/documenting-angular-dgeni"></a>' +
            '<ol class="list-unstyled" style="padding-left: 15px;">' +
            '<li ng-repeat="child in page.docs">' +
            '<a href=""></a>' +
            '</li>' +
            '</ol>' +
            '</li>' +
            '</ol>';

        // Assign our root state for API pages to var
        // Assigning the basepage as the partials
        // Setup the sidebar to use our ApiController and template
        var apiState = {
            name: 'api',
            url: '/api',
            views: {
                'main': {
                    templateUrl: 'partials/api.html',
                },
                'sidebar': {
                    template: sidebarTemplate,
                    controller: 'ApiController as ctrl',
                }
            }
        }

        // Same thing for our guide page
        var guideState = {
            name: 'guide',
            url: '/guide',
            views: {
                'main': {
                    templateUrl: 'partials/guide.html'
                },
                'sidebar': {
                    template: sidebarTemplate,
                    controller: 'GuideController as ctrl',
                }
            }
        }

        // Using the $stateProvider from UI-Router
        // to create the states in the application
        $stateProvider.state(apiState);
        $stateProvider.state(guideState);

        // Looping through all of our API pages
        // and dynamically creating new states based
        // on the data generated by Dgeni
        angular.forEach(API_DATA, function(parent) {

            var newState = {
                name: parent.name,
                url: '/' + parent.url,
                views: {
                    'main': {
                        templateUrl: parent.outputPath
                    },
                    'sidebar': {
                        template: sidebarTemplate,
                        controller: 'ApiController as ctrl'
                    }
                }
            };

            // Creating the states using $stateProvider
            $stateProvider.state(newState);

            // In the case of API, we have multiple modules and each
            // of them have children, so we are doing the same thing
            // here but for the child states
            angular.forEach(parent.docs, function(doc) {

                var newState = {
                    name: doc.name,
                    url: '/' + doc.url,
                    views: {
                        'main': {
                            templateUrl: doc.outputPath
                        },
                        'sidebar': {
                            template: sidebarTemplate,
                            controller: 'ApiController as ctrl'
                        }
                    }
                };

                // Creating the states using $stateProvider
                $stateProvider.state(newState);
            });
        });

        // Same thing for Guide, except in this case we only
        // have 'root' pages, so no need to loop twice
        angular.forEach(GUIDE_DATA, function(parent) {

            var newState = {
                name: parent.name,
                url: '/' + parent.url,
                views: {
                    'main': {
                        templateUrl: parent.outputPath
                    },
                    'sidebar': {
                        template: sidebarTemplate,
                        controller: 'GuideController as ctrl'
                    }
                }
            };

            // Creating the states using $stateProvider
            $stateProvider.state(newState);

        });

    }
    config.$inject = ['$locationProvider', '$stateProvider', 'API_DATA', 'GUIDE_DATA', '$urlRouterProvider'];

}());