(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('myProfileController', MyProfileController);

    MyProfileController.$inject = ['$scope', '$baseController', '$usersService'];

    function MyProfileController(
        $scope
        , $baseController
        , $usersService) {

        var vm = this;
        vm.currentUser = null;
        vm.editCurrentUser = {};
        vm.editCurrentUserForm = null;

        $baseController.merge(vm, $baseController);

        vm.$scope = $scope;
        vm.$usersService = $usersService;

        vm.notify = vm.$usersService.getNotifier($scope);

        vm.renderCurrentUserInfo = _renderCurrentUserInfo;
        vm.getCurrentUserInfoError = _getCurrentUserInfoError;
        vm.editCurrentUserInfo = _editCurrentUserInfo;
        vm.editCurrentUserInfoSuccess = _editCurrentUserInfoSuccess;
        vm.editCurrentUserInfoError = _editCurrentUserInfoError;

        render();

        function render() {

            vm.$usersService.getCurrentUserInfo(vm.renderCurrentUserInfo, vm.getCurrentUserInfoError);
        }

        function _renderCurrentUserInfo(data) {
            if (data !== null) {
                vm.notify(function () {
                    vm.currentUser = data.item;
                });
            }

            console.log(vm.currentUser);
            
        }

        function _getCurrentUserInfoError() {
            console.log("user info did not load");
        }

        function _editCurrentUserInfo() {

            vm.$usersService.updateCurrentUserInfo(vm.editCurrentUser, vm.editCurrentUserInfoSuccess, vm.editCurrentUserInfoError)
        }

        function _editCurrentUserInfoSuccess() {
            $("#editCurrentUserForm").trigger('reset');
            render();
        }

        function _editCurrentUserInfoError() {
            console.log('user did not update error');
        }
    }
})();
