(function () {
    'use strict';

    var tddDemoApp = angular.module('TDDDemoApp', ['ngRoute']);

    tddDemoApp.config(function ($routeProvider) {
        $routeProvider
            // route for the home page
            .when('/', {
                templateUrl: 'Pages/Home.html',
                controller: 'homeController',
                controllerAs: 'ctrl'
            });
    });
})();