(function () {
    "use strict",

    angular.module(APPNAME)
    .factory('$loginService', loginServiceFactory);

    loginServiceFactory.$inject = ['$baseService', '$sabio'];

    function loginServiceFactory($baseService, $sabio) {

        var aSabioServiceObject = sabio.services.login;

        var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService)

        return newService;

    }

})();