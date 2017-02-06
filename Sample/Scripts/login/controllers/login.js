(function () {
    "use strict";

    // Declare: Controller with APPNAME module
    angular.module(APPNAME)
    .controller('loginController', LoginController);

    LoginController.$inject = ["$scope", "$baseController", "$loginService"];

    // Declare: Controller Function
    function LoginController($scope, $baseController, $loginService) {

        // Save variables
        var vm = this;
        vm.User = null;

        // Initialize
        vm.$scope = $scope;
        vm.$loginService = $loginService;

        // Declaring functions
        vm.submitLogin = _submitLogin;
        vm.loginSuccess = _loginSuccess;
        vm.loginError = _loginError;

        // Inherit: View Model with $baseController
        $baseController.merge(vm, $baseController);

        vm.notify = vm.$loginService.getNotifier($scope);

        render();

        function render() {
            $.backstretch([
		        "../assets/pages/media/bg/1.jpg",
		        "../assets/pages/media/bg/2.jpg",
		        "../assets/pages/media/bg/3.jpg",
		        "../assets/pages/media/bg/4.jpg"
            ], {
                fade: 1000,
                duration: 8000
            }
        	);
        }

        function _submitLogin() {
            console.log(vm.User);
            vm.$loginService.insert(vm.User, vm.loginSuccess, vm.loginError);
        }

        function _loginSuccess(data) {
            window.location.href = "/portal/home";
        }

        function _loginError() {
            console.log('login error');
        }

    }
})();