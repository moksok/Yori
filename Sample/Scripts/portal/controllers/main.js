(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('mainController', MainController);

    MainController.$inject = ['$scope', '$baseController', '$usersService'];

    function MainController(
        $scope
        , $baseController
        , $usersService) {

        var vm = this;
        vm.tabs = [
            { link: '#/myprofile', label: 'My Profile' },
            { link: '#/myrecipes', label: 'My Recipes' }

        ];
        vm.selectedTab = vm.tabs[0];
        vm.currentUser = null;

        $baseController.merge(vm, $baseController);

        vm.$scope = $scope;
        vm.$usersService = $usersService;

        vm.notify = vm.$usersService.getNotifier($scope);

        vm.getCurrentUserInfoSuccess = getCurrentUserInfoSuccess;
        vm.getCurrentUserInfoError = getCurrentUserInfoError;

        vm.currentRequestLabel = "Current Request:";
        vm.tabClass = _tabClass;
        vm.setSelectedTab = _setSelectedTab;

        render();

        function render() {
            vm.setUpCurrentRequest(vm);
            vm.$usersService.getCurrentUserInfo(vm.getCurrentUserInfoSuccess, vm.getCurrentUserInfoError);
        }

        function _tabClass(tab) {
            if (vm.selectedTab == tab) {
                return "active";
            } else {
                return "";
            }
        }

        function _setSelectedTab(tab) {
            vm.selectedTab = tab;
        }

        function getCurrentUserInfoSuccess(data) {
            vm.notify(function () {
                vm.currentUser = data.item;
            });
        }

        function getCurrentUserInfoError() {
            window.location.href = "/login/index";
        };

    }
})();