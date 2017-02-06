(function () {
    "use strict";

    // Declare: Controller with APPNAME module
    angular.module(APPNAME)
        .controller('modalController', ModalController);

    // Inject:
    ModalController.$inject = ['$scope', '$baseController', '$uibModalInstance']

    // Declare: Controller Function
    function ModalController(
        $scope
        , $baseController
        , $uibModalInstance) {

        // Save variables
        var vm = this;
        Dropzone.autoDiscover = false;
        vm.showBtns = false;
        vm.lastFile = null;
        vm.mediaId = null;

        // Initialize 
        vm.$scope = $scope;
        vm.$uibModalInstance = $uibModalInstance;

        // Declare Functions
        vm.dzAddedFile = _dzAddedFile;
        vm.dzSending = _dzSending;
        vm.dzSuccess = _dzSuccess;

        vm.cancel = _cancel;
        vm.ok = _ok;

        // Inherit: View Model with $baseController
        $baseController.merge(vm, $baseController);

        vm.dzOptions = {
            url: '/api/media',
            autoProcessQueue: true,
            uploadMultiple: false,
            parallelUploads: 1,
            maxFiles: 1,
            maxFilesize: 3
        };

        function _dzAddedFile(file) {
            //console.log(file);
        }

        function _dzSending(file, xhr, formData) {
            //console.log('Sending');
        }

        function _dzSuccess(file, response) {
            vm.mediaId = response.item;

        }

        // Success function when review is posted
        function _ok() {
            vm.$uibModalInstance.close(vm.mediaId);
        };

        // Function fires when modal is cancelled before review is posted
        function _cancel() {
            vm.$uibModalInstance.dismiss('cancel');
        };

    }
})();