// tripEditorController.js
(function () {
    "use strict";

    angular.module("app-trips")
        .controller("tripEditorController", tripEditorController);
    
    function tripEditorController($routeParams, $http) {
        var vm = this;

        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/trips/" + vm.tripName + "/stops")
        .then(function (response) {
            // Success
            angular.copy(response.data, vm.stops);
        }, function (error) {
            // Failure
            vm.errorMessage = "Failed to load stops: " + error;
        })
        .finally(function () {
            vm.isBusy = false;
        });
    }

})();