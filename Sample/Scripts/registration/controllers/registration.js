(function () {
    "use strict";
    
    // Declare: Controller with APPNAME module
    angular.module(APPNAME)
    .controller('registrationController', RegistrationController);

    RegistrationController.$inject = ["$scope", "$baseController", "$registrationService"];
    
    // Declare: Controller Function
    function RegistrationController($scope, $baseController, $registrationService) {

        // Save variables
        var vm = this;
        vm.newUser = null;
        vm.userRegistrationForm = null;
        vm.showRegistrationError = false;

        // Initialize
        vm.$scope = $scope;
        vm.$registrationService = $registrationService;

        // Declaring functions
        vm.submitRegistration = _submitRegistration;
        vm.registrationSuccess = _registrationSuccess;
        vm.registrationError = _registrationError;

        // Inherit: View Model with $baseController
        $baseController.merge(vm, $baseController);

        vm.notify = vm.$registrationService.getNotifier($scope);
        
        render();

        function render() {
            $.backstretch([
		        "../Assets/pages/media/bg/1.jpg",
		        "../Assets/pages/media/bg/2.jpg",
		        "../Assets/pages/media/bg/3.jpg",
		        "../Assets/pages/media/bg/4.jpg"
            ], {
                fade: 1000,
                duration: 8000
            }
        	);
        }

        function _submitRegistration() {
            console.log(vm.newUser);

            vm.showRegistrationError = true;

            if (vm.userRegistrationForm.$valid) {
                vm.$registrationService.insert(vm.newUser, vm.registrationSuccess, vm.registrationError);
            }
            
        }

        function _registrationSuccess(data) {
            console.log('new user registered', data);
            window.location.href = "/portal/home";
        }

        function _registrationError() {
            console.log('registration error');
        }
    }
})();