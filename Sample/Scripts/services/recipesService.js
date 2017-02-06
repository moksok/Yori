(function () {
    "use strict",

    angular.module(APPNAME)
    .factory('$recipesService', recipesServiceFactory);

    recipesServiceFactory.$inject = ['$baseService', '$sabio'];

    function recipesServiceFactory($baseService, $sabio) {

        var aSabioServiceObject = sabio.services.recipes;

        var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService)

        return newService;

    }

})();