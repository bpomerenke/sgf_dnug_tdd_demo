(function () {
    'use strict';

    var tddDemoApp = angular.module('TDDDemoApp');


    tddDemoApp.controller('homeController', function ($http) {

        var vm = this;
        vm.loaded = false;
        vm.messageText = "";
        vm.messages = [];

        vm.init = function() {
            $http({ method: 'GET', url: 'api/v1/messages'}).then(function (response) {
                vm.messages = response.data;
                vm.loaded = true;
            }, function () {
                console.log("failed");
            });
        }

        vm.addMessage = function () {
            $http({ method: 'POST', url: 'api/v1/message', data: { message: vm.messageText } }).then(function (response) {
                //TODO
            }, function() {
                console.log("failed");
            });
        }
    });
})();
