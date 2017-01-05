// tripsController.js
(function () {

    "use strict";

    // Getting the existing module
    angular.module("app-trips").controller("tripsController", tripsController);

    function tripsController($http) {
        var vm = this;

        vm.name = "Paradigma";

        //vm.trips = [{
        //    name: "US Trip",
        //    created: new Date()
        //}, {
        //    name: "World Trip",
        //    created: new Date()
        //}];

        vm.trips = [];

        vm.newTrip = {};

        vm.errorMessage = "";
        vm.isBusy = true;

        vm.addTrip = function () {
            //alert(vm.newTrip.name);
            vm.trips.push({ name: vm.newTrip.name, created: new Date() })
            vm.newTrip = {};
        };

        $http.get("/api/trips")
        .then(function (response, error) {
            // Success
            angular.copy(response.data, vm.trips);
        }, function (error) {
            // Failure
            vm.errorMessage = "Failed to load data: " + error;
        })
        .finally(function () {
            vm.isBusy = false;
        });
    }

})();