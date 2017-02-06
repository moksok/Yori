(function () {
    "use strict";

    angular.module(APPNAME)
        .config(["$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider) {

            $routeProvider

            .when('/myprofile', {
                templateUrl: '/Scripts/sabio/yori/portal/templates/myProfileTemplate.html',
                controller: 'myProfileController as mpc',
            })

            
            .when('/myrecipes', {
                templateUrl: '/Scripts/sabio/yori/portal/templates/myRecipesTemplate.html',
                controller: 'myRecipesController as mrc',
            })
            
            .when('/allrecipes', {
                templateUrl: '/Scripts/sabio/yori/portal/templates/allRecipesTemplate.html',
                controller: 'allRecipesController as arc',
            })

            $locationProvider.html5Mode(false);

        }]);
})();