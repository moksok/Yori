(function () {
    "use strict",

    angular.module(APPNAME)
    .factory('$registrationService', registrationServiceFactory);

    registrationServiceFactory.$inject = ['$baseService', '$sabio'];

    function registrationServiceFactory($baseService, $sabio) {
        
        var aSabioServiceObject = sabio.services.registration;

        var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService)

        return newService;

    }

})();